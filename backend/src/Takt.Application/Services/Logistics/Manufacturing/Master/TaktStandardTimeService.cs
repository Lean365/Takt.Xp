//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktStandardTimeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 标准工时服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Manufacturing.Master;
using Takt.Domain.Entities.Logistics.Manufacturing.Master;
using Takt.Domain.IServices.SignalR;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准工时服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktStandardTimeService : TaktBaseService, ITaktStandardTimeService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取标准工时仓储
        /// </summary>
        private ITaktRepository<TaktStandardTime> StandardTimeRepository => _repositoryFactory.GetBusinessRepository<TaktStandardTime>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktStandardTimeService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            ITaktSignalRClient signalRClient,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient;
        }

        /// <summary>
        /// 获取标准工时分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktStandardTimeDto>> GetListAsync(TaktStandardTimeQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await StandardTimeRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktStandardTimeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktStandardTimeDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 获取标准工时详情
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>返回标准工时详情</returns>
        public async Task<TaktStandardTimeDto> GetByIdAsync(long standardTimeId)
        {
            var standardTime = await StandardTimeRepository.GetByIdAsync(standardTimeId);
            if (standardTime == null)
                throw new TaktException(L("StandardTime.NotFound", standardTimeId));

            return standardTime.Adapt<TaktStandardTimeDto>();
        }

        /// <summary>
        /// 创建标准工时
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回标准工时ID</returns>
        public async Task<long> CreateAsync(TaktStandardTimeCreateDto input)
        {
            var standardTime = input.Adapt<TaktStandardTime>();

            // 计算转换后标准工时
            standardTime.ConvertedMinutes = CalculateConvertedMinutes(
                standardTime.StandardMinutes,
                standardTime.StandardShorts,
                standardTime.PointsToMinutesRate);

            var result = await StandardTimeRepository.CreateAsync(standardTime);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    result,
                    1, // 新增
                    "创建标准工时",
                    null,
                    null,
                    null,
                    null,
                    null);
            }

            return result;
        }

        /// <summary>
        /// 更新标准工时
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktStandardTimeUpdateDto input)
        {
            var standardTime = await StandardTimeRepository.GetByIdAsync(input.StandardTimeId);
            if (standardTime == null)
                throw new TaktException(L("StandardTime.NotFound", input.StandardTimeId));

            // 保存更新前的值用于变更记录
            var beforeValue = JsonConvert.SerializeObject(standardTime);

            input.Adapt(standardTime);

            // 重新计算转换后标准工时
            standardTime.ConvertedMinutes = CalculateConvertedMinutes(
                standardTime.StandardMinutes,
                standardTime.StandardShorts,
                standardTime.PointsToMinutesRate);

            var result = await StandardTimeRepository.UpdateAsync(standardTime);

            // 创建变更记录
            if (result > 0)
            {
                var afterValue = JsonConvert.SerializeObject(standardTime);
                await CreateChangeLogAsync(
                    input.StandardTimeId,
                    2, // 修改
                    "更新标准工时",
                    beforeValue,
                    afterValue,
                    null,
                    null,
                    null);
            }

            return result > 0;
        }

        /// <summary>
        /// 删除标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long standardTimeId)
        {
            var standardTime = await StandardTimeRepository.GetByIdAsync(standardTimeId);
            if (standardTime == null)
                throw new TaktException(L("StandardTime.NotFound", standardTimeId));

            // 保存删除前的值用于变更记录
            var beforeValue = JsonConvert.SerializeObject(standardTime);

            var result = await StandardTimeRepository.DeleteAsync(standardTime);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    standardTimeId,
                    3, // 删除
                    "删除标准工时",
                    beforeValue,
                    null,
                    null,
                    null,
                    null);
            }

            return result > 0;
        }

        /// <summary>
        /// 批量删除标准工时
        /// </summary>
        /// <param name="ids">标准工时ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("StandardTime.SelectToDelete"));

            var standardTimes = await StandardTimeRepository.GetListAsync(x => ids.Contains(x.Id));
            if (!standardTimes.Any())
                throw new TaktException(L("StandardTime.NotFound"));

            var result = await StandardTimeRepository.DeleteAsync(standardTimes);
            return result > 0;
        }

        /// <summary>
        /// 导入标准工时数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "标准工时信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktStandardTimeImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var standardTime = dto.Adapt<TaktStandardTime>();

                    // 计算转换后标准工时
                    standardTime.ConvertedMinutes = CalculateConvertedMinutes(
                        standardTime.StandardMinutes,
                        standardTime.StandardShorts,
                        standardTime.PointsToMinutesRate);

                    await StandardTimeRepository.CreateAsync(standardTime);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("StandardTime.ImportFailed", dto.MaterialCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出标准工时数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktStandardTimeQueryDto query, string sheetName = "标准工时信息")
        {
            var predicate = QueryExpression(query);

            var standardTimes = await StandardTimeRepository.AsQueryable()
                .Where(predicate)
                .OrderByDescending(x => x.Id)
                .ToListAsync();

            var exportDtos = standardTimes.Adapt<List<TaktStandardTimeExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "标准工时信息")
        {
            var template = new List<TaktStandardTimeTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 更新标准工时状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktStandardTimeStatusDto input)
        {
            var standardTime = await StandardTimeRepository.GetByIdAsync(input.Id);
            if (standardTime == null)
                throw new TaktException(L("StandardTime.NotFound", input.Id));

            var oldStatus = standardTime.Status;
            standardTime.Status = input.Status;
            var result = await StandardTimeRepository.UpdateAsync(standardTime);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    input.Id,
                    4, // 状态变更
                    $"状态变更: {oldStatus} -> {input.Status}",
                    null,
                    null,
                    "Status",
                    oldStatus.ToString(),
                    input.Status.ToString());
            }

            return result > 0;
        }

        /// <summary>
        /// 获取标准工时选项列表
        /// </summary>
        /// <param name="materialCode">物料编码</param>
        /// <returns>标准工时选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync(string materialCode)
        {
            if (string.IsNullOrEmpty(materialCode))
                throw new TaktException("物料编码不能为空");

            var standardTimes = await StandardTimeRepository.GetListAsync(x => x.Status == 0 && x.MaterialCode == materialCode);
            return standardTimes.Select(x => new TaktSelectOption
            {
                DictLabel = $"{x.MaterialCode} - {x.WorkCenter}",
                DictValue = x.StandardMinutes,
                ExtLabel = $"{x.MaterialCode} - {x.WorkCenter}",
                ExtValue = x.StandardShorts,
                TransKey = null,
                CssClass = 1,
                ListClass = 1,
                OrderNum = (int)x.Id,
                Status = x.Status
            }).ToList();
        }



        /// <summary>
        /// 审核标准工时
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <param name="approver">审核人</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveAsync(long standardTimeId, string approver)
        {
            var standardTime = await StandardTimeRepository.GetByIdAsync(standardTimeId);
            if (standardTime == null)
                throw new TaktException(L("StandardTime.NotFound", standardTimeId));

            standardTime.Approver = approver;
            standardTime.ApprovalDate = DateTime.Now;
            var result = await StandardTimeRepository.UpdateAsync(standardTime);

            // 创建变更记录
            if (result > 0)
            {
                await CreateChangeLogAsync(
                    standardTimeId,
                    4, // 审核
                    $"审核通过 - 审核人: {approver}",
                    null,
                    null,
                    "Approver",
                    null,
                    approver);
            }

            return result > 0;
        }

        /// <summary>
        /// 计算转换后标准工时
        /// </summary>
        /// <param name="standardMinutes">标准工时(分钟)</param>
        /// <param name="standardShorts">标准点数</param>
        /// <param name="pointsToMinutesRate">点数转分钟汇率</param>
        /// <returns>转换后标准工时(分钟)</returns>
        public decimal CalculateConvertedMinutes(decimal standardMinutes, int standardShorts, decimal pointsToMinutesRate)
        {
            // 转换公式: 标准工时 + (标准点数 * 点数转分钟汇率)
            return standardMinutes + (standardShorts * pointsToMinutesRate);
        }

        /// <summary>
        /// 获取标准工时变更记录
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <returns>变更记录列表</returns>
        public async Task<List<TaktStandardTimeChangeLogDto>> GetChangeLogsAsync(long standardTimeId)
        {
            // 获取标准工时变更记录仓储
            var changeLogRepository = _repositoryFactory.GetBusinessRepository<TaktStandardTimeChangeLog>();

            // 查询指定标准工时的变更记录，按变更日期降序排列
            var changeLogs = await changeLogRepository.GetListAsync(
                x => x.StandardTimeId == standardTimeId && x.Status == 1);

            // 按变更日期降序排序
            changeLogs = changeLogs.OrderByDescending(x => x.ChangeDate).ToList();

            // 转换为DTO并返回
            return changeLogs.Adapt<List<TaktStandardTimeChangeLogDto>>();
        }

        /// <summary>
        /// 创建标准工时变更记录
        /// </summary>
        /// <param name="standardTimeId">标准工时ID</param>
        /// <param name="changeType">变更类型</param>
        /// <param name="changeReason">变更原因</param>
        /// <param name="beforeValue">变更前值</param>
        /// <param name="afterValue">变更后值</param>
        /// <param name="changeField">变更字段</param>
        /// <param name="beforeFieldValue">变更前字段值</param>
        /// <param name="afterFieldValue">变更后字段值</param>
        /// <returns>变更记录ID</returns>
        private async Task<long> CreateChangeLogAsync(
            long standardTimeId,
            int changeType,
            string? changeReason = null,
            string? beforeValue = null,
            string? afterValue = null,
            string? changeField = null,
            string? beforeFieldValue = null,
            string? afterFieldValue = null)
        {
            try
            {
                // 获取标准工时信息
                var standardTime = await StandardTimeRepository.GetByIdAsync(standardTimeId);
                if (standardTime == null)
                    return 0;

                // 获取变更记录仓储
                var changeLogRepository = _repositoryFactory.GetBusinessRepository<TaktStandardTimeChangeLog>();

                // 创建变更记录
                var changeLog = new TaktStandardTimeChangeLog
                {
                    StandardTimeId = standardTimeId,
                    PlantCode = standardTime.PlantCode,
                    MaterialCode = standardTime.MaterialCode,
                    WorkCenter = standardTime.WorkCenter,
                    OperationDesc = standardTime.OperationDesc,
                    StandardMinutes = standardTime.StandardMinutes,
                    StandardShorts = standardTime.StandardShorts,
                    PointsToMinutesRate = standardTime.PointsToMinutesRate,
                    ConvertedMinutes = standardTime.ConvertedMinutes,
                    TimeUnit = standardTime.TimeUnit,
                    PointsUnit = standardTime.PointsUnit,
                    EffectiveDate = standardTime.EffectiveDate,
                    ExpiryDate = standardTime.ExpiryDate,
                    ChangeType = changeType,
                    ChangeDate = DateTime.Now,
                    ChangeUser = _currentUser.UserName ?? "System",
                    ChangeReason = changeReason,
                    BeforeValue = beforeValue,
                    AfterValue = afterValue,
                    Status = 1
                };

                // 保存变更记录
                var result = await changeLogRepository.CreateAsync(changeLog);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"创建标准工时变更记录失败: StandardTimeId={standardTimeId}, ChangeType={changeType}", ex);
                return 0;
            }
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktStandardTime, bool>> QueryExpression(TaktStandardTimeQueryDto query)
        {
            var exp = Expressionable.Create<TaktStandardTime>();

            if (query != null)
            {
                // PlantCode: -1 表示显示全部
                if (!string.IsNullOrEmpty(query.PlantCode) && query.PlantCode != "-1")
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.MaterialCode))
                    exp = exp.And(x => x.MaterialCode.Contains(query.MaterialCode));

                // WorkCenter: -1 表示显示全部
                if (!string.IsNullOrEmpty(query.WorkCenter) && query.WorkCenter != "-1")
                    exp = exp.And(x => x.WorkCenter.Contains(query.WorkCenter));

                if (!string.IsNullOrEmpty(query.OperationDesc))
                    exp = exp.And(x => x.OperationDesc.Contains(query.OperationDesc));

                // Status: -1 表示显示全部
                if (query.Status.HasValue && query.Status.Value != -1)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (!string.IsNullOrEmpty(query.Approver))
                    exp = exp.And(x => x.Approver.Contains(query.Approver));

                // 使用StartDate和EndDate进行日期范围查询
                if (query.StartDate.HasValue)
                    exp = exp.And(x => x.EffectiveDate >= query.StartDate.Value);

                if (query.EndDate.HasValue)
                    exp = exp.And(x => x.EffectiveDate <= query.EndDate.Value);
            }

            return exp.ToExpression();
        }
    }
}





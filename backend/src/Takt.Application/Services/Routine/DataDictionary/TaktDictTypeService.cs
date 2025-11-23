#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictTypeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述   : 字典类型服务实现类
//===================================================================

using Takt.Application.Dtos.Routine.DataDictionary;
using Takt.Domain.Entities.Routine.DataDictionary;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.DataDictionary
{
    /// <summary>
    /// 字典类型服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class TaktDictTypeService : TaktBaseService, ITaktDictTypeService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取字典类型仓储
        /// </summary>
        private ITaktRepository<TaktDictType> DictTypeRepository => _repositoryFactory.GetBusinessRepository<TaktDictType>();

        /// <summary>
        /// 获取字典数据仓储
        /// </summary>
        private ITaktRepository<TaktDictData> DictDataRepository => _repositoryFactory.GetBusinessRepository<TaktDictData>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktDictTypeService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取字典类型分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典类型分页列表</returns>
        public async Task<TaktPagedResult<TaktDictTypeDto>> GetListAsync(TaktDictTypeQueryDto? query)
        {
            query ??= new TaktDictTypeQueryDto();

            var result = await DictTypeRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktDictTypeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktDictTypeDto>>()
            };
        }

        /// <summary>
        /// 获取字典类型详情
        /// </summary>
        /// <param name="id">字典类型ID</param>
        /// <returns>字典类型详情</returns>
        public async Task<TaktDictTypeDto> GetByIdAsync(long id)
        {
            var dictType = await DictTypeRepository.GetByIdAsync(id);
            if (dictType == null)
                throw new TaktException(L("Core.DictType.NotFound", id));

            var dictTypeDto = dictType.Adapt<TaktDictTypeDto>();

            // 获取子表数据
            var dictDataList = await DictDataRepository.GetListAsync(x => x.DictType == dictType.DictType);
            dictTypeDto.DictDataList = dictDataList.Adapt<List<TaktDictDataDto>>();

            return dictTypeDto;
        }

        /// <summary>
        /// 根据字典类型获取详情
        /// </summary>
        /// <param name="type">字典类型</param>
        /// <returns>字典类型详情</returns>
        public async Task<TaktDictTypeDto> GetByTypeAsync(string type)
        {
            var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == type);
            return dictType == null ? throw new TaktException(L("Core.DictType.NotFound", type)) : dictType.Adapt<TaktDictTypeDto>();
        }

        /// <summary>
        /// 创建字典类型
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典类型ID</returns>
        public async Task<long> CreateAsync(TaktDictTypeCreateDto input)
        {
            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(DictTypeRepository, "DictName", input.DictName);
            await TaktValidateUtils.ValidateFieldExistsAsync(DictTypeRepository, "DictType", input.DictType);

            var dictType = input.Adapt<TaktDictType>();
            var dictTypeId = await DictTypeRepository.CreateAsync(dictType);

            if (dictTypeId <= 0)
                throw new TaktException(L("Core.DictType.CreateFailed"));

            // 创建子表数据
            if (input.DictDataList != null && input.DictDataList.Any())
            {
                foreach (var dictDataDto in input.DictDataList)
                {
                    var dictData = dictDataDto.Adapt<TaktDictData>();
                    await DictDataRepository.CreateAsync(dictData);
                }
            }

            return dictTypeId;
        }

        /// <summary>
        /// 更新字典类型
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktDictTypeUpdateDto input)
        {
            var dictType = await DictTypeRepository.GetByIdAsync(input.DictTypeId)
                ?? throw new TaktException(L("Core.DictType.NotFound", input.DictTypeId));

            // 验证字段是否已存在
            if (dictType.DictName != input.DictName)
                await TaktValidateUtils.ValidateFieldExistsAsync(DictTypeRepository, "DictName", input.DictName, input.DictTypeId);
            if (dictType.DictType != input.DictType)
                await TaktValidateUtils.ValidateFieldExistsAsync(DictTypeRepository, "DictType", input.DictType, input.DictTypeId);

            input.Adapt(dictType);
            var updateResult = await DictTypeRepository.UpdateAsync(dictType) > 0;

            // 更新子表数据
            if (updateResult && input.DictDataList != null)
            {
                // 删除现有子表数据
                await DictDataRepository.DeleteAsync(x => x.DictType == input.DictType);

                // 创建新的子表数据
                foreach (var dictDataDto in input.DictDataList)
                {
                    var dictData = dictDataDto.Adapt<TaktDictData>();
                    await DictDataRepository.CreateAsync(dictData);
                }
            }

            return updateResult;
        }

        /// <summary>
        /// 删除字典类型
        /// </summary>
        /// <param name="dictTypeId">字典类型ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long dictTypeId)
        {
            var dictType = await DictTypeRepository.GetByIdAsync(dictTypeId)
                ?? throw new TaktException(L("Core.DictType.NotFound", dictTypeId));

            if (dictType.IsBuiltin == 1)
                throw new TaktException(L("Core.DictType.CannotDeleteBuiltin"));

            // 删除子表数据
            await DictDataRepository.DeleteAsync(x => x.DictType == dictType.DictType);

            // 删除主表数据
            return await DictTypeRepository.DeleteAsync(dictTypeId) > 0;
        }

        /// <summary>
        /// 批量删除字典类型
        /// </summary>
        /// <param name="dictTypeIds">字典类型ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictTypeIds)
        {
            if (dictTypeIds == null || dictTypeIds.Length == 0) return false;
            return await DictTypeRepository.DeleteRangeAsync(dictTypeIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入字典类型数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var dictTypes = await TaktExcelHelper.ImportAsync<TaktDictTypeImportDto>(fileStream, sheetName);
            if (dictTypes == null || !dictTypes.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var dictType in dictTypes)
            {
                try
                {
                    // 检查是否已存在相同DictType的记录
                    var existingDictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);

                    if (existingDictType != null)
                    {
                        // 更新现有记录
                        existingDictType.DictName = dictType.DictName;
                        existingDictType.DictSource = dictType.DictSource;
                        existingDictType.SqlScript = dictType.SqlScript;
                        existingDictType.OrderNum = dictType.OrderNum;
                        existingDictType.DictStatus = dictType.DictStatus;
                        existingDictType.UpdateBy = _currentUser.UserName;
                        existingDictType.UpdateTime = DateTime.Now;

                        await DictTypeRepository.UpdateAsync(existingDictType);
                    }
                    else
                    {
                        // 创建新记录
                        var newDictType = dictType.Adapt<TaktDictType>();
                        newDictType.CreateBy = _currentUser.UserName;
                        newDictType.CreateTime = DateTime.Now;

                        await DictTypeRepository.CreateAsync(newDictType);
                    }

                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.DictType.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典类型数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktDictTypeQueryDto query, string sheetName)
        {
            var list = await DictTypeRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktDictTypeExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportList, sheetName, L("Core.DictType.ExportTitle"));
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktDictTypeTemplateDto>(sheetName);
        }

        /// <summary>
        /// 执行字典SQL脚本
        /// </summary>
        /// <param name="sqlScript">SQL脚本</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<TaktDictDataDto>> ExecuteDictSqlAsync(string sqlScript)
        {
            var result = await DictDataRepository.GetListAsync(x => true);
            return await Task.FromResult(result.Adapt<List<TaktDictDataDto>>());
        }

        /// <summary>
        /// 主子表联合导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportMasterDetailAsync(Stream fileStream, string sheetName)
        {
            var dictTypes = await TaktExcelHelper.ImportAsync<TaktDictTypeImportDto>(fileStream, sheetName);
            if (dictTypes == null || !dictTypes.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var dictType in dictTypes)
            {
                try
                {
                    // 检查是否已存在相同DictType的记录
                    var existingDictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == dictType.DictType);
                    long dictTypeId;

                    if (existingDictType != null)
                    {
                        // 更新现有记录
                        existingDictType.DictName = dictType.DictName;
                        existingDictType.DictSource = dictType.DictSource;
                        existingDictType.SqlScript = dictType.SqlScript;
                        existingDictType.OrderNum = dictType.OrderNum;
                        existingDictType.DictStatus = dictType.DictStatus;
                        existingDictType.UpdateBy = _currentUser.UserName;
                        existingDictType.UpdateTime = DateTime.Now;

                        await DictTypeRepository.UpdateAsync(existingDictType);
                        dictTypeId = existingDictType.Id;
                    }
                    else
                    {
                        // 创建新记录
                        var newDictType = dictType.Adapt<TaktDictType>();
                        newDictType.CreateBy = _currentUser.UserName;
                        newDictType.CreateTime = DateTime.Now;

                        dictTypeId = await DictTypeRepository.CreateAsync(newDictType);
                    }

                    // 如果有子表数据，也一并导入
                    if (dictType.DictDataList != null && dictType.DictDataList.Any())
                    {
                        foreach (var dictDataDto in dictType.DictDataList)
                        {
                            // 检查是否已存在相同DictType和DictValue的记录
                            var existingDictData = await DictDataRepository.GetFirstAsync(x =>
                                x.DictType == dictDataDto.DictType && x.DictValue == dictDataDto.DictValue);

                            if (existingDictData != null)
                            {
                                // 更新现有记录
                                existingDictData.DictType = dictType.DictType;
                                existingDictData.DictLabel = dictDataDto.DictLabel;
                                existingDictData.ExtLabel = dictDataDto.ExtLabel;
                                existingDictData.ExtValue = dictDataDto.ExtValue;
                                existingDictData.TransKey = dictDataDto.TransKey;
                                existingDictData.CssClass = dictDataDto.CssClass;
                                existingDictData.ListClass = dictDataDto.ListClass;
                                existingDictData.OrderNum = dictDataDto.OrderNum;
                                existingDictData.Remark = dictDataDto.Remark;
                                existingDictData.UpdateBy = _currentUser.UserName;
                                existingDictData.UpdateTime = DateTime.Now;

                                await DictDataRepository.UpdateAsync(existingDictData);
                            }
                            else
                            {
                                // 创建新记录
                                var newDictData = dictDataDto.Adapt<TaktDictData>();
                                newDictData.DictType = dictType.DictType;
                                newDictData.CreateBy = _currentUser.UserName;
                                newDictData.CreateTime = DateTime.Now;

                                await DictDataRepository.CreateAsync(newDictData);
                            }
                        }
                    }

                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.DictType.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 更新字典类型状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktDictTypeStatusDto input)
        {
            var dictType = await DictTypeRepository.GetByIdAsync(input.DictTypeId)
                ?? throw new TaktException(L("Core.DictType.NotFound", input.DictTypeId));

            dictType.DictStatus = input.DictStatus;
            return await DictTypeRepository.UpdateAsync(dictType) > 0;
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktDictType, bool>> QueryExpression(TaktDictTypeQueryDto query)
        {
            return Expressionable.Create<TaktDictType>()
                .AndIF(!string.IsNullOrEmpty(query.DictName), x => x.DictName!.Contains(query.DictName!))
                .AndIF(!string.IsNullOrEmpty(query.DictType), x => x.DictType!.Contains(query.DictType!))
                .AndIF(query.DictStatus != -1, x => x.DictStatus == query.DictStatus)
                .AndIF(query.IsBuiltin != -1, x => x.IsBuiltin == query.IsBuiltin)
                .ToExpression();
        }
    }
}





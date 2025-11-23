#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExceptionLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 异常日志服务实现
//===================================================================

using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 异常日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktExceptionLogService : TaktBaseService, ITaktExceptionLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private ITaktRepository<TaktExceptionLog> ExceptionLogRepository => _repositoryFactory.GetAuthRepository<TaktExceptionLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktExceptionLogService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktExceptionLog, bool>> QueryExpression(TaktExceptionLogQueryDto query)
        {
            return Expressionable.Create<TaktExceptionLog>()
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName!))
                .AndIF(!string.IsNullOrEmpty(query.Method), x => x.Method.Contains(query.Method!))
                .AndIF(!string.IsNullOrEmpty(query.ExceptionType), x => x.ExceptionType.Contains(query.ExceptionType!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取异常日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktExceptionLogDto>> GetListAsync(TaktExceptionLogQueryDto? query)
        {
            query ??= new TaktExceptionLogQueryDto();

            var result = await ExceptionLogRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktExceptionLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktExceptionLogDto>>()
            };
        }

        /// <summary>
        /// 获取异常日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回异常日志详情</returns>
        public async Task<TaktExceptionLogDto> GetByIdAsync(long logId)
        {
            var log = await ExceptionLogRepository.GetByIdAsync(logId);
            return log == null ? throw new TaktException(L("Audit.ExceptionLog.NotFound", logId)) : log.Adapt<TaktExceptionLogDto>();
        }

        /// <summary>
        /// 导出异常日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktExceptionLogQueryDto query, string sheetName = "ExceptionLog")
        {
            try
            {
                var list = await ExceptionLogRepository.GetListAsync(QueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktExceptionLogExportDto>>(), sheetName, L("Audit.ExceptionLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.ExceptionLog.ExportFailed", ex.Message), ex);
                throw new TaktException(L("Audit.ExceptionLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空异常日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearUpAsync()
        {
            try
            {
                var result = await ExceptionLogRepository.DeleteAsync((Expression<Func<TaktExceptionLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.ExceptionLog.ClearFailed", ex.Message), ex);
                throw new TaktException(L("Audit.ExceptionLog.ClearFailed"));
            }
        }
    }
}





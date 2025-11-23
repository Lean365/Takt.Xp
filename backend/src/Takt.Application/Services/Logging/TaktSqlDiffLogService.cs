#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSqlDiffLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 差异日志服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 差异日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktSqlDiffLogService : TaktBaseService, ITaktSqlDiffLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private ITaktRepository<TaktSqlDiffLog> SqlDiffLogRepository => _repositoryFactory.GetAuthRepository<TaktSqlDiffLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">差异日志仓储</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktSqlDiffLogService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取SQL差异日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktSqlDiffLogDto>> GetListAsync(TaktSqlDiffLogQueryDto? query)
        {
            query ??= new TaktSqlDiffLogQueryDto();

            var result = await SqlDiffLogRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktSqlDiffLogDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktSqlDiffLogDto>>()
            };
        }

        /// <summary>
        /// 获取SQL差异日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回SQL差异日志详情</returns>
        public async Task<TaktSqlDiffLogDto> GetByIdAsync(long logId)
        {
            var log = await SqlDiffLogRepository.GetByIdAsync(logId);
            return log == null ? throw new TaktException(L("Audit.SqlDiffLog.NotFound", logId)) : log.Adapt<TaktSqlDiffLogDto>();
        }

        /// <summary>
        /// 导出SQL差异日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktSqlDiffLogQueryDto query, string sheetName = "SqlDiffLog")
        {
            try
            {
                var list = await SqlDiffLogRepository.GetListAsync(QueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktSqlDiffLogExportDto>>(), sheetName, L("Audit.SqlDiffLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.SqlDiffLog.ExportFailed", ex.Message), ex);
                throw new TaktException(L("Audit.SqlDiffLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空SQL差异日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearUpAsync()
        {
            try
            {
                var result = await SqlDiffLogRepository.DeleteAsync((Expression<Func<TaktSqlDiffLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.SqlDiffLog.ClearFailed", ex.Message), ex);
                throw new TaktException(L("Audit.SqlDiffLog.ClearFailed"));
            }
        }
        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktSqlDiffLog, bool>> QueryExpression(TaktSqlDiffLogQueryDto query)
        {
            return Expressionable.Create<TaktSqlDiffLog>()
                .AndIF(!string.IsNullOrEmpty(query.TableName), x => x.TableName.Contains(query.TableName))
                .AndIF(!string.IsNullOrEmpty(query.DiffType), x => x.DiffType.Contains(query.DiffType))
                .AndIF(!string.IsNullOrEmpty(query.BusinessData), x => x.BusinessData.Contains(query.BusinessData))
                .AndIF(!string.IsNullOrEmpty(query.OperBy), x => x.OperBy.Contains(query.OperBy))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
}





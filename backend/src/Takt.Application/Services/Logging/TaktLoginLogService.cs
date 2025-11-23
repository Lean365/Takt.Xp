#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLoginLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 登录日志服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logging;
using Takt.Application.Dtos.Logging;
using Takt.Shared.Exceptions;
using Takt.Shared.Utils;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Takt.Domain.IServices.Extensions;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 登录日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-20
    /// </remarks>
    public class TaktLoginLogService : TaktBaseService, ITaktLoginLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private ITaktRepository<TaktLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<TaktLoginLog>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktLoginLogService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));

        }
        /// <summary>
        /// 获取登录日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktLoginLogDto>> GetListAsync(TaktLoginLogQueryDto? query)
        {
            query ??= new TaktLoginLogQueryDto();

            try
            {
                var result = await LoginLogRepository.GetPagedListAsync(
                    QueryExpression(query),
                    query.PageIndex,
                    query.PageSize,
                    x => x.CreateTime,
                    OrderByType.Desc);

                return new TaktPagedResult<TaktLoginLogDto>
                {
                    TotalNum = result.TotalNum,
                    PageIndex = query.PageIndex,
                    PageSize = query.PageSize,
                    Rows = result.Rows.Adapt<List<TaktLoginLogDto>>()
                };
            }
            catch (Exception ex)
            {
                _logger.Error("获取登录日志分页列表失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取登录日志详情
        /// </summary>
        /// <param name="logId">日志ID</param>
        /// <returns>返回登录日志详情</returns>
        public async Task<TaktLoginLogDto> GetByIdAsync(long logId)
        {
            try
            {
                var log = await LoginLogRepository.GetByIdAsync(logId);
                return log == null ? throw new TaktException(L("Audit.LoginLog.NotFound", logId)) : log.Adapt<TaktLoginLogDto>();
            }
            catch (Exception ex)
            {
                _logger.Error("获取登录日志详情失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 导出登录日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktLoginLogQueryDto query, string sheetName = "LoginLog")
        {
            try
            {
                var list = await LoginLogRepository.GetListAsync(QueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktLoginLogExportDto>>(), sheetName, L("Audit.LoginLog.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.LoginLog.ExportFailed", ex.Message), ex);
                throw new TaktException(L("Audit.LoginLog.ExportFailed"));
            }
        }

        /// <summary>
        /// 清空登录日志
        /// </summary>
        /// <returns>返回是否清空成功</returns>
        public async Task<bool> ClearUpAsync()
        {
            try
            {
                var result = await LoginLogRepository.DeleteAsync((Expression<Func<TaktLoginLog, bool>>)(x => true));
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("删除登录日志失败", ex);
                throw;
            }
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>任务</returns>
        public async Task UnlockUserAsync(long userId)
        {
            try
            {
                var exp = Expressionable.Create<TaktLoginLog>();
                exp.And(x => x.UserId == userId);
                
                await LoginLogRepository.DeleteAsync(exp.ToExpression());
            }
            catch (Exception ex)
            {
                _logger.Error(L("Audit.LoginLog.UnlockFailed", userId, ex.Message), ex);
                throw new TaktException(L("Audit.LoginLog.UnlockFailed", userId));
            }
        }
        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktLoginLog, bool>> QueryExpression(TaktLoginLogQueryDto query)
        {
            return Expressionable.Create<TaktLoginLog>()
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName!))
                .AndIF(query.LoginStatus.HasValue, x => x.LoginStatus == query.LoginStatus)
                .AndIF(query.LoginType.HasValue, x => x.LoginType == query.LoginType)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .ToExpression();
        }
    }
} 






//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 项目服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine;
using Takt.Application.Dtos.Routine;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Utils;
using Takt.Domain.Utils;
using Takt.Shared.Constants;
using Takt.Domain.IServices.SignalR;
using Takt.Shared.Enums;

namespace Takt.Application.Services.Routine.Project
{
    /// <summary>
    /// 项目服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktProjectService : TaktBaseService, ITaktProjectService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取项目仓储
        /// </summary>
        private ITaktRepository<TaktProject> ProjectRepository => _repositoryFactory.GetBusinessRepository<TaktProject>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProjectService(
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
        /// 获取项目分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktProjectDto>> GetListAsync(TaktProjectQueryDto query)
        {
            _logger.Info("开始查询项目列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await ProjectRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.Id,
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                _logger.Info("第一条数据：{@FirstRow}", result.Rows.First());
            }

            var dtoResult = new TaktPagedResult<TaktProjectDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktProjectDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>返回项目详情</returns>
        public async Task<TaktProjectDto> GetByIdAsync(long projectId)
        {
            var project = await ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new TaktException(L("Project.NotFound", projectId));

            return project.Adapt<TaktProjectDto>();
        }

        /// <summary>
        /// 创建项目
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回项目ID</returns>
        public async Task<long> CreateAsync(TaktProjectCreateDto input)
        {
            var project = input.Adapt<TaktProject>();
            var result = await ProjectRepository.CreateAsync(project);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Project.Created"),
                    Content = L("Project.CreatedContent", project.ProjectName),
                    Timestamp = DateTime.Now,
                    Data = project
                });
            }
            
            return result;
        }

        /// <summary>
        /// 更新项目
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktProjectUpdateDto input)
        {
            var project = await ProjectRepository.GetByIdAsync(input.ProjectId);
            if (project == null)
                throw new TaktException(L("Project.NotFound", input.ProjectId));

            input.Adapt(project);
            var result = await ProjectRepository.UpdateAsync(project);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Project.Updated"),
                    Content = L("Project.UpdatedContent", project.ProjectName),
                    Timestamp = DateTime.Now,
                    Data = project
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="projectId">项目ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long projectId)
        {
            var project = await ProjectRepository.GetByIdAsync(projectId);
            if (project == null)
                throw new TaktException(L("Project.NotFound", projectId));

            var result = await ProjectRepository.DeleteAsync(project);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Project.Deleted"),
                    Content = L("Project.DeletedContent", project.ProjectName),
                    Timestamp = DateTime.Now,
                    Data = project
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除项目
        /// </summary>
        /// <param name="projectIds">项目ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] projectIds)
        {
            if (projectIds == null || projectIds.Length == 0)
                throw new TaktException(L("Project.SelectToDelete"));

            var projects = await ProjectRepository.GetListAsync(x => projectIds.Contains(x.Id));
            if (!projects.Any())
                throw new TaktException(L("Project.NotFound"));

            var result = await ProjectRepository.DeleteAsync(projects);
            return result > 0;
        }

        /// <summary>
        /// 导入项目数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "项目信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktProjectImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var project = dto.Adapt<TaktProject>();
                    await ProjectRepository.CreateAsync(project);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Project.ImportFailed", dto.ProjectName), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出项目数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktProjectQueryDto query, string sheetName = "项目信息")
        {
            var predicate = QueryExpression(query);

            var projects = await ProjectRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = projects.Adapt<List<TaktProjectExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "项目信息")
        {
            var template = new List<TaktProjectTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktProject, bool>> QueryExpression(TaktProjectQueryDto query)
        {
            var exp = Expressionable.Create<TaktProject>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.ProjectCode))
                    exp = exp.And(x => x.ProjectCode.Contains(query.ProjectCode));

                if (!string.IsNullOrEmpty(query.ProjectName))
                    exp = exp.And(x => x.ProjectName.Contains(query.ProjectName));

                if (query.ProjectType.HasValue)
                    exp = exp.And(x => x.ProjectType == query.ProjectType.Value);

                if (!string.IsNullOrEmpty(query.ProjectCategory))
                    exp = exp.And(x => x.ProjectCategory.Contains(query.ProjectCategory));

                if (query.ProjectStatus.HasValue)
                    exp = exp.And(x => x.ProjectStatus == query.ProjectStatus.Value);

                if (query.ProjectPhase.HasValue)
                    exp = exp.And(x => x.ProjectPhase == query.ProjectPhase.Value);

                if (!string.IsNullOrEmpty(query.ProjectManager))
                    exp = exp.And(x => x.ProjectManager.Contains(query.ProjectManager));

                if (!string.IsNullOrEmpty(query.CustomerName))
                    exp = exp.And(x => x.CustomerName.Contains(query.CustomerName));

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.CreateTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.CreateTime <= query.EndTime.Value);

                if (!string.IsNullOrEmpty(query.Participant))
                    exp = exp.And(x => x.ProjectManager == query.Participant || 
                                     (x.ProjectTeam != null && x.ProjectTeam.Contains(query.Participant)));
            }

            return exp.ToExpression();
        }

        /// <summary>
        /// 获取指定用户参与的项目状态统计
        /// </summary>
        /// <param name="participant">参与者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetParticipantProjectStatisticsAsync(string participant)
        {
            if (string.IsNullOrEmpty(participant))
                throw new ArgumentNullException(nameof(participant));

            var stats = await ProjectRepository.AsQueryable()
                .Where(x => x.ProjectManager == participant || 
                           (x.ProjectTeam != null && x.ProjectTeam.Contains(participant)))
                .GroupBy(x => x.ProjectStatus)
                .Select(x => new { Status = x.ProjectStatus, Count = SqlFunc.AggregateCount(x.ProjectStatus) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
} 





#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine.Schedule;
using Takt.Application.Dtos.Routine.Schedule;
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

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktTeamScheduleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 团队协作日程服务实现
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Application.Services.Routine.Schedule
{
    /// <summary>
    /// 团队协作日程服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktTeamScheduleService : TaktBaseService, ITaktTeamScheduleService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取团队协作日程仓储
        /// </summary>
        private ITaktRepository<TaktTeamSchedule> TeamScheduleRepository => _repositoryFactory.GetBusinessRepository<TaktTeamSchedule>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktTeamScheduleService(
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
        /// 获取团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktTeamScheduleDto>> GetListAsync(TaktTeamScheduleQueryDto query)
        {
            _logger.Info("开始查询团队协作日程列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await TeamScheduleRepository.GetPagedListAsync(
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

            var dtoResult = new TaktPagedResult<TaktTeamScheduleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktTeamScheduleDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取团队协作日程详情
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>返回团队协作日程详情</returns>
        public async Task<TaktTeamScheduleDto> GetByIdAsync(long teamScheduleId)
        {
            var teamSchedule = await TeamScheduleRepository.GetByIdAsync(teamScheduleId);
            if (teamSchedule == null)
                throw new TaktException(L("TeamSchedule.NotFound", teamScheduleId));

            return teamSchedule.Adapt<TaktTeamScheduleDto>();
        }

        /// <summary>
        /// 创建团队协作日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回团队协作日程ID</returns>
        public async Task<long> CreateAsync(TaktTeamScheduleCreateDto input)
        {
            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(TeamScheduleRepository, "TeamScheduleTitle", input.TeamScheduleTitle);

            var teamSchedule = input.Adapt<TaktTeamSchedule>();
            var result = await TeamScheduleRepository.CreateAsync(teamSchedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("TeamSchedule.Created"),
                    Content = L("TeamSchedule.CreatedContent", teamSchedule.TeamScheduleTitle),
                    Timestamp = DateTime.Now,
                    Data = teamSchedule
                });
            }
            
            return result;
        }

        /// <summary>
        /// 更新团队协作日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktTeamScheduleUpdateDto input)
        {
            var teamSchedule = await TeamScheduleRepository.GetByIdAsync(input.TeamScheduleId);
            if (teamSchedule == null)
                throw new TaktException(L("TeamSchedule.NotFound", input.TeamScheduleId));

            // 验证字段是否已存在
            if (teamSchedule.TeamScheduleTitle != input.TeamScheduleTitle)
                await TaktValidateUtils.ValidateFieldExistsAsync(TeamScheduleRepository, "TeamScheduleTitle", input.TeamScheduleTitle, input.TeamScheduleId);

            input.Adapt(teamSchedule);
            var result = await TeamScheduleRepository.UpdateAsync(teamSchedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("TeamSchedule.Updated"),
                    Content = L("TeamSchedule.UpdatedContent", teamSchedule.TeamScheduleTitle),
                    Timestamp = DateTime.Now,
                    Data = teamSchedule
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleId">团队协作日程ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long teamScheduleId)
        {
            var teamSchedule = await TeamScheduleRepository.GetByIdAsync(teamScheduleId);
            if (teamSchedule == null)
                throw new TaktException(L("TeamSchedule.NotFound", teamScheduleId));

            var result = await TeamScheduleRepository.DeleteAsync(teamSchedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("TeamSchedule.Deleted"),
                    Content = L("TeamSchedule.DeletedContent", teamSchedule.TeamScheduleTitle),
                    Timestamp = DateTime.Now,
                    Data = teamSchedule
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除团队协作日程
        /// </summary>
        /// <param name="teamScheduleIds">团队协作日程ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] teamScheduleIds)
        {
            if (teamScheduleIds == null || teamScheduleIds.Length == 0)
                throw new TaktException(L("TeamSchedule.SelectToDelete"));

            var teamSchedules = await TeamScheduleRepository.GetListAsync(x => teamScheduleIds.Contains(x.Id));
            if (!teamSchedules.Any())
                throw new TaktException(L("TeamSchedule.NotFound"));

            var result = await TeamScheduleRepository.DeleteAsync(teamSchedules);
            return result > 0;
        }

        /// <summary>
        /// 导入团队协作日程数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "团队协作日程信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktTeamScheduleImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var teamSchedule = dto.Adapt<TaktTeamSchedule>();
                    await TeamScheduleRepository.CreateAsync(teamSchedule);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("TeamSchedule.ImportFailed", dto.TeamScheduleTitle), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出团队协作日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktTeamScheduleQueryDto query, string sheetName = "团队协作日程信息")
        {
            var predicate = QueryExpression(query);

            var teamSchedules = await TeamScheduleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = teamSchedules.Adapt<List<TaktTeamScheduleExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "团队协作日程信息")
        {
            var template = new List<TaktTeamScheduleTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 获取指定用户的团队协作日程状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy)
        {
            if (string.IsNullOrEmpty(createBy))
                throw new ArgumentNullException(nameof(createBy));

            var stats = await TeamScheduleRepository.AsQueryable()
                .Where(x => x.CreateBy == createBy)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }

        /// <summary>
        /// 获取当前用户的团队协作日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>团队协作日程分页列表</returns>
        public async Task<TaktPagedResult<TaktTeamScheduleDto>> GetMyTeamSchedulesAsync(TaktTeamScheduleQueryDto query)
        {
            _logger.Info("开始查询当前用户团队协作日程列表，查询条件：{@Query}", query);

            // 设置当前用户为创建者
            query.CreateBy = _currentUser.UserName;

            // 构建查询表达式
            var exp = Expressionable.Create<TaktTeamSchedule>();
            
            // 基础条件：当前用户创建的团队协作日程
            exp = exp.And(x => x.CreateBy == _currentUser.UserName);
            
            // 时间范围查询：如果提供了时间范围，查询在该时间范围内有交集的团队协作日程
            if (query?.PlannedStartTime.HasValue == true && query?.PlannedEndTime.HasValue == true)
            {
                // 查询条件：团队协作日程的计划开始时间或计划结束时间在指定范围内，或者团队协作日程跨越整个范围
                exp = exp.And(x => 
                    (x.PlannedStartTime >= query.PlannedStartTime.Value && x.PlannedStartTime <= query.PlannedEndTime.Value) || // 计划开始时间在范围内
                    (x.PlannedEndTime >= query.PlannedStartTime.Value && x.PlannedEndTime <= query.PlannedEndTime.Value) || // 计划结束时间在范围内
                    (x.PlannedStartTime <= query.PlannedStartTime.Value && x.PlannedEndTime >= query.PlannedEndTime.Value) // 团队协作日程跨越整个范围
                );
            }
            
            // 其他查询条件
            if (!string.IsNullOrEmpty(query?.TeamScheduleTitle))
                exp = exp.And(x => x.TeamScheduleTitle.Contains(query.TeamScheduleTitle));

            if (query?.Priority.HasValue == true)
                exp = exp.And(x => x.Priority == query.Priority.Value);

            if (!string.IsNullOrEmpty(query?.TeamLeader))
                exp = exp.And(x => x.TeamLeader.Contains(query.TeamLeader));

            if (!string.IsNullOrEmpty(query?.CollaborationLocation))
                exp = exp.And(x => x.CollaborationLocation.Contains(query.CollaborationLocation));

            var predicate = exp.ToExpression();
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await TeamScheduleRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.PlannedStartTime, // 按计划开始时间排序
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                var dtoList = result.Rows.Adapt<List<TaktTeamScheduleDto>>();
                _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                    result.TotalNum,
                    query?.PageIndex ?? 1,
                    query?.PageSize ?? 10,
                    dtoList.Count);

                return new TaktPagedResult<TaktTeamScheduleDto>
                {
                    TotalNum = result.TotalNum,
                    PageIndex = query?.PageIndex ?? 1,
                    PageSize = query?.PageSize ?? 10,
                    Rows = dtoList
                };
            }

            return new TaktPagedResult<TaktTeamScheduleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = new List<TaktTeamScheduleDto>()
            };
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktTeamSchedule, bool>> QueryExpression(TaktTeamScheduleQueryDto query)
        {
            var exp = Expressionable.Create<TaktTeamSchedule>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.TeamScheduleTitle))
                    exp = exp.And(x => x.TeamScheduleTitle.Contains(query.TeamScheduleTitle));

                if (!string.IsNullOrEmpty(query.TeamScheduleCode))
                    exp = exp.And(x => x.TeamScheduleCode.Contains(query.TeamScheduleCode));

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (query.Priority.HasValue)
                    exp = exp.And(x => x.Priority == query.Priority.Value);

                if (!string.IsNullOrEmpty(query.TeamLeader))
                    exp = exp.And(x => x.TeamLeader.Contains(query.TeamLeader));

                if (!string.IsNullOrEmpty(query.CollaborationLocation))
                    exp = exp.And(x => x.CollaborationLocation.Contains(query.CollaborationLocation));

                if (query.PlannedStartTime.HasValue)
                    exp = exp.And(x => x.PlannedStartTime >= query.PlannedStartTime.Value);

                if (query.PlannedEndTime.HasValue)
                    exp = exp.And(x => x.PlannedEndTime <= query.PlannedEndTime.Value);

                if (!string.IsNullOrEmpty(query.CreateBy))
                    exp = exp.And(x => x.CreateBy == query.CreateBy);
            }

            return exp.ToExpression();
        }
    }
} 





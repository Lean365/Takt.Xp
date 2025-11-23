//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktScheduleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 日程服务实现
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

namespace Takt.Application.Services.Routine.Schedule
{
    /// <summary>
    /// 日程服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public class TaktScheduleService : TaktBaseService, ITaktScheduleService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取日程仓储
        /// </summary>
        private ITaktRepository<TaktSchedule> ScheduleRepository => _repositoryFactory.GetBusinessRepository<TaktSchedule>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktScheduleService(
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
        /// 获取日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktScheduleDto>> GetListAsync(TaktScheduleQueryDto query)
        {
            _logger.Info("开始查询日程列表，查询条件：{@Query}", query);

            var predicate = QueryExpression(query);
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await ScheduleRepository.GetPagedListAsync(
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

            var dtoResult = new TaktPagedResult<TaktScheduleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktScheduleDto>>()
            };

            _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                dtoResult.TotalNum,
                dtoResult.PageIndex,
                dtoResult.PageSize,
                dtoResult.Rows?.Count ?? 0);

            return dtoResult;
        }

        /// <summary>
        /// 获取日程详情
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>返回日程详情</returns>
        public async Task<TaktScheduleDto> GetByIdAsync(long scheduleId)
        {
            var schedule = await ScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new TaktException(L("Schedule.NotFound", scheduleId));

            return schedule.Adapt<TaktScheduleDto>();
        }

        /// <summary>
        /// 创建日程
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回日程ID</returns>
        public async Task<long> CreateAsync(TaktScheduleCreateDto input)
        {
            var schedule = input.Adapt<TaktSchedule>();
            var result = await ScheduleRepository.CreateAsync(schedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Schedule.Created"),
                    Content = L("Schedule.CreatedContent", schedule.Title),
                    Timestamp = DateTime.Now,
                    Data = schedule
                });
            }
            
            return result;
        }

        /// <summary>
        /// 更新日程
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktScheduleUpdateDto input)
        {
            var schedule = await ScheduleRepository.GetByIdAsync(input.ScheduleId);
            if (schedule == null)
                throw new TaktException(L("Schedule.NotFound", input.ScheduleId));

            input.Adapt(schedule);
            var result = await ScheduleRepository.UpdateAsync(schedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Schedule.Updated"),
                    Content = L("Schedule.UpdatedContent", schedule.Title),
                    Timestamp = DateTime.Now,
                    Data = schedule
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 删除日程
        /// </summary>
        /// <param name="scheduleId">日程ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long scheduleId)
        {
            var schedule = await ScheduleRepository.GetByIdAsync(scheduleId);
            if (schedule == null)
                throw new TaktException(L("Schedule.NotFound", scheduleId));

            var result = await ScheduleRepository.DeleteAsync(schedule);
            
            if (result > 0)
            {
                // 发送实时通知
                await _signalRClient.ReceiveBroadcast(new TaktRealTimeNotification
                {
                    Type = TaktMessageType.System,
                    Title = L("Schedule.Deleted"),
                    Content = L("Schedule.DeletedContent", schedule.Title),
                    Timestamp = DateTime.Now,
                    Data = schedule
                });
            }
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除日程
        /// </summary>
        /// <param name="scheduleIds">日程ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] scheduleIds)
        {
            if (scheduleIds == null || scheduleIds.Length == 0)
                throw new TaktException(L("Schedule.SelectToDelete"));

            var schedules = await ScheduleRepository.GetListAsync(x => scheduleIds.Contains(x.Id));
            if (!schedules.Any())
                throw new TaktException(L("Schedule.NotFound"));

            var result = await ScheduleRepository.DeleteAsync(schedules);
            return result > 0;
        }

        /// <summary>
        /// 导入日程数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "日程信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktScheduleImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var schedule = dto.Adapt<TaktSchedule>();
                    await ScheduleRepository.CreateAsync(schedule);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Schedule.ImportFailed", dto.Title), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出日程数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktScheduleQueryDto query, string sheetName = "日程信息")
        {
            var predicate = QueryExpression(query);

            var schedules = await ScheduleRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.Id)
                .ToListAsync();

            var exportDtos = schedules.Adapt<List<TaktScheduleExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "日程信息")
        {
            var template = new List<TaktScheduleTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 获取指定用户的日程状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy)
        {
            if (string.IsNullOrEmpty(createBy))
                throw new ArgumentNullException(nameof(createBy));

            var stats = await ScheduleRepository.AsQueryable()
                .Where(x => x.CreateBy == createBy)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }

        /// <summary>
        /// 获取当前用户的日程分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>日程分页列表</returns>
        public async Task<TaktPagedResult<TaktScheduleDto>> GetMySchedulesAsync(TaktScheduleQueryDto query)
        {
            _logger.Info("开始查询当前用户日程列表，查询条件：{@Query}", query);

            // 设置当前用户为创建者
            query.CreateBy = _currentUser.UserName;

            // 构建查询表达式
            var exp = Expressionable.Create<TaktSchedule>();
            
            // 基础条件：当前用户创建的日程
            exp = exp.And(x => x.CreateBy == _currentUser.UserName);
            
            // 时间范围查询：如果提供了时间范围，查询在该时间范围内有交集的日程
            if (query?.StartTime.HasValue == true && query?.EndTime.HasValue == true)
            {
                // 查询条件：日程的开始时间或结束时间在指定范围内，或者日程跨越整个范围
                exp = exp.And(x => 
                    (x.StartTime >= query.StartTime.Value && x.StartTime <= query.EndTime.Value) || // 开始时间在范围内
                    (x.EndTime >= query.StartTime.Value && x.EndTime <= query.EndTime.Value) || // 结束时间在范围内
                    (x.StartTime <= query.StartTime.Value && x.EndTime >= query.EndTime.Value) // 日程跨越整个范围
                );
            }
            
            // 其他查询条件
            if (!string.IsNullOrEmpty(query?.Title))
                exp = exp.And(x => x.Title.Contains(query.Title));

            if (query?.ScheduleType.HasValue == true)
                exp = exp.And(x => x.ScheduleType == query.ScheduleType.Value);

            if (query?.Status.HasValue == true)
                exp = exp.And(x => x.Status == query.Status.Value);

            if (!string.IsNullOrEmpty(query?.Location))
                exp = exp.And(x => x.Location.Contains(query.Location));

            var predicate = exp.ToExpression();
            _logger.Debug("生成的查询表达式：{@Predicate}", predicate);

            var result = await ScheduleRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.StartTime, // 按开始时间排序
                OrderByType.Asc);

            _logger.Info("查询结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                result.TotalNum,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                result.Rows?.Count ?? 0);

            if (result.Rows != null && result.Rows.Any())
            {
                var dtoList = result.Rows.Adapt<List<TaktScheduleDto>>();
                _logger.Info("转换后的DTO结果：总数={TotalNum}, 当前页={PageIndex}, 每页大小={PageSize}, 数据行数={RowCount}",
                    result.TotalNum,
                    query?.PageIndex ?? 1,
                    query?.PageSize ?? 10,
                    dtoList.Count);

                return new TaktPagedResult<TaktScheduleDto>
                {
                    TotalNum = result.TotalNum,
                    PageIndex = query?.PageIndex ?? 1,
                    PageSize = query?.PageSize ?? 10,
                    Rows = dtoList
                };
            }

            return new TaktPagedResult<TaktScheduleDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = new List<TaktScheduleDto>()
            };
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktSchedule, bool>> QueryExpression(TaktScheduleQueryDto query)
        {
            var exp = Expressionable.Create<TaktSchedule>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.Title))
                    exp = exp.And(x => x.Title.Contains(query.Title));

                if (query.ScheduleType.HasValue)
                    exp = exp.And(x => x.ScheduleType == query.ScheduleType.Value);

                if (query.Status.HasValue)
                    exp = exp.And(x => x.Status == query.Status.Value);

                if (!string.IsNullOrEmpty(query.Location))
                    exp = exp.And(x => x.Location.Contains(query.Location));

                if (query.StartTime.HasValue)
                    exp = exp.And(x => x.StartTime >= query.StartTime.Value);

                if (query.EndTime.HasValue)
                    exp = exp.And(x => x.EndTime <= query.EndTime.Value);

                if (!string.IsNullOrEmpty(query.CreateBy))
                    exp = exp.And(x => x.CreateBy == query.CreateBy);
            }

            return exp.ToExpression();
        }
    }
} 





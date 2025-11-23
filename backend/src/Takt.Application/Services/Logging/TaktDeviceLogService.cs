//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeviceLogService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 16:45
// 版本号 : V0.0.1
// 描述   : 设备日志服务实现
//===================================================================

#nullable enable

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Logging
{
    /// <summary>
    /// 设备日志服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktDeviceLogService : TaktBaseService, ITaktDeviceLogService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDeviceLogService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取设备日志仓储
        /// </summary>
        private ITaktRepository<TaktDeviceLog> DeviceLogRepository => _repositoryFactory.GetAuthRepository<TaktDeviceLog>();

        /// <summary>
        /// 获取设备日志分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>设备日志分页列表</returns>
        public async Task<TaktPagedResult<TaktDeviceLogDto>> GetListAsync(TaktDeviceLogQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await DeviceLogRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Desc);

            return new TaktPagedResult<TaktDeviceLogDto>
            {
                Rows = result.Rows.Adapt<List<TaktDeviceLogDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取设备日志详情
        /// </summary>
        /// <param name="id">设备日志ID</param>
        /// <returns>设备日志详情</returns>
        public async Task<TaktDeviceLogDto> GetByIdAsync(long id)
        {
            var deviceLog = await DeviceLogRepository.GetByIdAsync(id);
            if (deviceLog == null)
                throw new TaktException(L("Audit.DeviceLog.NotFound"));

            return deviceLog.Adapt<TaktDeviceLogDto>();
        }

        /// <summary>
        /// 删除设备日志
        /// </summary>
        /// <param name="id">设备日志ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var deviceLog = await DeviceLogRepository.GetByIdAsync(id);
            if (deviceLog == null)
                throw new TaktException(L("Audit.DeviceLog.NotFound"));

            var result = await DeviceLogRepository.DeleteAsync(deviceLog);
            if (result <= 0)
                throw new TaktException(L("Common.DeleteFailed"));

            return true;
        }

        /// <summary>
        /// 批量删除设备日志
        /// </summary>
        /// <param name="ids">设备日志ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                throw new TaktException(L("Common.BatchDeleteIdsRequired"));

            var deviceLogs = await DeviceLogRepository.GetListAsync(x => ids.Contains(x.Id));
            if (deviceLogs.Count != ids.Length)
                throw new TaktException(L("Common.SomeRecordsNotFound"));

            var result = await DeviceLogRepository.DeleteAsync(deviceLogs);
            return result > 0;
        }

        /// <summary>
        /// 记录设备登录日志
        /// </summary>
        /// <param name="input">设备日志信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RecordDeviceLoginAsync(TaktDeviceLogDto input)
        {
            try
            {
                var entity = input.Adapt<TaktDeviceLog>();
                entity.CreateTime = DateTime.Now;
                entity.CreateBy = _currentUser.UserName ?? "Takt365";

                // 检查是否已存在该设备的记录
                var existingDevice = await DeviceLogRepository.GetFirstAsync(
                    x => x.UserId == input.UserId && x.LastLoginDeviceId == input.DeviceId);

                if (existingDevice != null)
                {
                    // 更新现有记录
                    existingDevice.LastLoginTime = DateTime.Now;
                    existingDevice.LastLoginIp = input.LastLoginIp;
                    existingDevice.LastLoginLocation = input.LastLoginLocation;
                    existingDevice.LoginCount++;
                    existingDevice.UpdateTime = DateTime.Now;
                    existingDevice.UpdateBy = _currentUser.UserName ?? "System";

                    var result = await DeviceLogRepository.UpdateAsync(existingDevice);
                    return result > 0;
                }
                else
                {
                    // 创建新记录
                    entity.FirstLoginTime = DateTime.Now;
                    entity.FirstLoginIp = input.FirstLoginIp;
                    entity.FirstLoginLocation = input.FirstLoginLocation;
                    entity.LoginCount = 1;
                    entity.ContinuousLoginDays = 1;

                    var result = await DeviceLogRepository.CreateAsync(entity);
                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                _logger.Error("记录设备登录日志失败", ex);
                throw new TaktException("记录设备登录日志失败");
            }
        }

        /// <summary>
        /// 更新设备在线状态
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="isOnline">是否在线</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateOnlineStatusAsync(string deviceId, bool isOnline)
        {
            try
            {
                var entity = await DeviceLogRepository.GetFirstAsync(x => x.LastLoginDeviceId == deviceId);
                if (entity == null)
                    return false;

                if (isOnline)
                {
                    entity.LastOnlineTime = DateTime.Now;
                }
                else
                {
                    entity.LastOfflineTime = DateTime.Now;
                }

                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = _currentUser.UserName ?? "System";

                var result = await DeviceLogRepository.UpdateAsync(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("更新设备在线状态失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 获取用户在线设备数量
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>在线设备数量</returns>
        public async Task<int> GetOnlineDeviceCountAsync(long userId)
        {
            try
            {
                var today = DateTime.Today;
                var count = await DeviceLogRepository.GetCountAsync(x =>
                    x.UserId == userId &&
                    x.LastOnlineTime.HasValue &&
                    x.LastOnlineTime.Value.Date == today &&
                    (!x.LastOfflineTime.HasValue || x.LastOfflineTime.Value < x.LastOnlineTime.Value));

                return count;
            }
            catch (Exception ex)
            {
                _logger.Error("获取用户在线设备数量失败", ex);
                return 0;
            }
        }

        /// <summary>
        /// 更新今日在线时段
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="onlinePeriods">在线时段列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateTodayOnlinePeriodsAsync(string deviceId, List<string> onlinePeriods)
        {
            try
            {
                var entity = await DeviceLogRepository.GetFirstAsync(x => x.LastLoginDeviceId == deviceId);
                if (entity == null)
                    return false;

                entity.TodayOnlinePeriods = onlinePeriods != null ? string.Join(",", onlinePeriods) : null;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = _currentUser.UserName ?? "System";

                var result = await DeviceLogRepository.UpdateAsync(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("更新今日在线时段失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 更新连续登录天数
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="continuousDays">连续登录天数</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateContinuousLoginDaysAsync(string deviceId, int continuousDays)
        {
            try
            {
                var entity = await DeviceLogRepository.GetFirstAsync(x => x.LastLoginDeviceId == deviceId);
                if (entity == null)
                    return false;

                entity.ContinuousLoginDays = continuousDays;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = _currentUser.UserName ?? "Takt365";

                var result = await DeviceLogRepository.UpdateAsync(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("更新连续登录天数失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 更新登录次数
        /// </summary>
        /// <param name="deviceId">设备ID</param>
        /// <param name="loginCount">登录次数</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateLoginCountAsync(string deviceId, int loginCount)
        {
            try
            {
                var entity = await DeviceLogRepository.GetFirstAsync(x => x.LastLoginDeviceId == deviceId);
                if (entity == null)
                    return false;

                entity.LoginCount = loginCount;
                entity.UpdateTime = DateTime.Now;
                entity.UpdateBy = _currentUser.UserName ?? "System";

                var result = await DeviceLogRepository.UpdateAsync(entity);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("更新登录次数失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 清空设备日志数据
        /// </summary>
        /// <param name="query">清空条件</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ClearUpAsync(TaktDeviceLogQueryDto query)
        {
            try
            {
                var exp = QueryExpression(query);
                var result = await DeviceLogRepository.DeleteAsync(exp);
                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error("清空设备日志数据失败", ex);
                return false;
            }
        }

        /// <summary>
        /// 导出设备日志数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktDeviceLogQueryDto query, string sheetName)
        {
            var list = await DeviceLogRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktDeviceLogExportDto>>();
            return await TaktExcelHelper.ExportAsync(exportList, sheetName, "设备日志数据");
        }

        /// <summary>
        /// 构建设备日志查询条件
        /// </summary>
        private static Expression<Func<TaktDeviceLog, bool>> QueryExpression(TaktDeviceLogQueryDto query)
        {
            var exp = Expressionable.Create<TaktDeviceLog>();

            if (query.UserId.HasValue)
                exp.And(x => x.UserId == query.UserId.Value);

            if (query.LoginType.HasValue)
                exp.And(x => x.LoginType == query.LoginType.Value);

            if (query.LoginSource.HasValue)
                exp.And(x => x.LoginSource == query.LoginSource.Value);

            if (query.DeviceStatus.HasValue)
                exp.And(x => x.DeviceStatus == query.DeviceStatus.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            return exp.ToExpression();
        }
    }
}






#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktLogCleanupService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 日志清理服务
//===================================================================


using Takt.Shared.Options;
using Takt.Domain.Entities.Routine.Settings;
using Microsoft.Extensions.Options;

namespace Takt.Application.Services.Extensions
{
    /// <summary>
    /// 日志清理服务
    /// </summary>
    public class TaktLogCleanupService : ITaktLogCleanupService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly TaktLogCleanupOptions _defaultOptions;

        private ITaktRepository<TaktOperLog> OperLogRepository => _repositoryFactory.GetAuthRepository<TaktOperLog>();
        private ITaktRepository<TaktLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<TaktLoginLog>();
        private ITaktRepository<TaktExceptionLog> ExceptionLogRepository => _repositoryFactory.GetAuthRepository<TaktExceptionLog>();
        private ITaktRepository<TaktSqlDiffLog> DbDiffLogRepository => _repositoryFactory.GetAuthRepository<TaktSqlDiffLog>();
        private ITaktRepository<TaktGeneralSettings> SysConfigRepository => _repositoryFactory.GetBusinessRepository<TaktGeneralSettings>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLogCleanupService(
            ITaktRepositoryFactory repositoryFactory,
            IOptions<TaktLogCleanupOptions> options)
        {
            _repositoryFactory = repositoryFactory;
            _defaultOptions = options.Value;
        }

        /// <summary>
        /// 获取日志清理配置
        /// </summary>
        public async Task<TaktLogCleanupOptions> GetConfigAsync()
        {
            var configs = await SysConfigRepository.GetListAsync();
            var options = new TaktLogCleanupOptions();

            // 从系统配置中获取配置值，如果不存在则使用默认值
            options.Enabled = GetConfigValue(configs, "log.cleanup.enabled", _defaultOptions.Enabled);
            options.Interval = GetConfigValue(configs, "log.cleanup.interval", _defaultOptions.Interval);
            options.OperLogRetentionDays = GetConfigValue(configs, "log.cleanup.oper.retention", _defaultOptions.OperLogRetentionDays);
            options.LoginLogRetentionDays = GetConfigValue(configs, "log.cleanup.login.retention", _defaultOptions.LoginLogRetentionDays);
            options.ExceptionLogRetentionDays = GetConfigValue(configs, "log.cleanup.exception.retention", _defaultOptions.ExceptionLogRetentionDays);
            options.DbDiffLogRetentionDays = GetConfigValue(configs, "log.cleanup.dbdiff.retention", _defaultOptions.DbDiffLogRetentionDays);
            options.BatchSize = GetConfigValue(configs, "log.cleanup.batch.size", _defaultOptions.BatchSize);

            return options;
        }

        /// <summary>
        /// 执行日志清理
        /// </summary>
        public async Task CleanupAsync()
        {
            var options = await GetConfigAsync();
            if (!options.Enabled) return;

            var now = DateTime.Now;
            var batchSize = options.BatchSize;

            // 清理操作日志
            await CleanupOperLogs(now.AddDays(-options.OperLogRetentionDays), batchSize);

            // 清理登录日志
            await CleanupLoginLogs(now.AddDays(-options.LoginLogRetentionDays), batchSize);

            // 清理异常日志
            await CleanupExceptionLogs(now.AddDays(-options.ExceptionLogRetentionDays), batchSize);

            // 清理差异日志
            await CleanupDbDiffLogs(now.AddDays(-options.DbDiffLogRetentionDays), batchSize);
        }

        private T GetConfigValue<T>(List<TaktGeneralSettings> configs, string key, T defaultValue)
        {
            var config = configs.FirstOrDefault(c => c.SettingsKey == key);
            if (config == null) return defaultValue;

            try
            {
                return (T)Convert.ChangeType(config.SettingsValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }

        private async Task CleanupOperLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await OperLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await OperLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupLoginLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await LoginLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await LoginLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupExceptionLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await ExceptionLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await ExceptionLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }

        private async Task CleanupDbDiffLogs(DateTime beforeTime, int batchSize)
        {
            while (true)
            {
                var logs = await DbDiffLogRepository.GetListAsync(l => l.CreateTime < beforeTime);
                if (!logs.Any()) break;

                await DbDiffLogRepository.DeleteAsync(logs.Take(batchSize).ToList());
            }
        }
    }
}






using Takt.Shared.Options;
using Takt.Domain.Entities.Routine.Settings;
using Microsoft.Extensions.Options;
using System.IO.Compression;

namespace Takt.Application.Services.Extensions
{
    /// <summary>
    /// 日志归档服务实现类
    /// </summary>
    public class TaktLogArchiveService : ITaktLogArchiveService
    {
        private readonly IOptions<TaktLogArchiveOptions> _defaultOptions;
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultOptions"></param>
        /// <param name="repositoryFactory"></param>
        /// <param name="logger"></param>
        public TaktLogArchiveService(
            IOptions<TaktLogArchiveOptions> defaultOptions,
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger)
        {
            _defaultOptions = defaultOptions;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _logger = logger;
        }

        /// <summary>
        /// 获取配置仓储
        /// </summary>
        private ITaktRepository<TaktGeneralSettings> ConfigRepository => _repositoryFactory.GetAuthRepository<TaktGeneralSettings>();

        /// <summary>
        /// 获取操作日志仓储
        /// </summary>
        private ITaktRepository<TaktOperLog> OperLogRepository => _repositoryFactory.GetAuthRepository<TaktOperLog>();

        /// <summary>
        /// 获取登录日志仓储
        /// </summary>
        private ITaktRepository<TaktLoginLog> LoginLogRepository => _repositoryFactory.GetAuthRepository<TaktLoginLog>();

        /// <summary>
        /// 获取异常日志仓储
        /// </summary>
        private ITaktRepository<TaktExceptionLog> ExceptionLogRepository => _repositoryFactory.GetAuthRepository<TaktExceptionLog>();

        /// <summary>
        /// 获取数据库差异日志仓储
        /// </summary>
        private ITaktRepository<TaktSqlDiffLog> SqlDiffLogRepository => _repositoryFactory.GetAuthRepository<TaktSqlDiffLog>();

        /// <summary>
        /// 获取日志归档配置
        /// </summary>
        public async Task<TaktLogArchiveOptions> GetConfigAsync()
        {
            var configs = await ConfigRepository.GetListAsync(c => c.SettingsKey.StartsWith("log.archive."));
            if (configs == null || !configs.Any())
                return _defaultOptions.Value;

            return new TaktLogArchiveOptions
            {
                Enabled = GetConfigValue(configs, "log.archive.enabled", _defaultOptions.Value.Enabled),
                ArchivePeriodMonths = GetConfigValue(configs, "log.archive.periodMonths", _defaultOptions.Value.ArchivePeriodMonths),
                ArchivePath = GetConfigValue(configs, "log.archive.path", _defaultOptions.Value.ArchivePath),
                FileNameFormat = GetConfigValue(configs, "log.archive.fileNameFormat", _defaultOptions.Value.FileNameFormat),
                BatchSize = GetConfigValue(configs, "log.archive.batchSize", _defaultOptions.Value.BatchSize),
                CronExpression = GetConfigValue(configs, "log.archive.cronExpression", _defaultOptions.Value.CronExpression)
            };
        }

        /// <summary>
        /// 执行日志归档
        /// </summary>
        public async Task ArchiveAsync()
        {
            var options = await GetConfigAsync();
            if (!options.Enabled)
            {
                _logger.Info("日志归档功能未启用");
                return;
            }

            var archiveDate = DateTime.Now.AddMonths(-options.ArchivePeriodMonths);
            var archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.ArchivePath);
            Directory.CreateDirectory(archivePath);

            var fileName = string.Format(options.FileNameFormat, archiveDate);
            var archiveFile = Path.Combine(archivePath, fileName);

            using (var archive = ZipFile.Open(archiveFile, ZipArchiveMode.Create))
            {
                await ArchiveLogTable(archive, "Takt_oper_log.json", OperLogRepository, archiveDate, options.BatchSize);
                await ArchiveLogTable(archive, "Takt_login_log.json", LoginLogRepository, archiveDate, options.BatchSize);
                await ArchiveLogTable(archive, "Takt_exception_log.json", ExceptionLogRepository, archiveDate, options.BatchSize);
                await ArchiveLogTable(archive, "Takt_dbdiff_log.json", SqlDiffLogRepository, archiveDate, options.BatchSize);
            }

            _logger.Info($"日志归档完成: {archiveFile}");
        }

        /// <summary>
        /// 获取归档文件列表
        /// </summary>
        public async Task<List<string>> GetArchiveFilesAsync()
        {
            var options = await GetConfigAsync();
            var archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.ArchivePath);

            if (!Directory.Exists(archivePath))
                return new List<string>();

            return Directory.GetFiles(archivePath, "*.zip")
                .Select(Path.GetFileName)
                .Where(name => name != null)
                .Cast<string>()
                .ToList();
        }

        /// <summary>
        /// 删除归档文件
        /// </summary>
        public async Task DeleteArchiveFileAsync(string fileName)
        {
            var options = await GetConfigAsync();
            var archivePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, options.ArchivePath);
            var filePath = Path.Combine(archivePath, fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.Info($"删除归档文件: {fileName}");
            }
        }

        private async Task ArchiveLogTable<T>(ZipArchive archive, string entryName, ITaktRepository<T> repository, DateTime archiveDate, int batchSize)
            where T : TaktBaseEntity, new()
        {
            var entry = archive.CreateEntry(entryName);
            using var writer = new StreamWriter(entry.Open());

            var totalCount = 0;
            while (true)
            {
                var result = await repository.GetPagedListAsync(
                    x => x.CreateTime <= archiveDate,
                    1,
                    batchSize,
                    x => x.CreateTime,
                    OrderByType.Asc);

                if (result.Rows == null || !result.Rows.Any())
                    break;

                foreach (var log in result.Rows)
                {
                    await writer.WriteLineAsync(JsonConvert.SerializeObject(log));
                    totalCount++;
                }

                var ids = result.Rows.Select(l => l.Id).ToList();
                Expression<Func<T, bool>> predicate = x => ids.Contains(x.Id);
                await repository.DeleteAsync(predicate);
            }

            _logger.Info($"已归档 {typeof(T).Name}: {totalCount} 条记录");
        }

        private T GetConfigValue<T>(List<TaktGeneralSettings> configs, string key, T defaultValue)
        {
            var config = configs.FirstOrDefault(c => c.SettingsKey == key);
            if (config == null)
                return defaultValue;

            try
            {
                return (T)Convert.ChangeType(config.SettingsValue, typeof(T));
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}


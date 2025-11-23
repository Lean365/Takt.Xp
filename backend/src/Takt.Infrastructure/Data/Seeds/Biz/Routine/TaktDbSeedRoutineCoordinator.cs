//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRoutineCoordinator.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 常规业务模块种子数据协调器 - 专门处理配置、语言、车辆、通知、日程、会议
//===================================================================

using Takt.Domain.Entities.Routine.I18n;
using Takt.Domain.Entities.Routine.Metting;
using Takt.Domain.Entities.Routine.News;
using Takt.Domain.Entities.Routine.Notice;
using Takt.Domain.Entities.Routine.Numbering;
using Takt.Domain.Entities.Routine.Schedule;
using Takt.Domain.Entities.Routine.Settings;
using Takt.Domain.Entities.Routine.Vehicle;

namespace Takt.Infrastructure.Data.Seeds.Biz.Routine;

/// <summary>
/// 常规业务模块种子数据协调器
/// </summary>
/// <remarks>
/// 创建者: Claude
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 专门处理常规业务模块的配置、语言、车辆、通知、日程、会议种子数据
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供配置、语言、车辆、通知、日程、会议的初始化功能
/// 4. 支持增量更新
/// </remarks>
public class TaktDbSeedRoutineCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktGeneralSettings> GeneralSettingsRepository => _repositoryFactory.GetBusinessRepository<TaktGeneralSettings>();
    private ITaktRepository<TaktLanguage> LanguageRepository => _repositoryFactory.GetBusinessRepository<TaktLanguage>();
    private ITaktRepository<TaktVehicle> VehicleRepository => _repositoryFactory.GetBusinessRepository<TaktVehicle>();
    private ITaktRepository<TaktVehicleBooking> VehicleBookingRepository => _repositoryFactory.GetBusinessRepository<TaktVehicleBooking>();
    private ITaktRepository<TaktDriver> DriverRepository => _repositoryFactory.GetBusinessRepository<TaktDriver>();
    private ITaktRepository<TaktNotice> NoticeRepository => _repositoryFactory.GetBusinessRepository<TaktNotice>();
    private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();
    private ITaktRepository<TaktNewsComment> NewsCommentRepository => _repositoryFactory.GetBusinessRepository<TaktNewsComment>();
    private ITaktRepository<TaktNewsTopic> NewsTopicRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopic>();
    private ITaktRepository<TaktSchedule> ScheduleRepository => _repositoryFactory.GetBusinessRepository<TaktSchedule>();
    private ITaktRepository<TaktMeeting> MeetingRepository => _repositoryFactory.GetBusinessRepository<TaktMeeting>();
    private ITaktRepository<TaktNumberingRules> NumberRuleRepository => _repositoryFactory.GetBusinessRepository<TaktNumberingRules>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    public TaktDbSeedRoutineCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <summary>
    /// 初始化所有常规业务模块种子数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<RoutineSeedResult> InitializeAllRoutineDataAsync()
    {
        try
        {
            _logger.Info("开始初始化常规业务模块种子数据...");

            var result = new RoutineSeedResult();

            // 1. 初始化通用设置数据
            var configResult = await InitializeGeneralSettingsDataAsync();
            result.ConfigResults = configResult;

            // 2. 初始化语言数据
            var languageResult = await InitializeLanguageDataAsync();
            result.LanguageResults = languageResult;

            // 3. 初始化车辆数据
            var vehicleResult = await InitializeVehicleDataAsync();
            result.VehicleResults = vehicleResult;

            // 3.1. 初始化车辆预约数据
            var vehicleBookingResult = await InitializeVehicleBookingDataAsync();
            result.VehicleBookingResults = vehicleBookingResult;

            // 3.2. 初始化驾驶员数据
            var driverResult = await InitializeDriverDataAsync();
            result.DriverResults = driverResult;

            // 4. 初始化通知数据
            var noticeResult = await InitializeNoticeDataAsync();
            result.NoticeResults = noticeResult;

            // 4.1. 初始化新闻数据
            var newsResult = await InitializeNewsDataAsync();
            result.NewsResults = newsResult;

            // 4.2. 初始化新闻评论数据
            var newsCommentResult = await InitializeNewsCommentDataAsync();
            result.NewsCommentResults = newsCommentResult;

            // 4.3. 初始化新闻话题数据
            var newsTopicResult = await InitializeNewsTopicDataAsync();
            result.NewsTopicResults = newsTopicResult;

            // 5. 初始化日程数据
            var scheduleResult = await InitializeScheduleDataAsync();
            result.ScheduleResults = scheduleResult;

            // 6. 初始化会议数据
            var meetingResult = await InitializeMeetingDataAsync();
            result.MeetingResults = meetingResult;

            // 7. 初始化编号规则数据
            var numberRuleResult = await InitializeNumberRuleDataAsync();
            result.NumberRuleResults = numberRuleResult;

            _logger.Info($"常规业务模块种子数据初始化完成！通用设置: {configResult.insertCount + configResult.updateCount} 条, 语言: {languageResult.insertCount + languageResult.updateCount} 条, 车辆: {vehicleResult.insertCount + vehicleResult.updateCount} 条, 车辆预约: {vehicleBookingResult.insertCount + vehicleBookingResult.updateCount} 条, 驾驶员: {driverResult.insertCount + driverResult.updateCount} 条, 通知: {noticeResult.insertCount + noticeResult.updateCount} 条, 新闻: {newsResult.insertCount + newsResult.updateCount} 条, 新闻评论: {newsCommentResult.insertCount + newsCommentResult.updateCount} 条, 新闻话题: {newsTopicResult.insertCount + newsTopicResult.updateCount} 条, 日程: {scheduleResult.insertCount + scheduleResult.updateCount} 条, 会议: {meetingResult.insertCount + meetingResult.updateCount} 条, 编号规则: {numberRuleResult.insertCount + numberRuleResult.updateCount} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化常规业务模块种子数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化通用设置数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeGeneralSettingsDataAsync()
    {
        var configSeed = new TaktDbSeedGeneralSettings();
        var configs = configSeed.GetDefaultGeneralSettings();
        return await InitializeGeneralSettingsAsync(configs, "常规业务配置");
    }

    /// <summary>
    /// 初始化语言数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeLanguageDataAsync()
    {
        var languageSeed = new TaktDbSeedLanguage();
        var languages = languageSeed.GetDefaultLanguages();
        return await InitializeLanguagesAsync(languages, "常规业务语言");
    }

    /// <summary>
    /// 初始化车辆数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeVehicleDataAsync()
    {
        var vehicleSeed = new TaktDbSeedVehicle();
        var vehicles = vehicleSeed.GetDefaultVehicles();
        return await InitializeVehiclesAsync(vehicles, "车辆管理");
    }

    /// <summary>
    /// 初始化车辆预约数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeVehicleBookingDataAsync()
    {
        var vehicleSeed = new TaktDbSeedVehicle();
        var vehicleBookings = vehicleSeed.GetDefaultVehicleBookings();
        return await InitializeVehicleBookingsAsync(vehicleBookings, "车辆预约");
    }

    /// <summary>
    /// 初始化驾驶员数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeDriverDataAsync()
    {
        var vehicleSeed = new TaktDbSeedVehicle();
        var drivers = vehicleSeed.GetDefaultDrivers();
        return await InitializeDriversAsync(drivers, "驾驶员管理");
    }

    /// <summary>
    /// 初始化通知数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeNoticeDataAsync()
    {
        var noticeSeed = new TaktDbSeedNotice();
        var notices = noticeSeed.GetDefaultNotices();
        return await InitializeNoticesAsync(notices, "通知公告");
    }

    /// <summary>
    /// 初始化新闻数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeNewsDataAsync()
    {
        var newsSeed = new TaktDbSeedNews();
        var news = newsSeed.GetDefaultNews();
        return await InitializeNewsAsync(news, "新闻管理");
    }

    /// <summary>
    /// 初始化新闻评论数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeNewsCommentDataAsync()
    {
        var newsSeed = new TaktDbSeedNews();
        var newsComments = newsSeed.GetDefaultNewsComments();
        return await InitializeNewsCommentsAsync(newsComments, "新闻评论");
    }

    /// <summary>
    /// 初始化新闻话题数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeNewsTopicDataAsync()
    {
        var newsSeed = new TaktDbSeedNews();
        var newsTopics = newsSeed.GetDefaultNewsTopics();
        return await InitializeNewsTopicsAsync(newsTopics, "新闻话题");
    }

    /// <summary>
    /// 初始化日程数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeScheduleDataAsync()
    {
        var scheduleSeed = new TaktDbSeedSchedule();
        var schedules = scheduleSeed.GetDefaultSchedules();
        return await InitializeSchedulesAsync(schedules, "日程管理");
    }

    /// <summary>
    /// 初始化会议数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeMeetingDataAsync()
    {
        var meetingSeed = new TaktDbSeedMeeting();
        var meetings = meetingSeed.GetDefaultMeetings();
        return await InitializeMeetingsAsync(meetings, "会议管理");
    }

    /// <summary>
    /// 初始化编号规则数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<(int insertCount, int updateCount)> InitializeNumberRuleDataAsync()
    {
        var numberRuleSeed = new TaktDbSeedNumberRule();
        var numberRules = numberRuleSeed.GetDefaultNumberRules();
        return await InitializeNumberRulesAsync(numberRules, "编号规则");
    }

    /// <summary>
    /// 初始化通用设置数据
    /// </summary>
    /// <param name="generalSettings">通用设置列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeGeneralSettingsAsync(List<TaktGeneralSettings> generalSettings, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var config in generalSettings)
        {
            var existingConfig = await GeneralSettingsRepository.GetFirstAsync(c => c.SettingsKey == config.SettingsKey);
            if (existingConfig == null)
            {
                config.CreateBy = "Takt365";
                config.CreateTime = DateTime.Now;
                config.UpdateBy = "Takt365";
                config.UpdateTime = DateTime.Now;
                await GeneralSettingsRepository.CreateAsync(config);
                insertCount++;
                _logger.Info($"[创建] {category} '{config.SettingsName}' 创建成功");
            }
            else
            {
                existingConfig.SettingsName = config.SettingsName;
                existingConfig.SettingsValue = config.SettingsValue;
                existingConfig.SettingsType = config.SettingsType;
                existingConfig.Status = config.Status;
                existingConfig.Remark = config.Remark;
                existingConfig.UpdateBy = "Takt365";
                existingConfig.UpdateTime = DateTime.Now;

                await GeneralSettingsRepository.UpdateAsync(existingConfig);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingConfig.SettingsName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化语言数据
    /// </summary>
    /// <param name="languages">语言列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeLanguagesAsync(List<TaktLanguage> languages, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var language in languages)
        {
            var existingLanguage = await LanguageRepository.GetFirstAsync(l => l.LangCode == language.LangCode);
            if (existingLanguage == null)
            {
                language.CreateBy = "Takt365";
                language.CreateTime = DateTime.Now;
                language.UpdateBy = "Takt365";
                language.UpdateTime = DateTime.Now;
                await LanguageRepository.CreateAsync(language);
                insertCount++;
                _logger.Info($"[创建] {category} '{language.LangName}' 创建成功");
            }
            else
            {
                existingLanguage.LangName = language.LangName;
                existingLanguage.LangIcon = language.LangIcon;
                existingLanguage.I18nStatus = language.I18nStatus;
                existingLanguage.Remark = language.Remark;
                existingLanguage.UpdateBy = "Takt365";
                existingLanguage.UpdateTime = DateTime.Now;

                await LanguageRepository.UpdateAsync(existingLanguage);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingLanguage.LangName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化车辆数据
    /// </summary>
    /// <param name="vehicles">车辆列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeVehiclesAsync(List<TaktVehicle> vehicles, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var vehicle in vehicles)
        {
            var existingVehicle = await VehicleRepository.GetFirstAsync(v => v.PlateNumber == vehicle.PlateNumber);
            if (existingVehicle == null)
            {
                vehicle.CreateBy = "Takt365";
                vehicle.CreateTime = DateTime.Now;
                vehicle.UpdateBy = "Takt365";
                vehicle.UpdateTime = DateTime.Now;
                await VehicleRepository.CreateAsync(vehicle);
                insertCount++;
                _logger.Info($"[创建] {category} '{vehicle.PlateNumber}' 创建成功");
            }
            else
            {
                existingVehicle.VehicleType = vehicle.VehicleType;
                existingVehicle.Status = vehicle.Status;
                existingVehicle.Brand = vehicle.Brand;
                existingVehicle.Model = vehicle.Model;
                existingVehicle.Color = vehicle.Color;
                existingVehicle.SeatCount = vehicle.SeatCount;
                existingVehicle.PurchaseDate = vehicle.PurchaseDate;
                existingVehicle.PurchasePrice = vehicle.PurchasePrice;
                existingVehicle.CurrentMileage = vehicle.CurrentMileage;
                existingVehicle.InsuranceExpiryDate = vehicle.InsuranceExpiryDate;
                existingVehicle.InspectionExpiryDate = vehicle.InspectionExpiryDate;
                existingVehicle.UpdateBy = "Takt365";
                existingVehicle.UpdateTime = DateTime.Now;

                await VehicleRepository.UpdateAsync(existingVehicle);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingVehicle.PlateNumber}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化车辆预约数据
    /// </summary>
    /// <param name="vehicleBookings">车辆预约列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeVehicleBookingsAsync(List<TaktVehicleBooking> vehicleBookings, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var vehicleBooking in vehicleBookings)
        {
            var existingVehicleBooking = await VehicleBookingRepository.GetFirstAsync(vb => vb.BookingTitle == vehicleBooking.BookingTitle && vb.StartTime == vehicleBooking.StartTime);
            if (existingVehicleBooking == null)
            {
                vehicleBooking.CreateBy = "Takt365";
                vehicleBooking.CreateTime = DateTime.Now;
                vehicleBooking.UpdateBy = "Takt365";
                vehicleBooking.UpdateTime = DateTime.Now;
                await VehicleBookingRepository.CreateAsync(vehicleBooking);
                insertCount++;
                _logger.Info($"[创建] {category} '{vehicleBooking.BookingTitle}' 创建成功");
            }
            else
            {
                existingVehicleBooking.BookingContent = vehicleBooking.BookingContent;
                existingVehicleBooking.StartTime = vehicleBooking.StartTime;
                existingVehicleBooking.EndTime = vehicleBooking.EndTime;
                existingVehicleBooking.Status = vehicleBooking.Status;
                existingVehicleBooking.BookingType = vehicleBooking.BookingType;
                existingVehicleBooking.Purpose = vehicleBooking.Purpose;
                existingVehicleBooking.Destination = vehicleBooking.Destination;
                existingVehicleBooking.EstimatedMileage = vehicleBooking.EstimatedMileage;
                existingVehicleBooking.PassengerCount = vehicleBooking.PassengerCount;
                existingVehicleBooking.Passengers = vehicleBooking.Passengers;
                existingVehicleBooking.ContactPerson = vehicleBooking.ContactPerson;
                existingVehicleBooking.ContactPhone = vehicleBooking.ContactPhone;
                existingVehicleBooking.NeedDriver = vehicleBooking.NeedDriver;
                existingVehicleBooking.DriverId = vehicleBooking.DriverId;
                existingVehicleBooking.ApproverId = vehicleBooking.ApproverId;
                existingVehicleBooking.ApprovalTime = vehicleBooking.ApprovalTime;
                existingVehicleBooking.ApprovalRemarks = vehicleBooking.ApprovalRemarks;
                existingVehicleBooking.UpdateBy = "Takt365";
                existingVehicleBooking.UpdateTime = DateTime.Now;

                await VehicleBookingRepository.UpdateAsync(existingVehicleBooking);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingVehicleBooking.BookingTitle}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化驾驶员数据
    /// </summary>
    /// <param name="drivers">驾驶员列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeDriversAsync(List<TaktDriver> drivers, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var driver in drivers)
        {
            var existingDriver = await DriverRepository.GetFirstAsync(d => d.LicenseNo == driver.LicenseNo);
            if (existingDriver == null)
            {
                driver.CreateBy = "Takt365";
                driver.CreateTime = DateTime.Now;
                driver.UpdateBy = "Takt365";
                driver.UpdateTime = DateTime.Now;
                await DriverRepository.CreateAsync(driver);
                insertCount++;
                _logger.Info($"[创建] {category} '{driver.LicenseNo}' 创建成功");
            }
            else
            {
                existingDriver.Status = driver.Status;
                existingDriver.LicenseType = driver.LicenseType;
                existingDriver.LicenseStatus = driver.LicenseStatus;
                existingDriver.LicensePoints = driver.LicensePoints;
                existingDriver.DrivingYears = driver.DrivingYears;
                existingDriver.DrivingExperience = driver.DrivingExperience;
                existingDriver.DrivableVehicles = driver.DrivableVehicles;
                existingDriver.IsFullTime = driver.IsFullTime;
                existingDriver.WorkArea = driver.WorkArea;
                existingDriver.WorkSchedule = driver.WorkSchedule;
                existingDriver.Remarks = driver.Remarks;
                existingDriver.UpdateBy = "Takt365";
                existingDriver.UpdateTime = DateTime.Now;

                await DriverRepository.UpdateAsync(existingDriver);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingDriver.LicenseNo}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化通知数据
    /// </summary>
    /// <param name="notices">通知列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeNoticesAsync(List<TaktNotice> notices, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var notice in notices)
        {
            var existingNotice = await NoticeRepository.GetFirstAsync(n => n.NoticeTitle == notice.NoticeTitle);
            if (existingNotice == null)
            {
                notice.CreateBy = "Takt365";
                notice.CreateTime = DateTime.Now;
                notice.UpdateBy = "Takt365";
                notice.UpdateTime = DateTime.Now;
                await NoticeRepository.CreateAsync(notice);
                insertCount++;
                _logger.Info($"[创建] {category} '{notice.NoticeTitle}' 创建成功");
            }
            else
            {
                existingNotice.NoticeContent = notice.NoticeContent;
                existingNotice.NoticeType = notice.NoticeType;
                existingNotice.Status = notice.Status;
                existingNotice.NoticePublishTime = notice.NoticePublishTime;
                existingNotice.NoticeReceiverIds = notice.NoticeReceiverIds;
                existingNotice.NoticePriority = notice.NoticePriority;
                existingNotice.NoticeRequireConfirm = notice.NoticeRequireConfirm;
                existingNotice.NoticeAttachments = notice.NoticeAttachments;
                existingNotice.NoticeAccessUrl = notice.NoticeAccessUrl;
                existingNotice.NoticeReadCount = notice.NoticeReadCount;
                existingNotice.NoticeReadIds = notice.NoticeReadIds;
                existingNotice.NoticeConfirmCount = notice.NoticeConfirmCount;
                existingNotice.NoticeConfirmIds = notice.NoticeConfirmIds;
                existingNotice.NoticeLastReceiptTime = notice.NoticeLastReceiptTime;
                existingNotice.UpdateBy = "Takt365";
                existingNotice.UpdateTime = DateTime.Now;

                await NoticeRepository.UpdateAsync(existingNotice);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingNotice.NoticeTitle}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化日程数据
    /// </summary>
    /// <param name="schedules">日程列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeSchedulesAsync(List<TaktSchedule> schedules, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var schedule in schedules)
        {
            var existingSchedule = await ScheduleRepository.GetFirstAsync(s => s.Title == schedule.Title);
            if (existingSchedule == null)
            {
                schedule.CreateBy = "Takt365";
                schedule.CreateTime = DateTime.Now;
                schedule.UpdateBy = "Takt365";
                schedule.UpdateTime = DateTime.Now;
                await ScheduleRepository.CreateAsync(schedule);
                insertCount++;
                _logger.Info($"[创建] {category} '{schedule.Title}' 创建成功");
            }
            else
            {
                existingSchedule.Content = schedule.Content;
                existingSchedule.StartTime = schedule.StartTime;
                existingSchedule.EndTime = schedule.EndTime;
                existingSchedule.ScheduleType = schedule.ScheduleType;
                existingSchedule.Status = schedule.Status;
                existingSchedule.IsAllDay = schedule.IsAllDay;
                existingSchedule.RemindMinutes = schedule.RemindMinutes;
                existingSchedule.RepeatType = schedule.RepeatType;
                existingSchedule.RepeatEndTime = schedule.RepeatEndTime;
                existingSchedule.Location = schedule.Location;
                existingSchedule.UpdateBy = "Takt365";
                existingSchedule.UpdateTime = DateTime.Now;

                await ScheduleRepository.UpdateAsync(existingSchedule);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingSchedule.Title}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化会议数据
    /// </summary>
    /// <param name="meetings">会议列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeMeetingsAsync(List<TaktMeeting> meetings, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var meeting in meetings)
        {
            var existingMeeting = await MeetingRepository.GetFirstAsync(m => m.Title == meeting.Title);
            if (existingMeeting == null)
            {
                meeting.CreateBy = "Takt365";
                meeting.CreateTime = DateTime.Now;
                meeting.UpdateBy = "Takt365";
                meeting.UpdateTime = DateTime.Now;
                await MeetingRepository.CreateAsync(meeting);
                insertCount++;
                _logger.Info($"[创建] {category} '{meeting.Title}' 创建成功");
            }
            else
            {
                existingMeeting.Content = meeting.Content;
                existingMeeting.MeetingType = meeting.MeetingType;
                existingMeeting.Status = meeting.Status;
                existingMeeting.StartTime = meeting.StartTime;
                existingMeeting.EndTime = meeting.EndTime;
                existingMeeting.IsAllDay = meeting.IsAllDay;
                existingMeeting.Location = meeting.Location;
                existingMeeting.RoomId = meeting.RoomId;
                existingMeeting.OrganizerId = meeting.OrganizerId;
                existingMeeting.HostId = meeting.HostId;
                existingMeeting.RecorderId = meeting.RecorderId;
                existingMeeting.Participants = meeting.Participants;
                existingMeeting.NeedSignIn = meeting.NeedSignIn;
                existingMeeting.RemindMinutes = meeting.RemindMinutes;
                existingMeeting.MeetingLink = meeting.MeetingLink;
                existingMeeting.MeetingPassword = meeting.MeetingPassword;
                existingMeeting.IsPublic = meeting.IsPublic;
                existingMeeting.ActualStartTime = meeting.ActualStartTime;
                existingMeeting.ActualEndTime = meeting.ActualEndTime;
                existingMeeting.UpdateBy = "Takt365";
                existingMeeting.UpdateTime = DateTime.Now;

                await MeetingRepository.UpdateAsync(existingMeeting);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingMeeting.Title}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化编号规则数据
    /// </summary>
    /// <param name="numberRules">编号规则列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeNumberRulesAsync(List<TaktNumberingRules> numberRules, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var numberRule in numberRules)
        {
            var existingNumberRule = await NumberRuleRepository.GetFirstAsync(nr => nr.NumberRuleCode == numberRule.NumberRuleCode);
            if (existingNumberRule == null)
            {
                numberRule.CreateBy = "Takt365";
                numberRule.CreateTime = DateTime.Now;
                numberRule.UpdateBy = "Takt365";
                numberRule.UpdateTime = DateTime.Now;
                await NumberRuleRepository.CreateAsync(numberRule);
                insertCount++;
                _logger.Info($"[创建] {category} '{numberRule.NumberRuleName}' 创建成功");
            }
            else
            {
                existingNumberRule.NumberRuleName = numberRule.NumberRuleName;
                existingNumberRule.NumberRuleType = numberRule.NumberRuleType;
                existingNumberRule.NumberRuleDescription = numberRule.NumberRuleDescription;
                existingNumberRule.NumberPrefix = numberRule.NumberPrefix;
                existingNumberRule.NumberSuffix = numberRule.NumberSuffix;
                existingNumberRule.DateFormat = numberRule.DateFormat;
                existingNumberRule.SequenceLength = numberRule.SequenceLength;
                existingNumberRule.SequenceStart = numberRule.SequenceStart;
                existingNumberRule.SequenceStep = numberRule.SequenceStep;
                existingNumberRule.SequenceResetRule = numberRule.SequenceResetRule;
                existingNumberRule.NumberFormatTemplate = numberRule.NumberFormatTemplate;
                existingNumberRule.NumberExample = numberRule.NumberExample;
                existingNumberRule.IncludeCompanyCode = numberRule.IncludeCompanyCode;
                existingNumberRule.IncludeDepartmentCode = numberRule.IncludeDepartmentCode;
                existingNumberRule.IncludeYear = numberRule.IncludeYear;
                existingNumberRule.IncludeMonth = numberRule.IncludeMonth;
                existingNumberRule.IncludeDay = numberRule.IncludeDay;
                existingNumberRule.IncludeHour = numberRule.IncludeHour;
                existingNumberRule.IncludeMinute = numberRule.IncludeMinute;
                existingNumberRule.IncludeSecond = numberRule.IncludeSecond;
                existingNumberRule.IncludeMillisecond = numberRule.IncludeMillisecond;
                existingNumberRule.IncludeRandom = numberRule.IncludeRandom;
                existingNumberRule.RandomLength = numberRule.RandomLength;
                existingNumberRule.IncludeCheckDigit = numberRule.IncludeCheckDigit;
                existingNumberRule.CheckDigitAlgorithm = numberRule.CheckDigitAlgorithm;
                existingNumberRule.AllowDuplicate = numberRule.AllowDuplicate;
                existingNumberRule.DuplicateCheckScope = numberRule.DuplicateCheckScope;
                existingNumberRule.OrderNum = numberRule.OrderNum;
                existingNumberRule.Status = numberRule.Status;
                existingNumberRule.UpdateBy = "Takt365";
                existingNumberRule.UpdateTime = DateTime.Now;

                await NumberRuleRepository.UpdateAsync(existingNumberRule);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingNumberRule.NumberRuleName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化新闻数据
    /// </summary>
    /// <param name="news">新闻列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeNewsAsync(List<TaktNews> news, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var newsItem in news)
        {
            var existingNews = await NewsRepository.GetFirstAsync(n => n.NewsTitle == newsItem.NewsTitle);
            if (existingNews == null)
            {
                newsItem.CreateBy = "Takt365";
                newsItem.CreateTime = DateTime.Now;
                newsItem.UpdateBy = "Takt365";
                newsItem.UpdateTime = DateTime.Now;
                await NewsRepository.CreateAsync(newsItem);
                insertCount++;
                _logger.Info($"[创建] {category} '{newsItem.NewsTitle}' 创建成功");
            }
            else
            {
                existingNews.NewsSubtitle = newsItem.NewsSubtitle;
                existingNews.NewsContent = newsItem.NewsContent;
                existingNews.NewsSummary = newsItem.NewsSummary;
                existingNews.NewsCategory = newsItem.NewsCategory;
                existingNews.NewsTags = newsItem.NewsTags;
                existingNews.NewsAuthor = newsItem.NewsAuthor;
                existingNews.NewsSource = newsItem.NewsSource;
                existingNews.NewsSourceUrl = newsItem.NewsSourceUrl;
                existingNews.NewsCoverImage = newsItem.NewsCoverImage;
                existingNews.Status = newsItem.Status;
                existingNews.NewsPublishTime = newsItem.NewsPublishTime;
                existingNews.NewsOfflineTime = newsItem.NewsOfflineTime;
                existingNews.NewsIsTop = newsItem.NewsIsTop;
                existingNews.NewsIsRecommend = newsItem.NewsIsRecommend;
                existingNews.NewsIsHot = newsItem.NewsIsHot;
                existingNews.NewsReadCount = newsItem.NewsReadCount;
                existingNews.NewsLikeCount = newsItem.NewsLikeCount;
                existingNews.NewsCommentCount = newsItem.NewsCommentCount;
                existingNews.NewsShareCount = newsItem.NewsShareCount;
                existingNews.NewsRecommendCount = newsItem.NewsRecommendCount;
                existingNews.NewsEditorBy = newsItem.NewsEditorBy;
                existingNews.NewsEditTime = newsItem.NewsEditTime;
                existingNews.NewsAuditedBy = newsItem.NewsAuditedBy;
                existingNews.NewsAuditedTime = newsItem.NewsAuditedTime;
                existingNews.NewsAuditStatus = newsItem.NewsAuditStatus;
                existingNews.NewsAuditComments = newsItem.NewsAuditComments;
                existingNews.NewsSeoTitle = newsItem.NewsSeoTitle;
                existingNews.NewsSeoKeywords = newsItem.NewsSeoKeywords;
                existingNews.NewsSeoDescription = newsItem.NewsSeoDescription;
                existingNews.OrderNum = newsItem.OrderNum;
                existingNews.UpdateBy = "Takt365";
                existingNews.UpdateTime = DateTime.Now;

                await NewsRepository.UpdateAsync(existingNews);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingNews.NewsTitle}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化新闻评论数据
    /// </summary>
    /// <param name="newsComments">新闻评论列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeNewsCommentsAsync(List<TaktNewsComment> newsComments, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var newsComment in newsComments)
        {
            var existingNewsComment = await NewsCommentRepository.GetFirstAsync(nc => nc.CommentContent == newsComment.CommentContent && nc.CommentUserId == newsComment.CommentUserId);
            if (existingNewsComment == null)
            {
                newsComment.CreateBy = "Takt365";
                newsComment.CreateTime = DateTime.Now;
                newsComment.UpdateBy = "Takt365";
                newsComment.UpdateTime = DateTime.Now;
                await NewsCommentRepository.CreateAsync(newsComment);
                insertCount++;
                _logger.Info($"[创建] {category} '{newsComment.CommentContent.Substring(0, Math.Min(20, newsComment.CommentContent.Length))}...' 创建成功");
            }
            else
            {
                existingNewsComment.CommentUserName = newsComment.CommentUserName;
                existingNewsComment.CommentUserAvatar = newsComment.CommentUserAvatar;
                existingNewsComment.ParentCommentId = newsComment.ParentCommentId;
                existingNewsComment.ReplyUserId = newsComment.ReplyUserId;
                existingNewsComment.ReplyUserName = newsComment.ReplyUserName;
                existingNewsComment.CommentStatus = newsComment.CommentStatus;
                existingNewsComment.AuditedBy = newsComment.AuditedBy;
                existingNewsComment.AuditedTime = newsComment.AuditedTime;
                existingNewsComment.AuditComments = newsComment.AuditComments;
                existingNewsComment.AuditType = newsComment.AuditType;
                existingNewsComment.LikeCount = newsComment.LikeCount;
                existingNewsComment.ReplyCount = newsComment.ReplyCount;
                existingNewsComment.IpAddress = newsComment.IpAddress;
                existingNewsComment.UserAgent = newsComment.UserAgent;
                existingNewsComment.OrderNum = newsComment.OrderNum;
                existingNewsComment.UpdateBy = "Takt365";
                existingNewsComment.UpdateTime = DateTime.Now;

                await NewsCommentRepository.UpdateAsync(existingNewsComment);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingNewsComment.CommentContent.Substring(0, Math.Min(20, existingNewsComment.CommentContent.Length))}...' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化新闻话题数据
    /// </summary>
    /// <param name="newsTopics">新闻话题列表</param>
    /// <param name="category">分类名称</param>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeNewsTopicsAsync(List<TaktNewsTopic> newsTopics, string category)
    {
        int insertCount = 0;
        int updateCount = 0;

        foreach (var newsTopic in newsTopics)
        {
            var existingNewsTopic = await NewsTopicRepository.GetFirstAsync(nt => nt.TopicName == newsTopic.TopicName);
            if (existingNewsTopic == null)
            {
                newsTopic.CreateBy = "Takt365";
                newsTopic.CreateTime = DateTime.Now;
                newsTopic.UpdateBy = "Takt365";
                newsTopic.UpdateTime = DateTime.Now;
                await NewsTopicRepository.CreateAsync(newsTopic);
                insertCount++;
                _logger.Info($"[创建] {category} '{newsTopic.TopicName}' 创建成功");
            }
            else
            {
                existingNewsTopic.TopicDescription = newsTopic.TopicDescription;
                existingNewsTopic.TopicKeywords = newsTopic.TopicKeywords;
                existingNewsTopic.TopicCategory = newsTopic.TopicCategory;
                existingNewsTopic.TopicTags = newsTopic.TopicTags;
                existingNewsTopic.TopicIcon = newsTopic.TopicIcon;
                existingNewsTopic.TopicCover = newsTopic.TopicCover;
                existingNewsTopic.TopicColor = newsTopic.TopicColor;
                existingNewsTopic.Status = newsTopic.Status;
                existingNewsTopic.TopicIsHot = newsTopic.TopicIsHot;
                existingNewsTopic.TopicIsRecommend = newsTopic.TopicIsRecommend;
                existingNewsTopic.TopicIsTop = newsTopic.TopicIsTop;
                existingNewsTopic.TopicType = newsTopic.TopicType;
                existingNewsTopic.TopicStartTime = newsTopic.TopicStartTime;
                existingNewsTopic.TopicEndTime = newsTopic.TopicEndTime;
                existingNewsTopic.TopicParticipantCount = newsTopic.TopicParticipantCount;
                existingNewsTopic.TopicNewsCount = newsTopic.TopicNewsCount;
                existingNewsTopic.TopicCommentCount = newsTopic.TopicCommentCount;
                existingNewsTopic.TopicLikeCount = newsTopic.TopicLikeCount;
                existingNewsTopic.TopicShareCount = newsTopic.TopicShareCount;
                existingNewsTopic.TopicReadCount = newsTopic.TopicReadCount;
                existingNewsTopic.TopicCreatorId = newsTopic.TopicCreatorId;
                existingNewsTopic.TopicCreatorName = newsTopic.TopicCreatorName;
                existingNewsTopic.TopicAdminIds = newsTopic.TopicAdminIds;
                existingNewsTopic.TopicAdminNames = newsTopic.TopicAdminNames;
                existingNewsTopic.TopicRules = newsTopic.TopicRules;
                existingNewsTopic.TopicSettings = newsTopic.TopicSettings;
                existingNewsTopic.TopicSeoTitle = newsTopic.TopicSeoTitle;
                existingNewsTopic.TopicSeoKeywords = newsTopic.TopicSeoKeywords;
                existingNewsTopic.TopicSeoDescription = newsTopic.TopicSeoDescription;
                existingNewsTopic.OrderNum = newsTopic.OrderNum;
                existingNewsTopic.UpdateBy = "Takt365";
                existingNewsTopic.UpdateTime = DateTime.Now;

                await NewsTopicRepository.UpdateAsync(existingNewsTopic);
                updateCount++;
                _logger.Info($"[更新] {category} '{existingNewsTopic.TopicName}' 更新成功");
            }
        }

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 根据配置键获取配置值
    /// </summary>
    /// <param name="configKey">配置键</param>
    /// <returns>配置值</returns>
    public async Task<string?> GetConfigValueAsync(string configKey)
    {
        var config = await GeneralSettingsRepository.GetFirstAsync(c => c.SettingsKey == configKey && c.Status == 0);
        return config?.SettingsValue;
    }

    /// <summary>
    /// 根据语言代码和键获取语言值
    /// </summary>
    /// <param name="languageCode">语言代码</param>
    /// <param name="languageKey">语言键</param>
    /// <returns>语言值</returns>
    public async Task<string?> GetLanguageValueAsync(string languageCode, string languageKey)
    {
        var language = await LanguageRepository.GetFirstAsync(l => l.LangCode == languageCode && l.I18nStatus == 0);
        return language?.LangName;
    }

    /// <summary>
    /// 清理配置数据
    /// </summary>
    /// <param name="configKey">配置键</param>
    /// <returns>清理结果</returns>
    public async Task<bool> CleanConfigDataAsync(string configKey)
    {
        try
        {
            var result = await GeneralSettingsRepository.DeleteAsync(c => c.SettingsKey == configKey);
            _logger.Info($"清理配置键 '{configKey}' 的数据，共删除 {result} 条记录");
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.Error($"清理配置数据失败: {ex.Message}", ex);
            return false;
        }
    }

    /// <summary>
    /// 清理语言数据
    /// </summary>
    /// <param name="languageCode">语言代码</param>
    /// <returns>清理结果</returns>
    public async Task<bool> CleanLanguageDataAsync(string languageCode)
    {
        try
        {
            var result = await LanguageRepository.DeleteAsync(l => l.LangCode == languageCode);
            _logger.Info($"清理语言代码 '{languageCode}' 的数据，共删除 {result} 条记录");
            return result > 0;
        }
        catch (Exception ex)
        {
            _logger.Error($"清理语言数据失败: {ex.Message}", ex);
            return false;
        }
    }
}

/// <summary>
/// 常规业务模块种子初始化结果
/// </summary>
public class RoutineSeedResult
{
    /// <summary>
    /// 配置初始化结果
    /// </summary>
    public (int insertCount, int updateCount) ConfigResults { get; set; }

    /// <summary>
    /// 语言初始化结果
    /// </summary>
    public (int insertCount, int updateCount) LanguageResults { get; set; }

    /// <summary>
    /// 车辆初始化结果
    /// </summary>
    public (int insertCount, int updateCount) VehicleResults { get; set; }

    /// <summary>
    /// 车辆预约初始化结果
    /// </summary>
    public (int insertCount, int updateCount) VehicleBookingResults { get; set; }

    /// <summary>
    /// 驾驶员初始化结果
    /// </summary>
    public (int insertCount, int updateCount) DriverResults { get; set; }

    /// <summary>
    /// 通知初始化结果
    /// </summary>
    public (int insertCount, int updateCount) NoticeResults { get; set; }

    /// <summary>
    /// 新闻初始化结果
    /// </summary>
    public (int insertCount, int updateCount) NewsResults { get; set; }

    /// <summary>
    /// 新闻评论初始化结果
    /// </summary>
    public (int insertCount, int updateCount) NewsCommentResults { get; set; }

    /// <summary>
    /// 新闻话题初始化结果
    /// </summary>
    public (int insertCount, int updateCount) NewsTopicResults { get; set; }

    /// <summary>
    /// 日程初始化结果
    /// </summary>
    public (int insertCount, int updateCount) ScheduleResults { get; set; }

    /// <summary>
    /// 会议初始化结果
    /// </summary>
    public (int insertCount, int updateCount) MeetingResults { get; set; }

    /// <summary>
    /// 编号规则初始化结果
    /// </summary>
    public (int insertCount, int updateCount) NumberRuleResults { get; set; }

    /// <summary>
    /// 获取配置总数
    /// </summary>
    /// <returns>总数</returns>
    public int GetTotalConfigCount()
    {
        return ConfigResults.insertCount + ConfigResults.updateCount;
    }

    /// <summary>
    /// 获取语言总数
    /// </summary>
    /// <returns>总数</returns>
    public int GetTotalLanguageCount()
    {
        return LanguageResults.insertCount + LanguageResults.updateCount;
    }

    /// <summary>
    /// 获取总插入数
    /// </summary>
    /// <returns>插入数</returns>
    public int GetTotalInsertCount()
    {
        return ConfigResults.insertCount + LanguageResults.insertCount + VehicleResults.insertCount + VehicleBookingResults.insertCount + DriverResults.insertCount + NoticeResults.insertCount + NewsResults.insertCount + NewsCommentResults.insertCount + NewsTopicResults.insertCount + ScheduleResults.insertCount + MeetingResults.insertCount + NumberRuleResults.insertCount;
    }

    /// <summary>
    /// 获取总更新数
    /// </summary>
    /// <returns>更新数</returns>
    public int GetTotalUpdateCount()
    {
        return ConfigResults.updateCount + LanguageResults.updateCount + VehicleResults.updateCount + VehicleBookingResults.updateCount + DriverResults.updateCount + NoticeResults.updateCount + NewsResults.updateCount + NewsCommentResults.updateCount + NewsTopicResults.updateCount + ScheduleResults.updateCount + MeetingResults.updateCount + NumberRuleResults.updateCount;
    }
}




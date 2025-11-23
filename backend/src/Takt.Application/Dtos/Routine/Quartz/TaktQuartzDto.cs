//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktQuartzDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 定时任务数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Quartz
{
    /// <summary>
    /// 定时任务基础DTO
    /// </summary>
    public class TaktQuartzDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzAssemblyName = string.Empty;
            QuartzClassName = string.Empty;
            QuartzCronExpression = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long QuartzId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string QuartzAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string QuartzClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string QuartzCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public bool QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? QuartzEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 最后运行时间
        /// </summary>
        public DateTime? QuartzLastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        public DateTime? QuartzNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int QuartzExecuteCount { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 定时任务查询DTO
    /// </summary>
    public class TaktQuartzQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzQueryDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string? QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [MaxLength(100, ErrorMessage = "任务组名长度不能超过100个字符")]
        public string? QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int? QuartzType { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int? QuartzTriggerType { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建者（用于当前用户定时任务过滤）
        /// </summary>
        public string? CreateBy { get; set; }
    }

    /// <summary>
    /// 定时任务创建DTO
    /// </summary>
    public class TaktQuartzCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzCreateDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzAssemblyName = string.Empty;
            QuartzClassName = string.Empty;
            QuartzCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        [Required(ErrorMessage = "任务名称不能为空")]
        [MaxLength(100, ErrorMessage = "任务名称长度不能超过100个字符")]
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        [Required(ErrorMessage = "任务组名不能为空")]
        [MaxLength(100, ErrorMessage = "任务组名长度不能超过100个字符")]
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        [Required(ErrorMessage = "任务类型不能为空")]
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        [MaxLength(255, ErrorMessage = "程序集名称长度不能超过255个字符")]
        public string QuartzAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        [MaxLength(255, ErrorMessage = "任务类名长度不能超过255个字符")]
        public string QuartzClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        [MaxLength(255, ErrorMessage = "API接口地址长度不能超过255个字符")]
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        [MaxLength(10, ErrorMessage = "请求方式长度不能超过10个字符")]
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        [MaxLength(1000, ErrorMessage = "SQL语句长度不能超过1000个字符")]
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        [Required(ErrorMessage = "触发器类型不能为空")]
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        [Required(ErrorMessage = "Cron表达式不能为空")]
        [MaxLength(100, ErrorMessage = "Cron表达式长度不能超过100个字符")]
        public string QuartzCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public bool QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? QuartzEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        [MaxLength(500, ErrorMessage = "执行参数长度不能超过500个字符")]
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255, ErrorMessage = "备注长度不能超过255个字符")]
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 定时任务更新DTO
    /// </summary>
    public class TaktQuartzUpdateDto : TaktQuartzCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        [Required(ErrorMessage = "任务ID不能为空")]
        public long QuartzId { get; set; }
    }

    /// <summary>
    /// 定时任务删除DTO
    /// </summary>
    public class TaktQuartzDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long QuartzId { get; set; }
    }

    /// <summary>
    /// 定时任务批量删除DTO
    /// </summary>
    public class TaktQuartzBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> QuartzIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 定时任务导入DTO
    /// </summary>
    public class TaktQuartzImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzImportDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzAssemblyName = string.Empty;
            QuartzClassName = string.Empty;
            QuartzCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型（1程序集 2网络请求 3SQL语句）
        /// </summary>
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string QuartzAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string QuartzClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型（0简单 1Cron）
        /// </summary>
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string QuartzCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public int QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? QuartzEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 状态（0停用 1启用）
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 定时任务导出DTO
    /// </summary>
    public class TaktQuartzExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzExportDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzAssemblyName = string.Empty;
            QuartzClassName = string.Empty;
            QuartzCronExpression = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string QuartzAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string QuartzClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型
        /// </summary>
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string QuartzCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 是否并发执行
        /// </summary>
        public int QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? QuartzEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 最后运行时间
        /// </summary>
        public DateTime? QuartzLastRunTime { get; set; }

        /// <summary>
        /// 下次运行时间
        /// </summary>
        public DateTime? QuartzNextRunTime { get; set; }

        /// <summary>
        /// 执行次数
        /// </summary>
        public int QuartzExecuteCount { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 定时任务导入模板DTO
    /// </summary>
    public class TaktQuartzTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktQuartzTemplateDto()
        {
            QuartzName = string.Empty;
            QuartzGroupName = string.Empty;
            QuartzAssemblyName = string.Empty;
            QuartzClassName = string.Empty;
            QuartzCronExpression = string.Empty;
        }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string QuartzName { get; set; }

        /// <summary>
        /// 任务组名
        /// </summary>
        public string QuartzGroupName { get; set; }

        /// <summary>
        /// 任务类型(1=程序集,2=网络请求,3=SQL语句)
        /// </summary>
        public int QuartzType { get; set; }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string QuartzAssemblyName { get; set; }

        /// <summary>
        /// 任务类名
        /// </summary>
        public string QuartzClassName { get; set; }

        /// <summary>
        /// API接口地址
        /// </summary>
        public string? QuartzApiUrl { get; set; }

        /// <summary>
        /// 请求方式
        /// </summary>
        public string? QuartzRequestMethod { get; set; }

        /// <summary>
        /// SQL语句
        /// </summary>
        public string? QuartzSql { get; set; }

        /// <summary>
        /// 触发器类型(0=简单,1=Cron)
        /// </summary>
        public int QuartzTriggerType { get; set; }

        /// <summary>
        /// Cron表达式
        /// </summary>
        public string QuartzCronExpression { get; set; }

        /// <summary>
        /// 执行间隔（秒）
        /// </summary>
        public int QuartzInterval { get; set; }

        /// <summary>
        /// 是否并发执行(0=否,1=是)
        /// </summary>
        public int QuartzConcurrent { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public string? QuartzStartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public string? QuartzEndTime { get; set; }

        /// <summary>
        /// 执行参数
        /// </summary>
        public string? QuartzExecuteParams { get; set; }

        /// <summary>
        /// 状态(0=停用,1=启用)
        /// </summary>
        public int Status { get; set; }
    }
}



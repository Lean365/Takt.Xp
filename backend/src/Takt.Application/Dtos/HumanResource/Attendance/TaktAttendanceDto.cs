//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAttendanceDto.cs
// 创建者:Takt(Claude AI)
// 创建时间: 2024-01-17
// 版本号: V0.0.1
// 描述: 考勤数据传输对象
//===================================================================

using System;

namespace Takt.Application.Dtos.HumanResource.Attendance
{
    /// <summary>
    /// 考勤基础DTO
    /// </summary>
    public class TaktAttendanceDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 考勤ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长(小时)
        /// </summary>
        public decimal WorkHours { get; set; }

        /// <summary>
        /// 加班时长(小时)
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 迟到时长(分钟)
        /// </summary>
        public int LateMinutes { get; set; }

        /// <summary>
        /// 早退时长(分钟)
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 考勤状态(1=正常 2=迟到 3=早退 4=旷工 5=请假)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

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
    }

    /// <summary>
    /// 考勤查询DTO
    /// </summary>
    public class TaktAttendanceQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceQueryDto()
        {
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long? EmployeeId { get; set; }

        /// <summary>
        /// 考勤日期开始
        /// </summary>
        public DateTime? AttendanceDateStart { get; set; }

        /// <summary>
        /// 考勤日期结束
        /// </summary>
        public DateTime? AttendanceDateEnd { get; set; }

        /// <summary>
        /// 考勤状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 考勤创建DTO
    /// </summary>
    public class TaktAttendanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceCreateDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工ID
        /// </summary>
        public long EmployeeId { get; set; }

        /// <summary>
        /// 考勤日期
        /// </summary>
        public DateTime AttendanceDate { get; set; }

        /// <summary>
        /// 上班时间
        /// </summary>
        public DateTime? CheckInTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public DateTime? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长(小时)
        /// </summary>
        public decimal WorkHours { get; set; }

        /// <summary>
        /// 加班时长(小时)
        /// </summary>
        public decimal OvertimeHours { get; set; }

        /// <summary>
        /// 迟到时长(分钟)
        /// </summary>
        public int LateMinutes { get; set; }

        /// <summary>
        /// 早退时长(分钟)
        /// </summary>
        public int EarlyLeaveMinutes { get; set; }

        /// <summary>
        /// 考勤状态(1=正常 2=迟到 3=早退 4=旷工 5=请假)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 考勤更新DTO
    /// </summary>
    public class TaktAttendanceUpdateDto : TaktAttendanceCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceUpdateDto() : base()
        {
        }

        /// <summary>
        /// 考勤ID
        /// </summary>
        [AdaptMember("Id")]
        public long AttendanceId { get; set; }
    }

    /// <summary>
    /// 考勤删除DTO
    /// </summary>
    public class TaktAttendanceDeleteDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceDeleteDto()
        {
            Remark = string.Empty;
        }

        /// <summary>
        /// 考勤ID
        /// </summary>
        [AdaptMember("Id")]
        public long AttendanceId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 考勤导入DTO
    /// </summary>
    public class TaktAttendanceImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceImportDto()
        {
            EmployeeNo = string.Empty;
            AttendanceDate = string.Empty;
            CheckInTime = string.Empty;
            CheckOutTime = string.Empty;
            WorkHours = string.Empty;
            OvertimeHours = string.Empty;
            LateMinutes = string.Empty;
            EarlyLeaveMinutes = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 考勤日期
        /// </summary>
        public string AttendanceDate { get; set; } = string.Empty;

        /// <summary>
        /// 上班时间
        /// </summary>
        public string? CheckInTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public string? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长(小时)
        /// </summary>
        public string WorkHours { get; set; } = string.Empty;

        /// <summary>
        /// 加班时长(小时)
        /// </summary>
        public string OvertimeHours { get; set; } = string.Empty;

        /// <summary>
        /// 迟到时长(分钟)
        /// </summary>
        public string LateMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 早退时长(分钟)
        /// </summary>
        public string EarlyLeaveMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }

    /// <summary>
    /// 考勤导出DTO
    /// </summary>
    public class TaktAttendanceExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceExportDto()
        {
            EmployeeNo = string.Empty;
            EmployeeName = string.Empty;
            AttendanceDate = string.Empty;
            CheckInTime = string.Empty;
            CheckOutTime = string.Empty;
            WorkHours = string.Empty;
            OvertimeHours = string.Empty;
            LateMinutes = string.Empty;
            EarlyLeaveMinutes = string.Empty;
            Status = string.Empty;
            CreateTime = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工姓名
        /// </summary>
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>
        /// 考勤日期
        /// </summary>
        public string AttendanceDate { get; set; } = string.Empty;

        /// <summary>
        /// 上班时间
        /// </summary>
        public string? CheckInTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public string? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长(小时)
        /// </summary>
        public string WorkHours { get; set; } = string.Empty;

        /// <summary>
        /// 加班时长(小时)
        /// </summary>
        public string OvertimeHours { get; set; } = string.Empty;

        /// <summary>
        /// 迟到时长(分钟)
        /// </summary>
        public string LateMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 早退时长(分钟)
        /// </summary>
        public string EarlyLeaveMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; } = string.Empty;
    }

    /// <summary>
    /// 考勤模板DTO
    /// </summary>
    public class TaktAttendanceTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAttendanceTemplateDto()
        {
            EmployeeNo = string.Empty;
            AttendanceDate = string.Empty;
            CheckInTime = string.Empty;
            CheckOutTime = string.Empty;
            WorkHours = string.Empty;
            OvertimeHours = string.Empty;
            LateMinutes = string.Empty;
            EarlyLeaveMinutes = string.Empty;
            Status = string.Empty;
            Remark = string.Empty;
        }

        /// <summary>
        /// 员工编号
        /// </summary>
        public string EmployeeNo { get; set; } = string.Empty;

        /// <summary>
        /// 考勤日期
        /// </summary>
        public string AttendanceDate { get; set; } = string.Empty;

        /// <summary>
        /// 上班时间
        /// </summary>
        public string? CheckInTime { get; set; }

        /// <summary>
        /// 下班时间
        /// </summary>
        public string? CheckOutTime { get; set; }

        /// <summary>
        /// 工作时长(小时)
        /// </summary>
        public string WorkHours { get; set; } = string.Empty;

        /// <summary>
        /// 加班时长(小时)
        /// </summary>
        public string OvertimeHours { get; set; } = string.Empty;

        /// <summary>
        /// 迟到时长(分钟)
        /// </summary>
        public string LateMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 早退时长(分钟)
        /// </summary>
        public string EarlyLeaveMinutes { get; set; } = string.Empty;

        /// <summary>
        /// 考勤状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
} 



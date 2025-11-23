#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSqlDiffLogDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : SqlSugar差异日志数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Logging
{
    /// <summary>
    /// SqlSugar差异日志基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktSqlDiffLogDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSqlDiffLogDto()
        {
            TableName = string.Empty;
            OperBy = string.Empty;
            DiffType = string.Empty;
            BusinessData = string.Empty;
            BeforeData = string.Empty;
            AfterData = string.Empty;
            ExecuteSql = string.Empty;
            SqlParameters = string.Empty;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long SqlDiffLogId { get; set; }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 差异类型（Insert、Update、Delete）
        /// </summary>
        public string DiffType { get; set; }

        /// <summary>
        /// 业务数据（记录业务对象参数，如：{"title": "修改用户", "Modular": 1, "Operator": "admin"}）
        /// </summary>
        public string? BusinessData { get; set; }

        /// <summary>
        /// 变更前的数据（JSON格式，包含字段描述、列名、值、表名、表描述）
        /// </summary>
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据（JSON格式，包含字段描述、列名、值、表名、表描述）
        /// </summary>
        public string? AfterData { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数（JSON格式）
        /// </summary>
        public string? SqlParameters { get; set; }

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
    /// SqlSugar差异日志查询DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktSqlDiffLogQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSqlDiffLogQueryDto()
        {
            TableName = string.Empty;
            DiffType = string.Empty;
            BusinessData = string.Empty;
            OperBy = string.Empty;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 差异类型
        /// </summary>
        public string DiffType { get; set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        public string BusinessData { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// SqlSugar差异日志导出DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public class TaktSqlDiffLogExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktSqlDiffLogExportDto()
        {
            TableName = string.Empty;
            OperBy = string.Empty;
            DiffType = string.Empty;
            BusinessData = string.Empty;
            BeforeData = string.Empty;
            AfterData = string.Empty;
            ExecuteSql = string.Empty;
            SqlParameters = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string OperBy { get; set; }

        /// <summary>
        /// 差异类型
        /// </summary>
        public string DiffType { get; set; }

        /// <summary>
        /// 业务数据
        /// </summary>
        public string? BusinessData { get; set; }

        /// <summary>
        /// 变更前的数据
        /// </summary>
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据
        /// </summary>
        public string? AfterData { get; set; }

        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数
        /// </summary>
        public string? SqlParameters { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}





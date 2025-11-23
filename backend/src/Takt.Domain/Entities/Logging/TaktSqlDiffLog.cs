#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSqlDiffLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 15:45
// 版本号 : V.0.0.1
// 描述    : SqlSugar差异日志实体
//===================================================================

namespace Takt.Domain.Entities.Logging
{
    /// <summary>
    /// SqlSugar差异日志实体
    /// </summary>
    [SugarTable("Takt_audit_diff_log", "差异日志")]
    public class TaktSqlDiffLog : TaktBaseEntity
    {
        /// <summary>
        /// 表名
        /// </summary>
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string TableName { get; set; } = string.Empty;

        /// <summary>
        /// 操作人（记录执行操作的用户名，如：admin、张三等）
        /// </summary>
        [SugarColumn(ColumnName = "oper_by", ColumnDescription = "操作人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string OperBy { get; set; } = string.Empty;

        /// <summary>
        /// 差异类型（Insert、Update、Delete）
        /// </summary>
        [SugarColumn(ColumnName = "diff_type", ColumnDescription = "差异类型", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DiffType { get; set; } = string.Empty;

        /// <summary>
        /// 业务数据（记录业务对象参数，如：{"title": "修改用户", "Modular": 1, "Operator": "admin"}）
        /// </summary>
        [SugarColumn(ColumnName = "business_data", ColumnDescription = "业务数据", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BusinessData { get; set; }

        /// <summary>
        /// 变更前的数据（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "before_data", ColumnDescription = "变更前的数据", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeData { get; set; }

        /// <summary>
        /// 变更后的数据（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "after_data", ColumnDescription = "变更后的数据", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterData { get; set; }


        /// <summary>
        /// 执行的SQL语句
        /// </summary>
        [SugarColumn(ColumnName = "execute_sql", ColumnDescription = "执行的SQL语句", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExecuteSql { get; set; }

        /// <summary>
        /// SQL参数（JSON格式）
        /// </summary>
        [SugarColumn(ColumnName = "sql_parameters", ColumnDescription = "SQL参数", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SqlParameters { get; set; }
    }
}





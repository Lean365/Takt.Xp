#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGeneralSettings.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 通用设置实体
//===================================================================

namespace Takt.Domain.Entities.Routine.Settings
{
    /// <summary>
    /// 通用设置实体
    /// </summary>
    [SugarTable("Takt_routine_general_settings", "通用设置")]
    [SugarIndex("ix_name_settings", nameof(SettingsName), OrderByType.Asc)]
    public class TaktGeneralSettings : TaktBaseEntity
    {
        /// <summary>
        /// 设置名称
        /// </summary>
        [SugarColumn(ColumnName = "settings_name", ColumnDescription = "设置名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? SettingsName { get; set; }

        /// <summary>
        /// 设置键名
        /// </summary>
        [SugarColumn(ColumnName = "settings_key", ColumnDescription = "设置键名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? SettingsKey { get; set; }

        /// <summary>
        /// 设置键值
        /// </summary>
        [SugarColumn(ColumnName = "settings_value", ColumnDescription = "设置键值", Length = 500, ColumnDataType = "nvarchar", IsNullable = false)]
        public string? SettingsValue { get; set; }

        /// <summary>
        /// 设置类别（0是前端，1是后端）
        /// </summary>
        [SugarColumn(ColumnName = "settings_type", ColumnDescription = "设置类别", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int SettingsType { get; set; } = 1;


        /// <summary>
        /// 系统内置（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_builtin", ColumnDescription = "内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsBuiltin { get; set; } = 1;

        /// <summary>
        /// 是否加密（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_encrypted", ColumnDescription = "加密", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsEncrypted { get; set; } = 1;

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
}




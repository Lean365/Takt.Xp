#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictType.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典类型实体
//===================================================================

using Takt;

namespace Takt.Domain.Entities.Routine.DataDictionary
{
    /// <summary>
    /// 字典类型实体
    /// </summary>
    [SugarTable("Takt_routine_dict_type", "字典类型")]
    [SugarIndex("ix_dict_type", nameof(DictType), OrderByType.Asc, true)]
    [SugarIndex("ix_dict_name", nameof(DictName), OrderByType.Asc, false)]
    [SugarIndex("ix_dict_source", nameof(DictSource), OrderByType.Asc, false)]
    [SugarIndex("ix_dict_status", nameof(DictStatus), OrderByType.Asc, false)]
    public class TaktDictType : TaktBaseEntity
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典名称
        /// </summary>
        [SugarColumn(ColumnName = "dict_name", ColumnDescription = "字典名称", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictName { get; set; } = string.Empty;

        /// <summary>
        /// 字典数据源（0系统 1SQL）
        /// </summary>
        [SugarColumn(ColumnName = "dict_source", ColumnDescription = "字典数据源", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DictSource { get; set; } = 0;

        /// <summary>
        /// SQL脚本
        /// </summary>
        [SugarColumn(ColumnName = "sql_script", ColumnDescription = "SQL脚本", Length = -1, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? SqlScript { get; set; }

        /// <summary>
        /// 字典内置（0是 1否）
        /// </summary>
        [SugarColumn(ColumnName = "is_builtin", ColumnDescription = "内置", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsBuiltin { get; set; } = 1;
        /// <summary>
        /// 字典状态（0正常 1停用）
        /// </summary>
        [SugarColumn(ColumnName = "dict_status", ColumnDescription = "字典状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DictStatus { get; set; } = 0;

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 字典数据列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktDictData.DictType))]
        public List<TaktDictData>? DictDataList { get; set; }
    }
}




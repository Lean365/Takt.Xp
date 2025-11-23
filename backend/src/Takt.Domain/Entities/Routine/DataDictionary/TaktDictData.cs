#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictData.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 字典数据实体
//===================================================================

namespace Takt.Domain.Entities.Routine.DataDictionary
{
    /// <summary>
    /// 字典数据实体
    /// </summary>
    [SugarTable("Takt_routine_dict_data", "字典数据")]
    [SugarIndex("ix_dict_type", nameof(DictType), OrderByType.Asc, false)]
    [SugarIndex("ix_dict_value", nameof(DictValue), OrderByType.Asc, false)]
    public class TaktDictData : TaktBaseEntity
    {
        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictType { get; set; } = string.Empty;

        /// <summary>
        /// 字典标签
        /// </summary>
        [SugarColumn(ColumnName = "dict_label", ColumnDescription = "字典标签", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [SugarColumn(ColumnName = "dict_value", ColumnDescription = "字典键值", Length = 100, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
        public string DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        [SugarColumn(ColumnName = "ext_label", ColumnDescription = "扩展标签", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        [SugarColumn(ColumnName = "ext_value", ColumnDescription = "扩展键值", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? TransKey { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        [SugarColumn(ColumnName = "css_class", ColumnDescription = "CSS类名", ColumnDataType = "int", IsNullable = true)]
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        [SugarColumn(ColumnName = "list_class", ColumnDescription = "列表类名", ColumnDataType = "int", IsNullable = true)]
        public int? ListClass { get; set; }


        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 字典类型
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(DictType))]
        public TaktDictType? DictTypeEntity { get; set; }
    }
}




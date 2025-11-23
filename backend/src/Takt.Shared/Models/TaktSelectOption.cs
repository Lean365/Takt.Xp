//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSelectOption.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-21 10:30
// 版本号 : V0.0.1
// 描述   : 下拉选择框选项
//===================================================================

namespace Takt.Shared.Models
{
    /// <summary>
    /// 下拉选择框选项
    /// </summary>
    public class TaktSelectOption
    {
        /// <summary>
        /// 字典标签
        /// </summary>
        public string DictLabel { get; set; } = string.Empty;

        /// <summary>
        /// 字典键值
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        public object DictValue { get; set; } = string.Empty;

        /// <summary>
        /// 扩展标签
        /// </summary>
        public string? ExtLabel { get; set; }

        /// <summary>
        /// 扩展键值
        /// </summary>
        public object? ExtValue { get; set; }

        /// <summary>
        /// 翻译键
        /// </summary>
        public string? TransKey { get; set; }

        /// <summary>
        /// CSS类名
        /// </summary>
        public int? CssClass { get; set; }

        /// <summary>
        /// 列表类名
        /// </summary>
        public int? ListClass { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; } = 0;
    }
} 





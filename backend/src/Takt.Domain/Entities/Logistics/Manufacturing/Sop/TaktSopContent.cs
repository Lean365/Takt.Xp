#nullable enable

using SqlSugar;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSopContent.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : SOP内容表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Manufacturing.Sop
{
    /// <summary>
    /// SOP内容表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 描述: SOP内容存储
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_sop_content", "SOP内容表")]
    [SugarIndex("ix_sop_id", nameof(SopId), OrderByType.Asc, false)]
    [SugarIndex("ix_seq_num", nameof(SeqNum), OrderByType.Asc, false)]
    public class TaktSopContent : TaktBaseEntity
    {
        /// <summary>
        /// SOP编号
        /// </summary>
        [SugarColumn(ColumnName = "sop_id", ColumnDescription = "SOP编号", Length = 20, ColumnDataType = "varchar", IsNullable = false)]
        public string SopId { get; set; } = string.Empty;

        /// <summary>
        /// 内容类型(1=文本 2=图片 3=视频 4=PDF)
        /// </summary>
        [SugarColumn(ColumnName = "content_type", ColumnDescription = "内容类型", ColumnDataType = "tinyint", IsNullable = false)]
        public byte ContentType { get; set; }

        /// <summary>
        /// 内容存储路径
        /// </summary>
        [SugarColumn(ColumnName = "content_url", ColumnDescription = "内容存储路径", Length = 255, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ContentUrl { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        [SugarColumn(ColumnName = "content_text", ColumnDescription = "文本内容", ColumnDataType = "nvarchar(max)", IsNullable = true)]
        public string? ContentText { get; set; }

        /// <summary>
        /// 步骤顺序
        /// </summary>
        [SugarColumn(ColumnName = "seq_num", ColumnDescription = "步骤顺序", ColumnDataType = "int", IsNullable = false)]
        public int SeqNum { get; set; }

        /// <summary>
        /// 作业重点
        /// </summary>
        [SugarColumn(ColumnName = "key_points", ColumnDescription = "作业重点", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? KeyPoints { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        [SugarColumn(ColumnName = "precautions", ColumnDescription = "注意事项", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Precautions { get; set; }

        // ==================== 导航属性 ====================

        /// <summary>
        /// 关联的SOP主表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public TaktSopHead? SopHead { get; set; }
    }
}





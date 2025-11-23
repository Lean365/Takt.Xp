#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktAdvert.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 统一广告实体
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Routine.Advert
{
    /// <summary>
    /// 统一广告实体
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    [SugarTable("Takt_routine_advert", "系统广告")]
    [SugarIndex("ix_advert_title", nameof(AdvertTitle), OrderByType.Asc)]
    [SugarIndex("ix_advert_type", nameof(AdvertType), OrderByType.Asc)]
    [SugarIndex("ix_advert_status", nameof(Status), OrderByType.Asc)]
    public class TaktAdvert : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        #region 基础信息

        /// <summary>
        /// 广告标题
        /// </summary>
        [SugarColumn(ColumnName = "advert_title", ColumnDescription = "广告标题", ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        [SugarColumn(ColumnName = "advert_subtitle", ColumnDescription = "广告副标题", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        [SugarColumn(ColumnName = "advert_description", ColumnDescription = "广告描述", ColumnDataType = "nvarchar", Length = 1000, IsNullable = true)]
        public string? AdvertDescription { get; set; }

        /// <summary>
        /// 广告图片URL
        /// </summary>
        [SugarColumn(ColumnName = "advert_image_url", ColumnDescription = "广告图片URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = false)]
        public string AdvertImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 广告链接URL
        /// </summary>
        [SugarColumn(ColumnName = "advert_link_url", ColumnDescription = "广告链接URL", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? AdvertLinkUrl { get; set; }

        /// <summary>
        /// 广告类型
        /// 1: 横幅广告,2: 角落广告,3: 浮动广告,4: 插屏广告,5: 覆盖广告,6: 弹窗广告,7: 开屏广告
        /// </summary>
        [SugarColumn(ColumnName = "advert_type", ColumnDescription = "广告类型", ColumnDataType = "int", IsNullable = false)]
        public int AdvertType { get; set; }

        /// <summary>
        /// 广告位置
        /// </summary>
        [SugarColumn(ColumnName = "advert_position", ColumnDescription = "广告位置", ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
        public string AdvertPosition { get; set; } = string.Empty;

        #endregion 基础信息

        #region 状态管理

        /// <summary>
        /// 广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        [SugarColumn(ColumnName = "Status", ColumnDescription = "广告状态", ColumnDataType = "int", IsNullable = false)]
        public int Status { get; set; } = 0;

        /// <summary>
        /// 广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        [SugarColumn(ColumnName = "audit_status", ColumnDescription = "广告审核状态", ColumnDataType = "int", IsNullable = false)]
        public int AuditStatus { get; set; } = 0;

        /// <summary>
        /// 审批意见
        /// </summary>
        [SugarColumn(ColumnName = "audit_comments", ColumnDescription = "审批意见", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
        public string? AuditComments { get; set; }

        /// <summary>
        /// 广告审核人
        /// </summary>
        [SugarColumn(ColumnName = "audited_by", ColumnDescription = "广告审核人", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 广告审核时间
        /// </summary>
        [SugarColumn(ColumnName = "audited_time", ColumnDescription = "广告审核时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? AuditedTime { get; set; }

        #endregion 状态管理

        #region 时间管理

        /// <summary>
        /// 广告发布时间
        /// </summary>
        [SugarColumn(ColumnName = "publish_time", ColumnDescription = "广告发布时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 广告下线时间
        /// </summary>
        [SugarColumn(ColumnName = "offline_time", ColumnDescription = "广告下线时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? OfflineTime { get; set; }

        /// <summary>
        /// 广告开始时间
        /// </summary>
        [SugarColumn(ColumnName = "start_time", ColumnDescription = "广告开始时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 广告结束时间
        /// </summary>
        [SugarColumn(ColumnName = "end_time", ColumnDescription = "广告结束时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EndTime { get; set; }

        #endregion 时间管理

        #region 显示控制

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "is_top", ColumnDescription = "是否置顶", ColumnDataType = "int", IsNullable = false)]
        public int IsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "is_recommend", ColumnDescription = "是否推荐", ColumnDataType = "int", IsNullable = false)]
        public int IsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "is_hot", ColumnDescription = "是否热门", ColumnDataType = "int", IsNullable = false)]
        public int IsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false)]
        public int OrderNum { get; set; } = 0;

        #endregion 显示控制

        #region 尺寸和样式

        /// <summary>
        /// 广告宽度
        /// </summary>
        [SugarColumn(ColumnName = "width", ColumnDescription = "广告宽度", ColumnDataType = "int", IsNullable = false)]
        public int Width { get; set; } = 0;

        /// <summary>
        /// 广告高度
        /// </summary>
        [SugarColumn(ColumnName = "height", ColumnDescription = "广告高度", ColumnDataType = "int", IsNullable = false)]
        public int Height { get; set; } = 0;

        /// <summary>
        /// 广告透明度 (0-100)
        /// </summary>
        [SugarColumn(ColumnName = "opacity", ColumnDescription = "广告透明度", ColumnDataType = "int", IsNullable = false)]
        public int Opacity { get; set; } = 0;

        #endregion 尺寸和样式

        #region 交互控制

        /// <summary>
        /// 是否可关闭 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "closable", ColumnDescription = "是否可关闭", ColumnDataType = "int", IsNullable = false)]
        public int Closable { get; set; } = 0;

        /// <summary>
        /// 是否可拖拽 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "draggable", ColumnDescription = "是否可拖拽", ColumnDataType = "int", IsNullable = false)]
        public int Draggable { get; set; } = 0;

        /// <summary>
        /// 是否可最小化 (0: 否, 1: 是)
        /// </summary>
        [SugarColumn(ColumnName = "minimizable", ColumnDescription = "是否可最小化", ColumnDataType = "int", IsNullable = false)]
        public int Minimizable { get; set; } = 0;

        /// <summary>
        /// 最小化后宽度
        /// </summary>
        [SugarColumn(ColumnName = "minimized_width", ColumnDescription = "最小化后宽度", ColumnDataType = "int", IsNullable = false)]
        public int MinimizedWidth { get; set; } = 0;

        /// <summary>
        /// 最小化后高度
        /// </summary>
        [SugarColumn(ColumnName = "minimized_height", ColumnDescription = "最小化后高度", ColumnDataType = "int", IsNullable = false)]
        public int MinimizedHeight { get; set; } = 0;

        #endregion 交互控制

        #region 显示控制

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        [SugarColumn(ColumnName = "show_delay", ColumnDescription = "显示延迟（秒）", ColumnDataType = "int", IsNullable = false)]
        public int ShowDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒）
        /// </summary>
        [SugarColumn(ColumnName = "show_duration", ColumnDescription = "显示时长（秒）", ColumnDataType = "int", IsNullable = false)]
        public int ShowDuration { get; set; } = 0;

        /// <summary>
        /// 自动隐藏时间（秒）
        /// </summary>
        [SugarColumn(ColumnName = "auto_hide_time", ColumnDescription = "自动隐藏时间（秒）", ColumnDataType = "int", IsNullable = false)]
        public int AutoHideTime { get; set; } = 0;

        /// <summary>
        /// 显示频率 (0: 每次, 1: 每天一次, 2: 每周一次, 3: 每月一次)
        /// </summary>
        [SugarColumn(ColumnName = "show_frequency", ColumnDescription = "显示频率", ColumnDataType = "int", IsNullable = false)]
        public int ShowFrequency { get; set; } = 0;

        #endregion 显示控制

        #region 统计信息

        /// <summary>
        /// 点击次数
        /// </summary>
        [SugarColumn(ColumnName = "click_count", ColumnDescription = "点击次数", ColumnDataType = "int", IsNullable = false)]
        public int ClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        [SugarColumn(ColumnName = "show_count", ColumnDescription = "展示次数", ColumnDataType = "int", IsNullable = false)]
        public int ShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        [SugarColumn(ColumnName = "close_count", ColumnDescription = "关闭次数", ColumnDataType = "int", IsNullable = false)]
        public int CloseCount { get; set; } = 0;

        #endregion 统计信息

        #region 编辑信息

        /// <summary>
        /// 广告编辑人
        /// </summary>
        [SugarColumn(ColumnName = "editor_by", ColumnDescription = "广告编辑人", ColumnDataType = "nvarchar", Length = 100, IsNullable = true)]
        public string? EditorBy { get; set; }

        /// <summary>
        /// 广告编辑时间
        /// </summary>
        [SugarColumn(ColumnName = "edit_time", ColumnDescription = "广告编辑时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? EditTime { get; set; }

        #endregion 编辑信息

        #region 导航属性

        /// <summary>
        /// 关联的计费实体列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TaktAdvertBilling>? BillingList { get; set; }

        #endregion 导航属性

        #region 扩展属性

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        [SugarColumn(ColumnName = "ext_properties", ColumnDescription = "扩展属性JSON", ColumnDataType = "nvarchar", Length = 2000, IsNullable = true)]
        public string? ExtProperties { get; set; }

        #endregion 扩展属性
    }
}
//===================================================================
// 项目名: Takt.Application
// 文件名: TaktAdvertDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 广告数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Advertisement
{
    /// <summary>
    /// 广告基础DTO（与TaktAdvert实体字段严格对应）
    /// </summary>
    public class TaktAdvertDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertImageUrl = string.Empty;
            AdvertPosition = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        public string? AdvertDescription { get; set; }

        /// <summary>
        /// 广告图片URL
        /// </summary>
        public string AdvertImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 广告链接URL
        /// </summary>
        public string? AdvertLinkUrl { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public int AdvertType { get; set; }

        /// <summary>
        /// 广告位置
        /// </summary>
        public string AdvertPosition { get; set; } = string.Empty;

        /// <summary>
        /// 广告状态 (0: 草稿, 1: 已发布, 2: 已下线)
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 广告审核状态 (0: 待审核, 1: 审核通过, 2: 审核拒绝)
        /// </summary>
        public int AuditStatus { get; set; } = 0;

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? AuditComments { get; set; }

        /// <summary>
        /// 广告审核人
        /// </summary>
        public string? AuditedBy { get; set; }

        /// <summary>
        /// 广告审核时间
        /// </summary>
        public DateTime? AuditedTime { get; set; }

        /// <summary>
        /// 广告发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }

        /// <summary>
        /// 广告下线时间
        /// </summary>
        public DateTime? OfflineTime { get; set; }

        /// <summary>
        /// 广告开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 广告结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否置顶 (0: 否, 1: 是)
        /// </summary>
        public int IsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐 (0: 否, 1: 是)
        /// </summary>
        public int IsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门 (0: 否, 1: 是)
        /// </summary>
        public int IsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告宽度
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// 广告高度
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// 广告透明度 (0-100)
        /// </summary>
        public int Opacity { get; set; } = 0;

        /// <summary>
        /// 是否可关闭 (0: 否, 1: 是)
        /// </summary>
        public int Closable { get; set; } = 0;

        /// <summary>
        /// 是否可拖拽 (0: 否, 1: 是)
        /// </summary>
        public int Draggable { get; set; } = 0;

        /// <summary>
        /// 是否可最小化 (0: 否, 1: 是)
        /// </summary>
        public int Minimizable { get; set; } = 0;

        /// <summary>
        /// 最小化后宽度
        /// </summary>
        public int MinimizedWidth { get; set; } = 0;

        /// <summary>
        /// 最小化后高度
        /// </summary>
        public int MinimizedHeight { get; set; } = 0;

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        public int ShowDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒）
        /// </summary>
        public int ShowDuration { get; set; } = 0;

        /// <summary>
        /// 自动隐藏时间（秒）
        /// </summary>
        public int AutoHideTime { get; set; } = 0;

        /// <summary>
        /// 显示频率 (0: 每次, 1: 每天一次, 2: 每周一次, 3: 每月一次)
        /// </summary>
        public int ShowFrequency { get; set; } = 0;

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; } = 0;

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ShowCount { get; set; } = 0;

        /// <summary>
        /// 关闭次数
        /// </summary>
        public int CloseCount { get; set; } = 0;

        /// <summary>
        /// 广告编辑人
        /// </summary>
        public string? EditorBy { get; set; }

        /// <summary>
        /// 广告编辑时间
        /// </summary>
        public DateTime? EditTime { get; set; }


        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }

        /// <summary>
        /// 自定义字段1
        /// </summary>
        public string? Udf01 { get; set; }

        /// <summary>
        /// 自定义字段2
        /// </summary>
        public string? Udf02 { get; set; }

        /// <summary>
        /// 自定义字段3
        /// </summary>
        public string? Udf03 { get; set; }

        /// <summary>
        /// 自定义字段4
        /// </summary>
        public string? Udf04 { get; set; }

        /// <summary>
        /// 自定义字段5
        /// </summary>
        public string? Udf05 { get; set; }

        /// <summary>
        /// 自定义字段6
        /// </summary>
        public string? Udf06 { get; set; }

        /// <summary>
        /// 自定义数值1
        /// </summary>
        public decimal? Udf51 { get; set; } = 0;

        /// <summary>
        /// 自定义数值2
        /// </summary>
        public decimal? Udf52 { get; set; } = 0;

        /// <summary>
        /// 自定义数值3
        /// </summary>
        public decimal? Udf53 { get; set; } = 0;

        /// <summary>
        /// 自定义数值4
        /// </summary>
        public int? Udf54 { get; set; } = 0;

        /// <summary>
        /// 自定义数值5
        /// </summary>
        public int? Udf55 { get; set; } = 0;

        /// <summary>
        /// 自定义数值6
        /// </summary>
        public int? Udf56 { get; set; } = 0;

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
    /// 广告查询DTO
    /// </summary>
    public class TaktAdvertQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertQueryDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertPosition = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string? CompanyCode { get; set; }

        /// <summary>
        /// 广告标题
        /// </summary>
        public string? AdvertTitle { get; set; }

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public int? AdvertType { get; set; }

        /// <summary>
        /// 广告位置
        /// </summary>
        public string? AdvertPosition { get; set; }

        /// <summary>
        /// 广告状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 广告审核状态
        /// </summary>
        public int? AuditStatus { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int? IsTop { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? IsRecommend { get; set; }

        /// <summary>
        /// 是否热门
        /// </summary>
        public int? IsHot { get; set; }


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
    /// 广告创建DTO
    /// </summary>
    public class TaktAdvertCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertCreateDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertImageUrl = string.Empty;
            AdvertPosition = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        #region 基础信息

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        public string? AdvertDescription { get; set; }

        /// <summary>
        /// 广告图片URL
        /// </summary>
        public string AdvertImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 广告链接URL
        /// </summary>
        public string? AdvertLinkUrl { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public int AdvertType { get; set; }

        /// <summary>
        /// 广告位置
        /// </summary>
        public string AdvertPosition { get; set; } = string.Empty;

        #endregion

        #region 时间管理

        /// <summary>
        /// 广告开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 广告结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        #endregion

        #region 显示控制

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int IsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        public int IsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        #endregion

        #region 尺寸和样式

        /// <summary>
        /// 广告宽度
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// 广告高度
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// 广告透明度
        /// </summary>
        public int Opacity { get; set; } = 0;

        #endregion

        #region 交互控制

        /// <summary>
        /// 是否可关闭
        /// </summary>
        public int Closable { get; set; } = 0;

        /// <summary>
        /// 是否可拖拽
        /// </summary>
        public int Draggable { get; set; } = 0;

        /// <summary>
        /// 是否可最小化
        /// </summary>
        public int Minimizable { get; set; } = 0;

        /// <summary>
        /// 最小化后宽度
        /// </summary>
        public int MinimizedWidth { get; set; } = 0;

        /// <summary>
        /// 最小化后高度
        /// </summary>
        public int MinimizedHeight { get; set; } = 0;

        #endregion

        #region 显示控制

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        public int ShowDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒）
        /// </summary>
        public int ShowDuration { get; set; } = 0;

        /// <summary>
        /// 自动隐藏时间（秒）
        /// </summary>
        public int AutoHideTime { get; set; } = 0;

        /// <summary>
        /// 显示频率
        /// </summary>
        public int ShowFrequency { get; set; } = 0;

        #endregion


        #region 扩展属性

        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }

        #endregion

        #region 基础字段

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }

        #endregion
    }

    /// <summary>
    /// 广告更新DTO
    /// </summary>
    public class TaktAdvertUpdateDto : TaktAdvertCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }
    }

/// <summary>
    /// 广告模板DTO
    /// </summary>
    public class TaktAdvertTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertTemplateDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertImageUrl = string.Empty;
            AdvertPosition = string.Empty;
            Description = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        public string? AdvertDescription { get; set; }

        /// <summary>
        /// 广告图片URL
        /// </summary>
        public string AdvertImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 广告链接URL
        /// </summary>
        public string? AdvertLinkUrl { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public int AdvertType { get; set; }

        /// <summary>
        /// 广告位置
        /// </summary>
        public string AdvertPosition { get; set; } = string.Empty;

        /// <summary>
        /// 广告开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 广告结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public int IsTop { get; set; } = 0;

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int IsRecommend { get; set; } = 0;

        /// <summary>
        /// 是否热门
        /// </summary>
        public int IsHot { get; set; } = 0;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 广告宽度
        /// </summary>
        public int Width { get; set; } = 0;

        /// <summary>
        /// 广告高度
        /// </summary>
        public int Height { get; set; } = 0;

        /// <summary>
        /// 广告透明度
        /// </summary>
        public int Opacity { get; set; } = 0;

        /// <summary>
        /// 是否可关闭
        /// </summary>
        public int Closable { get; set; } = 0;

        /// <summary>
        /// 是否可拖拽
        /// </summary>
        public int Draggable { get; set; } = 0;

        /// <summary>
        /// 是否可最小化
        /// </summary>
        public int Minimizable { get; set; } = 0;

        /// <summary>
        /// 显示延迟（秒）
        /// </summary>
        public int ShowDelay { get; set; } = 0;

        /// <summary>
        /// 显示时长（秒）
        /// </summary>
        public int ShowDuration { get; set; } = 0;

        /// <summary>
        /// 自动隐藏时间（秒）
        /// </summary>
        public int AutoHideTime { get; set; } = 0;

        /// <summary>
        /// 显示频率
        /// </summary>
        public int ShowFrequency { get; set; } = 0;


        /// <summary>
        /// 扩展属性JSON
        /// </summary>
        public string? ExtProperties { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; } = string.Empty;

        /// <summary>
        /// 自定义字段1
        /// </summary>
        public string? Udf01 { get; set; }

        /// <summary>
        /// 自定义字段2
        /// </summary>
        public string? Udf02 { get; set; }

        /// <summary>
        /// 自定义字段3
        /// </summary>
        public string? Udf03 { get; set; }

        /// <summary>
        /// 自定义字段4
        /// </summary>
        public string? Udf04 { get; set; }

        /// <summary>
        /// 自定义字段5
        /// </summary>
        public string? Udf05 { get; set; }

        /// <summary>
        /// 自定义字段6
        /// </summary>
        public string? Udf06 { get; set; }

        /// <summary>
        /// 自定义数值1
        /// </summary>
        public decimal? Udf51 { get; set; } = 0;

        /// <summary>
        /// 自定义数值2
        /// </summary>
        public decimal? Udf52 { get; set; } = 0;

        /// <summary>
        /// 自定义数值3
        /// </summary>
        public decimal? Udf53 { get; set; } = 0;

        /// <summary>
        /// 自定义数值4
        /// </summary>
        public int? Udf54 { get; set; } = 0;

        /// <summary>
        /// 自定义数值5
        /// </summary>
        public int? Udf55 { get; set; } = 0;

        /// <summary>
        /// 自定义数值6
        /// </summary>
        public int? Udf56 { get; set; } = 0;

        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
   
   /// <summary>
    /// 广告导入DTO
    /// </summary>
    public class TaktAdvertImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertImportDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertImageUrl = string.Empty;
            AdvertPosition = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string? AdvertSubtitle { get; set; }

        /// <summary>
        /// 广告描述
        /// </summary>
        public string? AdvertDescription { get; set; }

        /// <summary>
        /// 广告图片URL
        /// </summary>
        public string AdvertImageUrl { get; set; } = string.Empty;

        /// <summary>
        /// 广告链接URL
        /// </summary>
        public string? AdvertLinkUrl { get; set; }

        /// <summary>
        /// 广告类型
        /// </summary>
        public string AdvertType { get; set; } = string.Empty;

        /// <summary>
        /// 广告位置
        /// </summary>
        public string AdvertPosition { get; set; } = string.Empty;

        /// <summary>
        /// 广告开始时间
        /// </summary>
        public string? StartTime { get; set; }

        /// <summary>
        /// 广告结束时间
        /// </summary>
        public string? EndTime { get; set; }

        /// <summary>
        /// 是否置顶
        /// </summary>
        public string IsTop { get; set; } = string.Empty;

        /// <summary>
        /// 是否推荐
        /// </summary>
        public string IsRecommend { get; set; } = string.Empty;

        /// <summary>
        /// 是否热门
        /// </summary>
        public string IsHot { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public string OrderNum { get; set; } = string.Empty;

        /// <summary>
        /// 广告宽度
        /// </summary>
        public string Width { get; set; } = string.Empty;

        /// <summary>
        /// 广告高度
        /// </summary>
        public string Height { get; set; } = string.Empty;


        /// <summary>
        /// 备注
        /// </summary>
        public string? Remark { get; set; }
    }
    /// <summary>
    /// 广告导出DTO
    /// </summary>
    public class TaktAdvertExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertExportDto()
        {
            CompanyCode = string.Empty;
            AdvertTitle = string.Empty;
            AdvertSubtitle = string.Empty;
            AdvertPosition = string.Empty;
            Status = string.Empty;
            AuditStatus = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 广告标题
        /// </summary>
        public string AdvertTitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告副标题
        /// </summary>
        public string AdvertSubtitle { get; set; } = string.Empty;

        /// <summary>
        /// 广告类型
        /// </summary>
        public string AdvertType { get; set; } = string.Empty;

        /// <summary>
        /// 广告位置
        /// </summary>
        public string AdvertPosition { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 审核状态
        /// </summary>
        public string AuditStatus { get; set; } = string.Empty;

        /// <summary>
        /// 是否置顶
        /// </summary>
        public string IsTop { get; set; } = string.Empty;

        /// <summary>
        /// 是否推荐
        /// </summary>
        public string IsRecommend { get; set; } = string.Empty;

        /// <summary>
        /// 是否热门
        /// </summary>
        public string IsHot { get; set; } = string.Empty;

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    

    

    /// <summary>
    /// 广告状态DTO
    /// </summary>
    public class TaktAdvertStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 广告状态
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 广告审核DTO
    /// </summary>
    public class TaktAdvertAuditDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertAuditDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int AuditStatus { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        public string? AuditComments { get; set; }
    }

    /// <summary>
    /// 广告发布DTO
    /// </summary>
    public class TaktAdvertPublishDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertPublishDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime? PublishTime { get; set; }
    }

    /// <summary>
    /// 广告下线DTO
    /// </summary>
    public class TaktAdvertOfflineDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertOfflineDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 下线时间
        /// </summary>
        public DateTime? OfflineTime { get; set; }
    }

    /// <summary>
    /// 广告统计DTO
    /// </summary>
    public class TaktAdvertStatsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertStatsDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        public long AdvertId { get; set; }

        /// <summary>
        /// 点击次数
        /// </summary>
        public int ClickCount { get; set; }

        /// <summary>
        /// 展示次数
        /// </summary>
        public int ShowCount { get; set; }

        /// <summary>
        /// 关闭次数
        /// </summary>
        public int CloseCount { get; set; }
    }

    /// <summary>
    /// 广告统计DTO
    /// </summary>
    public class TaktAdvertStatisticsDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktAdvertStatisticsDto()
        {
        }

        /// <summary>
        /// 总广告数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 已发布数量
        /// </summary>
        public int PublishedCount { get; set; }

        /// <summary>
        /// 草稿数量
        /// </summary>
        public int DraftCount { get; set; }

        /// <summary>
        /// 已下线数量
        /// </summary>
        public int OfflineCount { get; set; }

        /// <summary>
        /// 待审核数量
        /// </summary>
        public int PendingAuditCount { get; set; }

        /// <summary>
        /// 审核通过数量
        /// </summary>
        public int AuditPassedCount { get; set; }

        /// <summary>
        /// 审核拒绝数量
        /// </summary>
        public int AuditRejectedCount { get; set; }

        /// <summary>
        /// 置顶数量
        /// </summary>
        public int TopCount { get; set; }

        /// <summary>
        /// 推荐数量
        /// </summary>
        public int RecommendCount { get; set; }

        /// <summary>
        /// 热门数量
        /// </summary>
        public int HotCount { get; set; }

        /// <summary>
        /// 总点击次数
        /// </summary>
        public int TotalClickCount { get; set; }

        /// <summary>
        /// 总展示次数
        /// </summary>
        public int TotalShowCount { get; set; }

        /// <summary>
        /// 总关闭次数
        /// </summary>
        public int TotalCloseCount { get; set; }

    }
}

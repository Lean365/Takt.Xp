//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentAccessDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : ISO文件查阅数据传输对象
//===================================================================

using Takt.Shared.Models;

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文件查阅基础DTO
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录ISO文件的查阅情况，包括查阅人、查阅时间、查阅方式等
    /// </remarks>
    public class TaktIsoDocumentAccessDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentAccessDto()
        {
            DocumentVersion = string.Empty;
            AccessedBy = string.Empty;
        }

        /// <summary>
        /// 查阅记录ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long AccessId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 查阅人
        /// </summary>
        public string AccessedBy { get; set; } = string.Empty;

        /// <summary>
        /// 查阅类型。0=在线查阅，1=下载，2=打印，3=复制，4=其他
        /// </summary>
        public int AccessType { get; set; }

        /// <summary>
        /// 查阅日期
        /// </summary>
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 查阅时长（秒）
        /// </summary>
        public int AccessDuration { get; set; }

        /// <summary>
        /// 查阅IP地址
        /// </summary>
        public string? AccessIp { get; set; }

        /// <summary>
        /// 查阅设备
        /// </summary>
        public string? AccessDevice { get; set; }

        /// <summary>
        /// 查阅浏览器
        /// </summary>
        public string? AccessBrowser { get; set; }

        /// <summary>
        /// 查阅操作系统
        /// </summary>
        public string? AccessOs { get; set; }

        /// <summary>
        /// 查阅位置
        /// </summary>
        public string? AccessLocation { get; set; }

        /// <summary>
        /// 查阅目的
        /// </summary>
        public string? AccessPurpose { get; set; }

        /// <summary>
        /// 查阅结果
        /// </summary>
        public string? AccessResult { get; set; }

        /// <summary>
        /// 是否成功查阅
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool IsRated { get; set; }

        /// <summary>
        /// 评价等级。1=很差，2=较差，3=一般，4=较好，5=很好
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? RatingComment { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime? RatingDate { get; set; }

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

        /// <summary>
        /// 关联的ISO文档
        /// </summary>
        public TaktIsoDocumentDto? Document { get; set; }
    }

    /// <summary>
    /// ISO文件查阅查询DTO
    /// </summary>
    public class TaktIsoDocumentAccessQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentAccessQueryDto()
        {
            AccessedBy = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long? DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 查阅人
        /// </summary>
        public string? AccessedBy { get; set; }

        /// <summary>
        /// 查阅类型
        /// </summary>
        public int? AccessType { get; set; }

        /// <summary>
        /// 开始查阅日期
        /// </summary>
        public DateTime? StartAccessDate { get; set; }

        /// <summary>
        /// 结束查阅日期
        /// </summary>
        public DateTime? EndAccessDate { get; set; }

        /// <summary>
        /// 是否成功查阅
        /// </summary>
        public bool? IsSuccessful { get; set; }

        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool? IsRated { get; set; }

        /// <summary>
        /// 评价等级
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 查阅IP地址
        /// </summary>
        public string? AccessIp { get; set; }

        /// <summary>
        /// 查阅设备
        /// </summary>
        public string? AccessDevice { get; set; }

        /// <summary>
        /// 查阅浏览器
        /// </summary>
        public string? AccessBrowser { get; set; }

        /// <summary>
        /// 查阅操作系统
        /// </summary>
        public string? AccessOs { get; set; }

        /// <summary>
        /// 查阅位置
        /// </summary>
        public string? AccessLocation { get; set; }
    }

    /// <summary>
    /// ISO文件查阅创建DTO
    /// </summary>
    public class TaktIsoDocumentAccessCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentAccessCreateDto()
        {
            DocumentVersion = string.Empty;
            AccessedBy = string.Empty;
        }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 查阅人
        /// </summary>
        public string AccessedBy { get; set; } = string.Empty;

        /// <summary>
        /// 查阅类型。0=在线查阅，1=下载，2=打印，3=复制，4=其他
        /// </summary>
        public int AccessType { get; set; }

        /// <summary>
        /// 查阅日期
        /// </summary>
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 查阅时长（秒）
        /// </summary>
        public int AccessDuration { get; set; }

        /// <summary>
        /// 查阅IP地址
        /// </summary>
        public string? AccessIp { get; set; }

        /// <summary>
        /// 查阅设备
        /// </summary>
        public string? AccessDevice { get; set; }

        /// <summary>
        /// 查阅浏览器
        /// </summary>
        public string? AccessBrowser { get; set; }

        /// <summary>
        /// 查阅操作系统
        /// </summary>
        public string? AccessOs { get; set; }

        /// <summary>
        /// 查阅位置
        /// </summary>
        public string? AccessLocation { get; set; }

        /// <summary>
        /// 查阅目的
        /// </summary>
        public string? AccessPurpose { get; set; }

        /// <summary>
        /// 查阅结果
        /// </summary>
        public string? AccessResult { get; set; }

        /// <summary>
        /// 是否成功查阅
        /// </summary>
        public bool IsSuccessful { get; set; } = true;

        /// <summary>
        /// 失败原因
        /// </summary>
        public string? FailureReason { get; set; }
    }

    /// <summary>
    /// ISO文件查阅更新DTO
    /// </summary>
    public class TaktIsoDocumentAccessUpdateDto : TaktIsoDocumentAccessCreateDto
    {
        /// <summary>
        /// 查阅记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long AccessId { get; set; }

        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool IsRated { get; set; }

        /// <summary>
        /// 评价等级。1=很差，2=较差，3=一般，4=较好，5=很好
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? RatingComment { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime? RatingDate { get; set; }
    }

    /// <summary>
    /// ISO文件查阅导出DTO
    /// </summary>
    public class TaktIsoDocumentAccessExportDto
    {
        /// <summary>
        /// 查阅记录ID
        /// </summary>
        public long AccessId { get; set; }

        /// <summary>
        /// ISO文档ID
        /// </summary>
        public long DocumentId { get; set; }

        /// <summary>
        /// 文档版本号
        /// </summary>
        public string DocumentVersion { get; set; } = string.Empty;

        /// <summary>
        /// 查阅人
        /// </summary>
        public string AccessedBy { get; set; } = string.Empty;

        /// <summary>
        /// 查阅类型。0=在线查阅，1=下载，2=打印，3=复制，4=其他
        /// </summary>
        public int AccessType { get; set; }

        /// <summary>
        /// 查阅日期
        /// </summary>
        public DateTime AccessDate { get; set; }

        /// <summary>
        /// 查阅时长（秒）
        /// </summary>
        public int AccessDuration { get; set; }

        /// <summary>
        /// 查阅IP地址
        /// </summary>
        public string? AccessIp { get; set; }

        /// <summary>
        /// 查阅设备
        /// </summary>
        public string? AccessDevice { get; set; }

        /// <summary>
        /// 查阅浏览器
        /// </summary>
        public string? AccessBrowser { get; set; }

        /// <summary>
        /// 查阅操作系统
        /// </summary>
        public string? AccessOs { get; set; }

        /// <summary>
        /// 查阅位置
        /// </summary>
        public string? AccessLocation { get; set; }

        /// <summary>
        /// 查阅目的
        /// </summary>
        public string? AccessPurpose { get; set; }

        /// <summary>
        /// 查阅结果
        /// </summary>
        public string? AccessResult { get; set; }

        /// <summary>
        /// 是否成功查阅
        /// </summary>
        public bool IsSuccessful { get; set; }

        /// <summary>
        /// 失败原因
        /// </summary>
        public string? FailureReason { get; set; }

        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool IsRated { get; set; }

        /// <summary>
        /// 评价等级。1=很差，2=较差，3=一般，4=较好，5=很好
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? RatingComment { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime? RatingDate { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// ISO文件查阅评价DTO
    /// </summary>
    public class TaktIsoDocumentAccessRatingDto
    {
        /// <summary>
        /// 查阅记录ID
        /// </summary>
        [AdaptMember("Id")]
        public long AccessId { get; set; }

        /// <summary>
        /// 是否已评价
        /// </summary>
        public bool IsRated { get; set; }

        /// <summary>
        /// 评价等级。1=很差，2=较差，3=一般，4=较好，5=很好
        /// </summary>
        public int? Rating { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        public string? RatingComment { get; set; }

        /// <summary>
        /// 评价日期
        /// </summary>
        public DateTime? RatingDate { get; set; }
    }
}





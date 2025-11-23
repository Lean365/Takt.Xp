//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktLawDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 法律法规数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Document
{
    /// <summary>
    /// 法律法规基础DTO
    /// </summary>
    public class TaktLawDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLawDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
            LawVersion = "1.0";
        }

        /// <summary>
        /// 法律法规ID
        /// </summary>
        [AdaptMember("Id")]
        public long LawId { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        public string LawCode { get; set; }

        /// <summary>法规名称</summary>
        public string LawName { get; set; }

        /// <summary>法规标题</summary>
        public string LawTitle { get; set; }

        /// <summary>法规类型(1=法律 2=行政法规 3=部门规章 4=地方性法规 5=地方政府规章 6=标准规范 7=其他)</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法规级别(1=国家级 2=省级 3=市级 4=县级)</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string LawVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小(KB)</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态。0=草稿，1=已保存，2=已归档。</summary>
        public int Status { get; set; } = 0;

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }

        /// <summary>更新者</summary>
        public string? UpdateBy { get; set; }

        /// <summary>更新时间</summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>是否删除（0未删除 1已删除）</summary>
        public int IsDeleted { get; set; }

        /// <summary>删除者</summary>
        public string? DeleteBy { get; set; }

        /// <summary>删除时间</summary>
        public DateTime? DeleteTime { get; set; }

        /// <summary>父级法规</summary>
        public TaktLawDto? Parent { get; set; }

        /// <summary>子级法规列表</summary>
        public List<TaktLawDto>? Children { get; set; }
    }

    /// <summary>
    /// 法律法规查询DTO
    /// </summary>
    public class TaktLawQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLawQueryDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int? OrderNum { get; set; }

        /// <summary>法规编号</summary>
        public string? LawCode { get; set; }

        /// <summary>法规名称</summary>
        public string? LawName { get; set; }

        /// <summary>法规标题</summary>
        public string? LawTitle { get; set; }

        /// <summary>法规类型</summary>
        public int? LawType { get; set; }

        /// <summary>法规级别</summary>
        public int? LawLevel { get; set; }

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string? LawVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度</summary>
        public int? ImportanceLevel { get; set; }

        /// <summary>是否强制</summary>
        public int? IsMandatory { get; set; }

        /// <summary>是否公开</summary>
        public int? IsPublic { get; set; }

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int? ReadCount { get; set; }

        /// <summary>下载次数</summary>
        public int? DownloadCount { get; set; }

        /// <summary>状态</summary>
        public int? Status { get; set; }

        /// <summary>开始日期</summary>
        public DateTime? StartDate { get; set; }

        /// <summary>结束日期</summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// 法律法规创建DTO
    /// </summary>
    public class TaktLawCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktLawCreateDto()
        {
            LawCode = string.Empty;
            LawName = string.Empty;
            LawTitle = string.Empty;
            LawVersion = "1.0";
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        public string LawCode { get; set; }

        /// <summary>法规名称</summary>
        public string LawName { get; set; }

        /// <summary>法规标题</summary>
        public string LawTitle { get; set; }

        /// <summary>法规类型(1=法律 2=行政法规 3=部门规章 4=地方性法规 5=地方政府规章 6=标准规范 7=其他)</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法规级别(1=国家级 2=省级 3=市级 4=县级)</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string LawVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小(KB)</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }
    }

    /// <summary>
    /// 法律法规更新DTO
    /// </summary>
    public class TaktLawUpdateDto : TaktLawCreateDto
    {
        /// <summary>法律法规ID</summary>
        [AdaptMember("Id")]
        public long LawId { get; set; }
    }

    /// <summary>
    /// 法律法规导入DTO
    /// </summary>
    public class TaktLawImportDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法规类型</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法规级别</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string LawVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态</summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 法律法规导出DTO
    /// </summary>
    public class TaktLawExportDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法规类型</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法规级别</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string LawVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态</summary>
        public int Status { get; set; } = 0;

        /// <summary>创建者</summary>
        public string? CreateBy { get; set; }

        /// <summary>创建时间</summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 法律法规模板DTO
    /// </summary>
    public class TaktLawTemplateDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>法规编号</summary>
        public string LawCode { get; set; } = string.Empty;

        /// <summary>法规名称</summary>
        public string LawName { get; set; } = string.Empty;

        /// <summary>法规标题</summary>
        public string LawTitle { get; set; } = string.Empty;

        /// <summary>法规类型</summary>
        public int LawType { get; set; } = 1;

        /// <summary>法规级别</summary>
        public int LawLevel { get; set; } = 1;

        /// <summary>法规描述</summary>
        public string? LawDescription { get; set; }

        /// <summary>法规PDF文件路径</summary>
        public string? LawPdfPath { get; set; }

        /// <summary>法规版本</summary>
        public string LawVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>颁布机关</summary>
        public string? IssuingAuthority { get; set; }

        /// <summary>颁布日期</summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>来源机关</summary>
        public string? SourceAuthority { get; set; }

        /// <summary>下载日期</summary>
        public DateTime? DownloadDate { get; set; }

        /// <summary>下载人</summary>
        public string? Downloader { get; set; }

        /// <summary>原始文件路径</summary>
        public string? OriginalFilePath { get; set; }

        /// <summary>原始文件大小</summary>
        public int? OriginalFileSize { get; set; }

        /// <summary>原始文件格式</summary>
        public string? OriginalFileFormat { get; set; }

        /// <summary>相关法规</summary>
        public string? RelatedLaws { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态</summary>
        public int Status { get; set; } = 0;
    }

    /// <summary>
    /// 法律法规状态DTO
    /// </summary>
    public class TaktLawStatusDto
    {
        /// <summary>法律法规ID</summary>
        [AdaptMember("Id")]
        public long LawId { get; set; }

        /// <summary>状态。0=草稿，1=已保存，2=已归档。</summary>
        public int Status { get; set; }
    }
}



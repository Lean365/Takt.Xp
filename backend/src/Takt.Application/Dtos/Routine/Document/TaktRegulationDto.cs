//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktRegulationDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : 规章制度数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Document
{
    /// <summary>
    /// 规章制度基础DTO
    /// </summary>
    public class TaktRegulationDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRegulationDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
            RegulationVersion = "1.0";
        }

        /// <summary>
        /// 规章制度ID
        /// </summary>
        [AdaptMember("Id")]
        public long RegulationId { get; set; }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; }

        /// <summary>规章制度类型(1=管理制度 2=操作规程 3=工作标准 4=岗位职责 5=考核制度 6=奖惩制度 7=其他)</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }

        /// <summary>阅读次数</summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        public int DownloadCount { get; set; } = 0;

        /// <summary>状态。规章制度的状态（0=草稿，1=已发布，2=已作废，3=已归档）。</summary>
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

        /// <summary>父级规章制度</summary>
        public TaktRegulationDto? Parent { get; set; }

        /// <summary>子级规章制度列表</summary>
        public List<TaktRegulationDto>? Children { get; set; }
    }

    /// <summary>
    /// 规章制度查询DTO
    /// </summary>
    public class TaktRegulationQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRegulationQueryDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int? OrderNum { get; set; }

        /// <summary>规章制度编号</summary>
        public string? RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string? RegulationTitle { get; set; }

        /// <summary>规章制度类型</summary>
        public int? RegulationType { get; set; }

        /// <summary>规章制度级别</summary>
        public int? RegulationLevel { get; set; }

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string? RegulationVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式</summary>
        public int? PublishMethod { get; set; }

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度</summary>
        public int? ImportanceLevel { get; set; }

        /// <summary>是否强制</summary>
        public int? IsMandatory { get; set; }

        /// <summary>是否公开</summary>
        public int? IsPublic { get; set; }

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

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
    /// 规章制度创建DTO
    /// </summary>
    public class TaktRegulationCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktRegulationCreateDto()
        {
            RegulationCode = string.Empty;
            RegulationTitle = string.Empty;
            RegulationVersion = "1.0";
        }

        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; }

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; }

        /// <summary>规章制度类型(1=管理制度 2=操作规程 3=工作标准 4=岗位职责 5=考核制度 6=奖惩制度 7=其他)</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别(1=公司级 2=部门级 3=科室级 4=班组级)</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; }

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式(1=内部发布 2=全员发布 3=指定部门发布)</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度(1=一般 2=重要 3=非常重要)</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制(0=否 1=是)</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开(0=否 1=是)</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

        /// <summary>相关文件</summary>
        public string? RelatedFiles { get; set; }

        /// <summary>关键词</summary>
        public string? Keywords { get; set; }
    }

    /// <summary>
    /// 规章制度更新DTO
    /// </summary>
    public class TaktRegulationUpdateDto : TaktRegulationCreateDto
    {
        /// <summary>规章制度ID</summary>
        [AdaptMember("Id")]
        public long RegulationId { get; set; }
    }

    /// <summary>
    /// 规章制度导入DTO
    /// </summary>
    public class TaktRegulationImportDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

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
    /// 规章制度导出DTO
    /// </summary>
    public class TaktRegulationExportDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

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
    /// 规章制度模板DTO
    /// </summary>
    public class TaktRegulationTemplateDto
    {
        /// <summary>父级ID</summary>
        public long? ParentId { get; set; }

        /// <summary>排序号</summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>规章制度编号</summary>
        public string RegulationCode { get; set; } = string.Empty;

        /// <summary>规章制度标题</summary>
        public string RegulationTitle { get; set; } = string.Empty;

        /// <summary>规章制度类型</summary>
        public int RegulationType { get; set; } = 1;

        /// <summary>规章制度级别</summary>
        public int RegulationLevel { get; set; } = 1;

        /// <summary>规章制度描述</summary>
        public string? RegulationDescription { get; set; }

        /// <summary>规章制度完整内容</summary>
        public string? RegulationContent { get; set; }

        /// <summary>规章制度PDF文件路径</summary>
        public string? RegulationPdfPath { get; set; }

        /// <summary>规章制度版本</summary>
        public string RegulationVersion { get; set; } = "1.0";

        /// <summary>修订版本</summary>
        public string? RevisionVersion { get; set; }

        /// <summary>生效日期</summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>失效日期</summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>发布日期</summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>发布部门</summary>
        public string? PublishDepartment { get; set; }

        /// <summary>发布方式</summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>发布范围</summary>
        public string? PublishScope { get; set; }

        /// <summary>制定部门</summary>
        public string? DraftDepartment { get; set; }

        /// <summary>制定日期</summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>管理层审核人</summary>
        public string? ManagementReviewer { get; set; }

        /// <summary>管理层审核日期</summary>
        public DateTime? ManagementReviewDate { get; set; }

        /// <summary>管理层审核意见</summary>
        public string? ManagementReviewComment { get; set; }

        /// <summary>职工代表大会表决日期</summary>
        public DateTime? StaffVoteDate { get; set; }

        /// <summary>职工代表大会表决结果</summary>
        public string? StaffVoteResult { get; set; }

        /// <summary>法务合规审核人</summary>
        public string? LegalReviewer { get; set; }

        /// <summary>法务合规审核日期</summary>
        public DateTime? LegalReviewDate { get; set; }

        /// <summary>法务合规审核意见</summary>
        public string? LegalReviewComment { get; set; }

        /// <summary>相关备案部门</summary>
        public string? FilingDepartment { get; set; }

        /// <summary>备案日期</summary>
        public DateTime? FilingDate { get; set; }

        /// <summary>备案编号</summary>
        public string? FilingNumber { get; set; }

        /// <summary>重要程度</summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>是否强制</summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>是否公开</summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>相关规章制度</summary>
        public string? RelatedRegulations { get; set; }

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
    /// 规章制度状态DTO
    /// </summary>
    public class TaktRegulationStatusDto
    {
        /// <summary>规章制度ID</summary>
        [AdaptMember("Id")]
        public long RegulationId { get; set; }

        /// <summary>状态。规章制度的状态（0=草稿，1=已发布，2=已作废，3=已归档）。</summary>
        public int Status { get; set; }
    }
}



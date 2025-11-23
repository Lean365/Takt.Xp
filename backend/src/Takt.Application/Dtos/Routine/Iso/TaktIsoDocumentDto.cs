//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktIsoDocumentDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述   : ISO文档数据传输对象
//===================================================================

namespace Takt.Application.Dtos.Routine.Iso
{
    /// <summary>
    /// ISO文档基础DTO
    /// </summary>
    public class TaktIsoDocumentDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentDto()
        {
            DocumentCode = string.Empty;
            DocumentName = string.Empty;
        }

        /// <summary>
        /// 文档ID
        /// </summary>
        [AdaptMember("Id")]
        public long IsoDocumentId { get; set; }

        /// <summary>
        /// 父级ID。用于实现树形结构，0表示根节点。
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准。文档对应的ISO标准编号。
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型。1=质量手册，2=程序文件，3=作业指导书，4=表格记录，5=外来文件，6=其他。
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号。ISO文档的唯一编号。
        /// </summary>
        public string DocumentCode { get; set; }

        /// <summary>
        /// 文档名称。ISO文档的名称。
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// 文档级别。1=一级，2=二级，3=三级，4=四级，5=五级。
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述。ISO文档的详细描述。
        /// </summary>
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容。ISO文档的具体内容。
        /// </summary>
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本。ISO文档的版本号。
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期。ISO文档的生效日期。
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期。ISO文档的失效日期。
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号。用于自定义文档在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态。0=草稿，1=待审核，2=已审核，3=已发布，4=已作废，5=已归档。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 重要程度。1=一般，2=重要，3=非常重要。
        /// </summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制。0=否，1=是。
        /// </summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开。0=否，1=是。
        /// </summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门。制定ISO文档的部门。
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人。制定ISO文档的人员。
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期。ISO文档的制定日期。
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人。审核ISO文档的人员。
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期。ISO文档的审核日期。
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见。审核人对ISO文档的意见。
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表。管理代表对ISO文档的批准。
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期。管理代表批准ISO文档的日期。
        /// </summary>
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见。管理代表对ISO文档的批准意见。
        /// </summary>
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期。ISO文档的发布日期。
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人。发布ISO文档的人员。
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式。1=内部发布，2=全员发布，3=指定部门发布。
        /// </summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围。ISO文档的发布范围。
        /// </summary>
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档。与ISO文档相关的其他文档。
        /// </summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件。与ISO文档相关的文件。
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词。ISO文档的关键词，用于搜索。
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 阅读次数。ISO文档被阅读的次数。
        /// </summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>
        /// 下载次数。ISO文档被下载的次数。
        /// </summary>
        public int DownloadCount { get; set; } = 0;

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
        /// 父级文档。该文档的父级文档。
        /// </summary>
        public TaktIsoDocumentDto? Parent { get; set; }

        /// <summary>
        /// 子级文档列表。该文档的子级文档列表。
        /// </summary>
        public List<TaktIsoDocumentDto>? Children { get; set; }
    }

    /// <summary>
    /// ISO文档查询DTO
    /// </summary>
    public class TaktIsoDocumentQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentQueryDto()
        {
            DocumentCode = string.Empty;
            DocumentName = string.Empty;
        }

        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string? DocumentCode { get; set; }

        /// <summary>
        /// 文档名称
        /// </summary>
        public string? DocumentName { get; set; }

        /// <summary>
        /// 文档级别
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 重要程度
        /// </summary>
        public int? ImportanceLevel { get; set; }

        /// <summary>
        /// 是否强制
        /// </summary>
        public int? IsMandatory { get; set; }

        /// <summary>
        /// 是否公开
        /// </summary>
        public int? IsPublic { get; set; }

        /// <summary>
        /// 制定部门
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 管理代表
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式
        /// </summary>
        public int? PublishMethod { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }
    }

    /// <summary>
    /// ISO文档创建DTO
    /// </summary>
    public class TaktIsoDocumentCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktIsoDocumentCreateDto()
        {
            DocumentCode = string.Empty;
            DocumentName = string.Empty;
        }

        /// <summary>
        /// 父级ID。用于实现树形结构，0表示根节点。
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准。文档对应的ISO标准编号。
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型。1=质量手册，2=程序文件，3=作业指导书，4=表格记录，5=外来文件，6=其他。
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号。ISO文档的唯一编号。
        /// </summary>
        public string DocumentCode { get; set; }

        /// <summary>
        /// 文档名称。ISO文档的名称。
        /// </summary>
        public string DocumentName { get; set; }

        /// <summary>
        /// 文档级别。1=一级，2=二级，3=三级，4=四级，5=五级。
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述。ISO文档的详细描述。
        /// </summary>
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容。ISO文档的具体内容。
        /// </summary>
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本。ISO文档的版本号。
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期。ISO文档的生效日期。
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期。ISO文档的失效日期。
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号。用于自定义文档在列表中的显示顺序，值越小越靠前。
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态。0=草稿，1=待审核，2=已审核，3=已发布，4=已作废，5=已归档。
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 重要程度。1=一般，2=重要，3=非常重要。
        /// </summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制。0=否，1=是。
        /// </summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开。0=否，1=是。
        /// </summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门。制定ISO文档的部门。
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人。制定ISO文档的人员。
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期。ISO文档的制定日期。
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人。审核ISO文档的人员。
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期。ISO文档的审核日期。
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见。审核人对ISO文档的意见。
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表。管理代表对ISO文档的批准。
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期。管理代表批准ISO文档的日期。
        /// </summary>
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见。管理代表对ISO文档的批准意见。
        /// </summary>
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期。ISO文档的发布日期。
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人。发布ISO文档的人员。
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式。1=内部发布，2=全员发布，3=指定部门发布。
        /// </summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围。ISO文档的发布范围。
        /// </summary>
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档。与ISO文档相关的其他文档。
        /// </summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件。与ISO文档相关的文件。
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词。ISO文档的关键词，用于搜索。
        /// </summary>
        public string? Keywords { get; set; }
    }

    /// <summary>
    /// ISO文档更新DTO
    /// </summary>
    public class TaktIsoDocumentUpdateDto : TaktIsoDocumentCreateDto
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        [AdaptMember("Id")]
        public long IsoDocumentId { get; set; }
    }

    /// <summary>
    /// ISO文档导入DTO
    /// </summary>
    public class TaktIsoDocumentImportDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>
        /// 文档级别
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述
        /// </summary>
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容
        /// </summary>
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 重要程度
        /// </summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制
        /// </summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期
        /// </summary>
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见
        /// </summary>
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式
        /// </summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围
        /// </summary>
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档
        /// </summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadCount { get; set; } = 0;
    }

    /// <summary>
    /// ISO文档导出DTO
    /// </summary>
    public class TaktIsoDocumentExportDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>
        /// 文档级别
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述
        /// </summary>
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容
        /// </summary>
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 重要程度
        /// </summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制
        /// </summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期
        /// </summary>
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见
        /// </summary>
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式
        /// </summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围
        /// </summary>
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档
        /// </summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadCount { get; set; } = 0;

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
    /// ISO文档模板DTO
    /// </summary>
    public class TaktIsoDocumentTemplateDto
    {
        /// <summary>
        /// 父级ID
        /// </summary>
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准
        /// </summary>
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型
        /// </summary>
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号
        /// </summary>
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文档名称
        /// </summary>
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>
        /// 文档级别
        /// </summary>
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述
        /// </summary>
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容
        /// </summary>
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本
        /// </summary>
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期
        /// </summary>
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; } = 0;

        /// <summary>
        /// 重要程度
        /// </summary>
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制
        /// </summary>
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开
        /// </summary>
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门
        /// </summary>
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人
        /// </summary>
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期
        /// </summary>
        public DateTime? DraftDate { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string? Reviewer { get; set; }

        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? ReviewDate { get; set; }

        /// <summary>
        /// 审核意见
        /// </summary>
        public string? ReviewComment { get; set; }

        /// <summary>
        /// 管理代表
        /// </summary>
        public string? ManagementRepresentative { get; set; }

        /// <summary>
        /// 管理代表批准日期
        /// </summary>
        public DateTime? ManagementApprovalDate { get; set; }

        /// <summary>
        /// 管理代表批准意见
        /// </summary>
        public string? ManagementApprovalComment { get; set; }

        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 发布方式
        /// </summary>
        public int PublishMethod { get; set; } = 1;

        /// <summary>
        /// 发布范围
        /// </summary>
        public string? PublishScope { get; set; }

        /// <summary>
        /// 相关文档
        /// </summary>
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件
        /// </summary>
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; } = 0;

        /// <summary>
        /// 下载次数
        /// </summary>
        public int DownloadCount { get; set; } = 0;
    }

    /// <summary>
    /// ISO文档状态DTO
    /// </summary>
    public class TaktIsoDocumentStatusDto
    {
        /// <summary>
        /// 文档ID
        /// </summary>
        [AdaptMember("Id")]
        public long IsoDocumentId { get; set; }

        /// <summary>
        /// 状态。0=草稿，1=待审核，2=已审核，3=已发布，4=已作废，5=已归档。
        /// </summary>
        public int Status { get; set; }
    }
}



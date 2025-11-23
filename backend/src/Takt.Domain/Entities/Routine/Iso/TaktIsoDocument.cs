#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIsoDocument.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : ISO文档实体类（简化树形结构）
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Iso
{
    /// <summary>
    /// ISO文档实体类（简化树形结构）
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录ISO质量管理体系文档，支持复杂组织架构的树形层级结构
    /// </remarks>
    [SugarTable("Takt_routine_document_iso", "ISO文档表")]
    [SugarIndex("ix_iso_document_code", nameof(DocumentCode), OrderByType.Asc, true)]
    [SugarIndex("ix_iso_document_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_type", nameof(DocumentType), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_level", nameof(DocumentLevel), OrderByType.Asc, false)]
    [SugarIndex("ix_iso_document_parent_id", nameof(ParentId), OrderByType.Asc, false)]
    public class TaktIsoDocument : TaktBaseEntity
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 父级ID。用于实现树形结构，0表示根节点。
        /// </summary>
        [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ParentId { get; set; }

        /// <summary>
        /// ISO标准。文档对应的ISO标准编号。
        /// </summary>
        [SugarColumn(ColumnName = "iso_standard", ColumnDescription = "ISO标准", ColumnDataType = "int", IsNullable = true)]
        public int? IsoStandard { get; set; }

        /// <summary>
        /// 文档类型。1=质量手册，2=程序文件，3=作业指导书，4=表格记录，5=外来文件，6=其他。
        /// </summary>
        [SugarColumn(ColumnName = "document_type", ColumnDescription = "文档类型", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentType { get; set; }

        /// <summary>
        /// 文档编号。ISO文档的唯一编号。
        /// </summary>
        [SugarColumn(ColumnName = "document_code", ColumnDescription = "文档编号", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentCode { get; set; } = string.Empty;

        /// <summary>
        /// 文档名称。ISO文档的名称。
        /// </summary>
        [SugarColumn(ColumnName = "document_name", ColumnDescription = "文档名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentName { get; set; } = string.Empty;

        /// <summary>
        /// 文档级别。1=一级，2=二级，3=三级，4=四级，5=五级。
        /// </summary>
        [SugarColumn(ColumnName = "document_level", ColumnDescription = "文档级别", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentLevel { get; set; }

        /// <summary>
        /// 文档描述。ISO文档的详细描述。
        /// </summary>
        [SugarColumn(ColumnName = "document_description", ColumnDescription = "文档描述", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentDescription { get; set; }

        /// <summary>
        /// 文档内容。ISO文档的具体内容。
        /// </summary>
        [SugarColumn(ColumnName = "document_content", ColumnDescription = "文档内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentContent { get; set; }

        /// <summary>
        /// 文档版本。ISO文档的版本号。
        /// </summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentVersion { get; set; }

        /// <summary>
        /// 生效日期。ISO文档的生效日期。
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期。ISO文档的失效日期。
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? ExpiryDate { get; set; }

        /// <summary>
        /// 排序号。用于自定义文档在列表中的显示顺序，值越小越靠前。
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;     

        /// <summary>
        /// 状态。0=草稿，1=待审核，2=已审核，3=已发布，4=已作废，5=已归档。
        /// </summary>
        [SugarColumn(ColumnName = "document_status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;   

        /// <summary>
        /// 重要程度。1=一般，2=重要，3=非常重要。
        /// </summary>
        [SugarColumn(ColumnName = "importance_level", ColumnDescription = "重要程度", ColumnDataType = "int", IsNullable = false, DefaultValue = "2")]
        public int ImportanceLevel { get; set; } = 2;

        /// <summary>
        /// 是否强制。0=否，1=是。
        /// </summary>
        [SugarColumn(ColumnName = "is_mandatory", ColumnDescription = "是否强制", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsMandatory { get; set; } = 1;

        /// <summary>
        /// 是否公开。0=否，1=是。
        /// </summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>
        /// 制定部门。制定ISO文档的部门。
        /// </summary>
        [SugarColumn(ColumnName = "draft_department", ColumnDescription = "制定部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftDepartment { get; set; }

        /// <summary>
        /// 文档制定人。制定ISO文档的人员。
        /// </summary>
        [SugarColumn(ColumnName = "draft_by", ColumnDescription = "文档制定人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DraftBy { get; set; }

        /// <summary>
        /// 文档制定日期。ISO文档的制定日期。
        /// </summary>
        [SugarColumn(ColumnName = "draft_date", ColumnDescription = "文档制定日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DraftDate { get; set; }


        /// <summary>
        /// 相关文档。与ISO文档相关的其他文档。
        /// </summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>
        /// 相关文件。与ISO文档相关的文件。
        /// </summary>
        [SugarColumn(ColumnName = "related_files", ColumnDescription = "相关文件", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedFiles { get; set; }

        /// <summary>
        /// 关键词。ISO文档的关键词，用于搜索。
        /// </summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }


        /// <summary>
        /// 存储路径。文件电子稿在服务器或文档管理系统中的存放位置。
        /// </summary>
        [SugarColumn(ColumnName = "storage_path", ColumnDescription = "存储路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StoragePath { get; set; }


        /// <summary>
        /// 父级文档。该文档的父级文档。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToOne, nameof(ParentId))]
        public TaktIsoDocument? Parent { get; set; }

        /// <summary>
        /// 子级文档列表。该文档的子级文档列表。
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(ParentId))]
        public List<TaktIsoDocument>? Children { get; set; }

        /// <summary>
        /// 文档查阅记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentAccess.DocumentId))]
        public List<TaktIsoDocumentAccess>? AccessRecords { get; set; }

        /// <summary>
        /// 文档审批记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentApproval.DocumentId))]
        public List<TaktIsoDocumentApproval>? ApprovalRecords { get; set; }

        /// <summary>
        /// 文档分发记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentDistribution.DocumentId))]
        public List<TaktIsoDocumentDistribution>? DistributionRecords { get; set; }

        /// <summary>
        /// 文档修订历史列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentRevision.DocumentId))]
        public List<TaktIsoDocumentRevision>? RevisionRecords { get; set; }

        /// <summary>
        /// 文档作废记录列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(TaktIsoDocumentObsolete.DocumentId))]
        public List<TaktIsoDocumentObsolete>? ObsoleteRecords { get; set; }
    }
} 




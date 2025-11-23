#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktProjectDocumentCommunication.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 项目文档与沟通实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Project
{
    /// <summary>
    /// 项目文档与沟通实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 说明: 记录项目文档与沟通的相关信息，包括文档信息、沟通记录、版本管理等
    /// </remarks>
    [SugarTable("Takt_routine_project_document_communication", "项目文档与沟通表")]
    [SugarIndex("ix_project_document_communication_code", nameof(ProjectDocumentCommunicationCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_project_document_communication", nameof(CompanyCode), OrderByType.Asc, nameof(ProjectDocumentCommunicationCode), OrderByType.Asc, false)]
    [SugarIndex("ix_project_document_communication_status", nameof(ProjectDocumentCommunicationStatus), OrderByType.Asc, false)]
    [SugarIndex("ix_project_document_communication_type", nameof(ProjectDocumentCommunicationType), OrderByType.Asc, false)]
    public class TaktProjectDocumentCommunication : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>项目文档与沟通编号</summary>
        [SugarColumn(ColumnName = "project_document_communication_code", ColumnDescription = "项目文档与沟通编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ProjectDocumentCommunicationCode { get; set; } = string.Empty;

        /// <summary>文档或沟通标题</summary>
        [SugarColumn(ColumnName = "document_communication_title", ColumnDescription = "文档或沟通标题", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DocumentCommunicationTitle { get; set; } = string.Empty;

        /// <summary>文档与沟通类型(1=项目计划 2=需求文档 3=设计文档 4=技术文档 5=会议纪要 6=进度报告 7=问题报告 8=变更申请 9=其他)</summary>
        [SugarColumn(ColumnName = "project_document_communication_type", ColumnDescription = "文档与沟通类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int ProjectDocumentCommunicationType { get; set; } = 1;

        /// <summary>关联项目编号</summary>
        [SugarColumn(ColumnName = "related_project_code", ColumnDescription = "关联项目编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectCode { get; set; } = string.Empty;

        /// <summary>关联项目名称</summary>
        [SugarColumn(ColumnName = "related_project_name", ColumnDescription = "关联项目名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string RelatedProjectName { get; set; } = string.Empty;

        /// <summary>关联任务编号</summary>
        [SugarColumn(ColumnName = "related_task_code", ColumnDescription = "关联任务编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskCode { get; set; }

        /// <summary>关联任务名称</summary>
        [SugarColumn(ColumnName = "related_task_name", ColumnDescription = "关联任务名称", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedTaskName { get; set; }

        /// <summary>文档或沟通内容</summary>
        [SugarColumn(ColumnName = "document_communication_content", ColumnDescription = "文档或沟通内容", Length = 8000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCommunicationContent { get; set; }

        /// <summary>文档或沟通摘要</summary>
        [SugarColumn(ColumnName = "document_communication_summary", ColumnDescription = "文档或沟通摘要", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCommunicationSummary { get; set; }

        /// <summary>文档版本</summary>
        [SugarColumn(ColumnName = "document_version", ColumnDescription = "文档版本", Length = 20, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "1.0")]
        public string DocumentVersion { get; set; } = "1.0";

        /// <summary>文档状态(0=草稿 1=待审核 2=已审核 3=已发布 4=已作废 5=已归档)</summary>
        [SugarColumn(ColumnName = "document_status", ColumnDescription = "文档状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DocumentStatus { get; set; } = 0;

        /// <summary>文档分类</summary>
        [SugarColumn(ColumnName = "document_category", ColumnDescription = "文档分类", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCategory { get; set; }

        /// <summary>文档关键词</summary>
        [SugarColumn(ColumnName = "document_keywords", ColumnDescription = "文档关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentKeywords { get; set; }

        /// <summary>文档标签</summary>
        [SugarColumn(ColumnName = "document_tags", ColumnDescription = "文档标签", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentTags { get; set; }

        /// <summary>文档大小(KB)</summary>
        [SugarColumn(ColumnName = "document_size", ColumnDescription = "文档大小(KB)", ColumnDataType = "int", IsNullable = true)]
        public int? DocumentSize { get; set; }

        /// <summary>文档格式</summary>
        [SugarColumn(ColumnName = "document_format", ColumnDescription = "文档格式", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentFormat { get; set; }

        /// <summary>文档路径</summary>
        [SugarColumn(ColumnName = "document_path", ColumnDescription = "文档路径", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentPath { get; set; }

        /// <summary>文档URL</summary>
        [SugarColumn(ColumnName = "document_url", ColumnDescription = "文档URL", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentUrl { get; set; }

        /// <summary>沟通方式(1=会议 2=邮件 3=电话 4=即时消息 5=文档 6=其他)</summary>
        [SugarColumn(ColumnName = "communication_method", ColumnDescription = "沟通方式", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CommunicationMethod { get; set; } = 1;

        /// <summary>沟通日期</summary>
        [SugarColumn(ColumnName = "communication_date", ColumnDescription = "沟通日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? CommunicationDate { get; set; }

        /// <summary>沟通时间</summary>
        [SugarColumn(ColumnName = "communication_time", ColumnDescription = "沟通时间", ColumnDataType = "time", IsNullable = true)]
        public TimeSpan? CommunicationTime { get; set; }

        /// <summary>沟通地点</summary>
        [SugarColumn(ColumnName = "communication_location", ColumnDescription = "沟通地点", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CommunicationLocation { get; set; }

        /// <summary>参与人员</summary>
        [SugarColumn(ColumnName = "participants", ColumnDescription = "参与人员", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Participants { get; set; }

        /// <summary>主持人</summary>
        [SugarColumn(ColumnName = "moderator", ColumnDescription = "主持人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Moderator { get; set; }

        /// <summary>记录人</summary>
        [SugarColumn(ColumnName = "recorder", ColumnDescription = "记录人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Recorder { get; set; }

        /// <summary>决策事项</summary>
        [SugarColumn(ColumnName = "decisions", ColumnDescription = "决策事项", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Decisions { get; set; }

        /// <summary>行动项</summary>
        [SugarColumn(ColumnName = "action_items", ColumnDescription = "行动项", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ActionItems { get; set; }

        /// <summary>后续跟进</summary>
        [SugarColumn(ColumnName = "follow_up", ColumnDescription = "后续跟进", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FollowUp { get; set; }

        /// <summary>文档创建人</summary>
        [SugarColumn(ColumnName = "document_creator", ColumnDescription = "文档创建人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCreator { get; set; }

        /// <summary>文档创建日期</summary>
        [SugarColumn(ColumnName = "document_creation_date", ColumnDescription = "文档创建日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DocumentCreationDate { get; set; }

        /// <summary>文档修改人</summary>
        [SugarColumn(ColumnName = "document_modifier", ColumnDescription = "文档修改人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentModifier { get; set; }

        /// <summary>文档修改日期</summary>
        [SugarColumn(ColumnName = "document_modification_date", ColumnDescription = "文档修改日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DocumentModificationDate { get; set; }

        /// <summary>文档审核人</summary>
        [SugarColumn(ColumnName = "document_reviewer", ColumnDescription = "文档审核人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentReviewer { get; set; }

        /// <summary>文档审核日期</summary>
        [SugarColumn(ColumnName = "document_review_date", ColumnDescription = "文档审核日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DocumentReviewDate { get; set; }

        /// <summary>文档审核意见</summary>
        [SugarColumn(ColumnName = "document_review_comment", ColumnDescription = "文档审核意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentReviewComment { get; set; }

        /// <summary>文档批准人</summary>
        [SugarColumn(ColumnName = "document_approver", ColumnDescription = "文档批准人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentApprover { get; set; }

        /// <summary>文档批准日期</summary>
        [SugarColumn(ColumnName = "document_approval_date", ColumnDescription = "文档批准日期", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? DocumentApprovalDate { get; set; }

        /// <summary>文档批准意见</summary>
        [SugarColumn(ColumnName = "document_approval_comment", ColumnDescription = "文档批准意见", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentApprovalComment { get; set; }

        /// <summary>是否公开(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_public", ColumnDescription = "是否公开", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int IsPublic { get; set; } = 1;

        /// <summary>访问权限</summary>
        [SugarColumn(ColumnName = "access_permissions", ColumnDescription = "访问权限", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AccessPermissions { get; set; }

        /// <summary>阅读次数</summary>
        [SugarColumn(ColumnName = "read_count", ColumnDescription = "阅读次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReadCount { get; set; } = 0;

        /// <summary>下载次数</summary>
        [SugarColumn(ColumnName = "download_count", ColumnDescription = "下载次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DownloadCount { get; set; } = 0;

        /// <summary>相关文档</summary>
        [SugarColumn(ColumnName = "related_documents", ColumnDescription = "相关文档", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? RelatedDocuments { get; set; }

        /// <summary>项目文档与沟通状态(0=草稿 1=待审核 2=已审核 3=已发布 4=已作废 5=已归档)</summary>
        [SugarColumn(ColumnName = "project_document_communication_status", ColumnDescription = "项目文档与沟通状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ProjectDocumentCommunicationStatus { get; set; } = 0;

        /// <summary>文档与沟通备注</summary>
        [SugarColumn(ColumnName = "document_communication_remark", ColumnDescription = "文档与沟通备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DocumentCommunicationRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 




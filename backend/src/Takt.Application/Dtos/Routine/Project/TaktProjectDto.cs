//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktProjectDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 项目数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Project
{
    /// <summary>
    /// 项目基础DTO
    /// </summary>
    public class TaktProjectDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectDto()
        {
            CompanyCode = string.Empty;
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectDescription = string.Empty;
            ProjectObjectives = string.Empty;
            ProjectScope = string.Empty;
            Currency = "CNY";
            ProjectSponsor = string.Empty;
            ProjectSponsorPhone = string.Empty;
            ProjectSponsorEmail = string.Empty;
            ProjectManager = string.Empty;
            ProjectManagerPhone = string.Empty;
            ProjectManagerEmail = string.Empty;
            ProjectDepartment = string.Empty;
            ProjectTeam = string.Empty;
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            ProjectLocation = string.Empty;
            ProjectKeywords = string.Empty;
            RelatedProjects = string.Empty;
            RelatedContracts = string.Empty;
            ProjectRemark = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long ProjectId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public string? ProjectObjectives { get; set; }

        /// <summary>
        /// 项目范围
        /// </summary>
        public string? ProjectScope { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public decimal? ProjectBudget { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>
        /// 项目期限(天)
        /// </summary>
        public int? ProjectDuration { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 项目进度(%)
        /// </summary>
        public decimal ProjectProgress { get; set; }

        /// <summary>
        /// 项目优先级
        /// </summary>
        public int ProjectPriority { get; set; }

        /// <summary>
        /// 项目规模
        /// </summary>
        public int ProjectScale { get; set; }

        /// <summary>
        /// 项目复杂度
        /// </summary>
        public int ProjectComplexity { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public int ProjectPhase { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public string? ProjectSponsor { get; set; }

        /// <summary>
        /// 项目发起人电话
        /// </summary>
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>
        /// 项目发起人邮箱
        /// </summary>
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 项目经理电话
        /// </summary>
        public string? ProjectManagerPhone { get; set; }

        /// <summary>
        /// 项目经理邮箱
        /// </summary>
        public string? ProjectManagerEmail { get; set; }

        /// <summary>
        /// 项目部门
        /// </summary>
        public string? ProjectDepartment { get; set; }

        /// <summary>
        /// 项目团队
        /// </summary>
        public string? ProjectTeam { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public string? CustomerContact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目关键词
        /// </summary>
        public string? ProjectKeywords { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProjects { get; set; }

        /// <summary>
        /// 相关合同
        /// </summary>
        public string? RelatedContracts { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string? ProjectRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

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
        /// 是否删除
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
    /// 项目查询DTO
    /// </summary>
    public class TaktProjectQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectQueryDto()
        {
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectManager = string.Empty;
            CustomerName = string.Empty;
        }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string? ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string? ProjectName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int? ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int? ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public int? ProjectPhase { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 参与者（用于查询我参与的项目）
        /// </summary>
        public string? Participant { get; set; }
    }

    /// <summary>
    /// 项目创建DTO
    /// </summary>
    public class TaktProjectCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectCreateDto()
        {
            CompanyCode = string.Empty;
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectDescription = string.Empty;
            ProjectObjectives = string.Empty;
            ProjectScope = string.Empty;
            Currency = "CNY";
            ProjectSponsor = string.Empty;
            ProjectSponsorPhone = string.Empty;
            ProjectSponsorEmail = string.Empty;
            ProjectManager = string.Empty;
            ProjectManagerPhone = string.Empty;
            ProjectManagerEmail = string.Empty;
            ProjectDepartment = string.Empty;
            ProjectTeam = string.Empty;
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            ProjectLocation = string.Empty;
            ProjectKeywords = string.Empty;
            RelatedProjects = string.Empty;
            RelatedContracts = string.Empty;
            ProjectRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public string? ProjectObjectives { get; set; }

        /// <summary>
        /// 项目范围
        /// </summary>
        public string? ProjectScope { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public decimal? ProjectBudget { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>
        /// 项目期限(天)
        /// </summary>
        public int? ProjectDuration { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 项目进度(%)
        /// </summary>
        public decimal ProjectProgress { get; set; }

        /// <summary>
        /// 项目优先级
        /// </summary>
        public int ProjectPriority { get; set; }

        /// <summary>
        /// 项目规模
        /// </summary>
        public int ProjectScale { get; set; }

        /// <summary>
        /// 项目复杂度
        /// </summary>
        public int ProjectComplexity { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public int ProjectPhase { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public string? ProjectSponsor { get; set; }

        /// <summary>
        /// 项目发起人电话
        /// </summary>
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>
        /// 项目发起人邮箱
        /// </summary>
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 项目经理电话
        /// </summary>
        public string? ProjectManagerPhone { get; set; }

        /// <summary>
        /// 项目经理邮箱
        /// </summary>
        public string? ProjectManagerEmail { get; set; }

        /// <summary>
        /// 项目部门
        /// </summary>
        public string? ProjectDepartment { get; set; }

        /// <summary>
        /// 项目团队
        /// </summary>
        public string? ProjectTeam { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public string? CustomerContact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目关键词
        /// </summary>
        public string? ProjectKeywords { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProjects { get; set; }

        /// <summary>
        /// 相关合同
        /// </summary>
        public string? RelatedContracts { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string? ProjectRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 项目更新DTO
    /// </summary>
    public class TaktProjectUpdateDto : TaktProjectCreateDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public long ProjectId { get; set; }
    }

    /// <summary>
    /// 项目删除DTO
    /// </summary>
    public class TaktProjectDeleteDto
    {
        /// <summary>主键ID</summary>
        [AdaptMember("Id")]
        public long ProjectId { get; set; }
    }

    /// <summary>
    /// 项目批量删除DTO
    /// </summary>
    public class TaktProjectBatchDeleteDto
    {
        /// <summary>主键ID列表</summary>
        public List<long> ProjectIds { get; set; } = new List<long>();
    }

    /// <summary>
    /// 项目导入DTO
    /// </summary>
    public class TaktProjectImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectImportDto()
        {
            CompanyCode = string.Empty;
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectDescription = string.Empty;
            ProjectObjectives = string.Empty;
            ProjectScope = string.Empty;
            Currency = "CNY";
            ProjectSponsor = string.Empty;
            ProjectSponsorPhone = string.Empty;
            ProjectSponsorEmail = string.Empty;
            ProjectManager = string.Empty;
            ProjectManagerPhone = string.Empty;
            ProjectManagerEmail = string.Empty;
            ProjectDepartment = string.Empty;
            ProjectTeam = string.Empty;
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            ProjectLocation = string.Empty;
            ProjectKeywords = string.Empty;
            RelatedProjects = string.Empty;
            RelatedContracts = string.Empty;
            ProjectRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public string? ProjectObjectives { get; set; }

        /// <summary>
        /// 项目范围
        /// </summary>
        public string? ProjectScope { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public decimal? ProjectBudget { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>
        /// 项目期限(天)
        /// </summary>
        public int? ProjectDuration { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 项目进度(%)
        /// </summary>
        public decimal ProjectProgress { get; set; }

        /// <summary>
        /// 项目优先级
        /// </summary>
        public int ProjectPriority { get; set; }

        /// <summary>
        /// 项目规模
        /// </summary>
        public int ProjectScale { get; set; }

        /// <summary>
        /// 项目复杂度
        /// </summary>
        public int ProjectComplexity { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public int ProjectPhase { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public string? ProjectSponsor { get; set; }

        /// <summary>
        /// 项目发起人电话
        /// </summary>
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>
        /// 项目发起人邮箱
        /// </summary>
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 项目经理电话
        /// </summary>
        public string? ProjectManagerPhone { get; set; }

        /// <summary>
        /// 项目经理邮箱
        /// </summary>
        public string? ProjectManagerEmail { get; set; }

        /// <summary>
        /// 项目部门
        /// </summary>
        public string? ProjectDepartment { get; set; }

        /// <summary>
        /// 项目团队
        /// </summary>
        public string? ProjectTeam { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public string? CustomerContact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目关键词
        /// </summary>
        public string? ProjectKeywords { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProjects { get; set; }

        /// <summary>
        /// 相关合同
        /// </summary>
        public string? RelatedContracts { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string? ProjectRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }

    /// <summary>
    /// 项目导出DTO
    /// </summary>
    public class TaktProjectExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectExportDto()
        {
            CompanyCode = string.Empty;
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectDescription = string.Empty;
            ProjectObjectives = string.Empty;
            ProjectScope = string.Empty;
            Currency = "CNY";
            ProjectSponsor = string.Empty;
            ProjectSponsorPhone = string.Empty;
            ProjectSponsorEmail = string.Empty;
            ProjectManager = string.Empty;
            ProjectManagerPhone = string.Empty;
            ProjectManagerEmail = string.Empty;
            ProjectDepartment = string.Empty;
            ProjectTeam = string.Empty;
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            ProjectLocation = string.Empty;
            ProjectKeywords = string.Empty;
            RelatedProjects = string.Empty;
            RelatedContracts = string.Empty;
            ProjectRemark = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public string? ProjectObjectives { get; set; }

        /// <summary>
        /// 项目范围
        /// </summary>
        public string? ProjectScope { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public decimal? ProjectBudget { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>
        /// 项目期限(天)
        /// </summary>
        public int? ProjectDuration { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 项目进度(%)
        /// </summary>
        public decimal ProjectProgress { get; set; }

        /// <summary>
        /// 项目优先级
        /// </summary>
        public int ProjectPriority { get; set; }

        /// <summary>
        /// 项目规模
        /// </summary>
        public int ProjectScale { get; set; }

        /// <summary>
        /// 项目复杂度
        /// </summary>
        public int ProjectComplexity { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public int ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段
        /// </summary>
        public int ProjectPhase { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public string? ProjectSponsor { get; set; }

        /// <summary>
        /// 项目发起人电话
        /// </summary>
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>
        /// 项目发起人邮箱
        /// </summary>
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 项目经理电话
        /// </summary>
        public string? ProjectManagerPhone { get; set; }

        /// <summary>
        /// 项目经理邮箱
        /// </summary>
        public string? ProjectManagerEmail { get; set; }

        /// <summary>
        /// 项目部门
        /// </summary>
        public string? ProjectDepartment { get; set; }

        /// <summary>
        /// 项目团队
        /// </summary>
        public string? ProjectTeam { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public string? CustomerContact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目关键词
        /// </summary>
        public string? ProjectKeywords { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProjects { get; set; }

        /// <summary>
        /// 相关合同
        /// </summary>
        public string? RelatedContracts { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string? ProjectRemark { get; set; }

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
    /// 项目导入模板DTO
    /// </summary>
    public class TaktProjectTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktProjectTemplateDto()
        {
            CompanyCode = string.Empty;
            ProjectCode = string.Empty;
            ProjectName = string.Empty;
            ProjectCategory = string.Empty;
            ProjectDescription = string.Empty;
            ProjectObjectives = string.Empty;
            ProjectScope = string.Empty;
            Currency = "CNY";
            ProjectSponsor = string.Empty;
            ProjectSponsorPhone = string.Empty;
            ProjectSponsorEmail = string.Empty;
            ProjectManager = string.Empty;
            ProjectManagerPhone = string.Empty;
            ProjectManagerEmail = string.Empty;
            ProjectDepartment = string.Empty;
            ProjectTeam = string.Empty;
            CustomerName = string.Empty;
            CustomerContact = string.Empty;
            CustomerPhone = string.Empty;
            CustomerEmail = string.Empty;
            ProjectLocation = string.Empty;
            ProjectKeywords = string.Empty;
            RelatedProjects = string.Empty;
            RelatedContracts = string.Empty;
            ProjectRemark = string.Empty;
        }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public string ProjectCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目类型(1=研发项目 2=实施项目 3=维护项目 4=咨询项目 5=培训项目 6=其他项目)
        /// </summary>
        public int ProjectType { get; set; }

        /// <summary>
        /// 项目分类
        /// </summary>
        public string? ProjectCategory { get; set; }

        /// <summary>
        /// 项目描述
        /// </summary>
        public string? ProjectDescription { get; set; }

        /// <summary>
        /// 项目目标
        /// </summary>
        public string? ProjectObjectives { get; set; }

        /// <summary>
        /// 项目范围
        /// </summary>
        public string? ProjectScope { get; set; }

        /// <summary>
        /// 项目预算
        /// </summary>
        public decimal? ProjectBudget { get; set; }

        /// <summary>
        /// 币种(CNY=人民币 USD=美元 EUR=欧元)
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// 项目开始日期
        /// </summary>
        public DateTime? ProjectStartDate { get; set; }

        /// <summary>
        /// 项目结束日期
        /// </summary>
        public DateTime? ProjectEndDate { get; set; }

        /// <summary>
        /// 项目期限(天)
        /// </summary>
        public int? ProjectDuration { get; set; }

        /// <summary>
        /// 实际开始日期
        /// </summary>
        public DateTime? ActualStartDate { get; set; }

        /// <summary>
        /// 实际结束日期
        /// </summary>
        public DateTime? ActualEndDate { get; set; }

        /// <summary>
        /// 项目进度(%)
        /// </summary>
        public decimal ProjectProgress { get; set; }

        /// <summary>
        /// 项目优先级(1=低 2=中 3=高 4=紧急)
        /// </summary>
        public int ProjectPriority { get; set; }

        /// <summary>
        /// 项目规模(1=小型 2=中型 3=大型 4=特大型)
        /// </summary>
        public int ProjectScale { get; set; }

        /// <summary>
        /// 项目复杂度(1=简单 2=一般 3=复杂 4=极复杂)
        /// </summary>
        public int ProjectComplexity { get; set; }

        /// <summary>
        /// 项目状态(0=规划中 1=已启动 2=进行中 3=已完成 4=已暂停 5=已终止 6=已关闭)
        /// </summary>
        public int ProjectStatus { get; set; }

        /// <summary>
        /// 项目阶段(1=启动阶段 2=规划阶段 3=执行阶段 4=监控阶段 5=收尾阶段)
        /// </summary>
        public int ProjectPhase { get; set; }

        /// <summary>
        /// 项目发起人
        /// </summary>
        public string? ProjectSponsor { get; set; }

        /// <summary>
        /// 项目发起人电话
        /// </summary>
        public string? ProjectSponsorPhone { get; set; }

        /// <summary>
        /// 项目发起人邮箱
        /// </summary>
        public string? ProjectSponsorEmail { get; set; }

        /// <summary>
        /// 项目经理
        /// </summary>
        public string? ProjectManager { get; set; }

        /// <summary>
        /// 项目经理电话
        /// </summary>
        public string? ProjectManagerPhone { get; set; }

        /// <summary>
        /// 项目经理邮箱
        /// </summary>
        public string? ProjectManagerEmail { get; set; }

        /// <summary>
        /// 项目部门
        /// </summary>
        public string? ProjectDepartment { get; set; }

        /// <summary>
        /// 项目团队
        /// </summary>
        public string? ProjectTeam { get; set; }

        /// <summary>
        /// 客户名称
        /// </summary>
        public string? CustomerName { get; set; }

        /// <summary>
        /// 客户联系人
        /// </summary>
        public string? CustomerContact { get; set; }

        /// <summary>
        /// 客户联系电话
        /// </summary>
        public string? CustomerPhone { get; set; }

        /// <summary>
        /// 客户联系邮箱
        /// </summary>
        public string? CustomerEmail { get; set; }

        /// <summary>
        /// 项目地点
        /// </summary>
        public string? ProjectLocation { get; set; }

        /// <summary>
        /// 项目关键词
        /// </summary>
        public string? ProjectKeywords { get; set; }

        /// <summary>
        /// 相关项目
        /// </summary>
        public string? RelatedProjects { get; set; }

        /// <summary>
        /// 相关合同
        /// </summary>
        public string? RelatedContracts { get; set; }

        /// <summary>
        /// 项目备注
        /// </summary>
        public string? ProjectRemark { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }
    }
} 



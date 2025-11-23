#nullable enable

using Takt.Shared.Models;
using Mapster;

namespace Takt.Application.Dtos.Logistics.Manufacturing.Bom.Change
{
    /// <summary>
    /// 设计变更执行DTO
    /// </summary>
    public class TaktChangeExecutionDto : TaktPagedQuery
    {
        /// <summary>
        /// 设计变更执行ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionId { get; set; } = string.Empty;

        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        public int ManageDivision { get; set; }

        /// <summary>
        /// 设变文档
        /// </summary>
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 设计变更明细集合
        /// </summary>
        public List<TaktChangeExecutionDetailDto> ChangeExecutionDetails { get; set; } = new();
    }

    /// <summary>
    /// 设计变更执行查询DTO
    /// </summary>
    public class TaktChangeExecutionQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string? ChangeNumber { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 管理区分
        /// </summary>
        public int? ManageDivision { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 发行日期开始
        /// </summary>
        public DateTime? IssueDateStart { get; set; }

        /// <summary>
        /// 发行日期结束
        /// </summary>
        public DateTime? IssueDateEnd { get; set; }
    }

    /// <summary>
    /// 设计变更执行创建DTO
    /// </summary>
    public class TaktChangeExecutionCreateDto
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        public int ManageDivision { get; set; }

        /// <summary>
        /// 设变文档
        /// </summary>
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 设计变更执行更新DTO
    /// </summary>
    public class TaktChangeExecutionUpdateDto : TaktChangeExecutionCreateDto
    {
        /// <summary>
        /// 设计变更执行ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionId { get; set; } = string.Empty;
    }

    /// <summary>
    /// 设计变更执行导入DTO
    /// </summary>
    public class TaktChangeExecutionImportDto
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        public int ManageDivision { get; set; }

        /// <summary>
        /// 设变文档
        /// </summary>
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 设计变更执行导出DTO
    /// </summary>
    public class TaktChangeExecutionExportDto
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        public int ManageDivision { get; set; }

        /// <summary>
        /// 设变文档
        /// </summary>
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 设计变更执行模板DTO
    /// </summary>
    public class TaktChangeExecutionTemplateDto
    {
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime? IssueDate { get; set; }

        /// <summary>
        /// 设计变更号码
        /// </summary>
        public string ChangeNumber { get; set; } = string.Empty;

        /// <summary>
        /// 标题
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// 变更内容
        /// </summary>
        public string? ChangeDescription { get; set; }

        /// <summary>
        /// 技术担当
        /// </summary>
        public string? TechOwner { get; set; }

        /// <summary>
        /// 仕损金额
        /// </summary>
        public decimal? LossAmount { get; set; }

        /// <summary>
        /// 管理区分(0=未分配 1=全部 2=部管 3=技术 4=内部)
        /// </summary>
        public int ManageDivision { get; set; }

        /// <summary>
        /// 设变文档
        /// </summary>
        public string? ChangeDoc { get; set; }

        /// <summary>
        /// 联络文档
        /// </summary>
        public string? ContactDoc { get; set; }

        /// <summary>
        /// 技联文档
        /// </summary>
        public string? TechUnionDoc { get; set; }

        /// <summary>
        /// P番文档
        /// </summary>
        public string? PNumberDoc { get; set; }

        /// <summary>
        /// 外部文档
        /// </summary>
        public string? ExternalDoc { get; set; }

        /// <summary>
        /// 登录日期
        /// </summary>
        public DateTime? LoginDate { get; set; }

        /// <summary>
        /// 状态(1=工作的 2=取消的 3=发行的 4=PP中 5=固定的 6=挂起的 7=拒绝的)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 设计变更执行状态DTO
    /// </summary>
    public class TaktChangeExecutionStatusDto
    {
        /// <summary>
        /// 设计变更执行ID
        /// </summary>
        [AdaptMember("Id")]
        public string ChangeExecutionId { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}



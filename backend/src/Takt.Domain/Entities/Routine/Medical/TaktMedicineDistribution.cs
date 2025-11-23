#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMedicineDistribution.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 药品发放主表实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Medical
{
    /// <summary>
    /// 药品发放主表实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录医务室药品发放信息，与就诊记录关联
    /// </remarks>
    [SugarTable("Takt_routine_medical_medicine_distribution", "药品发放主表")]
    [SugarIndex("ix_medical_medicine_distribution_code", nameof(DistributionCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_medical_medicine_distribution", nameof(CompanyCode), OrderByType.Asc, nameof(DistributionCode), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_visit", nameof(VisitId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_employee", nameof(EmployeeId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_medicine_distribution_time", nameof(DistributionTime), OrderByType.Desc, false)]
    public class TaktMedicineDistribution : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>发放编号</summary>
        [SugarColumn(ColumnName = "distribution_code", ColumnDescription = "发放编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string DistributionCode { get; set; } = string.Empty;

        /// <summary>就诊记录ID</summary>
        [SugarColumn(ColumnName = "visit_id", ColumnDescription = "就诊记录ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? VisitId { get; set; }

        /// <summary>就诊编号</summary>
        [SugarColumn(ColumnName = "visit_code", ColumnDescription = "就诊编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? VisitCode { get; set; }

        /// <summary>员工ID</summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>员工姓名</summary>
        [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>员工部门</summary>
        [SugarColumn(ColumnName = "employee_department", ColumnDescription = "员工部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeeDepartment { get; set; }

        /// <summary>发放类型(1=就诊发放 2=定期发放 3=紧急发放 4=预防性发放)</summary>
        [SugarColumn(ColumnName = "distribution_type", ColumnDescription = "发放类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int DistributionType { get; set; } = 1;

        /// <summary>发放时间</summary>
        [SugarColumn(ColumnName = "distribution_time", ColumnDescription = "发放时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime DistributionTime { get; set; }

        /// <summary>发放原因</summary>
        [SugarColumn(ColumnName = "distribution_reason", ColumnDescription = "发放原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DistributionReason { get; set; }

        /// <summary>医生建议</summary>
        [SugarColumn(ColumnName = "doctor_advice", ColumnDescription = "医生建议", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DoctorAdvice { get; set; }

        /// <summary>用药说明</summary>
        [SugarColumn(ColumnName = "usage_instructions", ColumnDescription = "用药说明", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? UsageInstructions { get; set; }

        /// <summary>注意事项</summary>
        [SugarColumn(ColumnName = "precautions", ColumnDescription = "注意事项", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Precautions { get; set; }

        /// <summary>药品总数量</summary>
        [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "药品总数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TotalQuantity { get; set; } = 0;

        /// <summary>药品总金额</summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "药品总金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>是否收费(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_charged", ColumnDescription = "是否收费", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsCharged { get; set; } = 0;

        /// <summary>收费金额</summary>
        [SugarColumn(ColumnName = "charge_amount", ColumnDescription = "收费金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ChargeAmount { get; set; } = 0;

        /// <summary>是否已收费(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_paid", ColumnDescription = "是否已收费", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsPaid { get; set; } = 0;

        /// <summary>收费时间</summary>
        [SugarColumn(ColumnName = "payment_time", ColumnDescription = "收费时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PaymentTime { get; set; }

        /// <summary>收费方式(1=现金 2=刷卡 3=转账 4=其他)</summary>
        [SugarColumn(ColumnName = "payment_method", ColumnDescription = "收费方式", ColumnDataType = "int", IsNullable = true)]
        public int? PaymentMethod { get; set; }

        /// <summary>经办人ID</summary>
        [SugarColumn(ColumnName = "operator_id", ColumnDescription = "经办人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? OperatorId { get; set; }

        /// <summary>经办人姓名</summary>
        [SugarColumn(ColumnName = "operator_name", ColumnDescription = "经办人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperatorName { get; set; }

        /// <summary>状态(0=正常 1=已归档 2=已删除)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;



        /// <summary>
        /// 导航属性：关联的就诊记录
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TaktMedicalVisit.Id))]
        public TaktMedicalVisit? Visit { get; set; }

        /// <summary>
        /// 导航属性：药品发放明细列表
        /// </summary>
        [Navigate(NavigateType.OneToMany, nameof(TaktMedicineDistributionDetail.DistributionId))]
        public List<TaktMedicineDistributionDetail>? DistributionDetails { get; set; }
    }
} 




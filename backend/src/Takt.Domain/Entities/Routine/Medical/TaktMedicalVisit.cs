#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktVisit.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 医务就诊记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Medical
{
    /// <summary>
    /// 医务就诊记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录员工在医务室的就诊信息，适用于企业内部医务管理
    /// </remarks>
    [SugarTable("Takt_routine_medical_visit", "医务就诊记录")]
    [SugarIndex("ix_medical_visit_code", nameof(VisitCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_medical_visit", nameof(CompanyCode), OrderByType.Asc, nameof(VisitCode), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_visit_employee", nameof(EmployeeId), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_visit_type", nameof(VisitType), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_visit_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_medical_visit_visit_time", nameof(VisitTime), OrderByType.Desc, false)]
    public class TaktMedicalVisit : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>就诊编号</summary>
        [SugarColumn(ColumnName = "visit_code", ColumnDescription = "就诊编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string VisitCode { get; set; } = string.Empty;

        /// <summary>员工ID</summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>员工姓名</summary>
        [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>员工部门</summary>
        [SugarColumn(ColumnName = "employee_department", ColumnDescription = "员工部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeeDepartment { get; set; }

                /// <summary>医生ID</summary>
        [SugarColumn(ColumnName = "doctor_id", ColumnDescription = "医生ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? DoctorId { get; set; }


        /// <summary>就诊类型(1=一般就诊 2=体检 3=疫苗接种 4=健康咨询 5=急救处理)</summary>
        [SugarColumn(ColumnName = "visit_type", ColumnDescription = "就诊类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int VisitType { get; set; } = 1;

        /// <summary>就诊时间</summary>
        [SugarColumn(ColumnName = "visit_time", ColumnDescription = "就诊时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime VisitTime { get; set; }

        /// <summary>症状描述</summary>
        [SugarColumn(ColumnName = "symptoms", ColumnDescription = "症状描述", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Symptoms { get; set; }

        /// <summary>初步诊断</summary>
        [SugarColumn(ColumnName = "diagnosis", ColumnDescription = "初步诊断", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Diagnosis { get; set; }

        /// <summary>处理措施</summary>
        [SugarColumn(ColumnName = "treatment", ColumnDescription = "处理措施", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Treatment { get; set; }

        /// <summary>用药情况</summary>
        [SugarColumn(ColumnName = "medication", ColumnDescription = "用药情况", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Medication { get; set; }

        /// <summary>体温</summary>
        [SugarColumn(ColumnName = "temperature", ColumnDescription = "体温", ColumnDataType = "decimal(4,1)", IsNullable = true)]
        public decimal? Temperature { get; set; }

        /// <summary>血压</summary>
        [SugarColumn(ColumnName = "blood_pressure", ColumnDescription = "血压", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BloodPressure { get; set; }

        /// <summary>脉搏</summary>
        [SugarColumn(ColumnName = "pulse", ColumnDescription = "脉搏", ColumnDataType = "int", IsNullable = true)]
        public int? Pulse { get; set; }

        /// <summary>是否需要转诊(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "need_referral", ColumnDescription = "是否需要转诊", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int NeedReferral { get; set; } = 0;

        /// <summary>转诊医院</summary>
        [SugarColumn(ColumnName = "referral_hospital", ColumnDescription = "转诊医院", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReferralHospital { get; set; }

        /// <summary>转诊科室</summary>
        [SugarColumn(ColumnName = "referral_department", ColumnDescription = "转诊科室", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReferralDepartment { get; set; }

        /// <summary>转诊原因</summary>
        [SugarColumn(ColumnName = "referral_reason", ColumnDescription = "转诊原因", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReferralReason { get; set; }

        /// <summary>费用金额</summary>
        [SugarColumn(ColumnName = "cost_amount", ColumnDescription = "费用金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal CostAmount { get; set; } = 0;

        /// <summary>是否报销(0=否 1=是)</summary>
        [SugarColumn(ColumnName = "is_reimbursed", ColumnDescription = "是否报销", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int IsReimbursed { get; set; } = 0;

        /// <summary>报销金额</summary>
        [SugarColumn(ColumnName = "reimbursement_amount", ColumnDescription = "报销金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal ReimbursementAmount { get; set; } = 0;

        /// <summary>报销时间</summary>
        [SugarColumn(ColumnName = "reimbursement_time", ColumnDescription = "报销时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReimbursementTime { get; set; }

        /// <summary>下次复诊时间</summary>
        [SugarColumn(ColumnName = "next_visit_time", ColumnDescription = "下次复诊时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? NextVisitTime { get; set; }

        /// <summary>医嘱</summary>
        [SugarColumn(ColumnName = "doctor_advice", ColumnDescription = "医嘱", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? DoctorAdvice { get; set; }

        /// <summary>注意事项</summary>
        [SugarColumn(ColumnName = "precautions", ColumnDescription = "注意事项", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Precautions { get; set; }



        /// <summary>状态(0=正常 1=已归档 2=已删除)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;


        /// <summary>
        /// 导航属性：关联的药品发放记录
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(TaktMedicineDistribution.VisitId))]
        public TaktMedicineDistribution? MedicineDistribution { get; set; }
    }
} 




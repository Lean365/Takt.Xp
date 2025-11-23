#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktEmployeeHealthRecord.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 员工健康档案实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Medical
{
    /// <summary>
    /// 员工健康档案实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录员工的基本健康信息，适用于企业内部医务管理
    /// </remarks>
    [SugarTable("Takt_routine_medical_employee_health", "员工健康档案")]
    [SugarIndex("ix_employee_health_employee", nameof(EmployeeId), OrderByType.Asc, true)]
    [SugarIndex("ix_company_employee_health", nameof(CompanyCode), OrderByType.Asc, nameof(EmployeeId), OrderByType.Asc, false)]
    [SugarIndex("ix_employee_health_status", nameof(Status), OrderByType.Asc, false)]
    public class TaktEmployeeHealthRecord : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>员工ID</summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>员工姓名</summary>
        [SugarColumn(ColumnName = "employee_name", ColumnDescription = "员工姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string EmployeeName { get; set; } = string.Empty;

        /// <summary>员工部门</summary>
        [SugarColumn(ColumnName = "employee_department", ColumnDescription = "员工部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeeDepartment { get; set; }

        /// <summary>员工职位</summary>
        [SugarColumn(ColumnName = "employee_position", ColumnDescription = "员工职位", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmployeePosition { get; set; }

        /// <summary>血型</summary>
        [SugarColumn(ColumnName = "blood_type", ColumnDescription = "血型", Length = 10, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BloodType { get; set; }

        /// <summary>身高(cm)</summary>
        [SugarColumn(ColumnName = "height", ColumnDescription = "身高(cm)", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? Height { get; set; }

        /// <summary>体重(kg)</summary>
        [SugarColumn(ColumnName = "weight", ColumnDescription = "体重(kg)", ColumnDataType = "decimal(5,2)", IsNullable = true)]
        public decimal? Weight { get; set; }

        /// <summary>过敏史</summary>
        [SugarColumn(ColumnName = "allergy_history", ColumnDescription = "过敏史", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AllergyHistory { get; set; }

        /// <summary>慢性病史</summary>
        [SugarColumn(ColumnName = "chronic_disease_history", ColumnDescription = "慢性病史", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChronicDiseaseHistory { get; set; }

        /// <summary>家族病史</summary>
        [SugarColumn(ColumnName = "family_disease_history", ColumnDescription = "家族病史", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? FamilyDiseaseHistory { get; set; }

        /// <summary>紧急联系人</summary>
        [SugarColumn(ColumnName = "emergency_contact", ColumnDescription = "紧急联系人", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyContact { get; set; }

        /// <summary>紧急联系人电话</summary>
        [SugarColumn(ColumnName = "emergency_phone", ColumnDescription = "紧急联系人电话", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyPhone { get; set; }

        /// <summary>紧急联系人关系</summary>
        [SugarColumn(ColumnName = "emergency_relationship", ColumnDescription = "紧急联系人关系", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? EmergencyRelationship { get; set; }



        /// <summary>状态(0=正常 1=已归档)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
} 




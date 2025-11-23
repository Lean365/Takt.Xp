// -----------------------------------------------------------------------------
// <copyright file="TaktHousingFund.cs" company="Takt365">
//     Copyright (c) Takt365. All rights reserved.
// </copyright>
// <author>自动生成/Auto Generated</author>
// <date>2025-06-27</date>
// <description>HousingFund 实体</description>
// -----------------------------------------------------------------------------

#nullable enable

using SqlSugar;

namespace Takt.Domain.Entities.HumanResource.Salary;
    /// <summary>
    /// 公积金实体
    /// </summary>
    [SugarTable("takt_humanresource_housing_fund", "公积金表")]
    [SugarIndex("ix_housing_fund_employee", nameof(EmployeeId), OrderByType.Asc)]
    [SugarIndex("ix_housing_fund_month", nameof(FundMonth), OrderByType.Asc)]
    public class TaktHousingFund : TaktBaseEntity
    {
        /// <summary>
        /// 公积金编号
        /// </summary>
        [SugarColumn(ColumnName = "fund_no", ColumnDescription = "公积金编号", ColumnDataType = "nvarchar", Length = 20, IsNullable = false)]
        public string FundNo { get; set; } = string.Empty;

        /// <summary>
        /// 员工ID
        /// </summary>
        [SugarColumn(ColumnName = "employee_id", ColumnDescription = "员工ID", ColumnDataType = "bigint", IsNullable = false)]
        public long EmployeeId { get; set; }

        /// <summary>
        /// 公积金年月(如202406)
        /// </summary>
        [SugarColumn(ColumnName = "fund_month", ColumnDescription = "公积金年月", ColumnDataType = "nvarchar", Length = 10, IsNullable = false)]
        public string FundMonth { get; set; } = string.Empty;

        /// <summary>
        /// 公积金基数
        /// </summary>
        [SugarColumn(ColumnName = "fund_base", ColumnDescription = "公积金基数", ColumnDataType = "decimal(10,2)", IsNullable = false)]
        public decimal FundBase { get; set; }

        /// <summary>
        /// 个人缴纳比例(%)
        /// </summary>
        [SugarColumn(ColumnName = "personal_rate", ColumnDescription = "个人缴纳比例", ColumnDataType = "decimal(5,2)", IsNullable = false)]
        public decimal PersonalRate { get; set; }

        /// <summary>
        /// 单位缴纳比例(%)
        /// </summary>
        [SugarColumn(ColumnName = "company_rate", ColumnDescription = "单位缴纳比例", ColumnDataType = "decimal(5,2)", IsNullable = false)]
        public decimal CompanyRate { get; set; }

        /// <summary>
        /// 个人缴纳金额
        /// </summary>
        [SugarColumn(ColumnName = "personal_amount", ColumnDescription = "个人缴纳金额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
        public decimal PersonalAmount { get; set; }

        /// <summary>
        /// 单位缴纳金额
        /// </summary>
        [SugarColumn(ColumnName = "company_amount", ColumnDescription = "单位缴纳金额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
        public decimal CompanyAmount { get; set; }

        /// <summary>
        /// 缴纳总额
        /// </summary>
        [SugarColumn(ColumnName = "total_amount", ColumnDescription = "缴纳总额", ColumnDataType = "decimal(10,2)", IsNullable = false)]
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 缴纳状态(1=未缴纳 2=已缴纳 3=已补缴)
        /// </summary>
        [SugarColumn(ColumnName = "payment_status", ColumnDescription = "缴纳状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int PaymentStatus { get; set; }

        /// <summary>
        /// 缴纳时间
        /// </summary>
        [SugarColumn(ColumnName = "payment_time", ColumnDescription = "缴纳时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? PaymentTime { get; set; }



        /// <summary>
        /// 员工信息
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(EmployeeId))]
        public virtual Employee.TaktEmployee? Employee { get; set; }
    }



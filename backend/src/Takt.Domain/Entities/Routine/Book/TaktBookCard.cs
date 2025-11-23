#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBookCard.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 图书借阅卡实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Book
{
    /// <summary>
    /// 图书借阅卡实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录图书借阅卡信息，包括持卡人、卡号、有效期等
    /// </remarks>
    [SugarTable("Takt_routine_book_card", "图书借阅卡")]
    [SugarIndex("ix_book_card_code", nameof(CardCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_book_card", nameof(CompanyCode), OrderByType.Asc, nameof(CardCode), OrderByType.Asc, false)]
    [SugarIndex("ix_book_card_holder", nameof(CardHolderId), OrderByType.Asc, false)]
    [SugarIndex("ix_book_card_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_book_card_type", nameof(CardType), OrderByType.Asc, false)]
    public class TaktBookCard : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>借阅卡号</summary>
        [SugarColumn(ColumnName = "card_code", ColumnDescription = "借阅卡号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CardCode { get; set; } = string.Empty;

        /// <summary>持卡人ID</summary>
        [SugarColumn(ColumnName = "card_holder_id", ColumnDescription = "持卡人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CardHolderId { get; set; }

        /// <summary>持卡人姓名</summary>
        [SugarColumn(ColumnName = "card_holder_name", ColumnDescription = "持卡人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CardHolderName { get; set; } = string.Empty;

        /// <summary>持卡人部门</summary>
        [SugarColumn(ColumnName = "card_holder_department", ColumnDescription = "持卡人部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CardHolderDepartment { get; set; }

        /// <summary>持卡人职位</summary>
        [SugarColumn(ColumnName = "card_holder_position", ColumnDescription = "持卡人职位", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CardHolderPosition { get; set; }

        /// <summary>持卡人联系方式</summary>
        [SugarColumn(ColumnName = "card_holder_contact", ColumnDescription = "持卡人联系方式", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CardHolderContact { get; set; }

        /// <summary>卡类型(1=员工卡 2=临时卡 3=VIP卡 4=学生卡)</summary>
        [SugarColumn(ColumnName = "card_type", ColumnDescription = "卡类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int CardType { get; set; } = 1;

        /// <summary>发卡时间</summary>
        [SugarColumn(ColumnName = "issue_time", ColumnDescription = "发卡时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime IssueTime { get; set; }

        /// <summary>有效期开始时间</summary>
        [SugarColumn(ColumnName = "valid_start_time", ColumnDescription = "有效期开始时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ValidStartTime { get; set; }

        /// <summary>有效期结束时间</summary>
        [SugarColumn(ColumnName = "valid_end_time", ColumnDescription = "有效期结束时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ValidEndTime { get; set; }

        /// <summary>最大借阅数量</summary>
        [SugarColumn(ColumnName = "max_borrow_quantity", ColumnDescription = "最大借阅数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
        public int MaxBorrowQuantity { get; set; } = 5;

        /// <summary>当前借阅数量</summary>
        [SugarColumn(ColumnName = "current_borrow_quantity", ColumnDescription = "当前借阅数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int CurrentBorrowQuantity { get; set; } = 0;

        /// <summary>最大借阅天数</summary>
        [SugarColumn(ColumnName = "max_borrow_days", ColumnDescription = "最大借阅天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "30")]
        public int MaxBorrowDays { get; set; } = 30;

        /// <summary>逾期费用(每天)</summary>
        [SugarColumn(ColumnName = "overdue_fee_per_day", ColumnDescription = "逾期费用(每天)", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0.5")]
        public decimal OverdueFeePerDay { get; set; } = 0.5m;

        /// <summary>押金金额</summary>
        [SugarColumn(ColumnName = "deposit_amount", ColumnDescription = "押金金额", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal DepositAmount { get; set; } = 0;

        /// <summary>押金状态(0=未缴纳 1=已缴纳 2=已退还)</summary>
        [SugarColumn(ColumnName = "deposit_status", ColumnDescription = "押金状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DepositStatus { get; set; } = 0;

        /// <summary>发卡人ID</summary>
        [SugarColumn(ColumnName = "issuer_id", ColumnDescription = "发卡人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? IssuerId { get; set; }

        /// <summary>发卡人姓名</summary>
        [SugarColumn(ColumnName = "issuer_name", ColumnDescription = "发卡人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? IssuerName { get; set; }

        /// <summary>挂失时间</summary>
        [SugarColumn(ColumnName = "loss_report_time", ColumnDescription = "挂失时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? LossReportTime { get; set; }

        /// <summary>挂失人ID</summary>
        [SugarColumn(ColumnName = "loss_reporter_id", ColumnDescription = "挂失人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? LossReporterId { get; set; }

        /// <summary>挂失人姓名</summary>
        [SugarColumn(ColumnName = "loss_reporter_name", ColumnDescription = "挂失人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? LossReporterName { get; set; }

        /// <summary>补卡时间</summary>
        [SugarColumn(ColumnName = "reissue_time", ColumnDescription = "补卡时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReissueTime { get; set; }

        /// <summary>补卡人ID</summary>
        [SugarColumn(ColumnName = "reissuer_id", ColumnDescription = "补卡人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? ReissuerId { get; set; }

        /// <summary>补卡人姓名</summary>
        [SugarColumn(ColumnName = "reissuer_name", ColumnDescription = "补卡人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ReissuerName { get; set; }

        /// <summary>卡状态(0=正常 1=挂失 2=冻结 3=过期 4=注销)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "卡状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;


    }
}




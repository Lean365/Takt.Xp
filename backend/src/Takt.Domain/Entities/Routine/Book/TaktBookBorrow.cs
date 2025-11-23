#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBookBorrow.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 图书借阅实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Book
{
    /// <summary>
    /// 图书借阅实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录图书借阅信息，包括借阅人、借阅时间、归还时间等
    /// </remarks>
    [SugarTable("Takt_routine_book_borrow", "图书借阅")]
    [SugarIndex("ix_book_borrow_code", nameof(BorrowCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_book_borrow", nameof(CompanyCode), OrderByType.Asc, nameof(BorrowCode), OrderByType.Asc, false)]
    [SugarIndex("ix_book_borrow_borrower", nameof(BorrowerId), OrderByType.Asc, false)]
    [SugarIndex("ix_book_borrow_book", nameof(BookId), OrderByType.Asc, false)]
    [SugarIndex("ix_book_borrow_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_book_borrow_borrow_time", nameof(BorrowTime), OrderByType.Desc, false)]
    public class TaktBookBorrow : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>借阅编号</summary>
        [SugarColumn(ColumnName = "borrow_code", ColumnDescription = "借阅编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BorrowCode { get; set; } = string.Empty;

        /// <summary>图书ID</summary>
        [SugarColumn(ColumnName = "book_id", ColumnDescription = "图书ID", ColumnDataType = "bigint", IsNullable = false)]
        public long BookId { get; set; }

        /// <summary>图书编号</summary>
        [SugarColumn(ColumnName = "book_code", ColumnDescription = "图书编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BookCode { get; set; } = string.Empty;

        /// <summary>书名</summary>
        [SugarColumn(ColumnName = "book_name", ColumnDescription = "书名", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BookName { get; set; } = string.Empty;

        /// <summary>借阅人ID</summary>
        [SugarColumn(ColumnName = "borrower_id", ColumnDescription = "借阅人ID", ColumnDataType = "bigint", IsNullable = false)]
        public long BorrowerId { get; set; }

        /// <summary>借阅人姓名</summary>
        [SugarColumn(ColumnName = "borrower_name", ColumnDescription = "借阅人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BorrowerName { get; set; } = string.Empty;

        /// <summary>借阅人部门</summary>
        [SugarColumn(ColumnName = "borrower_department", ColumnDescription = "借阅人部门", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BorrowerDepartment { get; set; }

        /// <summary>借阅时间</summary>
        [SugarColumn(ColumnName = "borrow_time", ColumnDescription = "借阅时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime BorrowTime { get; set; }

        /// <summary>应还时间</summary>
        [SugarColumn(ColumnName = "due_time", ColumnDescription = "应还时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime DueTime { get; set; }

        /// <summary>实际归还时间</summary>
        [SugarColumn(ColumnName = "return_time", ColumnDescription = "实际归还时间", ColumnDataType = "datetime", IsNullable = true)]
        public DateTime? ReturnTime { get; set; }

        /// <summary>借阅状态(0=借阅中 1=已归还 2=逾期 3=丢失)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "借阅状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;

        /// <summary>借阅数量</summary>
        [SugarColumn(ColumnName = "borrow_quantity", ColumnDescription = "借阅数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
        public int BorrowQuantity { get; set; } = 1;

        /// <summary>归还数量</summary>
        [SugarColumn(ColumnName = "return_quantity", ColumnDescription = "归还数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int ReturnQuantity { get; set; } = 0;

        /// <summary>经办人ID</summary>
        [SugarColumn(ColumnName = "operator_id", ColumnDescription = "经办人ID", ColumnDataType = "bigint", IsNullable = true)]
        public long? OperatorId { get; set; }

        /// <summary>经办人姓名</summary>
        [SugarColumn(ColumnName = "operator_name", ColumnDescription = "经办人姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? OperatorName { get; set; }

        /// <summary>逾期天数</summary>
        [SugarColumn(ColumnName = "overdue_days", ColumnDescription = "逾期天数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OverdueDays { get; set; } = 0;

        /// <summary>逾期费用</summary>
        [SugarColumn(ColumnName = "overdue_fee", ColumnDescription = "逾期费用", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OverdueFee { get; set; } = 0;

    }
}




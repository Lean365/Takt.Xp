#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBook.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 图书实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Routine.Book
{
    /// <summary>
    /// 图书实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// 说明: 记录图书的基本信息，包括书名、作者、ISBN、分类等
    /// </remarks>
    [SugarTable("Takt_routine_book", "图书")]
    [SugarIndex("ix_book_isbn", nameof(Isbn), OrderByType.Asc, true)]
    [SugarIndex("ix_company_book", nameof(CompanyCode), OrderByType.Asc, nameof(Isbn), OrderByType.Asc, false)]
    [SugarIndex("ix_book_category", nameof(CategoryId), OrderByType.Asc, false)]
    [SugarIndex("ix_book_status", nameof(Status), OrderByType.Asc, false)]
    [SugarIndex("ix_book_author", nameof(Author), OrderByType.Asc, false)]
    public class TaktBook : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>图书编号</summary>
        [SugarColumn(ColumnName = "book_code", ColumnDescription = "图书编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BookCode { get; set; } = string.Empty;

        /// <summary>书名</summary>
        [SugarColumn(ColumnName = "book_name", ColumnDescription = "书名", Length = 200, ColumnDataType = "nvarchar", IsNullable = false)]
        public string BookName { get; set; } = string.Empty;

        /// <summary>作者</summary>
        [SugarColumn(ColumnName = "author", ColumnDescription = "作者", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Author { get; set; }

        /// <summary>ISBN</summary>
        [SugarColumn(ColumnName = "isbn", ColumnDescription = "ISBN", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Isbn { get; set; }

        /// <summary>图书分类ID</summary>
        [SugarColumn(ColumnName = "category_id", ColumnDescription = "图书分类ID", ColumnDataType = "bigint", IsNullable = false)]
        public long CategoryId { get; set; }

        /// <summary>出版社</summary>
        [SugarColumn(ColumnName = "publisher", ColumnDescription = "出版社", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Publisher { get; set; }

        /// <summary>出版日期</summary>
        [SugarColumn(ColumnName = "publish_date", ColumnDescription = "出版日期", ColumnDataType = "date", IsNullable = true)]
        public DateTime? PublishDate { get; set; }

        /// <summary>版次</summary>
        [SugarColumn(ColumnName = "edition", ColumnDescription = "版次", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Edition { get; set; }

        /// <summary>页数</summary>
        [SugarColumn(ColumnName = "pages", ColumnDescription = "页数", ColumnDataType = "int", IsNullable = true)]
        public int? Pages { get; set; }

        /// <summary>价格</summary>
        [SugarColumn(ColumnName = "price", ColumnDescription = "价格", ColumnDataType = "decimal(18,2)", IsNullable = false, DefaultValue = "0")]
        public decimal Price { get; set; } = 0;

        /// <summary>总数量</summary>
        [SugarColumn(ColumnName = "total_quantity", ColumnDescription = "总数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int TotalQuantity { get; set; } = 0;

        /// <summary>可借数量</summary>
        [SugarColumn(ColumnName = "available_quantity", ColumnDescription = "可借数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int AvailableQuantity { get; set; } = 0;

        /// <summary>已借数量</summary>
        [SugarColumn(ColumnName = "borrowed_quantity", ColumnDescription = "已借数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int BorrowedQuantity { get; set; } = 0;

        /// <summary>损坏数量</summary>
        [SugarColumn(ColumnName = "damaged_quantity", ColumnDescription = "损坏数量", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int DamagedQuantity { get; set; } = 0;

        /// <summary>存放位置</summary>
        [SugarColumn(ColumnName = "storage_location", ColumnDescription = "存放位置", Length = 100, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? StorageLocation { get; set; }

        /// <summary>图书简介</summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "图书简介", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Description { get; set; }

        /// <summary>封面图片</summary>
        [SugarColumn(ColumnName = "cover_image", ColumnDescription = "封面图片", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CoverImage { get; set; }

        /// <summary>关键词</summary>
        [SugarColumn(ColumnName = "keywords", ColumnDescription = "关键词", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? Keywords { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>状态(0=正常 1=停用 2=维护中)</summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
} 




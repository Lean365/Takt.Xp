#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktSalesPriceChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 销售价格变更记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Sales
{
    /// <summary>
    /// 销售价格变更记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_sales_price_change_log", "销售价格变更记录表")]
    [SugarIndex("ix_sales_price_code", nameof(PriceCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_price", nameof(CompanyCode), OrderByType.Asc, nameof(PriceCode), OrderByType.Asc, false)]
    public class TaktSalesPriceChangeLog : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>价格编号</summary>
        [SugarColumn(ColumnName = "price_code", ColumnDescription = "价格编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PriceCode { get; set; } = string.Empty;

        /// <summary>物料编号</summary>
        [SugarColumn(ColumnName = "material_code", ColumnDescription = "物料编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string MaterialCode { get; set; } = string.Empty;

        /// <summary>客户编号</summary>
        [SugarColumn(ColumnName = "customer_code", ColumnDescription = "客户编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? CustomerCode { get; set; }

        /// <summary>变更类型(1=新增 2=修改 3=删除)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>变更前价格</summary>
        [SugarColumn(ColumnName = "before_price", ColumnDescription = "变更前价格", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? BeforePrice { get; set; }

        /// <summary>变更后价格</summary>
        [SugarColumn(ColumnName = "after_price", ColumnDescription = "变更后价格", ColumnDataType = "decimal(15,2)", IsNullable = true)]
        public decimal? AfterPrice { get; set; }

        /// <summary>变更人</summary>
        [SugarColumn(ColumnName = "changed_by", ColumnDescription = "变更人", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ChangedBy { get; set; } = string.Empty;

        /// <summary>变更时间</summary>
        [SugarColumn(ColumnName = "change_time", ColumnDescription = "变更时间", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime ChangeTime { get; set; } = DateTime.Now;

        /// <summary>备注</summary>
        [SugarColumn(ColumnName = "change_remark", ColumnDescription = "备注", Length = 500, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? ChangeRemark { get; set; }

        /// <summary>排序号</summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;
    }
} 




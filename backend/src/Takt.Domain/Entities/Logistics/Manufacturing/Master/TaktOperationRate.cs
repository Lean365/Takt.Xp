#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktOperationRate.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 标准稼动率实体类 (生产稼动率基础记录)
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

using SqlSugar;
using System;
using System.ComponentModel.DataAnnotations;

namespace Takt.Domain.Entities.Logistics.Manufacturing.Master
{
    /// <summary>
    /// 标准稼动率实体类 (生产稼动率基础记录)
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// 参考: 精益生产管理标准
    /// </remarks>
    [SugarTable("Takt_logistics_manufacturing_operation_rate", "标准稼动率表")]
    [SugarIndex("ix_plant_code", nameof(PlantCode), OrderByType.Asc, false)]
    [SugarIndex("ix_financial_year", nameof(FinancialYear), OrderByType.Asc, false)]
    [SugarIndex("ix_operation_type", nameof(OperationType), OrderByType.Asc, false)]
    public class TaktOperationRate : TaktBaseEntity
    {
        
        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(ColumnName = "plant_code", ColumnDescription = "工厂代码", Length = 8, ColumnDataType = "nvarchar", IsNullable = false)]
        public string PlantCode { get; set; } = string.Empty;

        /// <summary>
        /// 财务年度
        /// </summary>
        [SugarColumn(ColumnName = "financial_year", ColumnDescription = "财务年度", Length = 4, ColumnDataType = "nvarchar", IsNullable = false)]
        public string FinancialYear { get; set; } = string.Empty;

        /// <summary>
        /// 稼动率类型(1=人员 2=SMT设备 3=测试设备 4=包装设备 5=其他)
        /// </summary>
        [SugarColumn(ColumnName = "operation_type", ColumnDescription = "稼动率类型", ColumnDataType = "int", IsNullable = false)]
        public int OperationType { get; set; }

        /// <summary>
        /// 稼动率(%)
        /// </summary>
        [SugarColumn(ColumnName = "operation_rate", ColumnDescription = "稼动率(%)", ColumnDataType = "decimal(5,2)", IsNullable = false, DefaultValue = "0")]
        public decimal OperationRate { get; set; } = 0;

        /// <summary>
        /// 生效日期
        /// </summary>
        [SugarColumn(ColumnName = "effective_date", ColumnDescription = "生效日期", ColumnDataType = "datetime", IsNullable = false)]
        public DateTime EffectiveDate { get; set; }

        /// <summary>
        /// 失效日期
        /// </summary>
        [SugarColumn(ColumnName = "expiry_date", ColumnDescription = "失效日期", ColumnDataType = "datetime", IsNullable = true, DefaultValue = "9999-12-31")]
        public DateTime? ExpiryDate { get; set; } = new DateTime(9999, 12, 31);

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int Status { get; set; } = 0;
    }
} 




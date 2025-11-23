#nullable enable

using SqlSugar;
using Takt.Domain.Entities.Identity;

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktServiceContractChangeLog.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07
// 版本号 : V0.0.1
// 描述    : 服务合同变更记录实体类
// 版权    : Copyright © 2024Takt(Claude AI). All rights reserved.
//===================================================================

namespace Takt.Domain.Entities.Logistics.Service
{
    /// <summary>
    /// 服务合同变更记录实体类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [SugarTable("Takt_logistics_service_contract_change_log", "服务合同变更记录表")]
    [SugarIndex("ix_service_contract_code", nameof(ContractCode), OrderByType.Asc, true)]
    [SugarIndex("ix_company_contract", nameof(CompanyCode), OrderByType.Asc, nameof(ContractCode), OrderByType.Asc, false)]
    public class TaktServiceContractChangeLog : TaktBaseEntity
    {
        /// <summary>公司代码</summary>
        [SugarColumn(ColumnName = "company_code", ColumnDescription = "公司代码", Length = 10, ColumnDataType = "nvarchar", IsNullable = false)]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>合同编号</summary>
        [SugarColumn(ColumnName = "contract_code", ColumnDescription = "合同编号", Length = 20, ColumnDataType = "nvarchar", IsNullable = false)]
        public string ContractCode { get; set; } = string.Empty;

        /// <summary>变更类型(1=新增 2=修改 3=删除)</summary>
        [SugarColumn(ColumnName = "change_type", ColumnDescription = "变更类型", ColumnDataType = "int", IsNullable = false)]
        public int ChangeType { get; set; }

        /// <summary>变更前内容</summary>
        [SugarColumn(ColumnName = "before_content", ColumnDescription = "变更前内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? BeforeContent { get; set; }

        /// <summary>变更后内容</summary>
        [SugarColumn(ColumnName = "after_content", ColumnDescription = "变更后内容", Length = 1000, ColumnDataType = "nvarchar", IsNullable = true)]
        public string? AfterContent { get; set; }

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




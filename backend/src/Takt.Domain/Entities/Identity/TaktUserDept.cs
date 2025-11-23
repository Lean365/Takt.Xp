//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktUserDept.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 用户部门关联实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 用户部门关联实体
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  [SugarTable("Takt_identity_user_dept", TableDescription = "用户部门关联表")]
  [SugarIndex("ix_user_dept", nameof(UserId), OrderByType.Asc, nameof(DeptId), OrderByType.Asc, true)]
  public class TaktUserDept : TaktBaseEntity
  {
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", ColumnDataType = "bigint", IsNullable = false)]
    public long DeptId { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UserId))]
    public TaktUser? User { get; set; }

    /// <summary>
    /// 部门导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(DeptId))]
    public TaktDept? Dept { get; set; }
  }
}




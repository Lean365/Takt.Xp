//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktUserPost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 用户岗位关联实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using SqlSugar;

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 用户岗位关联实体
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  [SugarTable("Takt_identity_user_post", TableDescription = "用户岗位关联表")]
  [SugarIndex("ix_user_post", nameof(UserId), OrderByType.Asc, nameof(PostId), OrderByType.Asc, true)]
  public class TaktUserPost : TaktBaseEntity
  {
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = false)]
    public long PostId { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(UserId))]
    public TaktUser? User { get; set; }

    /// <summary>
    /// 岗位导航属性
    /// </summary>
    [Navigate(NavigateType.OneToOne, nameof(PostId))]
    public TaktPost? Post { get; set; }
  }
}




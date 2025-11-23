#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktPost.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 岗位实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 岗位实体
  /// </summary>
  [SugarTable("Takt_identity_post", "岗位表")]
  [SugarIndex("ix_post_code", nameof(PostCode), OrderByType.Asc)]
  public class TaktPost : TaktBaseEntity
  {
    /// <summary>
    /// 岗位编码
    /// </summary>
    [SugarColumn(ColumnName = "post_code", ColumnDescription = "岗位编码", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string PostCode { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [SugarColumn(ColumnName = "post_name", ColumnDescription = "岗位名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false, DefaultValue = "")]
    public string PostName { get; set; } = string.Empty;

    /// <summary>
    /// 职级
    /// </summary>
    [SugarColumn(ColumnName = "rank", ColumnDescription = "职级", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Rank { get; set; } = 0;

    /// <summary>
    /// 用户数
    /// </summary>
    [SugarColumn(ColumnName = "user_count", ColumnDescription = "用户数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserCount { get; set; } = 0;

    /// <summary>
    /// 显示顺序
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "显示顺序", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int OrderNum { get; set; } = 0;

    /// <summary>
    /// 岗位状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "post_status", ColumnDescription = "岗位状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int PostStatus { get; set; } = 0;

    /// <summary>
    /// 用户岗位关联
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserPost.PostId))]
    public List<TaktUserPost>? UserPosts { get; set; }
  }
}




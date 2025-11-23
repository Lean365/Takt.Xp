#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 身份认证
// 命名空间 : Takt.Domain.Entities.Identity
// 文件名 : TaktUser.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 用户实体类
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

using System.ComponentModel.DataAnnotations;
using SqlSugar;

namespace Takt.Domain.Entities.Identity
{
  /// <summary>
  /// 用户实体
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  [SugarTable("Takt_identity_user", TableDescription = "用户表")]
  [SugarIndex("ix_user_name", nameof(UserName), OrderByType.Asc, true)]
  [SugarIndex("ix_email", nameof(Email), OrderByType.Asc, true)]
  [SugarIndex("ix_phone", nameof(PhoneNumber), OrderByType.Asc, true)]
  public class TaktUser : TaktBaseEntity
  {
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]

    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型（0系统用户 1普通用户 2管理员 3OAuth用户）
    /// </summary>
    [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型", ColumnDataType = "int", IsNullable = false, DefaultValue = "1")]
    public int UserType { get; set; } = 1;

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]

    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 盐值
    /// </summary>
    [SugarColumn(ColumnName = "salt", ColumnDescription = "盐值", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]

    public string Salt { get; set; } = string.Empty;

    /// <summary>
    /// 密码迭代次数
    /// </summary>
    [SugarColumn(ColumnName = "iterations", ColumnDescription = "密码迭代次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "600000")]
    public int Iterations { get; set; } = 600000;

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名称", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [SugarColumn(ColumnName = "real_name", ColumnDescription = "真实姓名", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 全名
    /// </summary>
    [SugarColumn(ColumnName = "full_name", ColumnDescription = "全名", Length = 100, ColumnDataType = "nvarchar", IsNullable = false)]
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 50, ColumnDataType = "nvarchar", IsNullable = false)]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 手机号
    /// </summary>
    [SugarColumn(ColumnName = "phone_number", ColumnDescription = "手机号", Length = 11, ColumnDataType = "nvarchar", IsNullable = false)]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// 性别（0未知 1男 2女）
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int Gender { get; set; } = 0;

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? Avatar { get; set; }


    /// <summary>
    /// 用户状态（0正常 1停用）
    /// </summary>
    [SugarColumn(ColumnName = "user_status", ColumnDescription = "用户状态", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int UserStatus { get; set; } = 0;

    /// <summary>
    /// 最后修改密码时间
    /// </summary>
    [SugarColumn(ColumnName = "last_password_change_time", ColumnDescription = "最后修改密码时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LastPasswordChangeTime { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    [SugarColumn(ColumnName = "lock_end_time", ColumnDescription = "锁定结束时间", ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? LockEndTime { get; set; }

    /// <summary>
    /// 锁定原因
    /// </summary>
    [SugarColumn(ColumnName = "lock_reason", ColumnDescription = "锁定原因", Length = 200, ColumnDataType = "nvarchar", IsNullable = true)]
    public string? LockReason { get; set; }
    /// <summary>
    /// 是否锁定（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_lock", ColumnDescription = "是否锁定", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int IsLock { get; set; } = 0;

    /// <summary>
    /// 错误次数限制
    /// </summary>
    [SugarColumn(ColumnName = "error_limit", ColumnDescription = "错误次数限制", ColumnDataType = "int", IsNullable = false, DefaultValue = "5")]
    public int ErrorLimit { get; set; } = 5;

    /// <summary>
    /// 登录次数
    /// </summary>
    [SugarColumn(ColumnName = "login_count", ColumnDescription = "登录次数", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
    public int LoginCount { get; set; } = 0;




    /// <summary>
    /// 用户角色关联列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserRole.UserId))]
    public List<TaktUserRole>? UserRoles { get; set; }

    /// <summary>
    /// 用户部门关联列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserDept.UserId))]
    public List<TaktUserDept>? UserDepts { get; set; }

    /// <summary>
    /// 用户岗位关联列表
    /// </summary>
    [Navigate(NavigateType.OneToMany, nameof(TaktUserPost.UserId))]
    public List<TaktUserPost>? UserPosts { get; set; }


  }
}




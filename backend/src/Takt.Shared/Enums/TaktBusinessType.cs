//===================================================================
// 项目名 : Takt.Xp
// 模块名 : 枚举
// 命名空间 : Takt.Shared.Enums
// 文件名 : TaktBusinessType.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 11:30
// 版本号 : V.0.0.1
// 描述    : 业务操作类型枚举
//
// 版权    : Copyright © 2025 Takt All rights reserved.
// 
// 免责声明: 基于 MIT License，按原样提供，开发者不承担任何后果和责任。
//===================================================================

namespace Takt.Shared.Enums
{
  /// <summary>
  /// 业务操作类型枚举
  /// </summary>
  /// <remarks>
  /// 创建者:Takt(Claude AI)
  /// 创建时间: 2024-01-16
  /// </remarks>
  public enum TaktBusinessType
  {
    /// <summary>
    /// 其他
    /// </summary>
    Other = 0,

    /// <summary>
    /// 新增
    /// </summary>
    Insert = 1,

    /// <summary>
    /// 修改
    /// </summary>
    Update = 2,

    /// <summary>
    /// 删除
    /// </summary>
    Delete = 3,

    /// <summary>
    /// 授权
    /// </summary>
    Grant = 4,

    /// <summary>
    /// 导出
    /// </summary>
    Export = 5,

    /// <summary>
    /// 导入
    /// </summary>
    Import = 6,

    /// <summary>
    /// 强退
    /// </summary>
    ForceLogout = 7,

    /// <summary>
    /// 生成代码
    /// </summary>
    GenCode = 8,

    /// <summary>
    /// 清空数据
    /// </summary>
    Clean = 9,

    /// <summary>
    /// 审核
    /// </summary>
    Audit = 10,

    /// <summary>
    /// 发布
    /// </summary>
    Publish = 11,

    /// <summary>
    /// 撤销
    /// </summary>
    Revoke = 12
  }
}





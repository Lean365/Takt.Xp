//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIdentityDBSeed.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 认证数据库种子数据初始化类 - 使用协调器模式
//===================================================================

using Takt.Shared.Exceptions;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds.Auth;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 认证数据库种子数据初始化类
/// </summary>
/// <remarks>
/// 更新: 2024-12-01 - 使用协调器模式统一管理认证种子数据
/// </remarks>
public class TaktIdentityDBSeed
{
    private readonly ITaktLogger _logger;
    private readonly TaktDbSeedIdentityCoordinator _identityCoordinator;
    private readonly TaktDbSeedMenuCoordinator _menuCoordinator;
    private readonly TaktDbSeedDeptCoordinator _deptCoordinator;
    private readonly TaktDbSeedRelationCoordinator _relationCoordinator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktIdentityDBSeed(
        ITaktLogger logger,
        TaktDbSeedIdentityCoordinator identityCoordinator,
        TaktDbSeedMenuCoordinator menuCoordinator,
        TaktDbSeedDeptCoordinator deptCoordinator,
        TaktDbSeedRelationCoordinator relationCoordinator)
    {
        _logger = logger;
        _identityCoordinator = identityCoordinator;
        _menuCoordinator = menuCoordinator;
        _deptCoordinator = deptCoordinator;
        _relationCoordinator = relationCoordinator;
    }

    /// <summary>
    /// 初始化认证数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化认证数据库种子数据...");

            // 1. 使用认证协调器初始化角色、用户、岗位数据
            var identityResult = await _identityCoordinator.InitializeAllIdentityDataAsync();
            _logger.Info($"认证数据初始化完成！新增: {identityResult.GetTotalInsertCount()} 条, 更新: {identityResult.GetTotalUpdateCount()} 条");

            // 2. 使用菜单协调器初始化菜单数据
            var (menuInsertCount, menuUpdateCount) = await _menuCoordinator.InitializeMenuAsync();
            _logger.Info($"菜单数据初始化完成！新增: {menuInsertCount} 条, 更新: {menuUpdateCount} 条");

            // 3. 使用部门协调器初始化部门数据
            var (deptInsertCount, deptUpdateCount) = await _deptCoordinator.InitializeDeptAsync();
            _logger.Info($"部门数据初始化完成！新增: {deptInsertCount} 条, 更新: {deptUpdateCount} 条");

            // 4. 使用关系协调器初始化关系数据
            var (relationInsertCount, relationUpdateCount) = await _relationCoordinator.InitializeRelationsAsync();
            _logger.Info($"关系数据初始化完成！新增: {relationInsertCount} 条, 更新: {relationUpdateCount} 条");

            // 计算总计
            var totalInsert = identityResult.GetTotalInsertCount() + menuInsertCount + deptInsertCount + relationInsertCount;
            var totalUpdate = identityResult.GetTotalUpdateCount() + menuUpdateCount + deptUpdateCount + relationUpdateCount;

            _logger.Info($"认证数据库种子数据初始化完成！总计: 新增 {totalInsert} 条, 更新 {totalUpdate} 条");
        }
        catch (Exception ex)
        {
            _logger.Error($"认证数据库种子数据初始化失败: {ex.Message}", ex);
            throw new TaktException("认证数据库种子数据初始化失败", ex);
        }
    }
}





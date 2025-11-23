//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowDbSeed.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 工作流数据库种子数据初始化类
//===================================================================

using Takt.Shared.Exceptions;
using Takt.Infrastructure.Data.Contexts;
using Takt.Infrastructure.Data.Seeds.Workflow;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 工作流数据库种子数据初始化类
/// </summary>
public class TaktWorkflowDbSeed
{
    private readonly TaktWorkflowDbContext _context;
    private readonly ITaktLogger _logger;
    private readonly TaktDbSeedWorkflowCoordinator _workflowCoordinator;

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktWorkflowDbSeed(
        TaktWorkflowDbContext context,
        ITaktLogger logger,
        TaktDbSeedWorkflowCoordinator workflowCoordinator)
    {
        _context = context;
        _logger = logger;
        _workflowCoordinator = workflowCoordinator;
    }

    /// <summary>
    /// 初始化工作流数据库种子数据
    /// </summary>
    public async Task InitializeAsync()
    {
        try
        {
            _logger.Info("开始初始化工作流数据库种子数据...");

            // 使用工作流协调器初始化所有工作流相关数据
            await _workflowCoordinator.InitializeAllWorkflowsAsync();
            _logger.Info("工作流数据初始化完成");

            _logger.Info("工作流数据库种子数据初始化完成");
        }
        catch (Exception ex)
        {
            _logger.Error($"工作流数据库种子数据初始化失败: {ex.Message}", ex);
            throw new TaktException("工作流数据库种子数据初始化失败", ex);
        }
    }
}





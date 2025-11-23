#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktWorkflowEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流引擎接口
//===================================================================

using Takt.Application.Dtos.Workflow;

namespace Takt.Application.Services.Workflow.Engine
{
    /// <summary>
    /// 工作流引擎接口
    /// </summary>
    public interface ITaktWorkflowEngine
    {
        /// <summary>
        /// 启动工作流实例
        /// </summary>
        /// <param name="dto">启动参数</param>
        /// <returns>工作流实例ID</returns>
        Task<long> StartAsync(TaktWorkflowStartDto dto);

        /// <summary>
        /// 暂停工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">暂停原因</param>
        Task SuspendAsync(long instanceId, string reason = "手动暂停");

        /// <summary>
        /// 恢复工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        Task ResumeAsync(long instanceId);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="reason">终止原因</param>
        Task TerminateAsync(long instanceId, string reason);

        /// <summary>
        /// 审批工作流
        /// </summary>
        /// <param name="dto">审批参数</param>
        /// <returns>审批结果</returns>
        Task<bool> ApproveAsync(TaktWorkflowApproveDto dto);

        /// <summary>
        /// 获取工作流实例状态
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流实例状态</returns>
        Task<TaktInstanceStatusDto> GetStatusAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例可用转换列表
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>可用转换列表</returns>
        Task<List<TaktInstanceTransDto>> GetAvailableTransitionsAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例当前节点信息
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>当前节点信息</returns>
        Task<TaktNodeDto?> GetCurrentNodeAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>工作流变量字典</returns>
        Task<Dictionary<string, object>> GetVariablesAsync(long instanceId);

        /// <summary>
        /// 设置工作流实例变量
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <param name="variables">变量字典</param>
        Task SetVariablesAsync(long instanceId, Dictionary<string, object> variables);

        /// <summary>
        /// 获取工作流实例流转历史
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>流转历史列表</returns>
        Task<List<TaktInstanceTransDto>> GetHistoryAsync(long instanceId);

        /// <summary>
        /// 获取工作流实例操作记录
        /// </summary>
        /// <param name="instanceId">工作流实例ID</param>
        /// <returns>操作记录列表</returns>
        Task<List<TaktInstanceOperDto>> GetOperationsAsync(long instanceId);

        // 转换历史相关方法

        /// <summary>
        /// 获取转换历史列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>转换历史列表</returns>
        Task<TaktPagedResult<TaktInstanceTransDto>> GetTransitionListAsync(TaktInstanceTransQueryDto query);

        /// <summary>
        /// 获取转换历史详情
        /// </summary>
        /// <param name="transitionId">转换ID</param>
        /// <returns>转换历史详情</returns>
        Task<TaktInstanceTransDto?> GetTransitionAsync(string transitionId);

        /// <summary>
        /// 拒绝工作流
        /// </summary>
        /// <param name="dto">拒绝参数</param>
        /// <returns>拒绝结果</returns>
        Task<bool> RejectAsync(TaktWorkflowRejectDto dto);

        /// <summary>
        /// 手动触发节点转换
        /// </summary>
        /// <param name="dto">转换参数</param>
        /// <returns>转换结果</returns>
        Task<bool> TransitAsync(TaktWorkflowTransitDto dto);

        /// <summary>
        /// 评估条件表达式
        /// </summary>
        /// <param name="dto">条件评估参数</param>
        /// <returns>评估结果</returns>
        Task<TaktConditionEvaluateResultDto> EvaluateConditionAsync(TaktConditionEvaluateDto dto);

        /// <summary>
        /// 获取工作流实例待办任务
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="query">查询条件</param>
        /// <returns>待办任务列表</returns>
        Task<List<TaktWorkflowTaskDto>> GetTodoTasksAsync(long userId, TaktWorkflowTodoQueryDto query);

        /// <summary>
        /// 获取工作流实例已办任务
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="query">查询条件</param>
        /// <returns>已办任务列表</returns>
        Task<List<TaktWorkflowTaskDto>> GetDoneTasksAsync(long userId, TaktWorkflowDoneQueryDto query);
    }
}



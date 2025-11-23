#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktExpressionEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流表达式引擎接口
//===================================================================

namespace Takt.Application.Services.Workflow.Engine.Expressions
{
    /// <summary>
    /// 工作流表达式引擎接口
    /// </summary>
    public interface ITaktExpressionEngine
    {
        /// <summary>
        /// 解析并执行表达式
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>表达式执行结果</returns>
        Task<bool> EvaluateAsync(string expression, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 验证表达式语法
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>是否有效</returns>
        bool Validate(string expression);

        /// <summary>
        /// 获取表达式中使用的变量名列表
        /// </summary>
        /// <param name="expression">条件表达式</param>
        /// <returns>变量名列表</returns>
        IEnumerable<string> GetVariableNames(string expression);

        /// <summary>
        /// 执行数学表达式
        /// </summary>
        /// <param name="expression">数学表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>计算结果</returns>
        Task<object> EvaluateMathAsync(string expression, Dictionary<string, object>? variables = null);

        /// <summary>
        /// 执行字符串表达式
        /// </summary>
        /// <param name="expression">字符串表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>字符串结果</returns>
        Task<string> EvaluateStringAsync(string expression, Dictionary<string, object>? variables = null);
    }
}


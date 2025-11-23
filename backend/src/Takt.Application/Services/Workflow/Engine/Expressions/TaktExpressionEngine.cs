#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExpressionEngine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流表达式引擎实现
//===================================================================

using System.Text.RegularExpressions;
using System.Linq.Expressions;

namespace Takt.Application.Services.Workflow.Engine.Expressions
{
    /// <summary>
    /// 工作流表达式引擎实现
    /// </summary>
    public class TaktExpressionEngine : ITaktExpressionEngine
    {
        private readonly ITaktLogger _logger;
        private readonly Dictionary<string, object> _compiledExpressions = new();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        public TaktExpressionEngine(ITaktLogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 执行表达式
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>执行结果</returns>
        public async Task<bool> EvaluateAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrEmpty(expression))
                    return true;

                variables ??= new Dictionary<string, object>();

                // 预处理表达式
                var processedExpression = PreprocessExpression(expression, variables);
                
                // 使用DynamicLinq评估表达式
                var result = await Task.Run(() => EvaluateDynamicExpression(processedExpression, variables));
                
                _logger.Debug($"表达式评估: {expression} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"表达式执行失败: {ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 验证表达式语法
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>是否有效</returns>
        public bool Validate(string expression)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                    return true;

                // 基本语法检查
                if (!ValidateBasicSyntax(expression))
                    return false;

                // 尝试编译表达式
                var testVariables = new Dictionary<string, object>
                {
                    { "test", 1 },
                    { "testString", "test" },
                    { "testBool", true }
                };

                var processedExpression = PreprocessExpression(expression, testVariables);
                EvaluateDynamicExpression(processedExpression, testVariables);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取表达式中的变量名
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <returns>变量名列表</returns>
        public IEnumerable<string> GetVariableNames(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression))
                return Enumerable.Empty<string>();

            try
            {
                var variables = new List<string>();
                
                // 支持 ${variable} 格式
                var pattern1 = @"\$\{([^}]+)\}";
                var matches1 = Regex.Matches(expression, pattern1);
                foreach (Match match in matches1)
                {
                    var variable = match.Groups[1].Value;
                    if (!IsReservedKeyword(variable) && !variables.Contains(variable))
                    {
                        variables.Add(variable);
                    }
                }
                
                // 支持直接变量名格式
                var pattern2 = @"\b([a-zA-Z_][a-zA-Z0-9_]*)\b";
                var matches2 = Regex.Matches(expression, pattern2);
                foreach (Match match in matches2)
                {
                    var variable = match.Groups[1].Value;
                    if (!IsReservedKeyword(variable) && !variables.Contains(variable))
                    {
                        variables.Add(variable);
                    }
                }
                
                return variables.Distinct();
            }
            catch
            {
                return Enumerable.Empty<string>();
            }
        }

        /// <summary>
        /// 执行数学表达式
        /// </summary>
        /// <param name="expression">数学表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>计算结果</returns>
        public async Task<object> EvaluateMathAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                    return 0;

                variables ??= new Dictionary<string, object>();

                // 预处理表达式
                var processedExpression = PreprocessExpression(expression, variables);
                
                // 使用DynamicLinq评估数学表达式
                var result = await Task.Run(() => EvaluateDynamicMathExpression(processedExpression, variables));
                
                _logger.Debug($"数学表达式评估: {expression} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"数学表达式执行失败: {ex.Message}", ex);
                return 0;
            }
        }

        /// <summary>
        /// 执行字符串表达式
        /// </summary>
        /// <param name="expression">字符串表达式</param>
        /// <param name="variables">变量字典</param>
        /// <returns>字符串结果</returns>
        public async Task<string> EvaluateStringAsync(string expression, Dictionary<string, object>? variables = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(expression))
                    return string.Empty;

                variables ??= new Dictionary<string, object>();

                // 预处理表达式
                var processedExpression = PreprocessExpression(expression, variables);
                
                // 使用DynamicLinq评估字符串表达式
                var result = await Task.Run(() => EvaluateDynamicStringExpression(processedExpression, variables));
                
                _logger.Debug($"字符串表达式评估: {expression} = {result}");
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"字符串表达式执行失败: {ex.Message}", ex);
                return string.Empty;
            }
        }

        #region 私有方法

        /// <summary>
        /// 预处理表达式
        /// </summary>
        private string PreprocessExpression(string expression, Dictionary<string, object> variables)
        {
            var processed = expression;

            // 处理 ${variable} 格式的变量
            foreach (var kvp in variables)
            {
                var placeholder = $"${{{kvp.Key}}}";
                if (processed.Contains(placeholder))
                {
                    if (kvp.Value is string strValue)
                    {
                        processed = processed.Replace(placeholder, $"\"{strValue}\"");
                    }
                    else
                    {
                        processed = processed.Replace(placeholder, kvp.Value?.ToString() ?? "null");
                    }
                }
            }

            // 处理直接变量名
            foreach (var kvp in variables)
            {
                if (kvp.Value is string strValue)
                {
                    processed = processed.Replace(kvp.Key, $"\"{strValue}\"");
                }
            }

            // 处理布尔值
            processed = processed.Replace("true", "true", StringComparison.OrdinalIgnoreCase);
            processed = processed.Replace("false", "false", StringComparison.OrdinalIgnoreCase);

            return processed;
        }

        /// <summary>
        /// 使用.NET表达式评估表达式
        /// </summary>
        private bool EvaluateDynamicExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 使用简单的表达式解析器
                return EvaluateSimpleExpression(expression, variables);
            }
            catch
            {
                // 如果解析失败，返回false
                return false;
            }
        }

        /// <summary>
        /// 使用.NET表达式评估数学表达式
        /// </summary>
        private object EvaluateDynamicMathExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 使用简单的数学表达式解析器
                return EvaluateSimpleMathExpression(expression, variables);
            }
            catch
            {
                // 如果解析失败，返回0
                return 0;
            }
        }

        /// <summary>
        /// 使用.NET表达式评估字符串表达式
        /// </summary>
        private string EvaluateDynamicStringExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 使用简单的字符串表达式解析器
                return EvaluateSimpleStringExpression(expression, variables);
            }
            catch
            {
                // 如果解析失败，返回空字符串
                return string.Empty;
            }
        }

        /// <summary>
        /// 简单表达式评估（增强版）
        /// </summary>
        private bool EvaluateSimpleExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 支持逻辑运算符 && 和 ||
                if (expression.Contains("&&"))
                {
                    var parts = expression.Split("&&", StringSplitOptions.RemoveEmptyEntries);
                    return parts.All(part => EvaluateSimpleExpression(part.Trim(), variables));
                }
                
                if (expression.Contains("||"))
                {
                    var parts = expression.Split("||", StringSplitOptions.RemoveEmptyEntries);
                    return parts.Any(part => EvaluateSimpleExpression(part.Trim(), variables));
                }

                // 支持括号
                if (expression.Contains("(") && expression.Contains(")"))
                {
                    var startIndex = expression.LastIndexOf('(');
                    var endIndex = expression.IndexOf(')', startIndex);
                    if (startIndex >= 0 && endIndex > startIndex)
                    {
                        var innerExpression = expression.Substring(startIndex + 1, endIndex - startIndex - 1);
                        var innerResult = EvaluateSimpleExpression(innerExpression, variables);
                        var newExpression = expression.Substring(0, startIndex) + innerResult.ToString().ToLower() + expression.Substring(endIndex + 1);
                        return EvaluateSimpleExpression(newExpression, variables);
                    }
                }

                // 支持比较表达式
                var operators = new[] { "==", "!=", ">=", "<=", ">", "<" };
                foreach (var op in operators)
                {
                    if (expression.Contains(op))
                    {
                        var parts = expression.Split(new[] { op }, StringSplitOptions.None);
                        if (parts.Length == 2)
                        {
                            var left = parts[0].Trim();
                            var right = parts[1].Trim();

                            var leftValue = GetValue(left, variables);
                            var rightValue = GetValue(right, variables);

                            if (leftValue != null && rightValue != null)
                            {
                                return CompareValues(leftValue, rightValue, op);
                            }
                        }
                    }
                }

                // 支持布尔变量
                var trimmedExpression = expression.Trim();
                if (variables.TryGetValue(trimmedExpression, out var boolValue))
                {
                    return boolValue is bool boolResult && boolResult;
                }

                // 支持直接布尔值
                if (bool.TryParse(trimmedExpression, out var directBool))
                {
                    return directBool;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 简单数学表达式评估（备用方案）
        /// </summary>
        private object EvaluateSimpleMathExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 支持基本的数学运算
                var operators = new[] { "+", "-", "*", "/", "%" };
                foreach (var op in operators)
                {
                    if (expression.Contains(op))
                    {
                        var parts = expression.Split(new[] { op }, StringSplitOptions.None);
                        if (parts.Length == 2)
                        {
                            var left = parts[0].Trim();
                            var right = parts[1].Trim();

                            if (variables.TryGetValue(left, out var leftValue) && 
                                variables.TryGetValue(right, out var rightValue))
                            {
                                return CalculateMathValues(leftValue, rightValue, op);
                            }
                        }
                    }
                }

                // 支持直接数值
                if (variables.TryGetValue(expression.Trim(), out var value))
                {
                    return value;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 简单字符串表达式评估（备用方案）
        /// </summary>
        private string EvaluateSimpleStringExpression(string expression, Dictionary<string, object> variables)
        {
            try
            {
                // 支持字符串连接
                if (expression.Contains("+"))
                {
                    var parts = expression.Split('+');
                    var result = string.Empty;
                    
                    foreach (var part in parts)
                    {
                        var trimmedPart = part.Trim();
                        if (variables.TryGetValue(trimmedPart, out var value))
                        {
                            result += value?.ToString() ?? string.Empty;
                        }
                        else if (trimmedPart.StartsWith("\"") && trimmedPart.EndsWith("\""))
                        {
                            result += trimmedPart.Substring(1, trimmedPart.Length - 2);
                        }
                    }
                    
                    return result;
                }

                // 支持直接字符串变量
                if (variables.TryGetValue(expression.Trim(), out var stringValue))
                {
                    return stringValue?.ToString() ?? string.Empty;
                }

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取值（支持变量和直接值）
        /// </summary>
        private object? GetValue(string value, Dictionary<string, object> variables)
        {
            // 如果是变量名，从变量字典中获取
            if (variables.TryGetValue(value, out var variableValue))
            {
                return variableValue;
            }

            // 尝试解析为直接值
            if (TryParseValue(value, out var parsedValue))
            {
                return parsedValue;
            }

            return null;
        }

        /// <summary>
        /// 尝试解析值
        /// </summary>
        private bool TryParseValue(string value, out object? result)
        {
            result = null;
            
            if (int.TryParse(value, out var intValue))
            {
                result = intValue;
                return true;
            }
            
            if (decimal.TryParse(value, out var decimalValue))
            {
                result = decimalValue;
                return true;
            }
            
            if (bool.TryParse(value, out var boolValue))
            {
                result = boolValue;
                return true;
            }
            
            if (value.StartsWith("\"") && value.EndsWith("\""))
            {
                result = value.Substring(1, value.Length - 2);
                return true;
            }
            
            result = value;
            return true;
        }

        /// <summary>
        /// 比较值
        /// </summary>
        private bool CompareValues(object left, object right, string op)
        {
            if (left == null || right == null)
                return false;

            try
            {
                var leftComparable = Convert.ChangeType(left, typeof(IComparable));
                var rightComparable = Convert.ChangeType(right, left.GetType());

                var comparison = ((IComparable)leftComparable).CompareTo(rightComparable);

                return op switch
                {
                    "==" => comparison == 0,
                    "!=" => comparison != 0,
                    ">=" => comparison >= 0,
                    "<=" => comparison <= 0,
                    ">" => comparison > 0,
                    "<" => comparison < 0,
                    _ => false
                };
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 计算数学值
        /// </summary>
        private object CalculateMathValues(object left, object right, string op)
        {
            try
            {
                if (left is decimal leftDecimal && right is decimal rightDecimal)
                {
                    return op switch
                    {
                        "+" => leftDecimal + rightDecimal,
                        "-" => leftDecimal - rightDecimal,
                        "*" => leftDecimal * rightDecimal,
                        "/" => rightDecimal != 0 ? leftDecimal / rightDecimal : 0,
                        "%" => rightDecimal != 0 ? leftDecimal % rightDecimal : 0,
                        _ => 0
                    };
                }

                if (left is int leftInt && right is int rightInt)
                {
                    return op switch
                    {
                        "+" => leftInt + rightInt,
                        "-" => leftInt - rightInt,
                        "*" => leftInt * rightInt,
                        "/" => rightInt != 0 ? leftInt / rightInt : 0,
                        "%" => rightInt != 0 ? leftInt % rightInt : 0,
                        _ => 0
                    };
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 验证基本语法
        /// </summary>
        private bool ValidateBasicSyntax(string expression)
        {
            // 检查括号匹配
            var parentheses = 0;
            foreach (var c in expression)
            {
                if (c == '(') parentheses++;
                if (c == ')') parentheses--;
                if (parentheses < 0) return false;
            }
            if (parentheses != 0) return false;

            // 检查基本操作符
            var validOperators = new[] { "==", "!=", ">=", "<=", ">", "<", "&&", "||", "!", "(", ")", "+", "-", "*", "/", "%" };
            var hasValidOperator = validOperators.Any(op => expression.Contains(op));
            
            return hasValidOperator;
        }

        /// <summary>
        /// 检查是否为保留关键字
        /// </summary>
        private bool IsReservedKeyword(string word)
        {
            var reservedKeywords = new[]
            {
                "true", "false", "null", "and", "or", "not", "if", "else", "for", "while", "do", "switch", "case"
            };
            
            return reservedKeywords.Contains(word.ToLower());
        }

        #endregion
    }
}


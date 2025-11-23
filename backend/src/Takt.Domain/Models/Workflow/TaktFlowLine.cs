#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : FlowLine.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流连线模型 - 基于 OpenAuth.Net 标准
//===================================================================

using Newtonsoft.Json.Linq;

namespace Takt.Domain.Models.Workflow
{
    /// <summary>
    /// 工作流连线模型
    /// </summary>
    public class TaktFlowLine
    {
        /// <summary>
        /// 连线ID
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// 连线标签
        /// </summary>
        public string Label { get; set; } = string.Empty;

        /// <summary>
        /// 连线类型
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// 起始节点ID
        /// </summary>
        public string From { get; set; } = string.Empty;

        /// <summary>
        /// 目标节点ID
        /// </summary>
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// 连线名称
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 是否为虚线
        /// </summary>
        public bool Dash { get; set; }

        /// <summary>
        /// 分支条件列表
        /// </summary>
        public List<TaktDataCompare>? Compares { get; set; }

        /// <summary>
        /// 判断条件是否满足
        /// </summary>
        /// <param name="formDataJson">表单数据（JSON对象）</param>
        /// <returns>是否满足条件</returns>
        public bool Compare(JObject formDataJson)
        {
            if (Compares == null || !Compares.Any())
            {
                return true; // 没有条件，默认通过
            }

            bool result = true;

            // 将 formDataJson 中的所有 key 转为小写（兼容处理）
            var properties = formDataJson.Properties().ToList();
            foreach (var property in properties)
            {
                if (property.Name != property.Name.ToLower())
                {
                    formDataJson[property.Name.ToLower()] = property.Value;
                    formDataJson.Remove(property.Name);
                }
            }

            foreach (var compare in Compares)
            {
                object? objVal = formDataJson.GetValue(compare.FieldName.ToLower());
                string? fieldVal = null;
                List<string> fieldVals = new List<string>();

                // Checkbox 类型特殊处理
                if (compare.FieldType == "checkbox")
                {
                    if (objVal is JArray jArray)
                    {
                        var tempvals = jArray.ToObject<List<string>>();
                        if (tempvals != null && tempvals.Any())
                        {
                            fieldVals.AddRange(tempvals);
                        }
                    }
                }
                else
                {
                    fieldVal = objVal?.ToString();
                }

                // 判断是否为空值操作符
                if (compare.Operation == TaktDataCompare.IsNull || compare.Operation == TaktDataCompare.IsNotNull)
                {
                    var fieldExists = objVal != null;
                    var fieldEmpty = !fieldExists || string.IsNullOrWhiteSpace(objVal?.ToString());

                    if (compare.Operation == TaktDataCompare.IsNull)
                    {
                        result &= fieldEmpty;
                    }
                    else
                    {
                        result &= !fieldEmpty;
                    }
                    continue;
                }

                // 确保字段存在且不为空
                if (objVal == null)
                {
                    result = false;
                    continue;
                }

                // Checkbox 类型特殊处理
                if (compare.FieldType == "checkbox")
                {
                // 针对 checkbox 的 IN 和 NOT IN 操作
                if (compare.Operation == TaktDataCompare.In || compare.Operation == TaktDataCompare.NotIn)
                    {
                        if (compare.ValueList == null || compare.ValueList.Length == 0)
                        {
                            result = false;
                            continue;
                        }

                        bool hasIntersection = fieldVals.Any(v => compare.ValueList.Contains(v));
                        result &= compare.Operation == TaktDataCompare.In ? hasIntersection : !hasIntersection;
                        continue;
                    }

                    // 针对 checkbox 的相等和不等操作
                    if (compare.Operation == TaktDataCompare.Equal)
                    {
                        result &= fieldVals.Contains(compare.Value);
                        continue;
                    }
                    else if (compare.Operation == TaktDataCompare.NotEqual)
                    {
                        result &= !fieldVals.Contains(compare.Value);
                        continue;
                    }

                    // 其他操作不适用于 checkbox
                    result = false;
                    continue;
                }

                // 范围操作符处理
                if (compare.Operation == TaktDataCompare.In || compare.Operation == TaktDataCompare.NotIn)
                {
                    if (compare.ValueList == null || compare.ValueList.Length == 0)
                    {
                        result = false;
                        continue;
                    }

                    bool inList = compare.ValueList.Contains(fieldVal);
                    result &= compare.Operation == TaktDataCompare.In ? inList : !inList;
                    continue;
                }

                // Between 操作符处理
                if (compare.Operation == TaktDataCompare.Between)
                {
                    if (compare.ValueRange == null || compare.ValueRange.Length != 2)
                    {
                        result = false;
                        continue;
                    }

                    bool isDecimal = decimal.TryParse(fieldVal, out decimal fieldDecimal);
                    if (isDecimal)
                    {
                        decimal min = decimal.Parse(compare.ValueRange[0]);
                        decimal max = decimal.Parse(compare.ValueRange[1]);
                        result &= fieldDecimal >= min && fieldDecimal <= max;
                    }
                    else
                    {
                        string min = compare.ValueRange[0];
                        string max = compare.ValueRange[1];
                        result &= string.Compare(fieldVal, min, false) >= 0 && string.Compare(fieldVal, max, false) <= 0;
                    }
                    continue;
                }

                // 文本特殊操作符处理
                if (compare.Operation == TaktDataCompare.Like || compare.Operation == TaktDataCompare.NotLike ||
                    compare.Operation == TaktDataCompare.StartWith || compare.Operation == TaktDataCompare.EndWith)
                {
                    switch (compare.Operation)
                    {
                        case TaktDataCompare.Like:
                            result &= fieldVal != null && fieldVal.Contains(compare.Value);
                            break;
                        case TaktDataCompare.NotLike:
                            result &= fieldVal != null && !fieldVal.Contains(compare.Value);
                            break;
                        case TaktDataCompare.StartWith:
                            result &= fieldVal != null && fieldVal.StartsWith(compare.Value);
                            break;
                        case TaktDataCompare.EndWith:
                            result &= fieldVal != null && fieldVal.EndsWith(compare.Value);
                            break;
                    }
                    continue;
                }

                // 数值比较
                bool isDecimalValue = decimal.TryParse(compare.Value, out decimal value);

                if (isDecimalValue && fieldVal != null && decimal.TryParse(fieldVal, out decimal frmvalue))
                {
                    switch (compare.Operation)
                    {
                        case TaktDataCompare.Equal:
                            result &= frmvalue == value;
                            break;
                        case TaktDataCompare.Larger:
                            result &= frmvalue > value;
                            break;
                        case TaktDataCompare.Less:
                            result &= frmvalue < value;
                            break;
                        case TaktDataCompare.LargerEqual:
                            result &= frmvalue >= value;
                            break;
                        case TaktDataCompare.LessEqual:
                            result &= frmvalue <= value;
                            break;
                        case TaktDataCompare.NotEqual:
                            result &= frmvalue != value;
                            break;
                    }
                }
                else // 字符串比较
                {
                    switch (compare.Operation)
                    {
                        case TaktDataCompare.Equal:
                            result &= compare.Value == fieldVal;
                            break;
                        case TaktDataCompare.Larger:
                            result &= string.Compare(fieldVal, compare.Value, false) > 0;
                            break;
                        case TaktDataCompare.Less:
                            result &= string.Compare(fieldVal, compare.Value, false) < 0;
                            break;
                        case TaktDataCompare.LargerEqual:
                            result &= string.Compare(fieldVal, compare.Value, false) >= 0;
                            break;
                        case TaktDataCompare.LessEqual:
                            result &= string.Compare(fieldVal, compare.Value, false) <= 0;
                            break;
                        case TaktDataCompare.NotEqual:
                            result &= compare.Value != fieldVal;
                            break;
                    }
                }
            }

            return result;
        }
    }

    /// <summary>
    /// 数据比较条件
    /// </summary>
    public class TaktDataCompare
    {
        // 基本比较操作符
        public const string Larger = ">";
        public const string Less = "<";
        public const string LargerEqual = ">=";
        public const string LessEqual = "<=";
        public const string NotEqual = "!=";
        public const string Equal = "=";

        // 文本操作符
        public const string Like = "LIKE";
        public const string NotLike = "NOT LIKE";
        public const string StartWith = "START_WITH";
        public const string EndWith = "END_WITH";

        // 范围操作符
        public const string In = "IN";
        public const string NotIn = "NOT IN";
        public const string Between = "BETWEEN";

        // 空值操作符
        public const string IsNull = "IS NULL";
        public const string IsNotNull = "IS NOT NULL";

        /// <summary>
        /// 操作类型（比如大于/等于/小于）
        /// </summary>
        public string Operation { get; set; } = string.Empty;

        /// <summary>
        /// 表单中的字段名称
        /// </summary>
        public string FieldName { get; set; } = string.Empty;

        /// <summary>
        /// 字段类型："form"：为表单中的字段，后期扩展系统表等
        /// </summary>
        public string FieldType { get; set; } = string.Empty;

        /// <summary>
        /// 比较的值
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// 值范围（BETWEEN 操作符使用）
        /// </summary>
        public string[]? ValueRange { get; set; }

        /// <summary>
        /// 值列表（IN 和 NOT IN 操作符使用）
        /// </summary>
        public string[]? ValueList { get; set; }
    }
}


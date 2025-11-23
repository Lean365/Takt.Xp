using System.ComponentModel;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Takt.Infrastructure.Swagger
{
    /// <summary>
    /// 枚举架构过滤器
    /// </summary>
    public class EnumSchemaFilter : ISchemaFilter
    {
        /// <summary>
        /// 日志服务
        /// </summary>
        protected readonly ITaktLogger _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public EnumSchemaFilter(
            ITaktLogger logger
                )
        {
            _logger = logger;
        }

        /// <summary>
        /// 应用过滤器
        /// </summary>
        /// <param name="schema">OpenAPI架构</param>
        /// <param name="context">架构过滤器上下文</param>
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            try
            {
                if (context.Type.IsEnum)
                {
                    schema.Enum.Clear();
                    schema.Type = "string";
                    schema.Format = null;

                    var enumValues = Enum.GetValues(context.Type);
                    foreach (var value in enumValues)
                    {
                        var name = Enum.GetName(context.Type, value);
                        var desc = GetEnumDescription(context.Type, name);
                        schema.Enum.Add(new Microsoft.OpenApi.Any.OpenApiString($"{value} = {desc}"));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error("处理枚举架构过滤器时发生错误", ex.Message);
            }
        }

        /// <summary>
        /// 获取枚举描述
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private static string GetEnumDescription(Type enumType, string name)
        {
            var field = enumType.GetField(name);
            var attribute = field?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            return attribute?.Description ?? name;
        }
    }
}

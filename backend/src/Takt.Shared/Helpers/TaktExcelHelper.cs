//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktExcelHelper.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:50
// 版本号 : V.0.0.1
// 描述    : Excel导入导出帮助类
//===================================================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Takt.Shared.Options;
using Takt.Shared.Exceptions;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
using OfficeOpenXml.DataValidation;
using System.Xml;
using NLog;

namespace Takt.Shared.Helpers
{
    /// <summary>
    /// Excel导入导出帮助类
    /// </summary>
    public class TaktExcelHelper
    {
        private static readonly ILogger _logger = LogManager.GetCurrentClassLogger();

        private static TaktExcelOptions? _options;

        /// <summary>
        /// 静态构造函数，设置EPPlus许可证
        /// </summary>
        static TaktExcelHelper()
        {
            ExcelPackage.License.SetNonCommercialOrganization("Lean.Takt");
        }

        /// <summary>
        /// 生成带时间戳的文件名
        /// </summary>
        /// <param name="baseFileName">基础文件名</param>
        /// <returns>带时间戳的文件名</returns>
        private static string GenerateTimestampFileName(string baseFileName)
        {
            var timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return $"{baseFileName}_{timestamp}.xlsx";
        }

        /// <summary>
        /// 设置Excel配置
        /// </summary>
        /// <param name="options">Excel配置选项</param>
        public static void Configure(IOptions<TaktExcelOptions> options)
        {
            ArgumentNullException.ThrowIfNull(options);
            _options = options.Value;
        }

        /// <summary>
        /// 设置工作簿属性
        /// </summary>
        private static void SetWorkbookProperties(ExcelWorkbook workbook)
        {
            ArgumentNullException.ThrowIfNull(workbook);
            if (_options == null) return;

            var props = workbook.Properties;
            props.Author = _options.Author;
            props.Title = _options.Title;
            props.Subject = _options.Subject;
            props.Category = _options.Category;
            props.Keywords = _options.Keywords;
            props.Comments = _options.Comments;
            props.Status = _options.Status;
            props.Application = _options.Application;
            props.Company = _options.Company;
            props.Manager = _options.Manager;
            props.Created = DateTime.Now;
            props.Modified = DateTime.Now;
            props.LastModifiedBy = _options.Author;
        }

        #region 单Sheet导入导出

        /// <summary>
        /// 导出Excel(单个Sheet，支持超大数据分批导出多个文件)
        /// </summary>
        /// <typeparam name="T">要导出的数据类型</typeparam>
        /// <param name="data">数据集合</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名（不包含扩展名）</param>
        /// <returns>包含所有文件名和内容的列表</returns>
        public static async Task<(string fileName, byte[] content)> ExportAsync<T>(IEnumerable<T> data, string sheetName = "Data", string? fileName = null) where T : class
        {
            ArgumentNullException.ThrowIfNull(data);
            ArgumentException.ThrowIfNullOrEmpty(sheetName);

            const int maxRows = 10000;
            var dataList = data.ToList();
            int total = dataList.Count;

            if (total == 0)
            {
                // 生成只有表头的空Excel
                using var package = new OfficeOpenXml.ExcelPackage();
                SetWorkbookProperties(package.Workbook);
                await Task.Run(() => ExportToSheetAsync(package, dataList, sheetName));
                var actualFileName = GenerateTimestampFileName(fileName ?? sheetName);
                var content = await package.GetAsByteArrayAsync();
                return (actualFileName, content);
            }

            if (total <= maxRows)
            {
                // 只生成一个Excel
                using var package = new OfficeOpenXml.ExcelPackage();
                SetWorkbookProperties(package.Workbook);
                await Task.Run(() => ExportToSheetAsync(package, dataList, sheetName));
                var actualFileName = GenerateTimestampFileName(fileName ?? sheetName);
                var content = await package.GetAsByteArrayAsync();
                return (actualFileName, content);
            }
            else
            {
                // 超过10000，分批生成多个Excel并打包zip
                int fileCount = (int)Math.Ceiling(total / (double)maxRows);
                var fileList = new List<(string fileName, byte[] content)>();
                for (int i = 0; i < fileCount; i++)
                {
                    var batch = dataList.Skip(i * maxRows).Take(maxRows).ToList();
                    using var package = new OfficeOpenXml.ExcelPackage();
                    SetWorkbookProperties(package.Workbook);
                    await Task.Run(() => ExportToSheetAsync(package, batch, sheetName));
                    var batchFileName = GenerateTimestampFileName($"{fileName ?? sheetName}_{i + 1}");
                    var batchContent = await package.GetAsByteArrayAsync();
                    fileList.Add((batchFileName, batchContent));
                }

                // 打包为zip
                using var ms = new MemoryStream();
                using (var zip = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Create, true))
                {
                    foreach (var (batchFileName, batchContent) in fileList)
                    {
                        var entry = zip.CreateEntry(batchFileName, System.IO.Compression.CompressionLevel.Optimal);
                        using var entryStream = entry.Open();
                        await entryStream.WriteAsync(batchContent, 0, batchContent.Length);
                    }
                }
                ms.Position = 0;
                var zipName = $"{fileName ?? sheetName}_{DateTime.Now:yyyyMMddHHmmss}.zip";
                return (zipName, ms.ToArray());
            }
        }

        /// <summary>
        /// 导入Excel(单个Sheet)
        /// </summary>
        /// <typeparam name="T">要导入的数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>数据集合</returns>
        public static Task<List<T>> ImportAsync<T>(Stream fileStream, string sheetName = "Data") where T : class, new()
        {
            ArgumentNullException.ThrowIfNull(fileStream);
            ArgumentException.ThrowIfNullOrEmpty(sheetName);

            using var package = new ExcelPackage(fileStream);
            return ImportFromSheetAsync<T>(package, sheetName);
        }

        #endregion 单Sheet导入导出

        #region 多Sheet导入导出

        /// <summary>
        /// 导出Excel(多个Sheet)
        /// </summary>
        /// <param name="sheets">Sheet数据字典，key为sheet名称，value为数据集合</param>
        /// <returns>包含文件名和内容的元组</returns>
        public static async Task<(string fileName, byte[] content)> ExportMultiSheetAsync<T>(Dictionary<string, IEnumerable<T>> sheets) where T : class
        {
            ArgumentNullException.ThrowIfNull(sheets);
            if (!sheets.Any()) throw new ArgumentException("至少需要一个Sheet", nameof(sheets));

            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);

            foreach (var sheet in sheets)
            {
                if (string.IsNullOrEmpty(sheet.Key))
                    throw new ArgumentException("Sheet名称不能为空", nameof(sheets));
                if (sheet.Value == null)
                    throw new ArgumentException($"Sheet '{sheet.Key}' 的数据不能为null", nameof(sheets));

                ExportToSheetAsync(package, sheet.Value, sheet.Key);
            }

            var fileName = GenerateTimestampFileName("多Sheet数据");
            var content = await package.GetAsByteArrayAsync();
            return (fileName, content);
        }

        /// <summary>
        /// 导入Excel(多个Sheet)
        /// </summary>
        /// <typeparam name="T">要导入的数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <returns>数据字典，key为sheet名称，value为数据集合</returns>
        public static Task<Dictionary<string, List<T>>> ImportMultiSheetAsync<T>(Stream fileStream) where T : class, new()
        {
            ArgumentNullException.ThrowIfNull(fileStream);

            using var package = new ExcelPackage(fileStream);
            var result = new Dictionary<string, List<T>>();

            foreach (var worksheet in package.Workbook.Worksheets)
            {
                if (worksheet?.Name == null) continue;
                result[worksheet.Name] = ImportFromSheetAsync<T>(package, worksheet.Name).Result;
            }

            return Task.FromResult(result);
        }

        #endregion 多Sheet导入导出

        #region 模板导入导出

        /// <summary>
        /// 生成Excel导入模板
        /// </summary>
        /// <typeparam name="T">模板类型</typeparam>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        public static async Task<(string fileName, byte[] content)> GenerateTemplateAsync<T>(string sheetName = "Data", string? fileName = null) where T : class, new()
        {
            ArgumentException.ThrowIfNullOrEmpty(sheetName);

            using var package = new ExcelPackage();
            SetWorkbookProperties(package.Workbook);
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // 获取属性信息
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false
                    && p.Name != "Id")  // 排除 Id 属性
                .ToList();

            // 读取XML注释
            var xmlDoc = new XmlDocument();
            string? xmlPath = null;
            try
            {
                var asm = typeof(T).Assembly;
                xmlPath = Path.ChangeExtension(asm.Location, ".xml");
                if (File.Exists(xmlPath))
                    xmlDoc.Load(xmlPath);
            }
            catch { }

            // 三行表头
            var headers = new string[properties.Count];     // XML注释
            var fields = new string[properties.Count];      // 字段名

            for (int i = 0; i < properties.Count; i++)
            {
                // 字段名
                fields[i] = properties[i].Name;

                // XML注释
                string summary = properties[i].Name;
                if (xmlDoc.DocumentElement != null)
                {
                    var memberName = $"P:{typeof(T).FullName}.{properties[i].Name}";
                    var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
                    if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
                        summary = node.InnerText.Trim();
                }
                headers[i] = summary;
            }

            // 写入两行表头
            worksheet.Cells[1, 1].LoadFromArrays(new[] { headers });  // 第一行：XML注释
            worksheet.Cells[2, 1].LoadFromArrays(new[] { fields });   // 第二行：字段名

            // 根据属性类型添加数据验证，从第3行开始
            for (int i = 0; i < properties.Count; i++)
            {
                var propertyType = properties[i].PropertyType;
                var column = worksheet.Cells[3, i + 1, 100, i + 1].Address;

                if (propertyType == typeof(string))
                {
                    // 字符串类型添加长度验证
                    var validation = worksheet.DataValidations.AddTextLengthValidation(column);
                    if (validation != null)
                    {
                        validation.ShowErrorMessage = true;
                        validation.Error = "请输入有效的文本";
                        validation.Operator = ExcelDataValidationOperator.between;
                        validation.Formula.Value = 0;
                        validation.Formula2.Value = 255;
                    }
                }
                else if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    // 日期类型添加日期验证
                    var validation = worksheet.DataValidations.AddDateTimeValidation(column);
                    if (validation != null)
                    {
                        validation.ShowErrorMessage = true;
                        validation.Error = "请输入有效的日期";
                        validation.Operator = ExcelDataValidationOperator.between;
                        validation.Formula.Value = new DateTime(1900, 1, 1);
                        validation.Formula2.Value = new DateTime(9999, 12, 31);
                    }
                    worksheet.Column(i + 1).Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                }
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?) ||
                         propertyType == typeof(double) || propertyType == typeof(double?) ||
                         propertyType == typeof(float) || propertyType == typeof(float?))
                {
                    // 数值类型添加数值验证
                    var validation = worksheet.DataValidations.AddDecimalValidation(column);
                    if (validation != null)
                    {
                        validation.ShowErrorMessage = true;
                        validation.Error = "请输入有效的数值";
                        validation.Operator = ExcelDataValidationOperator.between;
                        validation.Formula.Value = -999999999.0;
                        validation.Formula2.Value = 999999999.0;
                    }
                    worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0.00";
                }
                else if (propertyType == typeof(int) || propertyType == typeof(int?) ||
                         propertyType == typeof(long) || propertyType == typeof(long?))
                {
                    // 整数类型添加整数验证
                    var validation = worksheet.DataValidations.AddIntegerValidation(column);
                    if (validation != null)
                    {
                        validation.ShowErrorMessage = true;
                        validation.Error = "请输入有效的整数";
                        validation.Operator = ExcelDataValidationOperator.between;
                        validation.Formula.Value = -2147483648;
                        validation.Formula2.Value = 2147483647;
                    }
                    worksheet.Column(i + 1).Style.Numberformat.Format = "#,##0";
                }
            }

            // 自动调整列宽
            if (worksheet.Dimension != null)
            {
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();
            }

            var content = await package.GetAsByteArrayAsync();
            var actualFileName = fileName ?? $"{typeof(T).Name}模板";
            return (GenerateTimestampFileName(actualFileName), content);
        }

        #endregion 模板导入导出

        #region 私有辅助方法

        /// <summary>
        /// 导出数据到指定Sheet
        /// </summary>
        private static void ExportToSheetAsync<T>(ExcelPackage package, IEnumerable<T> data, string sheetName) where T : class
        {
            ArgumentNullException.ThrowIfNull(package);
            ArgumentNullException.ThrowIfNull(data);
            ArgumentException.ThrowIfNullOrEmpty(sheetName);

            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // 获取属性信息
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false
                    && p.Name != "Id")  // 排除 Id 属性
                .ToList();

            // 读取XML注释
            var xmlDoc = new XmlDocument();
            string? xmlPath = null;
            try
            {
                var asm = typeof(T).Assembly;
                xmlPath = Path.ChangeExtension(asm.Location, ".xml");
                if (File.Exists(xmlPath))
                    xmlDoc.Load(xmlPath);
            }
            catch { }

            // 优化：使用数组存储表头
            var headers = new string[properties.Count];
            for (int i = 0; i < properties.Count; i++)
            {
                // 优先使用XML注释
                string header = properties[i].Name;
                if (xmlDoc.DocumentElement != null)
                {
                    var memberName = $"P:{typeof(T).FullName}.{properties[i].Name}";
                    var node = xmlDoc.SelectSingleNode($"//member[@name='{memberName}']/summary");
                    if (node != null && !string.IsNullOrWhiteSpace(node.InnerText))
                        header = node.InnerText.Trim();
                }
                headers[i] = header;
            }

            // 优化：批量写入表头
            worksheet.Cells[1, 1].LoadFromArrays(new[] { headers });

            // 优化：使用数组批量写入数据
            var dataArray = new List<object[]>();
            foreach (var item in data)
            {
                var row = new object[properties.Count];
                for (int i = 0; i < properties.Count; i++)
                {
                    row[i] = properties[i].GetValue(item) ?? DBNull.Value;
                }
                dataArray.Add(row);
            }

            if (dataArray.Any())
            {
                worksheet.Cells[2, 1].LoadFromArrays(dataArray);
            }

            // 优化：设置列格式
            for (int i = 0; i < properties.Count; i++)
            {
                var propertyType = properties[i].PropertyType;
                var column = worksheet.Column(i + 1);

                if (propertyType == typeof(DateTime) || propertyType == typeof(DateTime?))
                {
                    column.Style.Numberformat.Format = "yyyy-MM-dd HH:mm:ss";
                }
                else if (propertyType == typeof(decimal) || propertyType == typeof(decimal?))
                {
                    column.Style.Numberformat.Format = "#,##0.00";
                }
            }

            // 优化：自动调整列宽
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            // 冻结首行
            worksheet.View.FreezePanes(2, 1);
        }

        /// <summary>
        /// 从指定Sheet导入数据
        /// </summary>
        private static Task<List<T>> ImportFromSheetAsync<T>(ExcelPackage package, string sheetName) where T : class, new()
        {
            ArgumentNullException.ThrowIfNull(package);
            ArgumentException.ThrowIfNullOrEmpty(sheetName);

            var worksheet = package.Workbook.Worksheets[sheetName];
            if (worksheet == null || worksheet.Dimension == null)
                throw new Exception($"找不到名为 {sheetName} 的工作表或工作表为空");

            var result = new List<T>();
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<BrowsableAttribute>()?.Browsable != false
                    && p.Name != "Id")  // 排除 Id 属性
                .ToList();

            // 输出Excel表头和DTO属性名日志
            _logger.Info("Excel表头：" + string.Join(",", Enumerable.Range(1, worksheet.Dimension.End.Column).Select(c => worksheet.Cells[2, c].Value?.ToString() ?? "null")));
            _logger.Info("DTO属性：" + string.Join(",", properties.Select(p => p.Name)));

            // 获取表头与属性的映射关系
            var headerMap = new Dictionary<string, PropertyInfo>();
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                // 从第二行（字段名）获取表头
                var headerValue = worksheet.Cells[2, col].Value?.ToString();
                if (string.IsNullOrEmpty(headerValue)) continue;

                var property = properties.FirstOrDefault(p =>
                    p.Name == headerValue);

                if (property != null)
                {
                    headerMap[headerValue] = property;
                }
            }

            _logger.Info($"表头映射: {string.Join(", ", headerMap.Keys)}");
            if (headerMap.Count == 0)
            {
                _logger.Warn("未找到任何表头映射！");
                _logger.Warn($"工作表维度: {worksheet.Dimension?.Address}");
                _logger.Warn($"第二行数据: {string.Join(", ", Enumerable.Range(1, worksheet.Dimension?.End.Column ?? 0).Select(c => worksheet.Cells[2, c].Value?.ToString() ?? "null"))}");
            }

            // 读取数据
            for (int row = 3; row <= worksheet.Dimension.End.Row; row++)
            {
                var item = new T();
                _logger.Info($"开始读取第 {row} 行数据");
                foreach (var header in headerMap)
                {
                    var property = header.Value;
                    var col = GetColumnByHeader(worksheet, header.Key);
                    if (col == -1)
                    {
                        _logger.Warn($"未找到列 {header.Key} 的位置");
                        continue;
                    }

                    var cell = worksheet.Cells[row, col];
                    var cellValue = cell.Value?.ToString();
                    _logger.Debug($"  列 {header.Key}: 原始值={cellValue}");
                    if (string.IsNullOrEmpty(cellValue))
                    {
                        _logger.Debug($"  列 {header.Key}: 值为空，跳过");
                        continue;
                    }

                    var value = ConvertValue(cellValue, property.PropertyType);
                    _logger.Debug($"  列 {header.Key}: 转换后值={value}");
                    if (value != null)
                    {
                        property.SetValue(item, value);
                        _logger.Debug($"  列 {header.Key}: 设置值成功");
                    }
                    else
                    {
                        _logger.Warn($"  列 {header.Key}: 值转换失败，原始值={cellValue}");
                    }
                }

                // 验证必填字段
                var requiredProps = typeof(T).GetProperties();
                foreach (var prop in requiredProps)
                {
                    var value = prop.GetValue(item);
                    if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                    {
                        _logger.Warn($"第 {row} 行 {prop.Name} 字段为空");
                    }
                }

                result.Add(item);
                _logger.Info($"第 {row} 行数据读取完成");
            }

            if (result.Count == 0)
            {
                _logger.Warn("未读取到任何数据！");
                return Task.FromResult(new List<T>());
            }

            _logger.Info($"成功读取 {result.Count} 行数据");
            return Task.FromResult(result);
        }

        private static int GetColumnByHeader(ExcelWorksheet worksheet, string headerText)
        {
            for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
            {
                // 查找第二行（字段名）
                if (worksheet.Cells[2, col].Value?.ToString() == headerText)
                    return col;
            }
            return -1;
        }

        private static object? ConvertValue(string value, Type targetType)
        {
            if (string.IsNullOrEmpty(value)) return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;

            try
            {
                if (targetType == typeof(string))
                    return value;

                if (targetType.IsEnum)
                    return Enum.Parse(targetType, value);

                if (targetType == typeof(DateTime) || targetType == typeof(DateTime?))
                {
                    return DateTime.Parse(value);
                }

                if (targetType == typeof(bool) || targetType == typeof(bool?))
                {
                    var strValue = value.ToLower();
                    return strValue == "是" || strValue == "1" || strValue == "true";
                }

                return Convert.ChangeType(value, targetType);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 将多个Excel文件打包为zip并返回
        /// </summary>
        /// <param name="files">要打包的文件列表</param>
        /// <param name="zipFileName">zip文件名（不含扩展名）</param>
        /// <returns>zip文件名和内容</returns>
        public static async Task<(string fileName, byte[] content)> PackToZipAsync(List<(string fileName, byte[] content)> files, string zipFileName)
        {
            if (files == null || files.Count == 0)
                throw new ArgumentException("没有可打包的文件", nameof(files));
            using var ms = new MemoryStream();
            using (var zip = new System.IO.Compression.ZipArchive(ms, System.IO.Compression.ZipArchiveMode.Create, true))
            {
                foreach (var (fileName, content) in files)
                {
                    var entry = zip.CreateEntry(fileName, System.IO.Compression.CompressionLevel.Optimal);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(content, 0, content.Length);
                }
            }
            ms.Position = 0;
            var zipName = $"{zipFileName}_{DateTime.Now:yyyyMMddHHmmss}.zip";
            return (zipName, ms.ToArray());
        }

        #endregion 私有辅助方法
    }
}





//===================================================================
// 项目名 : Takt.Xp
// 文件名 : FileUploadExtensions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01 10:00
// 版本号 : V0.0.1
// 描述   : 文件上传配置扩展方法
//===================================================================

using Takt.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Takt.Shared.Extensions
{
    /// <summary>
    /// 文件上传配置扩展方法
    /// </summary>
    public static class FileUploadExtensions
    {
        /// <summary>
        /// 验证文件是否符合指定类型的配置要求
        /// </summary>
        /// <param name="file">上传的文件</param>
        /// <param name="fileUploadOptions">文件上传配置选项</param>
        /// <param name="uploadType">上传类型（Normal, Import, Avatar）</param>
        /// <returns>验证结果</returns>
        public static (bool isValid, string errorMessage) ValidateFile(
            this IFormFile file, 
            FileUploadOptions fileUploadOptions, 
            string uploadType = "Normal")
        {
            if (file == null || file.Length == 0)
            {
                return (false, "未找到上传的文件");
            }

            // 根据上传类型获取配置
            var typeOptions = uploadType.ToLower() switch
            {
                "import" => fileUploadOptions.Import,
                "avatar" => fileUploadOptions.Avatar,
                _ => fileUploadOptions.Normal
            };

            // 验证文件大小
            if (file.Length > typeOptions.MaxFileSize)
            {
                return (false, $"文件大小不能超过{FormatFileSize(typeOptions.MaxFileSize)}");
            }

            // 验证文件类型
            var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!typeOptions.AllowedExtensions.Contains(fileExtension))
            {
                return (false, $"不支持的文件类型，允许的类型：{string.Join(", ", typeOptions.AllowedExtensions)}");
            }

            return (true, string.Empty);
        }

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="size">文件大小（字节）</param>
        /// <returns>格式化后的文件大小</returns>
        private static string FormatFileSize(long size)
        {
            if (size < 1024)
                return $"{size} B";
            if (size < 1024 * 1024)
                return $"{(size / 1024.0):F2} KB";
            if (size < 1024 * 1024 * 1024)
                return $"{(size / (1024.0 * 1024)):F2} MB";
            return $"{(size / (1024.0 * 1024 * 1024)):F2} GB";
        }
    }
}






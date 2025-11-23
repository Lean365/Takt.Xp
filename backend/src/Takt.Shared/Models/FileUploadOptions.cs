//===================================================================
// 项目名 : Takt.Xp
// 文件名 : FileUploadOptions.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01 10:00
// 版本号 : V0.0.1
// 描述   : 文件上传配置选项
//===================================================================

namespace Takt.Shared.Models
{
    /// <summary>
    /// 文件上传配置选项
    /// </summary>
    public class FileUploadOptions
    {
        /// <summary>
        /// 导入文件配置
        /// </summary>
        public FileUploadTypeOptions Import { get; set; } = new();

        /// <summary>
        /// 普通文件配置
        /// </summary>
        public FileUploadTypeOptions Normal { get; set; } = new();

        /// <summary>
        /// 头像文件配置
        /// </summary>
        public FileUploadTypeOptions Avatar { get; set; } = new();
    }

    /// <summary>
    /// 文件上传类型配置选项
    /// </summary>
    public class FileUploadTypeOptions
    {
        /// <summary>
        /// 多部分主体长度限制
        /// </summary>
        public long MultipartBodyLengthLimit { get; set; } = 20 * 1024 * 1024; // 20MB

        /// <summary>
        /// 多部分头部长度限制
        /// </summary>
        public long MultipartHeadersLengthLimit { get; set; } = 32 * 1024; // 32KB

        /// <summary>
        /// 多部分边界长度限制
        /// </summary>
        public long MultipartBoundaryLengthLimit { get; set; } = 128;

        /// <summary>
        /// 值长度限制
        /// </summary>
        public long ValueLengthLimit { get; set; } = 20 * 1024 * 1024; // 20MB

        /// <summary>
        /// 键长度限制
        /// </summary>
        public long KeyLengthLimit { get; set; } = 1024;

        /// <summary>
        /// 最大文件大小
        /// </summary>
        public long MaxFileSize { get; set; } = 20 * 1024 * 1024; // 20MB

        /// <summary>
        /// 允许的文件扩展名
        /// </summary>
        public string[] AllowedExtensions { get; set; } = Array.Empty<string>();
    }
}






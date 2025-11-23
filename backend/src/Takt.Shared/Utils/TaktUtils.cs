//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktUtils.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-05 10:00
// 版本号 : V.0.0.1
// 描述    : 系统通用工具类
//===================================================================

using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Security;

namespace Takt.Shared.Utils
{
    /// <summary>
    /// 系统通用工具类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-05
    /// </remarks>
    public static class TaktUtils
    {
        /// <summary>
        /// 生成GUID字符串(不含连字符)
        /// </summary>
        /// <returns>32位GUID字符串</returns>
        public static string GenerateGuid()
        {
            return Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 使用SHA256算法对密码进行哈希
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <returns>哈希后的密码字符串</returns>
        public static string HashPassword(string password)
        {
            var digest = new Sha256Digest();
            var inputBytes = Encoding.UTF8.GetBytes(password);
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// 验证密码是否匹配
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <param name="hashedPassword">哈希后的密码</param>
        /// <returns>密码是否匹配</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var hashedInput = HashPassword(password);
            return hashedInput == hashedPassword;
        }

        /// <summary>
        /// 生成指定长度的随机数字验证码
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns>随机数字验证码</returns>
        public static string GenerateRandomCode(int length = 6)
        {
            var random = new SecureRandom();
            var code = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                code.Append(random.Next(0, 10));
            }
            return code.ToString();
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">字符串长度</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomString(int length = 8)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new SecureRandom();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// 格式化文件大小
        /// </summary>
        /// <param name="bytes">文件字节数</param>
        /// <returns>格式化后的文件大小字符串</returns>
        public static string FormatFileSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            int order = 0;
            double size = bytes;
            while (size >= 1024 && order < sizes.Length - 1)
            {
                order++;
                size = size / 1024;
            }
            return $"{size:0.##} {sizes[order]}";
        }
    }
} 





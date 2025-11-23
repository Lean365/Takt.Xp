//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktPasswordUtils.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 10:00
// 版本号 : V0.0.1
// 描述    : 密码工具类 - 基于Bouncy Castle Cryptography提供安全的密码处理基础设施
//===================================================================

using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;

namespace Takt.Shared.Utils
{
    /// <summary>
    /// 密码工具类 - 提供密码加密和验证的基础功能
    /// </summary>
    public static class TaktPasswordUtils
    {
        // 加密参数配置
        private const int DEFAULT_SALT_SIZE = 32;        // 盐值长度
        private const int DEFAULT_HASH_SIZE = 32;        // 哈希长度
        private const int DEFAULT_ITERATIONS = 600000;   // PBKDF2迭代次数

        /// <summary>
        /// 创建密码哈希
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <returns>(哈希后的密码, 盐值, 迭代次数)</returns>
        public static (string Hash, string Salt, int Iterations) CreateHash(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "密码不能为空");
            
            if (password.Length < 6)
                throw new ArgumentException("密码长度必须至少为6个字符", nameof(password));

            try
            {
                // 每次都生成新的随机盐值
                var random = new SecureRandom();
                byte[] salt = new byte[DEFAULT_SALT_SIZE];
                random.NextBytes(salt);

                // 使用 PBKDF2 进行密码哈希
                var pbkdf2 = new Pkcs5S2ParametersGenerator(new Sha256Digest());
                pbkdf2.Init(Encoding.UTF8.GetBytes(password), salt, DEFAULT_ITERATIONS);
                var hash = ((KeyParameter)pbkdf2.GenerateDerivedMacParameters(8 * DEFAULT_HASH_SIZE)).GetKey();

                return (
                    Convert.ToBase64String(hash),
                    Convert.ToBase64String(salt),
                    DEFAULT_ITERATIONS
                );
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[密码哈希] 创建密码哈希时发生错误: {ex.Message}");
                throw new InvalidOperationException("创建密码哈希时发生错误", ex);
            }
        }

        /// <summary>
        /// 验证密码
        /// </summary>
        /// <param name="password">原始密码</param>
        /// <param name="hash">数据库中存储的密码哈希值</param>
        /// <param name="salt">盐值</param>
        /// <param name="iterations">迭代次数</param>
        /// <returns>是否验证通过</returns>
        public static bool VerifyHash(string password, string hash, string salt, int iterations)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password));
            if (string.IsNullOrEmpty(hash))
                throw new ArgumentNullException(nameof(hash));

            try
            {
                // 将Base64字符串转换为字节数组
                byte[] saltBytes = Convert.FromBase64String(salt);
                byte[] storedHashBytes = Convert.FromBase64String(hash);

                // 使用相同的参数重新计算密码哈希
                var pbkdf2 = new Pkcs5S2ParametersGenerator(new Sha256Digest());
                pbkdf2.Init(Encoding.UTF8.GetBytes(password), saltBytes, iterations);
                var computedHash = ((KeyParameter)pbkdf2.GenerateDerivedMacParameters(8 * DEFAULT_HASH_SIZE)).GetKey();

                // 使用固定时间比较防止计时攻击
                return FixedTimeEquals(computedHash, storedHashBytes);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[密码验证] 发生错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 生成指定长度的随机字符串
        /// </summary>
        /// <param name="length">长度</param>
        /// <param name="allowedChars">允许的字符集</param>
        /// <returns>随机字符串</returns>
        public static string GenerateRandomString(int length, string allowedChars)
        {
            if (length <= 0)
                throw new ArgumentException("长度必须大于0", nameof(length));
            if (string.IsNullOrEmpty(allowedChars))
                throw new ArgumentException("字符集不能为空", nameof(allowedChars));

            var result = new StringBuilder(length);
            var random = new SecureRandom();

            for (int i = 0; i < length; i++)
            {
                byte[] randomNumber = new byte[1];
                random.NextBytes(randomNumber);
                result.Append(allowedChars[randomNumber[0] % allowedChars.Length]);
            }

            return result.ToString();
        }

        /// <summary>
        /// 固定时间比较两个字节数组，防止计时攻击
        /// </summary>
        /// <param name="a">第一个字节数组</param>
        /// <param name="b">第二个字节数组</param>
        /// <returns>是否相等</returns>
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a.Length != b.Length)
                return false;

            int result = 0;
            for (int i = 0; i < a.Length; i++)
            {
                result |= a[i] ^ b[i];
            }

            return result == 0;
        }
    }
} 





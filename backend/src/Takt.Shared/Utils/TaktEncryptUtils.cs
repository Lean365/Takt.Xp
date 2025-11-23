#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktEncryptUtils.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-19 11:00
// 版本号 : V.0.0.1
// 描述    : 加密工具类 - 基于Bouncy Castle Cryptography
//===================================================================

using System.Text;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.X509;

namespace Takt.Shared.Utils
{
    /// <summary>
    /// 加密工具类 - 基于Bouncy Castle Cryptography
    /// </summary>
    public static class TaktEncryptUtils
    {
        private static readonly string DefaultKey = "Takt.2024.01"; // 16字节的密钥
        private static readonly string DefaultIV = "Takt.Encrypt"; // 16字节的初始化向量

        #region AES加密解密

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <returns>密文(Base64编码)</returns>
        public static string AesEncrypt(string plainText)
        {
            return AesEncrypt(plainText, DefaultKey, DefaultIV);
        }

        /// <summary>
        /// AES加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>密文(Base64编码)</returns>
        public static string AesEncrypt(string plainText, string key, string iv)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("明文不能为空", nameof(plainText));

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var ivBytes = Encoding.UTF8.GetBytes(iv);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            var cipher = new AesEngine();
            var blockCipher = new CbcBlockCipher(cipher);
            var cipherPadding = new Pkcs7Padding();
            var paddedCipher = new PaddedBufferedBlockCipher(blockCipher, cipherPadding);

            var keyParam = new KeyParameter(keyBytes);
            var keyParamWithIv = new ParametersWithIV(keyParam, ivBytes);

            paddedCipher.Init(true, keyParamWithIv);

            var output = new byte[paddedCipher.GetOutputSize(plainBytes.Length)];
            var length = paddedCipher.ProcessBytes(plainBytes, 0, plainBytes.Length, output, 0);
            paddedCipher.DoFinal(output, length);

            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文(Base64编码)</param>
        /// <returns>明文</returns>
        public static string AesDecrypt(string cipherText)
        {
            return AesDecrypt(cipherText, DefaultKey, DefaultIV);
        }

        /// <summary>
        /// AES解密
        /// </summary>
        /// <param name="cipherText">密文(Base64编码)</param>
        /// <param name="key">密钥</param>
        /// <param name="iv">初始化向量</param>
        /// <returns>明文</returns>
        public static string AesDecrypt(string cipherText, string key, string iv)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("密文不能为空", nameof(cipherText));

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var ivBytes = Encoding.UTF8.GetBytes(iv);
            var cipherBytes = Convert.FromBase64String(cipherText);

            var cipher = new AesEngine();
            var blockCipher = new CbcBlockCipher(cipher);
            var cipherPadding = new Pkcs7Padding();
            var paddedCipher = new PaddedBufferedBlockCipher(blockCipher, cipherPadding);

            var keyParam = new KeyParameter(keyBytes);
            var keyParamWithIv = new ParametersWithIV(keyParam, ivBytes);

            paddedCipher.Init(false, keyParamWithIv);

            var output = new byte[paddedCipher.GetOutputSize(cipherBytes.Length)];
            var length = paddedCipher.ProcessBytes(cipherBytes, 0, cipherBytes.Length, output, 0);
            paddedCipher.DoFinal(output, length);

            return Encoding.UTF8.GetString(output).TrimEnd('\0');
        }

        #endregion

        #region RSA加密解密

        /// <summary>
        /// 生成RSA密钥对
        /// </summary>
        /// <param name="keySize">密钥长度(1024/2048/4096)</param>
        /// <returns>RSA密钥对(公钥,私钥)</returns>
        public static (string publicKey, string privateKey) GenerateRsaKeyPair(int keySize = 2048)
        {
            var keyPairGenerator = GeneratorUtilities.GetKeyPairGenerator("RSA");
            var keyGenerationParameters = new KeyGenerationParameters(SecureRandom.GetInstance("SHA256PRNG"), keySize);
            keyPairGenerator.Init(keyGenerationParameters);

            var keyPair = keyPairGenerator.GenerateKeyPair();
            var publicKey = Convert.ToBase64String(SubjectPublicKeyInfoFactory.CreateSubjectPublicKeyInfo(keyPair.Public).GetEncoded());
            var privateKey = Convert.ToBase64String(PrivateKeyInfoFactory.CreatePrivateKeyInfo(keyPair.Private).GetEncoded());

            return (publicKey, privateKey);
        }

        /// <summary>
        /// RSA公钥加密
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="publicKey">公钥(Base64编码)</param>
        /// <returns>密文(Base64编码)</returns>
        public static string RsaEncrypt(string plainText, string publicKey)
        {
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("明文不能为空", nameof(plainText));

            var publicKeyBytes = Convert.FromBase64String(publicKey);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);

            var rsaEngine = new RsaEngine();
            var publicKeyInfo = SubjectPublicKeyInfo.GetInstance(publicKeyBytes);
            var rsaKeyParameters = (RsaKeyParameters)PublicKeyFactory.CreateKey(publicKeyInfo);

            rsaEngine.Init(true, rsaKeyParameters);

            var output = rsaEngine.ProcessBlock(plainBytes, 0, plainBytes.Length);
            return Convert.ToBase64String(output);
        }

        /// <summary>
        /// RSA私钥解密
        /// </summary>
        /// <param name="cipherText">密文(Base64编码)</param>
        /// <param name="privateKey">私钥(Base64编码)</param>
        /// <returns>明文</returns>
        public static string RsaDecrypt(string cipherText, string privateKey)
        {
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("密文不能为空", nameof(cipherText));

            var privateKeyBytes = Convert.FromBase64String(privateKey);
            var cipherBytes = Convert.FromBase64String(cipherText);

            var rsaEngine = new RsaEngine();
            var privateKeyInfo = PrivateKeyInfo.GetInstance(privateKeyBytes);
            var rsaKeyParameters = (RsaPrivateCrtKeyParameters)PrivateKeyFactory.CreateKey(privateKeyInfo);

            rsaEngine.Init(false, rsaKeyParameters);

            var output = rsaEngine.ProcessBlock(cipherBytes, 0, cipherBytes.Length);
            return Encoding.UTF8.GetString(output).TrimEnd('\0');
        }

        #endregion

        #region 哈希算法

        /// <summary>
        /// MD5哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>MD5哈希值(十六进制)</returns>
        public static string Md5Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入不能为空", nameof(input));

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var digest = new MD5Digest();
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToHexString(output).ToLower();
        }

        /// <summary>
        /// SHA1哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA1哈希值(十六进制)</returns>
        public static string Sha1Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入不能为空", nameof(input));

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var digest = new Sha1Digest();
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToHexString(output).ToLower();
        }

        /// <summary>
        /// SHA256哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA256哈希值(十六进制)</returns>
        public static string Sha256Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入不能为空", nameof(input));

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var digest = new Sha256Digest();
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToHexString(output).ToLower();
        }

        /// <summary>
        /// SHA512哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SHA512哈希值(十六进制)</returns>
        public static string Sha512Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入不能为空", nameof(input));

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var digest = new Sha512Digest();
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToHexString(output).ToLower();
        }

        #endregion

        #region 国密算法

        /// <summary>
        /// SM3哈希
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <returns>SM3哈希值(十六进制)</returns>
        public static string Sm3Hash(string input)
        {
            if (string.IsNullOrEmpty(input))
                throw new ArgumentException("输入不能为空", nameof(input));

            var inputBytes = Encoding.UTF8.GetBytes(input);
            var digest = new SM3Digest();
            digest.BlockUpdate(inputBytes, 0, inputBytes.Length);
            
            var output = new byte[digest.GetDigestSize()];
            digest.DoFinal(output, 0);
            
            return Convert.ToHexString(output).ToLower();
        }

        #endregion

        #region 工具方法

        /// <summary>
        /// 生成随机密钥
        /// </summary>
        /// <param name="length">密钥长度(字节)</param>
        /// <returns>随机密钥(Base64编码)</returns>
        public static string GenerateRandomKey(int length = 32)
        {
            var random = new SecureRandom();
            var keyBytes = new byte[length];
            random.NextBytes(keyBytes);
            return Convert.ToBase64String(keyBytes);
        }

        /// <summary>
        /// 生成随机IV
        /// </summary>
        /// <param name="length">IV长度(字节)</param>
        /// <returns>随机IV(Base64编码)</returns>
        public static string GenerateRandomIV(int length = 16)
        {
            var random = new SecureRandom();
            var ivBytes = new byte[length];
            random.NextBytes(ivBytes);
            return Convert.ToBase64String(ivBytes);
        }

        #endregion
    }
} 





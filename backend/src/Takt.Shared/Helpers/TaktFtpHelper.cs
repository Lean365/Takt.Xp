//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFtpHelper.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-04-27
// 版本号 : V0.0.1
// 描述   : FTP操作帮助类
//===================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;
using FluentFTP;
using NLog;

namespace Takt.Shared.Helpers
{
    /// <summary>
    /// FTP操作帮助类
    /// </summary>
    public class TaktFtpHelper : IDisposable
    {
        private static readonly ILogger Logger = LogManager.GetCurrentClassLogger();
        private readonly AsyncFtpClient _ftpClient;
        private bool _disposed;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="host">FTP主机地址</param>
        /// <param name="port">端口号</param>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="enableSsl">是否启用SSL</param>
        public TaktFtpHelper(string host, int port = 21, string username = "", string password = "", bool enableSsl = false)
        {
            _ftpClient = new AsyncFtpClient(host, username, password, port);
            _ftpClient.Config.EncryptionMode = enableSsl ? FtpEncryptionMode.Explicit : FtpEncryptionMode.None;
            _ftpClient.Config.ValidateAnyCertificate = true;
        }

        /// <summary>
        /// 连接FTP服务器
        /// </summary>
        public async Task ConnectAsync()
        {
            try
            {
                await _ftpClient.Connect();
                Logger.Info("FTP连接成功: {Host}", _ftpClient.Host);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "FTP连接失败: {Host}", _ftpClient.Host);
                throw;
            }
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UploadFileAsync(string localPath, string remotePath, Action<FtpProgress> progress = null)
        {
            try
            {
                var progressHandler = new Progress<FtpProgress>(p =>
                {
                    if (p.Progress > 0)
                    {
                        Logger.Info("FTP上传进度: {Progress}%", p.Progress);
                    }
                });

                var status = await _ftpClient.UploadFile(localPath, remotePath, FtpRemoteExists.Overwrite, true, FtpVerify.None, progressHandler);
                if (status == FtpStatus.Success)
                {
                    Logger.Info("文件上传成功: {LocalPath} -> {RemotePath}", localPath, remotePath);
                    return true;
                }
                Logger.Warn("文件上传失败: {LocalPath} -> {RemotePath}, 状态: {Status}", localPath, remotePath, status);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "文件上传异常: {LocalPath} -> {RemotePath}", localPath, remotePath);
                return false;
            }
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="localPath">本地文件路径</param>
        /// <param name="progress">进度回调</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DownloadFileAsync(string remotePath, string localPath, Action<FtpProgress> progress = null)
        {
            try
            {
                var progressHandler = new Progress<FtpProgress>(p =>
                {
                    if (p.Progress > 0)
                    {
                        Logger.Info("FTP下载进度: {Progress}%", p.Progress);
                    }
                });

                var status = await _ftpClient.DownloadFile(localPath, remotePath, FtpLocalExists.Overwrite, FtpVerify.None, progressHandler);
                if (status == FtpStatus.Success)
                {
                    Logger.Info("文件下载成功: {RemotePath} -> {LocalPath}", remotePath, localPath);
                    return true;
                }
                Logger.Warn("文件下载失败: {RemotePath} -> {LocalPath}, 状态: {Status}", remotePath, localPath, status);
                return false;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "文件下载异常: {RemotePath} -> {LocalPath}", remotePath, localPath);
                return false;
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteFileAsync(string remotePath)
        {
            try
            {
                await _ftpClient.DeleteFile(remotePath);
                Logger.Info("文件删除成功: {RemotePath}", remotePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "文件删除失败: {RemotePath}", remotePath);
                return false;
            }
        }

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="remotePath">远程目录路径</param>
        /// <returns>是否成功</returns>
        public async Task<bool> CreateDirectoryAsync(string remotePath)
        {
            try
            {
                await _ftpClient.CreateDirectory(remotePath);
                Logger.Info("目录创建成功: {RemotePath}", remotePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "目录创建失败: {RemotePath}", remotePath);
                return false;
            }
        }

        /// <summary>
        /// 删除目录
        /// </summary>
        /// <param name="remotePath">远程目录路径</param>
        /// <param name="recursive">是否递归删除</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteDirectoryAsync(string remotePath, bool recursive = true)
        {
            try
            {
                await _ftpClient.DeleteDirectory(remotePath, recursive ? FtpListOption.Recursive : FtpListOption.AllFiles);
                Logger.Info("目录删除成功: {RemotePath}", remotePath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "目录删除失败: {RemotePath}", remotePath);
                return false;
            }
        }

        /// <summary>
        /// 列出目录内容
        /// </summary>
        /// <param name="remotePath">远程目录路径</param>
        /// <returns>目录项列表</returns>
        public async Task<List<FtpListItem>> ListDirectoryAsync(string remotePath = "/")
        {
            try
            {
                var items = await _ftpClient.GetListing(remotePath);
                Logger.Info("目录列表获取成功: {RemotePath}, 项目数: {Count}", remotePath, items.Length);
                return new List<FtpListItem>(items);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "目录列表获取失败: {RemotePath}", remotePath);
                return new List<FtpListItem>();
            }
        }

        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns>是否存在</returns>
        public async Task<bool> FileExistsAsync(string remotePath)
        {
            try
            {
                return await _ftpClient.FileExists(remotePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "检查文件存在失败: {RemotePath}", remotePath);
                return false;
            }
        }

        /// <summary>
        /// 获取文件大小
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns>文件大小（字节）</returns>
        public async Task<long> GetFileSizeAsync(string remotePath)
        {
            try
            {
                return await _ftpClient.GetFileSize(remotePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "获取文件大小失败: {RemotePath}", remotePath);
                return -1;
            }
        }

        /// <summary>
        /// 重命名文件或目录
        /// </summary>
        /// <param name="fromPath">原路径</param>
        /// <param name="toPath">新路径</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RenameAsync(string fromPath, string toPath)
        {
            try
            {
                await _ftpClient.Rename(fromPath, toPath);
                Logger.Info("重命名成功: {FromPath} -> {ToPath}", fromPath, toPath);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "重命名失败: {FromPath} -> {ToPath}", fromPath, toPath);
                return false;
            }
        }

        /// <summary>
        /// 获取文件修改时间
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <returns>修改时间</returns>
        public async Task<DateTime?> GetModifiedTimeAsync(string remotePath)
        {
            try
            {
                return await _ftpClient.GetModifiedTime(remotePath);
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "获取文件修改时间失败: {RemotePath}", remotePath);
                return null;
            }
        }

        /// <summary>
        /// 设置文件权限
        /// </summary>
        /// <param name="remotePath">远程文件路径</param>
        /// <param name="permissions">权限值（如：644）</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetPermissionsAsync(string remotePath, int permissions)
        {
            try
            {
                await _ftpClient.SetFilePermissions(remotePath, permissions);
                Logger.Info("设置文件权限成功: {RemotePath}, 权限: {Permissions}", remotePath, permissions);
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "设置文件权限失败: {RemotePath}, 权限: {Permissions}", remotePath, permissions);
                return false;
            }
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing">是否正在释放</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_ftpClient != null && _ftpClient.IsConnected)
                    {
                        _ftpClient.Disconnect();
                    }
                    _ftpClient?.Dispose();
                }
                _disposed = true;
            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        ~TaktFtpHelper()
        {
            Dispose(false);
        }
    }
} 





//===================================================================
// 项目名 : Takt.Shared.Helpers
// 文件名 : TaktMailHelper.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-26 14:30
// 版本号 : V0.0.1
// 描述   : 邮件帮助类
//===================================================================

using Takt.Shared.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Takt.Shared.Helpers
{
    /// <summary>
    /// 邮件帮助类
    /// </summary>
    public static class TaktMailHelper
    {
        private static TaktMailOption? _mailOption;

        /// <summary>
        /// 初始化邮件配置
        /// </summary>
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetService(typeof(IOptions<TaktMailOption>)) as IOptions<TaktMailOption>;
            _mailOption = options?.Value ?? throw new InvalidOperationException($"未找到 {TaktMailOption.Position} 配置节点");
        }

        /// <summary>
        /// 发送纯文本邮件
        /// </summary>
        /// <param name="to">收件人邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="cc">抄送</param>
        /// <returns>发送结果</returns>
        public static async Task<bool> SendTextAsync(string to, string subject, string body, string? cc = null)
        {
            return await SendEmailAsync(to, subject, body, false, null, cc);
        }

        /// <summary>
        /// 发送HTML邮件
        /// </summary>
        /// <param name="to">收件人邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="htmlBody">HTML内容</param>
        /// <param name="cc">抄送</param>
        /// <returns>发送结果</returns>
        public static async Task<bool> SendHtmlAsync(string to, string subject, string htmlBody, string? cc = null)
        {
            return await SendEmailAsync(to, subject, htmlBody, true, null, cc);
        }

        /// <summary>
        /// 发送带附件的邮件
        /// </summary>
        /// <param name="to">收件人邮箱</param>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="attachments">附件路径列表</param>
        /// <param name="isHtml">是否为HTML内容</param>
        /// <param name="cc">抄送</param>
        /// <returns>发送结果</returns>
        public static async Task<bool> SendWithAttachmentsAsync(string to, string subject, string body, List<string> attachments, bool isHtml = false, string? cc = null)
        {
            return await SendEmailAsync(to, subject, body, isHtml, attachments, cc);
        }

        /// <summary>
        /// 发送邮件核心方法
        /// </summary>
        private static async Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml, List<string>? attachments = null, string? cc = null)
        {
            try
            {
                if (_mailOption == null)
                {
                    throw new InvalidOperationException($"邮件配置未初始化，请先调用 Initialize 方法");
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_mailOption.FromName, _mailOption.FromEmail));
                message.To.Add(MailboxAddress.Parse(to));

                if (!string.IsNullOrEmpty(cc))
                {
                    message.Cc.Add(MailboxAddress.Parse(cc));
                }

                message.Subject = subject;

                var builder = new BodyBuilder();
                if (isHtml)
                {
                    builder.HtmlBody = body;
                }
                else
                {
                    builder.TextBody = body;
                }

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        builder.Attachments.Add(attachment);
                    }
                }

                message.Body = builder.ToMessageBody();

                using var client = new SmtpClient();
                await client.ConnectAsync(_mailOption.Host, _mailOption.Port, _mailOption.UseSsl ? SecureSocketOptions.SslOnConnect : SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_mailOption.UserName, _mailOption.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                // 这里可以添加日志记录
                Console.WriteLine($"发送邮件时发生错误: {ex.Message}");
                return false;
            }
        }
    }
} 




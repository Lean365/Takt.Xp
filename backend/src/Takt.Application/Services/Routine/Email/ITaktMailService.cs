//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktMailService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件服务接口
//===================================================================

using System.Threading.Tasks;
using System.Collections.Generic;
using Takt.Shared.Models;
using Takt.Application.Dtos.Routine;

namespace Takt.Application.Services.Routine.Email
{
    /// <summary>
    /// 邮件服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktMailService
    {
        /// <summary>
        /// 获取邮件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>邮件分页列表</returns>
        Task<TaktPagedResult<TaktMailDto>> GetListAsync(TaktMailQueryDto query);

        /// <summary>
        /// 获取邮件详情
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>邮件详情</returns>
        Task<TaktMailDto> GetByIdAsync(long mailId);

        /// <summary>
        /// 创建邮件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>邮件ID</returns>
        Task<long> CreateAsync(TaktMailCreateDto input);

        /// <summary>
        /// 更新邮件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktMailUpdateDto input);

        /// <summary>
        /// 删除邮件
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long mailId);

        /// <summary>
        /// 批量删除邮件
        /// </summary>
        /// <param name="mailIds">邮件ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] mailIds);

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="input">发送邮件参数</param>
        /// <returns>是否成功</returns>
        Task<bool> SendAsync(TaktMailSendDto input);

        /// <summary>
        /// 批量发送邮件
        /// </summary>
        /// <param name="inputs">发送邮件参数集合</param>
        /// <returns>发送结果</returns>
        Task<(int success, int fail)> BatchSendAsync(List<TaktMailSendDto> inputs);

        /// <summary>
        /// 标记邮件为已读
        /// </summary>
        /// <param name="id">邮件ID</param>
        Task<bool> MarkAsReadAsync(long id);

        /// <summary>
        /// 标记所有邮件为已读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsReadAsync(long userId);

        /// <summary>
        /// 标记邮件为未读
        /// </summary>
        /// <param name="id">邮件ID</param>
        Task<bool> MarkAsUnreadAsync(long id);

        /// <summary>
        /// 标记所有邮件为未读
        /// </summary>
        /// <param name="userId">用户ID</param>
        Task<int> MarkAllAsUnreadAsync(long userId);

        /// <summary>
        /// 导出邮件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktMailQueryDto query, string sheetName = "邮件信息");

        /// <summary>
        /// 获取指定用户的邮件状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy);
    }
} 





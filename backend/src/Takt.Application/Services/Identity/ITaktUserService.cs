//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktUserService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户服务接口
//===================================================================

using Takt.Application.Dtos.Identity;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-17
    /// </remarks>
    public interface ITaktUserService
    {
        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用户分页列表</returns>
        Task<TaktPagedResult<TaktUserDto>> GetListAsync(TaktUserQueryDto query);

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户详情</returns>
        Task<TaktUserDto> GetByIdAsync(long userId);

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用户ID</returns>
        Task<long> CreateAsync(TaktUserCreateDto input);

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktUserUpdateDto input);

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long userId);

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] userIds);

        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktUserStatusDto input);

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码对象</param>
        /// <returns>是否成功</returns>
        Task<bool> ResetPasswordAsync(TaktUserResetPwdDto input);

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码对象</param>
        /// <returns>是否成功</returns>
        Task<bool> ChangePasswordAsync(TaktUserChangePwdDto input);

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="input">解锁用户信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UnlockUserAsync(TaktUserUnlockDto input);

        /// <summary>
        /// 更新用户个人信息
        /// </summary>
        /// <param name="input">个人信息更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateProfileAsync(TaktUserProfileUpdateDto input);

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input">头像更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAvatarAsync(TaktUserAvatarUpdateDto input);

        /// <summary>
        /// 获取用户角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户角色列表</returns>
        Task<List<TaktUserRoleDto>> GetUserRoleIdsAsync(long userId);

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户部门列表</returns>
        Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long userId);

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>用户岗位列表</returns>
        Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long userId);




        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignUserRolesAsync(long userId, long[] roleIds);

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignUserDeptsAsync(long userId, long[] deptIds);

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="postIds">岗位ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignUserPostsAsync(long userId, long[] postIds);


        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName);      

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName);

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktUserQueryDto query, string? sheetName, string? fileName);

  
    }
}




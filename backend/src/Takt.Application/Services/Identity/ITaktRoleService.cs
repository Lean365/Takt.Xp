//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktRoleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务接口
//===================================================================

using System.Collections.Generic;
using System.Threading.Tasks;
using Takt.Shared.Models;
using Takt.Application.Dtos.Identity;
using System.IO;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// </remarks>
    public interface ITaktRoleService
    {
        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>分页结果</returns>
        Task<TaktPagedResult<TaktRoleDto>> GetListAsync(TaktRoleQueryDto query);

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色详情</returns>
        Task<TaktRoleDto> GetByIdAsync(long roleId);

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        Task<long> CreateAsync(TaktRoleCreateDto input);

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktRoleUpdateDto input);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long roleId);

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="ids">角色ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] ids);



        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktRoleStatusDto input);



        /// <summary>
        /// 获取角色选项列表
        /// </summary>
        /// <returns>角色选项列表</returns>
        Task<List<TaktSelectOption>> GetOptionsAsync();

        /// <summary>
        /// 获取角色部门列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色部门列表</returns>
        Task<List<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long roleId);

        /// <summary>
        /// 获取角色菜单列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>角色菜单列表</returns>
        Task<List<TaktRoleMenuDto>> GetRoleMenuIdsAsync(long roleId);

        /// <summary>
        /// 分配角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuIds">菜单ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignRoleMenusAsync(long roleId, long[] menuIds);

        /// <summary>
        /// 分配角色用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignRoleUsersAsync(long roleId, long[] userIds);

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        Task<bool> AssignRoleDeptsAsync(long roleId, long[] deptIds);
    


        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);


                /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktRoleQueryDto query, string sheetName);

        /// <summary>
        /// 获取角色导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);        
    }
} 





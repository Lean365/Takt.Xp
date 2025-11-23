//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktRoleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 角色服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 角色服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class TaktRoleService : TaktBaseService, ITaktRoleService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktRoleService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取角色仓储
        /// </summary>
        private ITaktRepository<TaktRole> RoleRepository => _repositoryFactory.GetAuthRepository<TaktRole>();

        /// <summary>
        /// 获取用户角色关联仓储
        /// </summary>
        private ITaktRepository<TaktUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<TaktUserRole>();

        /// <summary>
        /// 获取角色菜单关联仓储
        /// </summary>
        private ITaktRepository<TaktRoleMenu> RoleMenuRepository => _repositoryFactory.GetAuthRepository<TaktRoleMenu>();

        /// <summary>
        /// 获取角色部门关联仓储
        /// </summary>
        private ITaktRepository<TaktRoleDept> RoleDeptRepository => _repositoryFactory.GetAuthRepository<TaktRoleDept>();

        /// <summary>
        /// 获取角色分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktRoleDto>> GetListAsync(TaktRoleQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await RoleRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktRoleDto>
            {
                Rows = result.Rows.Adapt<List<TaktRoleDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取角色详情
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>返回角色详细信息</returns>
        /// <exception cref="TaktException">当角色不存在时抛出异常</exception>
        public async Task<TaktRoleDto> GetByIdAsync(long roleId)
        {
            var role = await RoleRepository.GetByIdAsync(roleId);
            if (role == null)
                throw new TaktException(L("Identity.Role.NotFound", roleId));

            return role.Adapt<TaktRoleDto>();
        }

        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>角色ID</returns>
        public async Task<long> CreateAsync(TaktRoleCreateDto input)
        {

            // 验证角色名称是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleName", input.RoleName);

            // 验证角色编码是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleKey", input.RoleKey);

            var role = input.Adapt<TaktRole>();
            return await RoleRepository.CreateAsync(role) > 0 ? role.Id : throw new TaktException(L("Identity.Role.CreateFailed"));
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktRoleUpdateDto input)
        {
            var role = await RoleRepository.GetByIdAsync(input.RoleId)
                ?? throw new TaktException(L("Identity.Role.NotFound", input.RoleId));

            // 禁止修改系统管理员角色
            if (role.RoleKey == "system_admin")
                throw new TaktException("系统管理员角色不允许修改！");

            // 验证角色名称是否已存在
            if (role.RoleName != input.RoleName)
                await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleName", input.RoleName, input.RoleId);

            // 验证角色编码是否已存在
            if (role.RoleKey != input.RoleKey)
                await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleKey", input.RoleKey, input.RoleId);

            input.Adapt(role);
            return await RoleRepository.UpdateAsync(role) > 0;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long roleId)
        {
            var role = await RoleRepository.GetByIdAsync(roleId)
                ?? throw new TaktException(L("Identity.Role.NotFound", roleId));

            // 禁止删除系统管理员角色
            if (role.RoleKey == "system_admin")
                throw new TaktException("系统管理员角色不允许删除！");

            // 检查是否有用户关联
            var hasUsers = await UserRoleRepository.AsQueryable().AnyAsync(x => x.RoleId == roleId);
            if (hasUsers)
                throw new TaktException(L("Identity.Role.HasUsers"));

            // 删除角色及其关联数据
            await RoleMenuRepository.DeleteAsync((Expression<Func<TaktRoleMenu, bool>>)(x => x.RoleId == roleId));
            return await RoleRepository.DeleteAsync(roleId) > 0;
        }

        /// <summary>
        /// 批量删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] roleIds)
        {
            if (roleIds == null || roleIds.Length == 0)
                throw new TaktException(L("Identity.Role.SelectRequired"));

            foreach (var roleId in roleIds)
            {
                var role = await RoleRepository.GetByIdAsync(roleId);
                if (role == null) continue;
                // 禁止删除系统管理员角色
                if (role.RoleKey == "system_admin")
                    throw new TaktException($"系统管理员角色不允许删除！(ID: {roleId})");
                var hasUsers = await UserRoleRepository.AsQueryable().AnyAsync(x => x.RoleId == roleId);
                if (hasUsers)
                    throw new TaktException(L("Identity.Role.HasUsersWithId", roleId));
            }

            // 删除角色及其关联数据
            await RoleMenuRepository.DeleteAsync((Expression<Func<TaktRoleMenu, bool>>)(x => roleIds.Contains(x.RoleId)));
            return await RoleRepository.DeleteRangeAsync(roleIds.Cast<object>().ToList()) > 0;
        }


        /// <summary>
        /// 更新角色状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktRoleStatusDto input)
        {
            var role = await RoleRepository.GetByIdAsync(input.RoleId)
                ?? throw new TaktException(L("Identity.Role.NotFound", input.RoleId));

            // 禁止修改系统管理员角色状态
            if (role.RoleKey == "system_admin")
                throw new TaktException("系统管理员角色状态不允许修改！");

            input.Adapt(role);
            return await RoleRepository.UpdateAsync(role) > 0;
        }

        /// <summary>
        /// 获取角色选项列表
        /// </summary>
        /// <returns>角色选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var roles = await RoleRepository.AsQueryable()
                .Where(r => r.RoleStatus == 0 && r.IsDeleted == 0)  // 只获取正常状态且未删除的角色
                .OrderBy(r => r.RoleName)
                .Select(r => new TaktSelectOption
                {
                    DictLabel = r.RoleName,
                    DictValue = r.Id,
                })
                .ToListAsync();
            return roles;
        }

        /// <summary>
        /// 获取角色已分配的部门列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>部门列表</returns>
        public async Task<List<TaktRoleDeptDto>> GetRoleDeptIdsAsync(long roleId)
        {
            var roleDepts = await RoleDeptRepository.GetListAsync(rd => rd.RoleId == roleId && rd.IsDeleted == 0);
            return roleDepts.Adapt<List<TaktRoleDeptDto>>();
        }

        /// <summary>
        /// 获取角色已分配的菜单列表
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns>菜单列表</returns>
        public async Task<List<TaktRoleMenuDto>> GetRoleMenuIdsAsync(long roleId)
        {
            var roleMenus = await RoleMenuRepository.GetListAsync(rm => rm.RoleId == roleId && rm.IsDeleted == 0);
            return roleMenus.Select(rm => new TaktRoleMenuDto
            {
                RoleMenuId = rm.Id,
                RoleId = rm.RoleId,
                MenuId = rm.MenuId,
                CreateTime = rm.CreateTime,
                CreateBy = rm.CreateBy
            }).ToList();
        }

        /// <summary>
        /// 分配角色菜单
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="menuIds">菜单ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignRoleMenusAsync(long roleId, long[] menuIds)
        {
            try
            {
                _logger.Info($"开始分配角色菜单 - 角色ID: {roleId}, 菜单IDs: {string.Join(",", menuIds)}");

                // 获取角色信息并检查是否为系统管理员角色
                var role = await RoleRepository.GetByIdAsync(roleId);
                if (role == null)
                    throw new TaktException(L("Identity.Role.NotFound", roleId));

                // 禁止对系统管理员角色进行菜单分配
                if (role.RoleKey == "system_admin")
                    throw new TaktException("系统管理员角色不允许进行菜单分配！");

                // 1. 获取角色现有关联的菜单（包括已删除的）
                var existingMenus = await RoleMenuRepository.GetListAsync(rm => rm.RoleId == roleId);
                _logger.Info($"角色现有关联菜单数量: {existingMenus.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的菜单列表中）
                var menusToDelete = existingMenus.Where(rm => !menuIds.Contains(rm.MenuId)).ToList();
                if (menusToDelete.Any())
                {
                    _logger.Info($"需要标记删除的菜单关联数量: {menusToDelete.Count}, 菜单IDs: {string.Join(",", menusToDelete.Select(d => d.MenuId))}");
                    foreach (var menu in menusToDelete)
                    {
                        menu.IsDeleted = 1; // 1 表示已删除
                        menu.DeleteBy = _currentUser.UserName;
                        menu.DeleteTime = DateTime.Now;
                        menu.UpdateBy = _currentUser.UserName;
                        menu.UpdateTime = DateTime.Now;
                        await RoleMenuRepository.UpdateAsync(menu);
                    }
                    _logger.Info("菜单关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的菜单列表中且已存在但被标记为删除）
                var menusToRestore = existingMenus.Where(rm => menuIds.Contains(rm.MenuId) && rm.IsDeleted == 1).ToList();
                if (menusToRestore.Any())
                {
                    _logger.Info($"需要恢复的菜单关联数量: {menusToRestore.Count}, 菜单IDs: {string.Join(",", menusToRestore.Select(d => d.MenuId))}");
                    foreach (var menu in menusToRestore)
                    {
                        menu.IsDeleted = 0; // 0 表示未删除
                        menu.DeleteBy = null;
                        menu.DeleteTime = null;
                        menu.UpdateBy = _currentUser.UserName;
                        menu.UpdateTime = DateTime.Now;
                        await RoleMenuRepository.UpdateAsync(menu);
                    }
                    _logger.Info("菜单关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的菜单列表中且不存在任何记录）
                var existingMenuIds = existingMenus.Select(rm => rm.MenuId).ToList();
                var menusToAdd = menuIds.Where(menuId => !existingMenuIds.Contains(menuId))
                    .Select(menuId => new TaktRoleMenu
                    {
                        RoleId = roleId,
                        MenuId = menuId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (menusToAdd.Any())
                {
                    _logger.Info($"需要新增的菜单关联数量: {menusToAdd.Count}, 菜单IDs: {string.Join(",", menusToAdd.Select(d => d.MenuId))}");
                    await RoleMenuRepository.CreateRangeAsync(menusToAdd);
                    _logger.Info("菜单关联新增完成");
                }

                _logger.Info("角色菜单分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色菜单失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配角色用户
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignRoleUsersAsync(long roleId, long[] userIds)
        {
            try
            {
                _logger.Info($"开始分配角色用户 - 角色ID: {roleId}, 用户IDs: {string.Join(",", userIds)}");

                // 1. 获取角色现有关联的用户（包括已删除的）
                var existingUsers = await UserRoleRepository.GetListAsync(ur => ur.RoleId == roleId);
                _logger.Info($"角色现有关联用户数量: {existingUsers.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
                var usersToDelete = existingUsers.Where(ur => !userIds.Contains(ur.UserId)).ToList();
                if (usersToDelete.Any())
                {
                    _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                    foreach (var user in usersToDelete)
                    {
                        user.IsDeleted = 1; // 1 表示已删除
                        user.DeleteBy = _currentUser.UserName;
                        user.DeleteTime = DateTime.Now;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
                var usersToRestore = existingUsers.Where(ur => userIds.Contains(ur.UserId) && ur.IsDeleted == 1).ToList();
                if (usersToRestore.Any())
                {
                    _logger.Info($"需要恢复的用户关联数量: {usersToRestore.Count}, 用户IDs: {string.Join(",", usersToRestore.Select(d => d.UserId))}");
                    foreach (var user in usersToRestore)
                    {
                        user.IsDeleted = 0; // 0 表示未删除
                        user.DeleteBy = null;
                        user.DeleteTime = null;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
                var existingUserIds = existingUsers.Select(ur => ur.UserId).ToList();
                var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                    .Select(userId => new TaktUserRole
                    {
                        RoleId = roleId,
                        UserId = userId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (usersToAdd.Any())
                {
                    _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                    await UserRoleRepository.CreateRangeAsync(usersToAdd);
                    _logger.Info("用户关联新增完成");
                }

                _logger.Info("角色用户分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色用户失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignRoleDeptsAsync(long roleId, long[] deptIds)
        {
            try
            {
                _logger.Info($"开始分配角色部门 - 角色ID: {roleId}, 部门IDs: {string.Join(",", deptIds)}");

                // 获取角色信息并检查是否为系统管理员角色
                var role = await RoleRepository.GetByIdAsync(roleId);
                if (role == null)
                    throw new TaktException(L("Identity.Role.NotFound", roleId));

                // 禁止对系统管理员角色进行部门分配
                if (role.RoleKey == "system_admin")
                    throw new TaktException("系统管理员角色不允许进行部门分配！");

                // 1. 获取角色现有关联的部门（包括已删除的）
                var existingDepts = await RoleDeptRepository.GetListAsync(rd => rd.RoleId == roleId);
                _logger.Info($"角色现有关联部门数量: {existingDepts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
                var deptsToDelete = existingDepts.Where(rd => !deptIds.Contains(rd.DeptId)).ToList();
                if (deptsToDelete.Any())
                {
                    foreach (var dept in deptsToDelete)
                    {
                        dept.IsDeleted = 1;
                        dept.DeleteBy = _currentUser.UserName;
                        dept.DeleteTime = DateTime.Now;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await RoleDeptRepository.UpdateAsync(dept);
                    }
                }

                // 3. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
                var deptsToRestore = existingDepts.Where(rd => deptIds.Contains(rd.DeptId) && rd.IsDeleted == 1).ToList();
                if (deptsToRestore.Any())
                {
                    foreach (var dept in deptsToRestore)
                    {
                        dept.IsDeleted = 0;
                        dept.DeleteBy = null;
                        dept.DeleteTime = null;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await RoleDeptRepository.UpdateAsync(dept);
                    }
                }

                // 4. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
                var existingDeptIds = existingDepts.Select(rd => rd.DeptId).ToList();
                var deptsToAdd = deptIds.Where(deptId => !existingDeptIds.Contains(deptId))
                    .Select(deptId => new TaktRoleDept
                    {
                        RoleId = roleId,
                        DeptId = deptId,
                        IsDeleted = 0,
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();
                if (deptsToAdd.Any())
                {
                    await RoleDeptRepository.CreateRangeAsync(deptsToAdd);
                }

                _logger.Info("角色部门分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配角色部门失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktRoleTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导入角色数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            try
            {
                var importDtos = await TaktExcelHelper.ImportAsync<TaktRoleImportDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new TaktException(L("Identity.Role.ImportEmpty"));

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        var role = item.Adapt<TaktRole>();
                        role.CreateTime = DateTime.Now;
                        role.RoleStatus = 0;

                        // 验证角色名称是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleName", role.RoleName);
                        // 验证角色编码是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(RoleRepository, "RoleKey", role.RoleKey);

                        await RoleRepository.CreateAsync(role);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("Identity.Role.Log.ImportFailed", ex.Message), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Role.Log.ImportDataFailed"), ex);
                throw new TaktException(L("Identity.Role.ImportFailed"));
            }
        }

        /// <summary>
        /// 导出角色数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktRoleQueryDto query, string sheetName)
        {
            try
            {
                var list = await RoleRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<TaktRoleExportDto>>();
                return await TaktExcelHelper.ExportAsync(exportList, sheetName, "Role");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Role.Log.ExportDataFailed"), ex);
                throw new TaktException(L("Identity.Role.ExportFailed"));
            }
        }
        /// <summary>
        /// 构建角色查询条件
        /// </summary>
        private Expression<Func<TaktRole, bool>> QueryExpression(TaktRoleQueryDto query)
        {
            var exp = Expressionable.Create<TaktRole>();

            if (!string.IsNullOrEmpty(query.RoleName))
                exp.And(x => x.RoleName.Contains(query.RoleName));

            if (!string.IsNullOrEmpty(query.RoleKey))
                exp.And(x => x.RoleKey.Contains(query.RoleKey));

            if (query.RoleStatus.HasValue && query.RoleStatus.Value != -1)
                exp.And(x => x.RoleStatus == query.RoleStatus.Value);

            return exp.ToExpression();
        }
    }
}




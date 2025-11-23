//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedRelation.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 关联关系数据初始化类 - 使用仓储工厂模式
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 关联关系数据初始化类
/// </summary>
public class TaktDbSeedRelationCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;

    private ITaktRepository<TaktUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<TaktUserRole>();
    private ITaktRepository<TaktUserPost> UserPostRepository => _repositoryFactory.GetAuthRepository<TaktUserPost>();
    private ITaktRepository<TaktUserDept> UserDeptRepository => _repositoryFactory.GetAuthRepository<TaktUserDept>();
    private ITaktRepository<TaktRoleMenu> RoleMenuRepository => _repositoryFactory.GetAuthRepository<TaktRoleMenu>();
    private ITaktRepository<TaktRoleDept> RoleDeptRepository => _repositoryFactory.GetAuthRepository<TaktRoleDept>();
    private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();
    private ITaktRepository<TaktRole> RoleRepository => _repositoryFactory.GetAuthRepository<TaktRole>();
    private ITaktRepository<TaktPost> PostRepository => _repositoryFactory.GetAuthRepository<TaktPost>();
    private ITaktRepository<TaktDept> DeptRepository => _repositoryFactory.GetAuthRepository<TaktDept>();
    private ITaktRepository<TaktMenu> MenuRepository => _repositoryFactory.GetAuthRepository<TaktMenu>();

    /// <summary>
    /// 构造函数
    /// </summary>
    public TaktDbSeedRelationCoordinator(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger)
    {
        _repositoryFactory = repositoryFactory;
        _logger = logger;
    }

    /// <summary>
    /// 初始化所有关联关系数据
    /// </summary>
    public async Task<(int, int)> InitializeRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;


        // 初始化角色部门关联关系
        var (roleDeptInsert, roleDeptUpdate) = await InitializeRoleDeptRelationsAsync();
        insertCount += roleDeptInsert;
        updateCount += roleDeptUpdate;

        // 初始化角色菜单关联关系
        var (roleMenuInsert, roleMenuUpdate) = await InitializeRoleMenuRelationsAsync();
        insertCount += roleMenuInsert;
        updateCount += roleMenuUpdate;

        // 初始化用户角色关联关系
        var (userRoleInsert, userRoleUpdate) = await InitializeUserRoleRelationsAsync();
        insertCount += userRoleInsert;
        updateCount += userRoleUpdate;

        // 初始化用户部门关联关系
        var (userDeptInsert, userDeptUpdate) = await InitializeUserDeptRelationsAsync();
        insertCount += userDeptInsert;
        updateCount += userDeptUpdate;

        // 初始化用户岗位关联关系
        var (userPostInsert, userPostUpdate) = await InitializeUserPostRelationsAsync();
        insertCount += userPostInsert;
        updateCount += userPostUpdate;

        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户角色关联关系
    /// </summary>
    private async Task<(int, int)> InitializeUserRoleRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有用户和角色
        var allUsers = await UserRepository.GetListAsync(u => u.IsDeleted == 0);
        var allRoles = await RoleRepository.GetListAsync(r => r.IsDeleted == 0);

        _logger.Info($"[初始化] 开始处理用户角色关联关系，用户数量：{allUsers.Count}，角色数量：{allRoles.Count}");
        _logger.Info($"[用户列表] {string.Join(", ", allUsers.Select(u => u.UserName))}");
        _logger.Info($"[角色列表] {string.Join(", ", allRoles.Select(r => $"{r.RoleName}({r.RoleKey})"))}");

        foreach (var user in allUsers)
        {
            _logger.Info($"[处理] 开始处理用户 {user.UserName} 的角色关联");

            // 根据用户名称分配角色
            var role = allRoles.FirstOrDefault(r =>
            {
                // 系统管理员角色
                if (user.UserName.Contains("admin")) return r.RoleKey == "system_admin";

                // 开发相关角色
                if (user.UserName.Contains("dev")) return r.RoleKey == "code_manager";
                if (user.UserName.Contains("test")) return r.RoleKey == "code_manager";

                // 业务相关角色
                if (user.UserName.Contains("security")) return r.RoleKey == "security_admin";
                if (user.UserName.Contains("biz")) return r.RoleKey == "biz_manager";
                if (user.UserName.Contains("hr")) return r.RoleKey == "hr_manager";
                if (user.UserName.Contains("fin")) return r.RoleKey == "fin_manager";
                if (user.UserName.Contains("prod")) return r.RoleKey == "prod_manager";
                if (user.UserName.Contains("qc")) return r.RoleKey == "qc_manager";
                if (user.UserName.Contains("wh")) return r.RoleKey == "wh_manager";
                if (user.UserName.Contains("inv")) return r.RoleKey == "inv_manager";
                if (user.UserName.Contains("purchase")) return r.RoleKey == "purchase_manager";
                if (user.UserName.Contains("supplier")) return r.RoleKey == "supplier_manager";
                if (user.UserName.Contains("sales")) return r.RoleKey == "sales_manager";
                if (user.UserName.Contains("customer")) return r.RoleKey == "customer_manager";
                if (user.UserName.Contains("master")) return r.RoleKey == "master_manager";
                if (user.UserName.Contains("code")) return r.RoleKey == "code_manager";

                // 默认角色
                return r.RoleKey == "general_user"; // 默认一般用户角色
            });

            // 如果没找到匹配的角色，使用一般用户角色
            if (role == null)
            {
                role = allRoles.FirstOrDefault(r => r.RoleKey == "general_user");
                if (role == null)
                {
                    // 如果连一般用户角色都找不到，使用访客角色
                    role = allRoles.FirstOrDefault(r => r.RoleKey == "guest");
                    if (role == null)
                    {
                        // 如果连访客角色都找不到，使用第一个可用角色
                        role = allRoles.First();
                        _logger.Warn($"[警告] 未找到一般用户或访客角色，为用户 {user.UserName} 使用角色 {role.RoleName}");
                    }
                    else
                    {
                        _logger.Info($"[信息] 为用户 {user.UserName} 使用访客角色 {role.RoleName}");
                    }
                }
                else
                {
                    _logger.Info($"[信息] 为用户 {user.UserName} 使用默认角色 {role.RoleName}");
                }
            }

            if (role != null)
            {
                _logger.Info($"[处理] 为用户 {user.UserName} 找到角色 {role.RoleName}({role.RoleKey})");

                // 检查是否存在用户-角色关联
                var existingUserRole = await UserRoleRepository.GetFirstAsync(ur => ur.UserId == user.Id);

                // 如果不存在关联，创建新的关联
                if (existingUserRole == null)
                {
                    var userRole = new TaktUserRole
                    {
                        UserId = user.Id,
                        RoleId = role.Id,
                        IsDeleted = 0,
                        CreateBy = "Takt365",
                        CreateTime = DateTime.Now,
                        UpdateBy = "Takt365",
                        UpdateTime = DateTime.Now
                    };

                    await UserRoleRepository.CreateAsync(userRole);
                    insertCount++;
                    _logger.Info($"[创建] 用户角色关联 - 用户:{user.UserName}, 角色:{role.RoleName}({role.RoleKey})");
                }
                // 如果存在关联，更新关联
                else
                {
                    existingUserRole.RoleId = role.Id;
                    existingUserRole.IsDeleted = 0;
                    existingUserRole.DeleteBy = null;
                    existingUserRole.DeleteTime = null;
                    existingUserRole.UpdateBy = "Takt365";
                    existingUserRole.UpdateTime = DateTime.Now;
                    await UserRoleRepository.UpdateAsync(existingUserRole);
                    updateCount++;
                    _logger.Info($"[更新] 用户角色关联 - 用户:{user.UserName}, 角色:{role.RoleName}({role.RoleKey})");
                }
            }
            else
            {
                _logger.Error($"[错误] 未找到适合用户 {user.UserName} 的角色");
            }
        }

        _logger.Info($"[完成] 用户角色关联关系处理完成，插入：{insertCount}，更新：{updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户部门关联关系
    /// </summary>
    private async Task<(int, int)> InitializeUserDeptRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有用户和部门
        var allUsers = await UserRepository.GetListAsync(u => u.IsDeleted == 0);
        var allDepts = await DeptRepository.GetListAsync(d => d.IsDeleted == 0);

        _logger.Info($"[初始化] 开始处理用户部门关联关系，用户数量：{allUsers.Count}，部门数量：{allDepts.Count}");

        // 获取根部门（IT部）
        var rootDept = allDepts.FirstOrDefault(d => d.DeptName == "IT部");
        if (rootDept == null)
        {
            _logger.Error("[错误] 未找到IT部");
            return (0, 0);
        }

        // 获取所有子部门
        var childDepts = allDepts.Where(d => d.ParentId == rootDept.Id).ToList();
        _logger.Info($"[部门结构] 根部门：{rootDept.DeptName}，子部门数量：{childDepts.Count}");

        foreach (var user in allUsers)
        {
            // 检查是否存在用户-部门关联
            var existingUserDept = await UserDeptRepository.GetFirstAsync(ud => ud.UserId == user.Id);

            // 如果不存在关联，创建新的关联
            if (existingUserDept == null)
            {
                var userDept = new TaktUserDept
                {
                    UserId = user.Id,
                    DeptId = rootDept.Id,
                    IsDeleted = 0,
                    CreateBy = "Takt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Takt365",
                    UpdateTime = DateTime.Now
                };

                await UserDeptRepository.CreateAsync(userDept);
                insertCount++;
                _logger.Info($"[创建] 用户部门关联 - 用户:{user.UserName}, 部门:{rootDept.DeptName}");
            }
            // 如果存在关联，更新关联
            else
            {
                existingUserDept.DeptId = rootDept.Id;
                existingUserDept.IsDeleted = 0;
                existingUserDept.DeleteBy = null;
                existingUserDept.DeleteTime = null;
                existingUserDept.UpdateBy = "Takt365";
                existingUserDept.UpdateTime = DateTime.Now;
                await UserDeptRepository.UpdateAsync(existingUserDept);
                updateCount++;
                _logger.Info($"[更新] 用户部门关联 - 用户:{user.UserName}, 部门:{rootDept.DeptName}");
            }
        }

        _logger.Info($"[完成] 用户部门关联关系处理完成，插入：{insertCount}，更新：{updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化用户岗位关联关系
    /// </summary>
    private async Task<(int, int)> InitializeUserPostRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有用户和岗位
        var allUsers = await UserRepository.GetListAsync(u => u.IsDeleted == 0);
        var allPosts = await PostRepository.GetListAsync(p => p.IsDeleted == 0);

        _logger.Info($"[初始化] 开始处理用户岗位关联关系，用户数量：{allUsers.Count}，岗位数量：{allPosts.Count}");

        if (!allPosts.Any())
        {
            _logger.Error("[错误] 未找到任何岗位，无法创建用户岗位关联");
            return (0, 0);
        }

        // 获取第一个岗位作为默认岗位
        var defaultPost = allPosts.First();

        foreach (var user in allUsers)
        {
            // 获取用户当前的岗位关联
            var existingUserPost = await UserPostRepository.GetFirstAsync(up => up.UserId == user.Id);

            // 如果用户已有岗位关联，则更新为默认岗位
            if (existingUserPost != null)
            {
                existingUserPost.PostId = defaultPost.Id;
                existingUserPost.IsDeleted = 0;
                existingUserPost.DeleteBy = null;
                existingUserPost.DeleteTime = null;
                existingUserPost.UpdateBy = "Takt365";
                existingUserPost.UpdateTime = DateTime.Now;
                await UserPostRepository.UpdateAsync(existingUserPost);
                updateCount++;
                _logger.Info($"[更新] 用户岗位关联 - 用户:{user.UserName}, 岗位:{defaultPost.PostName}");
            }
            // 如果用户没有岗位关联，则创建新的关联
            else
            {
                var userPost = new TaktUserPost
                {
                    UserId = user.Id,
                    PostId = defaultPost.Id,
                    IsDeleted = 0,
                    CreateBy = "Takt365",
                    CreateTime = DateTime.Now,
                    UpdateBy = "Takt365",
                    UpdateTime = DateTime.Now
                };

                await UserPostRepository.CreateAsync(userPost);
                insertCount++;
                _logger.Info($"[创建] 用户岗位关联 - 用户:{user.UserName}, 岗位:{defaultPost.PostName}");
            }
        }

        _logger.Info($"[完成] 用户岗位关联关系处理完成，插入：{insertCount}，更新：{updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化角色部门关联关系
    /// </summary>
    private async Task<(int, int)> InitializeRoleDeptRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有角色和部门
        var allRoles = await RoleRepository.GetListAsync(r => r.IsDeleted == 0);
        var allDepts = await DeptRepository.GetListAsync(d => d.IsDeleted == 0);

        _logger.Info($"[初始化] 开始处理角色部门关联关系，角色数量：{allRoles.Count}，部门数量：{allDepts.Count}");

        // 获取根部门（IT部）
        var rootDept = allDepts.FirstOrDefault(d => d.DeptName == "IT部");
        if (rootDept == null)
        {
            _logger.Error("[错误] 未找到IT部");
            return (0, 0);
        }

        // 获取所有子部门
        var childDepts = allDepts.Where(d => d.ParentId == rootDept.Id).ToList();
        _logger.Info($"[部门结构] 根部门：{rootDept.DeptName}，子部门数量：{childDepts.Count}");

        foreach (var role in allRoles)
        {
            var depts = allDepts.Where(d =>
            {
                // 系统管理员可以管理所有部门
                if (role.RoleKey == "system_admin") return true;

                // 安全管理员管理安全相关部门
                if (role.RoleKey == "security_admin")
                    return d.DeptName.Contains("安全") || d.DeptName.Contains("IT");

                // 业务管理员管理业务相关部门
                if (role.RoleKey == "biz_manager")
                    return d.DeptName.Contains("业务") || d.DeptName.Contains("管理");

                // 人事管理员管理人事相关部门
                if (role.RoleKey == "hr_manager")
                    return d.DeptName.Contains("人事") || d.DeptName.Contains("人力资源");

                // 财务管理员管理财务相关部门
                if (role.RoleKey == "fin_manager")
                    return d.DeptName.Contains("财务") || d.DeptName.Contains("会计");

                // 生产管理员管理生产相关部门
                if (role.RoleKey == "prod_manager")
                    return d.DeptName.Contains("生产") || d.DeptName.Contains("制造");

                // 品质管理员管理品质相关部门
                if (role.RoleKey == "qc_manager")
                    return d.DeptName.Contains("品质") || d.DeptName.Contains("质量");

                // 仓库管理员管理仓库相关部门
                if (role.RoleKey == "wh_manager")
                    return d.DeptName.Contains("仓库") || d.DeptName.Contains("仓储");

                // 库存管理员管理库存相关部门
                if (role.RoleKey == "inv_manager")
                    return d.DeptName.Contains("库存") || d.DeptName.Contains("仓储");

                // 采购管理员管理采购相关部门
                if (role.RoleKey == "purchase_manager")
                    return d.DeptName.Contains("采购") || d.DeptName.Contains("供应");

                // 供应商管理员管理供应商相关部门
                if (role.RoleKey == "supplier_manager")
                    return d.DeptName.Contains("供应商") || d.DeptName.Contains("供应");

                // 销售管理员管理销售相关部门
                if (role.RoleKey == "sales_manager")
                    return d.DeptName.Contains("销售") || d.DeptName.Contains("市场");

                // 客户管理员管理客户相关部门
                if (role.RoleKey == "customer_manager")
                    return d.DeptName.Contains("客户") || d.DeptName.Contains("服务");

                return false;
            }).ToList();

            foreach (var dept in depts)
            {
                // 查找现有关联
                var existingRoleDept = await RoleDeptRepository.GetFirstAsync(rd =>
                    rd.RoleId == role.Id && rd.DeptId == dept.Id);

                if (existingRoleDept == null)
                {
                    // 创建新的关联
                    var roleDept = new TaktRoleDept
                    {
                        RoleId = role.Id,
                        DeptId = dept.Id,
                        IsDeleted = 0,
                        CreateBy = "Takt365",
                        CreateTime = DateTime.Now,
                        UpdateBy = "Takt365",
                        UpdateTime = DateTime.Now
                    };
                    await RoleDeptRepository.CreateAsync(roleDept);
                    insertCount++;
                    _logger.Info($"[创建] 角色部门关联 - 角色:{role.RoleName}, 部门:{dept.DeptName}");
                }
                else
                {
                    // 更新现有关联
                    existingRoleDept.IsDeleted = 0;
                    existingRoleDept.DeleteBy = null;
                    existingRoleDept.DeleteTime = null;
                    existingRoleDept.UpdateTime = DateTime.Now;
                    existingRoleDept.UpdateBy = "Takt365";
                    await RoleDeptRepository.UpdateAsync(existingRoleDept);
                    updateCount++;
                    _logger.Info($"[更新] 角色部门关联 - 角色:{role.RoleName}, 部门:{dept.DeptName}");
                }
            }
        }

        _logger.Info($"[完成] 角色部门关联关系处理完成，插入：{insertCount}，更新：{updateCount}");
        return (insertCount, updateCount);
    }

    /// <summary>
    /// 初始化角色菜单关联关系
    /// </summary>
    private async Task<(int, int)> InitializeRoleMenuRelationsAsync()
    {
        int insertCount = 0;
        int updateCount = 0;

        // 获取所有角色
        var allRoles = await RoleRepository.GetListAsync(r => r.IsDeleted == 0);
        // 获取所有菜单
        var allMenus = await MenuRepository.GetListAsync(m => m.IsDeleted == 0);
        _logger.Info($"[初始化] 开始处理角色菜单关联关系，角色数量：{allRoles.Count}，菜单数量：{allMenus.Count}");

        // 只处理系统管理员和一般用户角色
        var targetRoles = allRoles.Where(r => r.RoleKey == "system_admin" || r.RoleKey == "general_user").ToList();
        _logger.Info($"[角色] 将处理以下角色的菜单关联：{string.Join(", ", targetRoles.Select(r => r.RoleName))}");

        foreach (var role in targetRoles)
        {
            try
            {
                // 获取该角色的所有现有关联
                var existingRelations = await RoleMenuRepository.GetListAsync(
                    rm => rm.RoleId == role.Id);

                // 创建现有关联的字典，用于快速查找
                var existingRelationDict = existingRelations.ToDictionary(rm => rm.MenuId);

                foreach (var menu in allMenus)
                {
                    if (existingRelationDict.TryGetValue(menu.Id, out var existingRelation))
                    {
                        // 如果关联已存在，更新关联
                        existingRelation.IsDeleted = 0;
                        existingRelation.DeleteBy = null;
                        existingRelation.DeleteTime = null;
                        existingRelation.UpdateTime = DateTime.Now;
                        existingRelation.UpdateBy = "Takt365";
                        await RoleMenuRepository.UpdateAsync(existingRelation);
                        updateCount++;
                        _logger.Info($"[更新] 角色 {role.RoleName} 与菜单 {menu.MenuName} 的关联");
                    }
                    else
                    {
                        // 创建新的关联
                        var roleMenu = new TaktRoleMenu
                        {
                            RoleId = role.Id,
                            MenuId = menu.Id,
                            IsDeleted = 0,
                            CreateBy = "Takt365",
                            CreateTime = DateTime.Now,
                            UpdateBy = "Takt365",
                            UpdateTime = DateTime.Now
                        };
                        await RoleMenuRepository.CreateAsync(roleMenu);
                        insertCount++;
                        _logger.Info($"[创建] 角色 {role.RoleName} 与菜单 {menu.MenuName} 建立关联");
                    }
                }
                _logger.Info($"[完成] 角色 {role.RoleName} 的菜单关联处理完成，新增：{insertCount}，更新：{updateCount}");
            }
            catch (Exception ex)
            {
                _logger.Error($"[错误] 处理角色 {role.RoleName} 的菜单关联时出错：{ex.Message}");
                throw; // 重新抛出异常，确保错误不被忽略
            }
        }

        _logger.Info("[完成] 角色菜单关联关系初始化完成");
        return (insertCount, updateCount);
    }

}


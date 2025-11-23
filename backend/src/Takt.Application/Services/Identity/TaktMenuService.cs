//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMenuService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 菜单服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 菜单服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class TaktMenuService : TaktBaseService, ITaktMenuService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        private ITaktRepository<TaktMenu> MenuRepository => _repositoryFactory.GetAuthRepository<TaktMenu>();
        private ITaktRepository<TaktRoleMenu> RoleMenuRepository => _repositoryFactory.GetAuthRepository<TaktRoleMenu>();
        private ITaktRepository<TaktUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<TaktUserRole>();
        private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();

        /// <summary>
        /// 构造函数，注入依赖服务
        /// </summary>
        /// <param name="logger">日志记录器</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMenuService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取菜单分页列表
        /// </summary>
        /// <param name="query">查询条件，包含页码、每页大小、菜单名称、状态等</param>
        /// <returns>返回分页后的菜单列表</returns>
        public async Task<TaktPagedResult<TaktMenuDto>> GetListAsync(TaktMenuQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await MenuRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktMenuDto>
            {
                Rows = result.Rows.Adapt<List<TaktMenuDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取菜单详情
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>返回菜单详细信息</returns>
        /// <exception cref="TaktException">当菜单不存在时抛出异常</exception>
        public async Task<TaktMenuDto> GetByIdAsync(long menuId)
        {
            var menu = await MenuRepository.GetByIdAsync(menuId);
            if (menu == null)
                throw new TaktException(L("Identity.Menu.NotFound", menuId));

            return menu.Adapt<TaktMenuDto>();
        }

        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>菜单ID</returns>
        public async Task<long> CreateAsync(TaktMenuCreateDto input)
        {
            try
            {
                _logger.Info($"[菜单服务] 开始创建菜单: MenuName={input.MenuName}");

                // 验证菜单名称是否已存在
                await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "MenuName", input.MenuName);

                // 验证翻译键是否已存在
                if (!string.IsNullOrEmpty(input.TransKey))
                {
                    await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "TransKey", input.TransKey);
                }

                var menu = input.Adapt<TaktMenu>();
                var result = await MenuRepository.CreateAsync(menu);

                if (result > 0)
                {
                    _logger.Info($"[菜单服务] 菜单创建成功: MenuId={menu.Id}, MenuName={menu.MenuName}");

                    // 根据菜单名称获取新创建的菜单ID
                    var newMenu = await MenuRepository.AsQueryable()
                        .Where(m => m.MenuName == input.MenuName)
                        .FirstAsync();

                    if (newMenu != null)
                    {
                        // 获取 admin 用户ID
                        var adminUser = await UserRepository.AsQueryable()
                            .Where(u => u.UserName == "admin")
                            .FirstAsync();

                        if (adminUser != null)
                        {
                            // 获取 admin 用户的角色ID
                            var adminUserRole = await UserRoleRepository.AsQueryable()
                                .Where(ur => ur.UserId == adminUser.Id)
                                .FirstAsync();

                            if (adminUserRole != null)
                            {
                                // 检查角色菜单关联是否存在
                                var existingRoleMenu = await RoleMenuRepository.AsQueryable()
                                    .Where(rm => rm.RoleId == adminUserRole.RoleId && rm.MenuId == newMenu.Id)
                                    .FirstAsync();

                                if (existingRoleMenu != null)
                                {
                                    // 更新现有的角色菜单关联
                                    existingRoleMenu.UpdateTime = DateTime.Now;
                                    await RoleMenuRepository.UpdateAsync(existingRoleMenu);
                                    _logger.Info($"[菜单服务] 更新角色菜单关联: RoleId={adminUserRole.RoleId}, MenuId={newMenu.Id}");
                                }
                                else
                                {
                                    // 创建新的角色菜单关联
                                    var roleMenu = new TaktRoleMenu
                                    {
                                        RoleId = adminUserRole.RoleId,
                                        MenuId = newMenu.Id,
                                        CreateTime = DateTime.Now
                                    };
                                    await RoleMenuRepository.CreateAsync(roleMenu);
                                    _logger.Info($"[菜单服务] 创建角色菜单关联: RoleId={adminUserRole.RoleId}, MenuId={newMenu.Id}");
                                }
                            }
                        }
                    }
                    return newMenu.Id;
                }

                _logger.Error($"[菜单服务] 菜单创建失败: MenuName={input.MenuName}");
                throw new TaktException(L("Identity.Menu.CreateFailed"));
            }
            catch (TaktException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 创建菜单时发生异常: {ex.Message}", ex);
                throw new TaktException(L("Identity.Menu.CreateFailed"));
            }
        }

        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktMenuUpdateDto input)
        {
            var menu = await MenuRepository.GetByIdAsync(input.MenuId)
                ?? throw new TaktException(L("Identity.Menu.NotFound", input.MenuId));

            // 验证菜单名称是否已被其他记录使用
            if (menu.MenuName != input.MenuName)
                await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "MenuName", input.MenuName, input.MenuId);

            // 验证翻译键是否已被其他记录使用
            if (!string.IsNullOrEmpty(input.TransKey) && menu.TransKey != input.TransKey)
                await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "TransKey", input.TransKey, input.MenuId);

            // 检查是否存在循环引用
            if (input.ParentId != null && input.ParentId == input.MenuId)
                throw new TaktException(L("Identity.Menu.CannotBeParentOfItself"));

            // 检查父菜单是否存在
            if (input.ParentId > 0)
            {
                var parentMenu = await MenuRepository.GetByIdAsync(input.ParentId.Value);
                if (parentMenu == null)
                    throw new TaktException(L("Identity.Menu.ParentNotFound"));
            }

            input.Adapt(menu);
            var result = await MenuRepository.UpdateAsync(menu);

            if (result > 0)
            {
                // 根据菜单名称获取更新的菜单ID
                var updatedMenu = await MenuRepository.AsQueryable()
                    .Where(m => m.MenuName == input.MenuName)
                    .FirstAsync();

                if (updatedMenu != null)
                {
                    // 获取 admin 用户ID
                    var adminUser = await UserRepository.AsQueryable()
                        .Where(u => u.UserName == "admin")
                        .FirstAsync();

                    if (adminUser != null)
                    {
                        // 获取 admin 用户的角色ID
                        var adminUserRole = await UserRoleRepository.AsQueryable()
                            .Where(ur => ur.UserId == adminUser.Id)
                            .FirstAsync();

                        if (adminUserRole != null)
                        {
                            // 检查角色菜单关联是否存在
                            var existingRoleMenu = await RoleMenuRepository.AsQueryable()
                                .Where(rm => rm.RoleId == adminUserRole.RoleId && rm.MenuId == updatedMenu.Id)
                                .FirstAsync();

                            if (existingRoleMenu != null)
                            {
                                // 更新现有的角色菜单关联
                                existingRoleMenu.UpdateTime = DateTime.Now;
                                await RoleMenuRepository.UpdateAsync(existingRoleMenu);
                            }
                            else
                            {
                                // 创建新的角色菜单关联
                                var roleMenu = new TaktRoleMenu
                                {
                                    RoleId = adminUserRole.RoleId,
                                    MenuId = updatedMenu.Id,
                                    CreateTime = DateTime.Now
                                };
                                await RoleMenuRepository.CreateAsync(roleMenu);
                            }
                        }
                    }
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="menuId">菜单ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long menuId)
        {
            try
            {
                _logger.Info($"[菜单服务] 开始删除菜单: MenuId={menuId}");

                var menu = await MenuRepository.GetByIdAsync(menuId)
                    ?? throw new TaktException(L("Identity.Menu.NotFound", menuId));

                // 检查是否有子菜单
                var hasChildren = await MenuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
                if (hasChildren)
                {
                    _logger.Warn($"[菜单服务] 删除失败：菜单存在子菜单: MenuId={menuId}");
                    throw new TaktException(L("Identity.Menu.HasChildren"));
                }

                // 更新菜单状态和可见性
                menu.MenuStatus = 1; // 停用状态
                menu.Visible = 1; // 隐藏
                await MenuRepository.UpdateAsync(menu);
                _logger.Info($"[菜单服务] 已更新菜单状态和可见性: MenuId={menuId}, Status=1(停用), Visible=1(隐藏)");

                // 删除菜单及其关联数据
                var roleMenus = await RoleMenuRepository.AsQueryable()
                    .Where(x => x.MenuId == menuId)
                    .ToListAsync();

                foreach (var roleMenu in roleMenus)
                {
                    await RoleMenuRepository.DeleteAsync(roleMenu.Id);
                }
                _logger.Info($"[菜单服务] 已删除角色菜单关联: MenuId={menuId}");

                var result = await MenuRepository.DeleteAsync(menuId);
                if (result > 0)
                {
                    _logger.Info($"[菜单服务] 菜单删除成功: MenuId={menuId}, MenuName={menu.MenuName}");
                    return true;
                }

                _logger.Error($"[菜单服务] 菜单删除失败: MenuId={menuId}");
                return false;
            }
            catch (TaktException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 删除菜单时发生异常: MenuId={menuId}, Error={ex.Message}", ex);
                throw new TaktException(L("Identity.Menu.DeleteFailed"));
            }
        }

        /// <summary>
        /// 批量删除菜单
        /// </summary>
        /// <param name="menuIds">菜单ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] menuIds)
        {
            try
            {
                _logger.Info($"[菜单服务] 开始批量删除菜单: MenuIds={string.Join(",", menuIds)}");

                if (menuIds == null || menuIds.Length == 0)
                {
                    _logger.Warn("[菜单服务] 批量删除失败：未选择菜单");
                    throw new TaktException(L("Identity.Menu.SelectRequired"));
                }

                // 检查是否有子菜单
                foreach (var menuId in menuIds)
                {
                    var hasChildren = await MenuRepository.AsQueryable().AnyAsync(x => x.ParentId == menuId);
                    if (hasChildren)
                    {
                        _logger.Warn($"[菜单服务] 批量删除失败：菜单存在子菜单: MenuId={menuId}");
                        throw new TaktException(L("Identity.Menu.HasChildrenWithId", menuId));
                    }
                }

                // 更新菜单状态和可见性
                var menus = await MenuRepository.AsQueryable()
                    .Where(x => menuIds.Contains(x.Id))
                    .ToListAsync();

                foreach (var menu in menus)
                {
                    menu.MenuStatus = 1; // 停用状态
                    menu.Visible = 1; // 隐藏
                    await MenuRepository.UpdateAsync(menu);
                }
                _logger.Info($"[菜单服务] 已更新菜单状态和可见性: MenuIds={string.Join(",", menuIds)}, Status=1(停用), Visible=1(隐藏)");

                // 删除菜单及其关联数据
                var roleMenus = await RoleMenuRepository.AsQueryable()
                    .Where(x => menuIds.Contains(x.MenuId))
                    .ToListAsync();

                foreach (var roleMenu in roleMenus)
                {
                    await RoleMenuRepository.DeleteAsync(roleMenu.Id);
                }
                _logger.Info($"[菜单服务] 已删除角色菜单关联: MenuIds={string.Join(",", menuIds)}");

                var result = await MenuRepository.DeleteRangeAsync(menuIds.Cast<object>().ToList());
                if (result > 0)
                {
                    _logger.Info($"[菜单服务] 批量删除菜单成功: MenuIds={string.Join(",", menuIds)}");
                    return true;
                }

                _logger.Error($"[菜单服务] 批量删除菜单失败: MenuIds={string.Join(",", menuIds)}");
                return false;
            }
            catch (TaktException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 批量删除菜单时发生异常: MenuIds={string.Join(",", menuIds)}, Error={ex.Message}", ex);
                throw new TaktException(L("Identity.Menu.BatchDeleteFailed"));
            }
        }

        /// <summary>
        /// 导入菜单数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导入结果(success:成功数量,fail:失败数量)</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            try
            {
                var importDtos = await TaktExcelHelper.ImportAsync<TaktMenuImportDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new TaktException(L("Identity.Menu.ImportEmpty"));

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.MenuName))
                        {
                            _logger.Warn("导入菜单失败: 菜单名称不能为空");
                            fail++;
                            continue;
                        }

                        // 验证菜单名称是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "MenuName", item.MenuName);

                        // 验证翻译键是否已存在
                        if (!string.IsNullOrEmpty(item.TransKey))
                        {
                            await TaktValidateUtils.ValidateFieldExistsAsync(MenuRepository, "TransKey", item.TransKey);
                        }

                        var menu = item.Adapt<TaktMenu>();
                        menu.CreateTime = DateTime.Now;
                        menu.MenuStatus = 0;

                        await MenuRepository.CreateAsync(menu);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error(L("Identity.Menu.Log.ImportFailed", ex.Message), ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Menu.Log.ImportDataFailed"), ex);
                throw new TaktException(L("Identity.Menu.ImportFailed"));
            }
        }

        /// <summary>
        /// 导出菜单数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktMenuQueryDto query, string sheetName)
        {
            try
            {
                var list = await MenuRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<TaktMenuExportDto>>();
                return await TaktExcelHelper.ExportAsync(exportList, sheetName, "Menu");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Menu.Log.ExportDataFailed"), ex);
                throw new TaktException(L("Identity.Menu.ExportFailed"));
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktMenuImportDto>(sheetName);
        }

        /// <summary>
        /// 获取菜单选项列表
        /// </summary>
        /// <returns>菜单选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var menus = await MenuRepository.AsQueryable()
                .Where(m => m.MenuStatus == 0 && m.IsDeleted == 0)  // 只获取正常状态且未删除的菜单
                .OrderBy(m => m.MenuName)
                .Select(m => new TaktSelectOption
                {
                    DictLabel = m.MenuName,
                    DictValue = m.Id,
                })
                .ToListAsync();
            return menus;
        }

        /// <summary>
        /// 获取树形菜单选项列表
        /// </summary>
        /// <returns>树形菜单选项列表</returns>
        public async Task<List<TaktSelectOption>> GetTreeOptionsAsync()
        {
            var menus = await MenuRepository.AsQueryable()
                .Where(m => m.MenuStatus == 0 && m.IsDeleted == 0)  // 只获取正常状态且未删除的菜单
                .OrderBy(m => m.OrderNum)
                .ToListAsync();

            var menuDtos = menus.Adapt<List<TaktMenuDto>>();

            // 构建树形结构
            var tree = menuDtos.Where(m => m.ParentId == 0).ToList();
            foreach (var node in tree)
            {
                BuildMenuTree(node, menuDtos);
            }

            // 转换为选项列表，包含层级缩进
            var options = new List<TaktSelectOption>();
            void AddMenuToOptions(TaktMenuDto menu, int level = 0)
            {
                var indent = new string('　', level * 2); // 使用全角空格作为缩进
                options.Add(new TaktSelectOption
                {
                    DictLabel = indent + menu.MenuName,
                    DictValue = menu.MenuId,
                    ExtLabel = menu.MenuName,
                    ExtValue = menu.MenuName,
                    TransKey = null,
                    CssClass = 1,
                    ListClass = 1,
                    OrderNum = menu.OrderNum,
                    Status = menu.MenuStatus
                });

                if (menu.Children != null && menu.Children.Any())
                {
                    foreach (var child in menu.Children.OrderBy(c => c.OrderNum))
                    {
                        AddMenuToOptions(child, level + 1);
                    }
                }
            }

            foreach (var menu in tree.OrderBy(m => m.OrderNum))
            {
                AddMenuToOptions(menu);
            }

            return options;
        }

        /// <summary>
        /// 更新菜单状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktMenuStatusDto input)
        {
            var menu = await MenuRepository.GetByIdAsync(input.MenuId)
                ?? throw new TaktException(L("Identity.Menu.NotFound", input.MenuId));

            input.Adapt(menu);
            return await MenuRepository.UpdateAsync(menu) > 0;
        }

        /// <summary>
        /// 更新菜单排序
        /// </summary>
        /// <param name="input">排序更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateOrderAsync(TaktMenuOrderDto input)
        {
            var menu = await MenuRepository.GetByIdAsync(input.MenuId)
                ?? throw new TaktException(L("Identity.Menu.NotFound", input.MenuId));

            input.Adapt(menu);
            return await MenuRepository.UpdateAsync(menu) > 0;
        }

        /// <summary>
        /// 获取菜单树形结构
        /// </summary>
        /// <returns>返回菜单树形结构列表</returns>
        public async Task<List<TaktMenuDto>> GetTreeAsync(TaktMenuQueryDto query = null)
        {
            try
            {
                _logger.Info("[菜单服务] 开始获取菜单树");

                // 获取所有菜单
                var queryable = MenuRepository.AsQueryable()
                    .OrderBy(m => m.OrderNum);

                // 应用查询条件
                if (query != null)
                {
                    var exp = QueryExpression(query);
                    queryable = queryable.Where(exp);
                }

                var menus = await queryable.ToListAsync();

                _logger.Info($"[菜单服务] 从数据库获取菜单数: {menus?.Count ?? 0}");

                // 如果有查询条件，需要获取所有相关的子菜单
                if (query != null && !string.IsNullOrEmpty(query.MenuName))
                {
                    var matchedMenuIds = menus.Select(m => m.Id).ToList();
                    var childMenuIds = new List<long>();

                    // 递归获取所有子菜单
                    while (true)
                    {
                        var childMenus = await MenuRepository.AsQueryable()
                            .Where(m => matchedMenuIds.Contains(m.ParentId))
                            .ToListAsync();

                        if (!childMenus.Any()) break;

                        childMenuIds.AddRange(childMenus.Select(m => m.Id));
                        matchedMenuIds = childMenus.Select(m => m.Id).ToList();
                    }

                    // 获取所有相关的菜单
                    if (childMenuIds.Any())
                    {
                        var allRelatedMenus = await MenuRepository.AsQueryable()
                            .Where(m => childMenuIds.Contains(m.Id))
                            .ToListAsync();
                        menus.AddRange(allRelatedMenus);
                    }
                }

                // 转换为DTO
                var menuDtos = menus.Adapt<List<TaktMenuDto>>();

                _logger.Info($"[菜单服务] 转换后的菜单DTO数: {menuDtos?.Count ?? 0}");

                // 构建树形结构
                List<TaktMenuDto> tree;

                if (query != null && !string.IsNullOrEmpty(query.MenuName))
                {
                    // 如果有查询条件，找到匹配的菜单项
                    var matchedMenus = menuDtos.Where(m => m.MenuName.Contains(query.MenuName)).ToList();
                    if (matchedMenus.Any())
                    {
                        // 从匹配的菜单项开始构建树
                        tree = matchedMenus;
                        foreach (var node in tree)
                        {
                            BuildMenuTree(node, menuDtos);
                        }
                    }
                    else
                    {
                        // 如果没有找到匹配的菜单，返回空列表
                        tree = new List<TaktMenuDto>();
                    }
                }
                else
                {
                    // 如果没有查询条件，从顶级菜单开始构建树
                    tree = menuDtos.Where(m => m.ParentId == 0).ToList();
                    foreach (var node in tree)
                    {
                        BuildMenuTree(node, menuDtos);
                    }
                }

                _logger.Info("[菜单服务] 菜单树构建完成");
                return tree;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 获取菜单树时发生错误: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 递归构建菜单树
        /// </summary>
        /// <param name="node">当前节点</param>
        /// <param name="allMenus">所有菜单列表</param>
        private void BuildMenuTree(TaktMenuDto node, List<TaktMenuDto> allMenus)
        {
            try
            {
                var children = allMenus.Where(m => m.ParentId == node.MenuId).ToList();
                if (children.Any())
                {
                    node.Children = children;
                    foreach (var child in children)
                    {
                        BuildMenuTree(child, allMenus);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 构建节点 {node.MenuName}({node.MenuId}) 的子树时发生错误: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取当前用户的菜单列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户有权限访问的菜单树形结构</returns>
        public async Task<List<TaktMenuDto>> GetCurrentUserMenusAsync(long userId)
        {
            try
            {
                _logger.Info($"[菜单服务] 开始获取用户菜单: UserId={userId}");

                // 获取用户的角色ID列表
                var roleIdsQuery = UserRoleRepository.AsQueryable()
                    .Where(ur => ur.UserId == userId)
                    .Select(ur => ur.RoleId);

                var roleIds = await roleIdsQuery.ToListAsync();

                _logger.Info($"[菜单服务] 用户角色: Count={roleIds?.Count ?? 0}, RoleIds={string.Join(",", roleIds ?? new List<long>())}");

                if (roleIds == null || !roleIds.Any())
                {
                    _logger.Warn("[菜单服务] 用户没有角色");
                    return new List<TaktMenuDto>();
                }

                // 获取角色的菜单ID列表
                var menuIdsQuery = RoleMenuRepository.AsQueryable()
                    .Where(rm => roleIds.Contains(rm.RoleId))
                    .Select(rm => rm.MenuId)
                    .Distinct();

                var menuIds = await menuIdsQuery.ToListAsync();

                if (menuIds == null || !menuIds.Any())
                {
                    _logger.Warn("[菜单服务] 角色没有关联菜单");
                    return new List<TaktMenuDto>();
                }

                // 获取菜单列表 - 过滤掉按钮类型(MenuType=2)的菜单
                var menusQuery = MenuRepository.AsQueryable()
                    .Where(m => menuIds.Contains(m.Id) && m.MenuStatus == 0 && m.MenuType != 2)
                    .OrderBy(m => m.OrderNum);

                var menus = await menusQuery.ToListAsync();

                // 转换为DTO
                var menuDtos = menus.Adapt<List<TaktMenuDto>>();

                // 构建树形结构
                var tree = menuDtos.Where(m => m.ParentId == 0).ToList();
                foreach (var node in tree)
                {
                    BuildMenuTree(node, menuDtos);
                }

                return tree;
            }
            catch (Exception ex)
            {
                _logger.Error($"[菜单服务] 获取用户菜单失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 构建菜单查询条件
        /// </summary>
        private Expression<Func<TaktMenu, bool>> QueryExpression(TaktMenuQueryDto query)
        {
            var exp = Expressionable.Create<TaktMenu>();

            if (!string.IsNullOrEmpty(query.MenuName))
                exp.And(x => x.MenuName.Contains(query.MenuName));

            if (query.Status.HasValue && query.Status.Value != -1)
                exp.And(x => x.MenuStatus == query.Status.Value);

            return exp.ToExpression();
        }
    }
}




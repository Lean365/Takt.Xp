//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDeptService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 部门服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 部门服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class TaktDeptService : TaktBaseService, ITaktDeptService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        private ITaktRepository<TaktDept> DeptRepository => _repositoryFactory.GetAuthRepository<TaktDept>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktDeptService(
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktRepositoryFactory repositoryFactory,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取部门分页列表
        /// </summary>
        public async Task<TaktPagedResult<TaktDeptDto>> GetListAsync(TaktDeptQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await DeptRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktDeptDto>
            {
                Rows = result.Rows.Adapt<List<TaktDeptDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取部门树形结构
        /// </summary>
        public async Task<List<TaktDeptDto>> GetTreeAsync(TaktDeptQueryDto query)
        {
            // 1. 查询所有部门
            var allDepts = await DeptRepository.AsQueryable()
                .OrderBy(d => d.OrderNum)
                .ToListAsync();

            _logger.Info($"原始部门数据数量: {allDepts.Count}");
            foreach (var dept in allDepts)
            {
                _logger.Info($"原始数据: {dept.DeptName}, ID: {dept.Id}, ParentID: {dept.ParentId}");
            }

            // 2. 转换为DTO
            var dtos = allDepts.Adapt<List<TaktDeptDto>>();

            // 3. 调试：检查根节点
            var rootDepts = dtos.Where(d => d.ParentId == 0).ToList();
            _logger.Info($"根节点数量: {rootDepts.Count}");
            foreach (var root in rootDepts)
            {
                _logger.Info($"根节点: {root.DeptName}, ID: {root.DeptId}, ParentID: {root.ParentId}");
            }

            // 4. 构建树形结构
            var tree = rootDepts;
            foreach (var node in tree)
            {
                BuildDeptTreeRecursive(node, dtos);
            }

            return tree;
        }

        /// <summary>
        /// 获取部门详情
        /// </summary>
        public async Task<TaktDeptDto> GetByIdAsync(long id)
        {
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == id);
            if (dept == null)
                throw new TaktException("Identity.Dept.NotFound", id.ToString());
            return dept.Adapt<TaktDeptDto>();
        }

        /// <summary>
        /// 创建部门
        /// </summary>
        public async Task<long> CreateAsync(TaktDeptCreateDto input)
        {
            if (string.IsNullOrEmpty(input.DeptName))
                throw new TaktException(L("Identity.Dept.Name.Required"));

            // 验证部门名称是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(DeptRepository, "DeptName", input.DeptName);

            var dept = input.Adapt<TaktDept>();
            dept.CreateTime = DateTime.Now;
            dept.DeptStatus = 1;

            await DeptRepository.CreateAsync(dept);
            return dept.Id;
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        public async Task<bool> UpdateAsync(TaktDeptUpdateDto input)
        {
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == input.DeptId);
            if (dept == null)
            {
                throw new TaktException(L("Identity.Dept.NotFound", input.DeptId));
            }

            // 验证部门名称是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldExistsAsync(DeptRepository, "DeptName", input.DeptName, input.DeptId);

            input.Adapt(dept);
            dept.UpdateTime = DateTime.Now;

            return await DeptRepository.UpdateAsync(dept) > 0;
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            var dept = await DeptRepository.GetFirstAsync(x => x.Id == id);
            if (dept == null)
            {
                throw new InvalidOperationException($"Identity.Dept.Operation.DeleteFailed: {id}");
            }

            return await DeptRepository.DeleteAsync(dept) > 0;
        }

        /// <summary>
        /// 批量删除部门
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] deptIds)
        {
            // 1.检查是否存在子部门
            var hasChildren = await DeptRepository.AsQueryable()
                .AnyAsync(d => deptIds.Contains(d.ParentId == 0 ? 0 : d.ParentId));
            if (hasChildren)
                throw new TaktException("Identity.Dept.HasChildren");

            // 2.批量删除
            Expression<Func<TaktDept, bool>> condition = d => deptIds.Contains(d.Id);
            var result = await DeptRepository.DeleteAsync(condition);
            return result > 0;
        }

        /// <summary>
        /// 修改部门状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(long deptId, int status)
        {
            var entity = await DeptRepository.GetByIdAsync(deptId);
            if (entity == null)
            {
                return false;
            }

            entity.DeptStatus = status;
            await DeptRepository.UpdateAsync(entity);
            return true;
        }

        /// <summary>
        /// 获取部门选项列表
        /// </summary>
        /// <returns>部门选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var depts = await DeptRepository.AsQueryable()
                .Where(x => x.DeptStatus == 0 && x.IsDeleted == 0)  // 只获取正常状态且未删除的部门
                .OrderBy(x => x.DeptName)
                .Select(x => new TaktSelectOption
                {
                    DictLabel = x.DeptName,
                    DictValue = x.Id,
                })
                .ToListAsync();
            return depts;
        }

        /// <summary>
        /// 获取树形部门选项列表
        /// </summary>
        /// <returns>树形部门选项列表</returns>
        public async Task<List<TaktSelectOption>> GetTreeOptionsAsync()
        {
            var depts = await DeptRepository.AsQueryable()
                .Where(x => x.DeptStatus == 0 && x.IsDeleted == 0)  // 只获取正常状态且未删除的部门
                .OrderBy(x => x.OrderNum)
                .ToListAsync();

            var deptDtos = depts.Adapt<List<TaktDeptDto>>();

            // 构建树形结构
            var tree = deptDtos.Where(d => d.ParentId == 0).ToList();
            foreach (var node in tree)
            {
                BuildDeptTreeRecursive(node, deptDtos);
            }

            // 转换为选项列表，包含层级缩进
            var options = new List<TaktSelectOption>();
            void AddDeptToOptions(TaktDeptDto dept, int level = 0)
            {
                var indent = new string('　', level * 2); // 使用全角空格作为缩进
                options.Add(new TaktSelectOption
                {
                    DictLabel = indent + dept.DeptName,
                    DictValue = dept.DeptId,
                    ExtLabel = dept.DeptName,
                    ExtValue = dept.DeptName,
                    TransKey = null,
                    CssClass = 1,
                    ListClass = 1,
                    OrderNum = dept.OrderNum,
                    Status = dept.DeptStatus
                });

                if (dept.Children != null && dept.Children.Any())
                {
                    foreach (var child in dept.Children.OrderBy(c => c.OrderNum))
                    {
                        AddDeptToOptions(child, level + 1);
                    }
                }
            }

            foreach (var dept in tree.OrderBy(d => d.OrderNum))
            {
                AddDeptToOptions(dept);
            }

            return options;
        }

        /// <summary>
        /// 获取用户部门列表
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <returns>用户部门列表</returns>
        public async Task<List<TaktUserDeptDto>> GetUserDeptsAsync(long deptId)
        {
            var userDeptRepository = _repositoryFactory.GetAuthRepository<TaktUserDept>();
            var userDepts = await userDeptRepository.GetListAsync(ud => ud.DeptId == deptId && ud.IsDeleted == 0);
            return userDepts.Adapt<List<TaktUserDeptDto>>();
        }

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="userIds">用户ID数组</param>
        /// <returns>是否分配成功</returns>
        public async Task<bool> AssignUserDeptsAsync(long deptId, long[] userIds)
        {
            try
            {
                _logger.Info($"开始分配用户部门 - 部门ID: {deptId}, 用户IDs: {string.Join(",", userIds)}");

                // 1. 验证部门是否存在且状态正常
                var dept = await DeptRepository.GetByIdAsync(deptId);
                if (dept == null)
                    throw new TaktException(L("Identity.Dept.NotFound"));
                if (dept.DeptStatus != 0)
                    throw new TaktException(L("Identity.Dept.Disabled"));

                // 2. 获取部门现有关联的用户（包括已删除的）
                var userDeptRepository = _repositoryFactory.GetAuthRepository<TaktUserDept>();
                var existingUsers = await userDeptRepository.GetListAsync(ud => ud.DeptId == deptId);
                _logger.Info($"部门现有关联用户数量: {existingUsers.Count}");

                // 3. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
                var usersToDelete = existingUsers.Where(ud => !userIds.Contains(ud.UserId)).ToList();
                if (usersToDelete.Any())
                {
                    _logger.Info($"需要标记删除的用户关联数量: {usersToDelete.Count}, 用户IDs: {string.Join(",", usersToDelete.Select(d => d.UserId))}");
                    foreach (var user in usersToDelete)
                    {
                        // 标记删除
                        user.IsDeleted = 1; // 1 表示已删除
                        user.DeleteBy = _currentUser.UserName;
                        user.DeleteTime = DateTime.Now;
                        await userDeptRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联状态更新和删除标记完成");
                }

                // 4. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
                var usersToRestore = existingUsers.Where(ud => userIds.Contains(ud.UserId) && ud.IsDeleted == 1).ToList();
                if (usersToRestore.Any())
                {
                    _logger.Info($"需要恢复的用户关联数量: {usersToRestore.Count}, 用户IDs: {string.Join(",", usersToRestore.Select(d => d.UserId))}");
                    foreach (var user in usersToRestore)
                    {
                        // 取消删除标记
                        user.IsDeleted = 0; // 0 表示未删除
                        user.DeleteBy = null;
                        user.DeleteTime = null;
                        await userDeptRepository.UpdateAsync(user);
                    }
                    _logger.Info("用户关联状态恢复和删除标记取消完成");
                }

                // 5. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
                var existingUserIds = existingUsers.Select(ud => ud.UserId).ToList();
                var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                    .Select(userId => new TaktUserDept
                    {
                        UserId = userId,
                        DeptId = deptId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (usersToAdd.Any())
                {
                    _logger.Info($"需要新增的用户关联数量: {usersToAdd.Count}, 用户IDs: {string.Join(",", usersToAdd.Select(d => d.UserId))}");
                    await userDeptRepository.CreateRangeAsync(usersToAdd);
                    _logger.Info("用户关联新增完成");
                }

                _logger.Info("用户部门分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户部门失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 获取角色部门列表
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="query">查询条件</param>
        /// <returns>角色部门分页列表</returns>
        public async Task<TaktPagedResult<TaktRoleDeptDto>> GetRoleDeptsAsync(long deptId, TaktRoleQueryDto query)
        {
            try
            {
                // 验证部门是否存在
                var dept = await DeptRepository.GetByIdAsync(deptId);
                if (dept == null)
                    throw new TaktException(L("Identity.Dept.NotFound"));

                // 获取角色部门关联仓储
                var roleDeptRepository = _repositoryFactory.GetAuthRepository<TaktRoleDept>();
                var roleRepository = _repositoryFactory.GetAuthRepository<TaktRole>();

                // 构建查询表达式
                var exp = Expressionable.Create<TaktRoleDept>();
                exp.And(rd => rd.DeptId == deptId && rd.IsDeleted == 0);

                // 如果查询条件包含角色名称，需要关联角色表进行查询
                if (!string.IsNullOrEmpty(query.RoleName))
                {
                    var roleIds = await roleRepository.AsQueryable()
                        .Where(r => r.RoleName.Contains(query.RoleName) && r.IsDeleted == 0)
                        .Select(r => r.Id)
                        .ToListAsync();

                    if (roleIds.Any())
                    {
                        exp.And(rd => roleIds.Contains(rd.RoleId));
                    }
                    else
                    {
                        // 如果没有找到匹配的角色，返回空结果
                        return new TaktPagedResult<TaktRoleDeptDto>
                        {
                            Rows = new List<TaktRoleDeptDto>(),
                            TotalNum = 0,
                            PageIndex = query.PageIndex,
                            PageSize = query.PageSize
                        };
                    }
                }

                // 获取分页数据
                var result = await roleDeptRepository.GetPagedListAsync(
                    exp.ToExpression(),
                    query.PageIndex,
                    query.PageSize,
                    rd => rd.CreateTime,
                    OrderByType.Desc);

                // 获取角色详细信息
                var resultRoleIds = result.Rows.Select(rd => rd.RoleId).ToList();
                var roles = await roleRepository.GetListAsync(r => resultRoleIds.Contains(r.Id));

                // 转换为DTO
                var roleDeptDtos = result.Rows.Select(rd =>
                {
                    var role = roles.FirstOrDefault(r => r.Id == rd.RoleId);
                    return new TaktRoleDeptDto
                    {
                        RoleDeptId = rd.Id,
                        RoleId = rd.RoleId,
                        DeptId = rd.DeptId,
                        CreateTime = rd.CreateTime,
                        CreateBy = rd.CreateBy,
                        UpdateTime = rd.UpdateTime,
                        UpdateBy = rd.UpdateBy,
                        Remark = rd.Remark,
                        RoleName = role?.RoleName,
                        DeptName = dept.DeptName
                    };
                }).ToList();

                return new TaktPagedResult<TaktRoleDeptDto>
                {
                    Rows = roleDeptDtos,
                    TotalNum = result.TotalNum,
                    PageIndex = query.PageIndex,
                    PageSize = query.PageSize
                };
            }
            catch (Exception ex)
            {
                _logger.Error($"获取角色部门列表失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配角色部门
        /// </summary>
        /// <param name="deptId">部门ID</param>
        /// <param name="roleIds">角色ID数组</param>
        /// <returns>是否分配成功</returns>
        public async Task<bool> AssignRoleDeptsAsync(long deptId, long[] roleIds)
        {
            try
            {
                _logger.Info($"开始分配角色部门 - 部门ID: {deptId}, 角色IDs: {string.Join(",", roleIds)}");

                // 1. 验证部门是否存在且状态正常
                var dept = await DeptRepository.GetByIdAsync(deptId);
                if (dept == null)
                    throw new TaktException(L("Identity.Dept.NotFound"));
                if (dept.DeptStatus != 0)
                    throw new TaktException(L("Identity.Dept.Disabled"));

                // 2. 获取部门现有关联的角色（包括已删除的）
                var roleDeptRepository = _repositoryFactory.GetAuthRepository<TaktRoleDept>();
                var existingRoles = await roleDeptRepository.GetListAsync(rd => rd.DeptId == deptId);
                _logger.Info($"部门现有关联角色数量: {existingRoles.Count}");

                // 3. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
                var rolesToDelete = existingRoles.Where(rd => !roleIds.Contains(rd.RoleId)).ToList();
                if (rolesToDelete.Any())
                {
                    _logger.Info($"需要标记删除的角色关联数量: {rolesToDelete.Count}, 角色IDs: {string.Join(",", rolesToDelete.Select(d => d.RoleId))}");
                    foreach (var role in rolesToDelete)
                    {
                        // 标记删除
                        role.IsDeleted = 1; // 1 表示已删除
                        role.DeleteBy = _currentUser.UserName;
                        role.DeleteTime = DateTime.Now;
                        await roleDeptRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联状态更新和删除标记完成");
                }

                // 4. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
                var rolesToRestore = existingRoles.Where(rd => roleIds.Contains(rd.RoleId) && rd.IsDeleted == 1).ToList();
                if (rolesToRestore.Any())
                {
                    _logger.Info($"需要恢复的角色关联数量: {rolesToRestore.Count}, 角色IDs: {string.Join(",", rolesToRestore.Select(d => d.RoleId))}");
                    foreach (var role in rolesToRestore)
                    {
                        // 取消删除标记
                        role.IsDeleted = 0; // 0 表示未删除
                        role.DeleteBy = null;
                        role.DeleteTime = null;
                        await roleDeptRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联状态恢复和删除标记取消完成");
                }

                // 5. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
                var existingRoleIds = existingRoles.Select(rd => rd.RoleId).ToList();
                var rolesToAdd = roleIds.Where(roleId => !existingRoleIds.Contains(roleId))
                    .Select(roleId => new TaktRoleDept
                    {
                        RoleId = roleId,
                        DeptId = deptId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (rolesToAdd.Any())
                {
                    _logger.Info($"需要新增的角色关联数量: {rolesToAdd.Count}, 角色IDs: {string.Join(",", rolesToAdd.Select(d => d.RoleId))}");
                    await roleDeptRepository.CreateRangeAsync(rolesToAdd);
                    _logger.Info("角色关联新增完成");
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
        /// 导入部门数据
        /// </summary>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            try
            {
                var importDtos = await TaktExcelHelper.ImportAsync<TaktDeptTemplateDto>(fileStream, sheetName);
                if (importDtos == null || !importDtos.Any())
                    throw new TaktException("Identity.Dept.ImportEmpty");

                var success = 0;
                var fail = 0;

                foreach (var item in importDtos)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.DeptName))
                        {
                            _logger.Warn("导入部门失败: 部门名称不能为空");
                            fail++;
                            continue;
                        }

                        // 验证部门名称是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(DeptRepository, "DeptName", item.DeptName);

                        var dept = item.Adapt<TaktDept>();
                        dept.CreateTime = DateTime.Now;
                        dept.DeptStatus = 0;

                        await DeptRepository.CreateAsync(dept);
                        success++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("Identity.Dept.Log.ImportFailed", ex.Message, ex);
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception)
            {
                _logger.Error("Identity.Dept.Log.ImportDataFailed");
                throw new TaktException("Identity.Dept.ImportFailed");
            }
        }

        /// <summary>
        /// 导出部门数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktDeptQueryDto query, string sheetName)
        {
            try
            {
                var list = await DeptRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<TaktDeptExportDto>>();
                return await TaktExcelHelper.ExportAsync(exportList, sheetName, "Dept");
            }
            catch (Exception)
            {
                _logger.Error("Identity.Dept.Log.ExportDataFailed");
                throw new TaktException("Identity.Dept.ExportFailed");
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktDeptTemplateDto>(sheetName);
        }



        /// <summary>
        /// 递归构建部门树
        /// </summary>
        private void BuildDeptTreeRecursive(TaktDeptDto parent, List<TaktDeptDto> depts)
        {
            // 查找子部门（排除自引用）
            var children = depts
                .Where(d => d.ParentId == parent.DeptId && d.DeptId != parent.DeptId)
                .OrderBy(d => d.OrderNum)
                .ToList();

            _logger.Info($"部门 {parent.DeptName} (ID: {parent.DeptId}) 找到 {children.Count} 个子部门");
            foreach (var child in children)
            {
                _logger.Info($"  - 子部门: {child.DeptName} (ID: {child.DeptId}, ParentID: {child.ParentId})");
            }

            parent.Children = children;

            // 递归处理子部门
            foreach (var child in children)
            {
                BuildDeptTreeRecursive(child, depts);
            }
        }



        /// <summary>
        /// 构建部门查询条件
        /// </summary>
        private static Expression<Func<TaktDept, bool>> QueryExpression(TaktDeptQueryDto query)
        {
            var exp = Expressionable.Create<TaktDept>();

            if (!string.IsNullOrEmpty(query.DeptName))
                exp.And(x => x.DeptName.Contains(query.DeptName));

            if (query.DeptStatus.HasValue && query.DeptStatus.Value != -1)
                exp.And(x => x.DeptStatus == query.DeptStatus.Value);

            return exp.ToExpression();
        }


    }
}




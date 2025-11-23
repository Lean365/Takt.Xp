//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktUserService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-18 10:00
// 版本号 : V0.0.1
// 描述   : 用户服务实现类
//===================================================================

#nullable enable

using Takt.Domain.IServices.Security;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 用户服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-18
    /// </remarks>
    public class TaktUserService : TaktBaseService, ITaktUserService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktPasswordPolicy _passwordPolicy;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktUserService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            ITaktPasswordPolicy passwordPolicy,
            ITaktCurrentUser currentUser,
            IHttpContextAccessor httpContextAccessor,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _passwordPolicy = passwordPolicy ?? throw new ArgumentNullException(nameof(passwordPolicy));
        }

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();

        /// <summary>
        /// 获取用户角色仓储
        /// </summary>
        private ITaktRepository<TaktUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<TaktUserRole>();

        /// <summary>
        /// 获取用户岗位仓储
        /// </summary>
        private ITaktRepository<TaktUserPost> UserPostRepository => _repositoryFactory.GetAuthRepository<TaktUserPost>();

        /// <summary>
        /// 获取用户部门仓储
        /// </summary>
        private ITaktRepository<TaktUserDept> UserDeptRepository => _repositoryFactory.GetAuthRepository<TaktUserDept>();


        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktUserDto>> GetListAsync(TaktUserQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await UserRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktUserDto>
            {
                Rows = result.Rows.Adapt<List<TaktUserDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回用户详情</returns>
        public async Task<TaktUserDto> GetByIdAsync(long userId)
        {
            var user = await UserRepository.GetByIdAsync(userId);
            if (user == null)
                throw new TaktException(L("Identity.User.NotFound"));

            // 获取用户角色
            var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
            var roleIds = userRoles.Select(ur => ur.RoleId.ToString()).ToList();

            // 获取用户岗位
            var userPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId && up.IsDeleted == 0);
            var postIds = userPosts.Select(up => up.PostId.ToString()).ToList();

            // 获取用户部门
            var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
            var deptIds = userDepts.Select(ud => ud.DeptId.ToString()).ToList();


            var userDto = user.Adapt<TaktUserDto>();
            userDto.RoleIds = roleIds;
            userDto.PostIds = postIds;
            userDto.DeptIds = deptIds;



            return userDto;
        }

        /// <summary>
        /// 获取用户选项列表
        /// </summary>
        /// <returns>用户选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var users = await UserRepository.AsQueryable()
                .Where(u => u.UserStatus == 0 && u.IsDeleted == 0)  // 只获取正常状态且未删除的用户
                .OrderBy(u => u.UserName)
                .Select(u => new TaktSelectOption
                {
                    DictLabel = u.UserName,
                    DictValue = u.Id,
                    ExtLabel = u.RealName,
                    ExtValue = u.Email,
                    OrderNum = 0,
                    Status = u.UserStatus
                })
                .ToListAsync();
            return users;
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="input">用户创建信息</param>
        /// <returns>返回新创建的用户ID</returns>
        public async Task<long> CreateAsync(TaktUserCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            if (string.IsNullOrEmpty(input.UserName))
                throw new TaktException(L("Identity.User.Username.Required"));

            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "UserName", input.UserName);
            await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", input.PhoneNumber);
            await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", input.Email);

            // 使用默认密码或验证用户提供的密码
            var password = string.IsNullOrEmpty(input.Password) ? _passwordPolicy.DefaultPassword : input.Password;

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(password))
                throw new TaktException(L("Identity.User.Password.Invalid"));

            // 创建用户
            var (hash, salt, iterations) = TaktPasswordUtils.CreateHash(password);
            var user = input.Adapt<TaktUser>();
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UserType = 0;
            user.UserStatus = 0;
            user.CreateBy = _currentUser.UserName;
            user.CreateTime = DateTime.Now;

            var result = await UserRepository.CreateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.AddFailed"));

            // 获取创建后的用户ID
            var userId = user.Id;
            if (userId <= 0)
            {
                // 如果仍然无法获取ID，重新查询用户
                var createdUser = await UserRepository.GetFirstAsync(u => u.UserName == input.UserName);
                if (createdUser != null)
                {
                    userId = createdUser.Id;
                }
                else
                {
                    throw new TaktException("创建用户成功但无法获取用户ID");
                }
            }

            // 添加用户角色关联
            if (input.RoleIds != null && input.RoleIds.Any())
            {
                var userRoles = input.RoleIds.Select(roleId => new TaktUserRole
                {
                    UserId = userId,
                    RoleId = long.Parse(roleId),
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserRoleRepository.CreateRangeAsync(userRoles);
            }

            // 添加用户岗位关联
            if (input.PostIds != null && input.PostIds.Any())
            {
                var userPosts = input.PostIds.Select(postId => new TaktUserPost
                {
                    UserId = userId,
                    PostId = long.Parse(postId),
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserPostRepository.CreateRangeAsync(userPosts);
            }

            // 添加用户部门关联
            if (input.DeptIds != null && input.DeptIds.Any())
            {
                var userDepts = input.DeptIds.Select(deptId => new TaktUserDept
                {
                    UserId = userId,
                    DeptId = long.Parse(deptId),
                    CreateBy = _currentUser.UserName,
                    CreateTime = DateTime.Now
                }).ToList();

                await UserDeptRepository.CreateRangeAsync(userDepts);
            }


            _logger.Info(L("Common.AddSuccess"));
            return userId;
        }

        /// <summary>
        /// 更新用户
        /// </summary>
        /// <param name="input">用户更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateAsync(TaktUserUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId)
                ?? throw new TaktException(L("Identity.User.NotFound"));

            // 禁止修改管理用户（系统用户和管理员）
            if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                throw new TaktException("管理用户不允许修改！");

            // 验证字段是否已存在（排除当前用户）
            if (user.NickName != input.NickName)
                await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "NickName", input.NickName, input.UserId);
            if (user.PhoneNumber != input.PhoneNumber)
                await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", input.PhoneNumber, input.UserId);
            if (user.Email != input.Email)
                await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", input.Email, input.UserId);

            // 更新用户基本信息
            input.Adapt(user);

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            // 更新用户角色关联
            if (input.RoleIds != null)
            {
                // 获取用户现有关联的角色（包括已删除的）
                var existingRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == user.Id);

                // 1. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
                var rolesToDelete = existingRoles.Where(ur => !input.RoleIds.Contains(ur.RoleId.ToString())).ToList();
                if (rolesToDelete.Any())
                {
                    foreach (var role in rolesToDelete)
                    {
                        role.IsDeleted = 1; // 1 表示已删除
                        role.DeleteBy = _currentUser.UserName;
                        role.DeleteTime = DateTime.Now;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                }

                // 2. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
                var rolesToRestore = existingRoles.Where(ur => input.RoleIds.Contains(ur.RoleId.ToString()) && ur.IsDeleted == 1).ToList();
                if (rolesToRestore.Any())
                {
                    foreach (var role in rolesToRestore)
                    {
                        role.IsDeleted = 0; // 0 表示未删除
                        role.DeleteBy = null;
                        role.DeleteTime = null;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                }

                // 3. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
                var existingRoleIds = existingRoles.Select(ur => ur.RoleId).ToList();
                var rolesToAdd = input.RoleIds.Where(roleId => !existingRoleIds.Contains(long.Parse(roleId)))
                    .Select(roleId => new TaktUserRole
                    {
                        UserId = user.Id,
                        RoleId = long.Parse(roleId),
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (rolesToAdd.Any())
                {
                    await UserRoleRepository.CreateRangeAsync(rolesToAdd);
                }
            }

            // 更新用户岗位关联
            if (input.PostIds != null)
            {
                // 获取用户现有关联的岗位（包括已删除的）
                var existingPosts = await UserPostRepository.GetListAsync(up => up.UserId == user.Id);

                // 1. 找出需要标记删除的关联（在现有关联中但不在新的岗位列表中）
                var postsToDelete = existingPosts.Where(up => !input.PostIds.Contains(up.PostId.ToString())).ToList();
                if (postsToDelete.Any())
                {
                    foreach (var post in postsToDelete)
                    {
                        post.IsDeleted = 1; // 1 表示已删除
                        post.DeleteBy = _currentUser.UserName;
                        post.DeleteTime = DateTime.Now;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                }

                // 2. 处理需要恢复的关联（在新的岗位列表中且已存在但被标记为删除）
                var postsToRestore = existingPosts.Where(up => input.PostIds.Contains(up.PostId.ToString()) && up.IsDeleted == 1).ToList();
                if (postsToRestore.Any())
                {
                    foreach (var post in postsToRestore)
                    {
                        post.IsDeleted = 0; // 0 表示未删除
                        post.DeleteBy = null;
                        post.DeleteTime = null;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                }

                // 3. 找出需要新增的关联（在新的岗位列表中且不存在任何记录）
                var existingPostIds = existingPosts.Select(up => up.PostId).ToList();
                var postsToAdd = input.PostIds.Where(postId => !existingPostIds.Contains(long.Parse(postId)))
                    .Select(postId => new TaktUserPost
                    {
                        UserId = user.Id,
                        PostId = long.Parse(postId),
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (postsToAdd.Any())
                {
                    await UserPostRepository.CreateRangeAsync(postsToAdd);
                }
            }

            // 更新用户部门关联
            if (input.DeptIds != null)
            {
                // 获取用户现有关联的部门（包括已删除的）
                var existingDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == user.Id);

                // 1. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
                var deptsToDelete = existingDepts.Where(ud => !input.DeptIds.Contains(ud.DeptId.ToString())).ToList();
                if (deptsToDelete.Any())
                {
                    foreach (var dept in deptsToDelete)
                    {
                        dept.IsDeleted = 1; // 1 表示已删除
                        dept.DeleteBy = _currentUser.UserName;
                        dept.DeleteTime = DateTime.Now;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                }

                // 2. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
                var deptsToRestore = existingDepts.Where(ud => input.DeptIds.Contains(ud.DeptId.ToString()) && ud.IsDeleted == 1).ToList();
                if (deptsToRestore.Any())
                {
                    foreach (var dept in deptsToRestore)
                    {
                        dept.IsDeleted = 0; // 0 表示未删除
                        dept.DeleteBy = null;
                        dept.DeleteTime = null;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                }

                // 3. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
                var existingDeptIds = existingDepts.Select(ud => ud.DeptId).ToList();
                var deptsToAdd = input.DeptIds.Where(deptId => !existingDeptIds.Contains(long.Parse(deptId)))
                    .Select(deptId => new TaktUserDept
                    {
                        UserId = user.Id,
                        DeptId = long.Parse(deptId),
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (deptsToAdd.Any())
                {
                    await UserDeptRepository.CreateRangeAsync(deptsToAdd);
                }
            }

            _logger.Info(L("Common.UpdateSuccess"));
            return true;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> DeleteAsync(long userId)
        {
            var user = await UserRepository.GetByIdAsync(userId)
                ?? throw new TaktException(L("Identity.User.NotFound"));

            // 禁止删除管理用户（系统用户和管理员）
            if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                throw new TaktException("管理用户不允许删除！");

            // 更新用户状态为停用
            user.UserStatus = 1;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;
            await UserRepository.UpdateAsync(user);

            // 删除用户角色关联
            var userRoleIds = (await UserRoleRepository.GetListAsync(ur => ur.UserId == userId)).Select(ur => ur.Id).ToList();
            foreach (var id in userRoleIds)
            {
                await UserRoleRepository.DeleteAsync(id);
            }

            // 删除用户岗位关联
            var userPostIds = (await UserPostRepository.GetListAsync(up => up.UserId == userId)).Select(up => up.Id).ToList();
            foreach (var id in userPostIds)
            {
                await UserPostRepository.DeleteAsync(id);
            }

            // 删除用户部门关联
            var userDeptIds = (await UserDeptRepository.GetListAsync(ud => ud.UserId == userId)).Select(ud => ud.Id).ToList();
            foreach (var id in userDeptIds)
            {
                await UserDeptRepository.DeleteAsync(id);
            }


            // 删除用户
            var result = await UserRepository.DeleteAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.DeleteFailed"));

            return true;
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="userIds">用户ID列表</param>
        /// <returns>返回是否删除成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] userIds)
        {
            if (userIds == null || userIds.Length == 0)
                throw new TaktException(L("Identity.User.SelectRequired"));

            foreach (var userId in userIds)
            {
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null) continue;
                // 禁止删除管理用户（系统用户和管理员）
                if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                    throw new TaktException($"管理用户不允许删除！(ID: {userId})");
            }

            // 更新用户状态为停用
            var users = await UserRepository.GetListAsync(u => userIds.Contains(u.Id));
            foreach (var user in users)
            {
                user.UserStatus = 1;
                user.UpdateBy = _currentUser.UserName;
                user.UpdateTime = DateTime.Now;
            }
            await UserRepository.UpdateRangeAsync(users);

            // 删除用户角色关联
            await UserRoleRepository.DeleteAsync((TaktUserRole ur) => userIds.Contains(ur.UserId));

            // 删除用户岗位关联
            await UserPostRepository.DeleteAsync((TaktUserPost up) => userIds.Contains(up.UserId));

            // 删除用户部门关联
            await UserDeptRepository.DeleteAsync((TaktUserDept ud) => userIds.Contains(ud.UserId));


            // 删除用户
            return await UserRepository.DeleteRangeAsync(userIds.Cast<object>().ToList()) > 0;
        }



        /// <summary>
        /// 更新用户状态
        /// </summary>
        /// <param name="input">用户状态更新信息</param>
        /// <returns>返回是否更新成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktUserStatusDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new TaktException(L("Identity.User.NotFound"));

            // 禁止修改管理用户状态（系统用户和管理员）
            if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                throw new TaktException("管理用户状态不允许修改！");

            user.UserStatus = input.UserStatus;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input">重置密码信息</param>
        /// <returns>返回是否重置成功</returns>
        public async Task<bool> ResetPasswordAsync(TaktUserResetPwdDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new TaktException(L("Identity.User.NotFound"));

            // 禁止重置管理用户密码（系统用户和管理员）
            if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                throw new TaktException("管理用户密码不允许重置！");

            // 使用默认密码或验证用户提供的密码
            var password = string.IsNullOrEmpty(input.Password) ? _passwordPolicy.DefaultPassword : input.Password;

            // 验证密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(password))
                throw new TaktException(L("Identity.User.Password.Invalid"));

            var (hash, salt, iterations) = TaktPasswordUtils.CreateHash(password);
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="input">修改密码信息</param>
        /// <returns>返回是否修改成功</returns>
        public async Task<bool> ChangePasswordAsync(TaktUserChangePwdDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new TaktException(L("Identity.User.NotFound"));

            // 验证旧密码
            if (!TaktPasswordUtils.VerifyHash(input.OldPassword, user.Password, user.Salt, user.Iterations))
                throw new TaktException(L("Identity.User.Password.Incorrect"));

            // 验证新密码复杂度
            if (!_passwordPolicy.ValidatePasswordComplexity(input.NewPassword))
                throw new TaktException(L("Identity.User.Password.Invalid"));

            var (hash, salt, iterations) = TaktPasswordUtils.CreateHash(input.NewPassword);
            user.Password = hash;
            user.Salt = salt;
            user.Iterations = iterations;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }

        /// <summary>
        /// 解锁用户
        /// </summary>
        /// <param name="input">解锁用户信息</param>
        /// <returns>返回是否解锁成功</returns>
        public async Task<bool> UnlockUserAsync(TaktUserUnlockDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var user = await UserRepository.GetByIdAsync(input.UserId);
            if (user == null)
                throw new TaktException(L("Identity.User.NotFound"));

            // 禁止解锁管理用户（系统用户和管理员）
            if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                throw new TaktException("管理用户不允许解锁！");

            user.LockEndTime = null;
            user.LockReason = null;
            user.UpdateBy = _currentUser.UserName;
            user.UpdateTime = DateTime.Now;

            var result = await UserRepository.UpdateAsync(user);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return true;
        }




        /// <summary>
        /// 获取用户已分配的角色列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>角色列表</returns>
        public async Task<List<TaktUserRoleDto>> GetUserRoleIdsAsync(long userId)
        {
            var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
            return userRoles.Adapt<List<TaktUserRoleDto>>();
        }

        /// <summary>
        /// 获取用户已分配的部门列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>部门列表</returns>
        public async Task<List<TaktUserDeptDto>> GetUserDeptIdsAsync(long userId)
        {
            var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
            return userDepts.Adapt<List<TaktUserDeptDto>>();
        }

        /// <summary>
        /// 获取用户已分配的岗位列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>岗位列表</returns>
        public async Task<List<TaktUserPostDto>> GetUserPostIdsAsync(long userId)
        {
            var userPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId && up.IsDeleted == 0);
            return userPosts.Adapt<List<TaktUserPostDto>>();
        }


        /// <summary>
        /// 分配用户角色
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="roleIds">角色ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignUserRolesAsync(long userId, long[] roleIds)
        {
            try
            {
                _logger.Info($"开始分配用户角色 - 用户ID: {userId}, 角色IDs: {string.Join(",", roleIds)}");

                // 获取用户信息并检查是否为管理用户
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new TaktException(L("Identity.User.NotFound"));

                // 禁止对管理用户进行角色分配
                if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                    throw new TaktException("管理用户不允许进行角色分配！");

                // 1. 获取用户现有关联的角色（包括已删除的）
                var existingRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId);
                _logger.Info($"用户现有关联角色数量: {existingRoles.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的角色列表中）
                var rolesToDelete = existingRoles.Where(ur => !roleIds.Contains(ur.RoleId)).ToList();
                if (rolesToDelete.Any())
                {
                    _logger.Info($"需要标记删除的角色关联数量: {rolesToDelete.Count}, 角色IDs: {string.Join(",", rolesToDelete.Select(d => d.RoleId))}");
                    foreach (var role in rolesToDelete)
                    {
                        role.IsDeleted = 1; // 1 表示已删除
                        role.DeleteBy = _currentUser.UserName;
                        role.DeleteTime = DateTime.Now;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的角色列表中且已存在但被标记为删除）
                var rolesToRestore = existingRoles.Where(ur => roleIds.Contains(ur.RoleId) && ur.IsDeleted == 1).ToList();
                if (rolesToRestore.Any())
                {
                    _logger.Info($"需要恢复的角色关联数量: {rolesToRestore.Count}, 角色IDs: {string.Join(",", rolesToRestore.Select(d => d.RoleId))}");
                    foreach (var role in rolesToRestore)
                    {
                        role.IsDeleted = 0; // 0 表示未删除
                        role.DeleteBy = null;
                        role.DeleteTime = null;
                        role.UpdateBy = _currentUser.UserName;
                        role.UpdateTime = DateTime.Now;
                        await UserRoleRepository.UpdateAsync(role);
                    }
                    _logger.Info("角色关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的角色列表中且不存在任何记录）
                var existingRoleIds = existingRoles.Select(ur => ur.RoleId).ToList();
                var rolesToAdd = roleIds.Where(roleId => !existingRoleIds.Contains(roleId))
                    .Select(roleId => new TaktUserRole
                    {
                        UserId = userId,
                        RoleId = roleId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (rolesToAdd.Any())
                {
                    _logger.Info($"需要新增的角色关联数量: {rolesToAdd.Count}, 角色IDs: {string.Join(",", rolesToAdd.Select(d => d.RoleId))}");
                    await UserRoleRepository.CreateRangeAsync(rolesToAdd);
                    _logger.Info("角色关联新增完成");
                }

                _logger.Info("用户角色分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户角色失败: {ex.Message}", ex);
                throw;
            }
        }

        /// <summary>
        /// 分配用户部门
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="deptIds">部门ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignUserDeptsAsync(long userId, long[] deptIds)
        {
            try
            {
                _logger.Info($"开始分配用户部门 - 用户ID: {userId}, 部门IDs: {string.Join(",", deptIds)}");

                // 获取用户信息并检查是否为管理用户
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new TaktException(L("Identity.User.NotFound"));

                // 禁止对管理用户进行部门分配
                if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                    throw new TaktException("管理用户不允许进行部门分配！");

                // 1. 获取用户现有关联的部门（包括已删除的）
                var existingDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId);
                _logger.Info($"用户现有关联部门数量: {existingDepts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的部门列表中）
                var deptsToDelete = existingDepts.Where(ud => !deptIds.Contains(ud.DeptId)).ToList();
                if (deptsToDelete.Any())
                {
                    _logger.Info($"需要标记删除的部门关联数量: {deptsToDelete.Count}, 部门IDs: {string.Join(",", deptsToDelete.Select(d => d.DeptId))}");
                    foreach (var dept in deptsToDelete)
                    {
                        dept.IsDeleted = 1; // 1 表示已删除
                        dept.DeleteBy = _currentUser.UserName;
                        dept.DeleteTime = DateTime.Now;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                    _logger.Info("部门关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的部门列表中且已存在但被标记为删除）
                var deptsToRestore = existingDepts.Where(ud => deptIds.Contains(ud.DeptId) && ud.IsDeleted == 1).ToList();
                if (deptsToRestore.Any())
                {
                    _logger.Info($"需要恢复的部门关联数量: {deptsToRestore.Count}, 部门IDs: {string.Join(",", deptsToRestore.Select(d => d.DeptId))}");
                    foreach (var dept in deptsToRestore)
                    {
                        dept.IsDeleted = 0; // 0 表示未删除
                        dept.DeleteBy = null;
                        dept.DeleteTime = null;
                        dept.UpdateBy = _currentUser.UserName;
                        dept.UpdateTime = DateTime.Now;
                        await UserDeptRepository.UpdateAsync(dept);
                    }
                    _logger.Info("部门关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的部门列表中且不存在任何记录）
                var existingDeptIds = existingDepts.Select(ud => ud.DeptId).ToList();
                var deptsToAdd = deptIds.Where(deptId => !existingDeptIds.Contains(deptId))
                    .Select(deptId => new TaktUserDept
                    {
                        UserId = userId,
                        DeptId = deptId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (deptsToAdd.Any())
                {
                    _logger.Info($"需要新增的部门关联数量: {deptsToAdd.Count}, 部门IDs: {string.Join(",", deptsToAdd.Select(d => d.DeptId))}");
                    await UserDeptRepository.CreateRangeAsync(deptsToAdd);
                    _logger.Info("部门关联新增完成");
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
        /// 分配用户岗位
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="postIds">岗位ID列表</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignUserPostsAsync(long userId, long[] postIds)
        {
            try
            {
                _logger.Info($"开始分配用户岗位 - 用户ID: {userId}, 岗位IDs: {string.Join(",", postIds)}");

                // 获取用户信息并检查是否为管理用户
                var user = await UserRepository.GetByIdAsync(userId);
                if (user == null)
                    throw new TaktException(L("Identity.User.NotFound"));

                // 禁止对管理用户进行岗位分配
                if (user.UserName == "admin" || user.UserType == 0 || user.UserType == 2)
                    throw new TaktException("管理用户不允许进行岗位分配！");

                // 1. 获取用户现有关联的岗位（包括已删除的）
                var existingPosts = await UserPostRepository.GetListAsync(up => up.UserId == userId);
                _logger.Info($"用户现有关联岗位数量: {existingPosts.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的岗位列表中）
                var postsToDelete = existingPosts.Where(up => !postIds.Contains(up.PostId)).ToList();
                if (postsToDelete.Any())
                {
                    _logger.Info($"需要标记删除的岗位关联数量: {postsToDelete.Count}, 岗位IDs: {string.Join(",", postsToDelete.Select(d => d.PostId))}");
                    foreach (var post in postsToDelete)
                    {
                        post.IsDeleted = 1; // 1 表示已删除
                        post.DeleteBy = _currentUser.UserName;
                        post.DeleteTime = DateTime.Now;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                    _logger.Info("岗位关联标记删除完成");
                }

                // 3. 处理需要恢复的关联（在新的岗位列表中且已存在但被标记为删除）
                var postsToRestore = existingPosts.Where(up => postIds.Contains(up.PostId) && up.IsDeleted == 1).ToList();
                if (postsToRestore.Any())
                {
                    _logger.Info($"需要恢复的岗位关联数量: {postsToRestore.Count}, 岗位IDs: {string.Join(",", postsToRestore.Select(d => d.PostId))}");
                    foreach (var post in postsToRestore)
                    {
                        post.IsDeleted = 0; // 0 表示未删除
                        post.DeleteBy = null;
                        post.DeleteTime = null;
                        post.UpdateBy = _currentUser.UserName;
                        post.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(post);
                    }
                    _logger.Info("岗位关联恢复完成");
                }

                // 4. 找出需要新增的关联（在新的岗位列表中且不存在任何记录）
                var existingPostIds = existingPosts.Select(up => up.PostId).ToList();
                var postsToAdd = postIds.Where(postId => !existingPostIds.Contains(postId))
                    .Select(postId => new TaktUserPost
                    {
                        UserId = userId,
                        PostId = postId,
                        IsDeleted = 0, // 0 表示未删除
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();

                if (postsToAdd.Any())
                {
                    _logger.Info($"需要新增的岗位关联数量: {postsToAdd.Count}, 岗位IDs: {string.Join(",", postsToAdd.Select(d => d.PostId))}");
                    await UserPostRepository.CreateRangeAsync(postsToAdd);
                    _logger.Info("岗位关联新增完成");
                }

                _logger.Info("用户岗位分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户岗位失败: {ex.Message}", ex);
                throw;
            }
        }



        /// <summary>
        /// 更新用户个人信息
        /// </summary>
        /// <param name="input">个人信息更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateProfileAsync(TaktUserProfileUpdateDto input)
        {
            try
            {
                _logger.Info($"开始更新用户个人信息 - 用户ID: {input.UserId}");

                // 获取用户信息
                var user = await UserRepository.GetByIdAsync(input.UserId);
                if (user == null)
                {
                    _logger.Warn($"用户不存在 - 用户ID: {input.UserId}");
                    return false;
                }

                // 更新用户个人信息
                user.NickName = input.NickName;
                user.EnglishName = input.EnglishName;
                user.FullName = input.FullName;
                user.RealName = input.RealName;
                user.PhoneNumber = input.PhoneNumber;
                user.Email = input.Email;
                user.Gender = input.Gender;


                // 如果提供了密码，则更新密码
                if (!string.IsNullOrEmpty(input.NewPassword))
                {
                    if (string.IsNullOrEmpty(input.OldPassword))
                    {
                        _logger.Warn($"更新密码时旧密码不能为空 - 用户ID: {input.UserId}");
                        return false;
                    }

                    // 验证旧密码
                    if (!TaktPasswordUtils.VerifyHash(input.OldPassword, user.Password, user.Salt, user.Iterations))
                    {
                        _logger.Warn($"旧密码验证失败 - 用户ID: {input.UserId}");
                        return false;
                    }

                    // 生成新密码
                    var (hash, salt, iterations) = TaktPasswordUtils.CreateHash(input.NewPassword);
                    user.Password = hash;
                    user.Salt = salt;
                    user.Iterations = iterations;
                    user.LastPasswordChangeTime = DateTime.Now;
                }

                var updateResult = await UserRepository.UpdateAsync(user);

                if (updateResult > 0)
                {
                    _logger.Info($"成功更新用户个人信息 - 用户ID: {input.UserId}");
                }

                return updateResult > 0;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新用户个人信息失败 - 用户ID: {input.UserId}, 错误: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// 更新用户头像
        /// </summary>
        /// <param name="input">头像更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAvatarAsync(TaktUserAvatarUpdateDto input)
        {
            try
            {
                _logger.Info($"开始更新用户头像 - 用户ID: {input.UserId}");

                // 获取用户信息
                var user = await UserRepository.GetByIdAsync(input.UserId);
                if (user == null)
                {
                    _logger.Warn($"用户不存在 - 用户ID: {input.UserId}");
                    return false;
                }

                // 更新用户头像
                user.Avatar = input.Avatar;


                var updateResult = await UserRepository.UpdateAsync(user);

                if (updateResult > 0)
                {
                    _logger.Info($"成功更新用户头像 - 用户ID: {input.UserId}");
                }

                return updateResult > 0;
            }
            catch (Exception ex)
            {
                _logger.Error($"更新用户头像失败 - 用户ID: {input.UserId}, 错误: {ex.Message}");
                return false;
            }
        }



        /// <summary>
        /// 获取用户导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
        {
            var actualSheetName = sheetName ?? "User";
            var actualFileName = fileName ?? "UserTemplate";
            return await TaktExcelHelper.GenerateTemplateAsync<TaktUserTemplateDto>(actualSheetName, actualFileName);
        }

        /// <summary>
        /// 导入用户数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
        {
            try
            {
                var actualSheetName = sheetName ?? "User";
                var users = await TaktExcelHelper.ImportAsync<TaktUserImportDto>(fileStream, actualSheetName);
                if (!users.Any())
                    return (0, 0);

                int success = 0, fail = 0;

                foreach (var user in users)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(user.UserName))
                        {
                            _logger.Warn("导入用户失败: 用户名不能为空");
                            fail++;
                            continue;
                        }

                        // 校验用户名是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "UserName", user.UserName);
                        // 校验手机号是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "PhoneNumber", user.PhoneNumber);
                        // 校验邮箱是否已存在
                        await TaktValidateUtils.ValidateFieldExistsAsync(UserRepository, "Email", user.Email);
                        var entity = user.Adapt<TaktUser>();
                        var (hash, salt, iterations) = TaktPasswordUtils.CreateHash(_passwordPolicy.DefaultPassword);
                        entity.Password = hash;
                        entity.Salt = salt;
                        entity.Iterations = iterations;
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = _currentUser.UserName;
                        entity.UserStatus = 0;

                        var result = await UserRepository.CreateAsync(entity);
                        if (result > 0)
                            success++;
                        else
                            fail++;
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"导入用户失败: {ex.Message}");
                        fail++;
                    }
                }

                return (success, fail);
            }
            catch (Exception ex)
            {
                _logger.Error("导入用户数据失败", ex);
                throw new TaktException($"导入用户数据失败: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// 导出用户数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件或zip文件</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktUserQueryDto query, string? sheetName, string? fileName)
        {
            var list = await UserRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktUserExportDto>>();

            // 使用传入的参数，如果为空则使用默认值
            var actualSheetName = sheetName ?? "User";
            var actualFileName = fileName ?? "UserData";

            return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
        }
        /// <summary>
        /// 构建用户查询条件
        /// </summary>
        private static Expression<Func<TaktUser, bool>> QueryExpression(TaktUserQueryDto query)
        {
            return Expressionable.Create<TaktUser>()
                .AndIF(!string.IsNullOrEmpty(query?.UserName), x => x.UserName.Contains(query!.UserName!))
                .AndIF(!string.IsNullOrEmpty(query?.NickName), x => x.NickName.Contains(query!.NickName!))
                .AndIF(!string.IsNullOrEmpty(query?.PhoneNumber), x => x.PhoneNumber.Contains(query!.PhoneNumber!))
                .AndIF(!string.IsNullOrEmpty(query?.Email), x => x.Email.Contains(query!.Email!))
                .AndIF(query?.UserStatus.HasValue == true && query.UserStatus!.Value != -1, x => x.UserStatus == query!.UserStatus!.Value)
                .AndIF(query?.UserType.HasValue == true && query.UserType!.Value != -1, x => x.UserType == query!.UserType!.Value)
                .AndIF(query?.Gender.HasValue == true && query.Gender!.Value != -1, x => x.Gender == query!.Gender!.Value)
                .ToExpression();
        }
    }
}




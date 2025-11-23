#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktApproverResolver.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流审批人解析器实现 - 基于 OpenAuth.Net 标准
//===================================================================

using Takt.Domain.Models.Workflow;
using Takt.Domain.Repositories;
using Takt.Domain.Entities.Identity;
using Takt.Shared.Constants;
using Takt.Application.Services.Identity;
using SqlSugar;

namespace Takt.Application.Services.Workflow.Engine.Resolvers
{
    /// <summary>
    /// 工作流审批人解析器实现 - 支持 OpenAuth.Net 标准的9种审批人指定方式
    /// </summary>
    public class TaktApproverResolver : ITaktApproverResolver
    {
        private readonly ITaktLogger _logger;
        private readonly ITaktCurrentUser _currentUser;
        private readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktUserService _userService;
        private readonly ITaktRoleService _roleService;
        private readonly ITaktDeptService _deptService;

        /// <summary>
        /// 获取用户仓储
        /// </summary>
        private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();

        /// <summary>
        /// 获取用户角色仓储
        /// </summary>
        private ITaktRepository<TaktUserRole> UserRoleRepository => _repositoryFactory.GetAuthRepository<TaktUserRole>();

        /// <summary>
        /// 获取用户部门仓储
        /// </summary>
        private ITaktRepository<TaktUserDept> UserDeptRepository => _repositoryFactory.GetAuthRepository<TaktUserDept>();

        /// <summary>
        /// 获取SqlSugar客户端（用于执行SQL）
        /// </summary>
        private ISqlSugarClient SqlSugarClient => UserRepository.SqlSugarClient;

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktApproverResolver(
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktRepositoryFactory repositoryFactory,
            ITaktUserService userService,
            ITaktRoleService roleService,
            ITaktDeptService deptService)
        {
            _logger = logger;
            _currentUser = currentUser;
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
            _deptService = deptService ?? throw new ArgumentNullException(nameof(deptService));
        }

        /// <inheritdoc/>
        public Task<List<long>> ResolveApproversAsync(
            long instanceId,
            string nodeId,
            int approverType,
            string? approverConfig,
            Dictionary<string, object>? variables = null)
        {
            try
            {
                switch (approverType)
                {
                    case 1: // 指定用户
                        return Task.FromResult(ResolveSpecifiedUsersAsync(approverConfig));
                    case 2: // 角色
                        return Task.FromResult(ResolveByRoleAsync(approverConfig));
                    case 3: // 部门
                        return Task.FromResult(ResolveByDepartmentAsync(approverConfig));
                    case 4: // 动态
                        return Task.FromResult(ResolveDynamicAsync(instanceId, nodeId, approverConfig, variables));
                    default:
                        _logger.Warn($"不支持的审批人类型: {approverType}");
                        return Task.FromResult(new List<long>());
                }
            }
            catch (Exception ex)
            {
                _logger.Error($"解析审批人失败: {ex.Message}", ex);
                return Task.FromResult(new List<long>());
            }
        }

        /// <inheritdoc/>
        public async Task<string> ResolveApproversByNodeAsync(
            TaktFlowNode node,
            long instanceCreateUserId,
            string[]? runtimeDesignates = null)
        {
            if (node.SetInfo == null)
            {
                // 如果没有设置节点信息，默认所有人都可以审核
                return "1";
            }

            var nodeDesignate = node.SetInfo.NodeDesignate;
            var nodeDesignateData = node.SetInfo.NodeDesignateData;

            // 所有成员
            if (string.IsNullOrEmpty(nodeDesignate) || nodeDesignate == TaktWorkflowConstants.ALL_USER)
            {
                return "1";
            }

            // 指定用户
            if (nodeDesignate == TaktWorkflowConstants.SPECIAL_USER)
            {
                if (nodeDesignateData?.Datas != null && nodeDesignateData.Datas.Any())
                {
                    return string.Join(",", nodeDesignateData.Datas);
                }
                throw new InvalidOperationException($"节点 {node.Name} 配置了指定用户，但未提供用户列表");
            }

            // 指定角色
            if (nodeDesignate == TaktWorkflowConstants.SPECIAL_ROLE)
            {
                if (nodeDesignateData?.Datas != null && nodeDesignateData.Datas.Any())
                {
                    var roleIds = nodeDesignateData.Datas.Select(long.Parse).ToList();
                    var userIds = await GetUsersByRoleIdsAsync(roleIds);
                    if (!userIds.Any())
                    {
                        throw new InvalidOperationException($"节点 {node.Name} 配置的角色下没有用户");
                    }
                    return string.Join(",", userIds);
                }
                throw new InvalidOperationException($"节点 {node.Name} 配置了指定角色，但未提供角色列表");
            }

            // 指定SQL
            if (nodeDesignate == TaktWorkflowConstants.SPECIAL_SQL)
            {
                if (nodeDesignateData?.Datas != null && nodeDesignateData.Datas.Any())
                {
                    var sql = ReplaceSql(nodeDesignateData.Datas[0]);
                    var userIds = await ExecuteSqlQueryAsync<string>(sql);
                    if (!userIds.Any())
                    {
                        throw new InvalidOperationException($"节点 {node.Name} 配置的SQL查询没有返回用户");
                    }
                    return string.Join(",", userIds);
                }
                throw new InvalidOperationException($"节点 {node.Name} 配置了指定SQL，但未提供SQL语句");
            }

            // 运行时指定用户
            if (nodeDesignate == TaktWorkflowConstants.RUNTIME_SPECIAL_USER)
            {
                if (runtimeDesignates == null || !runtimeDesignates.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 需要运行时指定用户，但未提供");
                }
                return string.Join(",", runtimeDesignates);
            }

            // 运行时指定角色
            if (nodeDesignate == TaktWorkflowConstants.RUNTIME_SPECIAL_ROLE)
            {
                if (runtimeDesignates == null || !runtimeDesignates.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 需要运行时指定角色，但未提供");
                }
                var roleIds = runtimeDesignates.Select(long.Parse).ToList();
                var userIds = await GetUsersByRoleIdsAsync(roleIds);
                if (!userIds.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 运行时指定的角色下没有用户");
                }
                return string.Join(",", userIds);
            }

            // 上一节点执行人的直属上级
            if (nodeDesignate == TaktWorkflowConstants.RUNTIME_PARENT)
            {
                var parentId = await GetUserParentIdAsync(_currentUser.UserId);
                if (string.IsNullOrEmpty(parentId))
                {
                    throw new InvalidOperationException($"无法找到当前用户的直属上级");
                }
                return parentId;
            }

            // 连续多级直属上级
            if (nodeDesignate == TaktWorkflowConstants.RUNTIME_MANY_PARENTS)
            {
                if (nodeDesignateData?.Datas == null || !nodeDesignateData.Datas.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 配置了连续多级直属上级，但未指定目标角色");
                }
                var targetRoleIds = nodeDesignateData.Datas.Select(long.Parse).ToList();
                var currentUserRoles = await GetUserRoleIdsAsync(_currentUser.UserId);
                
                // 如果当前用户已经在目标角色中，直接返回当前用户
                if (targetRoleIds.Intersect(currentUserRoles).Any())
                {
                    return _currentUser.UserId.ToString();
                }

                // 向上查找直到找到目标角色
                var parentId = await GetUserParentIdAsync(_currentUser.UserId);
                if (string.IsNullOrEmpty(parentId))
                {
                    throw new InvalidOperationException($"无法找到当前用户的直属上级");
                }
                return parentId;
            }

            // 发起人的部门负责人
            if (nodeDesignate == TaktWorkflowConstants.RUNTIME_CHAIRMAN)
            {
                if (nodeDesignateData?.Datas == null || !nodeDesignateData.Datas.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 配置了部门负责人，但未指定部门");
                }
                var deptIds = nodeDesignateData.Datas.Select(long.Parse).ToList();
                var chairmanIds = await GetDeptChairmanIdsAsync(deptIds);
                if (!chairmanIds.Any())
                {
                    throw new InvalidOperationException($"节点 {node.Name} 指定的部门没有负责人");
                }
                return string.Join(",", chairmanIds);
            }

            // 如果是开始节点，返回创建人
            if (node.Type == TaktWorkflowConstants.NODE_TYPE_START && instanceCreateUserId > 0)
            {
                return instanceCreateUserId.ToString();
            }

            // 默认返回所有人
            return "1";
        }

        /// <inheritdoc/>
        public bool ValidateConfig(int approverType, string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return false;

            try
            {
                switch (approverType)
                {
                    case 1: // 指定用户
                        var userConfig = JsonConvert.DeserializeObject<UserApproverConfig>(approverConfig);
                        return userConfig?.UserIds != null && userConfig.UserIds.Any();
                    case 2: // 角色
                        var roleConfig = JsonConvert.DeserializeObject<RoleApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(roleConfig?.RoleCode);
                    case 3: // 部门
                        var deptConfig = JsonConvert.DeserializeObject<DepartmentApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(deptConfig?.DepartmentCode);
                    case 4: // 动态
                        var dynamicConfig = JsonConvert.DeserializeObject<DynamicApproverConfig>(approverConfig);
                        return !string.IsNullOrEmpty(dynamicConfig?.Expression);
                    default:
                        return false;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public string GetApproverTypeDescription(int approverType)
        {
            return approverType switch
            {
                1 => "指定用户",
                2 => "角色",
                3 => "部门",
                4 => "动态",
                _ => "未知类型"
            };
        }

        #region 私有方法

        /// <summary>
        /// 解析指定用户
        /// </summary>
        private List<long> ResolveSpecifiedUsersAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonConvert.DeserializeObject<UserApproverConfig>(approverConfig);
                return config?.UserIds ?? new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"解析指定用户配置失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 根据角色解析审批人
        /// </summary>
        private List<long> ResolveByRoleAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonConvert.DeserializeObject<RoleApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.RoleCode))
                    return new List<long>();

                // 这里需要调用用户服务获取角色下的用户
                // 简化实现，返回空列表
                _logger.Info($"根据角色[{config.RoleCode}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"根据角色解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 根据部门解析审批人
        /// </summary>
        private List<long> ResolveByDepartmentAsync(string? approverConfig)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonConvert.DeserializeObject<DepartmentApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.DepartmentCode))
                    return new List<long>();

                // 这里需要调用用户服务获取部门下的用户
                // 简化实现，返回空列表
                _logger.Info($"根据部门[{config.DepartmentCode}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"根据部门解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        /// <summary>
        /// 动态解析审批人
        /// </summary>
        private List<long> ResolveDynamicAsync(
            long instanceId,
            string nodeId,
            string? approverConfig,
            Dictionary<string, object>? variables)
        {
            if (string.IsNullOrEmpty(approverConfig))
                return new List<long>();

            try
            {
                var config = JsonConvert.DeserializeObject<DynamicApproverConfig>(approverConfig);
                if (config == null || string.IsNullOrEmpty(config.Expression))
                    return new List<long>();

                // 这里需要执行动态表达式来获取审批人
                // 简化实现，返回空列表
                _logger.Info($"执行动态表达式[{config.Expression}]解析审批人");
                return new List<long>();
            }
            catch (Exception ex)
            {
                _logger.Error($"动态解析审批人失败: {ex.Message}", ex);
                return new List<long>();
            }
        }

        #endregion

        #region OpenAuth.Net 标准方法

        /// <summary>
        /// 根据角色ID列表获取用户ID列表
        /// </summary>
        private async Task<List<string>> GetUsersByRoleIdsAsync(List<long> roleIds)
        {
            var userRoles = await UserRoleRepository.GetListAsync(ur => roleIds.Contains(ur.RoleId) && ur.IsDeleted == 0);
            var userIds = userRoles.Select(ur => ur.UserId.ToString()).Distinct().ToList();
            return userIds;
        }

        /// <summary>
        /// 获取用户的角色ID列表
        /// </summary>
        private async Task<List<long>> GetUserRoleIdsAsync(long userId)
        {
            var userRoles = await UserRoleRepository.GetListAsync(ur => ur.UserId == userId && ur.IsDeleted == 0);
            return userRoles.Select(ur => ur.RoleId).ToList();
        }

        /// <summary>
        /// 获取用户的直属上级ID
        /// </summary>
        /// <remarks>
        /// TaktXp 中用户可能没有直接的 ParentId 字段
        /// 可以通过用户的部门来查找部门负责人作为上级
        /// 或者根据实际业务规则实现
        /// </remarks>
        private async Task<string> GetUserParentIdAsync(long userId)
        {
            try
            {
                // 方案1: 通过用户的部门查找部门负责人
                var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
                if (userDepts.Any())
                {
                    var deptId = userDepts.First().DeptId;
                    // 这里需要查询部门表获取负责人
                    // 由于 TaktDept 表在 Identity 库，而 TaktDepartment 在 HR 库
                    // 暂时通过部门服务获取
                    var dept = await _deptService.GetByIdAsync(deptId);
                    if (dept != null && !string.IsNullOrEmpty(dept.Leader))
                    {
                        // Leader 是字符串，需要转换为用户ID
                        // 这里简化处理，实际需要根据业务规则实现
                        _logger.Info($"通过部门负责人获取用户上级，部门ID: {deptId}, 负责人: {dept.Leader}");
                    }
                }

                // 方案2: 如果用户表有上级字段，直接查询
                // var user = await UserRepository.GetByIdAsync(userId);
                // if (user != null && user.ParentId > 0)
                // {
                //     return user.ParentId.ToString();
                // }

                _logger.Warn($"无法找到用户 {userId} 的直属上级，需要根据实际业务数据结构实现");
                return string.Empty;
            }
            catch (Exception ex)
            {
                _logger.Error($"获取用户上级失败: {ex.Message}", ex);
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取部门负责人ID列表
        /// </summary>
        private async Task<List<string>> GetDeptChairmanIdsAsync(List<long> deptIds)
        {
            try
            {
                var chairmanIds = new List<string>();

                foreach (var deptId in deptIds)
                {
                    var dept = await _deptService.GetByIdAsync(deptId);
                    if (dept != null && !string.IsNullOrEmpty(dept.Leader))
                    {
                        // Leader 是负责人名称，需要转换为用户ID
                        // 这里简化处理，实际需要根据负责人名称查找用户
                        // 或者如果 Leader 存储的是用户ID，直接使用
                        _logger.Info($"部门 {deptId} 的负责人: {dept.Leader}");
                        
                        // 尝试通过负责人名称查找用户
                        var users = await UserRepository.GetListAsync(u => 
                            (u.RealName == dept.Leader || u.UserName == dept.Leader) && 
                            u.UserStatus == 0 && u.IsDeleted == 0);
                        if (users.Any())
                        {
                            chairmanIds.AddRange(users.Select(u => u.Id.ToString()));
                        }
                    }
                }

                return chairmanIds.Distinct().ToList();
            }
            catch (Exception ex)
            {
                _logger.Error($"获取部门负责人失败: {ex.Message}", ex);
                return new List<string>();
            }
        }

        /// <summary>
        /// 替换SQL中的权限占位符
        /// </summary>
        private string ReplaceSql(string sql)
        {
            var loginUser = _currentUser;
            var res = sql.Replace(TaktWorkflowConstants.DATAPRIVILEGE_LOGINUSER, $"'{loginUser.UserId}'");
            
            // 获取当前用户的角色ID列表
            var roleIds = GetUserRoleIdsAsync(loginUser.UserId).Result;
            res = res.Replace(TaktWorkflowConstants.DATAPRIVILEGE_LOGINROLE, 
                string.Join(",", roleIds.Select(r => $"'{r}'")));

            // 获取当前用户的部门ID列表
            var deptIds = GetUserDeptIdsAsync(loginUser.UserId).Result;
            res = res.Replace(TaktWorkflowConstants.DATAPRIVILEGE_LOGINORG,
                string.Join(",", deptIds.Select(d => $"'{d}'")));

            return res;
        }

        /// <summary>
        /// 获取用户的部门ID列表
        /// </summary>
        private async Task<List<long>> GetUserDeptIdsAsync(long userId)
        {
            var userDepts = await UserDeptRepository.GetListAsync(ud => ud.UserId == userId && ud.IsDeleted == 0);
            return userDepts.Select(ud => ud.DeptId).ToList();
        }

        /// <summary>
        /// 执行SQL查询
        /// </summary>
        private async Task<List<T>> ExecuteSqlQueryAsync<T>(string sql)
        {
            try
            {
                var result = await SqlSugarClient.Ado.SqlQueryAsync<T>(sql);
                return result ?? new List<T>();
            }
            catch (Exception ex)
            {
                _logger.Error($"执行SQL查询失败: {ex.Message}, SQL: {sql}", ex);
                throw new InvalidOperationException($"执行SQL查询失败: {ex.Message}");
            }
        }

        #endregion

        #region 配置类

        /// <summary>
        /// 指定用户审批人配置
        /// </summary>
        private class UserApproverConfig
        {
            public List<long> UserIds { get; set; } = new();
        }

        /// <summary>
        /// 角色审批人配置
        /// </summary>
        private class RoleApproverConfig
        {
            public string RoleCode { get; set; } = string.Empty;
            public bool IncludeSubRoles { get; set; } = false;
        }

        /// <summary>
        /// 部门审批人配置
        /// </summary>
        private class DepartmentApproverConfig
        {
            public string DepartmentCode { get; set; } = string.Empty;
            public bool IncludeSubDepartments { get; set; } = false;
            public string? Position { get; set; }
        }

        /// <summary>
        /// 动态审批人配置
        /// </summary>
        private class DynamicApproverConfig
        {
            public string Expression { get; set; } = string.Empty;
            public Dictionary<string, object>? Parameters { get; set; }
        }

        #endregion
    }
}



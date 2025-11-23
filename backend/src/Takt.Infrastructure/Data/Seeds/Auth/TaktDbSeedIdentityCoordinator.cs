//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedIdentityCoordinator.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 认证种子数据协调器 - 使用仓储工厂模式
//===================================================================

using Takt.Shared.Options;
using Takt.Domain.Entities.Identity;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Data.Seeds.Auth;

/// <summary>
/// 认证种子数据协调器
/// </summary>
/// <remarks>
/// 创建者:Takt(Claude AI)
/// 创建时间: 2024-12-01
/// 功能说明:
/// 1. 统一管理认证相关的种子数据初始化
/// 2. 使用仓储工厂模式支持多库架构
/// 3. 提供批量初始化功能
/// 4. 支持种子数据的增量更新
/// </remarks>
public class TaktDbSeedIdentityCoordinator
{
    /// <summary>
    /// 仓储工厂
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;
    private readonly ITaktLogger _logger;
    private readonly IOptions<TaktPasswordPolicyOptions> _passwordPolicyOptions;

    private ITaktRepository<TaktRole> RoleRepository => _repositoryFactory.GetAuthRepository<TaktRole>();
    private ITaktRepository<TaktUser> UserRepository => _repositoryFactory.GetAuthRepository<TaktUser>();
    private ITaktRepository<TaktPost> PostRepository => _repositoryFactory.GetAuthRepository<TaktPost>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">仓储工厂</param>
    /// <param name="logger">日志服务</param>
    /// <param name="passwordPolicyOptions">密码策略配置选项</param>
    public TaktDbSeedIdentityCoordinator(ITaktRepositoryFactory repositoryFactory, ITaktLogger logger, IOptions<TaktPasswordPolicyOptions> passwordPolicyOptions)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _passwordPolicyOptions = passwordPolicyOptions ?? throw new ArgumentNullException(nameof(passwordPolicyOptions));
    }

    /// <summary>
    /// 初始化所有认证相关种子数据
    /// </summary>
    /// <returns>初始化结果</returns>
    public async Task<IdentitySeedResult> InitializeAllIdentityDataAsync()
    {
        try
        {
            _logger.Info("开始初始化所有认证相关种子数据...");

            var result = new IdentitySeedResult();


            // 2. 初始化角色数据
            var (roleInsertCount, roleUpdateCount) = await InitializeRoleDataAsync();
            result.RoleResult = (roleInsertCount, roleUpdateCount);

            // 3. 初始化用户数据
            var (userInsertCount, userUpdateCount) = await InitializeUserDataAsync();
            result.UserResult = (userInsertCount, userUpdateCount);

            // 4. 初始化岗位数据
            var (postInsertCount, postUpdateCount) = await InitializePostDataAsync();
            result.PostResult = (postInsertCount, postUpdateCount);

            _logger.Info($"认证种子数据初始化完成！角色: {roleInsertCount + roleUpdateCount} 条, 用户: {userInsertCount + userUpdateCount} 条, 岗位: {postInsertCount + postUpdateCount} 条");
            return result;
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化认证种子数据失败: {ex.Message}", ex);
            throw;
        }
    }


    /// <summary>
    /// 初始化角色数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeRoleDataAsync()
    {
        try
        {
            _logger.Info("开始初始化角色数据...");

            var roleSeed = new TaktDbSeedRole();
            var defaultRoles = roleSeed.GetDefaultRoles();
            int insertCount = 0;
            int updateCount = 0;

            foreach (var role in defaultRoles)
            {
                var existingRole = await RoleRepository.GetFirstAsync(r => r.RoleKey == role.RoleKey);
                if (existingRole == null)
                {
                    // 设置审计字段
                    role.CreateBy = "Takt365";
                    role.CreateTime = DateTime.Now;
                    role.UpdateBy = "Takt365";
                    role.UpdateTime = DateTime.Now;

                    await RoleRepository.CreateAsync(role);
                    insertCount++;
                    _logger.Info($"新增角色: {role.RoleName} ({role.RoleKey})");
                }
                else
                {
                    // 更新现有角色信息
                    existingRole.RoleName = role.RoleName;
                    existingRole.OrderNum = role.OrderNum;
                    existingRole.RoleStatus = role.RoleStatus;
                    existingRole.Remark = role.Remark;
                    existingRole.UpdateBy = "Takt365";
                    existingRole.UpdateTime = DateTime.Now;

                    await RoleRepository.UpdateAsync(existingRole);
                    updateCount++;
                    _logger.Info($"更新角色: {role.RoleName} ({role.RoleKey})");
                }
            }

            _logger.Info($"角色数据初始化完成: 新增 {insertCount} 条, 更新 {updateCount} 条");
            return (insertCount, updateCount);
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化角色数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化用户数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializeUserDataAsync()
    {
        try
        {
            _logger.Info("开始初始化用户数据...");

            var userSeed = new TaktDbSeedUser(_passwordPolicyOptions);
            var defaultUsers = userSeed.GetDefaultUsers();
            int insertCount = 0;
            int updateCount = 0;

            foreach (var user in defaultUsers)
            {
                var existingUser = await UserRepository.GetFirstAsync(u => u.UserName == user.UserName);
                if (existingUser == null)
                {
                    // 设置审计字段
                    user.CreateBy = "Takt365";
                    user.CreateTime = DateTime.Now;
                    user.UpdateBy = "Takt365";
                    user.UpdateTime = DateTime.Now;

                    await UserRepository.CreateAsync(user);
                    insertCount++;
                    _logger.Info($"新增用户: {user.UserName} ({user.NickName})");
                }
                else
                {
                    // 更新现有用户信息
                    existingUser.NickName = user.NickName;
                    existingUser.RealName = user.RealName;
                    existingUser.FullName = user.FullName;
                    existingUser.EnglishName = user.EnglishName;
                    existingUser.UserType = user.UserType;

                    // 检查密码相关字段是否为空，如果为空则更新
                    if (string.IsNullOrEmpty(existingUser.Password) || string.IsNullOrEmpty(existingUser.Salt) || existingUser.Iterations == 0)
                    {
                        existingUser.Password = user.Password;
                        existingUser.Salt = user.Salt;
                        existingUser.Iterations = user.Iterations;
                        _logger.Info($"用户 {user.UserName} 密码字段为空，已更新密码信息");
                    }

                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Gender = user.Gender;
                    existingUser.Avatar = user.Avatar;
                    existingUser.UserStatus = user.UserStatus;
                    existingUser.IsLock = user.IsLock;
                    existingUser.ErrorLimit = user.ErrorLimit;
                    existingUser.LoginCount = user.LoginCount;
                    existingUser.UpdateBy = "Takt365";
                    existingUser.UpdateTime = DateTime.Now;

                    await UserRepository.UpdateAsync(existingUser);
                    updateCount++;
                    _logger.Info($"更新用户: {user.UserName} ({user.NickName})");
                }
            }

            _logger.Info($"用户数据初始化完成: 新增 {insertCount} 条, 更新 {updateCount} 条");
            return (insertCount, updateCount);
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化用户数据失败: {ex.Message}", ex);
            throw;
        }
    }

    /// <summary>
    /// 初始化岗位数据
    /// </summary>
    /// <returns>初始化结果</returns>
    private async Task<(int insertCount, int updateCount)> InitializePostDataAsync()
    {
        try
        {
            _logger.Info("开始初始化岗位数据...");

            var postSeed = new TaktDbSeedPost();
            var defaultPosts = postSeed.GetDefaultPosts();
            int insertCount = 0;
            int updateCount = 0;

            foreach (var post in defaultPosts)
            {
                var existingPost = await PostRepository.GetFirstAsync(p => p.PostCode == post.PostCode);
                if (existingPost == null)
                {
                    // 设置审计字段
                    post.CreateBy = "Takt365";
                    post.CreateTime = DateTime.Now;
                    post.UpdateBy = "Takt365";
                    post.UpdateTime = DateTime.Now;

                    await PostRepository.CreateAsync(post);
                    insertCount++;
                    _logger.Info($"新增岗位: {post.PostName} ({post.PostCode})");
                }
                else
                {
                    // 更新现有岗位信息
                    existingPost.PostName = post.PostName;
                    existingPost.OrderNum = post.OrderNum;
                    existingPost.PostStatus = post.PostStatus;
                    existingPost.Remark = post.Remark;
                    existingPost.UpdateBy = "Takt365";
                    existingPost.UpdateTime = DateTime.Now;

                    await PostRepository.UpdateAsync(existingPost);
                    updateCount++;
                    _logger.Info($"更新岗位: {post.PostName} ({post.PostCode})");
                }
            }

            _logger.Info($"岗位数据初始化完成: 新增 {insertCount} 条, 更新 {updateCount} 条");
            return (insertCount, updateCount);
        }
        catch (Exception ex)
        {
            _logger.Error($"初始化岗位数据失败: {ex.Message}", ex);
            throw;
        }
    }
}

/// <summary>
/// 认证种子数据初始化结果
/// </summary>
public class IdentitySeedResult
{

    /// <summary>
    /// 角色初始化结果
    /// </summary>
    public (int insertCount, int updateCount) RoleResult { get; set; }

    /// <summary>
    /// 用户初始化结果
    /// </summary>
    public (int insertCount, int updateCount) UserResult { get; set; }

    /// <summary>
    /// 岗位初始化结果
    /// </summary>
    public (int insertCount, int updateCount) PostResult { get; set; }

    /// <summary>
    /// 获取总插入数量
    /// </summary>
    /// <returns>总插入数量</returns>
    public int GetTotalInsertCount()
    {
        return RoleResult.insertCount + UserResult.insertCount + PostResult.insertCount;
    }

    /// <summary>
    /// 获取总更新数量
    /// </summary>
    /// <returns>总更新数量</returns>
    public int GetTotalUpdateCount()
    {
        return RoleResult.updateCount + UserResult.updateCount + PostResult.updateCount;
    }
}






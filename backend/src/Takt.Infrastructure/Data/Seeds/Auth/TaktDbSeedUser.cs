//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedUser.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 用户数据提供类
//===================================================================

using Takt.Shared.Options;
using Takt.Shared.Utils;
using Takt.Domain.Entities.Identity;
using Microsoft.Extensions.Options;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 用户数据提供类
/// </summary>
public class TaktDbSeedUser
{
    private readonly IOptions<TaktPasswordPolicyOptions> _passwordPolicyOptions;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="passwordPolicyOptions">密码策略配置选项</param>
    public TaktDbSeedUser(IOptions<TaktPasswordPolicyOptions> passwordPolicyOptions)
    {
        _passwordPolicyOptions = passwordPolicyOptions ?? throw new ArgumentNullException(nameof(passwordPolicyOptions));
    }

    /// <summary>
    /// 获取默认用户数据
    /// </summary>
    /// <returns>用户数据列表</returns>
    public List<TaktUser> GetDefaultUsers()
    {
        var users = new List<TaktUser>();

        // 使用配置中的默认密码创建哈希值
        var (adminHash, adminSalt, adminIterations) = TaktPasswordUtils.CreateHash(_passwordPolicyOptions.Value.DefaultPassword);
        var (developerHash, developerSalt, developerIterations) = TaktPasswordUtils.CreateHash(_passwordPolicyOptions.Value.DefaultPassword);
        var (testerHash, testerSalt, testerIterations) = TaktPasswordUtils.CreateHash(_passwordPolicyOptions.Value.DefaultPassword);
        var (userHash, userSalt, userIterations) = TaktPasswordUtils.CreateHash(_passwordPolicyOptions.Value.DefaultPassword);
        var (demoHash, demoSalt, demoIterations) = TaktPasswordUtils.CreateHash(_passwordPolicyOptions.Value.DefaultPassword);

        users.Add(new TaktUser
        {
            UserName = "admin",
            NickName = "超级管理员",
            RealName = "超级管理员",
            FullName = "超级管理员",
            EnglishName = "System Administrator",
            UserType = 2,
            Email = "admin@Takt365.net",
            PhoneNumber = "13800000001",
            Gender = 0,
            Avatar = "/avatar/default.gif",
            UserStatus = 0,
            IsLock = 0,
            ErrorLimit = 0,
            LoginCount = 0,
            Password = adminHash,
            Salt = adminSalt,
            Iterations = adminIterations
        });

        users.Add(new TaktUser
        {
            UserName = "developer",
            NickName = "开发工程师",
            RealName = "开发工程师",
            FullName = "开发工程师",
            EnglishName = "Developer",
            UserType = 1,
            Email = "developer@lean.com",
            PhoneNumber = "13800000002",
            Gender = 0,
            Avatar = "/avatar/default.gif",
            UserStatus = 0,
            IsLock = 0,
            ErrorLimit = 0,
            LoginCount = 0,
            Password = developerHash,
            Salt = developerSalt,
            Iterations = developerIterations
        });

        users.Add(new TaktUser
        {
            UserName = "tester",
            NickName = "测试工程师",
            RealName = "测试工程师",
            FullName = "测试工程师",
            EnglishName = "Test Engineer",
            UserType = 1,
            Email = "tester@lean.com",
            PhoneNumber = "13800000003",
            Gender = 0,
            Avatar = "/avatar/default.gif",
            UserStatus = 0,
            IsLock = 0,
            ErrorLimit = 0,
            LoginCount = 0,
            Password = testerHash,
            Salt = testerSalt,
            Iterations = testerIterations
        });

        users.Add(new TaktUser
        {
            UserName = "user",
            NickName = "普通用户",
            RealName = "普通用户",
            FullName = "普通用户",
            EnglishName = "Normal User",
            UserType = 1,
            Email = "user@lean.com",
            PhoneNumber = "13800000004",
            Gender = 0,
            Avatar = "/avatar/default.gif",
            UserStatus = 0,
            IsLock = 0,
            ErrorLimit = 0,
            LoginCount = 0,
            Password = userHash,
            Salt = userSalt,
            Iterations = userIterations
        });

        users.Add(new TaktUser
        {
            UserName = "demo",
            NickName = "演示用户",
            RealName = "演示用户",
            FullName = "演示用户",
            EnglishName = "Demo User",
            UserType = 1,
            Email = "demo@lean.com",
            PhoneNumber = "13800000005",
            Gender = 0,
            Avatar = "/avatar/default.gif",
            UserStatus = 0,
            IsLock = 0,
            ErrorLimit = 0,
            LoginCount = 0,
            Password = demoHash,
            Salt = demoSalt,
            Iterations = demoIterations
        });

        return users;
    }
}



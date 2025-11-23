//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktIpUtils.cs
// 创建者 : Claude
// 创建时间: 2024-12-01 14:30
// 版本号 : V0.0.1
// 框架版本: .NET 8.0
// 描述   : IP地址工具类 - 合并IP位置查询和本机IP获取功能
//===================================================================

using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using IP2Region.Net.XDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;

namespace Takt.Shared.Utils;

/// <summary>
/// IP地址工具类
/// </summary>
/// <remarks>
/// 提供完整的IP地址处理功能：
/// 1. 客户端IP地址获取（支持代理环境）
/// 2. IP位置查询（使用IP2Region数据库）
/// 3. 本机内网IP地址获取
/// 4. 网络信息获取
/// 5. 支持IPv4和IPv6地址
/// </remarks>
public static class TaktIpUtils
{
  #region IP位置查询相关

  private static Searcher? _searcherV4;
  private static Searcher? _searcherV6;
  private static readonly Regex _ipv4Regex = new(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", RegexOptions.Compiled);
  private static readonly Regex _ipv6Regex = new(@"^(?:[0-9a-fA-F]{1,4}:){7}[0-9a-fA-F]{1,4}$|^::1$|^::$", RegexOptions.Compiled);
  private static IWebHostEnvironment? _webHostEnvironment;

  /// <summary>
  /// 初始化 Searcher
  /// </summary>
  private static Searcher? InitializeSearcher(string dbPath, string dbType)
  {
    // 转换为绝对路径，确保路径正确
    dbPath = Path.GetFullPath(dbPath);
    Debug.WriteLine($"[{dbType}] 数据库文件路径: {dbPath}");

    if (!File.Exists(dbPath))
    {
      var fileNotFoundError = $"[{dbType}] IP2Region数据库文件不存在: {dbPath}";
      Debug.WriteLine(fileNotFoundError);
      return null;
    }

    // 验证文件是否可读且大小合理（至少应该大于0）
    var fileInfo = new FileInfo(dbPath);
    if (fileInfo.Length == 0)
    {
      var emptyFileError = $"[{dbType}] IP2Region数据库文件为空: {dbPath}";
      Debug.WriteLine(emptyFileError);
      return null;
    }

    // 验证文件大小是否合理（xdb 文件通常至少几MB）
    // IPv4 数据库通常 > 10MB，IPv6 数据库可能更大
    var minSize = dbType == "IPv4" ? 10 * 1024 * 1024 : 1 * 1024 * 1024; // IPv4 至少 10MB，IPv6 至少 1MB
    if (fileInfo.Length < minSize)
    {
      var sizeError = $"[{dbType}] IP2Region数据库文件大小异常: {dbPath}, 大小: {fileInfo.Length} 字节, 预期至少: {minSize} 字节";
      Debug.WriteLine(sizeError);
      // 不直接返回 null，继续尝试初始化，因为某些情况下文件可能较小但仍然有效
    }

    Debug.WriteLine($"[{dbType}] 数据库文件大小: {fileInfo.Length} 字节 ({fileInfo.Length / 1024.0 / 1024.0:F2} MB)");

    // 检查文件是否被锁定
    try
    {
      using (var fileStream = File.Open(dbPath, FileMode.Open, FileAccess.Read, FileShare.Read))
      {
        Debug.WriteLine($"[{dbType}] 数据库文件可以正常访问");
      }
    }
    catch (IOException ioEx)
    {
      var lockedFileError = $"[{dbType}] IP2Region数据库文件被锁定或无法访问: {ioEx.Message}";
      Debug.WriteLine(lockedFileError);
      return null;
    }

    // 尝试使用不同的缓存策略初始化
    // 根据官方性能测试：Content (101.5 ns) > VectorIndex (7,488.1 ns) > File (11,686.9 ns)
    // 优先使用 Content 策略以获得最佳性能
    Searcher? searcher = null;
    Exception? lastException = null;
    var errorDetails = new List<string>();

    // 首先尝试使用 Content 缓存策略（性能最好，根据官方性能测试）
    try
    {
      Debug.WriteLine($"[{dbType}] 尝试使用 Content 缓存策略初始化（性能最佳）...");
      searcher = new Searcher(CachePolicy.Content, dbPath);

      // 使用多个测试 IP 验证文件格式是否正确
      var testIps = dbType == "IPv4"
        ? new[] { "127.0.0.1", "8.8.8.8", "114.114.114.114" }
        : new[] { "::1", "2001:4860:4860::8888" };

      bool allTestsPassed = true;
      foreach (var testIp in testIps)
      {
        try
        {
          var testResult = searcher.Search(testIp);
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询成功: {testResult}");
        }
        catch (Exception testEx)
        {
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询失败: {testEx.GetType().Name} - {testEx.Message}");
          allTestsPassed = false;
          break;
        }
      }

      if (allTestsPassed)
      {
        Debug.WriteLine($"[{dbType}] IP2Region初始化成功（Content策略），所有测试通过");
        return searcher;
      }
      else
      {
        throw new InvalidOperationException("测试查询失败，文件可能不完整或格式不正确");
      }
    }
    catch (Exception ex)
    {
      lastException = ex;
      var errorMsg = $"[{dbType}] Content 缓存策略初始化失败: {ex.GetType().Name} - {ex.Message}";
      if (ex.InnerException != null)
      {
        errorMsg += $"\n内部异常: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}";
      }
      errorDetails.Add(errorMsg);
      Debug.WriteLine(errorMsg);
      searcher = null;
    }

    // 如果 Content 策略失败，尝试使用 VectorIndex 缓存策略（性能次之，内存占用较小）
    try
    {
      Debug.WriteLine($"[{dbType}] 尝试使用 VectorIndex 缓存策略初始化...");
      searcher = new Searcher(CachePolicy.VectorIndex, dbPath);

      // 使用多个测试 IP 验证文件格式是否正确
      var testIps = dbType == "IPv4"
        ? new[] { "127.0.0.1", "8.8.8.8", "114.114.114.114" }
        : new[] { "::1", "2001:4860:4860::8888" };

      bool allTestsPassed = true;
      foreach (var testIp in testIps)
      {
        try
        {
          var testResult = searcher.Search(testIp);
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询成功: {testResult}");
        }
        catch (Exception testEx)
        {
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询失败: {testEx.GetType().Name} - {testEx.Message}");
          allTestsPassed = false;
          break;
        }
      }

      if (allTestsPassed)
      {
        Debug.WriteLine($"[{dbType}] IP2Region初始化成功（VectorIndex策略），所有测试通过");
        return searcher;
      }
      else
      {
        throw new InvalidOperationException("测试查询失败，文件可能不完整或格式不正确");
      }
    }
    catch (Exception ex)
    {
      lastException = ex;
      var errorMsg = $"[{dbType}] VectorIndex 缓存策略初始化失败: {ex.GetType().Name} - {ex.Message}";
      if (ex.InnerException != null)
      {
        errorMsg += $"\n内部异常: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}";
      }
      errorDetails.Add(errorMsg);
      Debug.WriteLine(errorMsg);
      searcher = null;
    }

    // 如果 VectorIndex 策略也失败，尝试使用 File 缓存策略（最后备选，性能最差但最稳定）
    try
    {
      Debug.WriteLine($"[{dbType}] 尝试使用 File 缓存策略初始化...");
      searcher = new Searcher(CachePolicy.File, dbPath);

      // 使用多个测试 IP 验证文件格式是否正确
      var testIps = dbType == "IPv4"
        ? new[] { "127.0.0.1", "8.8.8.8", "114.114.114.114" }
        : new[] { "::1", "2001:4860:4860::8888" };

      bool allTestsPassed = true;
      foreach (var testIp in testIps)
      {
        try
        {
          var testResult = searcher.Search(testIp);
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询成功: {testResult}");
        }
        catch (Exception testEx)
        {
          Debug.WriteLine($"[{dbType}] 测试 IP {testIp} 查询失败: {testEx.GetType().Name} - {testEx.Message}");
          allTestsPassed = false;
          break;
        }
      }

      if (allTestsPassed)
      {
        Debug.WriteLine($"[{dbType}] IP2Region初始化成功（File策略），所有测试通过");
        return searcher;
      }
      else
      {
        throw new InvalidOperationException("测试查询失败，文件可能不完整或格式不正确");
      }
    }
    catch (Exception ex)
    {
      lastException = ex;
      var errorMsg = $"[{dbType}] File 缓存策略初始化失败: {ex.GetType().Name} - {ex.Message}";
      if (ex.InnerException != null)
      {
        errorMsg += $"\n内部异常: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}";
      }
      errorDetails.Add(errorMsg);
      Debug.WriteLine(errorMsg);
      searcher = null;
    }

    // 如果所有策略都失败，记录错误
    var initError = $"[{dbType}] 所有缓存策略初始化都失败。\n" +
                   $"文件路径: {dbPath}\n" +
                   $"文件大小: {fileInfo.Length} 字节\n" +
                   $"最后错误: {lastException?.GetType().Name} - {lastException?.Message}\n" +
                   $"详细错误信息:\n{string.Join("\n", errorDetails)}";
    Debug.WriteLine(initError);
    return null;
  }

  /// <summary>
  /// 设置Web环境
  /// </summary>
  /// <param name="webHostEnvironment">Web主机环境</param>
  /// <returns>是否初始化成功</returns>
  public static bool SetWebHostEnvironment(IWebHostEnvironment webHostEnvironment)
  {
    try
    {
      _webHostEnvironment = webHostEnvironment;

      // 初始化 IPv4 数据库
      var dbPathV4 = Path.Combine(_webHostEnvironment.WebRootPath, "IpRegion", "ip2region_v4.xdb");
      _searcherV4 = InitializeSearcher(dbPathV4, "IPv4");

      // 初始化 IPv6 数据库
      var dbPathV6 = Path.Combine(_webHostEnvironment.WebRootPath, "IpRegion", "ip2region_v6.xdb");
      _searcherV6 = InitializeSearcher(dbPathV6, "IPv6");

      // 检查是否至少有一个数据库初始化成功
      if (_searcherV4 == null && _searcherV6 == null)
      {
        var initError = $"IP2Region初始化失败：IPv4 和 IPv6 数据库都无法初始化。\n" +
                       $"IPv4 文件路径: {dbPathV4}\n" +
                       $"IPv6 文件路径: {dbPathV6}\n" +
                       $"可能的原因:\n" +
                       $"1. IP2Region.Net 库版本与 xdb 文件版本不兼容\n" +
                       $"2. xdb 文件损坏或不完整\n" +
                       $"3. 文件格式不正确\n" +
                       $"建议: 请重新下载 ip2region_v4.xdb 和 ip2region_v6.xdb 文件，确保使用最新版本";
        Debug.WriteLine(initError);
        throw new InvalidOperationException(initError);
      }

      if (_searcherV4 != null)
      {
        Debug.WriteLine("IPv4 数据库初始化成功");
      }
      else
      {
        Debug.WriteLine("警告: IPv4 数据库初始化失败，IPv4 地址查询将不可用");
      }

      if (_searcherV6 != null)
      {
        Debug.WriteLine("IPv6 数据库初始化成功");
      }
      else
      {
        Debug.WriteLine("警告: IPv6 数据库初始化失败，IPv6 地址查询将不可用");
      }

      return true;
    }
    catch (Exception ex)
    {
      var finalError = $"初始化IP2Region失败: {ex.GetType().Name} - {ex.Message}\n文件路径: {Path.Combine(_webHostEnvironment?.WebRootPath ?? "Unknown", "IpRegion")}\n{ex.StackTrace}";
      Debug.WriteLine(finalError);
      _searcherV4 = null;
      _searcherV6 = null;
      throw new InvalidOperationException(finalError, ex);
    }
  }

  /// <summary>
  /// 获取IP地址的位置信息
  /// </summary>
  /// <param name="ipAddress">IP地址</param>
  /// <returns>位置信息，格式：国家|区域|省份|城市|ISP</returns>
  /// <remarks>
  /// 使用IP2Region库查询IP地址的地理位置信息
  /// 如果是内网IP或查询失败则返回特定说明
  /// </remarks>
  public static async Task<string> GetLocationAsync(string ipAddress)
  {
    if (string.IsNullOrEmpty(ipAddress))
      return "Unknown Location";

    // 检查是否是本地回环地址
    if (ipAddress == "::1" || ipAddress == "127.0.0.1")
      return "本地(Local network)";

    // 判断是 IPv4 还是 IPv6
    bool isIPv4 = _ipv4Regex.IsMatch(ipAddress);
    bool isIPv6 = _ipv6Regex.IsMatch(ipAddress);

    if (!isIPv4 && !isIPv6)
      return "Unknown Location";

    // 选择对应的 Searcher
    Searcher? searcher = null;
    if (isIPv4)
    {
      if (_searcherV4 == null)
      {
        Debug.WriteLine($"IPv4 地址查询失败: {ipAddress}，IPv4 数据库未初始化");
        return "Unknown Location";
      }
      searcher = _searcherV4;
    }
    else // IPv6
    {
      if (_searcherV6 == null)
      {
        Debug.WriteLine($"IPv6 地址查询失败: {ipAddress}，IPv6 数据库未初始化");
        return "Unknown Location";
      }
      searcher = _searcherV6;
    }

    try
    {
      // 使用 System.Net.IPAddress 进行严格的 IP 地址验证
      if (!IPAddress.TryParse(ipAddress, out var parsedIp))
      {
        Debug.WriteLine($"IP地址格式无效，无法解析: {ipAddress}");
        return "Unknown Location";
      }

      // 验证 IP 地址类型是否匹配
      if (isIPv4 && parsedIp.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
      {
        Debug.WriteLine($"IP地址类型不匹配（期望IPv4，实际为其他类型）: {ipAddress}");
        return "Unknown Location";
      }

      if (isIPv6 && parsedIp.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
      {
        Debug.WriteLine($"IP地址类型不匹配（期望IPv6，实际为其他类型）: {ipAddress}");
        return "Unknown Location";
      }

      // 使用 Task.Run 包装同步调用以保持异步接口
      // 添加超时保护，避免长时间阻塞
      var searchTask = Task.Run(() =>
      {
        try
        {
          return searcher.Search(ipAddress);
        }
        catch (ArgumentOutOfRangeException)
        {
          // 重新抛出，让外层捕获处理
          throw;
        }
        catch (IndexOutOfRangeException)
        {
          // 重新抛出，让外层捕获处理
          throw;
        }
      });

      // 设置超时时间为 5 秒
      if (await Task.WhenAny(searchTask, Task.Delay(5000)) == searchTask)
      {
        var result = await searchTask;
        return result ?? "Unknown Location";
      }
      else
      {
        Debug.WriteLine($"IP位置查询超时: {ipAddress}");
        return "Unknown Location";
      }
    }
    catch (ArgumentOutOfRangeException ex)
    {
      // 处理文件读取范围错误（可能是文件格式问题、文件损坏或 IP 地址不在数据库中）
      Debug.WriteLine($"IP位置查询失败（范围错误）: {ipAddress}, 错误: {ex.Message}");
      Debug.WriteLine($"参数名: {ex.ParamName}");
      Debug.WriteLine($"这可能表示数据库文件可能已损坏或不完整，建议重新下载数据库文件");
      return "Unknown Location";
    }
    catch (IndexOutOfRangeException ex)
    {
      // 处理索引越界错误（通常是数据库文件问题）
      Debug.WriteLine($"IP位置查询失败（索引越界）: {ipAddress}, 错误: {ex.Message}");
      Debug.WriteLine($"这可能表示数据库文件格式不正确或已损坏，建议重新下载数据库文件");
      return "Unknown Location";
    }
    catch (TaskCanceledException)
    {
      Debug.WriteLine($"IP位置查询被取消: {ipAddress}");
      return "Unknown Location";
    }
    catch (Exception ex)
    {
      // 记录其他错误但不抛出异常
      Debug.WriteLine($"IP位置查询失败: {ipAddress}, 错误类型: {ex.GetType().Name}, 错误: {ex.Message}");
      if (ex.InnerException != null)
      {
        Debug.WriteLine($"内部异常: {ex.InnerException.GetType().Name} - {ex.InnerException.Message}");
      }
      return "Unknown Location";
    }
  }

  /// <summary>
  /// 判断是否是内网IP
  /// </summary>
  private static bool IsInternalIp(IPAddress ip)
  {
    if (IPAddress.IsLoopback(ip)) return true;

    byte[] bytes = ip.GetAddressBytes();
    return bytes[0] switch
    {
      10 => true,
      172 => bytes[1] >= 16 && bytes[1] <= 31,
      192 => bytes[1] == 168,
      _ => false
    };
  }

  #endregion

  #region 客户端IP获取相关

  /// <summary>
  /// 获取客户端IP地址
  /// </summary>
  /// <param name="httpContext">HTTP上下文</param>
  /// <param name="defaultIp">默认IP地址，当无法获取时返回</param>
  /// <returns>客户端IP地址</returns>
  /// <remarks>
  /// 支持多种环境下的IP获取：
  /// 1. 代理服务器环境（X-Forwarded-For, X-Real-IP）
  /// 2. 直连环境（RemoteIpAddress）
  /// 3. 自动处理IPv4和IPv6地址
  /// 4. 智能处理本地回环地址
  /// </remarks>
  public static string GetClientIpAddress(HttpContext? httpContext, string defaultIp = "127.0.0.1")
  {
    if (httpContext == null)
    {
      return defaultIp;
    }

    try
    {
      // 优先从X-Forwarded-For获取（代理服务器环境）
      var forwardedFor = httpContext.Request.Headers["X-Forwarded-For"].ToString();
      if (!string.IsNullOrEmpty(forwardedFor))
      {
        // 取第一个IP（最原始的客户端IP）
        var ips = forwardedFor.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (ips.Length > 0)
        {
          var ip = ips[0].Trim();
          return ProcessClientIpAddress(ip, defaultIp);
        }
      }

      // 其次从X-Real-IP获取（Nginx等反向代理）
      var realIp = httpContext.Request.Headers["X-Real-IP"].ToString();
      if (!string.IsNullOrEmpty(realIp))
      {
        return ProcessClientIpAddress(realIp, defaultIp);
      }

      // 最后从RemoteIpAddress获取（直连环境）
      var remoteIp = httpContext.Connection.RemoteIpAddress?.ToString();
      if (!string.IsNullOrEmpty(remoteIp))
      {
        return ProcessClientIpAddress(remoteIp, defaultIp);
      }

      // 如果都无法获取，尝试获取本机内网IP
      var localIp = GetPreferredLocalIpAddress();
      return !string.IsNullOrEmpty(localIp) ? localIp : defaultIp;
    }
    catch
    {
      return defaultIp;
    }
  }

  /// <summary>
  /// 处理客户端IP地址
  /// </summary>
  /// <param name="ip">原始IP地址</param>
  /// <param name="defaultIp">默认IP地址</param>
  /// <returns>处理后的IP地址</returns>
  private static string ProcessClientIpAddress(string ip, string defaultIp)
  {
    if (string.IsNullOrEmpty(ip))
    {
      return defaultIp;
    }

    // 处理IPv6回环地址
    if (ip == "::1")
    {
      // 尝试获取本机内网IP
      var localIp = GetPreferredLocalIpAddress();
      return !string.IsNullOrEmpty(localIp) ? localIp : "127.0.0.1";
    }

    // 处理IPv4回环地址
    if (ip == "127.0.0.1")
    {
      // 尝试获取本机内网IP
      var localIp = GetPreferredLocalIpAddress();
      return !string.IsNullOrEmpty(localIp) ? localIp : ip;
    }

    return ip;
  }

  #endregion

  #region 本机IP获取相关

  /// <summary>
  /// 获取本机内网IP地址列表
  /// </summary>
  /// <returns>内网IP地址列表</returns>
  public static List<string> GetLocalIpAddresses()
  {
    var localIps = new List<string>();

    try
    {
      // 获取所有网络接口
      var networkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

      foreach (var networkInterface in networkInterfaces)
      {
        // 只处理活跃的以太网和无线网卡
        if (networkInterface.OperationalStatus != OperationalStatus.Up ||
            (networkInterface.NetworkInterfaceType != NetworkInterfaceType.Ethernet &&
             networkInterface.NetworkInterfaceType != NetworkInterfaceType.Wireless80211))
        {
          continue;
        }

        // 获取IP地址信息
        var ipProperties = networkInterface.GetIPProperties();
        foreach (var unicastAddress in ipProperties.UnicastAddresses)
        {
          var ip = unicastAddress.Address;

          // 只处理IPv4地址
          if (ip.AddressFamily != AddressFamily.InterNetwork)
            continue;

          // 过滤掉回环地址
          if (IPAddress.IsLoopback(ip))
            continue;

          // 检查是否是内网地址
          if (IsInternalIp(ip))
          {
            localIps.Add(ip.ToString());
          }
        }
      }
    }
    catch (Exception ex)
    {
      System.Diagnostics.Debug.WriteLine($"获取本机内网IP失败: {ex.Message}");
    }

    return localIps;
  }

  /// <summary>
  /// 获取首选的本机内网IP地址
  /// </summary>
  /// <returns>首选的内网IP地址，如果没有则返回空字符串</returns>
  public static string GetPreferredLocalIpAddress()
  {
    var localIps = GetLocalIpAddresses();

    if (localIps.Count == 0)
      return string.Empty;

    // 优先选择192.168.x.x网段的IP
    var preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("192.168."));
    if (!string.IsNullOrEmpty(preferredIp))
      return preferredIp;

    // 其次选择10.x.x.x网段的IP
    preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("10."));
    if (!string.IsNullOrEmpty(preferredIp))
      return preferredIp;

    // 最后选择172.16-31.x.x网段的IP
    preferredIp = localIps.FirstOrDefault(ip => ip.StartsWith("172."));
    if (!string.IsNullOrEmpty(preferredIp))
      return preferredIp;

    // 如果都没有，返回第一个
    return localIps.First();
  }

  /// <summary>
  /// 获取本机主机名
  /// </summary>
  /// <returns>主机名</returns>
  public static string GetHostName()
  {
    try
    {
      return Dns.GetHostName();
    }
    catch (Exception ex)
    {
      System.Diagnostics.Debug.WriteLine($"获取主机名失败: {ex.Message}");
      return "Unknown";
    }
  }

  /// <summary>
  /// 获取本机网络信息
  /// </summary>
  /// <returns>网络信息字典</returns>
  public static Dictionary<string, object> GetLocalNetworkInfo()
  {
    var networkInfo = new Dictionary<string, object>
    {
      ["HostName"] = GetHostName(),
      ["LocalIpAddresses"] = GetLocalIpAddresses(),
      ["PreferredLocalIp"] = GetPreferredLocalIpAddress()
    };

    return networkInfo;
  }

  #endregion
}




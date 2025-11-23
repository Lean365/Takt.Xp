# 日志规范

## 1. 日志级别

1. Trace
   - 最详细的日志信息
   - 主要用于开发调试
   - 包含敏感信息
   - 生产环境不开启

2. Debug
   - 调试信息
   - 可能包含敏感信息
   - 生产环境不开启
   - 用于问题诊断

3. Information
   - 正常的系统操作信息
   - 不包含敏感信息
   - 记录重要的业务操作
   - 生产环境开启

4. Warning
   - 潜在的问题警告
   - 不影响系统运行
   - 需要关注的异常情况
   - 生产环境开启

5. Error
   - 错误信息
   - 影响功能但不影响系统
   - 需要立即关注
   - 生产环境开启

6. Fatal
   - 致命错误
   - 导致系统崩溃
   - 需要立即处理
   - 生产环境开启

## 2. 日志内容

1. 基础信息
   - 时间戳
   - 日志级别
   - 类名和方法名
   - 线程ID
   - 请求ID
   - 用户信息
   - 租户信息

2. 上下文信息
   - 请求URL
   - 请求方法
   - 请求参数
   - 响应状态
   - 执行时间
   - 客户端信息

3. 异常信息
   - 异常类型
   - 异常消息
   - 异常堆栈
   - 内部异常
   - 异常上下文

## 3. 日志格式

1. 日志文件命名
   ```
   ${shortdate}/${level}.log
   ```

2. 日志内容格式
   ```
   ${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=tostring}
   ```

3. 结构化日志
   ```json
   {
     "Timestamp": "2024-01-16 12:34:56",
     "Level": "Error",
     "Logger": "Takt.WebApi.Controllers.TaktUserController",
     "Message": "用户创建失败",
     "Exception": {
       "Type": "TaktBusinessException",
       "Message": "用户名已存在",
       "StackTrace": "..."
     },
     "Properties": {
       "UserId": "123",
       "UserName": "admin",
       "RequestId": "xyz",
       "RequestPath": "/api/user",
       "RequestMethod": "POST"
     }
   }
   ```

## 4. 日志配置

1. NLog配置
   ```xml
   <?xml version="1.0" encoding="utf-8" ?>
   <nlog xmlns="http://www.nlog-project.org/schemas/NLog.xsd"
         xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">
     <targets>
       <!-- 文件日志 -->
       <target xsi:type="File" 
               name="file" 
               fileName="${basedir}/logs/${shortdate}/${level}.log"
               layout="${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=tostring}" />
       
       <!-- 控制台日志 -->
       <target xsi:type="Console" 
               name="console" 
               layout="${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=tostring}" />
     </targets>

     <rules>
       <logger name="*" minlevel="Trace" writeTo="console" />
       <logger name="*" minlevel="Info" writeTo="file" />
     </rules>
   </nlog>
   ```

## 5. 日志记录规范

1. 操作日志
   ```csharp
   [TaktLog]
   public class TaktUserController
   {
       [HttpPost]
       public async Task<TaktApiResult<long>> CreateUser([FromBody] TaktUserCreateDto input)
       {
           _logger.Info("开始创建用户", new { UserName = input.UserName });
           try
           {
               var result = await _userService.CreateUserAsync(input);
               _logger.Info("用户创建成功", new { UserId = result });
               return Success(result);
           }
           catch (Exception ex)
           {
               _logger.Error(ex, "用户创建失败", new { UserName = input.UserName });
               throw;
           }
       }
   }
   ```

2. 性能日志
   ```csharp
   public async Task<TaktUserDto> GetUserAsync(long id)
   {
       using (_logger.BeginScope("获取用户信息"))
       {
           var stopwatch = Stopwatch.StartNew();
           try
           {
               var user = await _userRepository.GetByIdAsync(id);
               stopwatch.Stop();
               _logger.Info("获取用户信息完成", new 
               { 
                   UserId = id,
                   ElapsedMilliseconds = stopwatch.ElapsedMilliseconds 
               });
               return user.Adapt<TaktUserDto>();
           }
           catch (Exception ex)
           {
               stopwatch.Stop();
               _logger.Error(ex, "获取用户信息失败", new 
               { 
                   UserId = id,
                   ElapsedMilliseconds = stopwatch.ElapsedMilliseconds 
               });
               throw;
           }
       }
   }
   ```

## 6. 最佳实践

1. 日志级别选择
   - 正常操作使用Info
   - 异常情况使用Error
   - 系统崩溃使用Fatal
   - 调试信息使用Debug
   - 详细信息使用Trace

2. 日志内容
   - 记录必要的上下文
   - 避免记录敏感信息
   - 使用结构化日志
   - 保持信息完整性

3. 性能考虑
   - 合理使用日志级别
   - 避免过多的日志
   - 使用异步日志
   - 定期清理日志

4. 安全考虑
   - 脱敏敏感信息
   - 控制日志访问权限
   - 定期备份日志
   - 及时清理过期日志 

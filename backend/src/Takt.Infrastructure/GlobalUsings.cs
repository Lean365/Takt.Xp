//===================================================================
// 项目名 : Takt.Xp
// 文件名 : GlobalUsings.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 09:30
// 版本号 : V0.0.1
// 描述    : 基础设施层全局 Using 声明
//===================================================================

// System 命名空间
global using Takt.Application.Services.Routine.Contract;
global using Takt.Application.Services.Routine.Email;
global using Takt.Application.Services.Routine.Metting;
global using Takt.Application.Services.Routine.News;
global using Takt.Application.Services.Routine.Notice;
global using Takt.Application.Services.Routine.Project;
global using Takt.Application.Services.Routine.Quartz;
global using Takt.Application.Services.Routine.Schedule;
global using Takt.Application.Services.Routine.Vehicle;
global using Takt.Application.Services.Routine.SignalR;
// 项目依赖
global using Takt.Domain.Entities;
global using Takt.Domain.Entities.Logistics.Manufacturing.Master;
global using Takt.Domain.IServices.Extensions;
global using Takt.Domain.Repositories;
global using Takt.Domain.Data;
global using Takt.Infrastructure.Repositories;
// Microsoft 扩展
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
// 第三方库
global using SqlSugar;
global using StackExchange.Redis;
global using System;
global using System.Collections.Generic;
global using System.Data;
global using System.Threading.Tasks;
global using Newtonsoft.Json;





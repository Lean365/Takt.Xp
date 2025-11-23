//===================================================================
// 项目名 : Takt.Xp
// 文件名 : GlobalUsings.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-22 09:30
// 版本号 : V0.0.1
// 描述    : 应用层全局 Using 声明
//===================================================================

// System 命名空间
global using Takt.Application.Dtos.Logging;
global using Takt.Application.Dtos.Generator;
global using Takt.Application.Dtos.Identity;
global using Takt.Application.Dtos.Routine.Contract;
global using Takt.Application.Dtos.Routine.DataDictionary;
global using Takt.Application.Dtos.Routine.Document;
global using Takt.Application.Dtos.Routine.Email;
global using Takt.Application.Dtos.Routine.I18n;
global using Takt.Application.Dtos.Routine.Metting;
global using Takt.Application.Dtos.Routine.News;
global using Takt.Application.Dtos.Routine.Notice;
global using Takt.Application.Dtos.Routine.Numbering;
global using Takt.Application.Dtos.Routine.Project;
global using Takt.Application.Dtos.Routine.Quartz;
global using Takt.Application.Dtos.Routine.Schedule;
global using Takt.Application.Dtos.Routine.Settings;
global using Takt.Application.Dtos.Routine.SignalR;
global using Takt.Application.Dtos.Routine.Vehicle;
global using Takt.Application.Dtos.Workflow;
global using Takt.Shared.Enums;
global using Takt.Shared.Exceptions;
global using Takt.Shared.Helpers;

// 项目依赖
global using Takt.Shared.Models;
global using Takt.Shared.Utils;
global using Takt.Domain.Data;
global using Takt.Domain.Entities;
global using Takt.Domain.Entities.Logging;
global using Takt.Domain.Entities.Generator;
global using Takt.Domain.Entities.Identity;
global using Takt.Domain.Entities.Routine.Contract;
global using Takt.Domain.Entities.Routine.Document;
global using Takt.Domain.Entities.Routine.Email;
global using Takt.Domain.Entities.Routine.Metting;
global using Takt.Domain.Entities.Routine.News;
global using Takt.Domain.Entities.Routine.Notice;
global using Takt.Domain.Entities.Routine.Numbering;
global using Takt.Domain.Entities.Routine.Project;
global using Takt.Domain.Entities.Routine.Quartz;
global using Takt.Domain.Entities.Routine.Schedule;
global using Takt.Domain.Entities.Routine.Vehicle;
global using Takt.Domain.Entities.Routine.SignalR;
global using Takt.Domain.Entities.Workflow;
global using Takt.Domain.IServices.Extensions;
global using Takt.Domain.Repositories;
global using Takt.Domain.Utils;
global using Takt.Domain.IServices.Security;
global using Takt.Domain.IServices.SignalR;
global using Mapster;
global using Newtonsoft.Json;
global using SqlSugar;
global using System;
global using System.Collections.Generic;
global using System.Linq.Expressions;
global using System.Threading.Tasks;

// Microsoft 扩展
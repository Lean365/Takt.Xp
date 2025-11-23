//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktMailTplService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 邮件模板服务实现
//===================================================================

using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine;
using Takt.Application.Dtos.Routine;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using System.IO;
using System.Linq;

namespace Takt.Application.Services.Routine.Email
{
    /// <summary>
    /// 邮件模板服务实现
    /// </summary>
    public class TaktMailTplService : TaktBaseService, ITaktMailTplService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取邮件模板仓储
        /// </summary>
        private ITaktRepository<TaktMailTpl> MailTplRepository => _repositoryFactory.GetBusinessRepository<TaktMailTpl>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktMailTplService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取邮件模板分页列表
        /// </summary>
        public async Task<TaktPagedResult<TaktMailTplDto>> GetListAsync(TaktMailTplQueryDto query)
        {
            var exp = Expressionable.Create<TaktMailTpl>();

            if (!string.IsNullOrEmpty(query.MailTplName))
                exp.And(x => x.MailTplName.Contains(query.MailTplName));

            if (!string.IsNullOrEmpty(query.MailTplCode))
                exp.And(x => x.MailTplCode.Contains(query.MailTplCode));

            if (query.Status.HasValue)
                exp.And(x => x.Status == query.Status.Value);

            if (query.StartTime.HasValue)
                exp.And(x => x.CreateTime >= query.StartTime.Value);

            if (query.EndTime.HasValue)
                exp.And(x => x.CreateTime <= query.EndTime.Value);

            if (!string.IsNullOrEmpty(query.CreateBy))
                exp.And(x => x.CreateBy == query.CreateBy);

            var result = await MailTplRepository.GetPagedListAsync(
                exp.ToExpression(),
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktMailTplDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktMailTplDto>>()
            };
        }

        /// <summary>
        /// 获取邮件模板详情
        /// </summary>
        public async Task<TaktMailTplDto> GetByIdAsync(long mailTplId)
        {
            var tmpl = await MailTplRepository.GetByIdAsync(mailTplId);
            if (tmpl == null)
                throw new TaktException(L("MailTmpl.NotFound", mailTplId));

            return tmpl.Adapt<TaktMailTplDto>();
        }

        /// <summary>
        /// 创建邮件模板
        /// </summary>
        public async Task<long> CreateAsync(TaktMailTplCreateDto input)
        {
            var tmpl = input.Adapt<TaktMailTpl>();
            tmpl.CreateTime = DateTime.Now;

            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldsExistsAsync(MailTplRepository, 
                new Dictionary<string, string> 
                { 
                    { "MailTplName", input.MailTplName },
                    { "MailTplCode", input.MailTplCode }
                });

            var result = await MailTplRepository.CreateAsync(tmpl);
            if (result <= 0)
                throw new TaktException(L("MailTmpl.CreateFailed"));

            return tmpl.Id;
        }

        /// <summary>
        /// 更新邮件模板
        /// </summary>
        public async Task<bool> UpdateAsync(long mailTplId, TaktMailTplDto input)
        {
            var tmpl = await MailTplRepository.GetByIdAsync(mailTplId);
            if (tmpl == null)
                throw new TaktException(L("MailTmpl.NotFound", mailTplId));

            // 验证字段是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldsExistsAsync(MailTplRepository, 
                new Dictionary<string, string> 
                { 
                    { "MailTplName", input.MailTplName },
                    { "MailTplCode", input.MailTplCode }
                }, 
                mailTplId);

            input.Adapt(tmpl);
            var result = await MailTplRepository.UpdateAsync(tmpl);
            return result > 0;
        }

        /// <summary>
        /// 删除邮件模板
        /// </summary>
        public async Task<bool> DeleteAsync(long mailTplId)
        {
            var tmpl = await MailTplRepository.GetByIdAsync(mailTplId);
            if (tmpl == null)
                throw new TaktException(L("MailTmpl.NotFound", mailTplId));

            var result = await MailTplRepository.DeleteAsync(mailTplId);
            return result > 0;
        }

        /// <summary>
        /// 批量删除邮件模板
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] mailTplIds)
        {
            if (mailTplIds == null || mailTplIds.Length == 0)
                throw new TaktException(L("MailTmpl.SelectToDelete"));

            foreach (var mailTplId in mailTplIds)
            {
                await DeleteAsync(mailTplId);
            }
            return true;
        }

        /// <summary>
        /// 导出邮件模板数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktMailTplQueryDto query, string sheetName = "MailTmpl")
        {
            try
            {
                var list = await MailTplRepository.GetListAsync(MailTplQueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktMailTplExportDto>>(), sheetName, L("MailTmpl.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("MailTmpl.ExportFailed"), ex);
                throw new TaktException(L("MailTmpl.ExportFailed"));
            }
        }

        /// <summary>
        /// 导入邮件模板数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "邮件模板数据")
        {
            try
            {
                var list = await TaktExcelHelper.ImportAsync<TaktMailTplImportDto>(fileStream, sheetName);
                if (list == null || !list.Any())
                    return (0, 0);
                int success = 0, fail = 0;
                foreach (var item in list)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(item.MailTplName))
                        {
                            _logger.Warn("导入邮件模板失败: 模板名称不能为空");
                            fail++;
                            continue;
                        }

                        if (string.IsNullOrEmpty(item.MailTplCode))
                        {
                            _logger.Warn("导入邮件模板失败: 模板编码不能为空");
                            fail++;
                            continue;
                        }

                        // 验证字段是否已存在
                        await TaktValidateUtils.ValidateFieldsExistsAsync(MailTplRepository, 
                            new Dictionary<string, string> 
                            { 
                                { "MailTplName", item.MailTplName },
                                { "MailTplCode", item.MailTplCode }
                            });
                        var entity = item.Adapt<TaktMailTpl>();
                        entity.CreateTime = DateTime.Now;
                        await MailTplRepository.CreateAsync(entity);
                        success++;
                    }
                    catch
                    {
                        fail++;
                    }
                }
                return (success, fail);
            }
            catch
            {
                return (0, 0);
            }
        }

        /// <summary>
        /// 获取邮件模板导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "邮件模板数据")
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktMailTplTemplateDto>(sheetName);
        }

        private Expression<Func<TaktMailTpl, bool>> MailTplQueryExpression(TaktMailTplQueryDto query)
        {
            return Expressionable.Create<TaktMailTpl>()
                .AndIF(!string.IsNullOrEmpty(query.MailTplName), x => x.MailTplName.Contains(query.MailTplName))
                .AndIF(!string.IsNullOrEmpty(query.MailTplCode), x => x.MailTplCode.Contains(query.MailTplCode))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime.Value)
                .AndIF(!string.IsNullOrEmpty(query.CreateBy), x => x.CreateBy == query.CreateBy)
                .ToExpression();
        }

        /// <summary>
        /// 获取指定用户的邮件模板状态统计
        /// </summary>
        /// <param name="createBy">创建者</param>
        /// <returns>状态-数量字典</returns>
        public async Task<Dictionary<int, int>> GetStatusStatisticsAsync(string createBy)
        {
            if (string.IsNullOrEmpty(createBy))
                throw new ArgumentNullException(nameof(createBy));

            var stats = await MailTplRepository.AsQueryable()
                .Where(x => x.CreateBy == createBy)
                .GroupBy(x => x.Status)
                .Select(x => new { Status = x.Status, Count = SqlFunc.AggregateCount(x.Status) })
                .ToListAsync();

            return stats.ToDictionary(x => x.Status, x => x.Count);
        }
    }
} 





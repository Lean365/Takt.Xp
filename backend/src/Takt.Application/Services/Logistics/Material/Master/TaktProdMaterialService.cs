//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktProdMaterialService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述: 生产物料服务实现
//===================================================================

using Takt.Application.Dtos.Logistics.Material.Master;
using Takt.Application.Services.Logistics.Material.Master;
using Takt.Shared.Models;
using Takt.Domain.Entities.Logistics.Material.Master;
using Takt.Domain.IServices.Extensions;
using Takt.Shared.Utils;

using Microsoft.AspNetCore.Http;
using SqlSugar;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace Takt.Application.Services.Logistics.Material.Master
{
    /// <summary>
    /// 生产物料服务实现
    /// </summary>
    public class TaktProdMaterialService : TaktBaseService, ITaktProdMaterialService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取生产物料仓储
        /// </summary>
        private ITaktRepository<TaktProdMaterial> ProdMaterialRepository => _repositoryFactory.GetBusinessRepository<TaktProdMaterial>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktProdMaterialService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取生产物料分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回分页结果</returns>
        public async Task<TaktPagedResult<TaktProdMaterialDto>> GetListAsync(TaktProdMaterialQueryDto query)
        {
            var predicate = QueryExpression(query);
            var result = await ProdMaterialRepository.GetPagedListAsync(
                predicate,
                query?.PageIndex ?? 1,
                query?.PageSize ?? 10,
                x => x.CreateTime,
                OrderByType.Desc);

            var dtoResult = new TaktPagedResult<TaktProdMaterialDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query?.PageIndex ?? 1,
                PageSize = query?.PageSize ?? 10,
                Rows = result.Rows.Adapt<List<TaktProdMaterialDto>>()
            };

            return dtoResult;
        }

        /// <summary>
        /// 根据ID获取生产物料详情
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回生产物料详情</returns>
        public async Task<TaktProdMaterialDto> GetByIdAsync(long id)
        {
            var prodMaterial = await ProdMaterialRepository.GetByIdAsync(id);
            if (prodMaterial == null)
                throw new TaktException(L("ProdMaterial.NotFound", id));

            return prodMaterial.Adapt<TaktProdMaterialDto>();
        }

        /// <summary>
        /// 根据工厂编码和物料编码获取生产物料详情
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="materialCode">物料编码</param>
        /// <returns>返回生产物料详情</returns>
        public async Task<TaktProdMaterialDto> GetByPlantAndMaterialCodeAsync(string plantCode, string materialCode)
        {
            var prodMaterial = await ProdMaterialRepository.GetFirstAsync(x => 
                x.PlantCode == plantCode && x.MaterialCode == materialCode);
            
            if (prodMaterial == null)
                throw new TaktException(L("ProdMaterial.NotFoundByCode", plantCode, materialCode));

            return prodMaterial.Adapt<TaktProdMaterialDto>();
        }

        /// <summary>
        /// 创建生产物料
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>返回生产物料ID</returns>
        public async Task<long> CreateAsync(TaktProdMaterialCreateDto input)
        {
            // 验证生产物料是否已存在（工厂编码+物料编码组合）
            var fieldValues = new Dictionary<string, string>
            {
                { "PlantCode", input.PlantCode },
                { "MaterialCode", input.MaterialCode }
            };
            await TaktValidateUtils.ValidateFieldsExistsAsync(ProdMaterialRepository, fieldValues);

            var entity = input.Adapt<TaktProdMaterial>();
            var id = await ProdMaterialRepository.CreateAsync(entity);
            
            return id;
        }

        /// <summary>
        /// 更新生产物料
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateAsync(TaktProdMaterialUpdateDto input)
        {
            var entity = await ProdMaterialRepository.GetByIdAsync(input.MaterialId);
            if (entity == null)
                throw new TaktException(L("ProdMaterial.NotFound", input.MaterialId));

            input.Adapt(entity);
            var result = await ProdMaterialRepository.UpdateAsync(entity);
            
            return result > 0;
        }

        /// <summary>
        /// 删除生产物料
        /// </summary>
        /// <param name="id">生产物料ID</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await ProdMaterialRepository.GetByIdAsync(id);
            if (entity == null)
                throw new TaktException(L("ProdMaterial.NotFound", id));

            var result = await ProdMaterialRepository.DeleteAsync(id);
            
            return result > 0;
        }

        /// <summary>
        /// 批量删除生产物料
        /// </summary>
        /// <param name="ids">生产物料ID集合</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] ids)
        {
            if (ids == null || ids.Length == 0)
                return false;

            var entities = await ProdMaterialRepository.GetListAsync(x => ids.Contains(x.Id));
            if (entities == null || !entities.Any())
                return false;

            var result = await ProdMaterialRepository.DeleteAsync(ids);
            
            return result > 0;
        }

        /// <summary>
        /// 更新生产物料状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>返回是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktProdMaterialStatusDto input)
        {
            var entity = await ProdMaterialRepository.GetByIdAsync(input.MaterialId);
            if (entity == null)
                throw new TaktException(L("ProdMaterial.NotFound", input.MaterialId));

            entity.Status = input.Status;
            entity.Remark = input.Remark;

            var result = await ProdMaterialRepository.UpdateAsync(entity);
            
            return result > 0;
        }

        /// <summary>
        /// 导入生产物料数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "生产物料信息")
        {
            var importDtos = await TaktExcelHelper.ImportAsync<TaktProdMaterialImportDto>(fileStream, sheetName);
            if (importDtos == null || !importDtos.Any())
                return (0, 0);

            var success = 0;
            var fail = 0;

            foreach (var dto in importDtos)
            {
                try
                {
                    var material = dto.Adapt<TaktProdMaterial>();
                    await ProdMaterialRepository.CreateAsync(material);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("ProdMaterial.ImportFailed", dto.MaterialCode), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出生产物料数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktProdMaterialQueryDto query, string sheetName = "生产物料信息")
        {
            var predicate = QueryExpression(query);

            var materials = await ProdMaterialRepository.AsQueryable()
                .Where(predicate)
                .OrderBy(x => x.CreateTime)
                .ToListAsync();

            var exportDtos = materials.Adapt<List<TaktProdMaterialExportDto>>();
            
            return await TaktExcelHelper.ExportAsync(exportDtos, sheetName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "生产物料信息")
        {
            var template = new List<TaktProdMaterialTemplateDto>();
            return await TaktExcelHelper.ExportAsync(template, sheetName);
        }

        /// <summary>
        /// 获取生产物料选项列表
        /// </summary>
        /// <param name="plantCode">工厂编码</param>
        /// <param name="keyword">关键词</param>
        /// <returns>返回选项列表</returns>
        public async Task<List<TaktProdMaterialDto>> GetOptionsAsync(string plantCode, string? keyword = null)
        {
            Expression<Func<TaktProdMaterial, bool>> predicate = x => x.PlantCode == plantCode && x.Status == 1;
            
            if (!string.IsNullOrEmpty(keyword))
            {
                predicate = x => x.PlantCode == plantCode && x.Status == 1 && 
                                (x.MaterialCode.Contains(keyword) || x.MaterialDescription.Contains(keyword));
            }

            var entities = await ProdMaterialRepository.GetListAsync(predicate);
            return entities.Adapt<List<TaktProdMaterialDto>>();
        }



        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>返回查询表达式</returns>
        private Expression<Func<TaktProdMaterial, bool>> QueryExpression(TaktProdMaterialQueryDto query)
        {
            var exp = Expressionable.Create<TaktProdMaterial>();

            if (query != null)
            {
                if (!string.IsNullOrEmpty(query.PlantCode))
                    exp = exp.And(x => x.PlantCode.Contains(query.PlantCode));

                if (!string.IsNullOrEmpty(query.MaterialCode))
                    exp = exp.And(x => x.MaterialCode.Contains(query.MaterialCode));

                if (!string.IsNullOrEmpty(query.MaterialDescription))
                    exp = exp.And(x => x.MaterialDescription.Contains(query.MaterialDescription));

                if (!string.IsNullOrEmpty(query.MaterialType))
                    exp = exp.And(x => x.MaterialType.Contains(query.MaterialType));

                if (!string.IsNullOrEmpty(query.MaterialGroup))
                    exp = exp.And(x => x.MaterialGroup.Contains(query.MaterialGroup));

                // Status: -1 表示显示全部
                if (query.Status.HasValue && query.Status.Value != -1)
                    exp = exp.And(x => x.Status == query.Status.Value);
            }

            return exp.ToExpression();
        }
    }
}



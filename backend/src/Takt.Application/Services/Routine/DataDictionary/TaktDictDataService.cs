#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDictDataService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-20
// 版本号 : V0.0.1
// 描述    : 字典数据服务实现类
//===================================================================

using Takt.Application.Dtos.Routine.DataDictionary;
using Takt.Domain.Entities.Routine.DataDictionary;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.DataDictionary
{
    /// <summary>
    /// 字典数据服务实现类
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-22
    /// </remarks>
    public class TaktDictDataService : TaktBaseService, ITaktDictDataService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 获取字典数据仓储
        /// </summary>
        private ITaktRepository<TaktDictData> DictDataRepository => _repositoryFactory.GetBusinessRepository<TaktDictData>();

        /// <summary>
        /// 获取字典类型仓储
        /// </summary>
        private ITaktRepository<TaktDictType> DictTypeRepository => _repositoryFactory.GetBusinessRepository<TaktDictType>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktDictDataService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }


        /// <summary>
        /// 获取字典数据分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>字典数据分页列表</returns>
        public async Task<TaktPagedResult<TaktDictDataDto>> GetListAsync(TaktDictDataQueryDto query)
        {
            query ??= new TaktDictDataQueryDto();

            var result = await DictDataRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktDictDataDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktDictDataDto>>()
            };
        }

        /// <summary>
        /// 获取字典数据详情
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>字典数据详情</returns>
        public async Task<TaktDictDataDto> GetByIdAsync(long dictDataId)
        {
            var dictData = await DictDataRepository.GetByIdAsync(dictDataId);
            return dictData == null ? throw new TaktException(L("Core.DictData.NotFound", dictDataId)) : dictData.Adapt<TaktDictDataDto>();
        }

        /// <summary>
        /// 创建字典数据
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>字典数据ID</returns>
        public async Task<long> CreateAsync(TaktDictDataCreateDto input)
        {
            // 验证字典类型是否存在
            var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == input.DictType)
                ?? throw new TaktException(L("Core.DictType.NotFound", input.DictType));

            // 验证同一字典类型下字典标签是否唯一
            await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                new Dictionary<string, string> 
                { 
                    { "DictType", input.DictType },
                    { "DictLabel", input.DictLabel }
                });
            
            // 验证同一字典类型下字典值是否唯一
            await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                new Dictionary<string, string> 
                { 
                    { "DictType", input.DictType },
                    { "DictValue", input.DictValue }
                });

            var dictData = input.Adapt<TaktDictData>();
            return await DictDataRepository.CreateAsync(dictData) > 0 ? dictData.Id : throw new TaktException(L("Core.DictData.CreateFailed"));
        }

        /// <summary>
        /// 更新字典数据
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktDictDataUpdateDto input)
        {
            var dictData = await DictDataRepository.GetByIdAsync(input.DictDataId)
                ?? throw new TaktException(L("Core.DictData.NotFound", input.DictDataId));

            // 验证字典类型是否存在
            var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == input.DictType)
                ?? throw new TaktException(L("Core.DictType.NotFound", input.DictType));

            // 验证同一字典类型下字典标签是否唯一（排除当前记录）
            await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                new Dictionary<string, string> 
                { 
                    { "DictType", input.DictType },
                    { "DictLabel", input.DictLabel }
                }, 
                input.DictDataId);
            
            // 验证同一字典类型下字典值是否唯一（排除当前记录）
            await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                new Dictionary<string, string> 
                { 
                    { "DictType", input.DictType },
                    { "DictValue", input.DictValue }
                }, 
                input.DictDataId);

            input.Adapt(dictData);
            return await DictDataRepository.UpdateAsync(dictData) > 0;
        }

        /// <summary>
        /// 删除字典数据
        /// </summary>
        /// <param name="dictDataId">字典数据ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long dictDataId)
        {
            var dictData = await DictDataRepository.GetByIdAsync(dictDataId)
                ?? throw new TaktException(L("Core.DictData.NotFound", dictDataId));

            if (await CheckBuiltinData(dictDataId))
                throw new TaktException(L("Core.DictData.CannotDeleteBuiltin"));

            return await DictDataRepository.DeleteAsync(dictDataId) > 0;
        }

        /// <summary>
        /// 批量删除字典数据
        /// </summary>
        /// <param name="dictDataIds">字典数据ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] dictDataIds)
        {
            if (dictDataIds == null || dictDataIds.Length == 0) return false;
            return await DictDataRepository.DeleteRangeAsync(dictDataIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 导入字典数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            var dictDatas = await TaktExcelHelper.ImportAsync<TaktDictDataDto>(fileStream, sheetName);
            if (dictDatas == null || !dictDatas.Any()) return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var dictData in dictDatas)
            {
                try
                {
                    // 根据DictType获取字典类型
                    var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == dictData.DictType);
                    if (dictType == null)
                    {
                        fail++;
                        continue;
                    }

                    // 验证同一字典类型下字典标签是否唯一
                    await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                        new Dictionary<string, string> 
                        { 
                            { "DictType", dictData.DictType },
                            { "DictLabel", dictData.DictLabel }
                        });
                    
                    // 验证同一字典类型下字典值是否唯一
                    await TaktValidateUtils.ValidateFieldsExistsAsync(DictDataRepository, 
                        new Dictionary<string, string> 
                        { 
                            { "DictType", dictData.DictType },
                            { "DictValue", dictData.DictValue }
                        });

                    // 创建新记录
                    var newDictData = dictData.Adapt<TaktDictData>();
                    newDictData.DictType = dictType.DictType;
                    newDictData.CreateBy = _currentUser.UserName;
                    newDictData.CreateTime = DateTime.Now;

                    await DictDataRepository.CreateAsync(newDictData);
                    success++;
                }
                catch (Exception ex)
                {
                    _logger.Error(L("Core.DictData.ImportFailed", ex.Message), ex);
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出字典数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件内容</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktDictDataQueryDto query, string sheetName)
        {
            try
            {
                var list = await DictDataRepository.GetListAsync(QueryExpression(query));
                return await TaktExcelHelper.ExportAsync(list.Adapt<List<TaktDictDataDto>>(), sheetName, L("Core.DictData.ExportTitle"));
            }
            catch (Exception ex)
            {
                _logger.Error(L("Core.DictData.ExportFailed", ex.Message), ex);
                throw new TaktException(L("Core.DictData.ExportFailed"));
            }
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktDictDataDto>(sheetName);
        }


        /// <summary>
        /// 获取字典数据选项列表
        /// </summary>
        /// <param name="dictType">字典类型编码</param>
        /// <returns>字典数据选项列表</returns>
        public async Task<List<TaktDictDataDto>> GetOptionsAsync(string dictType)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType);
            return result.OrderBy(x => x.OrderNum).Adapt<List<TaktDictDataDto>>();
        }

        /// <summary>
        /// 检查字典数据是否存在
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <param name="dictValue">字典值</param>
        /// <param name="excludeId">排除的字典数据ID</param>
        /// <returns>是否存在</returns>
        public async Task<bool> CheckDictDataExists(string dictType, string dictValue, long? excludeId = null)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType);
            return result.Any(x => x.DictValue == dictValue && x.Id != (excludeId ?? 0));
        }

        /// <summary>
        /// 检查是否为内置字典数据
        /// </summary>
        /// <param name="id">字典数据ID</param>
        /// <returns>是否为内置数据</returns>
        public async Task<bool> CheckBuiltinData(long id)
        {
            var dictData = await DictDataRepository.GetByIdAsync(id);
            if (dictData == null) return false;

            var dictType = await DictTypeRepository.GetFirstAsync(x => x.DictType == dictData.DictType);
            return dictType != null && dictType.IsBuiltin == 1; // 1表示内置
        }

        /// <summary>
        /// 根据字典类型获取字典数据列表
        /// </summary>
        /// <param name="dictType">字典类型</param>
        /// <returns>字典数据列表</returns>
        public async Task<List<TaktDictDataDto>> GetListByDictTypeAsync(string dictType)
        {
            var result = await DictDataRepository.GetListAsync(x => x.DictType == dictType);
            return result.OrderBy(x => x.OrderNum).Adapt<List<TaktDictDataDto>>();
        }

        /// <summary>
        /// 获取字典数据列表
        /// </summary>
        /// <returns>字典数据列表</returns>
        public async Task<List<TaktDictDataDto>> GetListAsync()
        {
            var result = await DictDataRepository.GetListAsync();
            return result.OrderBy(x => x.OrderNum).Adapt<List<TaktDictDataDto>>();
        }

        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktDictData, bool>> QueryExpression(TaktDictDataQueryDto query)
        {
            return Expressionable.Create<TaktDictData>()
                .AndIF(!string.IsNullOrEmpty(query.DictType), x => x.DictType.Contains(query.DictType!))
                .AndIF(!string.IsNullOrEmpty(query.DictLabel), x => x.DictLabel.Contains(query.DictLabel!))
                .AndIF(!string.IsNullOrEmpty(query.DictValue), x => x.DictValue.Contains(query.DictValue!))
                .ToExpression();
        }
    }
}




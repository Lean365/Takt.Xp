#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktGenTableService.cs
// 创建者 : Claude
// 创建时间: 2024-03-21
// 版本号 : V0.0.1
// 描述   : 代码生成表服务实现
//===================================================================

using Takt.Shared.Utils;
using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Generator;

/// <summary>
/// 代码生成表服务实现
/// </summary>
public class TaktGenTableService : TaktBaseService, ITaktGenTableService
{
    /// <summary>
    /// 工厂仓储
    /// </summary>
    protected readonly ITaktRepositoryFactory _repositoryFactory;

    private ITaktRepository<TaktGenTable> TableRepository => _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
    private ITaktRepository<TaktGenColumn> ColumnRepository => _repositoryFactory.GetGeneratorRepository<TaktGenColumn>();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repositoryFactory">表仓储</param>
    /// <param name="logger">日志服务</param>
    /// <param name="httpContextAccessor">HTTP上下文访问器</param>
    /// <param name="currentUser">当前用户服务</param>
    /// <param name="localization">本地化服务</param>
    public TaktGenTableService(
        ITaktRepositoryFactory repositoryFactory,
        ITaktLogger logger,
        IHttpContextAccessor httpContextAccessor,
        ITaktCurrentUser currentUser,
        ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
    {
        _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
    }

    #region 基础操作

    /// <summary>
    /// 获取分页表列表
    /// </summary>
    /// <param name="input">查询参数</param>
    /// <returns>分页结果</returns>
    public async Task<TaktPagedResult<TaktGenTableDto>> GetListAsync(TaktGenTableQueryDto input)
    {
        var result = await TableRepository.GetPagedListAsync(
            QueryExpression(input),
            input.PageIndex,
            input.PageSize,
            x => x.CreateTime,
            OrderByType.Desc);

        return result.Adapt<TaktPagedResult<TaktGenTableDto>>();
    }

    /// <summary>
    /// 根据ID获取表信息
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>表信息</returns>
    public async Task<TaktGenTableDto?> GetByIdAsync(long id)
    {
        var table = await TableRepository.GetByIdAsync(id);
        if (table == null)
            return null;

        var dto = table.Adapt<TaktGenTableDto>();

        // 获取列信息
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == id);
        dto.Columns = columns.Adapt<List<TaktGenColumnDto>>();

        return dto;
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableId">表ID</param>
    /// <returns>字段列表</returns>
    public async Task<List<TaktGenColumnDto>> GetColumnListAsync(long tableId)
    {
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == tableId);
        return columns.Adapt<List<TaktGenColumnDto>>();
    }

    /// <summary>
    /// 创建表信息
    /// </summary>
    /// <param name="input">创建参数</param>
    /// <returns>创建结果</returns>
    public async Task<TaktGenTableDto> CreateAsync(TaktGenTableCreateDto input)
    {
        // 检查表名是否已存在
        await TaktValidateUtils.ValidateFieldExistsAsync(TableRepository, nameof(TaktGenTable.TableName), input.TableName);

        // 创建表信息
        var table = input.Adapt<TaktGenTable>();
        await TableRepository.CreateAsync(table);

        // 创建列信息
        if (input.Columns != null && input.Columns.Any())
        {
            var columns = input.Columns.Adapt<List<TaktGenColumn>>();
            foreach (var column in columns)
            {
                column.TableId = table.Id;
                await ColumnRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(table.Id) ?? throw new TaktException(L("Generator.Table.CreateFailed"));
    }

    /// <summary>
    /// 更新表信息
    /// </summary>
    /// <param name="input">更新参数</param>
    /// <returns>更新后的表信息</returns>
    public async Task<TaktGenTableDto> UpdateAsync(TaktGenTableUpdateDto input)
    {
        var table = await TableRepository.GetByIdAsync(input.GenTableId);
        if (table == null)
            throw new TaktException(L("Generator.Table.NotFound", input.GenTableId));

        // 检查表名是否已存在
        await TaktValidateUtils.ValidateFieldExistsAsync(TableRepository, nameof(TaktGenTable.TableName), input.TableName, input.GenTableId);

        // 更新表信息
        input.Adapt(table);
        await TableRepository.UpdateAsync(table);

        // 更新列信息
        if (input.Columns != null && input.Columns.Any())
        {
            // 删除旧的列信息
            var oldColumns = await ColumnRepository.GetListAsync(x => x.TableId == input.GenTableId);
            foreach (var column in oldColumns)
            {
                await ColumnRepository.DeleteAsync(column.Id);
            }

            // 创建新的列信息
            var columns = input.Columns.Adapt<List<TaktGenColumn>>();
            foreach (var column in columns)
            {
                column.TableId = input.GenTableId;
                await ColumnRepository.CreateAsync(column);
            }
        }

        return await GetByIdAsync(input.GenTableId) ?? throw new TaktException(L("Generator.Table.UpdateFailed"));
    }

    /// <summary>
    /// 删除表
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否删除成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        // 删除表信息
        await TableRepository.DeleteAsync(id);

        // 删除关联的列信息
        var columns = await ColumnRepository.GetListAsync(x => x.TableId == id);
        foreach (var column in columns)
        {
            await ColumnRepository.DeleteAsync(column.Id);
        }

        return true;
    }

    #endregion

    #region 表操作

    /// <summary>
    /// 导入表
    /// </summary>
    /// <param name="input">导入参数</param>
    /// <returns>导入的表列表</returns>
    public async Task<List<TaktGenTableDto>> ImportTablesAsync(TaktGenTableImportDto input)
    {
        var result = new List<TaktGenTableDto>();
        var failTables = new List<string>();

        try
        {
            _logger.Info($"开始导入表：{input.TableName}");

            // 获取表信息
            var tableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
            var tableInfo = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetTableInfoList(false).FirstOrDefault(x => x.Name == input.TableName));
            if (tableInfo == null)
            {
                _logger.Error($"表不存在：{input.TableName}");
                throw new TaktException(L("Generator.Table.NotFound", input.TableName));
            }

            _logger.Info($"获取到表信息：{input.TableName}");

            // 检查表是否已存在
            var existingTable = await TableRepository.GetFirstAsync(x => x.TableName == input.TableName);
            if (existingTable != null)
            {
                _logger.Info($"表已存在，准备更新：{input.TableName}");

                // 更新表信息
                existingTable.DatabaseName = input.DatabaseName;
                existingTable.TableName = input.TableName;
                existingTable.TableComment = input.TableComment;
                existingTable.IsDataTable = input.IsDataTable;
                existingTable.EntityClassName = input.EntityClassName;
                existingTable.EntityNamespace = input.EntityNamespace;
                existingTable.BaseNamespace = "Lean.Takt";
                existingTable.DtoType = input.DtoType;
                existingTable.DtoClassName = TaktNamingHelper.GetDtoClassName(input.TableName);
                existingTable.TplType = input.TplType;
                existingTable.TplCategory = input.TplCategory;
                existingTable.SubTableName = input.SubTableName;
                existingTable.SubTableFkName = input.SubTableFkName;
                existingTable.TreeCode = input.TreeCode;
                tableInfo.Description = input.TableComment;
                existingTable.TreeName = input.TreeName;
                existingTable.TreeParentCode = input.TreeParentCode;
                existingTable.GenModuleName = input.GenModuleName;
                existingTable.GenBusinessName = input.GenBusinessName;
                existingTable.GenFunctionName = input.GenFunctionName;
                existingTable.GenAuthor = input.GenAuthor;
                existingTable.GenMethod = input.GenMethod;
                existingTable.GenPath = input.GenPath;
                existingTable.ParentMenuId = input.ParentMenuId;
                existingTable.SortType = input.SortType;
                existingTable.SortField = input.SortField;
                existingTable.PermsPrefix = input.PermsPrefix;
                existingTable.IsGenMenu = input.IsGenMenu;
                existingTable.IsGenTranslation = input.IsGenTranslation;
                existingTable.FrontTpl = input.FrontTpl;
                existingTable.BtnStyle = input.BtnStyle;
                existingTable.FrontStyle = input.FrontStyle;
                existingTable.IsGenCode = input.IsGenCode;


                // 获取并创建新的列信息
                var genTableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
                var columns = await Task.Run(() => genTableRepository.SqlSugarClient.DbMaintenance.GetColumnInfosByTableName(input.TableName, false));
                if (columns == null || !columns.Any())
                {
                    _logger.Error($"表没有列信息：{input.TableName}");
                    throw new TaktException(L("Generator.Table.NoColumns", input.TableName));
                }

                _logger.Info($"获取到列信息，数量：{columns.Count}");

                // 处理列信息
                var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == existingTable.Id);
                var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

                // 更新或创建列
                foreach (var column in columns)
                {
                    var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                    if (existingColumn != null)
                    {
                        _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                        // 更新列信息
                        existingColumn.ColumnName = column.DbColumnName;
                        existingColumn.ColumnComment = column.ColumnDescription;
                        existingColumn.PropertyName = column.DataType;
                        existingColumn.DataType = TaktStringUtils.GetCsharpType(column.DataType);
                        existingColumn.ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName);
                        existingColumn.Length = column.Length;
                        existingColumn.DecimalDigits = column.DecimalDigits;
                        existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                        existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                        existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                        existingColumn.IsCreate = 1;
                        existingColumn.IsUpdate = 1;
                        existingColumn.IsList = 1;
                        existingColumn.IsQuery = 0;
                        existingColumn.QueryType = "EQ";
                        existingColumn.IsSort = 0;
                        existingColumn.IsExport = 1;
                        existingColumn.DisplayType = "input";
                        existingColumn.DictType = "";
                        existingColumn.OrderNum = column.CreateTableFieldSort;

                        await ColumnRepository.UpdateAsync(existingColumn);
                    }
                    else
                    {
                        _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                        // 创建新列
                        var newColumn = new TaktGenColumn
                        {
                            TableId = existingTable.Id,
                            ColumnName = column.DbColumnName,
                            ColumnComment = column.ColumnDescription,
                            PropertyName = column.DataType,
                            DataType = TaktStringUtils.GetCsharpType(column.DataType),
                            ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName),
                            Length = column.Length,
                            DecimalDigits = column.DecimalDigits,
                            IsIncrement = column.IsIdentity ? 1 : 0,
                            IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                            IsRequired = !column.IsNullable ? 1 : 0,
                            IsCreate = 1,
                            IsUpdate = 1,
                            IsList = 1,
                            IsQuery = 0,
                            QueryType = "EQ",
                            IsSort = 0,
                            IsExport = 1,
                            DisplayType = "input",
                            DictType = "",
                            OrderNum = column.CreateTableFieldSort
                        };

                        await ColumnRepository.CreateAsync(newColumn);
                    }
                }

                // 删除不再存在的列
                var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
                foreach (var column in columnsToDelete)
                {
                    _logger.Info($"删除不再存在的列：{column.ColumnName}");
                    await ColumnRepository.DeleteAsync(column.Id);
                }

                await TableRepository.UpdateAsync(existingTable);
                _logger.Info($"表信息更新成功，ID：{existingTable.Id}");

                var updatedTable = await GetByIdAsync(existingTable.Id);
                if (updatedTable == null)
                {
                    _logger.Error($"更新后无法获取表信息：{input.TableName}");
                    throw new TaktException(L("Generator.Table.ImportFailed"));
                }

                result.Add(updatedTable);
                _logger.Info($"表更新成功：{input.TableName}");
            }
            else
            {
                // 创建新表
                var table = new TaktGenTable
                {
                    DatabaseName = input.DatabaseName,
                    TableName = input.TableName,
                    TableComment = input.TableComment,
                    IsDataTable = input.IsDataTable,
                    Remark = input.TableComment,
                    EntityClassName = input.EntityClassName,
                    BaseNamespace = "Lean.Takt"
                };

                // 设置实体相关命名空间
                table.EntityNamespace = TaktNamingHelper.GetEntityNamespace(table.BaseNamespace, input.TableName);
                table.DtoType = input.DtoType;
                table.DtoNamespace = TaktNamingHelper.GetDtoNamespace(table.BaseNamespace, input.TableName);
                table.DtoClassName = TaktNamingHelper.GetDtoClassName(input.TableName);

                // 设置服务相关命名空间
                table.ServiceNamespace = TaktNamingHelper.GetServiceNamespace(table.BaseNamespace, input.TableName);
                table.IServiceClassName = TaktNamingHelper.GetIServiceClassName(input.TableName);
                table.ServiceClassName = TaktNamingHelper.GetServiceClassName(input.TableName);

                // 设置仓储相关命名空间
                table.IRepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, input.TableName);
                table.IRepositoryClassName = TaktNamingHelper.GetIRepositoryClassName(input.TableName);
                table.RepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, input.TableName);
                table.RepositoryClassName = TaktNamingHelper.GetRepositoryClassName(input.TableName);

                // 设置控制器相关命名空间
                table.ControllerNamespace = TaktNamingHelper.GetControllerNamespace(table.BaseNamespace, input.TableName);
                table.ControllerClassName = TaktNamingHelper.GetControllerClassName(input.TableName);

                // 设置其他属性
                table.TplCategory = input.TplCategory;
                table.TplType = input.TplType;
                table.SubTableName = input.SubTableName;
                table.SubTableFkName = input.SubTableFkName;
                table.TreeCode = input.TreeCode;
                table.TreeName = input.TreeName;
                table.TreeParentCode = input.TreeParentCode;
                table.GenModuleName = input.GenModuleName;
                table.GenBusinessName = input.GenBusinessName;
                table.GenFunctionName = input.GenFunctionName;
                table.GenAuthor = input.GenAuthor;
                table.GenMethod = input.GenMethod;
                table.GenPath = input.GenPath;
                table.ParentMenuId = input.ParentMenuId;
                table.SortType = input.SortType;
                table.SortField = input.SortField;
                table.PermsPrefix = input.PermsPrefix;
                table.IsGenMenu = input.IsGenMenu;
                table.IsGenTranslation = input.IsGenTranslation;
                table.FrontTpl = input.FrontTpl;
                table.BtnStyle = input.BtnStyle;
                table.FrontStyle = input.FrontStyle;
                table.IsGenCode = input.IsGenCode;


                _logger.Info($"开始获取列信息：{input.TableName}");

                // 获取并创建列信息
                var columns = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetColumnInfosByTableName(input.TableName, false));
                if (columns == null || !columns.Any())
                {
                    _logger.Error($"表没有列信息：{input.TableName}");
                    throw new TaktException(L("Generator.Table.NoColumns", input.TableName));
                }

                _logger.Info($"获取到列信息，数量：{columns.Count}");

                // 处理列信息
                var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
                var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

                // 更新或创建列
                foreach (var column in columns)
                {
                    var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                    if (existingColumn != null)
                    {
                        _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                        // 更新列信息
                        existingColumn.ColumnName = column.DbColumnName;
                        existingColumn.ColumnComment = column.ColumnDescription;
                        existingColumn.PropertyName = column.DataType;
                        existingColumn.DataType = TaktStringUtils.GetCsharpType(column.DataType);
                        existingColumn.ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName);
                        existingColumn.Length = column.Length;
                        existingColumn.DecimalDigits = column.DecimalDigits;
                        existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                        existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                        existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                        existingColumn.IsCreate = 1;
                        existingColumn.IsUpdate = 1;
                        existingColumn.IsList = 1;
                        existingColumn.IsQuery = 0;
                        existingColumn.QueryType = "EQ";
                        existingColumn.IsSort = 0;
                        existingColumn.IsExport = 1;
                        existingColumn.DisplayType = "input";
                        existingColumn.DictType = "";
                        existingColumn.OrderNum = column.CreateTableFieldSort;

                        await ColumnRepository.UpdateAsync(existingColumn);
                    }
                    else
                    {
                        _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                        // 创建新列
                        var newColumn = new TaktGenColumn
                        {
                            TableId = table.Id,
                            ColumnName = column.DbColumnName,
                            ColumnComment = column.ColumnDescription,
                            PropertyName = column.DataType,
                            DataType = TaktStringUtils.GetCsharpType(column.DataType),
                            ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName),
                            Length = column.Length,
                            DecimalDigits = column.DecimalDigits,
                            IsIncrement = column.IsIdentity ? 1 : 0,
                            IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                            IsRequired = !column.IsNullable ? 1 : 0,
                            IsCreate = 1,
                            IsUpdate = 1,
                            IsList = 1,
                            IsQuery = 0,
                            QueryType = "EQ",
                            IsSort = 0,
                            IsExport = 1,
                            DisplayType = "input",
                            DictType = "",
                            OrderNum = column.CreateTableFieldSort
                        };

                        await ColumnRepository.CreateAsync(newColumn);
                    }
                }

                // 删除不再存在的列
                var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
                foreach (var column in columnsToDelete)
                {
                    _logger.Info($"删除不再存在的列：{column.ColumnName}");
                    await ColumnRepository.DeleteAsync(column.Id);
                }

                _logger.Info($"开始保存表信息：{input.TableName}");

                // 保存表信息
                await TableRepository.CreateAsync(table);

                // 获取新创建表的ID
                var savedTable = await TableRepository.GetFirstAsync(x => x.TableName == input.TableName);
                if (savedTable == null)
                {
                    _logger.Error($"保存后无法获取表信息：{input.TableName}");
                    throw new TaktException(L("Generator.Table.ImportFailed"));
                }
                table = savedTable;

                result.Add(await GetByIdAsync(table.Id) ?? throw new TaktException(L("Generator.Table.ImportFailed")));
                _logger.Info($"表导入成功：{input.TableName}");
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"导入表失败，表名：{input.TableName}", ex);
            throw;
        }

        return result;
    }

    /// <summary>
    /// 导入表及其所有字段信息
    /// </summary>
    /// <param name="databaseName">数据库名</param>
    /// <param name="tableName">表名</param>
    /// <returns>是否成功</returns>
    public async Task<bool> ImportTableAndColumnsAsync(string databaseName, string tableName)
    {
        try
        {
            _logger.Info($"开始导入表和列信息：{tableName}");

            // 检查表是否存在
            var tableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
            var tableExists = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.IsAnyTable(tableName, false));
            if (!tableExists)
            {
                _logger.Error($"表不存在：{tableName}");
                throw new TaktException(L("Generator.Table.NotFound", tableName));
            }

            // 获取表信息
            var tableInfo = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetTableInfoList(false).FirstOrDefault(x => x.Name == tableName));
            if (tableInfo == null)
            {
                _logger.Error($"无法获取表信息：{tableName}");
                throw new TaktException(L("Generator.Table.InfoNotFound", tableName));
            }

            _logger.Info($"获取到表信息：{tableName}");

            // 检查表是否已存在
            var existingTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
            TaktGenTable table;

            if (existingTable != null)
            {
                _logger.Info($"表已存在，准备更新：{tableName}");
                table = existingTable;

                // 更新表信息
                table.DatabaseName = databaseName;
                table.TableName = tableName;
                table.TableComment = tableInfo.Description;
                table.IsDataTable = 1;
                table.Remark = tableName + "(" + tableInfo.Description + ")";
                table.EntityClassName = TaktNamingHelper.GetEntityClassName(tableName);
                table.BaseNamespace = "Lean.Takt";
                table.EntityNamespace = TaktNamingHelper.GetEntityNamespace(table.BaseNamespace, tableName);
                table.DtoType = "Dto,QueryDto,CreateDto,UpdateDto,DeleteDto,TplDto,ImportDto,ExportDto";
                table.DtoNamespace = TaktNamingHelper.GetDtoNamespace(table.BaseNamespace, tableName);
                table.DtoClassName = TaktNamingHelper.GetDtoClassName(tableName);
                table.ServiceNamespace = TaktNamingHelper.GetServiceNamespace(table.BaseNamespace, tableName);
                table.IServiceClassName = TaktNamingHelper.GetIServiceClassName(tableName);
                table.ServiceClassName = TaktNamingHelper.GetServiceClassName(tableName);
                table.IRepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.IRepositoryClassName = TaktNamingHelper.GetIRepositoryClassName(tableName);
                table.RepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.RepositoryClassName = TaktNamingHelper.GetRepositoryClassName(tableName);
                table.ControllerNamespace = TaktNamingHelper.GetControllerNamespace(table.BaseNamespace, tableName);
                table.ControllerClassName = TaktNamingHelper.GetControllerClassName(tableName);
                table.TplType = "0";
                table.TplCategory = "crud";
                table.GenModuleName = TaktNamingHelper.ExtractTransType(tableName);
                table.GenBusinessName = TaktNamingHelper.GetBusinessName(tableName);
                table.GenFunctionName = tableInfo.Description;
                table.GenAuthor = "Lean365";
                table.GenMethod = 0;
                table.GenPath = "/";
                table.ParentMenuId = 0;
                table.SortType = "asc";
                table.SortField = "CreateTime";
                table.PermsPrefix = TaktNamingHelper.GetPermsPrefix(tableName);
                table.IsGenMenu = 1;
                table.IsGenTranslation = 0;
                table.FrontTpl = 2;
                table.BtnStyle = 1;
                table.FrontStyle = 24;
                table.IsGenCode = 0;


                await TableRepository.UpdateAsync(table);

                // 获取更新后的表信息
                var updatedTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
                if (updatedTable == null)
                {
                    _logger.Error($"更新后无法获取表信息：{tableName}");
                    throw new TaktException(L("Generator.Table.ImportFailed"));
                }
                table = updatedTable;
            }
            else
            {
                _logger.Info($"表不存在，准备创建：{tableName}");

                // 创建新表
                table = new TaktGenTable
                {
                    DatabaseName = databaseName,
                    TableName = tableName,
                    TableComment = tableInfo.Description,
                    IsDataTable = 1,
                    Remark = tableName + "(" + tableInfo.Description + ")",
                    EntityClassName = TaktNamingHelper.GetEntityClassName(tableName),
                    BaseNamespace = "Lean.Takt"
                };

                // 设置实体相关命名空间
                table.EntityNamespace = TaktNamingHelper.GetEntityNamespace(table.BaseNamespace, tableName);
                table.DtoNamespace = TaktNamingHelper.GetDtoNamespace(table.BaseNamespace, tableName);
                table.DtoClassName = TaktNamingHelper.GetDtoClassName(tableName);
                table.DtoType = "Dto,QueryDto,CreateDto,UpdateDto,DeleteDto,TplDto,ImportDto,ExportDto";

                // 设置服务相关命名空间
                table.ServiceNamespace = TaktNamingHelper.GetServiceNamespace(table.BaseNamespace, tableName);
                table.IServiceClassName = TaktNamingHelper.GetIServiceClassName(tableName);
                table.ServiceClassName = TaktNamingHelper.GetServiceClassName(tableName);

                // 设置仓储相关命名空间
                table.IRepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.IRepositoryClassName = TaktNamingHelper.GetIRepositoryClassName(tableName);
                table.RepositoryNamespace = TaktNamingHelper.GetRepositoryNamespace(table.BaseNamespace, tableName);
                table.RepositoryClassName = TaktNamingHelper.GetRepositoryClassName(tableName);

                // 设置控制器相关命名空间
                table.ControllerNamespace = TaktNamingHelper.GetControllerNamespace(table.BaseNamespace, tableName);
                table.ControllerClassName = TaktNamingHelper.GetControllerClassName(tableName);

                // 设置其他属性
                table.TplType = "0";
                table.TplCategory = "crud";
                table.GenModuleName = TaktNamingHelper.ExtractTransType(tableName);
                table.GenBusinessName = TaktNamingHelper.GetBusinessName(tableName);
                table.GenFunctionName = tableInfo.Description;
                table.GenAuthor = "Lean365";
                table.GenMethod = 0;
                table.GenPath = "/";
                table.ParentMenuId = 0;
                table.SortType = "asc";
                table.SortField = "CreateTime";
                table.PermsPrefix = TaktNamingHelper.GetPermsPrefix(tableName);
                table.IsGenMenu = 1;
                table.IsGenTranslation = 0;
                table.FrontTpl = 2;
                table.BtnStyle = 1;
                table.FrontStyle = 24;
                table.IsGenCode = 0;


                await TableRepository.CreateAsync(table);

                // 获取新创建表的ID
                var savedTable = await TableRepository.GetFirstAsync(x => x.TableName == tableName);
                if (savedTable == null)
                {
                    _logger.Error($"保存后无法获取表信息：{tableName}");
                    throw new TaktException(L("Generator.Table.ImportFailed"));
                }
                table = savedTable;
            }

            // 获取列信息
            var columns = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetColumnInfosByTableName(tableName, false));
            if (columns == null || !columns.Any())
            {
                _logger.Error($"表没有列信息：{tableName}");
                throw new TaktException(L("Generator.Table.NoColumns", tableName));
            }

            _logger.Info($"获取到列信息，数量：{columns.Count}");

            // 处理列信息
            var existingColumns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
            var currentColumnNames = columns.Select(x => x.DbColumnName).ToList();

            // 更新或创建列
            foreach (var column in columns)
            {
                var existingColumn = existingColumns.FirstOrDefault(x => x.ColumnName == column.DbColumnName);

                if (existingColumn != null)
                {
                    _logger.Info($"列已存在，准备更新：{column.DbColumnName}");
                    // 更新列信息
                    existingColumn.ColumnName = column.DbColumnName;
                    existingColumn.ColumnComment = column.ColumnDescription;
                    existingColumn.PropertyName = column.DataType;
                    existingColumn.DataType = TaktStringUtils.GetCsharpType(column.DataType);
                    existingColumn.ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName);
                    existingColumn.Length = column.Length;
                    existingColumn.DecimalDigits = column.DecimalDigits;
                    existingColumn.IsIncrement = column.IsIdentity ? 1 : 0;
                    existingColumn.IsPrimaryKey = column.IsPrimarykey ? 1 : 0;
                    existingColumn.IsRequired = !column.IsNullable ? 1 : 0;
                    existingColumn.IsCreate = 1;
                    existingColumn.IsUpdate = 1;
                    existingColumn.IsList = 1;
                    existingColumn.IsQuery = 0;
                    existingColumn.QueryType = "EQ";
                    existingColumn.IsSort = 0;
                    existingColumn.IsExport = 1;
                    existingColumn.DisplayType = "input";
                    existingColumn.DictType = "";
                    existingColumn.OrderNum = column.CreateTableFieldSort;

                    await ColumnRepository.UpdateAsync(existingColumn);
                }
                else
                {
                    _logger.Info($"列不存在，准备创建：{column.DbColumnName}");
                    // 创建新列
                    var newColumn = new TaktGenColumn
                    {
                        TableId = table.Id,
                        ColumnName = column.DbColumnName,
                        ColumnComment = column.ColumnDescription,
                        PropertyName = column.DataType,
                        DataType = TaktStringUtils.GetCsharpType(column.DataType),
                        ColumnDataType = TaktStringUtils.ToCamelCase(column.DbColumnName),
                        Length = column.Length,
                        DecimalDigits = column.DecimalDigits,
                        IsIncrement = column.IsIdentity ? 1 : 0,
                        IsPrimaryKey = column.IsPrimarykey ? 1 : 0,
                        IsRequired = !column.IsNullable ? 1 : 0,
                        IsCreate = 1,
                        IsUpdate = 1,
                        IsList = 1,
                        IsQuery = 0,
                        QueryType = "EQ",
                        IsSort = 0,
                        IsExport = 1,
                        DisplayType = "input",
                        DictType = "",
                        OrderNum = column.CreateTableFieldSort
                    };

                    await ColumnRepository.CreateAsync(newColumn);
                }
            }

            // 删除不再存在的列
            var columnsToDelete = existingColumns.Where(x => !currentColumnNames.Contains(x.ColumnName)).ToList();
            foreach (var column in columnsToDelete)
            {
                _logger.Info($"删除不再存在的列：{column.ColumnName}");
                await ColumnRepository.DeleteAsync(column.Id);
            }

            _logger.Info($"表和列信息导入完成：{tableName}");
            return true;
        }
        catch (Exception ex)
        {
            _logger.Error($"导入表和列信息失败：{tableName}", ex);
            throw;
        }
    }

    /// <summary>
    /// 导出表
    /// </summary>
    /// <returns>导出的表列表</returns>
    public async Task<List<TaktGenTableExportDto>> ExportTablesAsync()
    {
        var tables = await TableRepository.GetListAsync();
        var result = new List<TaktGenTableExportDto>();

        foreach (var table in tables)
        {
            var dto = table.Adapt<TaktGenTableExportDto>();
            var columns = await ColumnRepository.GetListAsync(x => x.TableId == table.Id);
            dto.Columns = columns.Adapt<List<TaktGenColumnDto>>();
            result.Add(dto);
        }

        return result;
    }

    /// <summary>
    /// 获取数据库列表
    /// </summary>
    /// <returns>数据库列表</returns>
    public Task<List<string>> GetDatabaseListByDbAsync()
    {
        var tableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
        return Task.FromResult(tableRepository.SqlSugarClient.DbMaintenance.GetDataBaseList());
    }

    /// <summary>
    /// 获取表列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <returns>表列表</returns>
    public async Task<List<TaktGenTableInfoDto>> GetTableListByDbAsync(string databaseName)
    {
        var tableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
        var tables = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetTableInfoList(false));
        return tables.Select(x => new TaktGenTableInfoDto
        {
            TableName = x.Name,
            TableComment = x.Description
        }).ToList();
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="databaseName">数据库名称</param>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    public async Task<List<TaktGenTableColumnInfoDto>> GetTableColumnListByDbAsync(string databaseName, string tableName)
    {
        var tableRepository = _repositoryFactory.GetGeneratorRepository<TaktGenTable>();
        var columns = await Task.Run(() => tableRepository.SqlSugarClient.DbMaintenance.GetColumnInfosByTableName(tableName, false));
        return columns.Select(x => new TaktGenTableColumnInfoDto
        {
            DbColumnName = x.DbColumnName,
            ColumnDescription = x.ColumnDescription,
            DataType = x.DataType,
            IsPrimarykey = x.IsPrimarykey,
            IsIdentity = x.IsIdentity,
            IsNullable = x.IsNullable
        }).ToList();
    }

    /// <summary>
    /// 同步表结构
    /// </summary>
    /// <param name="id">表ID</param>
    /// <returns>是否同步成功</returns>
    public async Task<bool> SyncTableAsync(long id)
    {
        var table = await TableRepository.GetByIdAsync(id);
        if (table == null)
            throw new TaktException(L("Generator.Table.NotFound", id));

        return await ImportTableAndColumnsAsync(table.DatabaseName, table.TableName);
    }

    #endregion

    /// <summary>
    /// 构建查询条件
    /// </summary>
    private Expression<Func<TaktGenTable, bool>> QueryExpression(TaktGenTableQueryDto input)
    {
        return Expressionable.Create<TaktGenTable>()
            .AndIF(!string.IsNullOrEmpty(input.TableName), x => x.TableName.Contains(input.TableName))
            .AndIF(!string.IsNullOrEmpty(input.TableComment), x => x.TableComment.Contains(input.TableComment))
            .ToExpression();
    }
}




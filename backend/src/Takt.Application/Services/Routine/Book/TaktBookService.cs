//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktBookService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 图书服务实现
//===================================================================

using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.IO;
using Takt.Shared.Models;
using Takt.Domain.Entities.Routine.Book;
using Takt.Application.Dtos.Routine.Book;
using Takt.Shared.Exceptions;
using Takt.Shared.Helpers;
using Takt.Domain.Repositories;
using SqlSugar;
using Mapster;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Takt.Shared.Utils;
using Takt.Domain.Utils;
using Takt.Shared.Constants;
using Takt.Domain.IServices.SignalR;
using Takt.Shared.Enums;

namespace Takt.Application.Services.Routine.Book
{
    /// <summary>
    /// 图书服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktBookService : TaktBaseService, ITaktBookService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;
        private readonly ITaktSignalRClient _signalRClient;

        /// <summary>
        /// 获取图书仓储
        /// </summary>
        private ITaktRepository<TaktBook> BookRepository => _repositoryFactory.GetBusinessRepository<TaktBook>();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        /// <param name="signalRClient">SignalR客户端</param>
        public TaktBookService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization,
            ITaktSignalRClient signalRClient) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _signalRClient = signalRClient ?? throw new ArgumentNullException(nameof(signalRClient));
        }

        /// <summary>
        /// 获取图书分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>图书分页列表</returns>
        public async Task<TaktPagedResult<TaktBookDto>> GetListAsync(TaktBookQueryDto query)
        {
            var exp = QueryExpression(query);

            var result = await BookRepository.GetPagedListAsync(
                exp,
                query.PageIndex,
                query.PageSize,
                x => x.Id,
                OrderByType.Asc);

            return new TaktPagedResult<TaktBookDto>
            {
                Rows = result.Rows.Adapt<List<TaktBookDto>>(),
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize
            };
        }

        /// <summary>
        /// 获取图书详情
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <returns>图书详情</returns>
        public async Task<TaktBookDto> GetByIdAsync(long bookId)
        {
            var book = await BookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new TaktException(L("Routine.Book.NotFound", bookId));

            return book.Adapt<TaktBookDto>();
        }

        /// <summary>
        /// 获取图书选项列表
        /// </summary>
        /// <returns>图书选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var books = await BookRepository.AsQueryable()
                .Where(b => b.Status == 0 && b.IsDeleted == 0)  // 只获取正常状态且未删除的图书
                .OrderBy(b => b.BookName)
                .Select(b => new TaktSelectOption
                {
                    DictLabel = b.BookName,
                    DictValue = b.Id,
                    ExtLabel = b.BookCode,
                    ExtValue = b.Author,
                    OrderNum = 0,
                    Status = b.Status
                })
                .ToListAsync();
            return books;
        }

        /// <summary>
        /// 创建图书
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>图书ID</returns>
        public async Task<long> CreateAsync(TaktBookCreateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldsExistsAsync(BookRepository, 
                new Dictionary<string, string> 
                { 
                    { "BookCode", input.BookCode },
                    { "BookName", input.BookName }
                });

            // 如果ISBN不为空，验证ISBN是否已存在
            if (!string.IsNullOrEmpty(input.Isbn))
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(BookRepository, "Isbn", input.Isbn);
            }

            var book = input.Adapt<TaktBook>();
            book.CreateTime = DateTime.Now;
            book.CreateBy = _currentUser.UserName;
            book.AvailableQuantity = input.TotalQuantity; // 初始可借数量等于总数量

            var result = await BookRepository.CreateAsync(book);
            if (result <= 0)
                throw new TaktException(L("Common.AddFailed"));

            return book.Id;
        }

        /// <summary>
        /// 更新图书
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktBookUpdateDto input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            var book = await BookRepository.GetByIdAsync(input.BookId);
            if (book == null)
                throw new TaktException(L("Routine.Book.NotFound", input.BookId));

            // 验证字段是否已被其他记录使用
            await TaktValidateUtils.ValidateFieldsExistsAsync(BookRepository, 
                new Dictionary<string, string> 
                { 
                    { "BookCode", input.BookCode },
                    { "BookName", input.BookName }
                }, 
                input.BookId);

            // 如果ISBN不为空且与当前记录不同，验证ISBN是否已被其他记录使用
            if (!string.IsNullOrEmpty(input.Isbn) && book.Isbn != input.Isbn)
            {
                await TaktValidateUtils.ValidateFieldExistsAsync(BookRepository, "Isbn", input.Isbn, input.BookId);
            }

            input.Adapt(book);
            book.UpdateTime = DateTime.Now;
            book.UpdateBy = _currentUser.UserName;

            var result = await BookRepository.UpdateAsync(book);
            if (result <= 0)
                throw new TaktException(L("Common.UpdateFailed"));

            return result > 0;
        }

        /// <summary>
        /// 删除图书
        /// </summary>
        /// <param name="bookId">图书ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long bookId)
        {
            var book = await BookRepository.GetByIdAsync(bookId);
            if (book == null)
                throw new TaktException(L("Routine.Book.NotFound", bookId));

            var result = await BookRepository.DeleteAsync(bookId);
            if (result <= 0)
                throw new TaktException(L("Common.DeleteFailed"));

            return result > 0;
        }

        /// <summary>
        /// 批量删除图书
        /// </summary>
        /// <param name="bookIds">图书ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] bookIds)
        {
            if (bookIds == null || bookIds.Length == 0)
                throw new TaktException(L("Common.SelectToDelete"));

            var books = await BookRepository.GetListAsync(x => bookIds.Contains(x.Id));
            if (!books.Any())
                throw new TaktException(L("Routine.Book.NotFound"));

            var result = await BookRepository.DeleteAsync(books);
            if (result <= 0)
                throw new TaktException(L("Common.BatchDeleteFailed"));

            return result > 0;
        }

        /// <summary>
        /// 导入图书数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public async Task<(int success, int fail)> ImportAsync(Stream fileStream, string? sheetName)
        {
            var actualSheetName = sheetName ?? "Book";
            var books = await TaktExcelHelper.ImportAsync<TaktBookImportDto>(fileStream, actualSheetName);
            if (books == null || !books.Any())
                return (0, 0);

            var (success, fail) = (0, 0);
            foreach (var book in books)
            {
                try
                {
                    if (string.IsNullOrEmpty(book.BookName) || string.IsNullOrEmpty(book.BookCode))
                    {
                        fail++;
                        continue;
                    }

                    // 验证字段是否已存在
                    await TaktValidateUtils.ValidateFieldsExistsAsync(BookRepository, 
                        new Dictionary<string, string> 
                        { 
                            { "BookCode", book.BookCode },
                            { "BookName", book.BookName }
                        });

                    // 如果ISBN不为空，验证ISBN是否已存在
                    if (!string.IsNullOrEmpty(book.Isbn))
                    {
                        await TaktValidateUtils.ValidateFieldExistsAsync(BookRepository, "Isbn", book.Isbn);
                    }

                    var bookEntity = book.Adapt<TaktBook>();
                    bookEntity.CreateTime = DateTime.Now;
                    bookEntity.CreateBy = _currentUser.UserName;
                    bookEntity.AvailableQuantity = book.TotalQuantity; // 初始可借数量等于总数量

                    var result = await BookRepository.CreateAsync(bookEntity);
                    if (result > 0)
                        success++;
                    else
                        fail++;
                }
                catch (Exception ex)
                {
                    _logger.Warn($"导入图书失败: {ex.Message}");
                    fail++;
                }
            }

            return (success, fail);
        }

        /// <summary>
        /// 导出图书数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>包含文件名和内容的元组</returns>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktBookQueryDto query, string? sheetName, string? fileName)
        {
            var list = await BookRepository.GetListAsync(QueryExpression(query));
            var exportList = list.Adapt<List<TaktBookDto>>();
            
            // 使用传入的参数，如果为空则使用默认值
            var actualSheetName = sheetName ?? "Book";
            var actualFileName = fileName ?? "BookExport";
            
            return await TaktExcelHelper.ExportAsync(exportList, actualSheetName, actualFileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <param name="fileName">文件名</param>
        /// <returns>Excel文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string? sheetName, string? fileName)
        {
            var actualSheetName = sheetName ?? "Book";
            var actualFileName = fileName ?? "BookTemplate";
            return await TaktExcelHelper.GenerateTemplateAsync<TaktBookImportDto>(actualSheetName, actualFileName);
        }


        /// <summary>
        /// 构建查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktBook, bool>> QueryExpression(TaktBookQueryDto query)
        {
            return x => x.IsDeleted == 0
                && (string.IsNullOrEmpty(query.CompanyCode) || x.CompanyCode.Contains(query.CompanyCode))
                && (string.IsNullOrEmpty(query.BookCode) || x.BookCode.Contains(query.BookCode))
                && (string.IsNullOrEmpty(query.BookName) || x.BookName.Contains(query.BookName))
                && (string.IsNullOrEmpty(query.Author) || (x.Author != null && x.Author.Contains(query.Author)))
                && (string.IsNullOrEmpty(query.Isbn) || (x.Isbn != null && x.Isbn.Contains(query.Isbn)))
                && (query.CategoryId == null || x.CategoryId == query.CategoryId)
                && (string.IsNullOrEmpty(query.Publisher) || (x.Publisher != null && x.Publisher.Contains(query.Publisher)))
                && (query.Status == null || x.Status == query.Status);
        }
    }
}






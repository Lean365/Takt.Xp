#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktNewsService : TaktBaseService, ITaktNewsService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger">日志服务</param>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻仓储
        /// </summary>
        private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNews, bool>> NewsQueryExpression(TaktNewsQueryDto query)
        {
            return Expressionable.Create<TaktNews>()
                .AndIF(!string.IsNullOrEmpty(query.NewsTitle), x => x.NewsTitle.Contains(query.NewsTitle!))
                .AndIF(!string.IsNullOrEmpty(query.NewsCategory), x => x.NewsCategory == query.NewsCategory)
                .AndIF(!string.IsNullOrEmpty(query.NewsAuthor), x => x.NewsAuthor.Contains(query.NewsAuthor!))
                .AndIF(!string.IsNullOrEmpty(query.NewsTags), x => x.NewsTags != null && x.NewsTags.Contains(query.NewsTags!))
                .AndIF(!string.IsNullOrEmpty(query.Keyword), x =>
                    x.NewsTitle.Contains(query.Keyword!) ||
                    x.NewsContent != null && x.NewsContent.Contains(query.Keyword!) ||
                    x.NewsTags != null && x.NewsTags.Contains(query.Keyword!) ||
                    x.NewsAuthor.Contains(query.Keyword!) ||
                    x.NewsSubtitle != null && x.NewsSubtitle.Contains(query.Keyword!) ||
                    x.NewsSummary != null && x.NewsSummary.Contains(query.Keyword!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(query.NewsAuditStatus.HasValue, x => x.NewsAuditStatus == query.NewsAuditStatus!.Value)
                .AndIF(query.NewsIsTop.HasValue, x => x.NewsIsTop == query.NewsIsTop!.Value)
                .AndIF(query.NewsIsRecommend.HasValue, x => x.NewsIsRecommend == query.NewsIsRecommend!.Value)
                .AndIF(query.NewsIsHot.HasValue, x => x.NewsIsHot == query.NewsIsHot!.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsDto>> GetListAsync(TaktNewsQueryDto query)
        {
            query ??= new TaktNewsQueryDto();

            _logger.Info($"查询新闻列表，参数：NewsTitle={query.NewsTitle}, NewsCategory={query.NewsCategory}, Keyword={query.Keyword}");

            var result = await NewsRepository.GetPagedListAsync(
                NewsQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Desc);

            _logger.Info($"查询新闻列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsDto>>()
            };
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>新闻详情</returns>
        public async Task<TaktNewsDto> GetByIdAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            return news.Adapt<TaktNewsDto>();
        }

        /// <summary>
        /// 创建新闻
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>新闻ID</returns>
        public async Task<long> CreateAsync(TaktNewsCreateDto input)
        {
            var news = input.Adapt<TaktNews>();
            news.CreateBy = _currentUser.UserName ?? "system";
            news.CreateTime = DateTime.Now;

            // 如果状态为已发布，设置发布时间
            if (news.Status == 1)
            {
                news.NewsPublishTime = DateTime.Now;
            }

            var result = await NewsRepository.CreateAsync(news);
            return result > 0 ? news.Id : throw new TaktException("创建新闻失败");
        }

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsUpdateDto input)
        {
            var existingNews = await NewsRepository.GetByIdAsync(input.NewsId);
            if (existingNews == null)
            {
                throw TaktException.NotFound($"新闻ID {input.NewsId} 不存在");
            }

            var news = input.Adapt<TaktNews>();
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;
            news.NewsEditorBy = _currentUser.UserName ?? "system";
            news.NewsEditTime = DateTime.Now;

            // 如果状态从草稿变为已发布，设置发布时间
            if (existingNews.Status == 0 && news.Status == 1)
            {
                news.NewsPublishTime = DateTime.Now;
            }

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.DeleteBy = _currentUser.UserName ?? "system";
            news.DeleteTime = DateTime.Now;
            news.IsDeleted = 1;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="newsIds">新闻ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] newsIds)
        {
            var newsList = await NewsRepository.GetListAsync(x => newsIds.Contains(x.Id));
            if (!newsList.Any())
            {
                return false;
            }

            var currentUser = _currentUser.UserName ?? "system";
            var currentTime = DateTime.Now;

            foreach (var news in newsList)
            {
                news.DeleteBy = currentUser;
                news.DeleteTime = currentTime;
                news.IsDeleted = 1;
            }

            var result = await NewsRepository.DeleteRangeAsync(newsIds.Cast<object>().ToList());
            return result > 0;
        }

        /// <summary>
        /// 导入新闻数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        public Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName)
        {
            // TODO: 实现Excel导入逻辑
            throw new NotImplementedException("Excel导入功能待实现");
        }

        /// <summary>
        /// 导出新闻数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public Task<(string fileName, byte[] content)> ExportAsync(TaktNewsQueryDto query, string sheetName)
        {
            // TODO: 实现Excel导出逻辑
            throw new NotImplementedException("Excel导出功能待实现");
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        public Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            // TODO: 实现模板导出逻辑
            throw new NotImplementedException("模板导出功能待实现");
        }

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktNewsStatusDto input)
        {
            var news = await NewsRepository.GetByIdAsync(input.NewsId);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {input.NewsId} 不存在");
            }

            news.Status = input.Status;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            // 如果状态为已发布，设置发布时间
            if (news.Status == 1 && !news.NewsPublishTime.HasValue)
            {
                news.NewsPublishTime = DateTime.Now;
            }

            // 如果状态为已下线，设置下线时间
            if (news.Status == 2)
            {
                news.NewsOfflineTime = DateTime.Now;
            }

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 审核新闻
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AuditAsync(TaktNewsAuditDto input)
        {
            var news = await NewsRepository.GetByIdAsync(input.NewsId);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {input.NewsId} 不存在");
            }

            news.NewsAuditStatus = input.NewsAuditStatus;
            news.NewsAuditComments = input.NewsAuditComments;
            news.NewsAuditedBy = _currentUser.UserName ?? "system";
            news.NewsAuditedTime = DateTime.Now;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> PublishAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.Status = 1;
            news.NewsPublishTime = DateTime.Now;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 下线新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> OfflineAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.Status = 2;
            news.NewsOfflineTime = DateTime.Now;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 置顶新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetTopAsync(long id, bool isTop)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.NewsIsTop = isTop ? 1 : 0;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 推荐新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetRecommendAsync(long id, bool isRecommend)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.NewsIsRecommend = isRecommend ? 1 : 0;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 热门新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetHotAsync(long id, bool isHot)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null)
            {
                throw TaktException.NotFound($"新闻ID {id} 不存在");
            }

            news.NewsIsHot = isHot ? 1 : 0;
            news.UpdateBy = _currentUser.UserName ?? "system";
            news.UpdateTime = DateTime.Now;

            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 增加阅读次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseReadCountAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null) return false;

            news.NewsReadCount++;
            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 增加点赞次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseLikeCountAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null) return false;

            news.NewsLikeCount++;
            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 增加评论次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseCommentCountAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null) return false;

            news.NewsCommentCount++;
            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 增加分享次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseShareCountAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null) return false;

            news.NewsShareCount++;
            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 增加推荐次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseRecommendCountAsync(long id)
        {
            var news = await NewsRepository.GetByIdAsync(id);
            if (news == null) return false;

            news.NewsRecommendCount++;
            var result = await NewsRepository.UpdateAsync(news);
            return result > 0;
        }

        /// <summary>
        /// 获取热门新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门新闻列表</returns>
        public async Task<List<TaktNewsDto>> GetHotNewsAsync(int count = 10)
        {
            var newsList = await NewsRepository.GetListAsync(
                x => x.NewsIsHot == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return newsList.Take(count).OrderByDescending(x => x.NewsReadCount).Adapt<List<TaktNewsDto>>();
        }

        /// <summary>
        /// 获取推荐新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐新闻列表</returns>
        public async Task<List<TaktNewsDto>> GetRecommendNewsAsync(int count = 10)
        {
            var newsList = await NewsRepository.GetListAsync(
                x => x.NewsIsRecommend == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return newsList.Take(count).OrderByDescending(x => x.NewsRecommendCount).Adapt<List<TaktNewsDto>>();
        }

        /// <summary>
        /// 获取置顶新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>置顶新闻列表</returns>
        public async Task<List<TaktNewsDto>> GetTopNewsAsync(int count = 5)
        {
            var newsList = await NewsRepository.GetListAsync(
                x => x.NewsIsTop == 1 && x.Status == 1 && x.IsDeleted == 0
            );

            return newsList.Take(count).OrderByDescending(x => x.OrderNum).Adapt<List<TaktNewsDto>>();
        }

        /// <summary>
        /// 根据分类获取新闻列表
        /// </summary>
        /// <param name="category">新闻分类</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        public async Task<List<TaktNewsDto>> GetNewsByCategoryAsync(string category, int count = 20)
        {
            var newsList = await NewsRepository.GetListAsync(
                x => x.NewsCategory == category && x.Status == 1 && x.IsDeleted == 0
            );

            return newsList.Take(count).OrderByDescending(x => x.CreateTime).Adapt<List<TaktNewsDto>>();
        }

        /// <summary>
        /// 搜索新闻
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        public async Task<List<TaktNewsDto>> SearchNewsAsync(string keyword, int count = 20)
        {
            var newsList = await NewsRepository.GetListAsync(
                x => (x.NewsTitle.Contains(keyword) ||
                      x.NewsContent != null && x.NewsContent.Contains(keyword) ||
                      x.NewsTags != null && x.NewsTags.Contains(keyword)) &&
                     x.Status == 1 && x.IsDeleted == 0
            );

            return newsList.Take(count).OrderByDescending(x => x.CreateTime).Adapt<List<TaktNewsDto>>();
        }
    }
}


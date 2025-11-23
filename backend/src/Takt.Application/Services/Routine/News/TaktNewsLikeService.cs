#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsLikeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻点赞服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻点赞服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class TaktNewsLikeService : TaktBaseService, ITaktNewsLikeService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsLikeService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻点赞仓储
        /// </summary>
        private ITaktRepository<TaktNewsLike> LikeRepository => _repositoryFactory.GetBusinessRepository<TaktNewsLike>();

        /// <summary>
        /// 获取新闻仓储
        /// </summary>
        private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNewsLike, bool>> LikeQueryExpression(TaktNewsLikeQueryDto query)
        {
            return Expressionable.Create<TaktNewsLike>()
                .AndIF(query.NewsId.HasValue, x => x.NewsId == query.NewsId!.Value)
                .AndIF(query.UserId.HasValue, x => x.UserId == query.UserId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName!.Contains(query.UserName!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻点赞分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻点赞分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsLikeDto>> GetListAsync(TaktNewsLikeQueryDto? query)
        {
            query ??= new TaktNewsLikeQueryDto();

            _logger.Info($"查询新闻点赞列表，参数：NewsId={query.NewsId}, UserId={query.UserId}, UserName={query.UserName}");

            var result = await LikeRepository.GetPagedListAsync(
                LikeQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻点赞列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsLikeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsLikeDto>>()
            };
        }

        /// <summary>
        /// 获取新闻点赞详情
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>新闻点赞详情</returns>
        public async Task<TaktNewsLikeDto> GetByIdAsync(long id)
        {
            var like = await LikeRepository.GetByIdAsync(id);
            return like == null ? throw new TaktException(L("Routine.NewsLike.NotFound", id)) : like.Adapt<TaktNewsLikeDto>();
        }

        /// <summary>
        /// 创建新闻点赞
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>点赞ID</returns>
        public async Task<long> CreateAsync(TaktNewsLikeCreateDto input)
        {
            // 验证新闻是否存在
            var news = await NewsRepository.GetByIdAsync(input.NewsId);
            if (news == null)
            {
                throw new TaktException(L("Routine.News.NotFound", input.NewsId));
            }

            // 检查用户是否已经点赞过该新闻
            var existingLike = await LikeRepository.GetFirstAsync(x => x.NewsId == input.NewsId && x.UserId == input.UserId);
            if (existingLike != null)
            {
                throw new TaktException(L("Routine.NewsLike.AlreadyLiked"));
            }

            var like = input.Adapt<TaktNewsLike>();
            like.CreateBy = _currentUser.UserName ?? "system";
            like.CreateTime = DateTime.Now;

            // 获取客户端IP和用户代理
            like.IpAddress = GetClientIpAddress();
            like.UserAgent = GetUserAgent();

            var result = await LikeRepository.CreateAsync(like);
            if (result > 0)
            {
                // 增加新闻点赞次数
                await IncreaseNewsLikeCountAsync(input.NewsId);

                _logger.Info($"创建新闻点赞成功，点赞ID：{like.Id}，新闻ID：{input.NewsId}，用户ID：{input.UserId}");
                return like.Id;
            }

            throw new TaktException(L("Routine.NewsLike.CreateFailed"));
        }

        /// <summary>
        /// 更新新闻点赞
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsLikeUpdateDto input)
        {
            var like = await LikeRepository.GetByIdAsync(input.LikeId)
                ?? throw new TaktException(L("Routine.NewsLike.NotFound", input.LikeId));

            input.Adapt(like);
            like.UpdateBy = _currentUser.UserName ?? "system";
            like.UpdateTime = DateTime.Now;

            var result = await LikeRepository.UpdateAsync(like);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻点赞
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var like = await LikeRepository.GetByIdAsync(id)
                ?? throw new TaktException(L("Routine.NewsLike.NotFound", id));

            var result = await LikeRepository.DeleteAsync(id);
            if (result > 0)
            {
                // 减少新闻点赞次数
                await DecreaseNewsLikeCountAsync(like.NewsId);

                _logger.Info($"删除新闻点赞成功，点赞ID：{id}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量删除新闻点赞
        /// </summary>
        /// <param name="likeIds">点赞ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] likeIds)
        {
            if (likeIds == null || likeIds.Length == 0) return false;

            var likes = await LikeRepository.GetListAsync(x => likeIds.Contains(x.Id));
            if (!likes.Any()) return false;

            var result = await LikeRepository.DeleteRangeAsync(likeIds.Cast<object>().ToList());
            if (result > 0)
            {
                // 批量减少新闻点赞次数
                var newsIds = likes.Select(x => x.NewsId).Distinct();
                foreach (var newsId in newsIds)
                {
                    await DecreaseNewsLikeCountAsync(newsId, likes.Count(x => x.NewsId == newsId));
                }

                _logger.Info($"批量删除新闻点赞成功，删除数量：{result}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 用户点赞新闻
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        public async Task<bool> LikeNewsAsync(long newsId, long userId, string userName, string? userAvatar = null)
        {
            try
            {
                // 检查是否已经点赞
                var existingLike = await LikeRepository.GetFirstAsync(x => x.NewsId == newsId && x.UserId == userId);
                if (existingLike != null)
                {
                    _logger.Warn($"用户已点赞过该新闻，新闻ID：{newsId}，用户ID：{userId}");
                    return false;
                }

                var like = new TaktNewsLikeCreateDto
                {
                    NewsId = newsId,
                    UserId = userId,
                    UserName = userName,
                    UserAvatar = userAvatar
                };

                await CreateAsync(like);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"用户点赞新闻失败，新闻ID：{newsId}，用户ID：{userId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 用户取消点赞
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UnlikeNewsAsync(long newsId, long userId)
        {
            try
            {
                var like = await LikeRepository.GetFirstAsync(x => x.NewsId == newsId && x.UserId == userId);
                if (like == null)
                {
                    _logger.Warn($"用户未点赞过该新闻，新闻ID：{newsId}，用户ID：{userId}");
                    return false;
                }

                var result = await DeleteAsync(like.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"用户取消点赞失败，新闻ID：{newsId}，用户ID：{userId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否已点赞
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已点赞</returns>
        public async Task<bool> IsUserLikedAsync(long newsId, long userId)
        {
            var like = await LikeRepository.GetFirstAsync(x => x.NewsId == newsId && x.UserId == userId);
            return like != null;
        }

        /// <summary>
        /// 获取新闻的点赞用户列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞用户分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsLikeDto>> GetLikedUsersByNewsIdAsync(long newsId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsLikeQueryDto
            {
                NewsId = newsId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 获取用户点赞的新闻列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞新闻分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsLikeDto>> GetLikedNewsByUserIdAsync(long userId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsLikeQueryDto
            {
                UserId = userId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns>IP地址</returns>
        private string GetClientIpAddress()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                if (httpContext == null) return string.Empty;

                var ip = httpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ??
                        httpContext.Request.Headers["X-Real-IP"].FirstOrDefault() ??
                        httpContext.Connection.RemoteIpAddress?.ToString();

                return ip ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 获取用户代理
        /// </summary>
        /// <returns>用户代理</returns>
        private string GetUserAgent()
        {
            try
            {
                var httpContext = _httpContextAccessor.HttpContext;
                return httpContext?.Request.Headers["User-Agent"].FirstOrDefault() ?? string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 增加新闻点赞次数
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>是否成功</returns>
        private async Task<bool> IncreaseNewsLikeCountAsync(long newsId)
        {
            try
            {
                var news = await NewsRepository.GetByIdAsync(newsId);
                if (news != null)
                {
                    news.NewsLikeCount++;
                    await NewsRepository.UpdateAsync(news);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"增加新闻点赞次数失败，新闻ID：{newsId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 减少新闻点赞次数
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="count">减少数量</param>
        /// <returns>是否成功</returns>
        private async Task<bool> DecreaseNewsLikeCountAsync(long newsId, int count = 1)
        {
            try
            {
                var news = await NewsRepository.GetByIdAsync(newsId);
                if (news != null)
                {
                    news.NewsLikeCount = Math.Max(0, news.NewsLikeCount - count);
                    await NewsRepository.UpdateAsync(news);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"减少新闻点赞次数失败，新闻ID：{newsId}，错误：{ex.Message}", ex);
                return false;
            }
        }
    }
}




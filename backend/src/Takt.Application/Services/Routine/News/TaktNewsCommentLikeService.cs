#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsCommentLikeService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论点赞服务实现
//===================================================================

using System.Linq.Expressions;
using Takt.Shared.Utils;
using Takt.Domain.IServices.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using Takt.Application.Dtos.Routine.Document;
using Takt.Domain.Entities.Routine.News;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻评论点赞服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class TaktNewsCommentLikeService : TaktBaseService, ITaktNewsCommentLikeService
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
        public TaktNewsCommentLikeService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻评论点赞仓储
        /// </summary>
        private ITaktRepository<TaktNewsCommentLike> CommentLikeRepository => _repositoryFactory.GetBusinessRepository<TaktNewsCommentLike>();

        /// <summary>
        /// 获取新闻评论仓储
        /// </summary>
        private ITaktRepository<TaktNewsComment> CommentRepository => _repositoryFactory.GetBusinessRepository<TaktNewsComment>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNewsCommentLike, bool>> CommentLikeQueryExpression(TaktNewsCommentLikeQueryDto query)
        {
            return Expressionable.Create<TaktNewsCommentLike>()
                .AndIF(query.NewsId.HasValue, x => x.NewsId == query.NewsId!.Value)
                .AndIF(query.CommentId.HasValue, x => x.CommentId == query.CommentId!.Value)
                .AndIF(query.UserId.HasValue, x => x.UserId == query.UserId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName!.Contains(query.UserName!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻评论点赞分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻评论点赞分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentLikeDto>> GetListAsync(TaktNewsCommentLikeQueryDto? query)
        {
            query ??= new TaktNewsCommentLikeQueryDto();

            _logger.Info($"查询新闻评论点赞列表，参数：NewsId={query.NewsId}, CommentId={query.CommentId}, UserId={query.UserId}");

            var result = await CommentLikeRepository.GetPagedListAsync(
                CommentLikeQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻评论点赞列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsCommentLikeDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsCommentLikeDto>>()
            };
        }

        /// <summary>
        /// 获取新闻评论点赞详情
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>新闻评论点赞详情</returns>
        public async Task<TaktNewsCommentLikeDto> GetByIdAsync(long id)
        {
            var like = await CommentLikeRepository.GetByIdAsync(id);
            return like == null ? throw new TaktException(L("Routine.NewsCommentLike.NotFound", id)) : like.Adapt<TaktNewsCommentLikeDto>();
        }

        /// <summary>
        /// 创建新闻评论点赞
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>点赞ID</returns>
        public async Task<long> CreateAsync(TaktNewsCommentLikeCreateDto input)
        {
            // 验证评论是否存在
            var comment = await CommentRepository.GetByIdAsync(input.CommentId);
            if (comment == null)
            {
                throw new TaktException(L("Routine.NewsComment.NotFound", input.CommentId));
            }

            // 检查用户是否已经点赞过该评论
            var existingLike = await CommentLikeRepository.GetFirstAsync(x => x.CommentId == input.CommentId && x.UserId == input.UserId);
            if (existingLike != null)
            {
                throw new TaktException(L("Routine.NewsCommentLike.AlreadyLiked"));
            }

            var like = input.Adapt<TaktNewsCommentLike>();
            like.CreateBy = _currentUser.UserName ?? "system";
            like.CreateTime = DateTime.Now;

            // 获取客户端IP和用户代理
            like.IpAddress = GetClientIpAddress();
            like.UserAgent = GetUserAgent();

            var result = await CommentLikeRepository.CreateAsync(like);
            if (result > 0)
            {
                // 增加评论点赞次数
                await IncreaseCommentLikeCountAsync(input.CommentId);

                _logger.Info($"创建新闻评论点赞成功，点赞ID：{like.Id}，评论ID：{input.CommentId}，用户ID：{input.UserId}");
                return like.Id;
            }

            throw new TaktException(L("Routine.NewsCommentLike.CreateFailed"));
        }

        /// <summary>
        /// 更新新闻评论点赞
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsCommentLikeUpdateDto input)
        {
            var like = await CommentLikeRepository.GetByIdAsync(input.CommentLikeId)
                ?? throw new TaktException(L("Routine.NewsCommentLike.NotFound", input.CommentLikeId));

            input.Adapt(like);
            like.UpdateBy = _currentUser.UserName ?? "system";
            like.UpdateTime = DateTime.Now;

            var result = await CommentLikeRepository.UpdateAsync(like);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻评论点赞
        /// </summary>
        /// <param name="id">点赞ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var like = await CommentLikeRepository.GetByIdAsync(id)
                ?? throw new TaktException(L("Routine.NewsCommentLike.NotFound", id));

            var result = await CommentLikeRepository.DeleteAsync(id);
            if (result > 0)
            {
                // 减少评论点赞次数
                await DecreaseCommentLikeCountAsync(like.CommentId);

                _logger.Info($"删除新闻评论点赞成功，点赞ID：{id}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量删除新闻评论点赞
        /// </summary>
        /// <param name="likeIds">点赞ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] likeIds)
        {
            if (likeIds == null || likeIds.Length == 0) return false;

            var likes = await CommentLikeRepository.GetListAsync(x => likeIds.Contains(x.Id));
            if (!likes.Any()) return false;

            var result = await CommentLikeRepository.DeleteRangeAsync(likeIds.Cast<object>().ToList());
            if (result > 0)
            {
                // 批量减少评论点赞次数
                var commentIds = likes.Select(x => x.CommentId).Distinct();
                foreach (var commentId in commentIds)
                {
                    await DecreaseCommentLikeCountAsync(commentId, likes.Count(x => x.CommentId == commentId));
                }

                _logger.Info($"批量删除新闻评论点赞成功，删除数量：{result}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 用户点赞评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户姓名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        public async Task<bool> LikeCommentAsync(long commentId, long userId, string userName, string? userAvatar = null)
        {
            try
            {
                // 检查是否已经点赞
                var existingLike = await CommentLikeRepository.GetFirstAsync(x => x.CommentId == commentId && x.UserId == userId);
                if (existingLike != null)
                {
                    _logger.Warn($"用户已点赞过该评论，评论ID：{commentId}，用户ID：{userId}");
                    return false;
                }

                var like = new TaktNewsCommentLikeCreateDto
                {
                    CommentId = commentId,
                    UserId = userId,
                    UserName = userName,
                    UserAvatar = userAvatar
                };

                await CreateAsync(like);
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"用户点赞评论失败，评论ID：{commentId}，用户ID：{userId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 用户取消点赞评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UnlikeCommentAsync(long commentId, long userId)
        {
            try
            {
                var like = await CommentLikeRepository.GetFirstAsync(x => x.CommentId == commentId && x.UserId == userId);
                if (like == null)
                {
                    _logger.Warn($"用户未点赞过该评论，评论ID：{commentId}，用户ID：{userId}");
                    return false;
                }

                var result = await DeleteAsync(like.Id);
                return result;
            }
            catch (Exception ex)
            {
                _logger.Error($"用户取消点赞评论失败，评论ID：{commentId}，用户ID：{userId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 检查用户是否已点赞评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已点赞</returns>
        public async Task<bool> IsUserLikedCommentAsync(long commentId, long userId)
        {
            var like = await CommentLikeRepository.GetFirstAsync(x => x.CommentId == commentId && x.UserId == userId);
            return like != null;
        }

        /// <summary>
        /// 获取评论的点赞用户列表
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞用户分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentLikeDto>> GetLikedUsersByCommentIdAsync(long commentId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentLikeQueryDto
            {
                CommentId = commentId,
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 获取用户点赞的评论列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>点赞评论分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentLikeDto>> GetLikedCommentsByUserIdAsync(long userId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentLikeQueryDto
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
        /// 增加评论点赞次数
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns>是否成功</returns>
        private async Task<bool> IncreaseCommentLikeCountAsync(long commentId)
        {
            try
            {
                var comment = await CommentRepository.GetByIdAsync(commentId);
                if (comment != null)
                {
                    comment.LikeCount++;
                    await CommentRepository.UpdateAsync(comment);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"增加评论点赞次数失败，评论ID：{commentId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 减少评论点赞次数
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="count">减少数量</param>
        /// <returns>是否成功</returns>
        private async Task<bool> DecreaseCommentLikeCountAsync(long commentId, int count = 1)
        {
            try
            {
                var comment = await CommentRepository.GetByIdAsync(commentId);
                if (comment != null)
                {
                    comment.LikeCount = Math.Max(0, comment.LikeCount - count);
                    await CommentRepository.UpdateAsync(comment);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"减少评论点赞次数失败，评论ID：{commentId}，错误：{ex.Message}", ex);
                return false;
            }
        }
    }
} 





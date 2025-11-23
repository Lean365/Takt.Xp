#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsCommentService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻评论服务实现
//===================================================================

using Takt.Shared.Options;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻评论服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class TaktNewsCommentService : TaktBaseService, ITaktNewsCommentService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        /// <summary>
        /// 评论配置选项
        /// </summary>
        protected readonly TaktCommentOptions _commentOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="repositoryFactory">仓储工厂</param>
        /// <param name="commentOptions">评论配置选项</param>
        /// <param name="logger">日志服务</param>
        /// <param name="httpContextAccessor">HTTP上下文访问器</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktNewsCommentService(
            ITaktRepositoryFactory repositoryFactory,
            IOptions<TaktCommentOptions> commentOptions,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
            _commentOptions = commentOptions?.Value ?? throw new ArgumentNullException(nameof(commentOptions));
        }

        /// <summary>
        /// 获取新闻评论仓储
        /// </summary>
        private ITaktRepository<TaktNewsComment> CommentRepository => _repositoryFactory.GetBusinessRepository<TaktNewsComment>();

        /// <summary>
        /// 获取新闻仓储
        /// </summary>
        private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNewsComment, bool>> CommentQueryExpression(TaktNewsCommentQueryDto query)
        {
            return Expressionable.Create<TaktNewsComment>()
                .AndIF(query.NewsId.HasValue, x => x.NewsId == query.NewsId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.CommentContent), x => x.CommentContent!.Contains(query.CommentContent!))
                .AndIF(query.CommentUserId.HasValue, x => x.CommentUserId == query.CommentUserId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.CommentUserName), x => x.CommentUserName!.Contains(query.CommentUserName!))
                .AndIF(query.ParentCommentId.HasValue, x => x.ParentCommentId == query.ParentCommentId!.Value)
                .AndIF(query.ReplyUserId.HasValue, x => x.ReplyUserId == query.ReplyUserId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.ReplyUserName), x => x.ReplyUserName!.Contains(query.ReplyUserName!))
                .AndIF(query.CommentStatus.HasValue, x => x.CommentStatus == query.CommentStatus!.Value)
                .AndIF(!string.IsNullOrEmpty(query.AuditedBy), x => x.AuditedBy!.Contains(query.AuditedBy!))
                .AndIF(query.AuditType.HasValue, x => x.AuditType == query.AuditType!.Value)
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻评论分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻评论分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentDto>> GetListAsync(TaktNewsCommentQueryDto? query)
        {
            query ??= new TaktNewsCommentQueryDto();

            _logger.Info($"查询新闻评论列表，参数：NewsId={query.NewsId}, CommentContent={query.CommentContent}, CommentUserName={query.CommentUserName}, CommentStatus={query.CommentStatus}");

            var result = await CommentRepository.GetPagedListAsync(
                CommentQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻评论列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsCommentDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsCommentDto>>()
            };
        }

        /// <summary>
        /// 获取新闻评论详情
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>新闻评论详情</returns>
        public async Task<TaktNewsCommentDto> GetByIdAsync(long id)
        {
            var comment = await CommentRepository.GetByIdAsync(id);
            return comment == null ? throw new TaktException(L("Routine.NewsComment.NotFound", id)) : comment.Adapt<TaktNewsCommentDto>();
        }

        /// <summary>
        /// 创建新闻评论（包含敏感词过滤和字数限制）
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>评论ID</returns>
        public async Task<long> CreateAsync(TaktNewsCommentCreateDto input)
        {
            // 验证评论内容字数
            if (_commentOptions.EnableLengthLimit)
            {
                ValidateCommentLength(input.CommentContent);
            }

            // 验证评论频率限制
            await ValidateCommentRateLimitAsync(input.CommentUserId);

            // 验证IP限制
            if (_commentOptions.EnableIpLimit)
            {
                await ValidateIpCommentLimitAsync();
            }

            // 验证用户评论数量限制
            if (_commentOptions.EnableUserCommentLimit)
            {
                await ValidateUserCommentLimitAsync(input.CommentUserId);
            }

            // 验证重复评论
            if (_commentOptions.EnableDuplicateCheck)
            {
                await ValidateDuplicateCommentAsync(input.CommentContent, input.CommentUserId);
            }

            // 敏感词过滤
            string filteredContent = input.CommentContent;
            if (_commentOptions.EnableSensitiveWordFilter)
            {
                filteredContent = await FilterSensitiveWordsAsync(input.CommentContent);

                // 如果过滤后的内容与原文不同，记录日志
                if (filteredContent != input.CommentContent)
                {
                    _logger.Warn($"检测到敏感词，用户ID：{input.CommentUserId}，原文：{input.CommentContent}，过滤后：{filteredContent}");
                }
            }

            // 验证新闻是否存在
            var news = await NewsRepository.GetByIdAsync(input.NewsId);
            if (news == null)
            {
                throw new TaktException(L("Routine.News.NotFound", input.NewsId));
            }

            // 如果是回复评论，验证父评论是否存在
            if (input.ParentCommentId > 0)
            {
                var parentComment = await CommentRepository.GetByIdAsync(input.ParentCommentId);
                if (parentComment == null)
                {
                    throw new TaktException(L("Routine.NewsComment.ParentNotFound", input.ParentCommentId));
                }
            }

            var comment = input.Adapt<TaktNewsComment>();
            comment.CommentContent = filteredContent; // 使用过滤后的内容
            comment.CommentStatus = _commentOptions.DefaultAuditStatus; // 设置默认审核状态
            comment.AuditType = 1; // 设置审核类型为责任编辑审核
            comment.CreateBy = _currentUser.UserName ?? "system";
            comment.CreateTime = DateTime.Now;

            // 获取客户端IP和用户代理
            comment.IpAddress = GetClientIpAddress();
            comment.UserAgent = GetUserAgent();

            var result = await CommentRepository.CreateAsync(comment);
            if (result > 0)
            {
                // 增加新闻评论次数
                await IncreaseNewsCommentCountAsync(input.NewsId);

                // 如果是回复评论，增加父评论回复次数
                if (input.ParentCommentId > 0)
                {
                    await IncreaseParentCommentReplyCountAsync(input.ParentCommentId);
                }

                _logger.Info($"创建新闻评论成功，评论ID：{comment.Id}，新闻ID：{input.NewsId}，字数：{filteredContent.Length}");
                return comment.Id;
            }

            throw new TaktException(L("Routine.NewsComment.CreateFailed"));
        }

        /// <summary>
        /// 更新新闻评论
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsCommentUpdateDto input)
        {
            // 验证评论内容字数
            if (_commentOptions.EnableLengthLimit)
            {
                ValidateCommentLength(input.CommentContent);
            }

            var comment = await CommentRepository.GetByIdAsync(input.CommentId)
                ?? throw new TaktException(L("Routine.NewsComment.NotFound", input.CommentId));

            // 敏感词过滤
            string filteredContent = input.CommentContent;
            if (_commentOptions.EnableSensitiveWordFilter)
            {
                filteredContent = await FilterSensitiveWordsAsync(input.CommentContent);

                // 如果过滤后的内容与原文不同，记录日志
                if (filteredContent != input.CommentContent)
                {
                    _logger.Warn($"更新评论时检测到敏感词，评论ID：{input.CommentId}，原文：{input.CommentContent}，过滤后：{filteredContent}");
                }
            }

            input.CommentContent = filteredContent; // 使用过滤后的内容
            input.Adapt(comment);
            comment.UpdateBy = _currentUser.UserName ?? "system";
            comment.UpdateTime = DateTime.Now;

            var result = await CommentRepository.UpdateAsync(comment);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻评论
        /// </summary>
        /// <param name="id">评论ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var comment = await CommentRepository.GetByIdAsync(id)
                ?? throw new TaktException(L("Routine.NewsComment.NotFound", id));

            var result = await CommentRepository.DeleteAsync(id);
            if (result > 0)
            {
                // 减少新闻评论次数
                await DecreaseNewsCommentCountAsync(comment.NewsId);

                // 如果是回复评论，减少父评论回复次数
                if (comment.ParentCommentId > 0)
                {
                    await DecreaseParentCommentReplyCountAsync(comment.ParentCommentId);
                }

                _logger.Info($"删除新闻评论成功，评论ID：{id}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量删除新闻评论
        /// </summary>
        /// <param name="commentIds">评论ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] commentIds)
        {
            if (commentIds == null || commentIds.Length == 0) return false;

            var comments = await CommentRepository.GetListAsync(x => commentIds.Contains(x.Id));
            if (!comments.Any()) return false;

            var result = await CommentRepository.DeleteRangeAsync(commentIds.Cast<object>().ToList());
            if (result > 0)
            {
                // 批量减少新闻评论次数
                var newsIds = comments.Select(x => x.NewsId).Distinct();
                foreach (var newsId in newsIds)
                {
                    await DecreaseNewsCommentCountAsync(newsId, comments.Count(x => x.NewsId == newsId));
                }

                // 批量减少父评论回复次数
                var parentCommentIds = comments.Where(x => x.ParentCommentId > 0).Select(x => x.ParentCommentId).Distinct();
                foreach (var parentCommentId in parentCommentIds)
                {
                    await DecreaseParentCommentReplyCountAsync(parentCommentId, comments.Count(x => x.ParentCommentId == parentCommentId));
                }

                _logger.Info($"批量删除新闻评论成功，删除数量：{result}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 获取新闻的评论列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>评论分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentDto>> GetCommentsByNewsIdAsync(long newsId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentQueryDto
            {
                NewsId = newsId,
                CommentStatus = 1, // 只获取已通过的评论
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 获取评论的回复列表
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>回复分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentDto>> GetRepliesByCommentIdAsync(long commentId, int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentQueryDto
            {
                ParentCommentId = commentId,
                CommentStatus = 1, // 只获取已通过的回复
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 审核评论
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AuditAsync(TaktNewsCommentAuditDto input)
        {
            var comment = await CommentRepository.GetByIdAsync(input.CommentId)
                ?? throw new TaktException(L("Routine.NewsComment.NotFound", input.CommentId));

            // 更新审核信息
            comment.CommentStatus = input.CommentStatus;
            comment.AuditedBy = _currentUser.UserName ?? "system";
            comment.AuditedTime = DateTime.Now;
            comment.AuditComments = input.AuditComments;
            comment.AuditType = input.AuditType;
            comment.UpdateBy = _currentUser.UserName ?? "system";
            comment.UpdateTime = DateTime.Now;

            var result = await CommentRepository.UpdateAsync(comment);
            if (result > 0)
            {
                _logger.Info($"审核评论成功，评论ID：{input.CommentId}，状态：{input.CommentStatus}，审核人：{comment.AuditedBy}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量审核评论
        /// </summary>
        /// <param name="input">批量审核对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchAuditAsync(TaktNewsCommentBatchAuditDto input)
        {
            if (input.CommentIds == null || input.CommentIds.Length == 0) return false;

            var comments = await CommentRepository.GetListAsync(x => input.CommentIds.Contains(x.Id));
            if (!comments.Any()) return false;

            var currentTime = DateTime.Now;
            var currentUser = _currentUser.UserName ?? "system";

            foreach (var comment in comments)
            {
                comment.CommentStatus = input.CommentStatus;
                comment.AuditedBy = currentUser;
                comment.AuditedTime = currentTime;
                comment.AuditComments = input.AuditComments;
                comment.AuditType = input.AuditType;
                comment.UpdateBy = currentUser;
                comment.UpdateTime = currentTime;
            }

            var result = await CommentRepository.UpdateRangeAsync(comments);
            if (result > 0)
            {
                _logger.Info($"批量审核评论成功，评论数量：{result}，状态：{input.CommentStatus}，审核人：{currentUser}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 通过评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> ApproveAsync(long commentId, string? AuditComments = null)
        {
            var auditDto = new TaktNewsCommentAuditDto
            {
                CommentId = commentId,
                CommentStatus = 1, // 已通过
                AuditComments = AuditComments ?? "审核通过",
                AuditType = 1
            };

            return await AuditAsync(auditDto);
        }

        /// <summary>
        /// 拒绝评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <param name="AuditComments">审核意见</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RejectAsync(long commentId, string? AuditComments = null)
        {
            var auditDto = new TaktNewsCommentAuditDto
            {
                CommentId = commentId,
                CommentStatus = 2, // 已拒绝
                AuditComments = AuditComments ?? "审核拒绝",
                AuditType = 1
            };

            return await AuditAsync(auditDto);
        }

        /// <summary>
        /// 获取待审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>待审核评论分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentDto>> GetPendingAuditListAsync(int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentQueryDto
            {
                CommentStatus = 0, // 待审核
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 获取已审核评论列表
        /// </summary>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>已审核评论分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsCommentDto>> GetAuditedListAsync(int pageIndex = 1, int pageSize = 20)
        {
            var query = new TaktNewsCommentQueryDto
            {
                CommentStatus = 1, // 已通过
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            return await GetListAsync(query);
        }

        /// <summary>
        /// 验证评论内容字数
        /// </summary>
        /// <param name="content">评论内容</param>
        private void ValidateCommentLength(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
            {
                throw new TaktException(L("Routine.NewsComment.ContentEmpty"));
            }

            var contentLength = content.Trim().Length;

            if (contentLength < _commentOptions.MinCommentLength)
            {
                throw new TaktException(L("Routine.NewsComment.ContentTooShort", _commentOptions.MinCommentLength));
            }

            if (contentLength > _commentOptions.MaxCommentLength)
            {
                throw new TaktException(L("Routine.NewsComment.ContentTooLong", _commentOptions.MaxCommentLength));
            }
        }

        /// <summary>
        /// 验证评论频率限制
        /// </summary>
        /// <param name="userId">用户ID</param>
        private async Task ValidateCommentRateLimitAsync(long userId)
        {
            if (_commentOptions.CommentRateLimit <= 0) return;

            var lastCommentTime = await CommentRepository.GetFirstAsync(x => x.CommentUserId == userId);
            if (lastCommentTime != null)
            {
                var timeDiff = DateTime.Now - lastCommentTime.CreateTime;
                if (timeDiff.TotalSeconds < _commentOptions.CommentRateLimit)
                {
                    throw new TaktException(L("Routine.NewsComment.RateLimitExceeded", _commentOptions.CommentRateLimit));
                }
            }
        }

        /// <summary>
        /// 验证IP评论数量限制
        /// </summary>
        private async Task ValidateIpCommentLimitAsync()
        {
            if (_commentOptions.DailyIpCommentLimit <= 0) return;

            var clientIp = GetClientIpAddress();
            if (string.IsNullOrEmpty(clientIp)) return;

            var today = DateTime.Today;
            var ipCommentCount = await CommentRepository.GetCountAsync(x =>
                x.IpAddress == clientIp && x.CreateTime >= today);

            if (ipCommentCount >= _commentOptions.DailyIpCommentLimit)
            {
                throw new TaktException(L("Routine.NewsComment.IpLimitExceeded", _commentOptions.DailyIpCommentLimit));
            }
        }

        /// <summary>
        /// 验证用户评论数量限制
        /// </summary>
        /// <param name="userId">用户ID</param>
        private async Task ValidateUserCommentLimitAsync(long userId)
        {
            if (_commentOptions.DailyUserCommentLimit <= 0) return;

            var today = DateTime.Today;
            var userCommentCount = await CommentRepository.GetCountAsync(x =>
                x.CommentUserId == userId && x.CreateTime >= today);

            if (userCommentCount >= _commentOptions.DailyUserCommentLimit)
            {
                throw new TaktException(L("Routine.NewsComment.UserLimitExceeded", _commentOptions.DailyUserCommentLimit));
            }
        }

        /// <summary>
        /// 验证重复评论
        /// </summary>
        /// <param name="content">评论内容</param>
        /// <param name="userId">用户ID</param>
        private async Task ValidateDuplicateCommentAsync(string content, long userId)
        {
            if (_commentOptions.DuplicateCheckHours <= 0) return;

            var checkTime = DateTime.Now.AddHours(-_commentOptions.DuplicateCheckHours);
            var duplicateComment = await CommentRepository.GetFirstAsync(x =>
                x.CommentUserId == userId &&
                x.CommentContent == content &&
                x.CreateTime >= checkTime);

            if (duplicateComment != null)
            {
                throw new TaktException(L("Routine.NewsComment.DuplicateContent"));
            }
        }

        /// <summary>
        /// 获取评论字数限制信息
        /// </summary>
        /// <returns>字数限制信息</returns>
        public (int minLength, int maxLength) GetCommentLengthLimit()
        {
            return (_commentOptions.MinCommentLength, _commentOptions.MaxCommentLength);
        }

        /// <summary>
        /// 检查评论内容是否符合字数要求
        /// </summary>
        /// <param name="content">评论内容</param>
        /// <returns>检查结果</returns>
        public (bool isValid, string message) ValidateCommentContent(string content)
        {
            try
            {
                if (_commentOptions.EnableLengthLimit)
                {
                    ValidateCommentLength(content);
                }
                return (true, "评论内容符合要求");
            }
            catch (TaktException ex)
            {
                return (false, ex.Message);
            }
        }

        /// <summary>
        /// 敏感词过滤
        /// </summary>
        /// <param name="content">评论内容</param>
        /// <returns>过滤后的内容</returns>
        private Task<string> FilterSensitiveWordsAsync(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Task.FromResult(content);
            }

            try
            {
                // 检查是否包含敏感词
                if (TaktWordFilterHelper.ContainsSensitiveWord(content))
                {
                    // 获取敏感词列表
                    var sensitiveWords = TaktWordFilterHelper.GetSensitiveWords(content);
                    _logger.Warn($"检测到敏感词：{string.Join(", ", sensitiveWords)}");

                    // 过滤敏感词
                    var filteredContent = TaktWordFilterHelper.FilterSensitiveWord(content);
                    return Task.FromResult(filteredContent);
                }

                return Task.FromResult(content);
            }
            catch (Exception ex)
            {
                _logger.Error($"敏感词过滤失败：{ex.Message}", ex);
                // 如果敏感词过滤失败，返回原内容
                return Task.FromResult(content);
            }
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
        /// 增加新闻评论次数
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>是否成功</returns>
        private async Task<bool> IncreaseNewsCommentCountAsync(long newsId)
        {
            try
            {
                var news = await NewsRepository.GetByIdAsync(newsId);
                if (news != null)
                {
                    news.NewsCommentCount++;
                    await NewsRepository.UpdateAsync(news);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"增加新闻评论次数失败，新闻ID：{newsId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 减少新闻评论次数
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="count">减少数量</param>
        /// <returns>是否成功</returns>
        private async Task<bool> DecreaseNewsCommentCountAsync(long newsId, int count = 1)
        {
            try
            {
                var news = await NewsRepository.GetByIdAsync(newsId);
                if (news != null)
                {
                    news.NewsCommentCount = Math.Max(0, news.NewsCommentCount - count);
                    await NewsRepository.UpdateAsync(news);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"减少新闻评论次数失败，新闻ID：{newsId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 增加父评论回复次数
        /// </summary>
        /// <param name="parentCommentId">父评论ID</param>
        /// <returns>是否成功</returns>
        private async Task<bool> IncreaseParentCommentReplyCountAsync(long parentCommentId)
        {
            try
            {
                var parentComment = await CommentRepository.GetByIdAsync(parentCommentId);
                if (parentComment != null)
                {
                    parentComment.ReplyCount++;
                    await CommentRepository.UpdateAsync(parentComment);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"增加父评论回复次数失败，父评论ID：{parentCommentId}，错误：{ex.Message}", ex);
                return false;
            }
        }

        /// <summary>
        /// 减少父评论回复次数
        /// </summary>
        /// <param name="parentCommentId">父评论ID</param>
        /// <param name="count">减少数量</param>
        /// <returns>是否成功</returns>
        private async Task<bool> DecreaseParentCommentReplyCountAsync(long parentCommentId, int count = 1)
        {
            try
            {
                var parentComment = await CommentRepository.GetByIdAsync(parentCommentId);
                if (parentComment != null)
                {
                    parentComment.ReplyCount = Math.Max(0, parentComment.ReplyCount - count);
                    await CommentRepository.UpdateAsync(parentComment);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"减少父评论回复次数失败，父评论ID：{parentCommentId}，错误：{ex.Message}", ex);
                return false;
            }
        }
    }
}

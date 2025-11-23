#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsTopicParticipantService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题参与者服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题参与者服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktNewsTopicParticipantService : TaktBaseService, ITaktNewsTopicParticipantService
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
        public TaktNewsTopicParticipantService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻话题参与者仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopicParticipant> TopicParticipantRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopicParticipant>();

        /// <summary>
        /// 获取新闻话题仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopic> TopicRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopic>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNewsTopicParticipant, bool>> TopicParticipantQueryExpression(TaktNewsTopicParticipantQueryDto? query)
        {
            return Expressionable.Create<TaktNewsTopicParticipant>()
                .AndIF(query?.TopicId.HasValue == true, x => x.TopicId == query!.TopicId!.Value)
                .AndIF(query?.UserId.HasValue == true, x => x.UserId == query!.UserId!.Value)
                .AndIF(!string.IsNullOrEmpty(query?.UserName), x => x.UserName!.Contains(query!.UserName!))
                .AndIF(query?.ParticipationType.HasValue == true, x => x.ParticipationType == query!.ParticipationType!.Value)
                .AndIF(query?.ParticipationStatus.HasValue == true, x => x.ParticipationStatus == query!.ParticipationStatus!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻话题参与者分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题参与者分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetListAsync(TaktNewsTopicParticipantQueryDto? query)
        {
            query ??= new TaktNewsTopicParticipantQueryDto();

            _logger.Info($"查询新闻话题参与者列表，参数：TopicId={query.TopicId}, UserId={query.UserId}");

            var result = await TopicParticipantRepository.GetPagedListAsync(
                TopicParticipantQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.ParticipationTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻话题参与者列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsTopicParticipantDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicParticipantDto>>()
            };
        }

        /// <summary>
        /// 获取新闻话题参与者详情
        /// </summary>
        /// <param name="id">参与者ID</param>
        /// <returns>新闻话题参与者详情</returns>
        public async Task<TaktNewsTopicParticipantDto> GetByIdAsync(long id)
        {
            var participant = await TopicParticipantRepository.GetByIdAsync(id);
            if (participant == null)
            {
                throw TaktException.NotFound($"新闻话题参与者ID {id} 不存在");
            }

            return participant.Adapt<TaktNewsTopicParticipantDto>();
        }

        /// <summary>
        /// 创建新闻话题参与者
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>参与者ID</returns>
        public async Task<long> CreateAsync(TaktNewsTopicParticipantCreateDto input)
        {
            var participant = input.Adapt<TaktNewsTopicParticipant>();
            participant.CreateBy = _currentUser.UserName ?? "system";
            participant.CreateTime = DateTime.Now;
            participant.ParticipationTime = DateTime.Now;

            var result = await TopicParticipantRepository.CreateAsync(participant);
            return result > 0 ? participant.Id : throw new TaktException("创建新闻话题参与者失败");
        }

        /// <summary>
        /// 更新新闻话题参与者
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsTopicParticipantUpdateDto input)
        {
            var existingParticipant = await TopicParticipantRepository.GetByIdAsync(input.ParticipantId);
            if (existingParticipant == null)
            {
                throw TaktException.NotFound($"新闻话题参与者ID {input.ParticipantId} 不存在");
            }

            var participant = input.Adapt<TaktNewsTopicParticipant>();
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻话题参与者
        /// </summary>
        /// <param name="id">参与者ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var participant = await TopicParticipantRepository.GetByIdAsync(id);
            if (participant == null)
            {
                throw TaktException.NotFound($"新闻话题参与者ID {id} 不存在");
            }

            participant.DeleteBy = _currentUser.UserName ?? "system";
            participant.DeleteTime = DateTime.Now;
            participant.IsDeleted = 1;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 批量删除新闻话题参与者
        /// </summary>
        /// <param name="participantIds">参与者ID数组</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] participantIds)
        {
            var participantList = await TopicParticipantRepository.GetListAsync(x => participantIds.Contains(x.Id));
            if (!participantList.Any())
            {
                return false;
            }

            var currentUser = _currentUser.UserName ?? "system";
            var currentTime = DateTime.Now;

            foreach (var participant in participantList)
            {
                participant.DeleteBy = currentUser;
                participant.DeleteTime = currentTime;
                participant.IsDeleted = 1;
            }

            var result = await TopicParticipantRepository.DeleteRangeAsync(participantIds.Cast<object>().ToList());
            return result > 0;
        }

        /// <summary>
        /// 更新参与者状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktNewsTopicParticipantStatusDto input)
        {
            var participant = await TopicParticipantRepository.GetByIdAsync(input.ParticipantId);
            if (participant == null)
            {
                throw TaktException.NotFound($"新闻话题参与者ID {input.ParticipantId} 不存在");
            }

            participant.ParticipationStatus = input.ParticipationStatus;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <param name="participationType">参与类型</param>
        /// <returns>是否成功</returns>
        public async Task<bool> JoinTopicAsync(long topicId, long userId, string userName, string? userAvatar = null, int participationType = 1)
        {
            // 检查用户是否已经参与该话题
            var existingParticipant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (existingParticipant != null)
            {
                // 如果已存在，更新参与状态
                existingParticipant.ParticipationStatus = 1;
                existingParticipant.UpdateBy = _currentUser.UserName ?? "system";
                existingParticipant.UpdateTime = DateTime.Now;

                var result = await TopicParticipantRepository.UpdateAsync(existingParticipant);
                return result > 0;
            }

            // 创建新的参与者记录
            var participant = new TaktNewsTopicParticipant
            {
                TopicId = topicId,
                UserId = userId,
                UserName = userName,
                UserAvatar = userAvatar,
                ParticipationType = 1,
                ParticipationStatus = 1,
                ParticipationTime = DateTime.Now,
                CreateBy = _currentUser.UserName ?? "system",
                CreateTime = DateTime.Now
            };

            var createResult = await TopicParticipantRepository.CreateAsync(participant);
            return createResult > 0;
        }

        /// <summary>
        /// 用户退出话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> LeaveTopicAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.ParticipationStatus = 0;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 检查用户是否已参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已参与</returns>
        public async Task<bool> IsUserJoinedTopicAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            return participant != null && participant.ParticipationStatus == 1;
        }

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题参与者分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetUserJoinedTopicsAsync(long userId, int pageIndex = 1, int pageSize = 20)
        {
            var participants = await TopicParticipantRepository.GetPagedListAsync(
                x => x.UserId == userId && x.ParticipationStatus == 1 && x.IsDeleted == 0,
                pageIndex,
                pageSize,
                x => x.ParticipationTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktNewsTopicParticipantDto>
            {
                TotalNum = participants.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = participants.Rows.Adapt<List<TaktNewsTopicParticipantDto>>()
            };
        }

        /// <summary>
        /// 获取话题的参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetTopicParticipantsAsync(long topicId, int pageIndex = 1, int pageSize = 20)
        {
            var result = await TopicParticipantRepository.GetPagedListAsync(
                x => x.TopicId == topicId && x.ParticipationStatus == 1 && x.IsDeleted == 0,
                pageIndex,
                pageSize,
                x => x.ParticipationTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktNewsTopicParticipantDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicParticipantDto>>()
            };
        }

        /// <summary>
        /// 增加话题参与者数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseParticipantCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicParticipantCount++;
            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 减少话题参与者数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DecreaseParticipantCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            if (topic.TopicParticipantCount > 0)
            {
                topic.TopicParticipantCount--;
            }
            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 搜索参与者
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicParticipantDto>> SearchParticipantsAsync(string keyword, int pageIndex = 1, int pageSize = 20)
        {
            var result = await TopicParticipantRepository.GetPagedListAsync(
                x => x.UserName!.Contains(keyword) && x.IsDeleted == 0,
                pageIndex,
                pageSize,
                x => x.ParticipationTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktNewsTopicParticipantDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicParticipantDto>>()
            };
        }

        /// <summary>
        /// 获取参与者统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetParticipantStatisticsAsync()
        {
            var totalParticipants = await TopicParticipantRepository.GetCountAsync(x => x.IsDeleted == 0);
            var activeParticipants = await TopicParticipantRepository.GetCountAsync(x => x.ParticipationStatus == 1 && x.IsDeleted == 0);

            return new
            {
                TotalParticipants = totalParticipants,
                ActiveParticipants = activeParticipants,
                InactiveParticipants = totalParticipants - activeParticipants
            };
        }

        /// <summary>
        /// 更新用户活跃时间
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateUserActiveTimeAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.LastActiveTime = DateTime.Now;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 增加用户贡献分数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="score">增加的分数</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseContributionScoreAsync(long topicId, long userId, int score = 1)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.ContributionScore += score;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 增加用户内容数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseContentCountAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.ContentCount++;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 增加用户评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseCommentCountAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.CommentCount++;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 增加用户点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseLikeCountAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.LikeCount++;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 增加用户分享数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseShareCountAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.ShareCount++;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 设置用户通知设置
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="receiveNotification">是否接收通知</param>
        /// <param name="notificationType">通知类型</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SetNotificationSettingsAsync(long topicId, long userId, bool receiveNotification, int notificationType = 1)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => 
                x.TopicId == topicId && x.UserId == userId && x.IsDeleted == 0);

            if (participant == null)
            {
                return false;
            }

            participant.ReceiveNotification = receiveNotification ? 1 : 0;
            participant.NotificationType = notificationType;
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            return result > 0;
        }

        /// <summary>
        /// 获取话题活跃用户列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>活跃用户列表</returns>
        public async Task<List<TaktNewsTopicParticipantDto>> GetActiveUsersAsync(long topicId, int count = 10)
        {
            var participants = await TopicParticipantRepository.GetListAsync(x => 
                x.TopicId == topicId && x.ParticipationStatus == 1 && x.IsDeleted == 0);

            return participants.OrderByDescending(x => x.LastActiveTime)
                .Take(count)
                .Adapt<List<TaktNewsTopicParticipantDto>>();
        }

        /// <summary>
        /// 获取话题贡献者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="count">获取数量</param>
        /// <returns>贡献者列表</returns>
        public async Task<List<TaktNewsTopicParticipantDto>> GetTopContributorsAsync(long topicId, int count = 10)
        {
            var participants = await TopicParticipantRepository.GetListAsync(x => 
                x.TopicId == topicId && x.ParticipationStatus == 1 && x.IsDeleted == 0);

            return participants.OrderByDescending(x => x.ContributionScore)
                .Take(count)
                .Adapt<List<TaktNewsTopicParticipantDto>>();
        }

        /// <summary>
        /// 获取用户参与话题统计
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>统计信息</returns>
        public async Task<object> GetUserParticipationStatisticsAsync(long userId)
        {
            var participants = await TopicParticipantRepository.GetListAsync(x => 
                x.UserId == userId && x.IsDeleted == 0);

            var totalTopics = participants.Count;
            var activeTopics = participants.Count(x => x.ParticipationStatus == 1);
            var totalContribution = participants.Sum(x => x.ContributionScore);
            var totalContent = participants.Sum(x => x.ContentCount);
            var totalComments = participants.Sum(x => x.CommentCount);
            var totalLikes = participants.Sum(x => x.LikeCount);
            var totalShares = participants.Sum(x => x.ShareCount);

            return new
            {
                TotalTopics = totalTopics,
                ActiveTopics = activeTopics,
                TotalContribution = totalContribution,
                TotalContent = totalContent,
                TotalComments = totalComments,
                TotalLikes = totalLikes,
                TotalShares = totalShares
            };
        }

        /// <summary>
        /// 获取话题参与统计
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>统计信息</returns>
        public async Task<object> GetTopicParticipationStatisticsAsync(long topicId)
        {
            var participants = await TopicParticipantRepository.GetListAsync(x => 
                x.TopicId == topicId && x.IsDeleted == 0);

            var totalParticipants = participants.Count;
            var activeParticipants = participants.Count(x => x.ParticipationStatus == 1);
            var totalContribution = participants.Sum(x => x.ContributionScore);
            var totalContent = participants.Sum(x => x.ContentCount);
            var totalComments = participants.Sum(x => x.CommentCount);
            var totalLikes = participants.Sum(x => x.LikeCount);
            var totalShares = participants.Sum(x => x.ShareCount);

            return new
            {
                TotalParticipants = totalParticipants,
                ActiveParticipants = activeParticipants,
                TotalContribution = totalContribution,
                TotalContent = totalContent,
                TotalComments = totalComments,
                TotalLikes = totalLikes,
                TotalShares = totalShares
            };
        }
    }
} 


#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsTopicService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题服务实现
//===================================================================
using Microsoft.AspNetCore.Http;
namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public class TaktNewsTopicService : TaktBaseService, ITaktNewsTopicService
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
        public TaktNewsTopicService(
            ITaktRepositoryFactory repositoryFactory,
            ITaktLogger logger,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻话题仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopic> TopicRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopic>();

        /// <summary>
        /// 获取新闻话题关系仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopicRelation> TopicRelationRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopicRelation>();

        /// <summary>
        /// 获取新闻话题参与者仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopicParticipant> TopicParticipantRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopicParticipant>();

        /// <summary>
        /// 获取新闻仓储
        /// </summary>
        private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();

        /// <summary>
        /// 话题查询表达式
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>查询表达式</returns>
        private Expression<Func<TaktNewsTopic, bool>> TopicQueryExpression(TaktNewsTopicQueryDto query)
        {
            return Expressionable.Create<TaktNewsTopic>()
                .AndIF(!string.IsNullOrEmpty(query.TopicName), x => x.TopicName!.Contains(query.TopicName!))
                .AndIF(!string.IsNullOrEmpty(query.TopicDescription), x => x.TopicDescription!.Contains(query.TopicDescription!))
                .AndIF(!string.IsNullOrEmpty(query.TopicKeywords), x => x.TopicKeywords!.Contains(query.TopicKeywords!))
                .AndIF(!string.IsNullOrEmpty(query.TopicCategory), x => x.TopicCategory!.Contains(query.TopicCategory!))
                .AndIF(!string.IsNullOrEmpty(query.TopicTags), x => x.TopicTags!.Contains(query.TopicTags!))
                .AndIF(query.Status.HasValue, x => x.Status == query.Status!.Value)
                .AndIF(query.TopicIsHot.HasValue, x => x.TopicIsHot == query.TopicIsHot!.Value)
                .AndIF(query.TopicIsRecommend.HasValue, x => x.TopicIsRecommend == query.TopicIsRecommend!.Value)
                .AndIF(query.TopicIsTop.HasValue, x => x.TopicIsTop == query.TopicIsTop!.Value)
                .AndIF(query.TopicType.HasValue, x => x.TopicType == query.TopicType!.Value)
                .AndIF(query.TopicCreatorId.HasValue, x => x.TopicCreatorId == query.TopicCreatorId!.Value)
                .AndIF(!string.IsNullOrEmpty(query.TopicCreatorName), x => x.TopicCreatorName!.Contains(query.TopicCreatorName!))
                .AndIF(query.StartTime.HasValue, x => x.CreateTime >= query.StartTime!.Value)
                .AndIF(query.EndTime.HasValue, x => x.CreateTime <= query.EndTime!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻话题分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicDto>> GetListAsync(TaktNewsTopicQueryDto? query)
        {
            query ??= new TaktNewsTopicQueryDto();

            _logger.Info($"查询新闻话题列表，参数：TopicName={query.TopicName}, Status={query.Status}");

            var result = await TopicRepository.GetPagedListAsync(
                TopicQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻话题列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsTopicDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicDto>>()
            };
        }

        /// <summary>
        /// 获取新闻话题详情
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>新闻话题详情</returns>
        public async Task<TaktNewsTopicDto> GetByIdAsync(long id)
        {
            var topic = await TopicRepository.GetByIdAsync(id);
            return topic == null ? throw new TaktException(L("Routine.NewsTopic.NotFound", id)) : topic.Adapt<TaktNewsTopicDto>();
        }

        /// <summary>
        /// 创建新闻话题
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>话题ID</returns>
        public async Task<long> CreateAsync(TaktNewsTopicCreateDto input)
        {
            // 检查话题名称是否已存在
            var existingTopic = await TopicRepository.GetFirstAsync(x => x.TopicName == input.TopicName);
            if (existingTopic != null)
            {
                throw new TaktException(L("Routine.NewsTopic.NameExists", input.TopicName));
            }

            var topic = input.Adapt<TaktNewsTopic>();
            topic.CreateBy = _currentUser.UserName ?? "system";
            topic.CreateTime = DateTime.Now;
            topic.TopicCreatorId = _currentUser.UserId;
            topic.TopicCreatorName = _currentUser.UserName ?? "system";

            var result = await TopicRepository.CreateAsync(topic);
            if (result > 0)
            {
                _logger.Info($"创建新闻话题成功，话题ID：{topic.Id}，话题名称：{input.TopicName}");
                return topic.Id;
            }

            throw new TaktException(L("Routine.NewsTopic.CreateFailed"));
        }

        /// <summary>
        /// 更新新闻话题
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsTopicUpdateDto input)
        {
            var topic = await TopicRepository.GetByIdAsync(input.TopicId)
                ?? throw new TaktException(L("Routine.NewsTopic.NotFound", input.TopicId));

            // 检查话题名称是否已存在（排除当前话题）
            var existingTopic = await TopicRepository.GetFirstAsync(x => x.TopicName == input.TopicName && x.Id != input.TopicId);
            if (existingTopic != null)
            {
                throw new TaktException(L("Routine.NewsTopic.NameExists", input.TopicName));
            }

            input.Adapt(topic);
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻话题
        /// </summary>
        /// <param name="id">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var topic = await TopicRepository.GetByIdAsync(id)
                ?? throw new TaktException(L("Routine.NewsTopic.NotFound", id));

            var result = await TopicRepository.DeleteAsync(id);
            if (result > 0)
            {
                _logger.Info($"删除新闻话题成功，话题ID：{id}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 批量删除新闻话题
        /// </summary>
        /// <param name="topicIds">话题ID数组</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] topicIds)
        {
            if (topicIds == null || topicIds.Length == 0) return false;

            var topics = await TopicRepository.GetListAsync(x => topicIds.Contains(x.Id));
            if (!topics.Any()) return false;

            var result = await TopicRepository.DeleteRangeAsync(topics);
            if (result > 0)
            {
                _logger.Info($"批量删除新闻话题成功，话题数量：{result}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 更新话题状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktNewsTopicStatusDto input)
        {
            var topic = await TopicRepository.GetByIdAsync(input.TopicId)
                ?? throw new TaktException(L("Routine.NewsTopic.NotFound", input.TopicId));

            topic.Status = input.Status;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 获取热门话题列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门话题列表</returns>
        public async Task<List<TaktNewsTopicDto>> GetHotTopicsAsync(int count = 10)
        {
            var topics = await TopicRepository.GetListAsync(x => x.TopicIsHot == 1 && x.Status == 1);
            return topics.OrderByDescending(x => x.TopicParticipantCount).Take(count).Adapt<List<TaktNewsTopicDto>>();
        }

        /// <summary>
        /// 获取推荐话题列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐话题列表</returns>
        public async Task<List<TaktNewsTopicDto>> GetRecommendedTopicsAsync(int count = 10)
        {
            var topics = await TopicRepository.GetListAsync(x => x.TopicIsRecommend == 1 && x.Status == 1);
            return topics.OrderByDescending(x => x.CreateTime).Take(count).Adapt<List<TaktNewsTopicDto>>();
        }

        /// <summary>
        /// 获取话题下的新闻列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>新闻分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsDto>> GetNewsByTopicIdAsync(long topicId, int pageIndex = 1, int pageSize = 20)
        {
            var relations = await TopicRelationRepository.GetPagedListAsync(
                x => x.TopicId == topicId && x.RelationStatus == 1,
                pageIndex,
                pageSize,
                x => x.RelationTime,
                OrderByType.Desc);

            var newsIds = relations.Rows.Select(x => x.NewsId).ToArray();
            var newsList = await NewsRepository.GetListAsync(x => newsIds.Contains(x.Id));

            return new TaktPagedResult<TaktNewsDto>
            {
                TotalNum = relations.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = newsList.Adapt<List<TaktNewsDto>>()
            };
        }

        /// <summary>
        /// 获取话题参与者列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>参与者分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicParticipantDto>> GetTopicParticipantsAsync(long topicId, int pageIndex = 1, int pageSize = 20)
        {
            var result = await TopicParticipantRepository.GetPagedListAsync(
                x => x.TopicId == topicId && x.ParticipationStatus == 1,
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
        /// 用户参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <param name="userName">用户名</param>
        /// <param name="userAvatar">用户头像</param>
        /// <returns>是否成功</returns>
        public async Task<bool> JoinTopicAsync(long topicId, long userId, string userName, string? userAvatar = null)
        {
            // 检查话题是否存在
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null)
            {
                throw new TaktException(L("Routine.NewsTopic.NotFound", topicId));
            }

            // 检查用户是否已参与
            var existingParticipant = await TopicParticipantRepository.GetFirstAsync(x => x.TopicId == topicId && x.UserId == userId);
            if (existingParticipant != null)
            {
                if (existingParticipant.ParticipationStatus == 1)
                {
                    throw new TaktException(L("Routine.NewsTopic.AlreadyJoined"));
                }
                else
                {
                    // 重新参与
                    existingParticipant.ParticipationStatus = 1;
                    existingParticipant.ParticipationTime = DateTime.Now;
                    existingParticipant.LastActiveTime = DateTime.Now;
                    existingParticipant.UpdateBy = _currentUser.UserName ?? "system";
                    existingParticipant.UpdateTime = DateTime.Now;

                    var result = await TopicParticipantRepository.UpdateAsync(existingParticipant);
                    if (result > 0)
                    {
                        await IncreaseParticipantCountAsync(topicId);
                        return true;
                    }
                    return false;
                }
            }

            // 新增参与者
            var participant = new TaktNewsTopicParticipant
            {
                TopicId = topicId,
                UserId = userId,
                UserName = userName,
                UserAvatar = userAvatar,
                ParticipationType = 1, // 普通参与者
                ParticipationTime = DateTime.Now,
                ParticipationStatus = 1,
                LastActiveTime = DateTime.Now,
                ReceiveNotification = 0,
                NotificationType = 1, // 站内通知
                CreateBy = _currentUser.UserName ?? "system",
                CreateTime = DateTime.Now
            };

            var createResult = await TopicParticipantRepository.CreateAsync(participant);
            if (createResult > 0)
            {
                await IncreaseParticipantCountAsync(topicId);
                _logger.Info($"用户参与话题成功，话题ID：{topicId}，用户ID：{userId}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 用户退出话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> LeaveTopicAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => x.TopicId == topicId && x.UserId == userId);
            if (participant == null)
            {
                throw new TaktException(L("Routine.NewsTopic.NotJoined"));
            }

            participant.ParticipationStatus = 0; // 退出状态
            participant.UpdateBy = _currentUser.UserName ?? "system";
            participant.UpdateTime = DateTime.Now;

            var result = await TopicParticipantRepository.UpdateAsync(participant);
            if (result > 0)
            {
                await DecreaseParticipantCountAsync(topicId);
                _logger.Info($"用户退出话题成功，话题ID：{topicId}，用户ID：{userId}");
                return true;
            }

            return false;
        }

        /// <summary>
        /// 检查用户是否已参与话题
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <param name="userId">用户ID</param>
        /// <returns>是否已参与</returns>
        public async Task<bool> IsUserJoinedTopicAsync(long topicId, long userId)
        {
            var participant = await TopicParticipantRepository.GetFirstAsync(x => x.TopicId == topicId && x.UserId == userId);
            return participant?.ParticipationStatus == 1;
        }

        /// <summary>
        /// 获取用户参与的话题列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicDto>> GetUserJoinedTopicsAsync(long userId, int pageIndex = 1, int pageSize = 20)
        {
            var participants = await TopicParticipantRepository.GetPagedListAsync(
                x => x.UserId == userId && x.ParticipationStatus == 1,
                pageIndex,
                pageSize,
                x => x.ParticipationTime,
                OrderByType.Desc);

            var topicIds = participants.Rows.Select(x => x.TopicId).ToArray();
            var topics = await TopicRepository.GetListAsync(x => topicIds.Contains(x.Id));

            return new TaktPagedResult<TaktNewsTopicDto>
            {
                TotalNum = participants.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = topics.Adapt<List<TaktNewsTopicDto>>()
            };
        }

        /// <summary>
        /// 增加话题参与人数
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseParticipantCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicParticipantCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 减少话题参与人数
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
                topic.UpdateBy = _currentUser.UserName ?? "system";
                topic.UpdateTime = DateTime.Now;

                var result = await TopicRepository.UpdateAsync(topic);
                return result > 0;
            }

            return true;
        }

        /// <summary>
        /// 增加话题新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseNewsCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicNewsCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 减少话题新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DecreaseNewsCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            if (topic.TopicNewsCount > 0)
            {
                topic.TopicNewsCount--;
                topic.UpdateBy = _currentUser.UserName ?? "system";
                topic.UpdateTime = DateTime.Now;

                var result = await TopicRepository.UpdateAsync(topic);
                return result > 0;
            }

            return true;
        }

        /// <summary>
        /// 增加话题评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseCommentCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicCommentCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 减少话题评论数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DecreaseCommentCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            if (topic.TopicCommentCount > 0)
            {
                topic.TopicCommentCount--;
                topic.UpdateBy = _currentUser.UserName ?? "system";
                topic.UpdateTime = DateTime.Now;

                var result = await TopicRepository.UpdateAsync(topic);
                return result > 0;
            }

            return true;
        }

        /// <summary>
        /// 增加话题点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseLikeCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicLikeCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 减少话题点赞数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DecreaseLikeCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            if (topic.TopicLikeCount > 0)
            {
                topic.TopicLikeCount--;
                topic.UpdateBy = _currentUser.UserName ?? "system";
                topic.UpdateTime = DateTime.Now;

                var result = await TopicRepository.UpdateAsync(topic);
                return result > 0;
            }

            return true;
        }

        /// <summary>
        /// 增加话题分享数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseShareCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicShareCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 增加话题阅读数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> IncreaseReadCountAsync(long topicId)
        {
            var topic = await TopicRepository.GetByIdAsync(topicId);
            if (topic == null) return false;

            topic.TopicReadCount++;
            topic.UpdateBy = _currentUser.UserName ?? "system";
            topic.UpdateTime = DateTime.Now;

            var result = await TopicRepository.UpdateAsync(topic);
            return result > 0;
        }

        /// <summary>
        /// 搜索话题
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <returns>话题分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicDto>> SearchTopicsAsync(string keyword, int pageIndex = 1, int pageSize = 20)
        {
            var result = await TopicRepository.GetPagedListAsync(
                x => x.Status == 1 && (x.TopicName!.Contains(keyword) || x.TopicDescription!.Contains(keyword) || x.TopicKeywords!.Contains(keyword)),
                pageIndex,
                pageSize,
                x => x.CreateTime,
                OrderByType.Desc);

            return new TaktPagedResult<TaktNewsTopicDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = pageIndex,
                PageSize = pageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicDto>>()
            };
        }

        /// <summary>
        /// 获取话题统计信息
        /// </summary>
        /// <returns>统计信息</returns>
        public async Task<object> GetTopicStatisticsAsync()
        {
            var totalTopics = await TopicRepository.GetCountAsync(x => true);
            var activeTopics = await TopicRepository.GetCountAsync(x => x.Status == 1);
            var hotTopics = await TopicRepository.GetCountAsync(x => x.TopicIsHot == 1 && x.Status == 1);
            var recommendTopics = await TopicRepository.GetCountAsync(x => x.TopicIsRecommend == 1 && x.Status == 1);

            var today = DateTime.Today;
            var todayCreatedTopics = await TopicRepository.GetCountAsync(x => x.CreateTime >= today);

            return new
            {
                TotalTopics = totalTopics,
                ActiveTopics = activeTopics,
                HotTopics = hotTopics,
                RecommendTopics = recommendTopics,
                TodayCreatedTopics = todayCreatedTopics
            };
        }
    }
}




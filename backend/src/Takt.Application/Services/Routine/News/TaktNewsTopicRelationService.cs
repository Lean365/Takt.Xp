#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktNewsTopicRelationService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻话题关系服务实现
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题关系服务实现
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public class TaktNewsTopicRelationService : TaktBaseService, ITaktNewsTopicRelationService
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
        public TaktNewsTopicRelationService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取新闻话题关系仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopicRelation> TopicRelationRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopicRelation>();

        /// <summary>
        /// 获取新闻话题仓储
        /// </summary>
        private ITaktRepository<TaktNewsTopic> TopicRepository => _repositoryFactory.GetBusinessRepository<TaktNewsTopic>();

        /// <summary>
        /// 获取新闻仓储
        /// </summary>
        private ITaktRepository<TaktNews> NewsRepository => _repositoryFactory.GetBusinessRepository<TaktNews>();

        /// <summary>
        /// 构建查询条件
        /// </summary>
        private Expression<Func<TaktNewsTopicRelation, bool>> TopicRelationQueryExpression(TaktNewsTopicRelationQueryDto? query)
        {
            return Expressionable.Create<TaktNewsTopicRelation>()
                .AndIF(query?.NewsId.HasValue == true, x => x.NewsId == query!.NewsId!.Value)
                .AndIF(query?.TopicId.HasValue == true, x => x.TopicId == query!.TopicId!.Value)
                .AndIF(query?.RelationType.HasValue == true, x => x.RelationType == query!.RelationType!.Value)
                .AndIF(query?.RelationStatus.HasValue == true, x => x.RelationStatus == query!.RelationStatus!.Value)
                .AndIF(query?.IsAutoRelation.HasValue == true, x => x.IsAutoRelation == query!.IsAutoRelation!.Value)
                .AndIF(query?.RelationUserId.HasValue == true, x => x.RelationUserId == query!.RelationUserId!.Value)
                .ToExpression();
        }

        /// <summary>
        /// 获取新闻话题关系分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题关系分页列表</returns>
        public async Task<TaktPagedResult<TaktNewsTopicRelationDto>> GetListAsync(TaktNewsTopicRelationQueryDto? query)
        {
            query ??= new TaktNewsTopicRelationQueryDto();

            _logger.Info($"查询新闻话题关系列表，参数：NewsId={query.NewsId}, TopicId={query.TopicId}");

            var result = await TopicRelationRepository.GetPagedListAsync(
                TopicRelationQueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.RelationTime,
                OrderByType.Desc);

            _logger.Info($"查询新闻话题关系列表，共获取到 {result.TotalNum} 条记录");

            return new TaktPagedResult<TaktNewsTopicRelationDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktNewsTopicRelationDto>>()
            };
        }

        /// <summary>
        /// 获取新闻话题关系详情
        /// </summary>
        /// <param name="id">关系ID</param>
        /// <returns>新闻话题关系详情</returns>
        public async Task<TaktNewsTopicRelationDto> GetByIdAsync(long id)
        {
            var relation = await TopicRelationRepository.GetByIdAsync(id);
            if (relation == null)
            {
                throw TaktException.NotFound($"新闻话题关系ID {id} 不存在");
            }

            return relation.Adapt<TaktNewsTopicRelationDto>();
        }

        /// <summary>
        /// 创建新闻话题关系
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>关系ID</returns>
        public async Task<long> CreateAsync(TaktNewsTopicRelationCreateDto input)
        {
            var relation = input.Adapt<TaktNewsTopicRelation>();
            relation.CreateBy = _currentUser.UserName ?? "system";
            relation.CreateTime = DateTime.Now;
            relation.RelationTime = DateTime.Now;
            relation.RelationUserId = _currentUser.UserId;
            relation.RelationUserName = _currentUser.UserName ?? "system";

            var result = await TopicRelationRepository.CreateAsync(relation);
            return result > 0 ? relation.Id : throw new TaktException("创建新闻话题关系失败");
        }

        /// <summary>
        /// 更新新闻话题关系
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateAsync(TaktNewsTopicRelationUpdateDto input)
        {
            var existingRelation = await TopicRelationRepository.GetByIdAsync(input.RelationId);
            if (existingRelation == null)
            {
                throw TaktException.NotFound($"新闻话题关系ID {input.RelationId} 不存在");
            }

            var relation = input.Adapt<TaktNewsTopicRelation>();
            relation.UpdateBy = _currentUser.UserName ?? "system";
            relation.UpdateTime = DateTime.Now;

            var result = await TopicRelationRepository.UpdateAsync(relation);
            return result > 0;
        }

        /// <summary>
        /// 删除新闻话题关系
        /// </summary>
        /// <param name="id">关系ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteAsync(long id)
        {
            var relation = await TopicRelationRepository.GetByIdAsync(id);
            if (relation == null)
            {
                throw TaktException.NotFound($"新闻话题关系ID {id} 不存在");
            }

            relation.DeleteBy = _currentUser.UserName ?? "system";
            relation.DeleteTime = DateTime.Now;
            relation.IsDeleted = 1;

            var result = await TopicRelationRepository.UpdateAsync(relation);
            return result > 0;
        }

        /// <summary>
        /// 批量删除新闻话题关系
        /// </summary>
        /// <param name="relationIds">关系ID数组</param>
        /// <returns>是否成功</returns>
        public async Task<bool> BatchDeleteAsync(long[] relationIds)
        {
            var relationList = await TopicRelationRepository.GetListAsync(x => relationIds.Contains(x.Id));
            if (!relationList.Any())
            {
                return false;
            }

            var currentUser = _currentUser.UserName ?? "system";
            var currentTime = DateTime.Now;

            foreach (var relation in relationList)
            {
                relation.DeleteBy = currentUser;
                relation.DeleteTime = currentTime;
                relation.IsDeleted = 1;
            }

            var result = await TopicRelationRepository.DeleteRangeAsync(relationIds.Cast<object>().ToList());
            return result > 0;
        }

        /// <summary>
        /// 更新关系状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateStatusAsync(TaktNewsTopicRelationStatusDto input)
        {
            var relation = await TopicRelationRepository.GetByIdAsync(input.RelationId);
            if (relation == null)
            {
                throw TaktException.NotFound($"新闻话题关系ID {input.RelationId} 不存在");
            }

            relation.RelationStatus = input.RelationStatus;
            relation.UpdateBy = _currentUser.UserName ?? "system";
            relation.UpdateTime = DateTime.Now;

            var result = await TopicRelationRepository.UpdateAsync(relation);
            return result > 0;
        }

        /// <summary>
        /// 关联新闻到话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <param name="relationType">关系类型</param>
        /// <param name="relationWeight">关系权重</param>
        /// <param name="isAutoRelation">是否自动关联</param>
        /// <returns>是否成功</returns>
        public async Task<bool> RelateNewsToTopicAsync(long newsId, long topicId, int relationType = 1, int relationWeight = 1, bool isAutoRelation = false)
        {
            // 检查是否已存在关联
            var existingRelation = await TopicRelationRepository.GetFirstAsync(x => 
                x.NewsId == newsId && x.TopicId == topicId && x.IsDeleted == 0);

            if (existingRelation != null)
            {
                // 如果已存在，更新关联信息
                existingRelation.RelationType = relationType;
                existingRelation.RelationWeight = relationWeight;
                existingRelation.IsAutoRelation = isAutoRelation ? 1 : 0;
                existingRelation.RelationStatus = 1;
                existingRelation.UpdateBy = _currentUser.UserName ?? "system";
                existingRelation.UpdateTime = DateTime.Now;

                var result = await TopicRelationRepository.UpdateAsync(existingRelation);
                return result > 0;
            }

            // 创建新的关联
            var relation = new TaktNewsTopicRelation
            {
                NewsId = newsId,
                TopicId = topicId,
                RelationType = relationType,
                RelationWeight = relationWeight,
                RelationTime = DateTime.Now,
                RelationUserId = _currentUser.UserId,
                RelationUserName = _currentUser.UserName ?? "system",
                IsAutoRelation = isAutoRelation ? 1 : 0,
                RelationStatus = 1,
                CreateBy = _currentUser.UserName ?? "system",
                CreateTime = DateTime.Now
            };

            var createResult = await TopicRelationRepository.CreateAsync(relation);
            return createResult > 0;
        }

        /// <summary>
        /// 解除新闻与话题的关联
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UnrelateNewsFromTopicAsync(long newsId, long topicId)
        {
            var relation = await TopicRelationRepository.GetFirstAsync(x => 
                x.NewsId == newsId && x.TopicId == topicId && x.IsDeleted == 0);

            if (relation == null)
            {
                return false;
            }

            relation.DeleteBy = _currentUser.UserName ?? "system";
            relation.DeleteTime = DateTime.Now;
            relation.IsDeleted = 1;

            var result = await TopicRelationRepository.UpdateAsync(relation);
            return result > 0;
        }

        /// <summary>
        /// 批量关联新闻到话题
        /// </summary>
        /// <param name="newsIds">新闻ID数组</param>
        /// <param name="topicId">话题ID</param>
        /// <param name="relationType">关系类型</param>
        /// <param name="relationWeight">关系权重</param>
        /// <param name="isAutoRelation">是否自动关联</param>
        /// <returns>成功关联的数量</returns>
        public async Task<int> BatchRelateNewsToTopicAsync(long[] newsIds, long topicId, int relationType = 1, int relationWeight = 1, bool isAutoRelation = false)
        {
            var successCount = 0;
            foreach (var newsId in newsIds)
            {
                var result = await RelateNewsToTopicAsync(newsId, topicId, relationType, relationWeight, isAutoRelation);
                if (result)
                {
                    successCount++;
                }
            }
            return successCount;
        }

        /// <summary>
        /// 批量解除新闻与话题的关联
        /// </summary>
        /// <param name="newsIds">新闻ID数组</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>成功解除关联的数量</returns>
        public async Task<int> BatchUnrelateNewsFromTopicAsync(long[] newsIds, long topicId)
        {
            var successCount = 0;
            foreach (var newsId in newsIds)
            {
                var result = await UnrelateNewsFromTopicAsync(newsId, topicId);
                if (result)
                {
                    successCount++;
                }
            }
            return successCount;
        }

        /// <summary>
        /// 获取新闻关联的话题列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>话题关系列表</returns>
        public async Task<List<TaktNewsTopicRelationDto>> GetTopicsByNewsIdAsync(long newsId)
        {
            var relations = await TopicRelationRepository.GetListAsync(x => 
                x.NewsId == newsId && x.IsDeleted == 0);

            return relations.Adapt<List<TaktNewsTopicRelationDto>>();
        }

        /// <summary>
        /// 获取话题关联的新闻列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>新闻关系列表</returns>
        public async Task<List<TaktNewsTopicRelationDto>> GetNewsByTopicIdAsync(long topicId)
        {
            var relations = await TopicRelationRepository.GetListAsync(x => 
                x.TopicId == topicId && x.IsDeleted == 0);

            return relations.Adapt<List<TaktNewsTopicRelationDto>>();
        }

        /// <summary>
        /// 检查新闻是否已关联话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否已关联</returns>
        public async Task<bool> IsNewsRelatedToTopicAsync(long newsId, long topicId)
        {
            var relation = await TopicRelationRepository.GetFirstAsync(x => 
                x.NewsId == newsId && x.TopicId == topicId && x.IsDeleted == 0);

            return relation != null;
        }

        /// <summary>
        /// 获取新闻的话题数量
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>话题数量</returns>
        public async Task<int> GetNewsTopicCountAsync(long newsId)
        {
            var count = await TopicRelationRepository.GetCountAsync(x => 
                x.NewsId == newsId && x.IsDeleted == 0);

            return count;
        }

        /// <summary>
        /// 获取话题的新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>新闻数量</returns>
        public async Task<int> GetTopicNewsCountAsync(long topicId)
        {
            var count = await TopicRelationRepository.GetCountAsync(x => 
                x.TopicId == topicId && x.IsDeleted == 0);

            return count;
        }

        /// <summary>
        /// 自动关联新闻到相关话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="newsTitle">新闻标题</param>
        /// <param name="newsContent">新闻内容</param>
        /// <param name="newsKeywords">新闻关键词</param>
        /// <returns>成功关联的话题数量</returns>
        public async Task<int> AutoRelateNewsToTopicsAsync(long newsId, string newsTitle, string newsContent, string? newsKeywords = null)
        {
            // 获取所有活跃的话题
            var topics = await TopicRepository.GetListAsync(x => x.Status == 1 && x.IsDeleted == 0);

            var successCount = 0;
            var searchText = $"{newsTitle} {newsContent} {newsKeywords}".ToLower();

            foreach (var topic in topics)
            {
                // 检查话题关键词是否匹配
                var topicKeywords = $"{topic.TopicName} {topic.TopicDescription} {topic.TopicKeywords}".ToLower();
                
                if (topicKeywords.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Any(keyword => searchText.Contains(keyword)))
                {
                    var result = await RelateNewsToTopicAsync(newsId, topic.Id, 2, 1, true);
                    if (result)
                    {
                        successCount++;
                    }
                }
            }

            return successCount;
        }

        /// <summary>
        /// 根据关键词匹配话题
        /// </summary>
        /// <param name="keywords">关键词</param>
        /// <param name="count">返回数量</param>
        /// <returns>匹配的话题列表</returns>
        public async Task<List<TaktNewsTopicDto>> MatchTopicsByKeywordsAsync(string keywords, int count = 5)
        {
            var topics = await TopicRepository.GetListAsync(x => 
                x.Status == 1 && x.IsDeleted == 0 &&
                (x.TopicName!.Contains(keywords) || 
                 x.TopicDescription!.Contains(keywords) || 
                 x.TopicKeywords!.Contains(keywords)));

            return topics.Take(count).Adapt<List<TaktNewsTopicDto>>();
        }
    }
} 


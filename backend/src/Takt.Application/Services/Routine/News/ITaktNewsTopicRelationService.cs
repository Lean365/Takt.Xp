#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicRelationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题关系服务接口
//===================================================================



#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsTopicRelationService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-01
// 版本号 : V0.0.1
// 描述    : 新闻话题关系服务接口
//===================================================================

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻话题关系服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-01
    /// </remarks>
    public interface ITaktNewsTopicRelationService
    {
        /// <summary>
        /// 获取新闻话题关系分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻话题关系分页列表</returns>
        Task<TaktPagedResult<TaktNewsTopicRelationDto>> GetListAsync(TaktNewsTopicRelationQueryDto? query);

        /// <summary>
        /// 获取新闻话题关系详情
        /// </summary>
        /// <param name="id">关系ID</param>
        /// <returns>新闻话题关系详情</returns>
        Task<TaktNewsTopicRelationDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻话题关系
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>关系ID</returns>
        Task<long> CreateAsync(TaktNewsTopicRelationCreateDto input);

        /// <summary>
        /// 更新新闻话题关系
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsTopicRelationUpdateDto input);

        /// <summary>
        /// 删除新闻话题关系
        /// </summary>
        /// <param name="id">关系ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻话题关系
        /// </summary>
        /// <param name="relationIds">关系ID数组</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] relationIds);

        /// <summary>
        /// 更新关系状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktNewsTopicRelationStatusDto input);

        /// <summary>
        /// 关联新闻到话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <param name="relationType">关系类型</param>
        /// <param name="relationWeight">关系权重</param>
        /// <param name="isAutoRelation">是否自动关联</param>
        /// <returns>是否成功</returns>
        Task<bool> RelateNewsToTopicAsync(long newsId, long topicId, int relationType = 1, int relationWeight = 1, bool isAutoRelation = false);

        /// <summary>
        /// 解除新闻与话题的关联
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否成功</returns>
        Task<bool> UnrelateNewsFromTopicAsync(long newsId, long topicId);

        /// <summary>
        /// 批量关联新闻到话题
        /// </summary>
        /// <param name="newsIds">新闻ID数组</param>
        /// <param name="topicId">话题ID</param>
        /// <param name="relationType">关系类型</param>
        /// <param name="relationWeight">关系权重</param>
        /// <param name="isAutoRelation">是否自动关联</param>
        /// <returns>成功关联的数量</returns>
        Task<int> BatchRelateNewsToTopicAsync(long[] newsIds, long topicId, int relationType = 1, int relationWeight = 1, bool isAutoRelation = false);

        /// <summary>
        /// 批量解除新闻与话题的关联
        /// </summary>
        /// <param name="newsIds">新闻ID数组</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>成功解除关联的数量</returns>
        Task<int> BatchUnrelateNewsFromTopicAsync(long[] newsIds, long topicId);

        /// <summary>
        /// 获取新闻关联的话题列表
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>话题关系列表</returns>
        Task<List<TaktNewsTopicRelationDto>> GetTopicsByNewsIdAsync(long newsId);

        /// <summary>
        /// 获取话题关联的新闻列表
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>新闻关系列表</returns>
        Task<List<TaktNewsTopicRelationDto>> GetNewsByTopicIdAsync(long topicId);

        /// <summary>
        /// 检查新闻是否已关联话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="topicId">话题ID</param>
        /// <returns>是否已关联</returns>
        Task<bool> IsNewsRelatedToTopicAsync(long newsId, long topicId);

        /// <summary>
        /// 获取新闻的话题数量
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <returns>话题数量</returns>
        Task<int> GetNewsTopicCountAsync(long newsId);

        /// <summary>
        /// 获取话题的新闻数量
        /// </summary>
        /// <param name="topicId">话题ID</param>
        /// <returns>新闻数量</returns>
        Task<int> GetTopicNewsCountAsync(long topicId);

        /// <summary>
        /// 自动关联新闻到相关话题
        /// </summary>
        /// <param name="newsId">新闻ID</param>
        /// <param name="newsTitle">新闻标题</param>
        /// <param name="newsContent">新闻内容</param>
        /// <param name="newsKeywords">新闻关键词</param>
        /// <returns>成功关联的话题数量</returns>
        Task<int> AutoRelateNewsToTopicsAsync(long newsId, string newsTitle, string newsContent, string? newsKeywords = null);

        /// <summary>
        /// 根据关键词匹配话题
        /// </summary>
        /// <param name="keywords">关键词</param>
        /// <param name="count">返回数量</param>
        /// <returns>匹配的话题列表</returns>
        Task<List<TaktNewsTopicDto>> MatchTopicsByKeywordsAsync(string keywords, int count = 5);
    }
}




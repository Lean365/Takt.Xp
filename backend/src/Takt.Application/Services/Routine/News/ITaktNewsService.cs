//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻服务接口
//===================================================================


//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktNewsService.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻服务接口
//===================================================================

using Takt.Application.Dtos.Routine.Document;

namespace Takt.Application.Services.Routine.News
{
    /// <summary>
    /// 新闻服务接口
    /// </summary>
    /// <remarks>
    /// 创建者: Claude
    /// 创建时间: 2024-12-01
    /// </remarks>
    public interface ITaktNewsService
    {
        /// <summary>
        /// 获取新闻分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>新闻分页列表</returns>
        Task<TaktPagedResult<TaktNewsDto>> GetListAsync(TaktNewsQueryDto query);

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>新闻详情</returns>
        Task<TaktNewsDto> GetByIdAsync(long id);

        /// <summary>
        /// 创建新闻
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>新闻ID</returns>
        Task<long> CreateAsync(TaktNewsCreateDto input);

        /// <summary>
        /// 更新新闻
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktNewsUpdateDto input);

        /// <summary>
        /// 删除新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 批量删除新闻
        /// </summary>
        /// <param name="newsIds">新闻ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] newsIds);

        /// <summary>
        /// 导入新闻数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName);

        /// <summary>
        /// 导出新闻数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktNewsQueryDto query, string sheetName);

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>返回导出的Excel文件名</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName);

        /// <summary>
        /// 更新新闻状态
        /// </summary>
        /// <param name="input">状态更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateStatusAsync(TaktNewsStatusDto input);

        /// <summary>
        /// 审核新闻
        /// </summary>
        /// <param name="input">审核对象</param>
        /// <returns>是否成功</returns>
        Task<bool> AuditAsync(TaktNewsAuditDto input);

        /// <summary>
        /// 发布新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> PublishAsync(long id);

        /// <summary>
        /// 下线新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> OfflineAsync(long id);

        /// <summary>
        /// 置顶新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isTop">是否置顶</param>
        /// <returns>是否成功</returns>
        Task<bool> SetTopAsync(long id, bool isTop);

        /// <summary>
        /// 推荐新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isRecommend">是否推荐</param>
        /// <returns>是否成功</returns>
        Task<bool> SetRecommendAsync(long id, bool isRecommend);

        /// <summary>
        /// 热门新闻
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <param name="isHot">是否热门</param>
        /// <returns>是否成功</returns>
        Task<bool> SetHotAsync(long id, bool isHot);

        /// <summary>
        /// 增加阅读次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseReadCountAsync(long id);

        /// <summary>
        /// 增加点赞次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseLikeCountAsync(long id);

        /// <summary>
        /// 增加评论次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseCommentCountAsync(long id);

        /// <summary>
        /// 增加分享次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseShareCountAsync(long id);

        /// <summary>
        /// 增加推荐次数
        /// </summary>
        /// <param name="id">新闻ID</param>
        /// <returns>是否成功</returns>
        Task<bool> IncreaseRecommendCountAsync(long id);

        /// <summary>
        /// 获取热门新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>热门新闻列表</returns>
        Task<List<TaktNewsDto>> GetHotNewsAsync(int count = 10);

        /// <summary>
        /// 获取推荐新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>推荐新闻列表</returns>
        Task<List<TaktNewsDto>> GetRecommendNewsAsync(int count = 10);

        /// <summary>
        /// 获取置顶新闻列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns>置顶新闻列表</returns>
        Task<List<TaktNewsDto>> GetTopNewsAsync(int count = 5);

        /// <summary>
        /// 根据分类获取新闻列表
        /// </summary>
        /// <param name="category">新闻分类</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        Task<List<TaktNewsDto>> GetNewsByCategoryAsync(string category, int count = 20);

        /// <summary>
        /// 搜索新闻
        /// </summary>
        /// <param name="keyword">搜索关键词</param>
        /// <param name="count">获取数量</param>
        /// <returns>新闻列表</returns>
        Task<List<TaktNewsDto>> SearchNewsAsync(string keyword, int count = 20);
    }
} 


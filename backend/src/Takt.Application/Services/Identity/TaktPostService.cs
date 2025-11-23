//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktPostService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-20 16:30
// 版本号 : V0.0.1
// 描述   : 岗位服务实现 - 使用仓储工厂模式
//===================================================================

using Microsoft.AspNetCore.Http;

namespace Takt.Application.Services.Identity
{
    /// <summary>
    /// 岗位服务实现
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-20
    /// 更新: 2024-12-01 - 使用仓储工厂模式支持多库
    /// </remarks>
    public class TaktPostService : TaktBaseService, ITaktPostService
    {
        /// <summary>
        /// 仓储工厂
        /// </summary>
        protected readonly ITaktRepositoryFactory _repositoryFactory;

        private ITaktRepository<TaktPost> PostRepository => _repositoryFactory.GetAuthRepository<TaktPost>();
        private ITaktRepository<TaktUserPost> UserPostRepository => _repositoryFactory.GetAuthRepository<TaktUserPost>();

        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktPostService(
            ITaktLogger logger,
            ITaktRepositoryFactory repositoryFactory,
            IHttpContextAccessor httpContextAccessor,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, httpContextAccessor, currentUser, localization)
        {
            _repositoryFactory = repositoryFactory ?? throw new ArgumentNullException(nameof(repositoryFactory));
        }

        /// <summary>
        /// 获取岗位分页列表
        /// </summary>
        public async Task<TaktPagedResult<TaktPostDto>> GetListAsync(TaktPostQueryDto query)
        {
            var result = await PostRepository.GetPagedListAsync(
                QueryExpression(query),
                query.PageIndex,
                query.PageSize,
                x => x.OrderNum,
                OrderByType.Asc);

            return new TaktPagedResult<TaktPostDto>
            {
                TotalNum = result.TotalNum,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                Rows = result.Rows.Adapt<List<TaktPostDto>>()
            };
        }

        /// <summary>
        /// 获取岗位详情
        /// </summary>
        public async Task<TaktPostDto> GetByIdAsync(long id)
        {
            var post = await PostRepository.GetByIdAsync(id);
            if (post == null)
                throw new TaktException(L("Identity.Post.NotFound"));

            return post.Adapt<TaktPostDto>();
        }

        /// <summary>
        /// 创建岗位
        /// </summary>
        public async Task<long> CreateAsync(TaktPostCreateDto input)
        {

            // 验证字段是否已存在
            await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", input.PostCode);
            await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", input.PostName);

            var post = input.Adapt<TaktPost>();
            var result = await PostRepository.CreateAsync(post);
            if (result > 0)
                _logger.Info(L("Common.AddSuccess"));

            return post.Id;
        }

        /// <summary>
        /// 更新岗位
        /// </summary>
        public async Task<bool> UpdateAsync(TaktPostUpdateDto input)
        {
            var post = await PostRepository.GetByIdAsync(input.PostId)
                ?? throw new TaktException(L("Identity.Post.NotFound"));

            // 验证字段是否已存在
            if (post.PostCode != input.PostCode)
                await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", input.PostCode, input.PostId);
            if (post.PostName != input.PostName)
                await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", input.PostName, input.PostId);

            input.Adapt(post);
            return await PostRepository.UpdateAsync(post) > 0;
        }

        /// <summary>
        /// 删除岗位
        /// </summary>
        public async Task<bool> DeleteAsync(long id)
        {
            var userPostRepository = _repositoryFactory.GetAuthRepository<TaktUserPost>();

            var post = await PostRepository.GetByIdAsync(id)
                ?? throw new TaktException(L("Identity.Post.NotFound"));

            // 检查是否有用户关联
            if (await UserPostRepository.AsQueryable().AnyAsync(x => x.PostId == id))
                throw new TaktException(L("Identity.Post.DeleteFailed"));

            return await PostRepository.DeleteAsync(post) > 0;
        }

        /// <summary>
        /// 批量删除岗位
        /// </summary>
        public async Task<bool> BatchDeleteAsync(long[] postIds)
        {
            if (postIds == null || postIds.Length == 0)
                throw new TaktException(L("Identity.Post.SelectRequired"));

            var userPostRepository = _repositoryFactory.GetAuthRepository<TaktUserPost>();

            // 检查是否有用户关联
            if (await UserPostRepository.AsQueryable().AnyAsync(up => postIds.Contains(up.PostId)))
                throw new TaktException(L("Identity.Post.HasUsers"));

            return await PostRepository.DeleteRangeAsync(postIds.Cast<object>().ToList()) > 0;
        }

        /// <summary>
        /// 获取岗位选项列表
        /// </summary>
        /// <returns>岗位选项列表</returns>
        public async Task<List<TaktSelectOption>> GetOptionsAsync()
        {
            var posts = await PostRepository.AsQueryable()
                .Where(p => p.PostStatus == 0 && p.IsDeleted == 0)  // 只获取正常状态且未删除的岗位
                .OrderBy(p => p.PostName)
                .Select(p => new TaktSelectOption
                {
                    DictLabel = p.PostName,
                    DictValue = p.Id,
                })
                .ToListAsync();
            return posts;
        }



        /// <summary>
        /// 更新岗位状态
        /// </summary>
        public async Task<bool> UpdateStatusAsync(TaktPostStatusDto input)
        {
            var post = await PostRepository.GetByIdAsync(input.PostId);
            if (post == null)
                throw new TaktException(L("Identity.Post.NotFound"));

            post.PostStatus = input.PostStatus;
            var result = await PostRepository.UpdateAsync(post);
            return result > 0;
        }

        /// <summary>
        /// 获取用户岗位列表
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <returns>用户岗位列表</returns>
        public async Task<List<TaktUserPostDto>> GetUserPostsAsync(long postId)
        {
            var userPosts = await UserPostRepository.GetListAsync(up => up.PostId == postId && up.IsDeleted == 0);
            return userPosts.Adapt<List<TaktUserPostDto>>();
        }

        /// <summary>
        /// 分配用户岗位
        /// </summary>
        /// <param name="postId">岗位ID</param>
        /// <param name="userIds">用户ID集合</param>
        /// <returns>是否成功</returns>
        public async Task<bool> AssignUserPostsAsync(long postId, long[] userIds)
        {
            try
            {
                _logger.Info($"开始分配用户岗位 - 岗位ID: {postId}, 用户IDs: {string.Join(",", userIds)}");

                // 1. 获取岗位现有关联的用户（包括已删除的）
                var existingUsers = await UserPostRepository.GetListAsync(up => up.PostId == postId);
                _logger.Info($"岗位现有关联用户数量: {existingUsers.Count}");

                // 2. 找出需要标记删除的关联（在现有关联中但不在新的用户列表中）
                var usersToDelete = existingUsers.Where(up => !userIds.Contains(up.UserId)).ToList();
                if (usersToDelete.Any())
                {
                    foreach (var user in usersToDelete)
                    {
                        user.IsDeleted = 1;
                        user.DeleteBy = _currentUser.UserName;
                        user.DeleteTime = DateTime.Now;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(user);
                    }
                }

                // 3. 处理需要恢复的关联（在新的用户列表中且已存在但被标记为删除）
                var usersToRestore = existingUsers.Where(up => userIds.Contains(up.UserId) && up.IsDeleted == 1).ToList();
                if (usersToRestore.Any())
                {
                    foreach (var user in usersToRestore)
                    {
                        user.IsDeleted = 0;
                        user.DeleteBy = null;
                        user.DeleteTime = null;
                        user.UpdateBy = _currentUser.UserName;
                        user.UpdateTime = DateTime.Now;
                        await UserPostRepository.UpdateAsync(user);
                    }
                }

                // 4. 找出需要新增的关联（在新的用户列表中且不存在任何记录）
                var existingUserIds = existingUsers.Select(up => up.UserId).ToList();
                var usersToAdd = userIds.Where(userId => !existingUserIds.Contains(userId))
                    .Select(userId => new TaktUserPost
                    {
                        PostId = postId,
                        UserId = userId,
                        IsDeleted = 0,
                        CreateBy = _currentUser.UserName,
                        CreateTime = DateTime.Now,
                        UpdateBy = _currentUser.UserName,
                        UpdateTime = DateTime.Now
                    }).ToList();
                if (usersToAdd.Any())
                {
                    await UserPostRepository.CreateRangeAsync(usersToAdd);
                }

                _logger.Info("用户岗位分配完成");
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error($"分配用户岗位失败: {ex.Message}", ex);
                throw;
            }
        }



        /// <summary>
        /// 导入岗位数据
        /// </summary>
        public async Task<List<TaktPostImportDto>> ImportAsync(Stream fileStream, string sheetName)
        {
            try
            {
                var postRepository = _repositoryFactory.GetAuthRepository<TaktPost>();
                var posts = await TaktExcelHelper.ImportAsync<TaktPostImportDto>(fileStream, sheetName);
                if (!posts.Any())
                    return new List<TaktPostImportDto>();

                foreach (var post in posts)
                {
                    try
                    {
                        if (string.IsNullOrEmpty(post.PostCode) || string.IsNullOrEmpty(post.PostName))
                        {
                            _logger.Warn(L("Identity.Post.Log.ImportEmptyFields"));
                            continue;
                        }

                        await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostCode", post.PostCode);
                        await TaktValidateUtils.ValidateFieldExistsAsync(PostRepository, "PostName", post.PostName);

                        var entity = post.Adapt<TaktPost>();
                        entity.CreateTime = DateTime.Now;
                        entity.CreateBy = "system"; // TODO: 从当前用户上下文获取

                        await PostRepository.CreateAsync(entity);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn(L("Identity.Post.Log.ImportFailed", ex.Message));
                    }
                }

                return posts;
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Post.Log.ImportDataFailed"), ex);
                throw new TaktException(L("Identity.Post.ImportFailed"));
            }
        }

        /// <summary>
        /// 生成岗位导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public async Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName)
        {
            return await TaktExcelHelper.GenerateTemplateAsync<TaktPostTemplateDto>(sheetName);
        }

        /// <summary>
        /// 导出岗位数据
        /// </summary>
        public async Task<(string fileName, byte[] content)> ExportAsync(TaktPostQueryDto query, string sheetName)
        {
            try
            {
                var postRepository = _repositoryFactory.GetAuthRepository<TaktPost>();
                var list = await postRepository.GetListAsync(QueryExpression(query));
                var exportList = list.Adapt<List<TaktPostExportDto>>();
                return await TaktExcelHelper.ExportAsync(exportList, sheetName, "Post");
            }
            catch (Exception ex)
            {
                _logger.Error(L("Identity.Post.Log.ExportDataFailed"), ex);
                throw new TaktException(L("Identity.Post.ExportFailed"));
            }
        }

        /// <summary>
        /// 构建岗位查询条件
        /// </summary>
        private Expression<Func<TaktPost, bool>> QueryExpression(TaktPostQueryDto query)
        {
            var exp = Expressionable.Create<TaktPost>();

            if (!string.IsNullOrEmpty(query.PostName))
                exp.And(x => x.PostName.Contains(query.PostName));

            if (!string.IsNullOrEmpty(query.PostCode))
                exp.And(x => x.PostCode.Contains(query.PostCode));

            if (query.PostStatus.HasValue && query.PostStatus.Value != -1)
                exp.And(x => x.PostStatus == query.PostStatus.Value);

            return exp.ToExpression();
        }

    }
}




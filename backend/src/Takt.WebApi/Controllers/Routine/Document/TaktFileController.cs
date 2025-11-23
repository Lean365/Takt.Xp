//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktFileController.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 文件控制器
//===================================================================

using Takt.Application.Dtos.Routine;
using Takt.Application.Services.Routine;
using Takt.Shared.Helpers;
using Takt.Shared.Models;
using Takt.Shared.Extensions;
using Microsoft.Extensions.Options;
using System.IO;

namespace Takt.WebApi.Controllers.Routine
{
    /// <summary>
    /// 文件控制器
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    [Route("api/[controller]", Name = "文件管理")]
    [ApiController]
    [ApiModule("routine", "日常事务")]
    public class TaktFileController : TaktBaseController
    {
        private readonly ITaktFileService _fileService;
        private readonly FileUploadOptions _fileUploadOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileService">文件服务</param>
        /// <param name="fileUploadOptions">文件上传配置选项</param>
        /// <param name="logger">日志服务</param>
        /// <param name="currentUser">当前用户服务</param>
        /// <param name="localization">本地化服务</param>
        public TaktFileController(
            ITaktFileService fileService,
            IOptions<FileUploadOptions> fileUploadOptions,
            ITaktLogger logger,
            ITaktCurrentUser currentUser,
            ITaktLocalizationService localization) : base(logger, currentUser, localization)
        {
            _fileService = fileService;
            _fileUploadOptions = fileUploadOptions.Value;
        }

        /// <summary>
        /// 获取文件分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>文件分页列表</returns>
        [HttpGet("list")]
        [TaktPerm("routine:document:file:list")]
        public async Task<IActionResult> GetListAsync([FromQuery] TaktFileQueryDto query)
        {
            var result = await _fileService.GetListAsync(query);
            return Success(result);
        }

        /// <summary>
        /// 获取文件详情
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件详情</returns>
        [HttpGet("{fileId}")]
        [TaktPerm("routine:document:file:query")]
        public async Task<IActionResult> GetByIdAsync(long fileId)
        {
            var result = await _fileService.GetByIdAsync(fileId);
            return Success(result);
        }

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>文件ID</returns>
        [HttpPost]
        [TaktPerm("routine:document:file:create")]
        public async Task<IActionResult> CreateAsync([FromBody] TaktFileCreateDto input)
        {
            var result = await _fileService.CreateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 更新文件
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        [HttpPut]
        [TaktPerm("routine:document:file:update")]
        public async Task<IActionResult> UpdateAsync([FromBody] TaktFileUpdateDto input)
        {
            var result = await _fileService.UpdateAsync(input);
            return Success(result);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>是否成功</returns>
        [HttpDelete("{fileId}")]
        [TaktPerm("routine:document:file:delete")]
        public async Task<IActionResult> DeleteAsync(long fileId)
        {
            try
            {
                // 先获取文件信息，用于删除磁盘文件
                var fileInfo = await _fileService.GetByIdAsync(fileId);
                if (fileInfo == null)
                {
                    return Error("文件不存在");
                }

                // 删除数据库记录
                var result = await _fileService.DeleteAsync(fileId);
                if (!result)
                {
                    return Error("删除数据库记录失败");
                }

                // 删除磁盘上的文件
                if (!string.IsNullOrEmpty(fileInfo.FilePath))
                {
                    var deleteResult = TaktFileHelper.DeleteFile(fileInfo.FilePath);
                    if (deleteResult)
                    {
                        _logger.Info($"磁盘文件删除成功: {fileInfo.FilePath}");
                    }
                    else
                    {
                        _logger.Warn($"磁盘文件删除失败或文件不存在: {fileInfo.FilePath}");
                        // 磁盘文件删除失败不影响数据库删除的成功
                    }
                }

                _logger.Info($"文件删除成功: ID={fileId}, 路径={fileInfo.FilePath}");
                return Success(true);
            }
            catch (Exception ex)
            {
                _logger.Error($"删除文件失败: ID={fileId}, 错误: {ex.Message}");
                return Error($"删除文件失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 批量删除文件
        /// </summary>
        /// <param name="fileIds">文件ID集合</param>
        /// <returns>是否成功</returns>
        [HttpDelete("batch")]
        [TaktPerm("routine:document:file:delete")]
        public async Task<IActionResult> BatchDeleteAsync([FromBody] long[] fileIds)
        {
            if (fileIds == null || fileIds.Length == 0)
            {
                return Error("文件ID列表不能为空");
            }

            try
            {
                var successCount = 0;
                var failCount = 0;
                var deletedFiles = new List<string>();

                foreach (var fileId in fileIds)
                {
                    try
                    {
                        // 先获取文件信息
                        var fileInfo = await _fileService.GetByIdAsync(fileId);
                        if (fileInfo == null)
                        {
                            failCount++;
                            continue;
                        }

                        // 删除数据库记录
                        var result = await _fileService.DeleteAsync(fileId);
                        if (result)
                        {
                            successCount++;
                            deletedFiles.Add(fileInfo.FilePath);

                            // 删除磁盘上的文件
                            if (!string.IsNullOrEmpty(fileInfo.FilePath))
                            {
                                var deleteResult = TaktFileHelper.DeleteFile(fileInfo.FilePath);
                                if (deleteResult)
                                {
                                    _logger.Info($"磁盘文件删除成功: {fileInfo.FilePath}");
                                }
                                else
                                {
                                    _logger.Warn($"磁盘文件删除失败或文件不存在: {fileInfo.FilePath}");
                                    // 磁盘文件删除失败不影响数据库删除的成功
                                }
                            }
                        }
                        else
                        {
                            failCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.Error($"删除文件失败: ID={fileId}, 错误: {ex.Message}");
                        failCount++;
                    }
                }

                _logger.Info($"批量删除文件完成: 成功={successCount}, 失败={failCount}");
                
                return Success(new
                {
                    totalCount = fileIds.Length,
                    successCount = successCount,
                    failCount = failCount,
                    deletedFiles = deletedFiles
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"批量删除文件失败: 错误: {ex.Message}");
                return Error($"批量删除文件失败: {ex.Message}");
            }
        }

        /// <summary>
        /// 导入文件数据
        /// </summary>
        /// <param name="file">Excel文件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        [HttpPost("import")]
        [TaktPerm("routine:document:file:import")]
        public async Task<IActionResult> ImportAsync(IFormFile file, [FromQuery] string sheetName = "File")
        {
            using var stream = file.OpenReadStream();
            var result = await _fileService.ImportAsync(stream, sheetName);
            return Success(result);
        }

        /// <summary>
        /// 导出文件数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导出的Excel文件</returns>
        [HttpGet("export")]
        [TaktPerm("routine:document:file:export")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> ExportAsync([FromQuery] TaktFileQueryDto query, [FromQuery] string sheetName = "File")
        {
            var result = await _fileService.ExportAsync(query, sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel模板文件</returns>
        [HttpGet("template")]
        [TaktPerm("routine:document:file:template")]
        [ProducesResponseType(typeof(byte[]), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTemplateAsync([FromQuery] string sheetName = "File")
        {
            var result = await _fileService.GetTemplateAsync(sheetName);
            return File(result.content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.fileName);
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input">文件创建DTO</param>
        /// <returns>上传结果</returns>
        [HttpPost("upload")]
        [TaktPerm("routine:document:file:upload")]
        public async Task<IActionResult> UploadAsync([FromForm] TaktFileCreateDto input)
        {
            var file = Request.Form.Files.FirstOrDefault();
            if (file == null)
            {
                return Error("未找到上传的文件");
            }

            // 使用扩展方法验证文件
            var (isValid, errorMessage) = file.ValidateFile(_fileUploadOptions, "Normal");
            if (!isValid)
            {
                return Error(errorMessage);
            }

            // 构建保存路径
            var savePath = string.IsNullOrEmpty(input.FilePath) ? "uploads/files" : input.FilePath;
            
            // 使用通用方法上传文件，传入用户的备注和排序
            return await UploadFileInternalAsync(file, savePath, null, input.Remark ?? "普通文件", input.OrderNum);
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="fileId">文件ID</param>
        /// <returns>文件流</returns>
        [HttpGet("download/{fileId}")]
        [TaktPerm("routine:document:file:download")]
        public async Task<IActionResult> DownloadAsync(long fileId)
        {
            var result = await _fileService.GetByIdAsync(fileId);
            if (result == null)
            {
                return NotFound("文件不存在");
            }
            
            // TODO: Implement actual file download logic
            // For now, return file info as JSON
            return Success(result);
        }

        /// <summary>
        /// 上传头像
        /// </summary>
        /// <param name="file">头像文件</param>
        /// <returns>上传结果</returns>
        [HttpPost("upload-avatar")]
        [TaktPerm("routine:document:file:upload")]
        public async Task<IActionResult> UploadAvatarAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return Error("未找到上传的文件");
            }

            // 使用扩展方法验证头像文件
            var (isValid, errorMessage) = file.ValidateFile(_fileUploadOptions, "Avatar");
            if (!isValid)
            {
                return Error(errorMessage);
            }

            // 使用通用方法上传文件
            return await UploadFileInternalAsync(file, "uploads/avatars", "avatar", "头像文件", 0);
        }

        /// <summary>
        /// 通用的文件上传处理逻辑
        /// </summary>
        /// <param name="file">文件对象</param>
        /// <param name="savePath">保存路径</param>
        /// <param name="fileNamePrefix">文件名前缀</param>
        /// <param name="remark">备注</param>
        /// <param name="orderNum">排序</param>
        /// <returns>上传结果</returns>
        private async Task<IActionResult> UploadFileInternalAsync(IFormFile file, string savePath, string? fileNamePrefix = null, string remark = "", int orderNum = 0)
        {
            try
            {
                // 生成文件名
                var fileName = fileNamePrefix != null 
                    ? TaktFileHelper.GenerateUniqueFileName(file.FileName, fileNamePrefix)
                    : TaktFileHelper.GenerateUniqueFileName(file.FileName);

                // 构建完整保存路径
                var fullSavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", savePath);
                var filePath = Path.Combine(fullSavePath, fileName);

                // 保存文件到磁盘
                using var stream = file.OpenReadStream();
                if (!await TaktFileHelper.SaveFileAsync(stream, filePath))
                {
                    return Error("文件保存失败");
                }

                // 构建访问URL
                var fileUrl = $"/{savePath.Replace('\\', '/')}/{fileName}";

                // 创建文件记录DTO
                var fileCreateDto = new TaktFileCreateDto
                {
                    FileOriginalName = file.FileName,
                    FileExtension = Path.GetExtension(file.FileName),
                    FileName = fileName,
                    FilePath = filePath,
                    FileType = file.ContentType,
                    FileSize = file.Length,
                    FileStorageType = 0, // 本地存储
                    FileStorageLocation = fullSavePath,
                    FileAccessUrl = fileUrl,
                    Status = 0, // 启用状态
                    OrderNum = orderNum,
                    Remark = remark
                };

                // 保存到数据库
                var fileId = await _fileService.CreateAsync(fileCreateDto);

                _logger.Info($"文件上传成功: {fileName}, 路径: {filePath}, URL: {fileUrl}, 数据库ID: {fileId}");
                
                return Success(new
                {
                    fileId = fileId,
                    url = fileUrl,
                    fileName = fileName,
                    originalName = file.FileName,
                    fileSize = file.Length
                });
            }
            catch (Exception ex)
            {
                _logger.Error($"文件上传失败: {ex.Message}");
                return Error($"文件上传失败: {ex.Message}");
            }
        }
    }
}






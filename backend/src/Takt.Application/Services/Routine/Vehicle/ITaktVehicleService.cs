//===================================================================
// 项目名 : Takt.Xp
// 文件名 : ITaktVehicleService.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-03-07 16:30
// 版本号 : V0.0.1
// 描述   : 用车服务接口
//===================================================================

namespace Takt.Application.Services.Routine.Vehicle
{
    /// <summary>
    /// 用车服务接口
    /// </summary>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-03-07
    /// </remarks>
    public interface ITaktVehicleService
    {
        /// <summary>
        /// 获取用车分页列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>用车分页列表</returns>
        Task<TaktPagedResult<TaktVehicleDto>> GetListAsync(TaktVehicleQueryDto query);

        /// <summary>
        /// 获取用车详情
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>用车详情</returns>
        Task<TaktVehicleDto> GetByIdAsync(long vehicleId);

        /// <summary>
        /// 创建用车
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>用车ID</returns>
        Task<long> CreateAsync(TaktVehicleCreateDto input);

        /// <summary>
        /// 更新用车
        /// </summary>
        /// <param name="input">更新对象</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateAsync(TaktVehicleUpdateDto input);

        /// <summary>
        /// 删除用车
        /// </summary>
        /// <param name="vehicleId">用车ID</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteAsync(long vehicleId);

        /// <summary>
        /// 批量删除用车
        /// </summary>
        /// <param name="vehicleIds">用车ID集合</param>
        /// <returns>是否成功</returns>
        Task<bool> BatchDeleteAsync(long[] vehicleIds);

        /// <summary>
        /// 导入用车数据
        /// </summary>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>导入结果</returns>
        Task<(int success, int fail)> ImportAsync(Stream fileStream, string sheetName = "用车信息");

        /// <summary>
        /// 导出用车数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> ExportAsync(TaktVehicleQueryDto query, string sheetName = "用车信息");

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <param name="sheetName">工作表名称</param>
        /// <returns>Excel文件字节数组</returns>
        Task<(string fileName, byte[] content)> GetTemplateAsync(string sheetName = "用车信息");
    }
}




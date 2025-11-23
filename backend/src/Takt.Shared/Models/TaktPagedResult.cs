//===================================================================
// 项目名 : Takt.Xp 
// 文件名 : TaktPagedResult.cs 
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-01-16 21:35
// 版本号 : V.0.0.1
// 描述    : 分页结果基类
//===================================================================

using System.Collections.Generic;

namespace Takt.Shared.Models
{
    /// <summary>
    /// 分页结果基类
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <remarks>
    /// 创建者:Takt(Claude AI)
    /// 创建时间: 2024-01-16
    /// </remarks>
    public class TaktPagedResult<T>
    {
        /// <summary>
        /// 总记录数
        /// </summary>
        public long TotalNum { get; set; }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPage 
        { 
            get 
            {
                if (PageSize == 0) return 0;
                return (int)((TotalNum + PageSize - 1) / PageSize);
            }
        }

        /// <summary>
        /// 数据列表
        /// </summary>
        public List<T> Rows { get; set; } = new();
    }
} 





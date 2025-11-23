using System;

namespace Takt.Shared.Options
{
    /// <summary>
    /// Excel配置选项
    /// </summary>
    public class TaktExcelOptions
    {
        #region 基本属性
        /// <summary>
        /// 作者
        /// </summary>
        public required string Author { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        public required string Title { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public required string Subject { get; set; }

        /// <summary>
        /// 分类
        /// </summary>
        public required string Category { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public required string Keywords { get; set; }

        /// <summary>
        /// 注释
        /// </summary>
        public required string Comments { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public required string Status { get; set; }
        #endregion

        #region 应用程序相关
        /// <summary>
        /// 创建应用程序
        /// </summary>
        public required string Application { get; set; }

        /// <summary>
        /// 公司名称
        /// </summary>
        public required string Company { get; set; }

        /// <summary>
        /// 管理者
        /// </summary>
        public required string Manager { get; set; }
        #endregion
    }
} 


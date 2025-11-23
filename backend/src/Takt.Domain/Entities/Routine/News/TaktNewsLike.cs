#nullable enable


using Takt.Domain.Entities.Identity;
using SqlSugar;

//===================================================================
// 项目名 : Takt.Domain.Entities.Routine
// 文件名 : TaktNewsLike.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 新闻点赞实体
//===================================================================

namespace Takt.Domain.Entities.Routine.News
{
    /// <summary>
    /// 新闻点赞实体
    /// </summary>
    [SugarTable("Takt_routine_news_like", "新闻点赞")]
    public class TaktNewsLike : TaktBaseEntity
    {
        /// <summary>
        /// 新闻ID
        /// </summary>
        [SugarColumn(ColumnName = "news_id", ColumnDescription = "新闻ID", IsNullable = false, ColumnDataType = "bigint")]
        public long NewsId { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", IsNullable = false, ColumnDataType = "bigint")]
        public long UserId { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户姓名", Length = 50, IsNullable = false, DefaultValue = "", ColumnDataType = "nvarchar")]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 用户头像
        /// </summary>
        [SugarColumn(ColumnName = "user_avatar", ColumnDescription = "用户头像", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? UserAvatar { get; set; }

        /// <summary>
        /// 点赞类型。1=点赞，2=取消点赞。
        /// </summary>
        [SugarColumn(ColumnName = "like_type", ColumnDescription = "点赞类型", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
        public int LikeType { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? IpAddress { get; set; }

        /// <summary>
        /// 用户代理
        /// </summary>
        [SugarColumn(ColumnName = "user_agent", ColumnDescription = "用户代理", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
        public string? UserAgent { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", ColumnDataType = "int", IsNullable = false, DefaultValue = "0")]
        public int OrderNum { get; set; } = 0;

        /// <summary>
        /// 关联的新闻
        /// </summary>
        [Navigate(NavigateType.OneToOne, nameof(NewsId))]
        public TaktNews? News { get; set; }
    }
} 

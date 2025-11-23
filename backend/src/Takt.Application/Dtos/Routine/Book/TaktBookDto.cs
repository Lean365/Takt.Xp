//===================================================================
// 项目名 : Takt.Application
// 文件名 : TaktBookDto.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述   : 图书数据传输对象
//===================================================================

using System.ComponentModel.DataAnnotations;

namespace Takt.Application.Dtos.Routine.Book
{
    /// <summary>
    /// 图书基础DTO
    /// </summary>
    public class TaktBookDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktBookDto()
        {
            CompanyCode = string.Empty;
            BookCode = string.Empty;
            BookName = string.Empty;
            CreateTime = DateTime.Now;
        }

        /// <summary>
        /// ID
        /// </summary>
        [AdaptMember("Id")]
        public long BookId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; }

        /// <summary>
        /// 图书编号
        /// </summary>
        public string BookCode { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        /// 图书分类ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int? Pages { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 可借数量
        /// </summary>
        public int AvailableQuantity { get; set; }

        /// <summary>
        /// 已借数量
        /// </summary>
        public int BorrowedQuantity { get; set; }

        /// <summary>
        /// 损坏数量
        /// </summary>
        public int DamagedQuantity { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StorageLocation { get; set; }

        /// <summary>
        /// 图书简介
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用 2=维护中)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string? CreateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public string? UpdateBy { get; set; }
    }

    /// <summary>
    /// 图书查询DTO
    /// </summary>
    public class TaktBookQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        public string? CompanyCode { get; set; }

        /// <summary>
        /// 图书编号
        /// </summary>
        public string? BookCode { get; set; }

        /// <summary>
        /// 书名
        /// </summary>
        public string? BookName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        /// 图书分类ID
        /// </summary>
        public long? CategoryId { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 图书创建DTO
    /// </summary>
    public class TaktBookCreateDto
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        [Required(ErrorMessage = "公司代码不能为空")]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 图书编号
        /// </summary>
        [Required(ErrorMessage = "图书编号不能为空")]
        public string BookCode { get; set; } = string.Empty;

        /// <summary>
        /// 书名
        /// </summary>
        [Required(ErrorMessage = "书名不能为空")]
        public string BookName { get; set; } = string.Empty;

        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        /// 图书分类ID
        /// </summary>
        [Required(ErrorMessage = "图书分类不能为空")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int? Pages { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StorageLocation { get; set; }

        /// <summary>
        /// 图书简介
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用 2=维护中)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 图书更新DTO
    /// </summary>
    public class TaktBookUpdateDto
    {
        /// <summary>
        /// 图书ID
        /// </summary>
        [Required(ErrorMessage = "图书ID不能为空")]
        public long BookId { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        [Required(ErrorMessage = "公司代码不能为空")]
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 图书编号
        /// </summary>
        [Required(ErrorMessage = "图书编号不能为空")]
        public string BookCode { get; set; } = string.Empty;

        /// <summary>
        /// 书名
        /// </summary>
        [Required(ErrorMessage = "书名不能为空")]
        public string BookName { get; set; } = string.Empty;

        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        /// 图书分类ID
        /// </summary>
        [Required(ErrorMessage = "图书分类不能为空")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int? Pages { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StorageLocation { get; set; }

        /// <summary>
        /// 图书简介
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用 2=维护中)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 图书导入DTO
    /// </summary>
    public class TaktBookImportDto
    {
        /// <summary>
        /// 公司代码
        /// </summary>
        public string CompanyCode { get; set; } = string.Empty;

        /// <summary>
        /// 图书编号
        /// </summary>
        public string BookCode { get; set; } = string.Empty;

        /// <summary>
        /// 书名
        /// </summary>
        public string BookName { get; set; } = string.Empty;

        /// <summary>
        /// 作者
        /// </summary>
        public string? Author { get; set; }

        /// <summary>
        /// ISBN
        /// </summary>
        public string? Isbn { get; set; }

        /// <summary>
        /// 图书分类ID
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// 出版社
        /// </summary>
        public string? Publisher { get; set; }

        /// <summary>
        /// 出版日期
        /// </summary>
        public DateTime? PublishDate { get; set; }

        /// <summary>
        /// 版次
        /// </summary>
        public string? Edition { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int? Pages { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalQuantity { get; set; }

        /// <summary>
        /// 存放位置
        /// </summary>
        public string? StorageLocation { get; set; }

        /// <summary>
        /// 图书简介
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// 封面图片
        /// </summary>
        public string? CoverImage { get; set; }

        /// <summary>
        /// 关键词
        /// </summary>
        public string? Keywords { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public int OrderNum { get; set; }

        /// <summary>
        /// 状态(0=正常 1=停用 2=维护中)
        /// </summary>
        public int Status { get; set; }
    }
}




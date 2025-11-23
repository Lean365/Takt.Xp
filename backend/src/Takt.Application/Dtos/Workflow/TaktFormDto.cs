//===================================================================
// 项目名: Takt.Application
// 文件名: TaktFormDto.cs
// 创建者: Claude
// 创建时间: 2024-12-01
// 版本号: V0.0.1
// 描述: 表单数据传输对象
//===================================================================

using System;
using System.Collections.Generic;

namespace Takt.Application.Dtos.Workflow
{
    /// <summary>
    /// 表单基础DTO（与TaktForm实体字段严格对应）
    /// </summary>
    public class TaktFormDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
            CreateBy = string.Empty;
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体表名
        /// </summary>
        public string? BusinessTableName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建者
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新者
        /// </summary>
        public string? UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否删除（0未删除 1已删除）
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 删除者
        /// </summary>
        public string? DeleteBy { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DeleteTime { get; set; }
    }

    /// <summary>
    /// 表单查询DTO
    /// </summary>
    public class TaktFormQueryDto : TaktPagedQuery
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormQueryDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string? FormKey { get; set; }

        /// <summary>
        /// 表单名称
        /// </summary>
        public string? FormName { get; set; }

        /// <summary>
        /// 表单分类
        /// </summary>
        public int? FormCategory { get; set; }

        /// <summary>
        /// 表单类型
        /// </summary>
        public int? FormType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }
    }

    /// <summary>
    /// 表单创建DTO
    /// </summary>
    public class TaktFormCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormCreateDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体表名
        /// </summary>
        public string? BusinessTableName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 表单更新DTO
    /// </summary>
    public class TaktFormUpdateDto : TaktFormCreateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormUpdateDto() : base()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FormId { get; set; }
    }

    /// <summary>
    /// 表单状态DTO
    /// </summary>
    public class TaktFormStatusDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormStatusDto()
        {
        }

        /// <summary>
        /// 主键ID
        /// </summary>
        [AdaptMember("Id")]
        [JsonConverter(typeof(ValueToStringConverter))]
        public long FormId { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已作废)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 表单模板DTO
    /// </summary>
    public class TaktFormTemplateDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormTemplateDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体表名
        /// </summary>
        public string? BusinessTableName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 表单导入DTO
    /// </summary>
    public class TaktFormImportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormImportDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            Version = "1.0";
            FormConfig = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类(1:人事类 2:财务类 3:采购类 4:合同类 5:其他)
        /// </summary>
        public int FormCategory { get; set; }

        /// <summary>
        /// 表单类型(1:请假申请 2:报销申请 3:采购申请 4:合同审批 5:其他)
        /// </summary>
        public int FormType { get; set; }

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = "1.0";

        /// <summary>
        /// 表单配置(JSON格式)
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体表名
        /// </summary>
        public string? BusinessTableName { get; set; }

        /// <summary>
        /// 注意事项
        /// </summary>
        public string? Notes { get; set; }

        /// <summary>
        /// 状态(0:草稿 1:已发布 2:已完成 3:已停用)
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 表单导出DTO
    /// </summary>
    public class TaktFormExportDto
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TaktFormExportDto()
        {
            FormKey = string.Empty;
            FormName = string.Empty;
            FormCategory = string.Empty;
            FormType = string.Empty;
            Version = string.Empty;
            FormConfig = string.Empty;
            BusinessTableName = string.Empty;
            Notes = string.Empty;
            Status = string.Empty;
        }

        /// <summary>
        /// 表单键
        /// </summary>
        public string FormKey { get; set; } = string.Empty;

        /// <summary>
        /// 表单名称
        /// </summary>
        public string FormName { get; set; } = string.Empty;

        /// <summary>
        /// 表单分类
        /// </summary>
        public string FormCategory { get; set; } = string.Empty;

        /// <summary>
        /// 表单类型
        /// </summary>
        public string FormType { get; set; } = string.Empty;

        /// <summary>
        /// 表单版本
        /// </summary>
        public string Version { get; set; } = string.Empty;

        /// <summary>
        /// 表单配置
        /// </summary>
        public string FormConfig { get; set; } = string.Empty;

        /// <summary>
        /// 业务实体表名
        /// </summary>
        public string BusinessTableName { get; set; } = string.Empty;

        /// <summary>
        /// 注意事项
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}

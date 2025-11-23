//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedFileDictData.cs
// 创建者 : Takt(Claude AI)
// 创建时间: 2024-04-28
// 版本号 : V0.0.1
// 描述   : 文件相关字典数据种子数据初始化类
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 文件相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedFileDictData
{
    /// <summary>
    /// 获取文件相关字典数据
    /// </summary>
    /// <returns>文件相关字典数据列表</returns>
    public List<TaktDictData> GetFileDictData()
    {
        return new List<TaktDictData>
        {
            // 文件路径
            new TaktDictData { DictType = "file_path", DictLabel = "图片目录", DictValue = "uploads/images",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "图片上传目录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_path", DictLabel = "文件目录", DictValue = "uploads/files",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "通用文件上传目录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_path", DictLabel = "文档目录", DictValue = "uploads/documents",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "文档上传目录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_path", DictLabel = "视频目录", DictValue = "uploads/videos",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "视频上传目录", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 存储位置
            new TaktDictData { DictType = "file_storage_location", DictLabel = "默认存储位置", DictValue = "default",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "默认存储位置", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_storage_location", DictLabel = "备用存储位置1", DictValue = "backup1",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "备用存储位置1", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_storage_location", DictLabel = "备用存储位置2", DictValue = "backup2",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "备用存储位置2", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 存储类型
            new TaktDictData { DictType = "file_storage_type", DictLabel = "本地存储", DictValue = "local",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "本地存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_storage_type", DictLabel = "阿里云OSS", DictValue = "aliyun",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "阿里云对象存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_storage_type", DictLabel = "腾讯云COS", DictValue = "tencent",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "腾讯云对象存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_storage_type", DictLabel = "七牛云存储", DictValue = "qiniu",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "七牛云对象存储", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            //文件命名规则
            new TaktDictData { DictType = "file_name_rule", DictLabel = "原文件名", DictValue = "original",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "原文件名", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_name_rule", DictLabel = "随机文件名", DictValue = "random",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "随机文件名", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_name_rule", DictLabel = "时间戳文件名", DictValue = "timestamp",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "时间戳文件名", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件分类
            new TaktDictData { DictType = "file_category", DictLabel = "图片文件", DictValue = "image",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "图片文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_category", DictLabel = "文档文件", DictValue = "document",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "文档文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_category", DictLabel = "视频文件", DictValue = "video",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "视频文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_category", DictLabel = "音频文件", DictValue = "audio",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "音频文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_category", DictLabel = "压缩文件", DictValue = "archive",CssClass=5,ListClass=5, OrderNum = 5,   Remark = "压缩文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_category", DictLabel = "其他文件", DictValue = "other",CssClass=6,ListClass=6, OrderNum = 6,   Remark = "其他文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件状态
            new TaktDictData { DictType = "file_status", DictLabel = "正常", DictValue = "normal",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "正常状态", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_status", DictLabel = "已删除", DictValue = "deleted",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "已删除", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_status", DictLabel = "已锁定", DictValue = "locked",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "已锁定", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_status", DictLabel = "已损坏", DictValue = "corrupted",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "已损坏", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件权限
            new TaktDictData { DictType = "file_permission", DictLabel = "只读", DictValue = "readonly",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "只读权限", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_permission", DictLabel = "读写", DictValue = "readwrite",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "读写权限", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_permission", DictLabel = "完全控制", DictValue = "fullcontrol",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "完全控制权限", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_permission", DictLabel = "无权限", DictValue = "noaccess",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "无权限", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件版本
            new TaktDictData { DictType = "file_version", DictLabel = "当前版本", DictValue = "current",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "当前版本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_version", DictLabel = "历史版本", DictValue = "history",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "历史版本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_version", DictLabel = "草稿版本", DictValue = "draft",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "草稿版本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_version", DictLabel = "发布版本", DictValue = "release",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "发布版本", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件标签
            new TaktDictData { DictType = "file_tag", DictLabel = "重要", DictValue = "important",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "重要文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_tag", DictLabel = "机密", DictValue = "confidential",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "机密文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_tag", DictLabel = "临时", DictValue = "temporary",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "临时文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_tag", DictLabel = "归档", DictValue = "archived",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "归档文件", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 文件审核
            new TaktDictData { DictType = "file_audit", DictLabel = "待审核", DictValue = "pending",CssClass=1,ListClass=1, OrderNum = 1,   Remark = "待审核", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_audit", DictLabel = "审核通过", DictValue = "approved",CssClass=2,ListClass=2, OrderNum = 2,   Remark = "审核通过", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_audit", DictLabel = "审核拒绝", DictValue = "rejected",CssClass=3,ListClass=3, OrderNum = 3,   Remark = "审核拒绝", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "file_audit", DictLabel = "无需审核", DictValue = "noaudit",CssClass=4,ListClass=4, OrderNum = 4,   Remark = "无需审核", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}




//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedQualityDictData.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 质量相关字典数据种子数据初始化类 - 横排格式
//===================================================================

using Takt.Domain.Entities.Routine.DataDictionary;

namespace Takt.Infrastructure.Data.Seeds.Biz.Dict;

/// <summary>
/// 质量相关字典数据种子数据初始化类
/// </summary>
public class TaktDbSeedQualityDictData
{
    /// <summary>
    /// 获取质量相关字典数据
    /// </summary>
    /// <returns>质量相关字典数据列表</returns>
    public List<TaktDictData> GetQualityDictData()
    {
        return new List<TaktDictData>
        {
            // 质量等级 - 横排格式
            new TaktDictData { DictType = "qm_quality_grade", DictLabel = "特级", DictValue = "GRADE_A", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "特级质量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_grade", DictLabel = "一级", DictValue = "GRADE_B", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "一级质量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_grade", DictLabel = "二级", DictValue = "GRADE_C", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "二级质量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_grade", DictLabel = "三级", DictValue = "GRADE_D", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "三级质量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_grade", DictLabel = "不合格", DictValue = "UNQUALIFIED", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "不合格", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 检验类型 - 横排格式
            new TaktDictData { DictType = "qm_inspection_type", DictLabel = "进货检验", DictValue = "IQC", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "Incoming Quality Control - 对采购的原材料、零部件进行的入库前检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_type", DictLabel = "过程检验", DictValue = "IPQC", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "In-Process Quality Control - 在产品生产或加工过程中进行的检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_type", DictLabel = "最终检验", DictValue = "FQC", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "Final Quality Control - 对完工产品进行的全面检验，决定是否可出厂", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_type", DictLabel = "出货检验", DictValue = "OQC", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "Outgoing Quality Control - 对出货产品进行的最终检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 检验方法 - 横排格式
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "全数检验", DictValue = "100_PERCENT", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "100% Inspection - 对每一件产品进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "抽样检验", DictValue = "SAMPLING", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "Sampling Inspection - 从一批产品中抽取少量样本进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "计数检验", DictValue = "ATTRIBUTES", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "Inspection by Attributes - 判断产品合格或不合格，计数缺陷个数", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "计量检验", DictValue = "VARIABLES", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "Inspection by Variables - 测量产品的具体质量特性数值", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "破坏性检验", DictValue = "DESTRUCTIVE", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "Destructive Testing - 检验会导致产品丧失使用价值", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "非破坏性检验", DictValue = "NON_DESTRUCTIVE", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "Non-Destructive Testing (NDT) - 检验后产品功能完好", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "感官检验", DictValue = "SENSORY", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "Sensory Inspection - 依靠人的感觉器官进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "物理检验", DictValue = "PHYSICAL", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "Physical Test - 通过物理手段进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_method", DictLabel = "化学检验", DictValue = "CHEMICAL", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "Chemical Test - 通过化学手段进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 检验手段 - 横排格式
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "目视检验", DictValue = "VISUAL", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "Visual Inspection - 通过肉眼观察进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "测量工具", DictValue = "MEASURING", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "Measuring Tools - 使用量具、卡尺、千分尺等测量工具", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "检测设备", DictValue = "TESTING_EQUIPMENT", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "Testing Equipment - 使用专业检测设备进行检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "X射线检测", DictValue = "X_RAY", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "X-Ray Inspection - 使用X射线进行内部缺陷检测", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "超声波检测", DictValue = "ULTRASONIC", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "Ultrasonic Testing - 使用超声波进行无损检测", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "磁粉检测", DictValue = "MAGNETIC", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "Magnetic Particle Testing - 使用磁粉检测表面和近表面缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "渗透检测", DictValue = "PENETRANT", OrderNum = 7,  CssClass = 7, ListClass = 7, Remark = "Penetrant Testing - 使用渗透剂检测表面开口缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "涡流检测", DictValue = "EDDY_CURRENT", OrderNum = 8,  CssClass = 8, ListClass = 8, Remark = "Eddy Current Testing - 使用涡流检测导电材料缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "红外检测", DictValue = "INFRARED", OrderNum = 9,  CssClass = 9, ListClass = 9, Remark = "Infrared Testing - 使用红外技术进行温度检测", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "激光检测", DictValue = "LASER", OrderNum = 10,  CssClass = 10, ListClass = 10, Remark = "Laser Testing - 使用激光技术进行精密测量", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "计算机视觉", DictValue = "COMPUTER_VISION", OrderNum = 11,  CssClass = 11, ListClass = 11, Remark = "Computer Vision - 使用计算机视觉技术进行自动检测", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_means", DictLabel = "人工检验", DictValue = "MANUAL", OrderNum = 12,  CssClass = 12, ListClass = 12, Remark = "Manual Inspection - 依靠检验员的人工判断", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 检验结果 - 横排格式
            new TaktDictData { DictType = "qm_inspection_result", DictLabel = "合格", DictValue = "PASS", OrderNum = 1,  CssClass = 2, ListClass = 2, Remark = "检验合格", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_result", DictLabel = "不合格", DictValue = "FAIL", OrderNum = 2,  CssClass = 5, ListClass = 5, Remark = "检验不合格", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_result", DictLabel = "待定", DictValue = "PENDING", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "检验待定", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_inspection_result", DictLabel = "特采", DictValue = "SPECIAL_ACCEPT", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "特采接收", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 缺陷类型 - 横排格式
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "外观缺陷", DictValue = "COSMETIC", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "Cosmetic Defect - 仅影响产品的外观、颜色、纹理等，通常不影响功能", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "功能缺陷", DictValue = "FUNCTIONAL", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "Functional Defect - 产品无法执行其预期的功能或性能不达标", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "尺寸缺陷", DictValue = "DIMENSIONAL", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "Dimensional Defect - 产品的尺寸、形状、位置等不符合图纸或规格要求", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "材料缺陷", DictValue = "MATERIAL", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "Material Defect - 使用了错误的材料，或材料本身存在质量问题", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "装配缺陷", DictValue = "ASSEMBLY", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "Assembly/Workmanship Defect - 在制造或装配过程中因操作不当导致的问题", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_type", DictLabel = "包装缺陷", DictValue = "PACKAGING", OrderNum = 6,  CssClass = 6, ListClass = 6, Remark = "Packaging Defect - 产品包装不符合要求或存在质量问题", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 缺陷等级 - 横排格式
            new TaktDictData { DictType = "qm_defect_level", DictLabel = "轻微", DictValue = "MINOR", OrderNum = 1,  CssClass = 3, ListClass = 3, Remark = "轻微缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_level", DictLabel = "一般", DictValue = "MAJOR", OrderNum = 2,  CssClass = 4, ListClass = 4, Remark = "一般缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_level", DictLabel = "严重", DictValue = "CRITICAL", OrderNum = 3,  CssClass = 5, ListClass = 5, Remark = "严重缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_defect_level", DictLabel = "致命", DictValue = "FATAL", OrderNum = 4,  CssClass = 6, ListClass = 6, Remark = "致命缺陷", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 质量状态 - 横排格式
            new TaktDictData { DictType = "qm_quality_status", DictLabel = "待检验", DictValue = "PENDING", OrderNum = 1,  CssClass = 3, ListClass = 3, Remark = "待检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_status", DictLabel = "检验中", DictValue = "INSPECTING", OrderNum = 2,  CssClass = 1, ListClass = 1, Remark = "检验中", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_status", DictLabel = "已检验", DictValue = "INSPECTED", OrderNum = 3,  CssClass = 2, ListClass = 2, Remark = "已检验", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_status", DictLabel = "已处理", DictValue = "PROCESSED", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "已处理", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },

            // 质量体系 - 横排格式
            new TaktDictData { DictType = "qm_quality_system", DictLabel = "ISO9001", DictValue = "ISO9001", OrderNum = 1,  CssClass = 1, ListClass = 1, Remark = "ISO9001质量管理体系", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_system", DictLabel = "ISO14001", DictValue = "ISO14001", OrderNum = 2,  CssClass = 2, ListClass = 2, Remark = "ISO14001环境管理体系", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_system", DictLabel = "ISO45001", DictValue = "ISO45001", OrderNum = 3,  CssClass = 3, ListClass = 3, Remark = "ISO45001职业健康安全管理体系", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_system", DictLabel = "IATF16949", DictValue = "IATF16949", OrderNum = 4,  CssClass = 4, ListClass = 4, Remark = "IATF16949汽车质量管理体系", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now },
            new TaktDictData { DictType = "qm_quality_system", DictLabel = "其他", DictValue = "OTHER", OrderNum = 5,  CssClass = 5, ListClass = 5, Remark = "其他质量体系", CreateBy = "Takt365", CreateTime = DateTime.Now, UpdateBy = "Takt365", UpdateTime = DateTime.Now }
        };
    }
}


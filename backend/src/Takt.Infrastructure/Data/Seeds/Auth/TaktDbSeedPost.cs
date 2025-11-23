//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktDbSeedPost.cs
// 创建者 : Claude
// 创建时间: 2024-02-19
// 版本号 : V0.0.1
// 描述   : 岗位数据提供类
//===================================================================

using Takt.Domain.Entities.Identity;

namespace Takt.Infrastructure.Data.Seeds;

/// <summary>
/// 岗位数据提供类
/// </summary>
public class TaktDbSeedPost
{
    /// <summary>
    /// 获取默认岗位数据
    /// </summary>
    /// <returns>岗位数据列表</returns>
    public List<TaktPost> GetDefaultPosts()
    {
        return new List<TaktPost>
        {
            new TaktPost { PostCode = "CHAIRMAN", PostName = "董事长", OrderNum = 1, PostStatus=0, Remark = "Chairman;会長" },
            new TaktPost { PostCode = "PRESIDENT", PostName = "总裁", OrderNum = 2, PostStatus=0, Remark = "President;社長" },
            new TaktPost { PostCode = "CEO", PostName = "首席执行官", OrderNum = 3, PostStatus=0, Remark = "Chief Executive Officer;最高経営責任者" },
            new TaktPost { PostCode = "COO", PostName = "首席运营官", OrderNum = 4, PostStatus=0, Remark = "Chief Operating Officer;最高執行責任者" },
            new TaktPost { PostCode = "CFO", PostName = "首席财务官", OrderNum = 5, PostStatus=0, Remark = "Chief Financial Officer;最高財務責任者" },
            new TaktPost { PostCode = "CTO", PostName = "首席技术官", OrderNum = 6, PostStatus=0, Remark = "Chief Technology Officer;最高技術責任者" },
            new TaktPost { PostCode = "GM", PostName = "总经理", OrderNum = 7, PostStatus=0, Remark = "General Manager;総経理" },
            new TaktPost { PostCode = "DEPUTY_GM", PostName = "副总经理", OrderNum = 8, PostStatus=0, Remark = "Deputy General Manager;副総経理" },
            new TaktPost { PostCode = "DIVISION_DIRECTOR", PostName = "本部长", OrderNum = 9, PostStatus=0, Remark = "Division Director;本部長" },
            new TaktPost { PostCode = "DIRECTOR", PostName = "总监", OrderNum = 10, PostStatus=0, Remark = "Director;ディレクター" },
            new TaktPost { PostCode = "DEPT_DIRECTOR", PostName = "部长", OrderNum = 11, PostStatus=0, Remark = "Department Director;部長" },
            new TaktPost { PostCode = "MANAGER", PostName = "经理", OrderNum = 12, PostStatus=0, Remark = "Manager;マネージャー" },
            new TaktPost { PostCode = "SECTION_CHIEF", PostName = "课长", OrderNum = 13, PostStatus=0, Remark = "Section Chief;課長" },
            new TaktPost { PostCode = "SUPERVISOR", PostName = "主管", OrderNum = 14, PostStatus=0, Remark = "Supervisor;主任" },
            new TaktPost { PostCode = "GROUP_LEADER", PostName = "股长", OrderNum = 15, PostStatus=0, Remark = "Group Leader;係長" },
            new TaktPost { PostCode = "TEAM_LEADER", PostName = "班组长", OrderNum = 16, PostStatus=0, Remark = "Team Leader;班長" },
            new TaktPost { PostCode = "SENIOR_ENGINEER", PostName = "高级工程师", OrderNum = 17, PostStatus=0, Remark = "Senior Engineer;上級エンジニア" },
            new TaktPost { PostCode = "ENGINEER", PostName = "工程师", OrderNum = 18, PostStatus=0, Remark = "Engineer;エンジニア" },
            new TaktPost { PostCode = "TECHNICIAN", PostName = "技术员", OrderNum = 19, PostStatus=0, Remark = "Technician;技術員" },
            new TaktPost { PostCode = "CLERK", PostName = "事务员", OrderNum = 20, PostStatus=0, Remark = "Clerk;事務員" }
        };
    }
}


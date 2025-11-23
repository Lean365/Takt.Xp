#nullable enable

//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktWorkflowConstants.cs
// 创建者 : Claude
// 创建时间: 2024-12-01
// 版本号 : V0.0.1
// 描述    : 工作流常量定义
//===================================================================

namespace Takt.Shared.Constants
{
    /// <summary>
    /// 工作流常量
    /// </summary>
    public static class TaktWorkflowConstants
    {
        #region 节点类型

        /// <summary>
        /// 开始节点
        /// </summary>
        public const string NODE_TYPE_START = "start";

        /// <summary>
        /// 审批节点
        /// </summary>
        public const string NODE_TYPE_APPROVAL = "approval";

        /// <summary>
        /// 网关开始节点
        /// </summary>
        public const string NODE_TYPE_BRANCH = "branch";

        /// <summary>
        /// 网关结束节点
        /// </summary>
        public const string NODE_TYPE_JOIN = "join";

        /// <summary>
        /// 会签节点（多实例）
        /// </summary>
        public const string NODE_TYPE_PARALLEL = "parallel";

        /// <summary>
        /// 结束节点
        /// </summary>
        public const string NODE_TYPE_END = "end";

        #endregion

        #region 审批人指定方式

        /// <summary>
        /// 所有成员
        /// </summary>
        public const string ALL_USER = "ALL_USER";

        /// <summary>
        /// 指定用户
        /// </summary>
        public const string SPECIAL_USER = "SPECIAL_USER";

        /// <summary>
        /// 指定角色
        /// </summary>
        public const string SPECIAL_ROLE = "SPECIAL_ROLE";

        /// <summary>
        /// 指定SQL
        /// </summary>
        public const string SPECIAL_SQL = "SPECIAL_SQL";

        /// <summary>
        /// 运行时指定用户
        /// </summary>
        public const string RUNTIME_SPECIAL_USER = "RUNTIME_SPECIAL_USER";

        /// <summary>
        /// 运行时指定角色
        /// </summary>
        public const string RUNTIME_SPECIAL_ROLE = "RUNTIME_SPECIAL_ROLE";

        /// <summary>
        /// 上一节点执行人的直属上级
        /// </summary>
        public const string RUNTIME_PARENT = "RUNTIME_PARENT";

        /// <summary>
        /// 连续多级直属上级
        /// </summary>
        public const string RUNTIME_MANY_PARENTS = "RUNTIME_MANY_PARENTS";

        /// <summary>
        /// 发起人的部门负责人
        /// </summary>
        public const string RUNTIME_CHAIRMAN = "RUNTIME_CHAIRMAN";

        #endregion

        #region 会签类型

        /// <summary>
        /// 顺序会签
        /// </summary>
        public const string APPROVE_TYPE_SEQUENTIAL = "sequential";

        /// <summary>
        /// 并行会签（且）
        /// </summary>
        public const string APPROVE_TYPE_PARALLEL_AND = "parallel_and";

        /// <summary>
        /// 并行会签（或）
        /// </summary>
        public const string APPROVE_TYPE_PARALLEL_OR = "parallel_or";

        #endregion

        #region 网关汇聚类型

        /// <summary>
        /// 至少一个通过
        /// </summary>
        public const string CONFLUENCE_TYPE_ONE = "one";

        /// <summary>
        /// 全部通过
        /// </summary>
        public const string CONFLUENCE_TYPE_ALL = "all";

        #endregion

        #region 驳回类型

        /// <summary>
        /// 前一步
        /// </summary>
        public const string REJECT_TYPE_PREVIOUS = "0";

        /// <summary>
        /// 第一步
        /// </summary>
        public const string REJECT_TYPE_FIRST = "1";

        /// <summary>
        /// 指定节点
        /// </summary>
        public const string REJECT_TYPE_SPECIFIED = "2";

        /// <summary>
        /// 不处理
        /// </summary>
        public const string REJECT_TYPE_NONE = "3";

        #endregion

        #region 数据权限占位符

        /// <summary>
        /// 登录用户占位符
        /// </summary>
        public const string DATAPRIVILEGE_LOGINUSER = "{loginUser}";

        /// <summary>
        /// 登录角色占位符
        /// </summary>
        public const string DATAPRIVILEGE_LOGINROLE = "{loginRole}";

        /// <summary>
        /// 登录部门占位符
        /// </summary>
        public const string DATAPRIVILEGE_LOGINORG = "{loginOrg}";

        #endregion
    }
}


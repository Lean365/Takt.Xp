//===================================================================
// 项目名 : Takt.Xp
// 文件名 : TaktHumanResourceCollectionExtensions.cs
// 创建者 : Claude
// 创建时间: 2025-01-11
// 版本号 : V0.0.1
// 描述    : 人力资源服务集合扩展
//===================================================================

using Microsoft.Extensions.DependencyInjection;

namespace Takt.Infrastructure.Extensions
{
    /// <summary>
    /// 人力资源服务集合扩展
    /// </summary>
    /// <remarks>
    /// 此类用于集中管理和注册人力资源相关的所有服务，包括：
    /// 1. 组织架构管理 - 部门、岗位、职级等
    /// 2. 员工管理 - 员工信息、档案、合同等
    /// 3. 考勤管理 - 考勤记录、请假、加班等
    /// 4. 薪资管理 - 薪资结构、发放、计算等
    /// 5. 培训管理 - 培训计划、课程、评估等
    /// 6. 绩效管理 - 绩效指标、评估、面谈等
    /// 7. 招聘管理 - 招聘计划、简历、面试等
    /// 8. 福利管理 - 社保、公积金、商业保险等
    /// </remarks>
    public static class TaktHumanResourceCollectionExtensions
    {
        /// <summary>
        /// 添加人力资源服务
        /// </summary>
        /// <remarks>
        /// 注册人力资源相关的所有服务，包括：
        /// 1. 组织架构管理服务 - 部门、岗位、职级等
        /// 2. 员工管理服务 - 员工信息、档案、合同等
        /// 3. 考勤管理服务 - 考勤记录、请假、加班等
        /// 4. 薪资管理服务 - 薪资结构、发放、计算等
        /// 5. 培训管理服务 - 培训计划、课程、评估等
        /// 6. 绩效管理服务 - 绩效指标、评估、面谈等
        /// 7. 招聘管理服务 - 招聘计划、简历、面试等
        /// 8. 福利管理服务 - 社保、公积金、商业保险等
        /// </remarks>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddHumanResourceServices(this IServiceCollection services)
        {
            // 组织架构管理服务
            // services.AddScoped<ITaktDepartmentService, TaktDepartmentService>();        // 部门服务
            // services.AddScoped<ITaktPositionService, TaktPositionService>();            // 岗位服务
            // services.AddScoped<ITaktJobLevelService, TaktJobLevelService>();            // 职级服务
            // services.AddScoped<ITaktOrganizationService, TaktOrganizationService>();    // 组织架构服务

            // 员工管理服务
            // services.AddScoped<ITaktEmployeeService, TaktEmployeeService>();            // 员工服务
            // services.AddScoped<ITaktEmployeeFileService, TaktEmployeeFileService>();    // 员工档案服务
            // services.AddScoped<ITaktEmployeeContractService, TaktEmployeeContractService>(); // 员工合同服务
            // services.AddScoped<ITaktEmployeeTransferService, TaktEmployeeTransferService>(); // 员工调动服务

            // 考勤管理服务
            // services.AddScoped<ITaktAttendanceService, TaktAttendanceService>();        // 考勤服务
            // services.AddScoped<ITaktLeaveService, TaktLeaveService>();                  // 请假服务
            // services.AddScoped<ITaktOvertimeService, TaktOvertimeService>();            // 加班服务
            // services.AddScoped<ITaktWorkScheduleService, TaktWorkScheduleService>();    // 工作排班服务

            // 薪资管理服务
            // services.AddScoped<ITaktSalaryService, TaktSalaryService>();                // 薪资服务
            // services.AddScoped<ITaktSalaryStructureService, TaktSalaryStructureService>(); // 薪资结构服务
            // services.AddScoped<ITaktSalaryCalculationService, TaktSalaryCalculationService>(); // 薪资计算服务
            // services.AddScoped<ITaktSalaryPaymentService, TaktSalaryPaymentService>();  // 薪资发放服务

            // 培训管理服务
            // services.AddScoped<ITaktTrainingService, TaktTrainingService>();            // 培训服务
            // services.AddScoped<ITaktTrainingPlanService, TaktTrainingPlanService>();    // 培训计划服务
            // services.AddScoped<ITaktTrainingCourseService, TaktTrainingCourseService>(); // 培训课程服务
            // services.AddScoped<ITaktTrainingEvaluationService, TaktTrainingEvaluationService>(); // 培训评估服务

            // 绩效管理服务
            // services.AddScoped<ITaktPerformanceService, TaktPerformanceService>();      // 绩效服务
            // services.AddScoped<ITaktPerformanceIndicatorService, TaktPerformanceIndicatorService>(); // 绩效指标服务
            // services.AddScoped<ITaktPerformanceEvaluationService, TaktPerformanceEvaluationService>(); // 绩效评估服务
            // services.AddScoped<ITaktPerformanceInterviewService, TaktPerformanceInterviewService>(); // 绩效面谈服务

            // 招聘管理服务
            // services.AddScoped<ITaktRecruitmentService, TaktRecruitmentService>();      // 招聘服务
            // services.AddScoped<ITaktRecruitmentPlanService, TaktRecruitmentPlanService>(); // 招聘计划服务
            // services.AddScoped<ITaktResumeService, TaktResumeService>();                // 简历服务
            // services.AddScoped<ITaktInterviewService, TaktInterviewService>();          // 面试服务

            // 福利管理服务
            // services.AddScoped<ITaktBenefitService, TaktBenefitService>();              // 福利服务
            // services.AddScoped<ITaktSocialSecurityService, TaktSocialSecurityService>(); // 社保服务
            // services.AddScoped<ITaktHousingFundService, TaktHousingFundService>();      // 公积金服务
            // services.AddScoped<ITaktCommercialInsuranceService, TaktCommercialInsuranceService>(); // 商业保险服务

            return services;
        }
    }
}




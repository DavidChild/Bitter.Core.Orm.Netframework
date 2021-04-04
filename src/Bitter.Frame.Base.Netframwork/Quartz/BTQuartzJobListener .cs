using Bitter.Tools;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base.Quarizt
{
    /// <summary>
    /// 后台作业监听
    /// </summary>
    public class BTQuartzJobListener: IJobListener
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get
            {
                return "BTJobListener";
            }
        }
        /// <summary>
        /// 监听任务取消事件
        /// </summary>
        /// <param name="context"></param>
        public virtual void JobExecutionVetoed(IJobExecutionContext context)
        {
            LogService.Default.Info("后台作业 {"+ context.JobDetail.JobType.Name + "} 取消执行...");
        }
        /// <summary>
        /// 监听任务开始执行事件
        /// </summary>
        /// <param name="context"></param>
        public virtual void JobToBeExecuted(IJobExecutionContext context)
        {
            LogService.Default.Debug("后台作业 {"+ context.JobDetail.JobType.Name + "} 开始执行...");
        }
        /// <summary>
        /// 监听任务完成事件
        /// </summary>
        /// <param name="context"></param>
        /// <param name="jobException"></param>
        public virtual void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            if (jobException == null)
            {
                context.Scheduler.ResumeTrigger(context.Trigger.Key);
                LogService.Default.Debug("后台作业 {"+ context.JobDetail.JobType.Name + "} 执行成功.");
            }
            else
            {
                LogService.Default.Error("后台作业 {"+ context.JobDetail.JobType.Name + "} 执行失败，异常信息: {"+ jobException.Message+ "}",jobException);
            }
        }
    }
}


using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base.Quarizt
{
    /// <summary>
    /// 后台工作任务管理接口
    /// </summary>
    public interface IQuartzScheduleJobManager
    {
        /// <summary>
        /// 执行作业
        /// </summary>
        /// <typeparam name="TJob">后台作业</typeparam>
        /// <param name="configureJob">作业创建</param>
        /// <param name="configureTrigger">触发器创建</param>
        /// <returns></returns>
        Task ScheduleAsync<TJob>(Action<JobBuilder> configureJob, Action<TriggerBuilder> configureTrigger) where TJob : IJob;
    }
}

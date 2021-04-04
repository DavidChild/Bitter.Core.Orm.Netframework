using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base.Quarizt
{
    public abstract class JobBase: IJob
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="context"></param>
        public abstract void Execute(IJobExecutionContext context);

    }
}

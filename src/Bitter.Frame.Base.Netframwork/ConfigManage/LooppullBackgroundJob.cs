
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bitter.Base.Consul;
using Bitter.Base.Consul;
using Bitter.Base.Quarizt;
using Bitter.Tools;
using Quartz;

namespace Bitter.Base.Config
{
    public class LooppullBackgroundJob : JobBase
    {
        /// <summary>
        /// 获取路径
        /// </summary>
        public string ConPath { get; set; }
        /// <summary>
        /// 连接
        /// </summary>
        public string ConHost { get; set; }
        /// <summary>
        /// 数据中心
        /// </summary>
        public string ConDataCenter { get; set; }
        /// <summary>
        /// 超时时长
        /// </summary>
        public double ConTimeOut { get; set; }
        /// <summary>
        /// 等待时长
        /// </summary>
        public double ConWaitTime { get; set; }

        public override void Execute(IJobExecutionContext context)
        {
            try
            {
                //string conPath = context.JobDetail.JobDataMap.Get("ConPath").ToSafeString();
                //string conHost= context.JobDetail.JobDataMap.Get("ConHost").ToSafeString();
                //string conDataCenter= context.JobDetail.JobDataMap.Get("ConDataCenter").ToSafeString("");
                //double conTimeOut = context.JobDetail.JobDataMap.Get("ConTimeOut").ToSafeFloat(0);
                //double conWaitTime = context.JobDetail.JobDataMap.Get("ConWaitTime").ToSafeFloat(0);
                ConsulNetClient consulClient = new ConsulNetClient(new ConsulOption()
                {
                    Host = ConHost,
                    DataCenter = ConDataCenter,
                    WaitTime = TimeSpan.FromSeconds(ConWaitTime),
                    TimeOut = TimeSpan.FromSeconds(ConTimeOut)
                });
                //获取配置信息
               var conList= consulClient.KVListAsync<ConfigValue>(ConPath).GetAwaiter().GetResult();

                var valueList = conList.Where(o => o.IsEnable).ToList();
                //同步配置信息
                OperateWebConfigManage.WiteConfig(valueList);

                //释放Concul连接
                consulClient.Dispose();
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "执行后台任务拉取配置信息出错" + ex.Message);
            }
           
        }
    }
}

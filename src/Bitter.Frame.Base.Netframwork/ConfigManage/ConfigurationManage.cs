using Bitter.Base.Config;
using Bitter.Base.Quarizt;
using Bitter.Tools;
using EasyNetQ;
using EasyNetQ.Topology;
using Newtonsoft.Json;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base
{
    public class ConfigurationManage
    {
        private static ConfigOptionsDto _configoptionDto;

        private volatile static IBus bus = null;

        private static readonly object lockHelper = new object();

        /// <summary>
        /// 启用数据中心
        /// </summary>
        public static void UserConfigDataCenter(IBus _bus,string serverName)
        {
            try
            {
              
                bus = _bus;
                _configoptionDto = new ConfigOptionsDto();
                _configoptionDto.APPKey = serverName;
                _configoptionDto.IsEnableLooppull = ConfigManage.IsEnableLooppull;
                _configoptionDto.IsUseConfigManage = ConfigManage.IsUseConfigManage;
                _configoptionDto.ConsulDataCenter = ZkConfig.DataCenterName;
                _configoptionDto.ConsulHost = "http://" + ZkConfig.WriteServerList[0];
                _configoptionDto.ENVKey = ZkConfig.EnvName;
                _configoptionDto.ConsulTimeOut = ZkConfig.ConnectTimeOut;
                _configoptionDto.ConsulWaitTime = 30;
                RabbitMQRecive();
                Looppull();
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "启用配置中心出错" + ex.Message);
            }
          
        }
        /// <summary>
        /// 配置接收RabbitMQ推送信息
        /// </summary>
        /// <returns></returns>
        private static void RabbitMQRecive()
        {
            //判断是否启配置中心
            if (!_configoptionDto.IsUseConfigManage)
            {
                return;
            }
            var ex = bus.Advanced.ExchangeDeclare("bt_configDataCenter", ExchangeType.Direct);
            var qu = bus.Advanced.QueueDeclare();
            bus.Advanced.Bind(ex, qu, _configoptionDto.ENVKey + "_" + _configoptionDto.APPKey);
            bus.Advanced.Consume(qu, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                try
                {
                    lock (lockHelper)
                    {
                       
                        var message = Encoding.UTF8.GetString(body);
                        LogService.Default.Trace("收到配置中心更新信息:" + message);
                        //处理消息
                        var value = JsonConvert.DeserializeObject<ConfigValue>(message);
                        var valuelist = new List<ConfigValue>();
                        valuelist.Add(value);
                        //同步配置
                        OperateWebConfigManage.WiteConfig(valuelist);
                    }
                    
                }
                catch (Exception e)
                {
                    LogService.Default.Fatal(e, "接收配置中心数据出错:" + e.Message);
                }


            }));
        }
        /// <summary>
        /// 配置循环拉取
        /// </summary>
        /// <returns></returns>
        private static void Looppull()
        {
            if (!_configoptionDto.IsUseConfigManage)
            {
                return;
            }
            QuartzScheduleJobManager JobMange = new QuartzScheduleJobManager();

            if (_configoptionDto.IsEnableLooppull)
            {
                JobMange.ScheduleAsync<LooppullBackgroundJob>(job =>
                {
                    job.WithIdentity("LooppullConfig", "LooppullJob")
                        .UsingJobData("ConPath", "App/EnvConfig/" + _configoptionDto.APPKey + "/"+ _configoptionDto.ENVKey+"/")
                       .UsingJobData("ConHost", _configoptionDto.ConsulHost)
                       .UsingJobData("ConDataCenter", _configoptionDto.ConsulDataCenter)
                       .UsingJobData("ConTimeOut", _configoptionDto.ConsulTimeOut)
                       .UsingJobData("ConWaitTime", _configoptionDto.ConsulWaitTime);

                }, trigger =>
                {
                    trigger.WithIdentity("LooppullTri" + Guid.NewGuid().ToString(), "LooppullTrigger")
                    .StartNow()
                    .WithSimpleSchedule(x => x.WithIntervalInMinutes(1).RepeatForever());
                });
            }
            else
            {
                JobMange.ScheduleAsync<LooppullBackgroundJob>(job =>
                {
                    job.WithIdentity("LooppullConfig", "LooppullJob")
                       .UsingJobData("ConPath", "App/EnvConfig/" + _configoptionDto.APPKey  + "/" + _configoptionDto.ENVKey+"/")
                       .UsingJobData("ConHost", _configoptionDto.ConsulHost)
                       .UsingJobData("ConDataCenter", _configoptionDto.ConsulDataCenter)
                       .UsingJobData("ConTimeOut", _configoptionDto.ConsulTimeOut)
                       .UsingJobData("ConWaitTime", _configoptionDto.ConsulWaitTime);

                }, trigger =>
                {
                    trigger.WithIdentity("LooppullTri" + Guid.NewGuid().ToString(), "LooppullTrigger")
                    .StartNow();
                });
            }
            
        }
    }
}

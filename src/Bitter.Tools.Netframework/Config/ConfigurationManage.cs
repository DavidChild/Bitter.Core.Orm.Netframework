using Bitter.Tools.Consul;
using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.Config
{
    public  class ConfigurationManage
    {
        private static ConfigOptionsDto _configoptionDto;
        private ConsulNetClient _consulClient
        {
            get
            {
                return new ConsulNetClient(new ConsulOption()
                {
                    Host = _configoptionDto.ConsulHost,
                    DataCenter = _configoptionDto.ConsulDataCenter,
                    TimeOut = _configoptionDto.ConsulTimeOut,
                    WaitTime = _configoptionDto.ConsulWaitTime
                });
            }
        }

        private volatile static IBus bus = null;

        private static readonly object lockHelper = new object();

        /// <summary>
        /// 创建服务总线
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static IBus CreateEventBus(string config)
        {
            if (bus == null && !string.IsNullOrEmpty(config))
            {
                lock (lockHelper)
                {
                    if (bus == null)
                        bus = RabbitHutch.CreateBus(config);
                }
            }
            return bus;
        }

        /// <summary>
        /// 启用数据中心
        /// </summary>
        public static void UserConfigDataCenter(ConfigOptionsDto dto)
        {
            _configoptionDto = dto;
            RabbitMQRecive();
        }

        private void Dispose()
        {
            bus.Dispose();
        }
        /// <summary>
        /// 配置接收RabbitMQ推送信息
        /// </summary>
        /// <returns></returns>
        private static void RabbitMQRecive()
        {
            var ex = bus.Advanced.ExchangeDeclare("bt_configDataCenter", ExchangeType.Topic);
            var qu= bus.Advanced.QueueDeclare();
            bus.Advanced.Bind(ex, qu,_configoptionDto.ServiceName);
            bus.Advanced.Consume(qu, (body, properties, info) => Task.Factory.StartNew(() =>
            {
                try
                {
                    var message = Encoding.UTF8.GetString(body);
                    //处理消息
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

        }
    }
}

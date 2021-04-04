using Bitter.Base.Common;
using Bitter.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Bitter.Base.Consul
{
    public class ServiceEntity
    {
        public string IP { get; set; }
        public int Port { get; set; }
        private string serviceName { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName
        {
            get
            {
                return ZkConfig.EnvName + ":" + serviceName;
            }
            set
            {
                serviceName = value;
            }
        }

        /// <summary>
        /// 服务ID
        /// </summary>
        public string ServiceID()
        {
            return "【NETF】:" + this.ServiceName + "_" + IP + ":" + Port;
        }
        /// <summary>
        /// 健康检查地址
        /// </summary>
        public string CheckUrl { get; set; } = "ConsulCheckHealth/Healthy";
        /// <summary>
        /// 服务Id
        /// </summary>
        private static string LocalIpV4()
        {
            var ipV4 = string.Empty;
            return ipV4;
        }

        private bool _greyTestServer { get; set; }
        /// <summary>
        /// 是否灰度测试服务
        /// </summary>
        public bool GreyTestServer { get { return _greyTestServer; } set { _greyTestServer = value; } }




        public static string WriteConsulIP()
        {
            if (ZkConfig.WriteServerList == null || ZkConfig.WriteServerList.FirstOrDefault().Split(':').Count() == 0) return string.Empty;
            return ZkConfig.WriteServerList.FirstOrDefault().Split(':')[0];
        }
        public static int WriteConsulPort()
        {
            if (ZkConfig.WriteServerList == null || ZkConfig.WriteServerList.FirstOrDefault().Split(':').Count() == 0) return 0;
            return ZkConfig.WriteServerList.FirstOrDefault().Split(':')[1].ToSafeInt32(0);
        }
        public static string ReadConsulIP()
        {
            if (ZkConfig.ReadServerList == null || ZkConfig.ReadServerList.FirstOrDefault().Split(':').Count() == 0) return string.Empty;
            return ZkConfig.ReadServerList.FirstOrDefault().Split(':')[0];
        }
        public static int ReadConsulPort()
        {
            if (ZkConfig.ReadServerList == null || ZkConfig.ReadServerList.FirstOrDefault().Split(':').Count() == 0) return 0;
            return ZkConfig.ReadServerList.FirstOrDefault().Split(':')[1].ToSafeInt32(0);
        }

        public static bool IsClosedProvider()
        {
            return ZkConfig.ClosedSoaProviders;
        }
        public static string LocalServer()
        {
            return ZkConfig.LocalServer;
        }
        public static string EventName()
        {
            return ZkConfig.EnvName;
        }
        /// <summary>
        /// Tag
        /// </summary>
        public string[] Tags
        {
            get
            {
                var tags = new List<string>();
                //添加 urlprefix-/servicename 格式的 tag 标签，以便 Fabio 识别
                tags.Add($"urlprefix-/{ ServiceName}");
                //添加灰度Tag
                if (GreyTestServer)
                {
                    tags.Add(SysConstants.GreyTestTagStr);
                }
                return tags.ToArray();
            }
        }
    }
}

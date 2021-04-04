using Bitter.Base.Common;
using Bitter.Tools;
using Bitter.Tools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bitter.Base
{
    public class ZkConfig
    {


        public static SoaConfigInfo _zkConfig = SoaConfigInfo.GetConfig();
        /// <summary>
        /// 会话超时时间
        /// </summary>
        public static int SessionTimeOut
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                return _zkConfig.SessionTimeOut;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsRegister
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                return _zkConfig.IsRegister;

            }
        }
        /// <summary>
        /// 是否启用缓存机制
        /// </summary>
        public static bool ClosedSoaProviders
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                return _zkConfig.ClosedSoaProviders;
            }
        }
        /// <summary>
        /// 是否启用缓存机制
        /// </summary>
        public static bool EnabledCache
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                return _zkConfig.EnabledCache;
            }
        }
        /// <summary>
        /// 连接超时
        /// </summary>
        public static int ConnectTimeOut
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                return _zkConfig.ConnectTimeOut;
            }
        }


        public static string[] ReadServerList
        {

            get
            {
                SoaConfigInfo _zkConfig = SoaConfigInfo.GetConfig();
                var readServerList = _zkConfig.ReadServerList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (readServerList == null || readServerList.Length == 0)
                {
                    LogService.Default.Fatal("BT.Managet.SOA Warring:未配置consul服务读连接.");
                    return null;

                }
                else
                {
                    return readServerList;
                }
            }
        }


        public static string ConsulIP()
        {
            if (ZkConfig.WriteServerList == null || ZkConfig.WriteServerList.FirstOrDefault().Split(':').Count() == 0) return string.Empty;
            return ZkConfig.WriteServerList.FirstOrDefault().Split(':')[0];
        }
        /// <summary>
        /// 获取zk写连接
        /// </summary>
        public static string[] WriteServerList
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var writeServerList = _zkConfig.WriteServerList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                if (writeServerList == null || writeServerList.Length == 0)
                {
                    LogService.Default.Fatal("BT.Managet.SOA Warring:未配置consul服务写连接.");
                    return null;
                }
                else
                {
                    return writeServerList;
                }
            }
        }
        /// <summary>
        /// 获取zk写连接
        /// </summary>
        public static string LocalServer
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var localServer = _zkConfig.LocalServer;
                if (localServer == null || localServer.Length == 0)
                {
                    return string.Empty;
                }
                else
                {
                    return localServer;
                }
            }
        }
        /// <summary>
        /// 数据中心
        /// </summary>
        public static string DataCenterName
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var datacentername = _zkConfig.DataCenterName;
                return datacentername;
            }
        }

        /// <summary>
        /// 环境名称
        /// </summary>
        public static string EnvName
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var envName = _zkConfig.EnvName;
                return envName;
            }
        }

        /// <summary>
        /// 服务名称
        /// </summary>
        public static string ServiceName
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var servicename = _zkConfig.ServiceName;
                return servicename;
            }
        }

        /// <summary>
        /// 增加了是否部署在docker中(是否发布到docker环境中)
        /// </summary>
        public static bool IsDeployInDocker
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                bool deployInDocker = _zkConfig.IsDeployInDocker;
                return deployInDocker;
            }
        }

        /// <summary>
        /// 如果 此服务为docker部署,请设置宿主机存放的IP目录
        /// </summary>
        public static string DockerHostIPFile
        {

            get
            {
                // ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                string dockerHostIPFile = _zkConfig.DockerHostIPFile;
                return dockerHostIPFile;
            }
        }


        /// <summary>
        /// 排除调试连接地址
        /// </summary>
        public static List<string> ExcludeServers
        {

            get
            {
                //ZkConfigInfo _zkConfig = ZkConfigInfo.GetConfig();
                var excludeServers = _zkConfig.ExcludeServers;
                if (string.IsNullOrEmpty(excludeServers))
                {
                    return new List<string>();
                }
                else
                {
                    return excludeServers.Split(',').ToList();

                }

            }
        }

        /// <summary>
        /// 调试专用的debugServer
        /// </summary>
        public static string DebugServer
        {

            get
            {

                var debugServer = _zkConfig.DebugServer;
                if (string.IsNullOrEmpty(debugServer))
                {
                    return string.Empty;
                }
                else
                {

                    return debugServer;
                }

            }
        }

        /// <summary>
        /// 是否为灰度测试服务
        /// </summary>
        public static bool IsGreyTestServer
        {
            get
            {
                return _zkConfig.IsGreyTestServer.ToSafeBool(false);
            }
        }

        /// <summary>
        /// 如果命中不了缓存已经命中不了容器中的提供者,那么最后命中配置
        /// </summary>
        public static string GetUri(string serverFlag)
        {
            try
            {
                var apiUrl = System.Configuration.ConfigurationManager.AppSettings[serverFlag];
                //System.Configuration.ConfigurationSettings.AppSettings[serverFlag];

                return string.Format("rest://{0}/", apiUrl);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 服务寻址配置项0 Consul寻址 1配置文件寻址
        /// </summary>
        /// <param name="apiFlagKey"></param>
        /// <returns></returns>
        public static int GetApiSoaProvider(string apiFlagKey)
        {
            return System.Configuration.ConfigurationManager.AppSettings[$"{apiFlagKey}_{SysConstants.ApiCloseSoaProviderFlag}"].ToString().ToSafeInt32(0);
        }

        /// <summary>
        /// 灰度测试状态
        ///  1灰度测试中 0非灰度测试
        /// </summary>
        /// <returns></returns>
        public static int GetConfigGreyTestFalg()
        {
            return System.Configuration.ConfigurationManager.AppSettings[$"{SysConstants.GreyTestFlagStr}"].ToString().ToSafeInt32(0);
        }
    }
}

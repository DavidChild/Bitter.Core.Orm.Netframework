using System.Configuration;

/********************************************************************************
** auth： Jason
** date： 2016/7/14 10:02:26
** desc： Redis缓存接口配置信息
** Ver.:  V1.0.0
** Copyright (C) 2016 Bitter 版权所有。
*********************************************************************************/

namespace Bitter.Base
{
    public sealed class SoaConfigInfo : ConfigurationSection
    {




        /// <summary>
        ///  会话超时时间
        /// </summary>
        [ConfigurationProperty("SessionTimeOut", IsRequired = false, DefaultValue = 30)]
        public int SessionTimeOut
        {
            get
            {
                return (int)base["SessionTimeOut"];
            }
            set
            {
                base["SessionTimeOut"] = value;
            }
        }
        /// <summary>
        ///  连接超时
        /// </summary>
        [ConfigurationProperty("ConnectTimeOut", IsRequired = false, DefaultValue = 10)]
        public int ConnectTimeOut
        {
            get
            {
                return (int)base["ConnectTimeOut"];
            }
            set
            {
                base["ConnectTimeOut"] = value;
            }
        }

        /// <summary>
        ///  当前服务地址
        /// </summary>
        [ConfigurationProperty("LocalServer", IsRequired = false)]
        public string LocalServer
        {
            get
            {
                return (string)base["LocalServer"];
            }
            set
            {
                base["LocalServer"] = value;
            }
        }


        /// <summary>
        /// 最大读链接数
        /// </summary>
        [ConfigurationProperty("MaxReadPoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxReadPoolSize
        {
            get
            {
                int _maxReadPoolSize = (int)base["MaxReadPoolSize"];
                return _maxReadPoolSize > 0 ? _maxReadPoolSize : 5;
            }
            set
            {
                base["MaxReadPoolSize"] = value;
            }
        }

        /// <summary>
        /// 最大写链接数
        /// </summary>
        [ConfigurationProperty("MaxWritePoolSize", IsRequired = false, DefaultValue = 5)]
        public int MaxWritePoolSize
        {
            get
            {
                int _maxWritePoolSize = (int)base["MaxWritePoolSize"];
                return _maxWritePoolSize > 0 ? _maxWritePoolSize : 5;
            }
            set
            {
                base["MaxWritePoolSize"] = value;
            }
        }

        /// <summary>
        /// 可读的Redis链接地址
        /// </summary>
        [ConfigurationProperty("ReadServerList", IsRequired = false)]
        public string ReadServerList
        {
            get
            {
                return (string)base["ReadServerList"];
            }
            set
            {
                base["ReadServerList"] = value;
            }
        }

        /// <summary>
        /// 是否记录日志,该设置仅用于排查运行时出现的问题,如工作正常,请关闭该项
        /// </summary>
        [ConfigurationProperty("RecordeLog", IsRequired = false, DefaultValue = false)]
        public bool RecordeLog
        {
            get
            {
                return (bool)base["RecordeLog"];
            }
            set
            {
                base["RecordeLog"] = value;
            }
        }

        /// <summary>
        /// 是否启用缓存机制
        /// </summary>
        [ConfigurationProperty("EnabledCache", IsRequired = false, DefaultValue = false)]
        public bool EnabledCache
        {
            get
            {
                return (bool)base["EnabledCache"];
            }
            set
            {
                base["EnabledCache"] = value;
            }
        }


        /// <summary>
        /// 排除提供者
        /// </summary>
        [ConfigurationProperty("ExcludeServers", IsRequired = false, DefaultValue = "")]
        public string ExcludeServers
        {
            get
            {
                return (string)base["ExcludeServers"];
            }
            set
            {
                base["ExcludeServers"] = value;
            }
        }

        /// <summary>
        /// 调试用的Debug
        /// </summary>
        [ConfigurationProperty("DebugServer", IsRequired = false, DefaultValue = "")]
        public string DebugServer
        {
            get
            {
                return (string)base["DebugServer"];
            }
            set
            {
                base["DebugServer"] = value;
            }
        }






        /// <summary>
        /// 可写的链接地址
        /// </summary>
        [ConfigurationProperty("WriteServerList", IsRequired = false)]
        public string WriteServerList
        {
            get
            {
                return (string)base["WriteServerList"];
            }
            set
            {
                base["WriteServerList"] = value;
            }
        }

        /// <summary>
        /// 关闭SOA ZK  寻址
        /// </summary>
        [ConfigurationProperty("ClosedSoaProviders", IsRequired = false, DefaultValue = false)]
        public bool ClosedSoaProviders
        {
            get
            {
                return (bool)base["ClosedSoaProviders"];
            }
            set
            {
                base["ClosedSoaProviders"] = value;
            }
        }

        public static SoaConfigInfo GetConfig()
        {
            SoaConfigInfo section = GetConfig("SoaConfig");
            return section;
        }

        /// <summary>
        ///  数据中心名称：默认是dc1
        /// </summary>
        [ConfigurationProperty("DataCenterName", IsRequired = false, DefaultValue = "dc1")]
        public string DataCenterName
        {
            get
            {
                return (string)base["DataCenterName"];
            }
            set
            {
                base["DataCenterName"] = value;
            }
        }

        /// <summary>
        ///  数据中心名称：默认是dc1
        /// </summary>
        [ConfigurationProperty("IsRegister", IsRequired = false, DefaultValue = "false")]
        public bool IsRegister
        {
            get
            {
                return (bool)base["IsRegister"];
            }
            set
            {
                base["IsRegister"] = value;
            }
        }

        /// <summary>
        ///  服务名称
        /// </summary>
        [ConfigurationProperty("ServiceName", IsRequired =false, DefaultValue = "")]
        public string  ServiceName
        {
            get
            {
                return (string)base["ServiceName"];
            }
            set
            {
                base["ServiceName"] = value;
            }
        }
        /// <summary>
        ///  环境名称
        /// </summary>
        [ConfigurationProperty("EnvName", IsRequired = false)]
        public string EnvName
        {
            get
            {
                return (string)base["EnvName"];
            }
            set
            {
                base["EnvName"] = value;
            }
        }

        /// <summary>
        ///  是否灰度测试服务
        /// </summary>
        [ConfigurationProperty("IsGreyTestServer", IsRequired = false, DefaultValue = false)]
        public bool? IsGreyTestServer
        {
            get
            {
                return (bool?)base["IsGreyTestServer"];
            }
            set
            {
                base["IsGreyTestServer"] = value;
            }
        }

        /// <summary>
        ///  支持linxu/Windows  dokcer 部署(是否发布到docker 环境）
        /// </summary>
        [ConfigurationProperty("IsDeployInDocker", IsRequired = false, DefaultValue = false)]
        public bool IsDeployInDocker
        {
            get
            {
                return (bool)base["IsDeployInDocker"];
            }
            set
            {
                base["IsDeployInDocker"] = value;
            }
        }


        /// <summary>
        ///  支持linxu/Windows  dokcer 部署(如果是docker 部署,那么那么请确保配置此字段）
        /// </summary>
        [ConfigurationProperty("DockerHostIPFile", IsRequired =false, DefaultValue ="")]
        public string DockerHostIPFile
        {
            get
            {
                return (string)base["DockerHostIPFile"];
            }
            set
            {
                base["DockerHostIPFile"] = value;
            }
        }
        


        public static SoaConfigInfo GetConfig(string sectionName)
        {
            SoaConfigInfo section = (SoaConfigInfo)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }
    }
}
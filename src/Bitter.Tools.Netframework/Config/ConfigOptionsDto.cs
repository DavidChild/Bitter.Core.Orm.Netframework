using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.Config
{
    public class ConfigOptionsDto
    {
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 消息中心地址
        /// </summary>
        public string MessageHost { get; set; }
        /// <summary>
        /// 是否开启循环拉取
        /// </summary>
        public bool IsEnableLooppull { get; set; }
        /// <summary>
        /// Consul地址
        /// </summary>
        public string ConsulHost { get; set; }
        /// <summary>
        /// 对应的数据中心
        /// </summary>
        public string ConsulDataCenter { get; set; }
        /// <summary>
        /// 等待时长
        /// </summary>
        public TimeSpan? ConsulWaitTime { get; set; }
        /// <summary>
        /// 超时时间
        /// </summary>
        public TimeSpan? ConsulTimeOut { get; set; }
    }
}

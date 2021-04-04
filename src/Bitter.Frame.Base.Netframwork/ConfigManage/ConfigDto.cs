using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base.Config
{
    public class ConfigDto
    {
        public string Key { get; set; }

        public ConfigValue Value { get; set; }
    }

    public class ConfigValue
    {
        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 基础模板配置路径
        /// </summary>
        public string BaseEnvPath { get; set; }

        /// <summary>
        /// 基础环境配置值
        /// </summary>
        public string BaseEnvValue { get; set; }
        /// <summary>
        /// 应用基础配置
        /// </summary>
        public string AppTmpPath { get; set; }

        /// <summary>
        /// 应用Key
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 环境Key
        /// </summary>
        public string EnvKey { get; set; }

        /// <summary>
        /// 是否同步基础配置
        /// </summary>
        public bool IsSync { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// Key值
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Value值
        /// </summary>
        public string Value { get; set; }

        public int ConfigType { get; set; }
    }
}

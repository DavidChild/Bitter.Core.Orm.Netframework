using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Base
{
    /// <summary>
    /// Config
    /// </summary>
    public class ConfigForwardInfo : ConfigurationSection
    {
        /// <summary>
        /// isopencheck
        /// </summary>
        [ConfigurationProperty("isopencheck", IsRequired = false, DefaultValue = true)]
        public bool IsOpenCheck
        {
            get
            {
                return (bool)base["isopencheck"];
            }
            set
            {
                base["isopencheck"] = value;
            }
        }

        /// <summary>
        /// 默认超时时间 为空则为20
        /// </summary>
        [ConfigurationProperty("timeout", IsRequired = false)]
        public int TimeOut
        {
            get
            {
                return (int)base["timeout"] == 0 ? 20 : (int)base["timeout"];
            }
            set
            {
                base["timeout"] = value;
            }
        }
        /// <summary>
        /// forward
        /// </summary>
        [ConfigurationProperty("", IsDefaultCollection = true)]
        public ForwardElementCollection ForwardConfig
        {
            get
            {
                return (ForwardElementCollection)base[""];
            }
        }
    }

    /// <summary>
    /// collection
    /// </summary>
    public class ForwardElementCollection : ConfigurationElementCollection
    {
        /// <summary>
        /// create
        /// </summary>
        /// <returns></returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new ForwardElement();
        }

        /// <summary>
        /// get key
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ForwardElement)element).Route;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "forward";
            }
        }

        public ForwardElement this[int index]
        {
            get
            {
                return (ForwardElement)BaseGet(index);
            }
            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }
                BaseAdd(index, value);
            }
        }
    }

    /// <summary>
    /// 路由想
    /// </summary>
    public class ForwardElement : ConfigurationElement
    {
        /// <summary>
        /// 路由名称
        /// </summary>
        [ConfigurationProperty("route", IsRequired = true)]
        public string Route
        {
            get
            {
                return (string)base["route"];
            }
            set
            {
                base["route"] = value;
            }
        }

        /// <summary>
        /// 是否开启转发
        /// </summary>
        [ConfigurationProperty("isopen", IsRequired = false, DefaultValue = true)]
        public bool IsOpen
        {
            get
            {
                return (bool)base["isopen"];
            }
            set
            {
                base["isopen"] = value;
            }
        }

        /// <summary>
        /// 超时时间
        /// </summary>
        [ConfigurationProperty("timeout", IsRequired = false, DefaultValue = 7)]
        public int TimeOut
        {
            get
            {
                return (int)base["timeout"];
            }
            set
            {
                base["timeout"] = value;
            }
        }

        /// <summary>
        /// 转发目标路由
        /// 默认为路由名称
        /// </summary>
        [ConfigurationProperty("forwardroute", IsRequired = false)]
        public string ForwardRoute
        {
            get
            {
                return (string)base["forwardroute"];
            }
            set
            {
                base["forwardroute"] = value;
            }
        }

    }
}


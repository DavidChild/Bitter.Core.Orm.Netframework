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
   public sealed class ConfigManageInfo : ConfigurationSection
    {
        

       

        /// <summary>
        ///  会话超时时间
        /// </summary>
        [ConfigurationProperty("MqSetting", IsRequired = true, DefaultValue ="")]
        public string MqSetting
        {
            get
            {
                return (string)base["MqSetting"];
            }
            set
            {
                base["MqSetting"] = value;
            }
        }
       

        /// <summary>
        /// 是否启用间隔性拉取
        /// </summary>
        [ConfigurationProperty("IsEnableLooppull", IsRequired = false, DefaultValue = false)]
        public bool IsEnableLooppull
        {
            get
            {
                return (bool)base["IsEnableLooppull"];
            }
            set
            {
                base["IsEnableLooppull"] = value;
            }
        }

        /// <summary>
        /// 是否启用配置中心拉取
        /// </summary>
        [ConfigurationProperty("IsUseConfigManage", IsRequired = false, DefaultValue = false)]
        public bool IsUseConfigManage
        {
            get
            {
                return (bool)base["IsUseConfigManage"];
            }
            set
            {
                base["IsUseConfigManage"] = value;
            }
        }
        public static ConfigManageInfo GetConfig(string sectionName)
        {
            ConfigManageInfo section = (ConfigManageInfo)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");
            return section;
        }

        public static ConfigManageInfo GetConfig()
        {
            ConfigManageInfo section = GetConfig("ConfigManage");
            return section;
        }
    }
}
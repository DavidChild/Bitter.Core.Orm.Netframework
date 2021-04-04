using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bitter.Base
{
    public  class ConfigManage
    {


        public static ConfigManageInfo _configManage = ConfigManageInfo.GetConfig();
        /// <summary>
        /// 会话超时时间
        /// </summary>
        public static string MqSetting
        {

            get
            {
                
                return _configManage.MqSetting;
                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static bool IsUseConfigManage
        {

            get
            {
               return _configManage.IsUseConfigManage;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static bool IsEnableLooppull
        {

            get
            {
               return _configManage.IsEnableLooppull;
            }
        }
      
          

    }
}

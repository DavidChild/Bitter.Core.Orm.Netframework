using Bitter.Tools.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools
{
    public class Configsetting
    {


        private static MyAppsetting myAppsetting { get; set; } //appsetting 实例对象


        /// <summary>
        /// 获取实例
        /// </summary>
        public  static MyAppsetting Appsettings
        {

          get 
            {
                if (myAppsetting == null)
                {
                    myAppsetting = new MyAppsetting();
                    return myAppsetting;
                }
                else
                {
                    return myAppsetting;
                }
            
            
            }
        }
    }

    public class MyAppsetting
    {
        /// <summary>
        /// 构建索引器
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {

                return System.Configuration.ConfigurationManager.AppSettings[key].ToSafeString("");
            }

        }

        /// <summary>
        /// 获取appsetting 的配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T TryGet<T>(string key) where T : new()
        {
            var str = System.Configuration.ConfigurationManager.AppSettings[key].ToSafeString("");
            if (string.IsNullOrEmpty(str))
            {
                return JsonConvert.DeserializeObject<T>(str);
            }
            return new T();
        }


        /// <summary>
        /// 获取数据连接配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public ConnectionStringSettings DbConnection(string key)
        {
            return ConfigurationManager.ConnectionStrings[key];
        }
    }

     
}

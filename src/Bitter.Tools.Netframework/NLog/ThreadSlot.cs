using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Web;
using Bitter.Tools.Utils;
using Newtonsoft.Json;

namespace Bitter.Tools
{
    /********************************************************************************
    ** auth： weiyz
    ** date： 2018/2/6
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2018 Bitter 版权所有。
    *********************************************************************************/
    public class ThreadSlot
    {
        //线程安全 线程本地存储
        private static ThreadLocal<ThreadLocalData> _bagLocal = new ThreadLocal<ThreadLocalData>(true);

        public static bool IsContainsProperty()
        {
            //C#中获得所有的属性（xiaofu是一个自定义实体对象）
            System.Reflection.PropertyInfo[] pinfo = HttpContext.Current.GetType().GetProperties();
            //循环所有的属性字段
            foreach (System.Reflection.PropertyInfo p in pinfo)
            {
                if (p.Name.ToLower() == "request")
                {
                    return true;
                }
            }
            return false;
        }
       
        /// <summary>
        /// 初始化线程本地数据
        /// </summary>
        private static void InitThreadSlot()
        {
            //lock ("s")
            //{
            //if (_bagLocal.Value == null || string.IsNullOrEmpty(_bagLocal.Value.TraceId))
            //{
            ThreadLocalData data = null;
            //http请求
            try
            {
                if (HttpContext.Current != null && HttpContext.Current.Request != null && HttpContext.Current.Request.Url != null)
                {
                    //服务转发过来的请求
                    data = HttpContext.Current.Request.Headers["BTProcessInfo"] == null ? null : JsonConvert.DeserializeObject<ThreadLocalData>(HttpContext.Current.Request.Headers["BTProcessInfo"]);
                    if (data != null)
                    {
                        //Helper.LogHelper.WriteLog("Server:请求线程数据0000");
                        data.FromUrl = data.FromUrl; //
                        data.ToUrl = HttpContext.Current.Request.Url.ToSafeString("");
                        data.TraceSecondId = string.IsNullOrEmpty(ThreadSlot.GetClientID()) ? null : ThreadSlot.GetClientID().Split('|')[0];
                        data.StartTime = data.StartTime == DateTime.MinValue ? DateTime.Now : data.StartTime;
                    }
                    else
                    {
                       // Helper.LogHelper.WriteLog("Server:请求线程数据1111");
                        //app终端 请求带有requestToken
                        string requestToken = HttpContext.Current.Request.Headers["traceToken"] == null ? string.Empty : HttpContext.Current.Request.Headers["traceToken"].ToString();

                        //AccessToken  获取到用户token 判断是不是调试用户   add by weiyz 2019-12-9
                        string ISDEBUG = HttpContext.Current.Request.Headers.AllKeys.Contains("DEBUGEMP") == false ? string.Empty : HttpContext.Current.Request.Headers["DEBUGEMP"].ToString();
                        //Helper.LogHelper.WriteLog("Server:请求线程数据2222:" + ISDEBUG);
                        //LogService.Default.Debug(HttpContext.Current.Request.Url.AbsoluteUri + "-traceToken-" + (string.IsNullOrEmpty(requestToken) ? "notoken" : requestToken));
                        if (data == null && (!string.IsNullOrEmpty(requestToken)))
                        {
                            data = new ThreadLocalData();
                            data.TraceId = requestToken;//HttpContext.Current.Request.Headers["traceToken"].ToString();
                            data.FromUrl = GetAppOS();
                            data.ToUrl = HttpContext.Current.Request.Url.AbsoluteUri;
                            data.StartTime = DateTime.Now;
                            data.TraceSecondId = string.IsNullOrEmpty(ThreadSlot.GetClientID()) ? null : ThreadSlot.GetClientID().Split('|')[0];
                            data.IsDebug = ISDEBUG == "1";
                        }
                    }


                    if (data != null) CallContext.LogicalSetData("tp", data);
                    //ExecutionContext.RestoreFlow();
                }
                else
                {
                    data = CallContext.LogicalGetData("tp") == null ? null : CallContext.LogicalGetData("tp") as ThreadLocalData;
                    //ThreadPool.QueueUserWorkItem(p => { object s = CallContext.LogicalGetData("tp"); }); 
                }

            }
            catch
            {
                data = CallContext.LogicalGetData("tp") == null ? null : CallContext.LogicalGetData("tp") as ThreadLocalData;
            }
          

            _bagLocal.Value = data == null ? new ThreadLocalData() { TraceId = Guid.NewGuid().ToString("N"), StartTime = DateTime.Now, TraceSecondId = string.IsNullOrEmpty(ThreadSlot.GetClientID()) ? null : ThreadSlot.GetClientID().Split('|')[0], FromUrl = GetCurrentWebSiteName() } : data;
            if (CallContext.LogicalGetData("tp") == null) CallContext.LogicalSetData("tp", _bagLocal.Value);
            //LogService.Default.Debug("clientID=" + _bagLocal.Value.TraceSecondId);
            //}
            // }
        }

        /// <summary>
        /// 初始化并保存traceinfo 到逻辑调用上下文
        /// </summary>
        /// <returns></returns>
        public static ThreadLocalData LogicalGetData()
        {
            try
            {
                //Helper.LogHelper.WriteLog("Server:请求线程数据----00");
                //if(!_bagLocal.IsValueCreated)
                if (CallContext.LogicalGetData("tp") == null)
                    InitThreadSlot();
            }
            catch (Exception ex)
            {
                //
            }
            var k=  (ThreadLocalData)CallContext.LogicalGetData("tp");//_bagLocal.Value;
            if (k == null)
            {
                return new ThreadLocalData() { FromUrl = "", IsDebug=false, ToUrl="", StartTime=DateTime.Now, TraceId=Guid.NewGuid().ToString("N"),  TraceLayer="0", TraceSecondId=""};
            }
            return k;
        }

        /// <summary>
        /// 日志输出时自定义布局器<traceinfo>获取数据
        /// </summary>
        /// <returns></returns>
        public static string GetThreadTraceInfo()
        {
            string str = null;
            try
            {
                //Helper.LogHelper.WriteLog("Server:请求线程数据----1");
                var obj = CallContext.LogicalGetData("tp");
                //Helper.LogHelper.WriteLog("Server:请求线程数据----2" + JsonConvert.SerializeObject(obj));

                if (obj == null)
                {
                    //Helper.LogHelper.WriteLog("Server:请求线程数据--init----1");
                    InitThreadSlot();
                    obj = CallContext.LogicalGetData("tp");
                    //Helper.LogHelper.WriteLog("Server:请求线程数据--init----2"+ JsonConvert.SerializeObject(obj));
                }

                str = Newtonsoft.Json.JsonConvert.SerializeObject(obj);
            }
            catch (Exception)
            {
                //
               // LogService.Default.Debug("获取traceinfo失败");
            }
            return str;
        }

        /// <summary>
        /// 线程处理层级加1
        /// </summary>
        public static void TraceLevelAdd()
        {
            if (_bagLocal!=null&&_bagLocal.Value != null && _bagLocal.Value.TraceId != null)
            {
                _bagLocal.Value.TraceLayer += ".1";
            }
        }

        /// <summary>
        /// 设置调用结束时间
        /// </summary>
        public static void SetTraceEndTime()
        {
            //if (_bagLocal.Value != null && _bagLocal.Value.TraceId != null)
            //{
            //    _bagLocal.Value.EndTime = DateTime.Now;
            //}
            try
            {
                if (CallContext.LogicalGetData("tp") != null)
                {
                    ThreadLocalData tld = (ThreadLocalData)CallContext.LogicalGetData("tp");
                    tld.EndTime = DateTime.Now;
                    CallContext.LogicalSetData("tp", tld);
                }
            }
            catch (Exception)
            {
                //
            }
            
        }

        /// <summary>
        /// 清空线程本地数据
        /// </summary>
        public static void LogicalThreadDataClear()
        {
            _bagLocal.Dispose();
            _bagLocal = new ThreadLocal<ThreadLocalData>(true);
            CallContext.LogicalSetData("tp", null);
        }

        /// <summary>
        /// 当前服务id（|后面为终端标识 默认为否）
        /// </summary>
        /// <returns></returns>
        public static string GetClientID()
        {
            string str = string.Empty;
            string clientid = System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
            if (!string.IsNullOrEmpty(clientid))
            {
                if (clientid.IndexOf('|') == -1)
                {
                    clientid += "|0";
                }
                str = clientid;
            }
            return str;
        }

        /// <summary>
        /// 获取请求设备os
        /// </summary>
        /// <returns></returns>
        public static string GetAppOS()
        {
            string str = null;
            try
            {
                //string userAgent = HttpContext.Current.Request.Headers["User-Agent"];
                //if (!string.IsNullOrEmpty(userAgent))
                //{
                //    string[] keywords = { "Android", "iPhone", "iPod", "iPad", "Windows Phone", "MQQBrowser" };
                //    foreach (string item in keywords)
                //    {
                //        if (userAgent.Contains(item))
                //        {
                //            str = item;
                //            break;
                //        }
                //    }
                //}
                //安卓设备号
                string deviceModel = HttpContext.Current.Request.Headers["deviceModel"];
                if (!string.IsNullOrEmpty(deviceModel))
                    str = deviceModel;
            }
            catch (Exception)
            {
                
                throw;
            }

            return str;
        }

        /// <summary>
        /// 获取当前iis站点名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentWebSiteName()
        {
            try
            {
                return System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName();//iis站点
            }
            catch (Exception)
            {
                try
                {
                    return System.AppDomain.CurrentDomain.FriendlyName;//控制台
                }
                catch (Exception)
                {
                    //
                }
            }
            return null;
        }
    }

    public class ThreadLocalData:ICloneable
    {
        /// <summary>
        /// 多线程调用层级
        /// </summary>
        private string pTraceLayer = "1";

        public string TraceLayer { get { return pTraceLayer; } set { this.pTraceLayer = value; } }
        /// <summary>
        /// threadguid
        /// </summary>
        public string TraceId { get; set; }
        /// <summary>
        /// 终端单线程中远程调用id
        /// </summary>
        public string TraceSecondId { get; set; }
        /// <summary>
        /// 请求来源
        /// </summary>
        public string FromUrl { get; set; }
        /// <summary>
        /// 当前url
        /// </summary>
        public string ToUrl { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 是否调试线程
        /// </summary>
        public bool IsDebug { get; set; } = false;


        public object Clone()
        {
            ThreadLocalData cLocalData=new ThreadLocalData();
            cLocalData.TraceLayer = this.TraceLayer+".1";
            cLocalData.TraceId = this.TraceId;
            cLocalData.FromUrl = this.FromUrl;
            cLocalData.ToUrl = this.ToUrl;
            cLocalData.StartTime = this.StartTime;
            cLocalData.EndTime = this.EndTime;
            cLocalData.IsDebug = this.IsDebug;
            return cLocalData;
        }

     
    }
}

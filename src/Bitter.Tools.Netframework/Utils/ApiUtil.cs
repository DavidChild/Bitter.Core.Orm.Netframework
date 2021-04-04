using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Bitter.Tools.Helper;
using RestSharp;

namespace Bitter.Tools.Utils
{
    public class ApiUtil
    {
        public static int timeout = 30000;
        /// <summary>
        /// 下载文件接口
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static Stream FileDownLoad(string requestUri, string json)
        {
            //json格式请求数据

            string requestData = json;
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(requestUri);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(requestData);

            //post请求
            myRequest.Method = "POST";
            myRequest.ContentLength = buf.Length;
            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.Headers.Add("Api-Version", "1.0");
            myRequest.AllowAutoRedirect = true;


            myRequest.ContentType = "application/json; charset=utf-8";
            myRequest.Accept = "application/json";

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            string ReqResult = string.Empty;
            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse) myRequest.GetResponse();

            }
            catch (Exception e)
            {
                throw e;
            }

            return myResponse.GetResponseStream();
        }

        /// <summary>
        /// GET接口方法
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetHttpRequest(string requestUri, Int32 time = 7000)
        {
            try
            {
                HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(requestUri);

                myRequest.UseDefaultCredentials = true;
                myRequest.ContentType = "application/json; charset=utf-8";
                myRequest.Accept = "application/json";
                myRequest.Method = "GET";
                if(time==0||time> timeout)
                {
                    time = timeout;
                }
                myRequest.Timeout = time;

                //线程信息传递
                //BaseThreadEntity entity = BaseThread.GetTraceInfo();
                //if (entity.TraceId != null && entity.TraceId != "")
                //    myRequest.Headers.Add("MyProcessInfo", JsonHelper.ObjectToJson(entity));

                try
                {
                    ThreadLocalData entity = (ThreadLocalData) ThreadSlot.LogicalGetData().Clone();
                    if (entity != null)
                    {
                        string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                        if (!string.IsNullOrEmpty(clientID))
                        {
                            if (clientID.Split('|')[1] == "1")//终端(后台)
                            {
                                entity.TraceSecondId = clientID.Split('|')[0];
                                entity.TraceId = Guid.NewGuid().ToString("N");
                            }
                        }
                        myRequest.Headers.Add("BTProcessInfo", JsonHelper.SerializeObject(entity));
                    }
                    
                }
                catch (Exception ex)
                {
                    LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
                }

                string ReqResult = string.Empty;
                HttpWebResponse myResponse = null;
                myResponse = (HttpWebResponse) myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                ReqResult = reader.ReadToEnd();
                reader.Close();
                myResponse.Close();
                return ReqResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post接口方法
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string PostHttpRequest(string requestUri, string json, Int32 time = 7000, string version = "1.0")
        {
            //json格式请求数据
            string requestData = json;
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(requestUri);

            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(requestData);

            //post请求
            myRequest.Method = "POST";
            if (time == 0 || time > timeout)
            {
                time = timeout;
            }
            myRequest.Timeout = time;
            myRequest.ContentLength = buf.Length;


            //线程信息传递
            //BaseThreadEntity entity = BaseThread.GetTraceInfo();
            //if (entity.TraceId != null && entity.TraceId != "")
            //    myRequest.Headers.Add("MyProcessInfo", JsonHelper.ObjectToJson(entity));
            try
            {
                ThreadLocalData entity = (ThreadLocalData) ThreadSlot.LogicalGetData().Clone();
                if (entity != null)
                {
                    string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                    if (!string.IsNullOrEmpty(clientID))
                    {
                        if (clientID.Split('|')[1] == "1")//终端(后台)
                        {
                            entity.TraceSecondId = clientID.Split('|')[0];
                            entity.TraceId = Guid.NewGuid().ToString("N");
                        }
                    }
                    myRequest.Headers.Add("BTProcessInfo", JsonHelper.SerializeObject(entity));
                }
                
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
            }


            myRequest.AllowAutoRedirect = true;
            CookieContainer cookieContainer = new CookieContainer();
            myRequest.CookieContainer = cookieContainer;

            myRequest.Headers.Add("Api-Version", version);
            myRequest.AllowAutoRedirect = true;
            myRequest.Timeout = time;

            myRequest.ContentType = "application/json; charset=utf-8";
            myRequest.Accept = "application/json";

            Stream newStream = myRequest.GetRequestStream();
            newStream.Write(buf, 0, buf.Length);
            newStream.Close();

            string ReqResult = string.Empty;
            HttpWebResponse myResponse = null;
            try
            {
                myResponse = (HttpWebResponse) myRequest.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                ReqResult = reader.ReadToEnd();
                reader.Close();
                myResponse.Close();
            }
            catch (Exception e)
            {

                throw e;
            }

            return ReqResult;
        }

        /// <summary>
        /// Post接口方法
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string GetHttpRequest(string requestUri, string json, Int32 time = 7000, string version = "1.0")
        {
            //json格式请求数据   
            if (time==0||time> timeout)
            {
                time = timeout;
            }

            string requestData = json;
            HttpWebRequest myRequest = (HttpWebRequest) WebRequest.Create(requestUri);
            //utf-8编码
            byte[] buf = System.Text.Encoding.GetEncoding("utf-8").GetBytes(requestData);

            //post请求
            myRequest.Method = "GET";
            myRequest.ContentLength = buf.Length;

            myRequest.MaximumAutomaticRedirections = 1;
            myRequest.Headers.Add("Api-Version", version);
            myRequest.AllowAutoRedirect = true;
            myRequest.Timeout = time;

            myRequest.ContentType = "application/json; charset=utf-8";
            myRequest.Accept = "application/json";



            //线程信息传递
            //BaseThreadEntity entity = BaseThread.GetTraceInfo();
            //if (entity.TraceId != null && entity.TraceId != "")
            //    myRequest.Headers.Add("MyProcessInfo", JsonHelper.ObjectToJson(entity));

            try
            {
                ThreadLocalData entity = (ThreadLocalData) ThreadSlot.LogicalGetData().Clone();
                if (entity != null)
                {
                    string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                    if (!string.IsNullOrEmpty(clientID))
                    {
                        if (clientID.Split('|')[1] == "1")//终端(后台)
                        {
                            entity.TraceSecondId = clientID.Split('|')[0];
                            entity.TraceId = Guid.NewGuid().ToString("N");
                        }
                    }

                    myRequest.Headers.Add("BTProcessInfo", JsonHelper.SerializeObject(entity));
                }
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
            }
            
            string ReqResult = string.Empty;
            HttpWebResponse myResponse = null;
            StreamReader reader = null;
            try
            {
                myResponse = (HttpWebResponse) myRequest.GetResponse();
                reader = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                ReqResult = reader.ReadToEnd();

                reader.Close();
                myResponse.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                if (reader != null)
                    reader.Close();
                if (myResponse != null)
                    myResponse.Close();
            }

            return ReqResult;
        }



        /// <summary>
        /// Get 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IRestResponse PostComplexData(string requestUri, RestRequest request, int time = 7000)
        {
            if (time == 0 || time > timeout)
            {
                time = timeout;
            }
            request.Timeout = time;
            try
            {
                ThreadLocalData entity = (ThreadLocalData)ThreadSlot.LogicalGetData().Clone();
                if (entity != null)
                {
                    string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                    if (!string.IsNullOrEmpty(clientID))
                    {
                        if (clientID.Split('|')[1] == "1")//终端(后台)
                        {
                            entity.TraceSecondId = clientID.Split('|')[0];
                            entity.TraceId = Guid.NewGuid().ToString("N");
                        }
                    }
                    request.AddHeader("BTProcessInfo", JsonHelper.SerializeObject(entity));
                }
               
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
            }
            var restClient = new RestClient {BaseUrl = new Uri(requestUri)};
            IRestResponse r = restClient.Execute(request);
            return r;
        }


        /// <summary>
        /// Post 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IRestResponse PostComplexData(string requestUri, RestRequest request, string medieaURI = "",
            string fileName = "", string jsonBody = "", Int32 time = 7000, string version = "1.0")
        {

            request.Method = Method.POST;
            if (time == 0 || time > timeout)
            {
                time = timeout;
            }
            request.Timeout = time;
            try
            {
                ThreadLocalData entity = (ThreadLocalData) ThreadSlot.LogicalGetData().Clone();
                if (entity != null)
                {
                    string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                    if (!string.IsNullOrEmpty(clientID))
                    {
                        if (clientID.Split('|')[1] == "1")//终端(后台)
                        {
                            entity.TraceSecondId = clientID.Split('|')[0];
                            entity.TraceId = Guid.NewGuid().ToString("N");
                        }
                    }
                    request.AddHeader("BTProcessInfo", JsonHelper.SerializeObject(entity));
                }
                
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
            }

            if (!string.IsNullOrEmpty(medieaURI))
            {
                byte[] bufferBest = GetImage(medieaURI);
                if (string.IsNullOrEmpty(fileName))
                {
                    fileName = new Guid().ToString("N");
                }

                request.AddFileBytes(fileName, bufferBest, fileName);
            }
            if (!string.IsNullOrEmpty(jsonBody))
            {
                request.AddBody(jsonBody);
            }

            request.Timeout = time;
            var restClient = new RestClient {BaseUrl = new Uri(requestUri)};
            IRestResponse r = restClient.Execute(request);
            return r;
        }

        /// <summary>
        /// Post 
        /// </summary>
        /// <param name="requestUri"></param>
        /// <param name="json"></param>
        /// <returns></returns>
        public static IRestResponse PostComplexData(string requestUri, RestRequest request, string jsonBody = "",
            Int32 time = 7000, string version = "1.0")
        {

            request.Method = Method.POST;
            if (time == 0 || time > timeout)
            {
                time = timeout;
            }
            request.Timeout = time;
            try
            {
                ThreadLocalData entity = (ThreadLocalData) ThreadSlot.LogicalGetData().Clone();
                if (entity != null)
                {
                    string clientID = ThreadSlot.GetClientID();//System.Configuration.ConfigurationSettings.AppSettings["ClientID"];
                    if (!string.IsNullOrEmpty(clientID))
                    {
                        if (clientID.Split('|')[1] == "1")//终端(后台)
                        {
                            entity.TraceSecondId = clientID.Split('|')[0];
                            entity.TraceId = Guid.NewGuid().ToString("N");
                        }
                    }
                    request.AddHeader("BTProcessInfo", JsonHelper.SerializeObject(entity));
                }
                
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal(ex, "post请求添加追踪日志异常：" + ex.Message + ex.StackTrace);
            }
            if (!string.IsNullOrEmpty(jsonBody))
            {
                request.AddBody(jsonBody);
            }
            request.Timeout = time;

            var restClient = new RestClient {BaseUrl = new Uri(requestUri)};
            IRestResponse r = restClient.Execute(request);
            return r;
        }


      
        public static byte[] GetImage(string url)
        {
            try
            {
                WebClient wc = new WebClient();
                byte[] bytes;
             
                bytes = wc.DownloadData(url);
                return bytes;
            }
            catch (Exception ex)
            {
                LogService.Default.Fatal("下载的图片出错：" + url + ";" + ex.Message);
                throw ex;
            }
        }
    }



}
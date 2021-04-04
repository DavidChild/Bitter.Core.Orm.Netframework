using Bitter.Tools;
using Bitter.Tools.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using RestSharp;
using System.Linq;

namespace Bitter.Base
{
    /********************************************************************************
    ** auth： davidchild
    ** date： 2017/3/2 13:59:55
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2016 Bitter 版权所有。
    *********************************************************************************/
    public class RequestProvider
    {
      


        private static void SetIsWriteLog(string uri,out bool writerequestlog, out bool writeresponelog)
        {
            writerequestlog = true; //默认为写入
            writeresponelog = true;
            try
            {


                List<LogSwitchDto> logswtichlistb = Configsetting.Appsettings.TryGet<List<LogSwitchDto>>("Logswitch");

                if (logswtichlistb != null && logswtichlistb.Count > 0)
                {
                    var httplogswtichs = logswtichlistb.Where(o => o.LogType == enum_logswitchtype.Http请求日志).ToList();
                    if (httplogswtichs != null && logswtichlistb.Count > 0)
                    {
                        var restClient = new RestClient { BaseUrl = new Uri(uri) };
                        foreach (var item in logswtichlistb)
                        {
                            if (restClient.BaseUrl.AbsolutePath.ToLower() == item.MatchRule.ToLower())
                            {
                                writerequestlog = false;
                                writeresponelog = false;
                                break;
                            }

                        }
                    }

                }


                string logfileter = Configsetting.Appsettings["logfilter"].ToSafeString("");

                if (!string.IsNullOrEmpty(logfileter))
                {

                    List<LogSwitchDto> logswtichlist = JsonConvert.DeserializeObject<List<LogSwitchDto>>(logfileter);
                    foreach (var item in logswtichlist)
                    {
                        if (uri.ToLower().Contains(item.MatchRule.ToLower()))
                        {

                            if ( item.request)
                            {
                                writerequestlog = false;
                               
                            }
                            if (item.repones)
                            {
                                writeresponelog = false;
                               
                            }
                            break;
                        }
                       
                    }
                }

            }
            catch (Exception ex)
            {
                LogService.Default.Fatal("find the settig log filter setting error:"+ex.Message);
            }
           
        }

        public static Result SyncRequest(RestBag restBag)
        {
            Result re = new Result();
            string sentJson = string.Empty;
              bool writerequestlog;
            bool writeresponelog;
            SetIsWriteLog(restBag.uri, out writerequestlog,out writeresponelog);
            try
            {

                dynamic dy = new
                {

                    JsonBody = restBag.req.JsonSerializer.ToString(),
                    RestSharpParameter = restBag.req.Parameters

                };
                var dyJson = JsonConvert.SerializeObject(dy);
               
                if (writerequestlog)
                    LogService.Default.Trace("发送跟踪日志：" + dyJson + "，目标地址：" + restBag.uri);//日志记录
                                                                                          // 定义超时重试机制
                                                                                          //var policy = Policy.HandleResult<IRestResponse>(r =>
                                                                                          //{
                                                                                          //    return r.StatusCode == HttpStatusCode.RequestTimeout || r.StatusCode == HttpStatusCode.GatewayTimeout || r.StatusCode == 0;
                                                                                          //}).Or<TimeoutException>().Retry(3);

                //IRestResponse rjson = policy.Execute(() =>
                //{
                //    return ApiUtil.RequestData(restBag).Result;
                //});

                IRestResponse rjson = ApiUtil.RequestData(restBag).Result;
                if (writeresponelog)
                {
                    LogService.Default.Trace("返回跟踪日志：" + JsonConvert.SerializeObject("RestSharp_HTTP_StatusCode:" + rjson.StatusCode + "RestSharp_HTTP_Content:" + rjson.Content + "目标地址：" + restBag.uri));//日志记录););//日志记录
                }
                re.errorCode = ((int)rjson.StatusCode).ToSafeString();
                if ((int)rjson.StatusCode == 200)
                {
                    re.@object = rjson.Content;
                    re.code = 1;
                }
                else
                {
                    re.@object = rjson.Content;
                    re.message = rjson.ErrorMessage ?? "";
                    if (string.IsNullOrWhiteSpace(re.message))
                        re.message = rjson.Content;
                    re.code = 0;
                    LogService.Default.Trace("返回跟踪错误响应日志：result: message" + re.message + "目标地址：" + restBag.uri);//日志记录);
                }


            }
            catch (Exception ex)
            {
                re.code = 0;
                re.message = ex.Message;
                LogService.Default.Fatal(ex);
            }
            return re;
        }

        public static Result AnsyRequest(RestBag restBag)
        {
            Result re = new Result();
            string sentJson = string.Empty;

            try
            {

                dynamic dy = new
                {

                    JsonBody = restBag.req.JsonSerializer.ToString(),
                    RestSharpParameter = restBag.req.Parameters

                };
                var dyJson = JsonConvert.SerializeObject(dy);
                bool writerequestlog;
                bool writeresponelog;
                SetIsWriteLog(restBag.uri, out writerequestlog, out writeresponelog);

                if (writerequestlog)
                {
                    LogService.Default.Trace("发送跟踪日志：" + dyJson + "，目标地址：" + restBag.uri);//日志记录
                }
                
                ApiUtil.RequestData(restBag);
            }
            catch (Exception ex)
            {
                re.code = 0;
                re.message = ex.Message;
                LogService.Default.Fatal(ex);
            }
            return re;
        }


        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="requestData">请求数据信息</param>
        /// <returns>返回最终的Result</returns>
        public static Result FileDownLoad(string jsonResult, string ApiUri)
        {
            Result re = new Result();
            try
            {
                var sentJson = jsonResult;
                LogService.Default.Trace("发送跟踪日志：" + sentJson + "，目标地址：" + ApiUri);//日志记录
                Stream str = ApiUtil.FileDownLoad(ApiUri, sentJson);
                LogService.Default.Trace("返回跟踪日志：" + str.ToSafeString());//日志记录
                re.@object = str;
                re.code = 1;
            }
            catch (Exception ex)
            {
                re.code = 0;
                re.message = ex.Message;
            }
            return re;
        }

        #endregion 下载文件
    }
}
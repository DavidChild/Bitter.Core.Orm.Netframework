using CacheManager.Core;
using System;
using System.Collections.Generic;
using System.Text;
using Bitter.Tools.Utils;
using System.Net;
using Bitter.Tools;

namespace Bitter.Base
{
    public class CacheInstace<T>
    {


        private static ICacheManager<T> memerySingleton { get; set; }
        private static ICacheManager<T> redisCacheSingleton { get; set; }
        //获取框架Redis配置信息
        private static RedisConfigInfo redisConfigInfo = RedisConfigInfo.GetConfig();
        private static string[] redisReadServerList = redisConfigInfo.ReadServerList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        private static string[] redisWriteServerList = redisConfigInfo.WriteServerList.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
        private static readonly object lockobjk = string.Empty;
        /// <summary>
        /// 获取Redis缓存实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static CacheManager.Core.ICacheManager<T> GetRedisInstace()
        {
         
           
            if (redisCacheSingleton == null)
            {
                lock (lockobjk)
                {
                    if (redisCacheSingleton == null)
                    {
                        redisCacheSingleton = (CacheManager.Core.CacheFactory.Build<T>(settings =>
                        {

                            settings
                                .WithRedisConfiguration("redis", config =>//Redis缓存配置
                    {
                                    config.WithAllowAdmin()
                                        .WithDatabase(0)
                                        .WithEndpoint((GetRedisIP().Split(':')[0]).ToSafeString(""), (GetRedisIP().Split(':')[1]).ToSafeInt32(0));

                                })
                                .WithMaxRetries(1000)//尝试次数
                                .WithRetryTimeout(100)//尝试超时时间
                                .WithJsonSerializer()
                                .WithRedisCacheHandle("redis", true);//redis缓存handle

                        }));
                        return redisCacheSingleton;

                    }
                    else
                    {
                        return redisCacheSingleton;
                    }
                }
            }
            else
            {
                return redisCacheSingleton;
            }





            
        }


        /// <summary>
        /// 获取内存缓存单例实例
        /// <typeparam name="T"></typeparam>
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static ICacheManager<T> GetMemeryInstace()
        {

            if (memerySingleton == null)
            {
                lock (lockobjk)
                {
                    if (memerySingleton == null)
                    {
                        //memerySingleton = CacheManager.Core.CacheFactory.Build<T>("StartedMemoryCacheBlock",
                        //settings => {
                        //    settings.WithMicrosoftMemoryCacheHandle("inMyProcessCache");
                        //});
                        return memerySingleton;
                    }
                    else
                    {
                        return memerySingleton;
                    }
                }
            }
            else
            {
                return memerySingleton;
            }

        }


    


        /// <summary>
        /// 获取二级缓存实例,一级缓存本地内存，二级缓存Redis。 本地内存的依赖蓝本数据为Redis为准。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public static CacheManager.Core.ICacheManager<T> GetMultilevelInstace()
        {

          
         
            
            return (CacheManager.Core.CacheFactory.Build<T>(settings => 
            {

                //settings.WithMicrosoftMemoryCacheHandle("inProcessCache")//内存缓存Handle
                //    .And
                //    .WithRedisConfiguration("redis", config =>//Redis缓存配置
                //    {
                //     config.WithAllowAdmin()
                //            .WithDatabase(0)
                //            .WithEndpoint((GetRedisIP().Split(':')[0]).ToSafeString(""), (GetRedisIP().Split(':')[1]).ToSafeInt32(0));
                //    })
                //    .WithMaxRetries(1000)//尝试次数
                //    .WithRetryTimeout(100)//尝试超时时间
                //    .WithRedisBackplane("redis") //redis使用Back Plate
                //    .WithJsonSerializer()
                //    .WithRedisCacheHandle("redis", true);//redis缓存handle
            }));
        }


        /// <summary>
        /// 检查是否配置了redis
        /// </summary>
        /// <returns></returns>
        public static bool CheckedRedisConsetting()
        {
            if (redisWriteServerList.Length <= 0)
            {
                return false;
            }
            var rediscon = redisWriteServerList[0];
            return string.IsNullOrEmpty(rediscon) ? false : true;
        }

        /// <summary>
        /// 获取redis 配置信息
        /// </summary>
        /// <returns></returns>
        public static string  GetRedisIP()
        {
            if (redisWriteServerList.Length <= 0)
            {
                return "";
            }
            var rediscon = redisWriteServerList[0];
            return rediscon;
        }




        /// <summary>
        /// 获取缓存实例，优先redis,默认为内存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ICacheManager<T> GetRedisInstanceDefaultMemery()
        {
            if (CheckedRedisConsetting())
            {
                return GetRedisInstace();
            } else
            {
                return GetMemeryInstace();
            }
        }
    }
}

﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Bitter.Tools
{
    public sealed class BTCallContextData
    {
        /// <summary>
        /// 日志跟踪存储键
        /// </summary>
        public static readonly string LogTraceKey = "BTTrace";

        static ConcurrentDictionary<string, ThreadLocal<object>> state = new ConcurrentDictionary<string, ThreadLocal<object>>();

        public static void SetData(string name, object data) =>
            state.GetOrAdd(name, _ => new ThreadLocal<object>()).Value = data;

        public static object GetData(string name) =>
            state.TryGetValue(name, out ThreadLocal<object> data) ? data.Value : null;

        public static bool Remove(string name)
        {
            return state.TryRemove(name, out ThreadLocal<object> data);
        }

        public static int Count()
        {
            return state.Keys.Count;
        }

        public static object CurrentNlogTraceData
        {
            get
            {
                object o = GetData(LogTraceKey);
                if (o == null)
                {
                    o = new ThreadLocalData() { TraceId = Guid.NewGuid().ToString("N"), StartTime = DateTime.Now, TraceSecondId = AppDomain.CurrentDomain.FriendlyName, FromUrl = "Localhost" };
                    SetData(LogTraceKey, o);
                }

                return o;
            }
            
        }
    }
}

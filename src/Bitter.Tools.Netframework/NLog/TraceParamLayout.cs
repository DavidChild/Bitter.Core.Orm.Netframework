using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;
using NLog.LayoutRenderers;
using System.Runtime.Remoting.Messaging;

namespace Bitter.Tools
{
    /********************************************************************************
    ** auth： weiyz
    ** date： 2018/2/6
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2018 Bitter 版权所有。
    *********************************************************************************/

    [LayoutRenderer("bttraceid")]
    public sealed class TraceIDLayoutRenderer : LayoutRenderer
    {

        public string BTTraceID { get {

                if(ThreadSlot.LogicalGetData()!=null)
                return ThreadSlot.LogicalGetData().TraceId;
                return Guid.NewGuid().ToString("N");
            } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {

            builder.Append(BTTraceID);

        }
    }

    [LayoutRenderer("bttracesecondid")]
    public sealed class TraceSecondIDLayoutRenderer : LayoutRenderer
    {

        public string BTTraceSecondID { get { return ThreadSlot.LogicalGetData().TraceSecondId; } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {

            builder.Append(BTTraceSecondID);

        }
    }

    [LayoutRenderer("bttracelevel")]
    public sealed class TraceLevelLayoutRenderer : LayoutRenderer
    {

        public string BTTraceLevel { get { return ThreadSlot.LogicalGetData().TraceLayer; } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(BTTraceLevel);

        }
    }

    [LayoutRenderer("bttracefrom")]
    public sealed class TraceFromLayoutRenderer : LayoutRenderer
    {

        public string BTTraceFrom { get { return ThreadSlot.LogicalGetData().FromUrl; } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(BTTraceFrom);

        }
    }

    [LayoutRenderer("bttraceto")]
    public sealed class TraceToLayoutRenderer : LayoutRenderer
    {

        public string BTTraceTo { get { return ThreadSlot.LogicalGetData().ToUrl; } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(BTTraceTo);

        }
    }

    [LayoutRenderer("bttraceinfo")]
    public sealed class TraceInfoLayoutRenderer : LayoutRenderer
    {

        public string bttraceinfo { get { return ThreadSlot.GetThreadTraceInfo(); } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(bttraceinfo);

        }
    }

    /// <summary>
    /// 日志文件目录
    /// </summary>
    [LayoutRenderer("btlogdir")]
    public sealed class NlogDirNameLayoutRenderer : LayoutRenderer
    {

        public string BTLogDir { get { return LogService.GetLogDirName(); } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(BTLogDir);

        }
    }


    /// <summary>
    /// 日志文件目录
    /// </summary>
    [LayoutRenderer("servicename")]
    public sealed class ServiceNameLayoutRenderer : LayoutRenderer
    {

        public string serviceName { get { return LogService.GetLogDirName(); } }

        protected override void Append(StringBuilder builder, LogEventInfo ev)
        {
            // 最终添加给指定的StringBuilder 

            builder.Append(serviceName);

        }
    }
}

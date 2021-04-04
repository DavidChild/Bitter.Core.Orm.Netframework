using NLog;
using NLog.LayoutRenderers;
using System;
using NLog.Targets;
using System.Runtime.Remoting.Messaging;
using System.ComponentModel;

namespace Bitter.Tools
{
    /********************************************************************************
    ** auth： Jason
    ** date： 2017/2/28 17:22:58
    ** desc：
    ** Ver.:  V1.0.0
    ** Copyright (C) 2016 Bitter 版权所有。
    *********************************************************************************/

    public class LogService
    {

        private static string ServiceName;

        private NLog.Logger logger;

        static LogService()
        {
            //注册Nlog布局器(输出参数)
            //LayoutRenderer.Register("bttraceid", typeof(TraceIDLayoutRenderer));
            //LayoutRenderer.Register("bttracelevel", typeof(TraceLevelLayoutRenderer));
            //LayoutRenderer.Register("bttracefrom", typeof(TraceFromLayoutRenderer));
            //LayoutRenderer.Register("bttraceto", typeof(TraceToLayoutRenderer));
            //LayoutRenderer.Register("bttracesecondid", typeof(TraceSecondIDLayoutRenderer));
            LayoutRenderer.Register("bttraceinfo", typeof(TraceInfoLayoutRenderer));
            LayoutRenderer.Register("btlogdir", typeof(NlogDirNameLayoutRenderer));
            LayoutRenderer.Register("servicename", typeof(ServiceNameLayoutRenderer));
            //委托一个日志写入前事件
            //MethodCallTarget target = new MethodCallTarget();
            //target.ClassName = typeof(LogService).AssemblyQualifiedName;
            //target.MethodName = "BeforeLog";
            //target.Parameters.Add(new MethodCallParameter("${level}"));
            //target.Parameters.Add(new MethodCallParameter("${message}"));

            //NLog.Config.SimpleConfigurator.ConfigureForTargetLogging(target, LogLevel.Trace);

            //Default = NLog.LogManager.GetCurrentClassLogger();
            Default = new NLogDefault();

        }

        public static void BeforeLog(string level, string message)
        {
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
 //Default.Trace(message);
                //Tools.Helper.LogHelper.WriteLog($"{level}:{message}");

            }
               
        }

        public LogService(string name)
            : this(NLog.LogManager.GetLogger(name))
        {
        }

        private LogService(NLog.Logger logger)
        {
            this.logger = logger;
        }

        /// <summary>
        /// 封装一层  weiyz 20191212
        /// </summary>
        public static NLogDefault Default { get; private set; }

        //public static Logger Default { get; private set; }

        /// <summary>
        /// 写入调试目录
        /// </summary>
        /// <param name="msg"></param>
        private void logDebugWay(string msg)
        {
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }

        #region Debug
        public void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
            logDebugWay(msg);
        }

        public void Debug(string msg, Exception err)
        {
            logger.Debug(msg, err);
            logDebugWay(msg);
        }

        public void Debug(string msg)
        {
            logger.Debug(msg);
            logDebugWay(msg);
        }




        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, float argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, double argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, object argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(string message, sbyte argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, long argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(string message, uint argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, decimal argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        //
        //   arg3:
        //     Third argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(string message, object arg1, object arg2, object arg3)
        {
            logger.Debug(message, arg1, arg2, arg3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, string argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, byte argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, char argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, bool argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(string message, object arg1, object arg2)
        {
            logger.Debug(message, arg1, arg2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(IFormatProvider formatProvider, object value)
        {
            logger.Debug(formatProvider, value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level.
        //
        // 参数:
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(object value)
        {
            logger.Debug(value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Debug(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Debug(string message, int argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Debug(string message, ulong argument)
        {
            logger.Debug(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level.
        //
        // 参数:
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Debug<T>(T value)
        {
            logger.Debug<T>(value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameter
        //     and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Debug<TArgument>(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Debug level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, formatProvider, message, args);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Debug<TArgument>(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Debug<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Debug<TArgument1, TArgument2>(message, argument1, argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Debug<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Debug(message, argument1, argument2, argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level using the specified parameters
        //     and formatting them with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Debug level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Debug(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Debug(exception, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level.
        //
        // 参数:
        //   messageFunc:
        //     A function returning message to be written. Function is not evaluated if logging
        //     is not enabled.
        public void Debug(LogMessageGenerator messageFunc)
        {
            logger.Debug(messageFunc);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Debug level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Debug<T>(IFormatProvider formatProvider, T value)
        {
            logger.Debug<T>(formatProvider, value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Debug level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        public void Debug(Exception exception, [Localizable(false)] string message)
        {
            logger.Debug(exception, message);
            logDebugWay(message);
        }

        #endregion

        #region Error

        public void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
            logDebugWay(msg);
        }

        public void Error(string msg, Exception err)
        {
            logger.Error(msg, err);
            logDebugWay(msg);
        }

        public void Error(string msg)
        {
            logger.Error(msg);
            logDebugWay(msg);
        }




        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, string argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, byte argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, char argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, bool argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        //
        //   arg3:
        //     Third argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(string message, object arg1, object arg2, object arg3)
        {
            logger.Error(message, arg1, arg2, arg3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level.
        //
        // 参数:
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(object value)
        {
            logger.Error(value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, double argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, decimal argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, object argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(string message, sbyte argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(string message, uint argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(string message, ulong argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, float argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, long argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(string message, int argument)
        {
            logger.Error(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Error(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Error(IFormatProvider formatProvider, object value)
        {
            logger.Error(formatProvider, value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Error(string message, object arg1, object arg2)
        {
            logger.Error(message, arg1, arg2);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Error<TArgument1, TArgument2, TArgument3>(message, argument1, argument2, argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Error<TArgument1, TArgument2>(formatProvider, message, argument1, argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Error<TArgument>(message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameter
        //     and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Error<TArgument>(formatProvider, message, argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Error level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Error(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Error level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        public void Error(Exception exception, [Localizable(false)] string message)
        {
            logger.Error(exception, message);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Error level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(exception, formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameters
        //     and formatting them with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Error(formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Error<TArgument1, TArgument2, TArgument3>(formatProvider, message, argument1, argument2, argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level.
        //
        // 参数:
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Error<T>(T value)
        {
            logger.Error<T>(value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Error<TArgument1, TArgument2>(message, argument1, argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level.
        //
        // 参数:
        //   messageFunc:
        //     A function returning message to be written. Function is not evaluated if logging
        //     is not enabled.
        public void Error(LogMessageGenerator messageFunc)
        {
            logger.Error(messageFunc);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Error level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Error<T>(IFormatProvider formatProvider, T value)
        {
            logger.Error<T>(formatProvider, value);
        }

        #endregion

        #region Fatal

        /// <summary>
        /// 指定参数记录诊断信息
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="args">object[]</param>
        public void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
            logDebugWay(msg);
        }

        /// <summary>
        /// 程序出错了,记录Exception信息
        /// </summary>
        /// <param name="err">Exception</param>
        public void Fatal(string msg, Exception err)
        {
            logger.Fatal(msg, err);
            logDebugWay(msg);
        }

        /// <summary>
        /// 程序出错了,记录Exception信息
        /// </summary>
        /// <param name="err">Exception</param>
        public void Fatal(Exception err)
        {
            logger.Fatal(err);
            logDebugWay(err.Message);
        }

        public void Fatal(string msg)
        {
            logger.Fatal(msg);
            logDebugWay(msg);
        }




        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, long argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }


        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(string message, sbyte argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, decimal argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(string message, uint argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, double argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, float argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, object argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, int argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, string argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, byte argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, char argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(string message, bool argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(string message, object arg1, object arg2, object arg3)
        {
            logger.Fatal( message,  arg1,  arg2,  arg3);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Fatal<T>(IFormatProvider formatProvider, T value)
        {
            logger.Fatal<T>( formatProvider,  value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(string message, object arg1, object arg2)
        {
            logger.Fatal( message,  arg1,  arg2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(IFormatProvider formatProvider, object value)
        {
            logger.Fatal( formatProvider,  value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level.
        //
        // 参数:
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Fatal(object value)
        {
            logger.Fatal( value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Fatal( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level.
        //
        // 参数:
        //   messageFunc:
        //     A function returning message to be written. Function is not evaluated if logging
        //     is not enabled.
        public void Fatal(LogMessageGenerator messageFunc)
        {
            logger.Fatal( messageFunc);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Fatal<TArgument1, TArgument2, TArgument3>(message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Fatal level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        public void Fatal(Exception exception, [Localizable(false)] string message)
        {
            logger.Fatal( exception, message);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Fatal level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Fatal(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal( exception, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Fatal level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal( exception,  formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameter
        //     and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Fatal<TArgument>( formatProvider, message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Fatal<TArgument>(message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Fatal<TArgument1, TArgument2>( formatProvider, message,  argument1,  argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Fatal<TArgument1, TArgument2>(message,  argument1,  argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Fatal<TArgument1, TArgument2, TArgument3>( formatProvider, message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified parameters
        //     and formatting them with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Fatal( formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Fatal level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Fatal(string message, ulong argument)
        {
            logger.Fatal( message,  argument);
            logDebugWay(message);
        }

        #endregion

        #region Info

        public void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
            logDebugWay(msg);
        }

        public void Info(string msg, Exception err)
        {
            logger.Info(msg, err);
            logDebugWay(msg);
        }

        public void Info(string msg)
        {
            logger.Info(msg);
            logDebugWay(msg);
        }



        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Info<TArgument1, TArgument2, TArgument3>( formatProvider, message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Info<T>(IFormatProvider formatProvider, T value)
        {
            logger.Info<T>( formatProvider,  value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level.
        //
        // 参数:
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Info<T>(T value)
        {
            logger.Info<T>(value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, double argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, float argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, long argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, int argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, string argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, byte argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, char argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, bool argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level.
        //
        // 参数:
        //   messageFunc:
        //     A function returning message to be written. Function is not evaluated if logging
        //     is not enabled.
        public void Info(LogMessageGenerator messageFunc)
        {
            logger.Info( messageFunc);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameters
        //     and formatting them with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Info( formatProvider,  message, args);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Info<TArgument1, TArgument2, TArgument3>(message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Info<TArgument1, TArgument2>( formatProvider, message,  argument1,  argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Info<TArgument>(message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(string message, ulong argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(string message, uint argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(string message, sbyte argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Info<TArgument1, TArgument2>( message,  argument1,  argument2);
            logDebugWay(message);

        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, decimal argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        //
        //   arg3:
        //     Third argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(string message, object arg1, object arg2, object arg3)
        {
            logger.Info( message,  arg1,  arg2,  arg3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameter
        //     and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Info<TArgument>( formatProvider, message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Info level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Info( exception,  formatProvider, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Info level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Info(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Info( exception, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Info level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        public void Info(Exception exception, [Localizable(false)] string message)
        {
            logger.Info( exception, message);
            logDebugWay(message);
        }

        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(string message, object argument)
        {
            logger.Info( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Info( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Info(string message, object arg1, object arg2)
        {
            logger.Info( message,  arg1,  arg2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level.
        //
        // 参数:
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(object value)
        {
            logger.Info( value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Info level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Info(IFormatProvider formatProvider, object value)
        {
            logger.Info( formatProvider,  value);
            logDebugWay(value.ToString());
        }

        #endregion

        /// <summary>
        /// 用于记录框架执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public void LogSql(string sql)
        {
            logger.Info("执行sql:" + sql);
        }

        #region Trace


        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, float argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, string argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        //
        //   arg3:
        //     Third argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(string message, object arg1, object arg2, object arg3)
        {
            logger.Trace( message,  arg1,  arg2,  arg3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, bool argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, char argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, char argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, byte argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, byte argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   arg1:
        //     First argument to format.
        //
        //   arg2:
        //     Second argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(string message, object arg1, object arg2)
        {
            logger.Trace( message,  arg1,  arg2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, string argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, int argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, long argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, long argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, float argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, double argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, double argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, int argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(IFormatProvider formatProvider, object value)
        {
            logger.Trace( formatProvider,  value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   value:
        //     A object to be written.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(object value)
        {
            logger.Trace( value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Trace<TArgument1, TArgument2, TArgument3>(message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Trace<T>(T value)
        {
            logger.Trace<T>( value);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   value:
        //     The value to be written.
        //
        // 类型参数:
        //   T:
        //     Type of the value.
        public void Trace<T>(IFormatProvider formatProvider, T value)
        {
            logger.Trace<T>( formatProvider,  value);
            logDebugWay(value.ToString());
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   messageFunc:
        //     A function returning message to be written. Function is not evaluated if logging
        //     is not enabled.
        public void Trace(LogMessageGenerator messageFunc)
        {
            logger.Trace( messageFunc);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters
        //     and formatting them with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace( formatProvider,  message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level.
        //
        // 参数:
        //   message:
        //     Log message.
        public void Trace([Localizable(false)] string message)
        {
            logger.Trace(message);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing format items.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Trace([Localizable(false)] string message, params object[] args)
        {
            logger.Trace(message,args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Trace level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        // 备注:
        //     This method was marked as obsolete before NLog 4.3.11 and it may be removed in
        //     a future release.
        [Obsolete("Use Trace(Exception exception, string message, params object[] args) method instead. Marked obsolete before v4.3.11")]
        public void Trace([Localizable(false)] string message, Exception exception)
        {
            logger.Trace(message,  exception);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Trace level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        public void Trace(Exception exception, [Localizable(false)] string message)
        {
            logger.Trace( exception, message);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Trace level.
        //
        // 参数:
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Trace(Exception exception, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace( exception, message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message and exception at the Trace level.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string to be written.
        //
        //   exception:
        //     An exception to be logged.
        //
        //   args:
        //     Arguments to format.
        [MessageTemplateFormatMethod("message")]
        public void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args)
        {
            logger.Trace( exception,  formatProvider,  message, args);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameter
        //     and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument)
        {
            logger.Trace<TArgument>( formatProvider, message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        //
        // 类型参数:
        //   TArgument:
        //     The type of the argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument>([Localizable(false)] string message, TArgument argument)
        {
            logger.Trace<TArgument>(message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Trace<TArgument1, TArgument2>( formatProvider, message,  argument1,  argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified parameters.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2)
        {
            logger.Trace<TArgument1, TArgument2>(message,  argument1,  argument2);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified arguments
        //     formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument1:
        //     The first argument to format.
        //
        //   argument2:
        //     The second argument to format.
        //
        //   argument3:
        //     The third argument to format.
        //
        // 类型参数:
        //   TArgument1:
        //     The type of the first argument.
        //
        //   TArgument2:
        //     The type of the second argument.
        //
        //   TArgument3:
        //     The type of the third argument.
        [MessageTemplateFormatMethod("message")]
        public void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3)
        {
            logger.Trace<TArgument1, TArgument2, TArgument3>( formatProvider, message,  argument1,  argument2,  argument3);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, decimal argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, decimal argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, bool argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void Trace(string message, object argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, sbyte argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(string message, sbyte argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, uint argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(string message, uint argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, ulong argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter.
        //
        // 参数:
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [CLSCompliant(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(string message, ulong argument)
        {
            logger.Trace( message,  argument);
            logDebugWay(message);
        }
        //
        // 摘要:
        //     Writes the diagnostic message at the Trace level using the specified value as
        //     a parameter and formatting it with the supplied format provider.
        //
        // 参数:
        //   formatProvider:
        //     An IFormatProvider that supplies culture-specific formatting information.
        //
        //   message:
        //     A string containing one format item.
        //
        //   argument:
        //     The argument to format.
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MessageTemplateFormatMethod("message")]
        public void Trace(IFormatProvider formatProvider, string message, object argument)
        {
            logger.Trace( formatProvider,  message,  argument);
            logDebugWay(message);
        }

        #endregion

        /// <summary>
        /// 设置服务名称（日志注册）
        /// </summary>
        /// <param name="name"></param>
        public static void SetServiceName(string serviceName)
        {
            ServiceName = serviceName;
        }


        /// <summary>
        /// 设置日志文件夹目录名称
        /// </summary>
        /// <param name="name"></param>
        public static void SetLogDirName(string name)
        {
            ServiceName = name;
        }

        /// <summary>
        /// 获取日志文件夹目录名称
        /// </summary>
        /// <returns></returns>
        internal static string GetLogDirName()
        {
            return ServiceName;
        }
    }
}
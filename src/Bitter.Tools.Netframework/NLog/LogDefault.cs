using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools
{
    public class NLogDefault
    {
        private static Logger logger;
        public NLogDefault()
        {
          
                logger = NLog.LogManager.GetCurrentClassLogger(); ;
         }

        public void Debug(string msg, params object[] args)
        {
            logger.Debug(msg, args);
        }

        public void Debug(string msg, Exception err)
        {
            logger.Debug(msg, err);
        }

        public void Debug(string msg)
        {
            logger.Debug(msg);
            
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
               logger.Warn(msg);
            }
        }

        public void Error(string msg, params object[] args)
        {
            logger.Error(msg, args);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg, args);
            }
        }

        public void Error(string msg, Exception err)
        {
            logger.Error(msg, err);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg,err);
            }
        }

        public void Error(string msg)
        {
            logger.Error(msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }

        public void Error(Exception ex)
        {
            logger.Error(ex);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(ex);
            }
        }

        public void Error(Exception ex,string msg)
        {
            logger.Error(ex,msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(ex,msg);
            }
        }

        /// <summary>
        /// 指定参数记录诊断信息
        /// </summary>
        /// <param name="msg">string</param>
        /// <param name="args">object[]</param>
        public void Fatal(string msg, params object[] args)
        {
            logger.Fatal(msg, args);
        }

        /// <summary>
        /// 程序出错了,记录Exception信息
        /// </summary>
        /// <param name="err">Exception</param>
        public void Fatal(string msg, Exception err)
        {
            logger.Fatal(msg, err);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg,err);
            }
        }

        /// <summary>
        /// 程序出错了,记录Exception信息
        /// </summary>
        /// <param name="err">Exception</param>
        public void Fatal(Exception err)
        {
            logger.Fatal(err);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(err);
            }
        }

        public void Fatal(Exception err,string msg)
        {
            logger.Fatal(err,msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(err,msg);
            }
        }

        public void Fatal(string msg)
        {
            logger.Fatal(msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }

        public void Info(string msg, params object[] args)
        {
            logger.Info(msg, args);
        }

        public void Info(string msg, Exception err)
        {
            logger.Info(msg, err);
        }

        public void Info(string msg)
        {
            logger.Info(msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }

        /// <summary>
        /// 用于记录框架执行SQL
        /// </summary>
        /// <param name="sql"></param>
        public void LogSql(string sql)
        {
            logger.Info("执行sql:" + sql);
        }

        public void Trace(string msg, params object[] args)
        {
            logger.Trace(msg, args);
        }

        public void Trace(string msg, Exception err)
        {
            logger.Trace(msg, err);
        }

        public void Trace(string msg)
        {
            logger.Trace(msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }


        public void Warn(string msg, params object[] args)
        {
            logger.Warn(msg, args);
        }

        public void Warn(string msg, Exception err)
        {
            logger.Warn(msg, err);
        }

        public void Warn(string msg)
        {
            logger.Warn(msg);
            if (ThreadSlot.LogicalGetData()!=null&&ThreadSlot.LogicalGetData().IsDebug)
            {
                //记录到表里
                logger.Warn(msg);
            }
        }
    }
}

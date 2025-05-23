﻿using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace Bitter.Tools.Helper
{
    public class LogHelper
    {
        /// <summary>
        /// 根据错误写日志
        /// </summary>
        /// <param name="e">报错信息</param>
        /// <param name="exceptionFile">log文件地址</param>
        public static void ExceptionLog(Exception e, string exceptionFile)
        {
            string logPath = ConfigurationSettings.AppSettings["LogPath"].ToString();

            var sb = new StringBuilder();
            sb.Append("# Begin # ");
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}:{1}\r\n", e.Message, e.StackTrace + ((e.InnerException != null) ? e.InnerException.Message : ""));
            sb.Append("# Over #\r\n");
            WriteLog(logPath + @"\error\", string.Format("{0}.{1}.log", DateTime.Now.ToString("yyyy-M-d"), exceptionFile), sb.ToString());
        }

        /// <summary>
        /// 记录错误到sql日志
        /// </summary>
        /// <param name="e"></param>
        public static void SqlExceptionLog(Exception e)
        {
            
            string logPath = ConfigurationSettings.AppSettings["LogPath"].ToString();
            var sb = new StringBuilder();
            sb.Append("# Begin # ");
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}:{1}\r\n", e.Message, e.StackTrace + ((e.InnerException != null) ? e.InnerException.Message : ""));
            sb.Append("# Over #\r\n");
            WriteLog(logPath + @"\sql\", string.Format("{0}.log", DateTime.Now.ToString("yyyy-M-d")), sb.ToString());
        }

        /// <summary>
        /// 写文本信息到syslogs日志里
        /// </summary>
        /// <param name="contentText"></param>
        /// <param name="logTypeName"></param>
        public static void WriteLog(string contentText, string logTypeName = "SysLogs")
        {
            string logPath = ConfigurationSettings.AppSettings["LogPath"].ToString();
            var sb = new StringBuilder();
            sb.AppendFormat("Time:[{0}]\r\n", DateTime.Now.ToString("HH:mm:ss"));
            sb.AppendFormat("Message:{0}\r\n", contentText);
            string folder = string.IsNullOrWhiteSpace(logTypeName) ? "SysLogs" : logTypeName;
            WriteLog(logPath + @"\" + folder + @"\", string.Format("{0}.log", DateTime.Now.ToString("yyyy-M-d")), sb.ToString());
        }

        public static void WriteLog(string dirName, string fileName, string contentText)
        {
            if (!Directory.Exists(dirName))
            {
                Directory.CreateDirectory(dirName);
            }
            string logName = dirName + fileName;

            FileStream logFile = null;
            StreamWriter streamWriter = null;

            try
            {
                logFile = new FileStream(logName, FileMode.OpenOrCreate);
                logFile.Seek(0, SeekOrigin.End);
                streamWriter = new StreamWriter(logFile, Encoding.GetEncoding("gb2312"));
                streamWriter.WriteLine(contentText);
            }
            catch (Exception)
            {
            }
            finally
            {
                if (streamWriter != null) streamWriter.Close();
                if (logFile != null) logFile.Close();
            }
        }
    }
}
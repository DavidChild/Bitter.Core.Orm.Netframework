﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Bitter.Base.Hangfire
{
    /// <summary>
    /// 任务基类
    /// </summary>
    /// <typeparam name="TArgs"></typeparam>
    public interface IBackgroundJob<in TArgs>
    {
        /// <summary>
        /// 执行任务的方法
        /// </summary>
        /// <param name="args"></param>
        void Execute(TArgs args);
    }
}

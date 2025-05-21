using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Core
{
    // 全局静态
    public static class TargetCustomMutilDb
    {
        internal static ITargetMultiDb targetMultiDb;
        public static void RegisterCustomMutilDb(ITargetMultiDb customMultiDbs) {
            if (targetMultiDb == null){
                targetMultiDb = customMultiDbs;
            }
        }
        internal static ITargetMultiDb getCustomTargetDbs() {
            return targetMultiDb;
        }
    }
}

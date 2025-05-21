using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Core
{

    // 通过ITargetMutilDb自定义寻找库
    public interface ITargetMultiDb{
        DatabaseProperty FindTargetDb(String targetDbKeyName);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    internal delegate TValue Hoisted<in TModel, out TValue>(TModel model, List<object> capturedConstants);
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    public class ConstantExpressionFingerprint: ExpressionFingerprint
    {
        public ConstantExpressionFingerprint(ExpressionType nodeType, Type type)
           : base(nodeType, type)
        {
            // There are no properties on ConstantExpression that are worth including in
            // the fingerprint.
        }

        public override bool Equals(object obj)
        {
            ConstantExpressionFingerprint other = obj as ConstantExpressionFingerprint;
            return (other != null)
                   && Equals(other);
        }
    }
}

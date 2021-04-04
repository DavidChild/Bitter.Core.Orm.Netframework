using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    internal sealed class LambdaExpressionFingerprint: ExpressionFingerprint
    {
        public LambdaExpressionFingerprint(ExpressionType nodeType, Type type)
           : base(nodeType, type)
        {
            // There are no properties on LambdaExpression that are worth including in
            // the fingerprint.
        }

        public override bool Equals(object obj)
        {
            LambdaExpressionFingerprint other = obj as LambdaExpressionFingerprint;
            return (other != null)
                   && Equals(other);
        }
    }
}

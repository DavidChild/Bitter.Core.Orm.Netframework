using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    public class DefaultExpressionFingerprint: ExpressionFingerprint
    {
        public DefaultExpressionFingerprint(ExpressionType nodeType, Type type)
           : base(nodeType, type)
        {
            // There are no properties on DefaultExpression that are worth including in
            // the fingerprint.
        }

        public override bool Equals(object obj)
        {
            DefaultExpressionFingerprint other = obj as DefaultExpressionFingerprint;
            return (other != null)
                   && Equals(other);
        }
    }
}

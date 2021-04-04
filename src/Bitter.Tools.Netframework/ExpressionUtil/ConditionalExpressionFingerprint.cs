using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
   public class ConditionalExpressionFingerprint: ExpressionFingerprint
    {
        public ConditionalExpressionFingerprint(ExpressionType nodeType, Type type)
            : base(nodeType, type)
        {
            // There are no properties on ConditionalExpression that are worth including in
            // the fingerprint.
        }

        public override bool Equals(object obj)
        {
            ConditionalExpressionFingerprint other = obj as ConditionalExpressionFingerprint;
            return (other != null)
                   && Equals(other);
        }
    }
}

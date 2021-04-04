using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    [ExcludeFromCodeCoverage]
    internal sealed class UnaryExpressionFingerprint: ExpressionFingerprint
    {
        public UnaryExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method)
           : base(nodeType, type)
        {
            // Other properties on UnaryExpression (like IsLifted / IsLiftedToNull) are simply derived
            // from Type and NodeType, so they're not necessary for inclusion in the fingerprint.

            Method = method;
        }

  
        public MethodInfo Method { get; }

        public override bool Equals(object obj)
        {
            UnaryExpressionFingerprint other = obj as UnaryExpressionFingerprint;
            return (other != null)
                   && Equals(Method, other.Method)
                   && Equals(other);
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Method);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}

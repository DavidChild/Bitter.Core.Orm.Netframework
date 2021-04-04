using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    internal sealed class MethodCallExpressionFingerprint: ExpressionFingerprint
    {
        public MethodCallExpressionFingerprint(ExpressionType nodeType, Type type, MethodInfo method)
           : base(nodeType, type)
        {
            // Other properties on MethodCallExpression (like the argument count) are simply derived
            // from Type and Indexer, so they're not necessary for inclusion in the fingerprint.

            Method = method;
        }

        
        public MethodInfo Method { get; }

        public override bool Equals(object obj)
        {
            MethodCallExpressionFingerprint other = obj as MethodCallExpressionFingerprint;
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

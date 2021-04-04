using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    [ExcludeFromCodeCoverage]
    internal sealed class ParameterExpressionFingerprint: ExpressionFingerprint
    {
        public ParameterExpressionFingerprint(ExpressionType nodeType, Type type, int parameterIndex)
           : base(nodeType, type)
        {
            ParameterIndex = parameterIndex;
        }

        // Parameter position within the overall expression, used to maintain alpha equivalence.
        public int ParameterIndex { get; }

        public override bool Equals(object obj)
        {
            ParameterExpressionFingerprint other = obj as ParameterExpressionFingerprint;
            return (other != null)
                   && (ParameterIndex == other.ParameterIndex)
                   && Equals(other);
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddInt32(ParameterIndex);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}

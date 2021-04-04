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
    internal sealed class TypeBinaryExpressionFingerprint: ExpressionFingerprint
    {
        public TypeBinaryExpressionFingerprint(ExpressionType nodeType, Type type, Type typeOperand)
           : base(nodeType, type)
        {
            TypeOperand = typeOperand;
        }

       
        public Type TypeOperand { get; }

        public override bool Equals(object obj)
        {
            TypeBinaryExpressionFingerprint other = obj as TypeBinaryExpressionFingerprint;
            return (other != null)
                   && TypeOperand == other.TypeOperand
                   && Equals(other);
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(TypeOperand);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}

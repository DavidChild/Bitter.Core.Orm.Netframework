using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    internal sealed class MemberExpressionFingerprint: ExpressionFingerprint
    {
        public MemberExpressionFingerprint(ExpressionType nodeType, Type type, MemberInfo member)
           : base(nodeType, type)
        {
            Member = member;
        }

       
        public MemberInfo Member { get; }

        public override bool Equals(object obj)
        {
            MemberExpressionFingerprint other = obj as MemberExpressionFingerprint;
            return (other != null)
                   && Equals(Member, other.Member)
                   && Equals(other);
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Member);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}

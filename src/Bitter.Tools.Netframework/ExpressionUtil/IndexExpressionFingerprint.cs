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
    internal sealed class IndexExpressionFingerprint: ExpressionFingerprint
    {
        public IndexExpressionFingerprint(ExpressionType nodeType, Type type, PropertyInfo indexer)
           : base(nodeType, type)
        {
            // Other properties on IndexExpression (like the argument count) are simply derived
            // from Type and Indexer, so they're not necessary for inclusion in the fingerprint.

            Indexer = indexer;
        }

    
        public PropertyInfo Indexer { get; }

        public override bool Equals(object obj)
        {
            IndexExpressionFingerprint other = obj as IndexExpressionFingerprint;
            return (other != null)
                   && Equals(Indexer, other.Indexer)
                   && Equals(other);
        }

        internal override void AddToHashCodeCombiner(HashCodeCombiner combiner)
        {
            combiner.AddObject(Indexer);
            base.AddToHashCodeCombiner(combiner);
        }
    }
}

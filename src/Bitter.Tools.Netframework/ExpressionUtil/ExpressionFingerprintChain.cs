using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    public class ExpressionFingerprintChain: IEquatable<ExpressionFingerprintChain>
    {
        public readonly List<ExpressionFingerprint> Elements = new List<ExpressionFingerprint>();

        public bool Equals(ExpressionFingerprintChain other)
        {
            // Two chains are considered equal if two elements appearing in the same index in
            // each chain are equal (value equality, not referential equality).

            if (Elements.Count != other?.Elements.Count)
            {
                return false;
            }

            for (int i = 0; i < Elements.Count; i++)
            {
                if (!Equals(Elements[i], other.Elements[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as ExpressionFingerprintChain);
        }

        public override int GetHashCode()
        {
            HashCodeCombiner combiner = new HashCodeCombiner();
            Elements.ForEach(combiner.AddFingerprint);
            return combiner.CombinedHash;
        }
    }
}

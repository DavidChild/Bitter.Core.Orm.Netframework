using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bitter.Tools.ExpressionUtil
{
    public class HashCodeCombiner
    {
        private long _combinedHash64 = 0x1505L;

        public int CombinedHash => _combinedHash64.GetHashCode();

        public void AddFingerprint(ExpressionFingerprint fingerprint)
        {
            if (fingerprint != null)
            {
                fingerprint.AddToHashCodeCombiner(this);
            }
            else
            {
                AddInt32(0);
            }
        }

        public void AddEnumerable(IEnumerable e)
        {
            if (e == null)
            {
                AddInt32(0);
            }
            else
            {
                int count = 0;
                foreach (object o in e)
                {
                    AddObject(o);
                    count++;
                }
                AddInt32(count);
            }
        }

        public void AddInt32(int i)
        {
            _combinedHash64 = ((_combinedHash64 << 5) + _combinedHash64) ^ i;
        }

        public void AddObject(object o)
        {
            int hashCode = o?.GetHashCode() ?? 0;
            AddInt32(hashCode);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Utilities
{
    internal static class HashHelper
    {
        public static int CombineHashCodes(object first, object second)
        {
            int hash = 17;
            hash = hash * 31 + first.GetHashCode();
            hash = hash * 31 + second.GetHashCode();
            return hash;
        }
    }
}

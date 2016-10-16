using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    internal static class TestHelper
    {
        public static void AssertEnumerablesAreEqual<T>(IEnumerable<T> expectedEnum, IEnumerable<T> actualEnum)
        {
            Assert.AreEqual(expectedEnum.Count(), actualEnum.Count());

            for (int i = 0; i < expectedEnum.Count(); i++)
            {
                Assert.AreEqual(expectedEnum.ElementAt(i), actualEnum.ElementAt(i));
            }
        }
    }
}

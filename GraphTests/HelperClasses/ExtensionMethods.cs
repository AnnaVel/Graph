using System;
using System.Diagnostics;

namespace GraphTestsSL.HelperClasses
{
    internal static class ExtensionMethods
    {
        public static TimeSpan GetElapsedTimeSpan(this Stopwatch sw)
        {
#if SILVERLIGHT
            return TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds);
#else
            return sw.Elapsed;
#endif
        }
    }
}

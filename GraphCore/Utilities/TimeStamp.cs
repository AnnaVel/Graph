using System;
using System.Diagnostics;

namespace GraphCore.Utilities
{
    internal static class TimeStamp
    {
        private static DateTime beginning;
        private static Stopwatch elapsedTimeSinceBegining;

        static TimeStamp()
        {
            beginning = DateTime.Now;
            elapsedTimeSinceBegining = new Stopwatch();
            elapsedTimeSinceBegining.Start();
        }

        public static double GetCurrentTimeStamp()
        {
            TimeSpan elapsed = elapsedTimeSinceBegining.Elapsed;
            DateTime currentTime = beginning + elapsed;
            string currentTimeString = currentTime.ToString("yyMMddHHmmssfffff");
            double stamp = double.Parse(currentTimeString);

            return stamp;
        }
    }
}

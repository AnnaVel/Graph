﻿using System;
using System.Diagnostics;
using System.Threading;

namespace GraphCore.Utilities
{
    internal static class TimeStampCreator
    {
        private static int index;

        static TimeStampCreator()
        {
            index = 0;
        }

        public static double GetCurrentTimeStamp()
        {
            DateTime currentTime = DateTime.Now;
            string currentTimeString = currentTime.ToString("yyMMddHHmmssfff");
            string stampString = currentTimeString + GetIndexTimeStampPart();
            double stamp = double.Parse(stampString);

            return stamp;
        }

        private static string GetIndexTimeStampPart()
        {
            index++;
            return index.ToString();
        }
    }
}

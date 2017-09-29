using GraphCore.DynamicAttributes;
using System;

namespace GraphCore.Utilities
{
    public static class TimeStampCreator
    {
        private static int index;

        static TimeStampCreator()
        {
            index = 0;
        }

        public static TimeStamp GetCurrentTimeStamp()
        {
            DateTime currentTime = DateTime.Now;
           // string currentTimeString = currentTime.ToString("yyMMddHHmmssfff");
            int indexPart = GetIndexTimeStampPart();
            TimeStamp stamp = new TimeStamp(currentTime, indexPart);

            return stamp;
        }

        public static int GetIndexTimeStampPart()
        {
            index++;
            return index;
        }
    }
}

using System;

namespace GraphCore.DynamicAttributes
{
    internal static class TimeStampCreator
    {
        private static int index;

        static TimeStampCreator()
        {
            index = 0;
        }

        public static TimeStamp GetCurrentTimeStamp()
        {
            DateTime currentTime = DateTime.Now;
            int indexPart = GetIndexTimeStampPart();
            TimeStamp stamp = new TimeStamp(currentTime, indexPart);

            return stamp;
        }

        private static int GetIndexTimeStampPart()
        {
            index++;
            return index;
        }
    }
}

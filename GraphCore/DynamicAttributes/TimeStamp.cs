using System;

namespace GraphCore.DynamicAttributes
{
    public struct TimeStamp : IComparable<TimeStamp>
    {
        private DateTime timePart;
        private int indexPart;

        public TimeStamp(DateTime timePart, int indexPart)
        {
            this.timePart = timePart;
            this.indexPart = indexPart;
        }

        public int CompareTo(TimeStamp other)
        {
            int timePartComparison = this.timePart.CompareTo(other.timePart);

            if (timePartComparison != 0)
            {
                return timePartComparison;
            }
            else
            {
                return this.indexPart.CompareTo(other.indexPart);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}", this.timePart, this.indexPart);
        }
    }
}

using GraphCore.Utilities;
using System;

namespace GraphCore.DynamicAttributes
{
    internal class DynamicAttributeWithTimeStamp
    {
        private IDynamicAttribute attribute;
        private double stamp;

        public IDynamicAttribute Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        public double Stamp
        {
            get { return stamp; }
            set { stamp = value; }
        }
        
        public DynamicAttributeWithTimeStamp(IDynamicAttribute attribute)
        {
            Guard.ThrowExceptionIfNull(attribute, "attribute");

            this.attribute = attribute;
            this.stamp = this.GetCurrentTimeStamp();
        }

        private double GetCurrentTimeStamp()
        {
            DateTime currentTime = DateTime.Now;
            string currentTimeString = currentTime.ToString("yyMMddHHmmssfff");
            double stamp = double.Parse(currentTimeString);

            return stamp;
        }
    }
}

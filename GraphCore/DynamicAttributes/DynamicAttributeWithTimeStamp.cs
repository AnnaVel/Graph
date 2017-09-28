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
            this.stamp = TimeStamp.GetCurrentTimeStamp();
        }
    }
}

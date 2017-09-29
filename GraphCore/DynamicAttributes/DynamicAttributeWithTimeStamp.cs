using GraphCore.Utilities;
using System;

namespace GraphCore.DynamicAttributes
{
    internal class DynamicAttributeWithTimeStamp
    {
        private IDynamicAttribute attribute;
        private TimeStamp stamp;

        public IDynamicAttribute Attribute
        {
            get { return attribute; }
            set { attribute = value; }
        }

        public TimeStamp Stamp
        {
            get { return stamp; }
            set { stamp = value; }
        }
        
        public DynamicAttributeWithTimeStamp(IDynamicAttribute attribute)
        {
            Guard.ThrowExceptionIfNull(attribute, "attribute");

            this.attribute = attribute;
            this.stamp = TimeStampCreator.GetCurrentTimeStamp();
        }
    }
}

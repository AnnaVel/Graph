using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Events
{
    public class DynamicAttributeChangedEventArgs
    {
        private string dynamicAttributeName;

        public string DynamicAttributeName
        {
            get
            {
                return this.dynamicAttributeName;
            }
        }

        public DynamicAttributeChangedEventArgs(string dynamicAttributeName)
        {
            Guard.ThrowExceptionIfNullOrEmpty(dynamicAttributeName, "dynamicAttributeName");

            this.dynamicAttributeName = dynamicAttributeName;
        }
    }
}

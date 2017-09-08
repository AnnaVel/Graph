using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Events
{
    public class GraphItemPropertyChangedEventArgs
    {
        private string propertyName;

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public GraphItemPropertyChangedEventArgs(string propertyName)
        {
            Guard.ThrowExceptionIfNullOrEmpty(propertyName, "propertyName");

            this.propertyName = propertyName;
        }
    }
}

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
        private PropertyChangeAction changeAction;

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public PropertyChangeAction ChangeAction
        {
            get
            {
                return this.changeAction;
            }
        }

        public GraphItemPropertyChangedEventArgs(string propertyName, PropertyChangeAction changeAction)
        {
            Guard.ThrowExceptionIfNullOrEmpty(propertyName, "propertyName");

            this.propertyName = propertyName;
            this.changeAction = changeAction;
        }
    }
}

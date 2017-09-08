using GraphCore.Utilities;
using GraphViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.Events
{
    public class GraphViewModelPropertyChangedEventArgs : EventArgs
    {
        private string propertyName;
        private GraphItemViewModel sender;

        public GraphItemViewModel Sender
        {
            get
            {
                return this.sender;
            }
        }

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public GraphViewModelPropertyChangedEventArgs(GraphItemViewModel sender, string propertyName)
        {
            Guard.ThrowExceptionIfNullOrEmpty(propertyName, "propertyName");
            Guard.ThrowExceptionIfNull(sender, "sender");

            this.propertyName = propertyName;
            this.sender = sender;
        }
    }
}

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

        public string PropertyName
        {
            get
            {
                return this.propertyName;
            }
        }

        public GraphViewModelPropertyChangedEventArgs(string propertyName)
        {
            Guard.ThrowExceptionIfNullOrEmpty(propertyName, "propertyName");

            this.propertyName = propertyName;
        }
    }
}

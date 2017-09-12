using GraphCore.Utilities;
using GraphViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.Events
{
    public class ViewModelDynamicAttributeChangedEventArgs : EventArgs
    {
        private string dynamicAttributeName;

        public string DynamicAttributeName
        {
            get
            {
                return this.dynamicAttributeName;
            }
        }

        public ViewModelDynamicAttributeChangedEventArgs(string dynamicAttributeName)
        {
            Guard.ThrowExceptionIfNullOrEmpty(dynamicAttributeName, "dynamicAttributeName");

            this.dynamicAttributeName = dynamicAttributeName;
        }
    }
}

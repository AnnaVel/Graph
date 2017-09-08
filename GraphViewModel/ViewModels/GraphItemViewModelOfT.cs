using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.ViewModels
{
    public abstract class GraphItemViewModel<T> : GraphItemViewModel
    {
        protected T RelatedItem
        {
            get
            {
                return (T)this.RelatedItemAsObject;
            }
        }

        public GraphItemViewModel(GraphItemViewModelType graphItemViewModelType, T relatedItem) 
            : base(graphItemViewModelType, relatedItem)
        {
        }
    }
}

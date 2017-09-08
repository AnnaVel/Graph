using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.ViewModels
{
    public abstract class VertexViewModel<T> : VertexViewModel
    {
        protected T RelatedVertexItem
        {
            get
            {
                return (T)this.RelatedItem;
            }
        }

        public VertexViewModel(GraphItemViewModelType graphItemViewModelType, T relatedItem) 
            : base(graphItemViewModelType, relatedItem)
        {
        }
    }
}

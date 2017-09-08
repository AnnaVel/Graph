using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphViewModel.ViewModels
{
    public abstract class VertexViewModel : GraphItemViewModel<object>
    {
        private Point location;
        private Size size;

        public Point Location
        {
            get
            {
                return this.location;
            }
            set
            {
                if(this.location != value)
                {
                    this.location = value;
                    this.OnPropertyChanged();
                }
                
            }
        }

        public Size Size
        {
            get
            {
                return size;
            }
            set
            {
                if (this.size != value)
                {
                    this.size = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public VertexViewModel(GraphItemViewModelType graphItemViewModelType, object relatedItem) 
            : base(graphItemViewModelType, relatedItem)
        {
        }
    }
}

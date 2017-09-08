using GraphCore.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.GraphItemProperties;
using System.Windows;

namespace GraphViewModel.ViewModels
{
    public class EdgeViewModel : GraphItemViewModel<Edge>
    {
        private Point startLocation;
        private Point endLocation;

        public Point StartLocation
        {
            get
            {
                return this.startLocation;
            }
            set
            {
                if(this.startLocation != value)
                {
                    this.startLocation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public Point EndLocation
        {
            get
            {
                return this.endLocation;
            }
            set
            {
                if (this.endLocation != value)
                {
                    this.endLocation = value;
                    this.OnPropertyChanged();
                }
            }
        }

        public EdgeViewModel(Edge relatedItem) 
            : base(GraphItemViewModelType.Single, relatedItem)
        {
        }

        protected override IEnumerable<string> EnumeratePropertyValuesForRelatedItem()
        {
            return this.RelatedItem.EnumeratePropertyNames();
        }

        protected override object RecalculatePropertyValue(string propertyName)
        {
            IGraphItemProperty property = this.RelatedItem.GetProperty(propertyName);

            if (property != null)
            {
                return property.ValueAsObject;
            }

            return null;
        }

        protected override void SubscribeToRelatedItemEvents(GraphItemPropertyChangedEventHandler graphPropertyChangedHandler)
        {
            this.RelatedItem.PropertyList.GraphItemPropertyChanged += graphPropertyChangedHandler;
        }

        protected override void UnsubscribeFromRelatedItemEvents(GraphItemPropertyChangedEventHandler graphPropertyChangedHandler)
        {
            this.RelatedItem.PropertyList.GraphItemPropertyChanged -= graphPropertyChangedHandler;
        }
    }
}

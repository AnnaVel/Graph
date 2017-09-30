using GraphCore.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.DynamicAttributes;
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
            : base( relatedItem)
        {
        }

        protected override IEnumerable<string> EnumerateDynamicAttributeNamesForRelatedItem()
        {
            return this.RelatedItem.EnumerateDynamicAttributeNames();
        }

        protected override object RecalculateDynamicAttributeValue(string attributeName)
        {
            IDynamicAttribute attribute = this.RelatedItem.GetDynamicAttributeThatWasLastSetInGroup(attributeName);

            if (attribute != null)
            {
                return attribute.ValueAsObject;
            }

            return null;
        }

        protected override void SubscribeToRelatedItemEvents(DynamicAttributeChangedEventHandler attributeChangedHandler)
        {
            this.RelatedItem.DynamicAttributeList.DynamicAttributeChanged += attributeChangedHandler;
        }

        protected override void UnsubscribeFromRelatedItemEvents(DynamicAttributeChangedEventHandler attributeChangedHandler)
        {
            this.RelatedItem.DynamicAttributeList.DynamicAttributeChanged -= attributeChangedHandler;
        }
    }
}

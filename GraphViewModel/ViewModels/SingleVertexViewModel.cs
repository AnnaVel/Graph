using GraphCore.Events;
using GraphCore.GraphItemProperties;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.ViewModels
{
    public class SingleVertexViewModel : VertexViewModel<Vertex>
    {
        public SingleVertexViewModel(Vertex vertex) : base(vertex)
        {
        }

        protected override IEnumerable<string> EnumeratePropertyValuesForRelatedItem()
        {
            return this.RelatedVertexItem.EnumeratePropertyNames();
        }

        protected override object RecalculatePropertyValue(string propertyName)
        {
            IGraphItemProperty property = this.RelatedVertexItem.GetProperty(propertyName);

            if(property != null)
            {
                return property.ValueAsObject;
            }

            return null;
        }

        protected override void SubscribeToRelatedItemEvents(GraphItemPropertyChangedEventHandler graphPropertyChangedHandler)
        {
            this.RelatedVertexItem.PropertyList.GraphItemPropertyChanged += graphPropertyChangedHandler;
        }

        protected override void UnsubscribeFromRelatedItemEvents(GraphItemPropertyChangedEventHandler graphPropertyChangedHandler)
        {
            this.RelatedVertexItem.PropertyList.GraphItemPropertyChanged -= graphPropertyChangedHandler;
        }
    }
}

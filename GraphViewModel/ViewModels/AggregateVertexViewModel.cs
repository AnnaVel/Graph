using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.GraphItemProperties;

namespace GraphViewModel.ViewModels
{
    public class AggregateVertexViewModel : VertexViewModel<List<Vertex>>
    {
        public AggregateVertexViewModel(List<Vertex> relatedItem) 
            : base(relatedItem)
        {
        }

        protected override IEnumerable<string> EnumeratePropertyValuesForRelatedItem()
        {
            HashSet<string> properties = new HashSet<string>();

            foreach(Vertex vertex in this.RelatedVertexItem)
            {
                foreach (string property in vertex.EnumeratePropertyNames())
                {
                    if (!properties.Contains(property))
                    {
                        yield return property;
                        properties.Add(property);
                    }
                }
            }
        }

        protected override object RecalculatePropertyValue(string propertyName)
        {
            foreach (Vertex vertex in this.RelatedVertexItem)
            {
                IGraphItemProperty graphProperty = vertex.GetProperty(propertyName);
                if (graphProperty != null)
                {
                    return graphProperty.ValueAsObject;
                }
            }

            return null;               
        }

        protected override void SubscribeToRelatedItemEvents(GraphItemPropertyChangedEventHandler handler)
        {
            foreach(Vertex vertex in this.RelatedVertexItem)
            {
                vertex.PropertyList.GraphItemPropertyChanged += handler;
            }
        }

        protected override void UnsubscribeFromRelatedItemEvents(GraphItemPropertyChangedEventHandler handler)
        {
            foreach (Vertex vertex in this.RelatedVertexItem)
            {
                vertex.PropertyList.GraphItemPropertyChanged -= handler;
            }
        }
    }
}

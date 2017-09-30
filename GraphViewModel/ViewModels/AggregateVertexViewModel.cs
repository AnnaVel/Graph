using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.DynamicAttributes;

namespace GraphViewModel.ViewModels
{
    public class AggregateVertexViewModel : VertexViewModel<List<Vertex>>
    {
        public AggregateVertexViewModel(List<Vertex> relatedItem) 
            : base(relatedItem)
        {
        }

        protected override IEnumerable<string> EnumerateDynamicAttributeNamesForRelatedItem()
        {
            HashSet<string> attributes = new HashSet<string>();

            foreach(Vertex vertex in this.RelatedVertexItem)
            {
                foreach (string attributeName in vertex.EnumerateDynamicAttributeNames())
                {
                    if (!attributes.Contains(attributeName))
                    {
                        yield return attributeName;
                        attributes.Add(attributeName);
                    }
                }
            }
        }

        protected override object RecalculateDynamicAttributeValue(string attributeName)
        {
            foreach (Vertex vertex in this.RelatedVertexItem)
            {
                IDynamicAttribute attribute = vertex.GetDynamicAttributeThatWasLastSetInGroup(attributeName); ;
                if (attribute != null)
                {
                    return attribute.ValueAsObject;
                }
            }

            return null;               
        }

        protected override void SubscribeToRelatedItemEvents(DynamicAttributeChangedEventHandler handler)
        {
            foreach(Vertex vertex in this.RelatedVertexItem)
            {
                vertex.DynamicAttributeList.DynamicAttributeChanged += handler;
            }
        }

        protected override void UnsubscribeFromRelatedItemEvents(DynamicAttributeChangedEventHandler handler)
        {
            foreach (Vertex vertex in this.RelatedVertexItem)
            {
                vertex.DynamicAttributeList.DynamicAttributeChanged -= handler;
            }
        }
    }
}

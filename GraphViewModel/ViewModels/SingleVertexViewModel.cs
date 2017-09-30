using GraphCore.DynamicAttributes;
using GraphCore.Vertices;
using System.Collections.Generic;

namespace GraphViewModel.ViewModels
{
    public class SingleVertexViewModel : VertexViewModel<Vertex>
    {
        public SingleVertexViewModel(Vertex vertex) : base(vertex)
        {
        }

        protected override IEnumerable<string> EnumerateDynamicAttributeNamesForRelatedItem()
        {
            return this.RelatedVertexItem.EnumerateDynamicAttributeNames();
        }

        protected override object RecalculateDynamicAttributeValue(string attributeName)
        {
            IDynamicAttribute attribute = this.RelatedVertexItem.GetDynamicAttributeThatWasLastSetInGroup(attributeName);

            if(attribute != null)
            {
                return attribute.ValueAsObject;
            }

            return null;
        }

        protected override void SubscribeToRelatedItemEvents(DynamicAttributeChangedEventHandler attributeChangedHandler)
        {
            this.RelatedVertexItem.DynamicAttributeList.DynamicAttributeChanged += attributeChangedHandler;
        }

        protected override void UnsubscribeFromRelatedItemEvents(DynamicAttributeChangedEventHandler attributeChangedHandler)
        {
            this.RelatedVertexItem.DynamicAttributeList.DynamicAttributeChanged -= attributeChangedHandler;
        }
    }
}

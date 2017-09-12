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

        protected override IEnumerable<string> EnumerateDynamicAttributeNamesForRelatedItem()
        {
            return this.RelatedVertexItem.EnumerateDynamicAttributeNames();
        }

        protected override object RecalculateDynamicAttributeValue(string attributeName)
        {
            IDynamicAttribute attribute = this.RelatedVertexItem.GetDynamicAttribute(attributeName);

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

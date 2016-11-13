using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphViewModel;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;
using GraphViewModel.LayoutChange;
using GraphCore.Vertices;
using System.Windows;

namespace GraphView.UILayers
{
    internal class VertexUILayer : UILayer
    {
        public const int EllipseDiameter = 20;

        private readonly Dictionary<Vertex, UIElement> vertexToElement;

        public VertexUILayer(Canvas mainContainer)
            :base(mainContainer)
        {
            this.vertexToElement = new Dictionary<Vertex, UIElement>();
        }

        public override void UpdateUI(LayoutUpdateContext updateContext)
        {
            foreach(VertexLayoutChange vertexLayoutChange in updateContext.VertexChanges)
            {
                switch(vertexLayoutChange.LayoutChangeAction)
                {
                    case LayoutChangeAction.Add:
                        UIElement correspondingElementToAdd = this.GetEllipse();
                        this.AddElementToUI(correspondingElementToAdd, vertexLayoutChange.Location);
                        this.vertexToElement.Add(vertexLayoutChange.ChangedObject, correspondingElementToAdd);
                        break;
                    case LayoutChangeAction.Remove:
                        UIElement correspondingElementToRemove = this.vertexToElement[vertexLayoutChange.ChangedObject];
                        this.RemoveElementFromUI(correspondingElementToRemove);
                        this.vertexToElement.Remove(vertexLayoutChange.ChangedObject);
                        break;
                    case LayoutChangeAction.Change:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private Ellipse GetEllipse()
        {
            Ellipse ellipse = new Ellipse();
            ellipse.Width = EllipseDiameter;
            ellipse.Height = EllipseDiameter;
            ellipse.Fill = new SolidColorBrush(Colors.Red);

            return ellipse;
        }
    }
}

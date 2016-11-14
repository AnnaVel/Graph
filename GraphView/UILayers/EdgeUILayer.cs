using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphViewModel;
using System.Windows.Controls;
using GraphViewModel.LayoutChange;
using System.Windows;
using System.Windows.Shapes;
using GraphCore.Edges;
using System.Windows.Media;

namespace GraphView.UILayers
{
    internal class EdgeUILayer : UILayer
    {
        private Dictionary<Edge, UIElement> edgeToElement;

        public EdgeUILayer(Canvas mainContainer)
            :base(mainContainer)
        {
            this.edgeToElement = new Dictionary<Edge, UIElement>();
        }

        public override void UpdateUI(LayoutUpdateContext updateContext)
        {
            foreach (EdgeLayoutChange edgeLayoutChange in updateContext.EdgeChanges)
            {
                switch (edgeLayoutChange.LayoutChangeAction)
                {
                    case LayoutChangeAction.Add:
                        UIElement correspondingElementToAdd = this.GetLine(edgeLayoutChange.Location, edgeLayoutChange.EndLocation);
                        this.AddElementToUI(correspondingElementToAdd, new Point(0,0));
                        this.edgeToElement.Add(edgeLayoutChange.ChangedObject, correspondingElementToAdd);
                        break;
                    case LayoutChangeAction.Remove:
                        UIElement correspondingElementToRemove = this.edgeToElement[edgeLayoutChange.ChangedObject];
                        this.RemoveElementFromUI(correspondingElementToRemove);
                        this.edgeToElement.Remove(edgeLayoutChange.ChangedObject);
                        break;
                    case LayoutChangeAction.Change:
                        throw new NotImplementedException();
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private Line GetLine(Point startPoint, Point endPoint)
        {
            Line line = new Line();
            line.X1 = startPoint.X + VertexUILayer.EllipseDiameter / 2;
            line.Y1 = startPoint.Y + VertexUILayer.EllipseDiameter / 2;
            line.X2 = endPoint.X + VertexUILayer.EllipseDiameter / 2;
            line.Y2 = endPoint.Y + VertexUILayer.EllipseDiameter / 2;
            line.Stroke = new SolidColorBrush(Colors.Black);
            line.StrokeThickness = 1;

            return line;
        }
    }
}

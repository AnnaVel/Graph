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
using GraphViewModel.ViewModels;
using GraphCore.Events;
using GraphView.Views;
using GraphViewModel.Layout;

namespace GraphView.UILayers
{
    internal class EdgeUILayer : UILayer
    {
        private Dictionary<EdgeViewModel, UIElement> viewModelToElement;

        public EdgeUILayer(Canvas mainContainer)
            :base(mainContainer)
        {
            this.viewModelToElement = new Dictionary<EdgeViewModel, UIElement>();
        }

        public override void UpdateUI(LayoutUpdateContext updateContext)
        {
            foreach (EdgeLayoutChange edgeLayoutChange in updateContext.EdgeChanges)
            {
                switch (edgeLayoutChange.LayoutChangeAction)
                {
                    case ChangeAction.Add:
                        AddEdge(edgeLayoutChange.ChangedObjectViewModel);
                        break;
                    case ChangeAction.Remove:
                        RemoveEdge(edgeLayoutChange.ChangedObjectViewModel);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void AddEdge(EdgeViewModel edgeViewModel)
        {
            UIElement correspondingElementToAdd = this.GetNewEdgeView(edgeViewModel);
            this.AddElementToUI(correspondingElementToAdd);
            this.viewModelToElement.Add(edgeViewModel, correspondingElementToAdd);
        }

        private void RemoveEdge(EdgeViewModel edgeViewModel)
        {
            UIElement correspondingElementToRemove = this.viewModelToElement[edgeViewModel];
            this.RemoveElementFromUI(correspondingElementToRemove);
            this.viewModelToElement.Remove(edgeViewModel);
        }

        private EdgeView GetNewEdgeView(EdgeViewModel viewModel)
        {
            EdgeView edgeView = new EdgeView();
            edgeView.DataContext = viewModel;

            return edgeView;
        }
    }
}

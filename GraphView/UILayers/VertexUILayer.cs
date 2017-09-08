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
using GraphCore.Events;
using GraphView.Views;
using GraphViewModel.ViewModels;
using GraphViewModel.Layout;

namespace GraphView.UILayers
{
    internal class VertexUILayer : UILayer
    {
        private readonly Dictionary<VertexViewModel, UIElement> viewModelToElement;

        public VertexUILayer(Canvas mainContainer)
            :base(mainContainer)
        {
            this.viewModelToElement = new Dictionary<VertexViewModel, UIElement>();
        }

        public override void UpdateUI(LayoutUpdateContext updateContext)
        {
            foreach(VertexLayoutChange vertexLayoutChange in updateContext.VertexChanges)
            {
                switch(vertexLayoutChange.LayoutChangeAction)
                {
                    case ChangeAction.Add:
                        this.AddVertex(vertexLayoutChange.ChangedObjectViewModel);
                        break;
                    case ChangeAction.Remove:
                        this.RemoveVertex(vertexLayoutChange.ChangedObjectViewModel);
                        break;
                    default:
                        throw new NotSupportedException();
                }
            }
        }

        private void AddVertex(VertexViewModel viewModel)
        {
            UIElement correspondingElementToAdd = this.GetNewVertexView(viewModel);
            this.AddElementToUI(correspondingElementToAdd);
            this.viewModelToElement.Add(viewModel, correspondingElementToAdd);
        }

        private void RemoveVertex(VertexViewModel viewModel)
        {
            UIElement correspondingElementToRemove = this.viewModelToElement[viewModel];
            this.RemoveElementFromUI(correspondingElementToRemove);
            this.viewModelToElement.Remove(viewModel);
        }

        private VertexView GetNewVertexView(VertexViewModel viewModel)
        {
            VertexView vertexView = new VertexView();
            vertexView.DataContext = viewModel;

            return vertexView;
        }
    }
}

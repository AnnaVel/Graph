using GVM = GraphViewModel;
using System.Windows;
using System.Windows.Controls;
using GraphViewModel.Layout;
using GraphCore;
using GraphView.UILayers;

namespace GraphView
{
    public class GraphView : Control
    {
        private const string MainContainerName = "mainContainer";

        private readonly GVM.GraphViewModel graphViewModel;
        private UILayerContainer uiLayerContainer;

        public Graph Graph
        {
            get
            {
                return graphViewModel.Graph;
            }
            set
            {
                graphViewModel.Graph = value;
            }
        }

        static GraphView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphView), new FrameworkPropertyMetadata(typeof(GraphView)));
        }

        public GraphView()
        {
            this.graphViewModel = new GVM.GraphViewModel();
            this.graphViewModel.GraphLayoutChanged += GraphViewModel_GraphLayoutChanged;
        }

        private void GraphViewModel_GraphLayoutChanged(LayoutUpdateContext updateContext)
        {
            this.uiLayerContainer.UpdateUI(updateContext);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            Canvas mainContainer = this.GetTemplateChild(MainContainerName) as Canvas;
            this.uiLayerContainer = new UILayerContainer(mainContainer);
        }
    }
}

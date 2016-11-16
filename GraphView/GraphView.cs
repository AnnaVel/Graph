using GVM = GraphViewModel;
using System.Windows;
using System.Windows.Controls;
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

#if WPF
        static GraphView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GraphView), new FrameworkPropertyMetadata(typeof(GraphView)));
        }
#endif

        public GraphView()
        {
#if SILVERLIGHT
            this.DefaultStyleKey = typeof(GraphView);
#endif

            this.graphViewModel = new GVM.GraphViewModel();
            this.graphViewModel.GraphLayoutChanged += GraphViewModel_GraphLayoutChanged;
        }

        private void GraphViewModel_GraphLayoutChanged(GVM.LayoutUpdateContext updateContext)
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

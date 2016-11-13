using GVM = GraphViewModel;
using System.Windows;
using System.Windows.Controls;
using GraphCore;
using GraphView.UILayers;

namespace GraphView
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GraphView"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is 
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:GraphView;assembly=GraphView"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:CustomControl1/>
    ///
    /// </summary>
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

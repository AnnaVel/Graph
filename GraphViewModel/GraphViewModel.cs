using GraphCore;
using GraphCore.Events;
using GraphCore.Vertices;
using GraphViewModel.Layout;

namespace GraphViewModel
{
    public class GraphViewModel
    {
        private readonly GraphLayout layoutCalculator;
        private Graph graph;

        public Graph Graph
        {
            get
            {
                return this.graph;
            }
            set
            {
                if (this.graph != value)
                {
                    this.OnGraphChanging();
                    this.graph = value;
                    this.OnGraphChanged();
                }
            }
        }

        public GraphViewModel()
        {
            this.layoutCalculator = new GraphLayout();
        }

        private void OnGraphChanging()
        {
            if (this.graph != null)
            {
                this.graph.GraphStructure.GraphStructureChanged -= GraphStructure_GraphStructureChanged;
            }
        }

        private void OnGraphChanged()
        {
            if (this.graph != null)
            {
                this.graph.GraphStructure.GraphStructureChanged += GraphStructure_GraphStructureChanged;
            }

            LayoutUpdateContext updateContext = this.layoutCalculator.CreateInitialLayoutUpdateContext(this.graph);
            this.OnGraphLayoutChanged(updateContext);
        }

        private void GraphStructure_GraphStructureChanged(GraphStructureChangedEventArgs args)
        {
            LayoutUpdateContext updateContext = this.layoutCalculator.CreateLayoutUpdateContext(args);
            this.OnGraphLayoutChanged(updateContext);
        }

        public delegate void GraphLayoutChangedEventHandler(LayoutUpdateContext updateContext);

        public event GraphLayoutChangedEventHandler GraphLayoutChanged;

        private void OnGraphLayoutChanged(LayoutUpdateContext updateContext)
        {
            if (this.GraphLayoutChanged != null)
            {
                this.GraphLayoutChanged(updateContext);
            }
        }
    }
}

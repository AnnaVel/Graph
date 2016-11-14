using GraphCore;
using GraphCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            LayoutUpdateContext updateContext = this.layoutCalculator.UpdateLayout(this.graph);
            this.OnGraphLayoutChanged(updateContext);
        }

        private void GraphStructure_GraphStructureChanged(GraphStructureChangedEventArgs args)
        {
            LayoutUpdateContext updateContext = this.layoutCalculator.UpdateLayout(args);
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

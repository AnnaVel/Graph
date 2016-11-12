using GraphCore;
using GraphCore.Edges;
using GraphCore.Events;
using GraphCore.Vertices;
using GraphViewModel.LayoutChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GraphViewModel
{
    internal class GraphLayout
    {
        private readonly Random rand = new Random();

        private readonly Dictionary<Vertex, Point> vertexToLocation;
        private readonly HashSet<Edge> edges;

        public GraphLayout()
        {
            this.vertexToLocation = new Dictionary<Vertex, Point>();
            this.edges = new HashSet<Edge>();
        }

        public LayoutUpdateContext UpdateLayout(Graph graph)
        {
            List<VertexLayoutChange> vertexChanges = new List<VertexLayoutChange>();
            List<EdgeLayoutChange> edgeChanges = new List<EdgeLayoutChange>();

            foreach(var vertexToLocationPair in this.vertexToLocation)
            {
                Vertex vertexToRemove = vertexToLocationPair.Key;
                VertexLayoutChange removeVertexChange = this.MakeVertexLayoutChange(vertexToRemove, LayoutChangeAction.Remove);
                vertexChanges.Add(removeVertexChange);
            }

            foreach(Edge edgeToRemove in this.edges)
            {
                EdgeLayoutChange removeEdgeChange = this.MakeEdgeLayoutChange(edgeToRemove, LayoutChangeAction.Remove);
                edgeChanges.Add(removeEdgeChange);
            }

            foreach (Vertex vertex in graph.GraphStructure.Vertices)
            {
                VertexLayoutChange addVertexChange = this.MakeVertexLayoutChange(vertex, LayoutChangeAction.Add);
                vertexChanges.Add(addVertexChange);
            }

            foreach(Edge edge in graph.GraphStructure.Edges)
            {
                EdgeLayoutChange addEdgeChange = this.MakeEdgeLayoutChange(edge, LayoutChangeAction.Add);
                edgeChanges.Add(addEdgeChange);
            }

            LayoutUpdateContext layoutUpdateContext = new LayoutUpdateContext(vertexChanges, edgeChanges);
            return layoutUpdateContext;
        }

        public LayoutUpdateContext UpdateLayout(GraphStructureChangedEventArgs args)
        {
            LayoutChangeAction layoutAction = args.ChangeAction == ChangeAction.Add ?
                LayoutChangeAction.Add :
                LayoutChangeAction.Remove;

            List<VertexLayoutChange> vertexChanges = new List<VertexLayoutChange>();
            List<EdgeLayoutChange> edgeChanges = new List<EdgeLayoutChange>();

            foreach (GraphStructureItem itemChanged in args.ItemsChanged)
            {
                if (itemChanged is Vertex)
                {
                    Vertex changedVertex = itemChanged as Vertex;
                    VertexLayoutChange vertexLayoutChange = this.MakeVertexLayoutChange(changedVertex, layoutAction);
                    vertexChanges.Add(vertexLayoutChange);
                }
                else if (itemChanged is Edge)
                {
                    Edge changedEdge = itemChanged as Edge;
                    EdgeLayoutChange edgeLayoutChange = this.MakeEdgeLayoutChange(changedEdge, layoutAction);
                    edgeChanges.Add(edgeLayoutChange);
                }
                else
                {
                    throw new NotSupportedException();
                }
            }

            LayoutUpdateContext layoutUpdateContext = new LayoutUpdateContext(vertexChanges, edgeChanges);
            return layoutUpdateContext;
        }

        private EdgeLayoutChange MakeEdgeLayoutChange(Edge edge, LayoutChangeAction action)
        {
            EdgeLayoutChange edgeLayoutChange = null;

            if (action == LayoutChangeAction.Add)
            {
                Point firstLocation = this.vertexToLocation[edge.FirstVertex];
                Point secondLocation = this.vertexToLocation[edge.SecondVertex];
                edgeLayoutChange = new EdgeLayoutChange(action, firstLocation, secondLocation, edge);
                this.edges.Add(edge);
            }
            else if (action == LayoutChangeAction.Remove)
            {
                edgeLayoutChange = new EdgeLayoutChange(action, new Point(0, 0), new Point(0, 0), edge);
                this.edges.Remove(edge);
            }
            else if (action == LayoutChangeAction.Change)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }

            return edgeLayoutChange;
        }

        private VertexLayoutChange MakeVertexLayoutChange(Vertex vertex, LayoutChangeAction action)
        {
            VertexLayoutChange vertexLayoutChange = null;

            if(action == LayoutChangeAction.Add)
            {
                Point location = this.GetLocation();
                vertexLayoutChange = new VertexLayoutChange(action, location, vertex);
                this.vertexToLocation.Add(vertex, location);
            }
            else if(action == LayoutChangeAction.Remove)
            {
                vertexLayoutChange = new VertexLayoutChange(action, new Point(0,0), vertex);
                this.vertexToLocation.Remove(vertex);
            }
            else if(action == LayoutChangeAction.Change)
            {
                throw new NotImplementedException();
            }
            else
            {
                throw new NotSupportedException();
            }

            return vertexLayoutChange;
        }

        private Point GetLocation()
        {
            int top = rand.Next(0, 300);
            int left = rand.Next(0, 300);

            return new Point(left, top);
        }
    }
}

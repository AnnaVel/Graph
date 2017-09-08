using GraphCore;
using GraphCore.Edges;
using GraphCore.Events;
using GraphCore.Vertices;
using GraphViewModel.LayoutChange;
using GraphViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Windows;

namespace GraphViewModel.Layout
{
    internal class GraphLayout
    {
        private static readonly Size vertexSize = new Size(30, 20);
        private readonly Random positionCalculator = new Random();

        private readonly Dictionary<Vertex, VertexViewModel> vertexToViewModel;
        private readonly Dictionary<Edge, EdgeViewModel> edgeToViewModel;

        public GraphLayout()
        {
            this.vertexToViewModel = new Dictionary<Vertex, VertexViewModel>();
            this.edgeToViewModel = new Dictionary<Edge, EdgeViewModel>();
        }

        public LayoutUpdateContext CreateInitialLayoutUpdateContext(Graph graph)
        {
            List<VertexLayoutChange> vertexChanges = new List<VertexLayoutChange>();
            List<EdgeLayoutChange> edgeChanges = new List<EdgeLayoutChange>();

            foreach(var vertexToVMPair in this.vertexToViewModel)
            {
                Vertex vertexToRemove = vertexToVMPair.Key;
                VertexLayoutChange removeVertexChange = this.MakeVertexLayoutChange(vertexToRemove, ChangeAction.Remove);
                vertexChanges.Add(removeVertexChange);
            }

            foreach(var edgeToVMPair in this.edgeToViewModel)
            {
                Edge edgeToRemove = edgeToVMPair.Key;
                EdgeLayoutChange removeEdgeChange = this.MakeEdgeLayoutChange(edgeToRemove, ChangeAction.Remove);
                edgeChanges.Add(removeEdgeChange);
            }

            foreach (Vertex vertex in graph.GraphStructure.Vertices)
            {
                VertexLayoutChange addVertexChange = this.MakeVertexLayoutChange(vertex, ChangeAction.Add);
                vertexChanges.Add(addVertexChange);
            }

            foreach(Edge edge in graph.GraphStructure.Edges)
            {
                EdgeLayoutChange addEdgeChange = this.MakeEdgeLayoutChange(edge, ChangeAction.Add);
                edgeChanges.Add(addEdgeChange);
            }

            LayoutUpdateContext layoutUpdateContext = new LayoutUpdateContext(vertexChanges, edgeChanges);
            return layoutUpdateContext;
        }

        public LayoutUpdateContext CreateLayoutUpdateContext(GraphStructureChangedEventArgs args)
        {
            ChangeAction layoutAction = args.ChangeAction;

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

        private EdgeLayoutChange MakeEdgeLayoutChange(Edge edge, ChangeAction action)
        {
            EdgeLayoutChange edgeLayoutChange = null;

            if (action == ChangeAction.Add)
            {
                Point firstLocation = this.vertexToViewModel[edge.FirstVertex].Location;
                Point secondLocation = this.vertexToViewModel[edge.SecondVertex].Location;
                EdgeViewModel edgeViewModelToAdd = new EdgeViewModel(edge);
                edgeViewModelToAdd.StartLocation = new Point(firstLocation.X + vertexSize.Width / 2, firstLocation.Y + vertexSize.Height / 2);
                edgeViewModelToAdd.EndLocation = new Point(secondLocation.X + vertexSize.Width / 2, secondLocation.Y + vertexSize.Height / 2);
                edgeLayoutChange = new EdgeLayoutChange(action, edgeViewModelToAdd);
                this.edgeToViewModel.Add(edge, edgeViewModelToAdd);
            }
            else if (action == ChangeAction.Remove)
            {
                EdgeViewModel edgeViewModelToRemove = this.edgeToViewModel[edge];
                edgeViewModelToRemove.ReleaseRelatedItem();
                edgeLayoutChange = new EdgeLayoutChange(action, edgeViewModelToRemove);
                this.edgeToViewModel.Remove(edge);
            }
            else
            {
                throw new NotSupportedException();
            }

            return edgeLayoutChange;
        }

        private VertexLayoutChange MakeVertexLayoutChange(Vertex vertex, ChangeAction action)
        {
            VertexLayoutChange vertexLayoutChange = null;

            if (action == ChangeAction.Add)
            {
                Point location = this.GetLocation();
                SingleVertexViewModel vertexViewModelToAdd = new SingleVertexViewModel(vertex);
                vertexViewModelToAdd.Location = location;
                vertexViewModelToAdd.Size = vertexSize;
                vertexLayoutChange = new VertexLayoutChange(action, vertexViewModelToAdd);
                this.vertexToViewModel.Add(vertex, vertexViewModelToAdd);
            }
            else if (action == ChangeAction.Remove)
            {
                VertexViewModel vertexViewModelToRemove = this.vertexToViewModel[vertex];
                vertexViewModelToRemove.ReleaseRelatedItem();
                vertexLayoutChange = new VertexLayoutChange(action, vertexViewModelToRemove);
                this.vertexToViewModel.Remove(vertex);
            }
            else
            {
                throw new NotSupportedException();
            }

            return vertexLayoutChange;
        }

        private Point GetLocation()
        {
            int top = positionCalculator.Next(0, 300);
            int left = positionCalculator.Next(0, 300);

            return new Point(left, top);
        }
    }
}

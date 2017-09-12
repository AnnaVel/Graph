using GraphCore.Edges;
using GraphCore.Utilities;
using GraphCore.DynamicAttributes;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.Events;

namespace GraphCore
{
    public class GraphStructure
    {
        private readonly AdjacencyList adjacencyList;

        private readonly Dictionary<object, Vertex> valueToVertexIndex;
        private readonly HashSet<Edge> edges;

        private VertexFactory vertexFactory;
        private EdgeFactory edgeFactory;
        private DynamicAttributeFactory vertexDynamicAttributeFactory;
        private DynamicAttributeFactory edgeDynamicAttributeFactory;

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.valueToVertexIndex.Values;
            }
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                return this.edges;
            }
        }

        public VertexFactory VertexFactory
        {
            get
            {
                return this.vertexFactory;
            }
            set
            {
                this.vertexFactory = value;
            }
        }

        public EdgeFactory EdgeFactory
        {
            get
            {
                return this.edgeFactory;
            }
            set
            {
                this.edgeFactory = value;
            }
        }

        public DynamicAttributeFactory VertexDynamicAttributeFactory
        {
            get
            {
                return this.vertexDynamicAttributeFactory;
            }
            set
            {
                this.vertexDynamicAttributeFactory = value;
            }
        }

        public DynamicAttributeFactory EdgeDynamicAttributeFactory
        {
            get
            {
                return this.edgeDynamicAttributeFactory;
            }
            set
            {
                this.edgeDynamicAttributeFactory = value;
            }
        }

        public GraphStructure()
        {
            this.adjacencyList = new AdjacencyList();
            
            this.valueToVertexIndex = new Dictionary<object, Vertex>();
            this.edges = new HashSet<Edge>();

            this.vertexFactory = new VertexFactory();
            this.edgeFactory = new EdgeFactory();
            this.vertexDynamicAttributeFactory = new DynamicAttributeFactory();
            this.edgeDynamicAttributeFactory = new DynamicAttributeFactory();
        }

        public Vertex AddVertex(object value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            Vertex newVertex = this.vertexFactory.CreateVertex(value);

            this.RegisterVertex(newVertex);

            this.OnGraphStructureChanged(ChangeAction.Add, newVertex);

            return newVertex;
        }

        public bool RemoveVertex(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            if (!this.VertexBelongsToThisStructure(vertex))
            {
                return false;
            }

            this.UnregisterEdgesRelatedToVertex(vertex);
            this.UnregisterVertex(vertex);

            this.adjacencyList.RemoveVertex(vertex);

            this.OnGraphStructureChanged(ChangeAction.Remove, vertex);

            return true;
        }

        private void UnregisterEdgesRelatedToVertex(Vertex vertex)
        {
            HashSet<Edge> relatedEdges = new HashSet<Edge>();

            foreach (Edge edge in vertex.GetIncomingEdges())
            {
                relatedEdges.Add(edge);
            }

            foreach (Edge edge in vertex.GetOutgoingEdges())
            {
                relatedEdges.Add(edge);
            }

            foreach (Edge edge in relatedEdges)
            {
                this.UnregisterEdge(edge);
            }

            if (relatedEdges.Count > 0)
            {
                this.OnGraphStructureChanged(ChangeAction.Remove, relatedEdges);
            }
        }

        public Edge AddArrow(Vertex firstVertex, Vertex secondVertex)
        {
            return this.AddArrow(firstVertex, secondVertex, null);
        }

        public Edge AddArrow(Vertex firstVertex, Vertex secondVertex, object arrowValue)
        {
            return this.AddEdge(firstVertex, secondVertex, true, arrowValue);
        }

        public Edge AddLine(Vertex firstVertex, Vertex secondVertex)
        {
            return this.AddLine(firstVertex, secondVertex, null);
        }

        public Edge AddLine(Vertex firstVertex, Vertex secondVertex, object lineValue)
        {
            return this.AddEdge(firstVertex, secondVertex, false, lineValue);
        }

        private Edge AddEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected, object edgeValue)
        {
            this.CheckVertexValidity(firstVertex);
            this.CheckVertexValidity(secondVertex);

            Edge edge = this.EdgeFactory.CreateEdge(firstVertex, secondVertex, isDirected, edgeValue);
            this.adjacencyList.AddEdge(edge);

            this.RegisterEdge(edge);

            this.OnGraphStructureChanged(ChangeAction.Add, edge);

            return edge;
        }

        public bool RemoveEdge(Edge edge)
        {
            Guard.ThrowExceptionIfNull(edge, "edge");

            if(!this.EdgeBelongsToThisStructure(edge))
            {
                return false;
            }

            bool result = this.adjacencyList.RemoveEdge(edge);

            if (result)
            {
                this.UnregisterEdge(edge);
                this.OnGraphStructureChanged(ChangeAction.Remove, edge);
            }

            return result;
        }

        public bool RemoveEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            this.CheckVertexValidity(firstVertex);
            this.CheckVertexValidity(secondVertex);

            IEnumerable<Edge> edgesBetween = this.GetEdgesBetween(firstVertex, secondVertex);

            bool result = this.adjacencyList.RemoveAllEdgesBetween(firstVertex, secondVertex);

            if (result)
            {
                foreach (Edge edge in edgesBetween)
                {
                    this.UnregisterEdge(edge);
                }

                this.OnGraphStructureChanged(ChangeAction.Remove, edgesBetween);
            }

            return result;
        }

        public IEnumerable<Vertex> GetVertexSuccessors(Vertex vertex)
        {
            this.CheckVertexValidity(vertex);

            return this.GetVertexSuccessorsWithoutValidityCheck(vertex);
        }

        internal IEnumerable<Vertex> GetVertexSuccessorsWithoutValidityCheck(Vertex vertex)
        {
            return this.adjacencyList.GetAllSuccessors(vertex);
        }

        public IEnumerable<Vertex> GetVertexPredecessors(Vertex vertex)
        {
            this.CheckVertexValidity(vertex);

            return this.GetVertexPredecessorsWithoutValidityCheck(vertex);
        }

        internal IEnumerable<Vertex> GetVertexPredecessorsWithoutValidityCheck(Vertex vertex)
        {
            return this.adjacencyList.GetAllPredecessors(vertex);
        }

        public IEnumerable<Edge> GetEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            this.CheckVertexValidity(firstVertex);
            this.CheckVertexValidity(secondVertex);

            return this.adjacencyList.GetAllEdgesBetween(firstVertex, secondVertex);
        }

        public IEnumerable<Edge> GetEdgesLeadingFromTo(Vertex predecessor, Vertex successor)
        {
            this.CheckVertexValidity(predecessor);
            this.CheckVertexValidity(successor);

            return this.GetEdgesLeadingFromToWithoutValidityCheck(predecessor, successor);
        }

        public IEnumerable<Edge> GetEdgesLeadingFromToWithoutValidityCheck(Vertex predecessor, Vertex successor)
        {
            return this.adjacencyList.GetAllEdgesLeadingFromTo(predecessor, successor);
        }

        public IEnumerable<Edge> GetEdgesGoingOutOfVertex(Vertex vertex)
        {
            this.CheckVertexValidity(vertex);

            return this.GetEdgesGoingOutOfVertexWithoutValidityCheck(vertex);
        }

        internal IEnumerable<Edge> GetEdgesGoingOutOfVertexWithoutValidityCheck(Vertex vertex)
        {
            IEnumerable<Vertex> successors = this.GetVertexSuccessorsWithoutValidityCheck(vertex);
            HashSet<Edge> allEdges = new HashSet<Edge>();

            foreach (Vertex successor in successors)
            {
                IEnumerable<Edge> edgesToAdd = this.GetEdgesLeadingFromToWithoutValidityCheck(vertex, successor);
                foreach (Edge edge in edgesToAdd)
                {
                    allEdges.Add(edge);
                }
            }
            
            return allEdges;
        }

        public IEnumerable<Edge> GetEdgesComingIntoVertex(Vertex vertex)
        {
            this.CheckVertexValidity(vertex);

            return this.GetEdgesComingIntoVertexWithoutValidityCheck(vertex);
        }

        internal IEnumerable<Edge> GetEdgesComingIntoVertexWithoutValidityCheck(Vertex vertex)
        {
            IEnumerable<Vertex> predecessors = this.GetVertexPredecessorsWithoutValidityCheck(vertex);
            HashSet<Edge> allEdges = new HashSet<Edge>();

            foreach (Vertex predecessor in predecessors)
            {
                IEnumerable<Edge> edgesToAdd = this.GetEdgesLeadingFromToWithoutValidityCheck(predecessor, vertex);
                foreach (Edge edge in edgesToAdd)
                {
                    allEdges.Add(edge);
                }
            }
            
            return allEdges;
        }

        private void RegisterVertex(Vertex vertex)
        {
            if (this.valueToVertexIndex.ContainsKey(vertex.ValueAsObject))
            {
                throw new InvalidOperationException("There is already a vertex containing this value.");
            }

            this.valueToVertexIndex.Add(vertex.ValueAsObject, vertex);
            vertex.RegisterItemToAStructure(this);
        }

        private void UnregisterVertex(Vertex vertex)
        {
            vertex.UnregisterItemFromAnyStructure();
            this.valueToVertexIndex.Remove(vertex.ValueAsObject);
        }

        private void RegisterEdge(Edge edge)
        {
            this.edges.Add(edge);
            edge.RegisterItemToAStructure(this);
        }

        private void UnregisterEdge(Edge edge)
        {
            edge.UnregisterItemFromAnyStructure();
            this.edges.Remove(edge);
        }

        private void CheckVertexValidity(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            if (!this.VertexBelongsToThisStructure(vertex))
            {
                throw new ArgumentException("The vertex does not belong to this structure.");
            }
        }

        private bool EdgeBelongsToThisStructure(Edge edge)
        {
            return edge.Owner == this;
        }

        private bool VertexBelongsToThisStructure(Vertex vertex)
        {
            return vertex.Owner == this;
        }

        public delegate void GraphStructureChangedEventHandler(GraphStructureChangedEventArgs args);

        public event GraphStructureChangedEventHandler GraphStructureChanged;

        private void OnGraphStructureChanged(ChangeAction changeAction, params GraphStructureItem[] itemsChanged)
        {
            this.OnGraphStructureChanged(changeAction, itemsChanged as IEnumerable<GraphStructureItem>);
        }

        private void OnGraphStructureChanged(ChangeAction changeAction, IEnumerable<GraphStructureItem> itemsChanged)
        {
            if (this.GraphStructureChanged != null)
            {
                this.GraphStructureChanged(new GraphStructureChangedEventArgs(changeAction, itemsChanged));
            }
        }
    }
}

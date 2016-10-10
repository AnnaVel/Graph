using GraphCore.Edges;
using GraphCore.Utilities;
using GraphCore.VertexProperties;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    public class GraphStructure
    {
        private VertexFactory vertexFactory;
        private EdgeFactory edgeFactory;
        private GraphItemPropertyFactory vertexPropertyFactory;
        private GraphItemPropertyFactory edgePropertyFactory;

        private readonly Dictionary<object, Vertex> valueToVertexIndex;
        private readonly AdjacencyList adjacencyList;

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

        public GraphItemPropertyFactory VertexPropertyFactory
        {
            get
            {
                return this.vertexPropertyFactory;
            }
            set
            {
                this.vertexPropertyFactory = value;
            }
        }

        public GraphItemPropertyFactory EdgePropertyFactory
        {
            get
            {
                return this.edgePropertyFactory;
            }
            set
            {
                this.edgePropertyFactory = value;
            }
        }

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
                throw new NotImplementedException();
            }
        }

        public GraphStructure()
        {
            this.vertexFactory = new VertexFactory();
            this.edgeFactory = new EdgeFactory();
            this.vertexPropertyFactory = new GraphItemPropertyFactory();

            this.valueToVertexIndex = new Dictionary<object, Vertex>();
            this.adjacencyList = new AdjacencyList();
        }

        public Vertex AddVertex(object value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            Vertex newVertex = this.vertexFactory.CreateVertex(value);

            this.RegisterVertex(newVertex);

            return newVertex;
        }

        public bool RemoveVertex(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            return this.UnregisterVertex(vertex);
        }

        public Edge AddArrow(Vertex firstVertex, Vertex secondVertex)
        {
            return this.AddArrow(firstVertex, secondVertex, null);
        }

        public Edge AddArrow(Vertex firstVertex, Vertex secondVertex, object arrowValue)
        {
            this.CheckValidityOfVertexDuo(firstVertex, secondVertex);

            Edge edge = this.EdgeFactory.CreateEdge(firstVertex, secondVertex, true, arrowValue);
            this.adjacencyList.AddEdge(edge);

            return edge;
        }

        public Edge AddLine(Vertex firstVertex, Vertex secondVertex)
        {
            return this.AddLine(firstVertex, secondVertex, null);
        }

        public Edge AddLine(Vertex firstVertex, Vertex secondVertex, object lineValue)
        {
            this.CheckValidityOfVertexDuo(firstVertex, secondVertex);

            Edge edge = this.EdgeFactory.CreateEdge(firstVertex, secondVertex, false, lineValue);
            this.adjacencyList.AddEdge(edge);

            return edge;
        }

        public bool RemoveEdge(Edge edge)
        {
            return this.adjacencyList.RemoveEdge(edge);
        }

        public bool RemoveEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            this.CheckValidityOfVertexDuo(firstVertex, secondVertex);

            return this.adjacencyList.RemoveAllEdgesBetween(firstVertex, secondVertex);
        }

        public IEnumerable<Vertex> GetVertexSuccessors(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            return this.adjacencyList.GetAllSuccessors(vertex);
        }

        public IEnumerable<Vertex> GetVertexPredecessors(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            return this.adjacencyList.GetAllPredecessors(vertex);
        }

        public IEnumerable<Edge> GetEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            this.CheckValidityOfVertexDuo(firstVertex, secondVertex);

            return this.adjacencyList.GetAllEdgesBetween(firstVertex, secondVertex);
        }

        public IEnumerable<Edge> GetEdgesLeadingFromTo(Vertex predecessor, Vertex successor)
        {
            this.CheckValidityOfVertexDuo(predecessor, successor);

            return this.adjacencyList.GetAllEdgesLeadingFromTo(predecessor, successor);
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

        private bool UnregisterVertex(Vertex vertex)
        {
            if (!this.VertexBelongsToThisStructure(vertex))
            {
                return false;
            }

            this.adjacencyList.RemoveVertex(vertex);
            this.valueToVertexIndex.Remove(vertex.ValueAsObject);
            vertex.UnregisterItemFromAnyStructure();

            return true;
        }

        private void CheckValidityOfVertexDuo(Vertex firstVertex, Vertex secondVertex)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            if (!this.VertexBelongsToThisStructure(firstVertex) ||
                !this.VertexBelongsToThisStructure(secondVertex))
            {
                throw new ArgumentException("One of the vertices does not belong to this structure.");
            }
        }

        private bool VertexBelongsToThisStructure(Vertex vertex)
        {
            return this.valueToVertexIndex.ContainsKey(vertex.ValueAsObject) &&
                this.valueToVertexIndex[vertex.ValueAsObject] == vertex;
        }
    }
}

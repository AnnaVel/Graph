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
    internal class GraphStructure
    {
        private VertexFactory vertexFactory;
        private GraphItemPropertyFactory graphItemPropertyFactory;

        private readonly Dictionary<object, Vertex> valueToVertexIndex;
        private readonly AdjacencyList successorAdjacencyList;
        private readonly AdjacencyList predecessorAdjacencyList;

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

        public GraphItemPropertyFactory GraphItemPropertyFactory
        {
            get
            {
                return this.graphItemPropertyFactory;
            }
            set
            {
                this.graphItemPropertyFactory = value;
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.valueToVertexIndex.Values;
            }
        }

        public GraphStructure()
        {
            this.vertexFactory = new VertexFactory();
            this.graphItemPropertyFactory = new GraphItemPropertyFactory();

            this.valueToVertexIndex = new Dictionary<object, Vertex>();
            this.successorAdjacencyList = new AdjacencyList();
            this.predecessorAdjacencyList = new AdjacencyList();
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

        public void AddArrow(Vertex firstVertex, Vertex secondVertex, double? weight)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            if (!this.VertexBelongsToThisStructure(firstVertex) ||
                !this.VertexBelongsToThisStructure(secondVertex))
            {
                throw new ArgumentException("One of the vertices does not belong to this structure.");
            }

            this.successorAdjacencyList.AddAdjacentVertexToVertex(firstVertex, secondVertex, weight);
            this.predecessorAdjacencyList.AddAdjacentVertexToVertex(secondVertex, firstVertex, weight);
        }

        public bool RemoveArrows(Vertex firstVertex, Vertex secondVertex)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            if (!this.VertexBelongsToThisStructure(firstVertex) ||
                !this.VertexBelongsToThisStructure(secondVertex))
            {
                throw new ArgumentException("One of the vertices does not belong to this structure.");
            }

            bool result = this.successorAdjacencyList.RemoveAdjacentVertexFromVertex(firstVertex, secondVertex);

            if (result)
            {
                this.predecessorAdjacencyList.RemoveAdjacentVertexFromVertex(secondVertex, firstVertex);
            }

            return result;
        }

        public IEnumerable<Vertex> GetVertexSuccessors(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            if (!this.VertexBelongsToThisStructure(vertex))
            {
                throw new ArgumentException("The vertex does not belong to this structure.");
            }

            return this.successorAdjacencyList.GetAdjacentVertices(vertex);
        }

        public IEnumerable<double?> GetArrowWeights(Vertex vertex, Vertex successor)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");
            Guard.ThrowExceptionIfNull(successor, "successor");

            if (!this.VertexBelongsToThisStructure(vertex) ||
                !this.VertexBelongsToThisStructure(successor))
            {
                throw new ArgumentException("One of the vertices does not belong to this structure.");
            }

            return this.successorAdjacencyList.GetWeights(vertex, successor);
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

            IEnumerable<Vertex> predecessors = this.predecessorAdjacencyList.GetAdjacentVertices(vertex);

            foreach (Vertex predecessor in predecessors)
            {
                this.successorAdjacencyList.RemoveAdjacentVertexFromVertex(predecessor, vertex);
            }

            this.predecessorAdjacencyList.RemoveAdjacencyListItem(vertex);
            this.successorAdjacencyList.RemoveAdjacencyListItem(vertex);
            this.valueToVertexIndex.Remove(vertex.ValueAsObject);

            vertex.UnregisterItemFromAnyStructure();

            return true;
        }

        private bool VertexBelongsToThisStructure(Vertex vertex)
        {
            return this.valueToVertexIndex.ContainsKey(vertex.ValueAsObject) &&
                this.valueToVertexIndex[vertex.ValueAsObject] == vertex;
        }
    }
}

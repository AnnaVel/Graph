using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    internal class VertexStructure
    {
        private VertexFactory vertexFactory;

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
                vertexFactory = value;
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.valueToVertexIndex.Values;
            }
        }

        public VertexStructure()
        {
            this.vertexFactory = new VertexFactory();

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

        public void AddArrow(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public bool RemoveArrows(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> GetVertexSuccessors(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<double?> GetArrowWeights(Vertex vertex, Vertex successor)
        {
            throw new NotImplementedException();
        }

        private void RegisterVertex(Vertex vertex)
        {
            if (this.valueToVertexIndex.ContainsKey(vertex.ValueAsObject))
            {
                throw new InvalidOperationException("There is already a vertex containing this value.");
            }

            this.valueToVertexIndex.Add(vertex.ValueAsObject, vertex);
            vertex.RegisterVertexToAStructure(this);
        }

        private bool UnregisterVertex(Vertex vertex)
        {
            if (!this.valueToVertexIndex.ContainsKey(vertex.ValueAsObject) ||
                this.valueToVertexIndex[vertex.ValueAsObject] != vertex)
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

            vertex.UnregisterVertexFromAnyStructure();

            return true;
        }
    }
}

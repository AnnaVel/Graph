using GraphCore.Edges;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    internal class AdjacencyList
    {
        private readonly Dictionary<Vertex, AdjacencyItem> innerList;

        public AdjacencyList()
        {
            this.innerList = new Dictionary<Vertex, AdjacencyItem>();
        }

        public IEnumerable<Edge> Edges
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public void AddEdge(Edge edge)
        {
            if (edge.IsDirected)
            {
                this.AddDirectedEdge(edge);
            }
            else
            {
                this.AddUndirectedEdge(edge);
            }
        }

        private void AddUndirectedEdge(Edge edge)
        {
            throw new NotImplementedException();
        }

        private void AddDirectedEdge(Edge edge)
        {
            Vertex predecessor = edge.FirstVertex;

            if (!this.innerList.ContainsKey(predecessor))
            {
                this.innerList.Add(predecessor, new AdjacencyItem(predecessor));
            }

            this.innerList[predecessor].AddSuccessorEdge(edge);
        }

        public bool RemoveEdge(Edge edge)
        {
            if (edge.IsDirected)
            {
                return this.RemoveDirectedEdge(edge);
            }
            else
            {
                return this.RemoveUndirectedEdge(edge);
            }
        }

        private bool RemoveDirectedEdge(Edge edge)
        {
            this.RemoveVertexFromDictionaryIfItemIsEmpty();

            throw new NotImplementedException();
        }

        private bool RemoveUndirectedEdge(Edge edge)
        {
            this.RemoveVertexFromDictionaryIfItemIsEmpty();

            throw new NotImplementedException();
        }

        public bool RemoveAllEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            this.RemoveVertexFromDictionaryIfItemIsEmpty();

            throw new NotImplementedException();
        }

        public IEnumerable<Edge> GetAllEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Edge> GetAllEdgesLeadingFromTo(Vertex predecessor, Vertex successor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> GetAllSuccessors(Vertex vertex)
        {
            if (!this.innerList.ContainsKey(vertex))
            {
                return Enumerable.Empty<Vertex>();
            }

            return this.innerList[vertex].GetSuccessors();
        }

        public IEnumerable<Vertex> GetAllPredecessors(Vertex vertex)
        {
            if (!this.innerList.ContainsKey(vertex))
            {
                return Enumerable.Empty<Vertex>();
            }

            return this.innerList[vertex].GetPredecessors();
        }

        public bool RemoveVertex(Vertex vertex)
        {
            if(!this.innerList.ContainsKey(vertex))
            {
                return false;
            }

            IEnumerable<Vertex> vertexSuccessors = this.innerList[vertex].GetSuccessors();
            IEnumerable<Vertex> vertexPredecessors = this.innerList[vertex].GetPredecessors();

            foreach (Vertex successor in vertexSuccessors)
            {
                this.innerList[successor].RemovePredecessor(vertex);
            }

            foreach (Vertex predecessor in vertexPredecessors)
            {
                this.innerList[predecessor].RemoveSuccessor(vertex);
            }

            this.innerList.Remove(vertex);

            return true;
        }

        private void RemoveVertexFromDictionaryIfItemIsEmpty()
        {
            throw new NotImplementedException();
        }
    }
}

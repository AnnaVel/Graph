using GraphCore.Edges;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    internal class AdjacencyItem
    {
        private readonly Vertex owningVertex;

        private Dictionary<Vertex, List<Edge>> successors;
        private Dictionary<Vertex, List<Edge>> predecessors;

        public IEnumerable<Edge> RelatedEdges
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private Dictionary<Vertex, List<Edge>> Successors
        {
            get
            {
                if (this.successors == null)
                {
                    this.successors = new Dictionary<Vertex, List<Edge>>();
                }

                return this.successors;
            }
        }

        private Dictionary<Vertex, List<Edge>> Predecessors
        {
            get
            {
                if (this.predecessors == null)
                {
                    this.predecessors = new Dictionary<Vertex, List<Edge>>();
                }

                return this.predecessors;
            }
        }

        public AdjacencyItem(Vertex owningVertex)
        {
            Guard.ThrowExceptionIfNull(owningVertex, "owningVertex");

            this.owningVertex = owningVertex;
        }

        public void AddSuccessorEdge(Edge edge)
        {
            this.CheckEdgeValidity(edge);

            Vertex successor;

            if (edge.IsDirected)
            {
                successor = edge.SecondVertex;
            }
            else
            {
                successor = edge.FirstVertex == this.owningVertex ? edge.SecondVertex : edge.FirstVertex;
            }

            Dictionary<Vertex, List<Edge>> successors = this.Successors;

            if (!successors.ContainsKey(successor))
            {
                successors.Add(successor, new List<Edge>());
            }

            successors[successor].Add(edge);
        }

        public void AddPredecessorEdge(Edge edge)
        {
            this.CheckEdgeValidity(edge);

            Vertex predecessor;

            if (edge.IsDirected)
            {
                predecessor = edge.FirstVertex;
            }
            else
            {
                predecessor = edge.SecondVertex == this.owningVertex ? edge.FirstVertex : edge.SecondVertex;
            }

            Dictionary<Vertex, List<Edge>> predecessors = this.Predecessors;

            if (!predecessors.ContainsKey(predecessor))
            {
                predecessors.Add(predecessor, new List<Edge>());
            }

            predecessors[predecessor].Add(edge);
        }

        public bool RemoveSuccessorEdge(Edge edge)
        {
            //this.RemoveVertexFromSuccessorsDictionaryIfEdgeListEmpty();

            throw new NotImplementedException();
        }

        public bool RemovePredecessorEdge(Edge edge)
        {


            //this.RemoveVertexFromPredecessorsrDictionaryIfEdgeListEmpty();

            throw new NotImplementedException();
        }

        public bool RemoveSuccessor(Vertex successor)
        {
            this.SetSuccessorsDictionaryToNullIfEmpty();

            throw new NotImplementedException();
        }

        public bool RemovePredecessor(Vertex successor)
        {
            this.SetPredecessorsDictionaryToNullIfEmpty();

            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> GetSuccessors()
        {
            return this.Successors.Keys;
        }

        public IEnumerable<Vertex> GetPredecessors()
        {
            return this.Predecessors.Keys;
        }

        public IEnumerable<Edge> GetEdgesLeadingTo(Vertex successor)
        {
            Dictionary<Vertex, List<Edge>> successors = this.Successors;

            return this.GetEdgesConnectedToVertex(successors, successor);
        }

        public IEnumerable<Edge> GetEdgesComingFrom(Vertex predecessor)
        {
            Dictionary<Vertex, List<Edge>> predecessors = this.Predecessors;

            return this.GetEdgesConnectedToVertex(predecessors, predecessor);
        }

        private IEnumerable<Edge> GetEdgesConnectedToVertex(Dictionary<Vertex, List<Edge>> dictionary, Vertex neighbour)
        {
            if (!dictionary.ContainsKey(neighbour))
            {
                return Enumerable.Empty<Edge>();
            }

            return dictionary[neighbour];
        }

        private void CheckEdgeValidity(Edge edge)
        {
            Guard.ThrowExceptionIfNull(edge, "edge");

            bool owningVertexIsOneOfEdgeVertices = this.owningVertex == edge.FirstVertex || this.owningVertex == edge.SecondVertex;

            if (!owningVertexIsOneOfEdgeVertices)
            {
                throw new InvalidOperationException("This edge is not related to the vertex.");
            }
        }

        private void RemoveVertexFromSuccessorsDictionaryIfEdgeListEmpty(Vertex vertex)
        {

            this.SetSuccessorsDictionaryToNullIfEmpty();
            throw new NotImplementedException();
        }

        private void RemoveVertexFromPredecessorsrDictionaryIfEdgeListEmpty(Vertex vertex)
        {
            this.SetPredecessorsDictionaryToNullIfEmpty();
            throw new NotImplementedException();
        }

        private void SetSuccessorsDictionaryToNullIfEmpty()
        {
            if (this.successors.Count == 0)
            {
                this.successors = null;
            }
        }

        private void SetPredecessorsDictionaryToNullIfEmpty()
        {
            if (this.predecessors.Count == 0)
            {
                this.predecessors = null;
            }
        }
    }
}

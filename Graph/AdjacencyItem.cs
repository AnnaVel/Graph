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

        private bool edgeListInvalidated;
        private HashSet<Edge> relatedEdges;

        private Dictionary<Vertex, List<Edge>> successors;
        private Dictionary<Vertex, List<Edge>> predecessors;

        public IEnumerable<Edge> RelatedEdges
        {
            get
            {
                if (this.edgeListInvalidated)
                {
                    this.ConstructEdgeList();
                }

                return this.relatedEdges;
            }
        }

        public bool IsEmpty
        {
            get
            {
                bool successorAndPredecessorListsEmpty = (this.successors == null || this.successors.Count == 0) &&
                                                        (this.predecessors == null || this.predecessors.Count == 0);
                
                return successorAndPredecessorListsEmpty;
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
            this.edgeListInvalidated = true;
        }

        public void AddSuccessorEdge(Edge edge)
        {
            this.ThrowExceptionIfEdgeIsInvalid(edge);

            Vertex successor = this.GetSuccessorVertexFromEdge(edge);

            Dictionary<Vertex, List<Edge>> successors = this.Successors;

            if (!successors.ContainsKey(successor))
            {
                successors.Add(successor, new List<Edge>());
            }

            successors[successor].Add(edge);

            this.OnEdgesChanged();
        }

        public void AddPredecessorEdge(Edge edge)
        {
            this.ThrowExceptionIfEdgeIsInvalid(edge);

            Vertex predecessor = this.GetPredecessorVertexFromEdge(edge);

            Dictionary<Vertex, List<Edge>> predecessors = this.Predecessors;

            if (!predecessors.ContainsKey(predecessor))
            {
                predecessors.Add(predecessor, new List<Edge>());
            }

            predecessors[predecessor].Add(edge);

            this.OnEdgesChanged();
        }

        public bool RemoveSuccessorEdge(Edge edge)
        {
            if (!this.OwningVertexIsOneOfEdgeVertices(edge))
            {
                return false;
            }

            Vertex successor = this.GetSuccessorVertexFromEdge(edge);

            bool result = this.Successors[successor].Remove(edge);

            if (result)
            {
                this.RemoveVertexFromSuccessorsDictionaryIfEdgeListEmpty(successor);
                this.OnEdgesChanged();
            }

            return result;
        }
       
        public bool RemovePredecessorEdge(Edge edge)
        {
            if (!this.OwningVertexIsOneOfEdgeVertices(edge))
            {
                return false;
            }

            Vertex predecessor = this.GetPredecessorVertexFromEdge(edge);

            bool result = this.Predecessors[predecessor].Remove(edge);

            if (result)
            {
                this.RemoveVertexFromPredecessorsDictionaryIfEdgeListEmpty(predecessor);
                this.OnEdgesChanged();
            }

            return result;
        }

        public bool RemoveSuccessorAndCorrespondingEdges(Vertex successor)
        {
            bool result = this.Successors.Remove(successor);

            if (result)
            {
                this.SetSuccessorsDictionaryToNullIfEmpty();
                this.OnEdgesChanged();
            }

            return result;
        }

        public bool RemovePredecessorAndCorrespondingEdges(Vertex predecessor)
        {
            bool result = this.Predecessors.Remove(predecessor);

            if (result)
            {
                this.SetPredecessorsDictionaryToNullIfEmpty();
                this.OnEdgesChanged();
            }

            return result;
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

        private void ThrowExceptionIfEdgeIsInvalid(Edge edge)
        {
            Guard.ThrowExceptionIfNull(edge, "edge");

            if (!this.OwningVertexIsOneOfEdgeVertices(edge))
            {
                throw new InvalidOperationException("This edge is not related to the vertex.");
            }
        }

        private bool OwningVertexIsOneOfEdgeVertices(Edge edge)
        {
            return this.owningVertex == edge.FirstVertex || this.owningVertex == edge.SecondVertex;
        }

        private Vertex GetSuccessorVertexFromEdge(Edge edge)
        {
            if(!this.OwningVertexIsOneOfEdgeVertices(edge))
            {
                throw new InvalidOperationException();
            }

            Vertex successor;

            if (edge.IsDirected)
            {
                successor = edge.SecondVertex;
            }
            else
            {
                successor = edge.FirstVertex == this.owningVertex ? edge.SecondVertex : edge.FirstVertex;
            }

            return successor;
        }

        private Vertex GetPredecessorVertexFromEdge(Edge edge)
        {
            if (!this.OwningVertexIsOneOfEdgeVertices(edge))
            {
                throw new InvalidOperationException();
            }

            Vertex predecessor;

            if (edge.IsDirected)
            {
                predecessor = edge.FirstVertex;
            }
            else
            {
                predecessor = edge.SecondVertex == this.owningVertex ? edge.FirstVertex : edge.SecondVertex;
            }

            return predecessor;
        }

        private void OnEdgesChanged()
        {
            this.edgeListInvalidated = true;
        }

        private void ConstructEdgeList()
        {
            HashSet<Edge> relatedEdges = new HashSet<Edge>();

            //TODO: using the properties will probably lead to a lot of setting and unsetting of the field.
            foreach (var pair in this.Predecessors)
            {
                List<Edge> edgesLeadingToSuccessorVertex = pair.Value;
                foreach (Edge edge in edgesLeadingToSuccessorVertex)
                {
                    relatedEdges.Add(edge);
                }
            }

            foreach (var pair in this.Predecessors)
            {
                List<Edge> edgesLeadingToSuccessorVertex = pair.Value;
                foreach (Edge edge in edgesLeadingToSuccessorVertex)
                {
                    relatedEdges.Add(edge);
                }
            }

            this.relatedEdges = relatedEdges;
            this.edgeListInvalidated = false;
        }

        private void RemoveVertexFromSuccessorsDictionaryIfEdgeListEmpty(Vertex vertex)
        {
            if (this.Successors[vertex].Count == 0)
            {
                this.Successors.Remove(vertex);
            }

            this.SetSuccessorsDictionaryToNullIfEmpty();
        }

        private void RemoveVertexFromPredecessorsDictionaryIfEdgeListEmpty(Vertex vertex)
        {
            if (this.Predecessors[vertex].Count == 0)
            {
                this.Predecessors.Remove(vertex);
            }

            this.SetPredecessorsDictionaryToNullIfEmpty();
        }

        private void SetSuccessorsDictionaryToNullIfEmpty()
        {
            if (this.Successors.Count == 0)
            {
                this.successors = null;
            }
        }

        private void SetPredecessorsDictionaryToNullIfEmpty()
        {
            if (this.Predecessors.Count == 0)
            {
                this.predecessors = null;
            }
        }
    }
}

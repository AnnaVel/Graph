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

        public void AddEdge(Edge edge)
        {
            if (edge.IsDirected)
            {
                this.AddEdgeInOneDirection(edge, edge.FirstVertex, edge.SecondVertex);
            }
            else
            {
                this.AddEdgeInOneDirection(edge, edge.FirstVertex, edge.SecondVertex);
                this.AddEdgeInOneDirection(edge, edge.SecondVertex, edge.FirstVertex);
            }
        }

        private void AddEdgeInOneDirection(Edge edge, Vertex predecessor, Vertex successor)
        {
            if (!this.innerList.ContainsKey(predecessor))
            {
                this.innerList.Add(predecessor, new AdjacencyItem(predecessor));
            }

            this.innerList[predecessor].AddSuccessorEdge(edge);

            if (!this.innerList.ContainsKey(successor))
            {
                this.innerList.Add(successor, new AdjacencyItem(successor));
            }

            this.innerList[successor].AddPredecessorEdge(edge);
        }

        public bool RemoveEdge(Edge edge)
        {
            bool result = false;

            if (edge.IsDirected)
            {
                result = this.RemoveEdgeInOneDirection(edge, edge.FirstVertex, edge.SecondVertex);
            }
            else
            {
                result |= this.RemoveEdgeInOneDirection(edge, edge.FirstVertex, edge.SecondVertex);
                result |= this.RemoveEdgeInOneDirection(edge, edge.SecondVertex, edge.FirstVertex);
            }

            return result;
        }

        private bool RemoveEdgeInOneDirection(Edge edge, Vertex predecessor, Vertex successor)
        {
            if (!this.innerList.ContainsKey(predecessor))
            {
                return false;
            }

            bool result = this.innerList[predecessor].RemoveSuccessorEdge(edge);

            this.RemoveVertexFromDictionaryIfItemIsEmpty(predecessor);

            if (!this.innerList.ContainsKey(successor))
            {
                return false;
            }

            result |= this.innerList[successor].RemovePredecessorEdge(edge);

            this.RemoveVertexFromDictionaryIfItemIsEmpty(successor);

            return result;
        }

        public bool RemoveAllEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            bool resultFirstDirection = false;

            if (!this.innerList.ContainsKey(firstVertex))
            {
                resultFirstDirection = false;
            }
            else
            {
                resultFirstDirection = this.innerList[firstVertex].RemoveSuccessorAndCorrespondingEdges(secondVertex);
                resultFirstDirection |= this.innerList[secondVertex].RemovePredecessorAndCorrespondingEdges(firstVertex);
            }

            bool resultSecondDirection = false;

            if (!this.innerList.ContainsKey(secondVertex))
            {
                resultSecondDirection = false;
            }
            else
            {
                resultSecondDirection = this.innerList[secondVertex].RemoveSuccessorAndCorrespondingEdges(firstVertex);
                resultSecondDirection |= this.innerList[firstVertex].RemovePredecessorAndCorrespondingEdges(secondVertex);
            }

            return resultFirstDirection || resultSecondDirection;
        }

        public IEnumerable<Edge> GetAllEdgesBetween(Vertex firstVertex, Vertex secondVertex)
        {
            HashSet<Edge> edges = new HashSet<Edge>();

            if(this.innerList.ContainsKey(firstVertex))
            {
                IEnumerable<Edge> firstToSecond = this.innerList[firstVertex].GetEdgesLeadingTo(secondVertex);

                foreach (Edge edge in firstToSecond)
                {
                    edges.Add(edge);
                }
            }

            if (this.innerList.ContainsKey(secondVertex))
            {
                IEnumerable<Edge> secondToFirst = this.innerList[secondVertex].GetEdgesLeadingTo(firstVertex);

                foreach (Edge edge in secondToFirst)
                {
                    edges.Add(edge);
                }
            }

            return edges;
        }

        public IEnumerable<Edge> GetAllEdgesLeadingFromTo(Vertex predecessor, Vertex successor)
        {
            if (!this.innerList.ContainsKey(predecessor))
            {
                return Enumerable.Empty<Edge>();
            }

            return this.innerList[predecessor].GetEdgesLeadingTo(successor);
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
                this.innerList[successor].RemovePredecessorAndCorrespondingEdges(vertex);
            }

            foreach (Vertex predecessor in vertexPredecessors)
            {
                this.innerList[predecessor].RemoveSuccessorAndCorrespondingEdges(vertex);
            }

            this.innerList.Remove(vertex);

            return true;
        }

        private void RemoveVertexFromDictionaryIfItemIsEmpty(Vertex key)
        {
            if (this.innerList.ContainsKey(key) && this.innerList[key].IsEmpty)
            {
                this.innerList.Remove(key);
            }
        }
    }
}

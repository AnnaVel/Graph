using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    internal class AdjacencyList
    {
        private readonly Dictionary<Vertex, Dictionary<Vertex, List<double?>>> internalList;

        public AdjacencyList()
        {
            this.internalList = new Dictionary<Vertex, Dictionary<Vertex, List<double?>>>();
        }

        public void AddAdjacentVertexToVertex(Vertex vertex, Vertex adjacentVertex, double? weight)
        {
            if (!this.internalList.ContainsKey(vertex))
            {
                this.internalList.Add(vertex, new Dictionary<Vertex,List<double?>>());
            }

            Dictionary<Vertex, List<double?>> adjacentVertices = this.internalList[vertex];

            if(!adjacentVertices.ContainsKey(adjacentVertex))
            {
                adjacentVertices.Add(adjacentVertex, new List<double?>());
            }

            adjacentVertices[adjacentVertex].Add(weight);
        }

        public bool RemoveAdjacentVertexFromVertex(Vertex vertex, Vertex adjacentVertex)
        {
            if (!this.internalList.ContainsKey(vertex))
            {
                return false;
            }

            bool success = this.internalList[vertex].Remove(adjacentVertex);

            if (this.internalList[vertex].Count == 0)
            {
                this.RemoveAdjacencyListItem(vertex);
            }

            return success;
        }

        public bool RemoveAdjacencyListItem(Vertex vertex)
        {
            return this.internalList.Remove(vertex);
        }

        public IEnumerable<Vertex> GetAdjacentVertices(Vertex vertex)
        {
            if (!this.internalList.ContainsKey(vertex))
            {
                return Enumerable.Empty<Vertex>();
            }

            return this.internalList[vertex].Keys;
        }

        public IEnumerable<double?> GetWeights(Vertex vertex, Vertex adjacentVertex)
        {
            if (!this.internalList.ContainsKey(vertex))
            {
                return Enumerable.Empty<double?>();
            }

            if (!this.internalList[vertex].ContainsKey(adjacentVertex))
            {
                return Enumerable.Empty<double?>();
            }

            return this.internalList[vertex][adjacentVertex];
        }
    }
}

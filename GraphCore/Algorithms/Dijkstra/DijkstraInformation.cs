using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

namespace GraphCore.Algorithms
{
    internal class DijkstraInformation
    {
        private readonly HashSet<Vertex> visitedVertices;
        private readonly OrderedMultiDictionary<double, Vertex> notVisitedVertices;
        private readonly Dictionary<Vertex, double> verticesDistancesFromStart;
        private readonly Dictionary<Vertex, Vertex> previousVertex;

        private readonly Comparison<double> doubleComparison = (d, o) => { return d.CompareTo(o); };
        private readonly Comparison<Vertex> vertexComparison = (v, o) => 
        {
            // OrderedMultiDictionary requires comparison in order to arrange the vertices in the collection.
            // While the vertices cannot be compared as their values might not always be comparable, 
            // the hashcodes should do the trick as they will both identify the vertices which are equal, 
            // and arrange the vertices in a consistent order. This solution relies on the fact that the vertices
            // all have unique values. Moreover, the order will be consistent for the run of the algorithm,
            // but in some cases the same graph will be traversed in different order in consecutive runs.
            // E.g. two vertices are at the same distance from the start and on the first run the value of
            // the first has a smaller hash and on the second, the value of the second has a smaller hash.
            // The best solution is to implement another OrderedMultiDictionary where only the keys are ordered
            // and the values are ordered in order of their being put in the dictionary and compared based on 
            // their Equals implementation.
            return v.ValueAsObject.GetHashCode().CompareTo(o.ValueAsObject.GetHashCode());
        };

        public bool NegativeEdgeFound { get; set; }

        public DijkstraInformation()
        {
            this.visitedVertices = new HashSet<Vertex>();
            this.notVisitedVertices = new OrderedMultiDictionary<double, Vertex>(true, doubleComparison, vertexComparison);
            this.verticesDistancesFromStart = new Dictionary<Vertex, double>();
            this.previousVertex = new Dictionary<Vertex, Vertex>();
        }

        public void SetDistanceFromStartForVertex(Vertex vertex, double distance, Vertex previous)
        {
            if (this.verticesDistancesFromStart.ContainsKey(vertex)) 
            //If the distance collection does not contain this vertex, then the notVisistedVertices will not either and removing it will not be necessary.
            {
                this.notVisitedVertices.Remove(this.verticesDistancesFromStart[vertex], vertex);
            }

            this.notVisitedVertices.Add(distance, vertex);
            this.verticesDistancesFromStart[vertex] = distance;
            this.previousVertex[vertex] = previous;
        }

        public double GetVertexDistanceFromStart(Vertex vertex)
        {
            return verticesDistancesFromStart.ContainsKey(vertex) ? verticesDistancesFromStart[vertex] : Double.PositiveInfinity;
        }

        public Vertex GetNotVisistedVertexWithSmallestDistanceFromStart()
        {
            if(this.notVisitedVertices.Count == 0)
            {
                return null;
            }

            KeyValuePair<double, ICollection<Vertex>> smallestPair = this.notVisitedVertices.First();
            return smallestPair.Value.First();
        }

        public void MarkAsVisisted(Vertex vertex)
        {
            this.notVisitedVertices.Remove(this.verticesDistancesFromStart[vertex], vertex);
            this.visitedVertices.Add(vertex);
        }

        public bool IsVisisted(Vertex vertex)
        {
            return this.visitedVertices.Contains(vertex);
        }

        public List<Vertex> ConstructShortestPath(Vertex endVertex)
        {
            List<Vertex> path = new List<Vertex>();

            Vertex currentVertex = endVertex;

            while (this.previousVertex.TryGetValue(currentVertex, out currentVertex) &&
                currentVertex != null)
            {
                path.Add(currentVertex);
            }

            // This means that either the start and end vertices coincide or there is no path between them.
            if (path.Count == 0)
            {
                return path;
            }

            path.Reverse();
            path.Add(endVertex);

            return path;
        }

        public Dictionary<Vertex, double> GetDistancesFromStartForAllVertices()
        {
            return new Dictionary<Vertex, double>(this.verticesDistancesFromStart);
        }
    }
}

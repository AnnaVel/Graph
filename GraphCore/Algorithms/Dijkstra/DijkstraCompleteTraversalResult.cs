using System.Collections.Generic;
using GraphCore.Vertices;
using GraphCore.Utilities;

namespace GraphCore.Algorithms
{
    public class DijkstraCompleteTraversalResult : DijkstraResult
    {
        private readonly Dictionary<Vertex, double> distancesFromStart;

        /// <summary>
        /// Contains information about the distance of each vertex from the start vertex. If the vertex
        /// is not present in the dictionary, the distance is infinity.
        /// </summary>
        public Dictionary<Vertex, double> DistancesFromStart
        {
            get
            {
                return this.distancesFromStart;
            }
        }

        public DijkstraCompleteTraversalResult(Dictionary<Vertex, double> distancesFromStart, bool resultIsValid)
            : base(resultIsValid)
        {
            Guard.ThrowExceptionIfNull(distancesFromStart, "distancesFromStart");

            this.distancesFromStart = distancesFromStart;
        }
    }
}

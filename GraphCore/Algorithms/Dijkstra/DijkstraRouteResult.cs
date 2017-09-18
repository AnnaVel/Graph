using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public class DijkstraRouteResult : DijkstraResult
    {
        private List<Vertex> shortestPath;
        private double totalDistance;

        public IEnumerable<Vertex> ShortestPath
        {
            get
            {
                return this.shortestPath;
            }
        }

        public double TotalDistance
        {
            get
            {
                return this.totalDistance;
            }
        }
        public DijkstraRouteResult(double totalDistance, List<Vertex> shortestPath, bool resultIsValid)
            :base(resultIsValid)
        {
            Guard.ThrowExceptionIfNull(shortestPath, "shortestPath");

            this.shortestPath = shortestPath;
            this.totalDistance = totalDistance;
        }
    }
}

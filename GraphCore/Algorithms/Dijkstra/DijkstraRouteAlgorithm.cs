using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public class DijkstraRouteAlgorithm : DijkstraAlgorithmBase<DijkstraRouteParameter, DijkstraRouteResult>
    {
        public override string Name
        {
            get
            {
                return AlgorithmNames.DijkstraRouteAlgorithmName;
            }
        }

        internal override DijkstraRouteResult ConstructResult(DijkstraInformation dijkstraInfos, DijkstraRouteParameter parameter)
        {
            double totalDistance = dijkstraInfos.GetVertexDistanceFromStart(parameter.EndVertex);
            List<Vertex> shortestPath = dijkstraInfos.ConstructShortestPath(parameter.EndVertex);
            bool resultIsValid = !dijkstraInfos.NegativeEdgeFound;
            return new DijkstraRouteResult(totalDistance, shortestPath, resultIsValid);
        }

        protected override bool ShouldExitAlgorithm(Vertex currentVertex, DijkstraRouteParameter parameter)
        {
            if (currentVertex == parameter.EndVertex || currentVertex == null)
            {
                return true;
            }

            return false;
        }
    }
}

using System;
using GraphCore.Vertices;
using System.Collections.Generic;

namespace GraphCore.Algorithms
{
    public class DijkstraCompleteTraversalAlgorithm : DijkstraAlgorithmBase<DijkstraParameter, DijkstraCompleteTraversalResult>
    {
        public override string Name
        {
            get
            {
                return AlgorithmNames.DijkstraRouteAlgorithmName;
            }
        }

        protected override bool ShouldExitAlgorithm(Vertex currentVertex, DijkstraParameter parameter)
        {
            if (currentVertex == null)
            {
                return true;
            }

            return false;
        }

        internal override DijkstraCompleteTraversalResult ConstructResult(DijkstraInformation dijkstraInfos, DijkstraParameter parameter)
        {
            Dictionary<Vertex, double> allDistances = dijkstraInfos.GetDistancesFromStartForAllVertices();
            bool isResultValid = !dijkstraInfos.NegativeEdgeFound;
            return new DijkstraCompleteTraversalResult(allDistances, isResultValid);
        }
    }
}

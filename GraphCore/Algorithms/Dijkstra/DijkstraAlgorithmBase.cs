using GraphCore.Edges;
using GraphCore.Vertices;
using System.Collections.Generic;
using System;
using System.Diagnostics;

namespace GraphCore.Algorithms
{
    public abstract class DijkstraAlgorithmBase<P, R> : AlgorithmBase<DijkstraParameter, DijkstraResult>
        where P : DijkstraParameter 
        where R : DijkstraResult
    {
        private string isVisitedAttributeName = "isVisited:" + AlgorithmNames.DijkstraRouteAlgorithmName;
        private string isCurrentAttributeName = "isCurrent:" + AlgorithmNames.DijkstraRouteAlgorithmName;
        private string distanceAttributeName = "distance:" + AlgorithmNames.DijkstraRouteAlgorithmName;

        public override IEnumerable<string> ReservedDynamicAttributeNames
        {
            get
            {
                yield return this.isVisitedAttributeName;
                yield return this.isCurrentAttributeName;
                yield return this.distanceAttributeName;
            }
        }

        public override Type ParameterType
        {
            get
            {
                return typeof(P);
            }
        }

        public override Type ResultType
        {
            get
            {
                return typeof(R);
            }
        }

        public override DijkstraResult Execute(GraphStructure graphStructure, DijkstraParameter parameter)
        {
            this.ClearAllSetDynamicAttributes(graphStructure);

            DijkstraInformation dijkstraInfos = new DijkstraInformation();

            this.SetDistanceFromStartForVertex(dijkstraInfos, null, parameter.StartVertex, 0);

            Vertex vertexWithSmallestDistanceFromStart = parameter.StartVertex;
            while (!this.ShouldExitAlgorithm(vertexWithSmallestDistanceFromStart, parameter as P))
            {
                this.ProcessCurrentVertex(dijkstraInfos, vertexWithSmallestDistanceFromStart);
                vertexWithSmallestDistanceFromStart = dijkstraInfos.GetNotVisistedVertexWithSmallestDistanceFromStart();
            }

            return this.ConstructResult(dijkstraInfos, parameter as P);
        }

        internal abstract R ConstructResult(DijkstraInformation dijkstraInfos, P parameter);

        protected abstract bool ShouldExitAlgorithm(Vertex currentVertex, P parameter);

        private void ProcessCurrentVertex(DijkstraInformation dijkstraInfos, Vertex currentVertex)
        {
            this.SetAttributeIfAllowed(this.isCurrentAttributeName, true, currentVertex);

            this.ConsiderAllSuccessors(dijkstraInfos, currentVertex);

            this.MarkCurrentVertexAsVisited(dijkstraInfos, currentVertex);

            this.ClearAttribute(this.isCurrentAttributeName, currentVertex);
        }

        private void ConsiderAllSuccessors(DijkstraInformation dijkstraInfos, Vertex currentVertex)
        {
            double currentVertexDistanceFromStart = dijkstraInfos.GetVertexDistanceFromStart(currentVertex);

            foreach (Vertex successor in currentVertex.GetSuccessors())
            {
                if (dijkstraInfos.IsVisisted(successor))
                {
                    continue;
                }

                double distanceFromCurrentToSuccessor = this.GetShortestDistanceBetweenNeighbors(currentVertex, successor);
                double distanceFromStartToSuccessor = dijkstraInfos.GetVertexDistanceFromStart(successor);

                double newDistanceFromStartToSuccessor = currentVertexDistanceFromStart + distanceFromCurrentToSuccessor;

                if (newDistanceFromStartToSuccessor < distanceFromStartToSuccessor)
                {
                    this.SetDistanceFromStartForVertex(dijkstraInfos, currentVertex, successor, newDistanceFromStartToSuccessor);
                }

                if (distanceFromCurrentToSuccessor < 0)
                {
                    dijkstraInfos.NegativeEdgeFound = true;
                }
            }
        }

        private void SetDistanceFromStartForVertex(DijkstraInformation dijkstraInfos, Vertex previousVertex, Vertex vertex, double newDistanceFromStartToSuccessor)
        {
            dijkstraInfos.SetDistanceFromStartForVertex(vertex, newDistanceFromStartToSuccessor, previousVertex);
            this.SetAttributeIfAllowed(this.distanceAttributeName, newDistanceFromStartToSuccessor, vertex);
        }

        private void MarkCurrentVertexAsVisited(DijkstraInformation dijkstraInfos, Vertex currentVertex)
        {
            dijkstraInfos.MarkAsVisisted(currentVertex);
            this.SetAttributeIfAllowed(this.isVisitedAttributeName, true, currentVertex);
        }

        private double GetShortestDistanceBetweenNeighbors(Vertex firstVertex, Vertex secondVertex)
        {
            IEnumerable<Edge> edges = firstVertex.GetEdgesTo(secondVertex);
            double shortestDistance = double.PositiveInfinity;

            foreach (Edge edge in edges)
            {
                if (shortestDistance > edge.Weight)
                {
                    shortestDistance = edge.Weight;
                }
            }

            return shortestDistance;
        }
    }
}

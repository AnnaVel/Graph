using GraphCore.Utilities;
using GraphCore.VertexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public abstract class Vertex : GraphStructureItem
    {
        public IEnumerable<Vertex> GetSuccessors()
        {
            if (this.Owner == null)
            {
                throw new InvalidOperationException("The vertex is not registered in a graph structure.");
            }

            return this.Owner.GetVertexSuccessors(this);
        }

        public IEnumerable<double?> GetArrowWeights(Vertex successor)
        {
            Guard.ThrowExceptionIfNull(successor, "successor");

            if (this.Owner == null)
            {
                throw new InvalidOperationException("The vertex is not registered in a graph structure.");
            }

            IEnumerable<double?> arrowWeights = this.Owner.GetArrowWeights(this, successor);
            return arrowWeights;
        }

        public double? GetMinArrowWeight(Vertex successor)
        {
            Guard.ThrowExceptionIfNull(successor, "successor");

            IEnumerable<double?> allArrowWeightsToSuccessor = this.GetArrowWeights(successor);

            if (allArrowWeightsToSuccessor.Count() == 0)
            {
                throw new ArgumentException("The vertex passed is not a successor of this vertex.");
            }

            double? minimalWeight = allArrowWeightsToSuccessor.Min();
            return minimalWeight;
        }
    }
}

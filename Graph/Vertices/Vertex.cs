using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public abstract class Vertex
    {
        private VertexStructure owner;

        public abstract object ValueAsObject { get; }

        public IEnumerable<Vertex> GetSuccessors()
        {
            if (this.owner == null)
            {
                throw new InvalidOperationException("The vertex is not registered in a graph structure.");
            }

            return this.owner.GetVertexSuccessors(this);
        }

        public IEnumerable<double?> GetArrowWeights(Vertex successor)
        {
            return this.owner.GetArrowWeights(this, successor);
        }

        public double? GetMinArrowWeight(Vertex successor)
        {
            return this.GetArrowWeights(successor).Min();
        }

        internal void RegisterVertexToAStructure(VertexStructure vertexStructure)
        {
            Guard.ThrowExceptionIfNull(vertexStructure, "vertexStructure");

            if (this.owner != null)
            {
                throw new InvalidOperationException("This vertex is already part of a structure.");
            }

            this.owner = vertexStructure;
        }

        internal void UnregisterVertexFromAnyStructure()
        {
            this.owner = null;
        }
    }
}

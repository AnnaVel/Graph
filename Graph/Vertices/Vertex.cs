using GraphCore.Utilities;
using GraphCore.VertexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public abstract class Vertex
    {
        private readonly VertexPropertyList propertyList;

        private VertexStructure owner;

        public abstract object ValueAsObject { get; }

        public Vertex()
        {
            this.propertyList = new VertexPropertyList();
        }

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
            Guard.ThrowExceptionIfNull(successor, "successor");

            if (this.owner == null)
            {
                throw new InvalidOperationException("The vertex is not registered in a graph structure.");
            }

            IEnumerable<double?> arrowWeights = this.owner.GetArrowWeights(this, successor);
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

        public IVertexProperty GetProperty(string name)
        {
            return this.propertyList.GetProperty(name);
        }

        public void SetProperty(string name, object value)
        {
            this.propertyList.SetProperty(name, value);
        }

        public bool RemoveProperty(string name)
        {
            return this.propertyList.RemoveProperty(name);
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

using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.StructureDescription
{
    internal class AdjacencyDescriptor
    {
        private readonly string vertexName;
        private readonly string successorVertexName;
        private readonly double? weight;

        public string VertexName
        {
            get
            {
                return this.vertexName;
            }
        }

        public string SuccessorVertexName
        {
            get
            {
                return this.successorVertexName;
            }
        }

        public double? Weight
        {
            get
            {
                return this.weight;
            }
        }

        public AdjacencyDescriptor(string vertexName, string successorVertexName, double? weight)
        {
            Guard.ThrowExceptionIfNullOrEmpty(vertexName, "vertexName");

            this.vertexName = vertexName;
            this.successorVertexName = successorVertexName;
            this.weight = weight;
        }

        public AdjacencyDescriptor(string vertexName, string successorVertexName)
            : this(vertexName, successorVertexName, null)
        {
        }

        public AdjacencyDescriptor(string vertexName)
            : this(vertexName, null, null)
        {
        }
    }
}

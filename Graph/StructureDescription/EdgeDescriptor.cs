using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.StructureDescription
{
    public class EdgeDescriptor : StructureDescriptor
    {
        private readonly string firstVertexName;
        private readonly string secondVertexName;
        private readonly double? weight;

        public string FirstVertexName
        {
            get
            {
                return this.firstVertexName;
            }
        }

        public string SecondVertexName
        {
            get
            {
                return this.secondVertexName;
            }
        }

        public double? Weight
        {
            get
            {
                return this.weight;
            }
        }

        public EdgeDescriptor(string firstVertexName, string secondVertexName, double? weight)
        {
            Guard.ThrowExceptionIfNullOrEmpty(firstVertexName, "firstVertexName");
            Guard.ThrowExceptionIfNullOrEmpty(secondVertexName, "secondVertexName");

            this.firstVertexName = firstVertexName;
            this.secondVertexName = secondVertexName;
            this.weight = weight;
        }

        public EdgeDescriptor(string firstVertexName, string secondVertexName)
            :this(firstVertexName, secondVertexName, null)
        {
        }

        internal override IEnumerable<AdjacencyDescriptor> TranslateToAdjacencyDescriptors()
        {
            return new List<AdjacencyDescriptor>()
            {
                new AdjacencyDescriptor(this.firstVertexName, this.secondVertexName, this.weight),
                new AdjacencyDescriptor(this.secondVertexName, this.firstVertexName, this.weight)
            };
        }
    }
}

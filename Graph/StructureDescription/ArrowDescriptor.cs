using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.StructureDescription
{
    public class ArrowDescriptor : StructureDescriptor
    {
        private readonly string predecessorName;
        private readonly string successorName;
        private readonly double? weight;

        public string PredecessorName
        {
            get
            {
                return this.predecessorName;
            }
        }

        public string SuccessorName
        {
            get
            {
                return this.successorName;
            }
        }

        public double? Weight
        {
            get
            {
                return this.weight;
            }
        }

        public ArrowDescriptor(string predecessorName, string successorName, double? weight)
        {
            Guard.ThrowExceptionIfNullOrEmpty(predecessorName, "predecessorName");
            Guard.ThrowExceptionIfNullOrEmpty(successorName, "successorName");

            this.predecessorName = predecessorName;
            this.successorName = successorName;
            this.weight = weight;
        }

        public ArrowDescriptor(string predecessorName, string successorName)
            :this(predecessorName, successorName, null)
        {
        }

        internal override IEnumerable<AdjacencyDescriptor> TranslateToAdjacencyDescriptors()
        {
            return new List<AdjacencyDescriptor>()
            {
                new AdjacencyDescriptor(this.predecessorName, this.successorName, this.weight)
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class ArrowDescriptor
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
            this.predecessorName = predecessorName;
            this.successorName = successorName;
            this.weight = weight;
        }

        public ArrowDescriptor(string predecessorName, string successorName)
            :this(predecessorName, successorName, null)
        {
        }
    }
}

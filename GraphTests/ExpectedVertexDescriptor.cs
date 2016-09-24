using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class ExpectedVertexDescriptor
    {
        private Dictionary<object, double?> successorNameToWeight;

        public object Value { get; set; }

        public Dictionary<object, double?> SuccessorNameToWeight
        {
            get 
            {
                return this.successorNameToWeight;
            }
        }

        public ExpectedVertexDescriptor(object value, Dictionary<object, double?> successorNameToWeight)
        {
            this.Value = value;
            this.successorNameToWeight = new Dictionary<object, double?>(successorNameToWeight);
        }
    }
}

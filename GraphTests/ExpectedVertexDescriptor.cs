using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class ExpectedVertexDescriptor
    {
        private Dictionary<string, double?> successorNameToWeight;

        public string Name { get; set; }

        public Dictionary<string, double?> SuccessorNameToWeight
        {
            get 
            {
                return this.successorNameToWeight;
            }
        }

        public ExpectedVertexDescriptor(string name, Dictionary<string, double?> successorNameToWeight)
        {
            this.Name = name;
            this.successorNameToWeight = new Dictionary<string, double?>(successorNameToWeight);
        }
    }
}

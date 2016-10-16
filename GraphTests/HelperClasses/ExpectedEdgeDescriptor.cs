using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    internal class ExpectedEdgeDescriptor
    {
        public object FirstVertexValue { get; set; }
        public object SecondVertexValue { get; set; }
        public bool IsDirected { get; set; }
        public object Value { get; set; }
    }
}

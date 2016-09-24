using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public class DoubleValueVertex : VertexBase<double>
    {
        public DoubleValueVertex(double value)
            :base(value)
        {
        }
    }
}

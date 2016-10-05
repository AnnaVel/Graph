using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class DoubleGraphItemProperty : VertexPropertyBase<double>
    {
        public DoubleGraphItemProperty(string name, double value)
            :base(name, value)
        {

        }
    }
}

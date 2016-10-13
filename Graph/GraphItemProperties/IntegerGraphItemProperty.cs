using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class IntegerGraphItemProperty : VertexPropertyBase<int>
    {
        public IntegerGraphItemProperty(string name, int value)
            :base(name, value)
        {

        }
    }
}

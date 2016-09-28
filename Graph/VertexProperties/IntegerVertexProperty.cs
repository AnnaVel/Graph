using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class IntegerVertexProperty : VertexPropertyBase<int>
    {
        public IntegerVertexProperty(string name, int value)
            :base(name, value)
        {

        }
    }
}

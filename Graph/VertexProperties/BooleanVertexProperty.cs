using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class BooleanVertexProperty : VertexPropertyBase<bool>
    {
        public BooleanVertexProperty(string name, bool value)
            :base(name, value)
        {

        }
    }
}

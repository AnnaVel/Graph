using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public class ObjectValueVertex : VertexBase<object>
    {
        public ObjectValueVertex(object value)
            :base(value)
        {
        }
    }
}

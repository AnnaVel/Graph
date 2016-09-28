using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class ObjectVertexProperty : VertexPropertyBase<object>
    {
        public ObjectVertexProperty(string name, object value)
            :base(name, value)
        {

        }
    }
}

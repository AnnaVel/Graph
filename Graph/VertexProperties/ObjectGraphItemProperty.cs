using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class ObjectGraphItemProperty : VertexPropertyBase<object>
    {
        public ObjectGraphItemProperty(string name, object value)
            :base(name, value)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class BooleanGraphItemProperty : VertexPropertyBase<bool>
    {
        public BooleanGraphItemProperty(string name, bool value)
            :base(name, value)
        {

        }
    }
}

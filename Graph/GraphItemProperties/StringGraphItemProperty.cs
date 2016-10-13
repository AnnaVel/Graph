using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class StringGraphItemProperty : VertexPropertyBase<string>
    {
        public StringGraphItemProperty(string name, string value)
            :base(name, value)
        {

        }
    }
}

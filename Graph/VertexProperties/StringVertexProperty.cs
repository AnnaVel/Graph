using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class StringVertexProperty : VertexPropertyBase<string>
    {
        public StringVertexProperty(string name, string value)
            :base(name, value)
        {

        }
    }
}

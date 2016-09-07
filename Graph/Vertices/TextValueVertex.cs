using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public class TextValueVertex : VertexBase<string>
    {
        public TextValueVertex(string value)
            :base(value)
        {
        }
    }
}

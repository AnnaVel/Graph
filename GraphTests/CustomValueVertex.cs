using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomValueVertex : Vertex
    {
        public override object ValueAsObject
        {
            get { return this.GetHashCode(); }
        }
    }
}

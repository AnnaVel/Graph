using GraphCore.Edges;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomValueEdge : EdgeBase<string>
    {
        public CustomValueEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected, string value)
            :base(firstVertex, secondVertex, isDirected, value)
        {

        }
    }
}

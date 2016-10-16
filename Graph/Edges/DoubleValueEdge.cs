using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public class DoubleValueEdge : EdgeBase<double?>
    {
        public DoubleValueEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected, double? value)
            :base(firstVertex, secondVertex, isDirected, value)
        {
        }
    }
}

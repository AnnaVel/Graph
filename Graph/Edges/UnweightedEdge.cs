using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public class UnweightedEdge : EdgeBase<object>
    {
        public UnweightedEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected)
            :base(firstVertex, secondVertex, isDirected, null)
        {
        }
    }
}

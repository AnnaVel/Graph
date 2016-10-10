using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public class ObjectValueEdge : EdgeBase<object>
    {
        public ObjectValueEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected, object value)
            :base(firstVertex, secondVertex, isDirected, value)
        {
        }
    }
}

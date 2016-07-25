using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Graph
    {
        private IEnumerable<Vertex> vertices;

        public Graph(params ArrowDescriptor[] structureDescription)
        {
            this.vertices = Vertex.CreateVerticesStructure(structureDescription);
        }
    }
}

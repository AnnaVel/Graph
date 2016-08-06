using GraphCore.StructureDescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    public class Graph
    {
        private readonly IEnumerable<Vertex> vertices;

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.vertices;
            }
        }

        public Graph(params StructureDescriptor[] structureDescription)
        {
            this.vertices = Vertex.CreateVerticesStructure(structureDescription);
        }
    }
}

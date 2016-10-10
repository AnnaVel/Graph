using GraphCore.Utilities;
using GraphCore.VertexProperties;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    public class Graph
    {
        private GraphStructure graphStructure;

        public GraphStructure GraphStructure
        {
            get
            {
                return this.graphStructure;
            }
        }

        public Graph()
        {
            this.graphStructure = new GraphStructure();
        }
    }
}

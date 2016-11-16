using GraphCore.Utilities;
using GraphCore.GraphItemProperties;
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
        private readonly GraphStructure graphStructure;

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

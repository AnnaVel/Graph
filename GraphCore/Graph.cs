using GraphCore.Utilities;
using GraphCore.DynamicAttributes;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GraphCore.Algorithms;

namespace GraphCore
{
    public class Graph
    {
        private readonly GraphStructure graphStructure;
        private readonly AlgorithmLibrary algorithmLibrary;

        public GraphStructure GraphStructure
        {
            get
            {
                return this.graphStructure;
            }
        }

        public AlgorithmLibrary AlgorithmLibrary
        {
            get
            {
                return this.algorithmLibrary;
            }
        }

        public Graph()
        {
            this.graphStructure = new GraphStructure();
            this.algorithmLibrary = new AlgorithmLibrary(this.graphStructure);
        }
    }
}

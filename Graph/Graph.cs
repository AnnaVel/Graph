using GraphCore.Utilities;
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
        private VertexStructure vertexStructure;

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                return this.vertexStructure.Vertices;
            }
        }

        public VertexFactory VertexFactory
        {
            get
            {
                return this.vertexStructure.VertexFactory;
            }
            set
            {
                this.vertexStructure.VertexFactory = value;
            }
        }

        public Graph()
        {
            this.vertexStructure = new VertexStructure();
        }

        public Vertex AddVertex(object value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            return this.vertexStructure.AddVertex(value);
        }

        public bool RemoveVertex(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            return this.vertexStructure.RemoveVertex(vertex);
        }

        public void AddLine(Vertex firstVertex, Vertex secondVertex)
        {
            this.AddLine(firstVertex, secondVertex, null);
        }

        public void AddLine(Vertex firstVertex, Vertex secondVertex, double? weight)
        {
            this.AddArrow(firstVertex, secondVertex, weight);
            this.AddArrow(secondVertex, firstVertex, weight);
        }

        public void AddArrow(Vertex firstVertex, Vertex secondVertex)
        {
            this.AddArrow(firstVertex, secondVertex, null);
        }

        public void AddArrow(Vertex firstVertex, Vertex secondVertex, double? weight)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            this.vertexStructure.AddArrow(firstVertex, secondVertex);
        }

        public bool RemoveAllEdges(Vertex firstVertex, Vertex secondVertex)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            bool firstToSecondArrowsRemoved = this.vertexStructure.RemoveArrows(firstVertex, secondVertex);
            bool secondToFirstArrowsRemoved = this.vertexStructure.RemoveArrows(secondVertex, firstVertex);

            return firstToSecondArrowsRemoved || secondToFirstArrowsRemoved;
        }
    }
}

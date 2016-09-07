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

        public Graph()
        {
            this.vertexStructure = new VertexStructure();
        }

        public void AddVertex(object value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            throw new NotImplementedException();
        }

        public bool RemoveVertex(Vertex vertex)
        {
            Guard.ThrowExceptionIfNull(vertex, "vertex");

            throw new NotImplementedException();
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

            throw new NotImplementedException();
        }

        public bool RemoveAllEdges(Vertex firstVertex, Vertex secondVertex)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            throw new NotImplementedException();
        }
    }
}

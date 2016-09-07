using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    internal class VertexStructure
    {
        private readonly Dictionary<object, Vertex> valueToVertexIndex;
        private VertexFactory vertexFactory;

        public VertexFactory VertexFactory
        {
            get
            {
                return this.vertexFactory;
            }
            set
            {
                vertexFactory = value;
            }
        }

        public VertexStructure()
        {
            this.valueToVertexIndex = new Dictionary<object, Vertex>();
            this.vertexFactory = new VertexFactory();
        }

        public void AddVertex(object value)
        {
            Vertex newVertex = this.vertexFactory.CreateVertex(value);
        }

        public bool RemoveVertex(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        public bool RemoveEdge(Vertex firstVertex, Vertex secondVertex)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Vertex> GetVertexSuccessors(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        private void RegisterVertex(Vertex vertex)
        {
            throw new NotImplementedException();
        }

        private void UnregisterVertex(Vertex vertex)
        {
            throw new NotImplementedException();
        }
    }
}

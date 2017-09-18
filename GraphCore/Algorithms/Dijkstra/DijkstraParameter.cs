using GraphCore.Utilities;
using GraphCore.Vertices;

namespace GraphCore.Algorithms
{
    public class DijkstraParameter
    {
        private Vertex startVertex;

        public Vertex StartVertex
        {
            get
            {
                return this.startVertex;
            }
        }

        public DijkstraParameter(Vertex startVertex)
        {
            Guard.ThrowExceptionIfNull(startVertex, "startVertex");

            this.startVertex = startVertex;
        }
    }
}

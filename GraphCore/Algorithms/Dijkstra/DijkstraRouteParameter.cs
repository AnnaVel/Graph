using GraphCore.Vertices;
using GraphCore.Utilities;

namespace GraphCore.Algorithms
{
    public class DijkstraRouteParameter : DijkstraParameter
    {
        private Vertex endVertex;

        public Vertex EndVertex
        {
            get
            {
                return this.endVertex;
            }
        }

        public DijkstraRouteParameter(Vertex startVertex, Vertex endVertex) : base(startVertex)
        {
            Guard.ThrowExceptionIfNull(endVertex, "endVertex");

            this.endVertex = endVertex;
        }
    }
}

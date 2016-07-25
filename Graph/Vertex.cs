using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph
{
    public class Vertex
    {
        private readonly string name;
        private readonly Dictionary<Vertex, double?> successorToArrowWeight;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        private Vertex(string name)
        {
            this.name = name;
            this.successorToArrowWeight = new Dictionary<Vertex, double?>();
        }

        public static IEnumerable<Vertex> CreateVerticesStructure(IEnumerable<ArrowDescriptor> structureDescription)
        {
            Dictionary<string, Vertex> allNamesToVertices = new Dictionary<string, Vertex>();

            foreach(var arrow in structureDescription)
            {
                Vertex predecessor = RetrieveVertexOrCreateNew(ref allNamesToVertices, arrow.PredecessorName);
                Vertex successor = RetrieveVertexOrCreateNew(ref allNamesToVertices, arrow.SuccessorName);

                predecessor.successorToArrowWeight.Add(successor, arrow.Weight);
            }

            return allNamesToVertices.Values;
        }

        private static Vertex RetrieveVertexOrCreateNew(ref Dictionary<string, Vertex> allNamesToVertices, string name)
        {
            Vertex resultVertex;

            if (!allNamesToVertices.ContainsKey(name))
            {
                resultVertex = new Vertex(name);
            }
            else
            {
                resultVertex = allNamesToVertices[name];
            }

            return resultVertex;
        }

        public IEnumerable<Vertex> GetSuccessors()
        {
            //TODO: check if this is not too slow.
            foreach (var key in this.successorToArrowWeight.Keys)
            {
                yield return key;
            }
        }

        public double? GetArrowWeight(Vertex successor)
        {
            if (!this.successorToArrowWeight.ContainsKey(successor))
            {
                throw new ArgumentException(string.Format("The vertex {0} is not a successor of {1}.", successor, this));
            }

            return this.successorToArrowWeight[successor];
        }

        public override string ToString()
        {
            return this.name;
        }
    }
}

using GraphCore.StructureDescription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
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

        public IEnumerable<Vertex> GetSuccessors()
        {
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

        internal static IEnumerable<Vertex> CreateVerticesStructure(IEnumerable<StructureDescriptor> structureDescription)
        {
            Dictionary<string, Vertex> allNamesToVertices = new Dictionary<string, Vertex>();

            foreach (var structureDescriptor in structureDescription)
            {
                IEnumerable<AdjacencyDescriptor> adjacencyDescriptors = structureDescriptor.TranslateToAdjacencyDescriptors();

                foreach (AdjacencyDescriptor adjacencyDescriptor in adjacencyDescriptors)
                {
                    Vertex vertex = RetrieveVertexOrCreateNew(ref allNamesToVertices, adjacencyDescriptor.VertexName);

                    if (adjacencyDescriptor.SuccessorVertexName != null)
                    {
                        Vertex vertexSuccessor = RetrieveVertexOrCreateNew(ref allNamesToVertices, adjacencyDescriptor.SuccessorVertexName);
                        vertex.successorToArrowWeight.Add(vertexSuccessor, adjacencyDescriptor.Weight);
                    }
                }
            }

            return allNamesToVertices.Values;
        }

        private static Vertex RetrieveVertexOrCreateNew(ref Dictionary<string, Vertex> allNamesToVertices, string name)
        {
            Vertex resultVertex;

            if (!allNamesToVertices.ContainsKey(name))
            {
                resultVertex = new Vertex(name);
                allNamesToVertices.Add(name, resultVertex);
            }
            else
            {
                resultVertex = allNamesToVertices[name];
            }

            return resultVertex;
        }
    }
}

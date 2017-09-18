using GraphCore.Algorithms;
using GraphCore.Vertices;
using System.Collections.Generic;
using System.Linq;
using GraphCore;

namespace GraphTests.AlgorithmTests
{
    public class TestAlgorithm : AlgorithmBase<Vertex, Vertex>
    {
        private string reservedAttributeName = "visited:TestAlgorithm";

        public override string Name
        {
            get
            {
                return "Test Algorithm";
            }
        }

        public override IEnumerable<string> ReservedDynamicAttributeNames
        {
            get
            {
                yield return this.reservedAttributeName;
            }
        }

        public override Vertex Execute(GraphStructure graphStructure, Vertex parameter)
        {
            Vertex result = parameter.GetSuccessors().FirstOrDefault();

            if (this.SetDynamicAttributesInStructure)
            {
                parameter.SetDynamicAttribute(this.reservedAttributeName, true);

                if (result != null)
                {
                    result.SetDynamicAttribute(this.reservedAttributeName, true);
                }
            }

            return result;
        }
    }
}

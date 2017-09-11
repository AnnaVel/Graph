using GraphCore.Algorithms;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public class FindNextVertex : AlgorithmBase<Vertex, Vertex>
    {
        public override string Name
        {
            get
            {
                return AlgorithmNames.FindNextVertexName;
            }
        }

        public override IEnumerable<string> ReservedPropertyNames
        {
            get
            {
                yield return "color:FindNext";
            }
        }

        public override Vertex Execute(GraphStructure graphStructure, Vertex parameter)
        {
            Guard.ThrowExceptionIfNull(parameter, "parameter");

            parameter.SetProperty("color:FindNext", "Purple");

            Vertex next = parameter.GetSuccessors().FirstOrDefault();

            next.SetProperty("color:FindNext", "Purple");

            return next;
        }
    }
}

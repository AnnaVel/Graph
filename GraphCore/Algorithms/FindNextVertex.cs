using GraphCore.Algorithms;
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

        //public Type ParameterType
        //{
        //    get
        //    {
        //        return typeof(Vertex);
        //    }
        //}

        //public Type ReturnType
        //{
        //    get
        //    {
        //        return typeof(Vertex);
        //    }
        //}

        public override Vertex Execute(GraphStructure graphStructure, Vertex parameter)
        {
            return parameter.GetSuccessors().FirstOrDefault();
        }
    }
}

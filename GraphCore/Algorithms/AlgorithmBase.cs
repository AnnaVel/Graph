using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public abstract class AlgorithmBase<R, P> : IAlgorithm
    {
        public abstract string Name{ get; }

        public Type ParameterType
        {
            get
            {
                return typeof(P);
            }
        }

        public Type ReturnType
        {
            get
            {
                return typeof(R);
            }
        }

        public object ExecuteBase(GraphStructure graphStructure, object parameter)
        {
            return this.Execute(graphStructure, (P)parameter);
        }

        public abstract R Execute(GraphStructure graphStructure, P parameter);
    }
}

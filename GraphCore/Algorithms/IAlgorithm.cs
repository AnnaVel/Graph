using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public interface IAlgorithm
    {
        string Name { get; }

        Type ParameterType { get; }

        Type ReturnType { get; }

        object ExecuteBase(GraphStructure graphStructure, object parameter);
    }
}

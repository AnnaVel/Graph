using System;
using System.Collections.Generic;

namespace GraphCore.Algorithms
{
    public interface IAlgorithm
    {
        string Name { get;  }

        IEnumerable<string> ReservedDynamicAttributeNames { get; }

        bool SetDynamicAttributesInStructure { get; set; }

        Type ResultType { get; }

        Type ParameterType { get; }

        object ExecuteBase(GraphStructure graphStructure, object parameter);

        void ClearAllSetDynamicAttributes(GraphStructure graphStructure);
    }
}

using GraphCore.Edges;
using GraphCore.Utilities;
using GraphCore.Vertices;
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

        public abstract IEnumerable<string> ReservedPropertyNames { get; }

        public object ExecuteBase(GraphStructure graphStructure, object parameter)
        {
            Guard.ThrowExceptionIfNotOfType(parameter, "parameter", this.ParameterType);

            return this.Execute(graphStructure, (P)parameter);
        }

        public abstract R Execute(GraphStructure graphStructure, P parameter);

        public void ClearPropertiesSetByAlgorithm(GraphStructure graphStructure)
        {
            foreach(string reservedProperty in this.ReservedPropertyNames)
            {
                foreach(Vertex vertex in graphStructure.Vertices)
                {
                    vertex.RemoveProperty(reservedProperty);
                }

                foreach (Edge edge in graphStructure.Edges)
                {
                    edge.RemoveProperty(reservedProperty);
                }
            }
        }
    }
}

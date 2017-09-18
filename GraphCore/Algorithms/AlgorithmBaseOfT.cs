using GraphCore.Edges;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;

namespace GraphCore.Algorithms
{
    public abstract class AlgorithmBase<P, R> : IAlgorithm
    {
        private bool setDynamicAttributesInStructure = false;

        public abstract string Name { get; }

        public virtual Type ParameterType
        {
            get
            {
                return typeof(P);
            }
        }

        public virtual Type ResultType
        {
            get
            {
                return typeof(R);
            }
        }

        public abstract IEnumerable<string> ReservedDynamicAttributeNames { get; }

        public bool SetDynamicAttributesInStructure
        {
            get
            {
                return this.setDynamicAttributesInStructure;
            }
            set
            {
                this.setDynamicAttributesInStructure = value;
            }
        }

        public object ExecuteBase(GraphStructure graphStructure, object parameter)
        {
            Guard.ThrowExceptionIfNotOfType(parameter, "parameter", this.ParameterType);
            Guard.ThrowExceptionIfNull(graphStructure, "graphStructure");
            Guard.ThrowExceptionIfNull(parameter, "parameter");

            return this.Execute(graphStructure, (P)parameter);
        }

        public abstract R Execute(GraphStructure graphStructure, P parameter);

        public void ClearAllSetDynamicAttributes(GraphStructure graphStructure)
        {
            foreach (string reservedName in this.ReservedDynamicAttributeNames)
            {
                foreach (Vertex vertex in graphStructure.Vertices)
                {
                    vertex.RemoveDynamicAttribute(reservedName);
                }

                foreach (Edge edge in graphStructure.Edges)
                {
                    edge.RemoveDynamicAttribute(reservedName);
                }
            }
        }

        protected void SetAttributeIfAllowed(string attributeName, object attributeValue, GraphStructureItem item)
        {
            if(this.SetDynamicAttributesInStructure)
            {
                item.SetDynamicAttribute(attributeName, attributeValue);
            }
        }

        protected void ClearAttribute(string attributeName, GraphStructureItem item)
        {
            item.RemoveDynamicAttribute(attributeName);
        }
    }
}

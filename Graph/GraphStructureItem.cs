using GraphCore.Utilities;
using GraphCore.GraphItemProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    public abstract class GraphStructureItem
    {
        private GraphStructure owner;

        internal GraphStructure Owner
        {
            get
            {
                return this.owner;
            }
        }

        internal abstract GraphItemPropertyList PropertyList { get; }

        public abstract object ValueAsObject
        {
            get;
        }

        public IGraphItemProperty GetProperty(string name)
        {
            return this.PropertyList.GetProperty(name);
        }

        public void SetProperty(string name, object value)
        {
            this.PropertyList.SetProperty(name, value);
        }

        public bool RemoveProperty(string name)
        {
            return this.PropertyList.RemoveProperty(name);
        }

        internal void RegisterItemToAStructure(GraphStructure graphStructure)
        {
            Guard.ThrowExceptionIfNull(graphStructure, "graphStructure");

            if (this.owner != null)
            {
                throw new InvalidOperationException("This vertex is already part of a structure.");
            }

            this.owner = graphStructure;
        }

        internal void UnregisterItemFromAnyStructure()
        {
            this.owner = null;
        }
    }
}

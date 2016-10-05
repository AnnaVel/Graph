using GraphCore.Utilities;
using GraphCore.VertexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore
{
    public abstract class GraphStructureItem
    {
        private readonly GraphItemPropertyList propertyList;

        private GraphStructure owner;

        internal GraphStructure Owner
        {
            get
            {
                return this.owner;
            }
        }

        public abstract object ValueAsObject
        {
            get;
        }

        public GraphStructureItem()
        {
            this.propertyList = new GraphItemPropertyList(this);
        }

        public IGraphItemProperty GetProperty(string name)
        {
            return this.propertyList.GetProperty(name);
        }

        public void SetProperty(string name, object value)
        {
            this.propertyList.SetProperty(name, value);
        }

        public bool RemoveProperty(string name)
        {
            return this.propertyList.RemoveProperty(name);
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

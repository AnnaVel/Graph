using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    internal class GraphItemPropertyList
    {
        private readonly GraphStructureItem owner;

        private readonly Dictionary<string, IGraphItemProperty> innerList;

        public GraphItemPropertyList(GraphStructureItem owner)
        {
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.owner = owner;

            this.innerList = new Dictionary<string, IGraphItemProperty>();
        }

        public void SetProperty(string name, object value)
        {
            GraphStructure owningStructure = this.owner.Owner;

            if (owningStructure == null)
            {
                throw new InvalidOperationException("The item is not part of a structure and a property cannot be added.");
            }

            IGraphItemProperty newProperty = owningStructure.GraphItemPropertyFactory.CreateVertexProperty(name, value);

            this.innerList[name] = newProperty;
        }

        public IGraphItemProperty GetProperty(string name)
        {
            IGraphItemProperty result = null;
            this.innerList.TryGetValue(name, out result);

            return result;
        }

        public bool RemoveProperty(string name)
        {
            return this.innerList.Remove(name);
        }
    }
}

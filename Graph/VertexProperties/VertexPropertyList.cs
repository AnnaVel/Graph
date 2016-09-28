using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    internal class VertexPropertyList
    {
        private readonly Vertex owner;

        private readonly Dictionary<string, IVertexProperty> innerList;

        public VertexPropertyList(Vertex owner)
        {
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.owner = owner;

            this.innerList = new Dictionary<string, IVertexProperty>();
        }

        public void SetProperty(string name, object value)
        {
            VertexStructure owningStructure = this.owner.Owner;

            if (owningStructure == null)
            {
                throw new InvalidOperationException("The vertex is not part of a structure and a property cannot be added.");
            }

            IVertexProperty newProperty = this.owner.Owner.VertexPropertyFactory.CreateVertexProperty(name, value);

            this.innerList[name] = newProperty;
        }

        public IVertexProperty GetProperty(string name)
        {
            IVertexProperty result = null;
            this.innerList.TryGetValue(name, out result);

            return result;
        }

        public bool RemoveProperty(string name)
        {
            return this.innerList.Remove(name);
        }
    }
}

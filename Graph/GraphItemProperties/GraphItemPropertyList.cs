using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    internal abstract class GraphItemPropertyList
    {
        private readonly GraphStructureItem owner;
        private readonly Dictionary<string, IGraphItemProperty> innerList;

        protected GraphStructureItem Owner
        {
            get
            {
                return this.owner;
            }
        }

        public GraphItemPropertyList(GraphStructureItem owner)
        {
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.owner = owner;

            this.innerList = new Dictionary<string, IGraphItemProperty>();
        }

        protected abstract GraphItemPropertyFactory GetPropertyFactoryFromOwner();

        public void SetProperty(string name, object value)
        {
            GraphItemPropertyFactory factory = this.GetPropertyFactoryFromOwner();
            IGraphItemProperty newProperty = factory.CreateVertexProperty(name, value);

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

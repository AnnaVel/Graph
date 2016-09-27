using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    internal class VertexPropertyList
    {
        private readonly Dictionary<string, IVertexProperty> innerList;
        private readonly VertexPropertyFactory factory;

        public VertexPropertyList()
        {
            this.innerList = new Dictionary<string, IVertexProperty>();
            this.factory = new VertexPropertyFactory();
        }

        public void SetProperty(string name, object value)
        {
            IVertexProperty newProperty = this.factory.CreateVertexProperty(name, value);

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

using GraphCore.Events;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public delegate void GraphItemPropertyChangedEventHandler(GraphItemPropertyChangedEventArgs args);

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

            this.OnGraphItemPropertyChanged(name, PropertyChangeAction.Set);
        }

        public IGraphItemProperty GetProperty(string name)
        {
            IGraphItemProperty result = null;
            this.innerList.TryGetValue(name, out result);

            return result;
        }

        public bool RemoveProperty(string name)
        {
            bool result = this.innerList.Remove(name);

            if(result)
            {
                this.OnGraphItemPropertyChanged(name, PropertyChangeAction.Remove);
            }

            return result;
        }

        public IEnumerable<string> EnumeratePropertyNames()
        {
            foreach(var pair in this.innerList)
            {
                yield return pair.Key;
            }
        }

        public event GraphItemPropertyChangedEventHandler GraphItemPropertyChanged;

        private void OnGraphItemPropertyChanged(string propertyName, PropertyChangeAction changeAction)
        {
            if(this.GraphItemPropertyChanged != null)
            {
                this.GraphItemPropertyChanged(new GraphItemPropertyChangedEventArgs(propertyName, changeAction));
            }
        }
    }
}

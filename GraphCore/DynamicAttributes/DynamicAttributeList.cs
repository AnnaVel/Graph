using GraphCore.Events;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    public delegate void DynamicAttributeChangedEventHandler(DynamicAttributeChangedEventArgs args);

    internal abstract class DynamicAttributeList
    {
        private readonly GraphStructureItem owner;
        private readonly Dictionary<string, IDynamicAttribute> innerList;

        protected GraphStructureItem Owner
        {
            get
            {
                return this.owner;
            }
        }

        public DynamicAttributeList(GraphStructureItem owner)
        {
            Guard.ThrowExceptionIfNull(owner, "owner");

            this.owner = owner;

            this.innerList = new Dictionary<string, IDynamicAttribute>();
        }

        protected abstract DynamicAttributeFactory GetDynamicAttributeFactoryFromOwner();

        public void SetDynamicAttribute(string name, object value)
        {
            DynamicAttributeFactory factory = this.GetDynamicAttributeFactoryFromOwner();
            IDynamicAttribute newDynamicAttribute = factory.CreateDynamicAttribute(name, value);

            this.innerList[name] = newDynamicAttribute;

            this.OnDynamicAttributeChanged(name);
        }

        public IDynamicAttribute GetDynamicAttribute(string name)
        {
            IDynamicAttribute result = null;
            this.innerList.TryGetValue(name, out result);

            return result;
        }

        public bool RemoveDynamicAttribute(string name)
        {
            bool result = this.innerList.Remove(name);

            if(result)
            {
                this.OnDynamicAttributeChanged(name);
            }

            return result;
        }

        public IEnumerable<string> EnumerateDynamicAttributeNames()
        {
            foreach(var pair in this.innerList)
            {
                yield return pair.Key;
            }
        }

        public event DynamicAttributeChangedEventHandler DynamicAttributeChanged;

        private void OnDynamicAttributeChanged(string dynamicAttributeName)
        {
            if(this.DynamicAttributeChanged != null)
            {
                this.DynamicAttributeChanged(new DynamicAttributeChangedEventArgs(dynamicAttributeName));
            }
        }
    }
}

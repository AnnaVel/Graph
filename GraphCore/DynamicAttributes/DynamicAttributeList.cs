using GraphCore.Events;
using GraphCore.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace GraphCore.DynamicAttributes
{
    public delegate void DynamicAttributeChangedEventHandler(DynamicAttributeChangedEventArgs args);

    internal abstract class DynamicAttributeList
    {
        private readonly GraphStructureItem owner;
        private readonly Dictionary<string, DynamicAttributeWithTimeStamp> innerList;

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

            this.innerList = new Dictionary<string, DynamicAttributeWithTimeStamp>();
        }

        protected abstract DynamicAttributeFactory GetDynamicAttributeFactoryFromOwner();

        public void SetDynamicAttribute(string name, object value)
        {
            DynamicAttributeFactory factory = this.GetDynamicAttributeFactoryFromOwner();
            IDynamicAttribute newDynamicAttribute = factory.CreateDynamicAttribute(name, value);
            DynamicAttributeWithTimeStamp stampedAttribute = new DynamicAttributeWithTimeStamp(newDynamicAttribute);

            this.innerList[name] = stampedAttribute;

            this.OnDynamicAttributeChanged(name);
        }

        public IDynamicAttribute GetDynamicAttribute(string name)
        {
            DynamicAttributeWithTimeStamp result = null;
            this.innerList.TryGetValue(name, out result);

            return result != null ? result.Attribute : null;
        }

        /// <summary>
        /// Gets the dynamic attribute that was set last according to its time stamp among a group of attributes sharing the same prefix. E.g.
        /// passing "isVisisted" will look among the "isVisisted", "isVisisted:Dijkstra", "isVisisted:custom", etc. and will return the one which
        /// was set last.
        /// </summary>
        /// <param name="name">The name of the prefix.</param>
        /// <returns>The dynamic attribute whose name starts with the specified prefix and was set the latest.</returns>
        public IDynamicAttribute GetDynamicAttributeThatWasLastSetInGroup(string name)
        {
            DynamicAttributeWithTimeStamp stampedResult = this.innerList
                .Where(pair => pair.Key.StartsWith(name))
                .Select(pair => pair.Value)
                .OrderByDescending(stampedAttribute => stampedAttribute.Stamp)
                .FirstOrDefault();

            return stampedResult != null ? stampedResult.Attribute : null;
        }

        public TimeStamp GetTimeStampt(string name)
        {
            return this.innerList[name].Stamp;
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

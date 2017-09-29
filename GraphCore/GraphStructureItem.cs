using GraphCore.Utilities;
using GraphCore.DynamicAttributes;
using System;
using System.Collections.Generic;

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

        internal abstract DynamicAttributeList DynamicAttributeList { get; }

        public abstract object ValueAsObject
        {
            get;
        }

        public IDynamicAttribute GetDynamicAttribute(string name)
        {
            return this.DynamicAttributeList.GetDynamicAttribute(name);
        }

        public IDynamicAttribute GetDynamicAttributeThatWasLastSetInGroup(string name)
        {
            return this.DynamicAttributeList.GetDynamicAttributeThatWasLastSetInGroup(name);
        }

        public TimeStamp GetTimeStamp(string name)
        {
            return this.DynamicAttributeList.GetTimeStampt(name);
        }

        public void SetDynamicAttribute(string name, object value)
        {
            this.DynamicAttributeList.SetDynamicAttribute(name, value);
        }

        public bool RemoveDynamicAttribute(string name)
        {
            return this.DynamicAttributeList.RemoveDynamicAttribute(name);
        }

        public IEnumerable<string> EnumerateDynamicAttributeNames()
        {
            return this.DynamicAttributeList.EnumerateDynamicAttributeNames();
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

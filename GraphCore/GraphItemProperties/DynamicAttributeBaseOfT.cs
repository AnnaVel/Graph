using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public abstract class DynamicAttributeBase<T> : IDynamicAttribute
    {
        private string name;
        private T value;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public T Value
        {
            get
            {
                return this.value;
            }
        }

        public Type ValueType
        {
            get
            {
                return this.Value.GetType();
            }
        }

        public object ValueAsObject
        {
            get 
            { 
                return this.Value as object; 
            }
        }

        public DynamicAttributeBase(string name, T value)
        {
            Guard.ThrowExceptionIfNullOrEmpty(name, "name");

            this.name = name;
            this.value = value;
        }
    }
}

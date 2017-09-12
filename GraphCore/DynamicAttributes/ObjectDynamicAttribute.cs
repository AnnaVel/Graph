using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    public class ObjectDynamicAttribute : DynamicAttributeBase<object>
    {
        public ObjectDynamicAttribute(string name, object value)
            :base(name, value)
        {

        }
    }
}

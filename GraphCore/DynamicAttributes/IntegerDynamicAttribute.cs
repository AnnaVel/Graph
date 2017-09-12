using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    public class IntegerDynamicAttribute : DynamicAttributeBase<int>
    {
        public IntegerDynamicAttribute(string name, int value)
            :base(name, value)
        {

        }
    }
}

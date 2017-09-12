using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    public class StringDynamicAttribute : DynamicAttributeBase<string>
    {
        public StringDynamicAttribute(string name, string value)
            :base(name, value)
        {

        }
    }
}

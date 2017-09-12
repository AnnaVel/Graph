using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class BooleanDynamicAttribute : DynamicAttributeBase<bool>
    {
        public BooleanDynamicAttribute(string name, bool value)
            :base(name, value)
        {

        }
    }
}

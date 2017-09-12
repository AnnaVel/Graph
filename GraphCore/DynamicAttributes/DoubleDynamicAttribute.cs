using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    public class DoubleDynamicAttribute : DynamicAttributeBase<double>
    {
        public DoubleDynamicAttribute(string name, double value)
            :base(name, value)
        {

        }
    }
}

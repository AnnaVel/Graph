using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class DoubleGraphItemProperty : GraphItemPropertyBase<double>
    {
        public DoubleGraphItemProperty(string name, double value)
            :base(name, value)
        {

        }
    }
}

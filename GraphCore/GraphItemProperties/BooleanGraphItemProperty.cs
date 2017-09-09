using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class BooleanGraphItemProperty : GraphItemPropertyBase<bool>
    {
        public BooleanGraphItemProperty(string name, bool value)
            :base(name, value)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class ObjectGraphItemProperty : GraphItemPropertyBase<object>
    {
        public ObjectGraphItemProperty(string name, object value)
            :base(name, value)
        {

        }
    }
}

using GraphCore.GraphItemProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomProperty : IGraphItemProperty
    {
        public string Name
        {
            get { return "This is a custom property"; }
        }

        public Type ValueType
        {
            get { return typeof(string); }
        }

        public object ValueAsObject
        {
            get { return "This is a custom property"; }
        }
    }
}

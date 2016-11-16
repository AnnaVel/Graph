using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public interface IGraphItemProperty
    {
        string Name { get; }
        Type ValueType { get; }
        object ValueAsObject { get; }
    }
}

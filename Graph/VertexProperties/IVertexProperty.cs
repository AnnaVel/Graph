using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public interface IVertexProperty
    {
        string Name { get; }
        Type ValueType { get; }
        object ValueAsObject { get; }
    }
}

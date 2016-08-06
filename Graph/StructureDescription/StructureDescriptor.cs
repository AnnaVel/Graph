using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.StructureDescription
{
    public abstract class StructureDescriptor
    {
        internal abstract IEnumerable<AdjacencyDescriptor> TranslateToAdjacencyDescriptors();
    }
}

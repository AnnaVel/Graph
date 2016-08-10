using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.StructureDescription
{
    public class VertexDescriptor : StructureDescriptor
    {
        private readonly string name;

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        public VertexDescriptor(string name)
        {
           // Guard.ThrowExceptionIfNullOrEmpty(name, "name");

            this.name = name;
        }

        internal override IEnumerable<AdjacencyDescriptor> TranslateToAdjacencyDescriptors()
        {
            return new List<AdjacencyDescriptor>()
            {
                new AdjacencyDescriptor(this.name)
            };
        }
    }
}

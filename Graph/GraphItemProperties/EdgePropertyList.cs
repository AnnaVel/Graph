using GraphCore.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    internal class EdgePropertyList : GraphItemPropertyList
    {
        public EdgePropertyList(Edge owner)
            :base(owner)
        {

        }

        protected override GraphItemPropertyFactory GetPropertyFactoryFromOwner()
        {
            GraphStructure owningStructure = this.Owner.Owner;

            if (owningStructure == null)
            {
                throw new InvalidOperationException("The item is not part of a structure and a property cannot be added.");
            }

            return owningStructure.EdgePropertyFactory;
        }
    }
}

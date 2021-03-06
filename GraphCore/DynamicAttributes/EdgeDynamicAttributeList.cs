﻿using GraphCore.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.DynamicAttributes
{
    internal class EdgeDynamicAttributeList : DynamicAttributeList
    {
        public EdgeDynamicAttributeList(Edge owner)
            :base(owner)
        {

        }

        protected override DynamicAttributeFactory GetDynamicAttributeFactoryFromOwner()
        {
            GraphStructure owningStructure = this.Owner.Owner;

            if (owningStructure == null)
            {
                throw new InvalidOperationException("The item is not part of a structure and a dynamic attribute cannot be added.");
            }

            return owningStructure.EdgeDynamicAttributeFactory;
        }
    }
}

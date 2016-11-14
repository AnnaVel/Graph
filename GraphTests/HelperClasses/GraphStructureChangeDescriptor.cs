using GraphCore;
using GraphCore.Events;
using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    internal class GraphStructureChangeDescriptor
    {
        public ChangeAction ChangeAction { get; }
        public GraphStructureItem GraphStructureItem { get; }

        public GraphStructureChangeDescriptor(ChangeAction changeAction, GraphStructureItem graphStructureItem)
        {
            this.GraphStructureItem = graphStructureItem;
            this.ChangeAction = changeAction;
        }

        public override bool Equals(object obj)
        {
            GraphStructureChangeDescriptor otherDescriptor = obj as GraphStructureChangeDescriptor;

            if(otherDescriptor == null)
            {
                return false;
            }

            return this.ChangeAction.Equals(otherDescriptor.ChangeAction) &&
                this.GraphStructureItem.Equals(otherDescriptor.GraphStructureItem);
        }

        public override int GetHashCode()
        {
            return HashHelper.CombineHashCodes(this.ChangeAction, this.GraphStructureItem);
        }
    }
}

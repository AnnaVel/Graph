using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Events
{
    public class GraphStructureChangedEventArgs : EventArgs
    {
        private ChangeAction changeAction;
        private IEnumerable<GraphStructureItem> itemsChanged;

        public ChangeAction ChangeAction
        {
            get
            {
                return this.changeAction;
            }
        }

        public IEnumerable<GraphStructureItem> ItemsChanged
        {
            get
            {
                return this.itemsChanged;
            }
        }

        public GraphStructureChangedEventArgs(ChangeAction changeAction, IEnumerable<GraphStructureItem> itemsChanged)
        {
            Guard.ThrowExceptionIfNull(itemsChanged, "itemsChanged");

            this.changeAction = changeAction;
            this.itemsChanged = itemsChanged;
        }
    }
}

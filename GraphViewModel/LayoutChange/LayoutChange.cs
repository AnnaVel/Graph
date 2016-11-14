using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.LayoutChange
{
    public abstract class LayoutChange
    {
        private LayoutChangeAction layoutChangeAction;

        public LayoutChangeAction LayoutChangeAction
        {
            get
            {
                return this.layoutChangeAction;
            }
        }

        public LayoutChange(LayoutChangeAction layoutChangeAction)
        {
            this.layoutChangeAction = layoutChangeAction;
        }
    }
}

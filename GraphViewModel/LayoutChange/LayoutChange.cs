using GraphCore.Events;

namespace GraphViewModel.LayoutChange
{
    public abstract class LayoutChange
    {
        private ChangeAction layoutChangeAction;

        public ChangeAction LayoutChangeAction
        {
            get
            {
                return this.layoutChangeAction;
            }
        }

        public LayoutChange(ChangeAction layoutChangeAction)
        {
            this.layoutChangeAction = layoutChangeAction;
        }
    }
}

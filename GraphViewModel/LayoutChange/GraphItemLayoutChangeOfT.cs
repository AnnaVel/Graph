using GraphCore;
using GraphCore.Events;
using GraphCore.Utilities;
using GraphViewModel.ViewModels;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public abstract class GraphItemLayoutChange<T> : LayoutChange
        where T : GraphItemViewModel
    {
        private T changedObjectViewModel;

        public T ChangedObjectViewModel
        {
            get
            {
                return this.changedObjectViewModel;
            }
        }

        public GraphItemLayoutChange(ChangeAction layoutChangeAction, T changedObject)
            :base(layoutChangeAction)
        {
            Guard.ThrowExceptionIfNull(changedObject, "changedObject");

            this.changedObjectViewModel = changedObject;
        }
    }
}

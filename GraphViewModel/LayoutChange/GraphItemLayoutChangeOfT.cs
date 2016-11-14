using GraphCore;
using GraphCore.Utilities;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public abstract class GraphItemLayoutChange<T> : LayoutChange
        where T : GraphStructureItem
    {
        private T changedObject;
        private Point location;

        public T ChangedObject
        {
            get
            {
                return this.changedObject;
            }
        }

        public Point Location
        {
            get
            {
                return this.location;
            }
        }

        public GraphItemLayoutChange(LayoutChangeAction layoutChangeAction, Point location, T changedObject)
            :base(layoutChangeAction)
        {
            Guard.ThrowExceptionIfNull(changedObject, "changedObject");

            this.location = location;
            this.changedObject = changedObject;
        }
    }
}

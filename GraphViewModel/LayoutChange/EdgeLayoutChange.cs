using GraphCore.Edges;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public class EdgeLayoutChange : GraphItemLayoutChange<Edge>
    {
        private Point endLocation;

        public Point EndLocation
        {
            get
            {
                return this.endLocation;
            }
        }

        public EdgeLayoutChange(LayoutChangeAction layoutChangeAction, Point location, Point endLocation, Edge changedEdge)
            : base(layoutChangeAction, location, changedEdge)
        {
            this.endLocation = endLocation;
        }
    }
}

using GraphCore.Vertices;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public class VertexLayoutChange : GraphItemLayoutChange<Vertex>
    {
        public VertexLayoutChange(LayoutChangeAction layoutChangeAction, Point location, Vertex changedVertex)
            : base(layoutChangeAction, location, changedVertex)
        {

        }
    }
}

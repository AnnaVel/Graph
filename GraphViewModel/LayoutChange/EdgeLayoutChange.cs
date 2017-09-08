using GraphCore.Edges;
using GraphCore.Events;
using GraphViewModel.ViewModels;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public class EdgeLayoutChange : GraphItemLayoutChange<EdgeViewModel>
    {
        public EdgeLayoutChange(ChangeAction layoutChangeAction, EdgeViewModel changedEdgeViewModel)
            : base(layoutChangeAction, changedEdgeViewModel)
        {
        }
    }
}

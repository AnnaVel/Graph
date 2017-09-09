using GraphCore.Events;
using GraphViewModel.ViewModels;
using System.Windows;

namespace GraphViewModel.LayoutChange
{
    public class VertexLayoutChange : GraphItemLayoutChange<VertexViewModel>
    {
        public VertexLayoutChange(ChangeAction layoutChangeAction, VertexViewModel changedVertexViewModel)
            : base(layoutChangeAction, changedVertexViewModel)
        {

        }
    }
}

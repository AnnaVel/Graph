using System.Windows;

namespace GraphView.Views
{
    public class VertexView : GraphItemView
    {
        static VertexView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(VertexView), new FrameworkPropertyMetadata(typeof(VertexView)));
        }
    }
}

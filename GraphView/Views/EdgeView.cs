using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphView.Views
{
    public class EdgeView : GraphItemView
    {
        static EdgeView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EdgeView), new FrameworkPropertyMetadata(typeof(EdgeView)));
        }
    }
}

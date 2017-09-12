using GraphCore;
using GraphCore.Vertices;
using GraphView.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGraphTestApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.graphView.Loaded += GraphView_Loaded;
        }

        private void GraphView_Loaded(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();

            this.graphView.Graph = graph;

            Vertex x = graph.GraphStructure.AddVertex("x");
            x.SetDynamicAttribute("order", "1");
            Vertex y = graph.GraphStructure.AddVertex("y");
            y.SetDynamicAttribute("order", "2");
            graph.GraphStructure.AddLine(x, y);
            Vertex z = graph.GraphStructure.AddVertex("z");
            z.SetDynamicAttribute("order", "3");
            graph.GraphStructure.AddLine(x, z);
        }

        private void ChangeColorToSecondVertex(object sender, RoutedEventArgs e)
        {
            Vertex second = this.graphView.Graph.GraphStructure.Vertices.ElementAt(1);
            second.SetDynamicAttribute("color", "Green");
        }

        private void RemoveFirstVertex(object sender, RoutedEventArgs e)
        {
            Vertex first = this.graphView.Graph.GraphStructure.Vertices.ElementAt(0);
            this.graphView.Graph.GraphStructure.RemoveVertex(first);
        }
    }
}

using GraphCore;
using GraphCore.Vertices;
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
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y);
            Vertex z = graph.GraphStructure.AddVertex("z");
            graph.GraphStructure.AddLine(x, z);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Vertex first = this.graphView.Graph.GraphStructure.Vertices.First();
            Vertex second = this.graphView.Graph.GraphStructure.Vertices.ElementAt(1);
            this.graphView.Graph.GraphStructure.RemoveEdgesBetween(first, second);
        }
    }
}

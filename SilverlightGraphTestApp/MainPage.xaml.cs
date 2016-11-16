using GraphCore;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace SilverlightGraphTestApp
{
    public partial class MainPage : UserControl
    {
        public MainPage()
        {
            InitializeComponent();
            this.graphView.Loaded += GraphView_Loaded;
        }

        private void GraphView_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();

            this.graphView.Graph = graph;

            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y);
            Vertex z = graph.GraphStructure.AddVertex("z");
            graph.GraphStructure.AddLine(x, z);

            Vertex first = this.graphView.Graph.GraphStructure.Vertices.First();
            Vertex second = this.graphView.Graph.GraphStructure.Vertices.ElementAt(1);
            this.graphView.Graph.GraphStructure.RemoveEdgesBetween(first, second);
        }
    }
}

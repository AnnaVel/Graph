using GraphCore;
using GraphCore.Algorithms;
using GraphCore.Vertices;
using System.Linq;
using System.Windows;

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
            
        }

        private void RunDijkstra(object sender, RoutedEventArgs e)
        {
            Graph graph = this.graphView.Graph;
            IAlgorithm dijkstra = graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraRouteAlgorithmName);
            dijkstra.SetDynamicAttributesInStructure = true;

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex six = graph.GraphStructure.GetVertexByValue(6);

            graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, six));
        }

        private void ConstructDijkstraGraph(object sender, RoutedEventArgs e)
        {
            Graph graph = new Graph();
            this.graphView.Graph = graph;

            Vertex one = graph.GraphStructure.AddVertex(1);
            Vertex two = graph.GraphStructure.AddVertex(2);
            Vertex three = graph.GraphStructure.AddVertex(3);
            Vertex four = graph.GraphStructure.AddVertex(4);
            Vertex five = graph.GraphStructure.AddVertex(5);
            Vertex six = graph.GraphStructure.AddVertex(6);

            graph.GraphStructure.AddLine(one, two, 7);
            graph.GraphStructure.AddLine(one, six, 14);
            graph.GraphStructure.AddLine(one, three, 9);
            graph.GraphStructure.AddLine(two, three, 10);
            graph.GraphStructure.AddLine(two, four, 15);
            graph.GraphStructure.AddLine(three, six, 14);
            graph.GraphStructure.AddLine(three, four, 11);
            graph.GraphStructure.AddLine(six, five, 9);
            graph.GraphStructure.AddLine(four, five, 4);
        }

        private void ConstructSimpleGraph(object sender, RoutedEventArgs e)
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

        private void SetComplexAttribute(object sender, RoutedEventArgs e)
        {
            Vertex second = this.graphView.Graph.GraphStructure.Vertices.ElementAt(1);
            second.SetDynamicAttribute("color:test", "Purple");
        }

        private void RemoveComplexAttribute(object sender, RoutedEventArgs e)
        {
            Vertex second = this.graphView.Graph.GraphStructure.Vertices.ElementAt(1);
            second.RemoveDynamicAttribute("color:test");
        }
    }
}

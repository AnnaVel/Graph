using GraphCore;
using GraphCore.Edges;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestFixture]
    public class EdgeFactoryTests
    {
        [Test]
        public void CreateDoubleValuedEdgeTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y, 1d);

            Edge edge = graph.GraphStructure.Edges.First();
            Assert.IsInstanceOf(typeof(DoubleValueEdge), edge);
        }

        [Test]
        public void CreateObjectValuedEdgeTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y, graph);

            Edge edge = graph.GraphStructure.Edges.First();
            Assert.IsInstanceOf(typeof(ObjectValueEdge), edge);
        }

        [Test]
        public void CreateCustomValuedEdgeTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.EdgeFactory = new CustomEdgeFactory();
            graph.GraphStructure.AddLine(x, y, "customEdge");

            Edge edge = graph.GraphStructure.Edges.First();
            Assert.IsInstanceOf(typeof(CustomValueEdge), edge);
        }

        [Test]
        public void CreateUnweightedEdgeTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y, null);

            Edge edge = graph.GraphStructure.Edges.First();
            Assert.IsInstanceOf(typeof(UnweightedEdge), edge);
        }
    }
}

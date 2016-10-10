using GraphCore;
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
    public class VertexFactoryTests
    {
        [Test]
        public void CreateTextValuedVertexTest()
        {
            Graph graph = new Graph();

            graph.GraphStructure.AddVertex("test");

            Vertex vertex = graph.GraphStructure.Vertices.First();
            Assert.IsInstanceOf(typeof(TextValueVertex), vertex);
        }

        [Test]
        public void CreateDoubleValuedVertexTest()
        {
            Graph graph = new Graph();

            graph.GraphStructure.AddVertex(1d);

            Vertex vertex = graph.GraphStructure.Vertices.First();
            Assert.IsInstanceOf(typeof(DoubleValueVertex), vertex);
        }

        [Test]
        public void CreateObjectValuedVertexTest()
        {
            Graph graph = new Graph();

            graph.GraphStructure.AddVertex(graph);

            Vertex vertex = graph.GraphStructure.Vertices.First();
            Assert.IsInstanceOf(typeof(ObjectValueVertex), vertex);
        }

        [Test]
        public void CreateCustomValuedVertexTest()
        {
            Graph graph = new Graph();

            graph.GraphStructure.VertexFactory = new CustomVertexFactory();
            graph.GraphStructure.AddVertex("customVertex");

            Vertex vertex = graph.GraphStructure.Vertices.First();
            Assert.IsInstanceOf(typeof(CustomValueVertex), vertex);
        }

        [Test]
        public void CreateVertexWithNullValue()
        {
            Graph graph = new Graph();

            Assert.Throws<ArgumentNullException>(() =>
                graph.GraphStructure.AddVertex(null)
            );
        }

        [Test]
        public void CreateVertexWithEmptyStringValue()
        {
            Graph graph = new Graph();

            Assert.Throws<ArgumentException>(() =>
                graph.GraphStructure.AddVertex(string.Empty)
            );
        }
    }
}

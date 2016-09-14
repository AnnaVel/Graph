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
    public class GraphAddRemoveVerticesTests
    {
        [Test]
        public void AddVertexTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.AddVertex(vertexValue);

            Assert.AreEqual(1, graph.Vertices.Count());
            Assert.AreEqual(xVertex, graph.Vertices.First());
        }

        [Test]
        public void AddVertexValueTwiceTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.AddVertex(vertexValue);

            Assert.Throws<InvalidOperationException>(() =>
                    graph.AddVertex(vertexValue)
                );
        }

        [Test]
        public void RemoveVertexTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.AddVertex(vertexValue);

            bool result = graph.RemoveVertex(xVertex);

            Assert.IsTrue(result);
            Assert.AreEqual(0, graph.Vertices.Count());
            Assert.DoesNotThrow(() => graph.AddVertex(vertexValue));
        }

        [Test]
        public void RemoveVertexRelationshipTest()
        {
            string firstVertexValue = "x";
            string secondVertexValue = "y";
            Graph graph = new Graph();
            Vertex xVertex = graph.AddVertex(firstVertexValue);
            Vertex yVertex = graph.AddVertex(secondVertexValue);
            graph.AddLine(xVertex, yVertex);

            graph.RemoveVertex(yVertex);

            Assert.AreEqual(0, xVertex.GetSuccessors().Count());
            Assert.AreEqual(0, yVertex.GetSuccessors().Count());
        }

        [Test]
        public void RemoveVertexTwiceTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.AddVertex(vertexValue);
            graph.RemoveVertex(xVertex);

            bool result = graph.RemoveVertex(xVertex);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddVertexToOneGraphAndTryToRemoveItFromAnother()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex graphXVertex = graph.AddVertex(vertexValue);
            Graph otherGraph = new Graph();
            Vertex otherGraphXVertex = otherGraph.AddVertex(vertexValue);

            bool result = otherGraph.RemoveVertex(graphXVertex);

            Assert.IsFalse(result);
            Assert.AreEqual(1, otherGraph.Vertices.Count());
            Assert.AreEqual(otherGraphXVertex, otherGraph.Vertices.First());
        }
    }
}

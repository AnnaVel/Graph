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
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);

            Assert.AreEqual(1, graph.GraphStructure.Vertices.Count());
            Assert.AreEqual(xVertex, graph.GraphStructure.Vertices.First());
            Assert.AreEqual(graph.GraphStructure, xVertex.Owner);
        }

        [Test]
        public void AddVertexValueTwiceTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);

            Assert.Throws<InvalidOperationException>(() =>
                    graph.GraphStructure.AddVertex(vertexValue)
                );
        }

        [Test]
        public void RemoveVertexTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);

            bool result = graph.GraphStructure.RemoveVertex(xVertex);

            Assert.IsTrue(result);
            Assert.IsNull(xVertex.Owner);
            Assert.AreEqual(0, graph.GraphStructure.Vertices.Count());
            Assert.DoesNotThrow(() => graph.GraphStructure.AddVertex(vertexValue));
        }

        [Test]
        public void RemoveVertexResultRelationshipTest()
        {
            string firstVertexValue = "x";
            string secondVertexValue = "y";
            Graph graph = new Graph();
            Vertex xVertex = graph.GraphStructure.AddVertex(firstVertexValue);
            Vertex yVertex = graph.GraphStructure.AddVertex(secondVertexValue);
            graph.GraphStructure.AddLine(xVertex, yVertex);

            graph.GraphStructure.RemoveVertex(yVertex);

            Assert.AreEqual(0, xVertex.GetSuccessors().Count());
            Assert.AreEqual(0, graph.GraphStructure.Edges.Count());
            Assert.AreEqual(0, graph.GraphStructure.GetVertexSuccessors(xVertex));
        }

        [Test]
        public void RemoveVertexInactiveApiTest()
        {
            string firstVertexValue = "x";
            string secondVertexValue = "y";
            Graph graph = new Graph();
            Vertex xVertex = graph.GraphStructure.AddVertex(firstVertexValue);
            Vertex yVertex = graph.GraphStructure.AddVertex(secondVertexValue);
            graph.GraphStructure.AddLine(xVertex, yVertex);

            graph.GraphStructure.RemoveVertex(yVertex);

            Assert.Throws<InvalidOperationException>(() => yVertex.GetSuccessors());
            Assert.Throws<InvalidOperationException>(() => yVertex.GetPredecessors());
            Assert.Throws<InvalidOperationException>(() => yVertex.GetEdgesFrom(xVertex));
            Assert.Throws<InvalidOperationException>(() => yVertex.GetEdgesTo(xVertex));
            Assert.Throws<InvalidOperationException>(() => yVertex.GetIncomingEdges());
            Assert.Throws<InvalidOperationException>(() => yVertex.GetOutgoingEdges());
            Assert.Throws<ArgumentException>(() => xVertex.GetEdgesTo(yVertex));
            Assert.Throws<ArgumentException>(() => graph.GraphStructure.GetEdgesBetween(xVertex, yVertex));
            Assert.Throws<ArgumentException>(() => graph.GraphStructure.GetEdgesLeadingFromTo(xVertex, yVertex));
            Assert.Throws<ArgumentException>(() => graph.GraphStructure.GetVertexSuccessors(yVertex));
            Assert.Throws<ArgumentException>(() => graph.GraphStructure.GetVertexPredecessors(yVertex));
        }

        [Test]
        public void RemoveVertexTwiceTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);
            graph.GraphStructure.RemoveVertex(xVertex);

            bool result = graph.GraphStructure.RemoveVertex(xVertex);

            Assert.IsFalse(result);
        }

        [Test]
        public void AddVertexToOneGraphAndTryToRemoveItFromAnother()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            Vertex graphXVertex = graph.GraphStructure.AddVertex(vertexValue);
            Graph otherGraph = new Graph();
            Vertex otherGraphXVertex = otherGraph.GraphStructure.AddVertex(vertexValue);

            bool result = otherGraph.GraphStructure.RemoveVertex(graphXVertex);

            Assert.IsFalse(result);
            Assert.AreEqual(1, otherGraph.GraphStructure.Vertices.Count());
            Assert.AreEqual(otherGraphXVertex, otherGraph.GraphStructure.Vertices.First());
        }
    }
}

using GraphCore;
using GraphCore.Edges;
using GraphCore.Events;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);

            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);
            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);

            Assert.AreEqual(1, graph.GraphStructure.Vertices.Count());
            Assert.AreEqual(xVertex, graph.GraphStructure.Vertices.First());
            Assert.AreEqual(graph.GraphStructure, xVertex.Owner);
            eventFireAsserter.AssertFiredChanges();
        }

        [Test]
        public void AddVertexValueTwiceTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);

            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);

            Assert.Throws<InvalidOperationException>(() =>
                    graph.GraphStructure.AddVertex(vertexValue)
                );
            eventFireAsserter.AssertFiredChanges();
        }

        [Test]
        public void RemoveVertexTest()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);

            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, xVertex);

            bool result = graph.GraphStructure.RemoveVertex(xVertex);

            Assert.IsTrue(result);
            Assert.IsNull(xVertex.Owner);
            Assert.AreEqual(0, graph.GraphStructure.Vertices.Count());
            eventFireAsserter.AssertFiredChanges();
            Assert.DoesNotThrow(() => graph.GraphStructure.AddVertex(vertexValue));
        }

        [Test]
        public void RemoveVertexResultRelationshipTest()
        {
            string firstVertexValue = "x";
            string secondVertexValue = "y";
            Graph graph = new Graph();
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex xVertex = graph.GraphStructure.AddVertex(firstVertexValue);
            Vertex yVertex = graph.GraphStructure.AddVertex(secondVertexValue);
            Edge xYEdge = graph.GraphStructure.AddLine(xVertex, yVertex);

            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);
            eventFireAsserter.AddExpectedChange(ChangeAction.Add, yVertex);
            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xYEdge);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, xYEdge);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, yVertex);

            graph.GraphStructure.RemoveVertex(yVertex);

            Assert.AreEqual(0, xVertex.GetSuccessors().Count());
            Assert.AreEqual(0, graph.GraphStructure.Edges.Count());
            Assert.AreEqual(0, graph.GraphStructure.GetVertexSuccessors(xVertex).Count());
            eventFireAsserter.AssertFiredChanges();
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
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);
            graph.GraphStructure.RemoveVertex(xVertex);

            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, xVertex);

            bool result = graph.GraphStructure.RemoveVertex(xVertex);

            Assert.IsFalse(result);
            eventFireAsserter.AssertFiredChanges();
        }

        [Test]
        public void AddVertexToOneGraphAndTryToRemoveItFromAnother()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex graphXVertex = graph.GraphStructure.AddVertex(vertexValue);
            eventFireAsserter.AddExpectedChange(ChangeAction.Add, graphXVertex);

            Graph otherGraph = new Graph();
            GraphStructureChangedEventAsserter otherEventFireAsserter = new GraphStructureChangedEventAsserter(otherGraph);
            Vertex otherGraphXVertex = otherGraph.GraphStructure.AddVertex(vertexValue);
            otherEventFireAsserter.AddExpectedChange(ChangeAction.Add, otherGraphXVertex);

            bool result = otherGraph.GraphStructure.RemoveVertex(graphXVertex);

            Assert.IsFalse(result);
            Assert.AreEqual(1, otherGraph.GraphStructure.Vertices.Count());
            Assert.AreEqual(otherGraphXVertex, otherGraph.GraphStructure.Vertices.First());
            eventFireAsserter.AssertFiredChanges();
            otherEventFireAsserter.AssertFiredChanges();
        }

        [Test]
        public void RemoveVertexWithCircularEdge()
        {
            string vertexValue = "x";
            Graph graph = new Graph();
            GraphStructureChangedEventAsserter eventFireAsserter = new GraphStructureChangedEventAsserter(graph);
            Vertex xVertex = graph.GraphStructure.AddVertex(vertexValue);
            Edge xEdge = graph.GraphStructure.AddLine(xVertex, xVertex);

            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xVertex);
            eventFireAsserter.AddExpectedChange(ChangeAction.Add, xEdge);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, xEdge);
            eventFireAsserter.AddExpectedChange(ChangeAction.Remove, xVertex);

            bool result = graph.GraphStructure.RemoveVertex(xVertex);

            Assert.IsTrue(result);
            Assert.AreEqual(0, graph.GraphStructure.Vertices.Count());
            Assert.AreEqual(0, graph.GraphStructure.Edges.Count());
            eventFireAsserter.AssertFiredChanges();
        }
    }
}

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
    public class GraphAddRemoveEdgesTests
    {
        [Test]
        public void AddUnweightedArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            Edge edge = graph.GraphStructure.AddArrow(x, y);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, null, typeof(UnweightedEdge), edge);
            this.AssertRelationship(graph, x, y, new List<Edge>() { edge }, new List<Edge>());
        }

        [Test]
        public void AddWeightedArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            double weight = 0.5;
            Edge edge = graph.GraphStructure.AddArrow(x, y, weight);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, weight, typeof(DoubleValueEdge), edge);
            this.AssertRelationship(graph, x, y, new List<Edge>() { edge }, new List<Edge>());
        }

        [Test]
        public void AddMultipleWeightedAndUnweightedArrowsTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            double weight = 0.5;
            Edge firstArrow = graph.GraphStructure.AddArrow(x, y, weight);
            Edge secondArrow = graph.GraphStructure.AddArrow(x, y);
            Edge thirdArrow = graph.GraphStructure.AddArrow(x, y, weight);
            Edge fourthArrow = graph.GraphStructure.AddArrow(x, y);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, weight, typeof(DoubleValueEdge), firstArrow);
            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, null, typeof(UnweightedEdge), secondArrow);
            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, weight, typeof(DoubleValueEdge), thirdArrow);
            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, true, null, typeof(UnweightedEdge), fourthArrow);
            this.AssertRelationship(graph, x, y, new List<Edge>() { firstArrow, secondArrow, thirdArrow, fourthArrow }, new List<Edge>());
        }

        [Test]
        public void AddArrowRemovedVerticesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.RemoveVertex(x);

            Assert.Throws<ArgumentException>(() => graph.GraphStructure.AddArrow(x, y));
        }

        [Test]
        public void AddArrowPredecessorAndSuccessorAreSameVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");

            Edge arrow = graph.GraphStructure.AddArrow(x, x);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, x, true, null, typeof(UnweightedEdge), arrow);
            this.AssertRelationship(graph, x, x, new List<Edge>() { arrow }, new List<Edge>());
        }

        [Test]
        public void AddUnweightedLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            Edge lineEdge = graph.GraphStructure.AddLine(x, y);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, false, null, typeof(UnweightedEdge), lineEdge);
            this.AssertRelationship(graph, x, y, new List<Edge>() { lineEdge }, new List<Edge>() { lineEdge });
        }

        [Test]
        public void AddWeightedLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            double weight = 0.5;
            Edge lineEdge = graph.GraphStructure.AddLine(x, y, weight);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, false, weight, typeof(DoubleValueEdge), lineEdge);
            this.AssertRelationship(graph, x, y, new List<Edge>() { lineEdge }, new List<Edge>() { lineEdge });
        }

        [Test]
        public void AddMultipleLinesFromOneVertexToOthers()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Vertex z = graph.GraphStructure.AddVertex("z");
            Vertex u = graph.GraphStructure.AddVertex("u");

            Edge xY = graph.GraphStructure.AddLine(x, y);
            Edge xZ = graph.GraphStructure.AddLine(x, z);
            Edge xU = graph.GraphStructure.AddLine(x, u);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, false, null, typeof(UnweightedEdge), xY);
            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, z, false, null, typeof(UnweightedEdge), xZ);
            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, u, false, null, typeof(UnweightedEdge), xU);

            this.AssertRelationship(graph, x, y, new List<Edge>() { xY }, new List<Edge>() { xY });
            this.AssertRelationship(graph, x, z, new List<Edge>() { xZ }, new List<Edge>() { xZ });
            this.AssertRelationship(graph, x, u, new List<Edge>() { xU }, new List<Edge>() { xU });
        }

        [Test]
        public void AddLineRemovedVerticesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.RemoveVertex(x);
            graph.GraphStructure.RemoveVertex(y);

            Assert.Throws<ArgumentException>(() => graph.GraphStructure.AddArrow(x, y));
        }

        [Test]
        public void AddLinePredecessorAndSuccessorAreSameVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");

            Edge line = graph.GraphStructure.AddLine(x, x);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, x, false, null, typeof(UnweightedEdge), line);
            this.AssertRelationship(graph, x, x, new List<Edge>() { line }, new List<Edge>() { line });
        }

        [Test]
        public void RemoveEdgesBetweenSingleArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge arrow = graph.GraphStructure.AddArrow(x, y);

            bool result = graph.GraphStructure.RemoveEdgesBetween(x, y);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(arrow);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgesBetweenLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);

            bool result = graph.GraphStructure.RemoveEdgesBetween(x, y);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(line);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgesBetweenNoEdgesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            bool result = graph.GraphStructure.RemoveEdgesBetween(x, y);

            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveEdgesBetweenMultipleArrowsTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge firstArrow = graph.GraphStructure.AddArrow(x, y);
            Edge secondArrow = graph.GraphStructure.AddArrow(x, y);
            Edge thirdArrow = graph.GraphStructure.AddArrow(x, y);
            Edge fourthArrow = graph.GraphStructure.AddArrow(x, y);

            bool result = graph.GraphStructure.RemoveEdgesBetween(x, y);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(firstArrow, secondArrow, thirdArrow, fourthArrow);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgesBetweenRemovedVerticesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddArrow(x, y);
            graph.GraphStructure.RemoveVertex(x);
            graph.GraphStructure.RemoveVertex(y);

            Assert.Throws<ArgumentException>(() => graph.GraphStructure.RemoveEdgesBetween(x, y));
        }

        [Test]
        public void RemoveEdgesBetweenPredecessorAndSuccessorAreSameVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Edge arrow = graph.GraphStructure.AddArrow(x, x);
            Edge line = graph.GraphStructure.AddLine(x, x);

            bool result = graph.GraphStructure.RemoveEdgesBetween(x, x);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(line, arrow);
            this.AssertRelationship(graph, x, x, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeSingleArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge arrow = graph.GraphStructure.AddArrow(x, y);

            bool result = graph.GraphStructure.RemoveEdge(arrow);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(arrow);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);

            bool result = graph.GraphStructure.RemoveEdge(line);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(line);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeSpecificLineAmongMany()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge arrow = graph.GraphStructure.AddArrow(x, y, 0.5);
            Edge line = graph.GraphStructure.AddLine(x, y);
            Edge weightedLine = graph.GraphStructure.AddLine(x, y, 0.5);

            bool result = graph.GraphStructure.RemoveEdge(weightedLine);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(weightedLine);
            this.AssertRelationship(graph, x, y, new List<Edge>() { arrow, line }, new List<Edge>() { line });
        }

        [Test]
        public void RemoveEdgeSpecificArrowAmongMany()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge arrow = graph.GraphStructure.AddArrow(x, y, 0.5);
            Edge line = graph.GraphStructure.AddLine(x, y);
            Edge weightedLine = graph.GraphStructure.AddLine(x, y, 0.5);

            bool result = graph.GraphStructure.RemoveEdge(arrow);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(arrow);
            this.AssertRelationship(graph, x, y, new List<Edge>() { line, weightedLine }, new List<Edge>() { line, weightedLine });
        }

        [Test]
        public void RemoveRemovedEdgeTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);
            graph.GraphStructure.RemoveEdge(line);

            bool result = graph.GraphStructure.RemoveEdge(line);

            Assert.IsFalse(result);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeRemovedVerticesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);
            graph.GraphStructure.RemoveVertex(x);
            graph.GraphStructure.RemoveVertex(y);

            Assert.Throws<ArgumentException>(() => graph.GraphStructure.RemoveEdge(line));
        }

        [Test]
        public void RemoveEdgeArrowPredecessorAndSuccessorAreSameVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Edge arrow = graph.GraphStructure.AddArrow(x, x);

            bool result = graph.GraphStructure.RemoveEdge(arrow);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(arrow);
            this.AssertRelationship(graph, x, x, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeLinePredecessorAndSuccessorAreSameVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Edge line = graph.GraphStructure.AddLine(x, x);

            bool result = graph.GraphStructure.RemoveEdge(line);

            Assert.IsTrue(result);
            this.AssertEdgesHaveBeenUnregistered(line);
            this.AssertRelationship(graph, x, x, new List<Edge>(), new List<Edge>());
        }

        [Test]
        public void RemoveEdgeFromDifferentStructureTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);
            Graph otherGraph = new Graph();


            bool result = otherGraph.GraphStructure.RemoveEdge(line);

            Assert.IsFalse(result);
            Assert.AreEqual(graph.GraphStructure, line.Owner);
        }

        [Test]
        public void AddAndRemoveIterationTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            int iterations = 5;
            bool result = false;

            for (int i = 0; i < iterations; i++)
            {
                Edge arrow = graph.GraphStructure.AddArrow(x, y);
                result |= graph.GraphStructure.RemoveEdgesBetween(x, y);
                Assert.IsTrue(result);
                this.AssertEdgesHaveBeenUnregistered(arrow);
            }

            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
            this.AssertRelationship(graph, x, y, new List<Edge>(), new List<Edge>());
        }

        private void AssertRelationship(Graph graph, Vertex firstVertex, Vertex secondVertex, 
            IEnumerable<Edge> expectedEdgesFirstToSecond, IEnumerable<Edge> expectedEdgesSecondToFirst)
        {
            this.AssertRelationshipFromTo(graph, firstVertex, secondVertex, expectedEdgesFirstToSecond);
            this.AssertRelationshipFromTo(graph, secondVertex, firstVertex, expectedEdgesSecondToFirst);

            IEnumerable<Edge> allExpectedEdges = expectedEdgesFirstToSecond.Union(expectedEdgesSecondToFirst);
            IEnumerable<Edge> actualEdges = graph.GraphStructure.GetEdgesBetween(firstVertex, secondVertex);
            TestHelper.AssertEnumerablesAreEqual<Edge>(allExpectedEdges, actualEdges);
        }

        private void AssertRelationshipFromTo(Graph graph, Vertex fromVertex, Vertex toVertex, IEnumerable<Edge> expectedEdges)
        {
            IEnumerable<Vertex> fromVertexSuccessorsVertexApi = fromVertex.GetSuccessors();
            IEnumerable<Vertex> fromVertexSuccessorsGraphApi = graph.GraphStructure.GetVertexSuccessors(fromVertex);
            IEnumerable<Vertex> toVertexPredecessorsVertexApi = toVertex.GetPredecessors();
            IEnumerable<Vertex> toVertexPredecessorsGraphApi = graph.GraphStructure.GetVertexPredecessors(toVertex);

            if (expectedEdges.Count() != 0)
            {
                Assert.IsTrue(fromVertexSuccessorsVertexApi.Contains(toVertex));
                Assert.IsTrue(fromVertexSuccessorsGraphApi.Contains(toVertex));
                Assert.IsTrue(toVertexPredecessorsVertexApi.Contains(fromVertex));
                Assert.IsTrue(toVertexPredecessorsGraphApi.Contains(fromVertex));
            }
            else
            {
                Assert.IsFalse(fromVertexSuccessorsVertexApi.Contains(toVertex));
                Assert.IsFalse(fromVertexSuccessorsGraphApi.Contains(toVertex));
                Assert.IsFalse(toVertexPredecessorsVertexApi.Contains(fromVertex));
                Assert.IsFalse(toVertexPredecessorsGraphApi.Contains(fromVertex));
            }

            IEnumerable<Edge> actualEdges = graph.GraphStructure.GetEdgesLeadingFromTo(fromVertex, toVertex);
            TestHelper.AssertEnumerablesAreEqual<Edge>(expectedEdges, actualEdges);

            actualEdges = fromVertex.GetEdgesTo(toVertex);
            TestHelper.AssertEnumerablesAreEqual<Edge>(expectedEdges, actualEdges);

            actualEdges = toVertex.GetEdgesFrom(fromVertex);
            TestHelper.AssertEnumerablesAreEqual<Edge>(expectedEdges, actualEdges);
        }

        private void AssertEdgeCreatedAndAddedCorrectly(Graph graph, Vertex firstVertex, Vertex secondVertex, bool isDirected, object value, Type edgeType, Edge actualEdge)
        {
            Assert.AreEqual(firstVertex, actualEdge.FirstVertex);
            Assert.AreEqual(secondVertex, actualEdge.SecondVertex);
            Assert.AreEqual(isDirected, actualEdge.IsDirected);
            Assert.AreEqual(value, actualEdge.ValueAsObject);
            Assert.AreEqual(graph.GraphStructure, actualEdge.Owner);
            Assert.IsInstanceOf(edgeType, actualEdge);
            Assert.AreEqual(graph.GraphStructure, actualEdge.Owner);
        }

        private void AssertEdgesHaveBeenUnregistered(params Edge[] edges)
        {
            foreach (Edge edge in edges)
            {
                Assert.IsNull(edge.Owner);
            }
        }

        private void AssertOnlyRelationshipsHaveBeenRemoved(Graph graph, Vertex first, Vertex second)
        {
            Assert.AreEqual(2, graph.GraphStructure.Vertices.Count());
            Assert.AreEqual(first, graph.GraphStructure.Vertices.ElementAt(0));
            Assert.AreEqual(second, graph.GraphStructure.Vertices.ElementAt(1));

            Assert.AreEqual(0, first.GetSuccessors().Count());
            Assert.AreEqual(0, second.GetSuccessors().Count());
            Assert.AreEqual(0, first.GetEdgesTo(second).Count());
            Assert.AreEqual(0, second.GetEdgesFrom(first).Count());
        }
    }
}

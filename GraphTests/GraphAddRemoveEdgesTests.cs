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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { edge });
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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { edge });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { edge });
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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { firstArrow, secondArrow, thirdArrow, fourthArrow });
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
            Assert.Fail();
        }

        [Test]
        public void AddUnweightedLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            Edge lineEdge = graph.GraphStructure.AddLine(x, y);

            this.AssertEdgeCreatedAndAddedCorrectly(graph, x, y, false, null, typeof(UnweightedEdge), lineEdge);
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { lineEdge });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { lineEdge });
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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { lineEdge });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { lineEdge });
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

            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { xY });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { xY });

            this.AssertRelationshipFromTo(graph, x, z, new List<Edge>() { xZ });
            this.AssertRelationshipFromTo(graph, z, x, new List<Edge>() { xZ });

            this.AssertRelationshipFromTo(graph, x, u, new List<Edge>() { xU });
            this.AssertRelationshipFromTo(graph, u, x, new List<Edge>() { xU });
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
            Assert.Fail();
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
            Assert.Fail();
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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { arrow, line });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { line });
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
            this.AssertRelationshipFromTo(graph, x, y, new List<Edge>() { line, weightedLine });
            this.AssertRelationshipFromTo(graph, y, x, new List<Edge>() { line, weightedLine });
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
            Assert.Fail();
        }

        [Test]
        public void RemoveEdgeLinePredecessorAndSuccessorAreSameVertexTest()
        {
            Assert.Fail();
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
        }

        private void AssertRelationshipFromTo(Graph graph, Vertex fromVertex, Vertex toVertex, IEnumerable<Edge> expectedEdges)
        {
            IEnumerable<Vertex> fromVertexSuccessors = fromVertex.GetSuccessors();
            Assert.AreEqual(1, fromVertexSuccessors.Count());
            Assert.AreEqual(toVertex, fromVertexSuccessors.First());

            fromVertexSuccessors = graph.GraphStructure.GetVertexSuccessors(fromVertex);
            Assert.AreEqual(1, fromVertexSuccessors.Count());
            Assert.AreEqual(toVertex, fromVertexSuccessors.First());

            IEnumerable<Vertex> toVertexPredecessors = toVertex.GetPredecessors();
            Assert.AreEqual(1, toVertexPredecessors.Count());
            Assert.AreEqual(fromVertex, toVertexPredecessors.First());

            toVertexPredecessors = graph.GraphStructure.GetVertexPredecessors(toVertex);
            Assert.AreEqual(1, toVertexPredecessors.Count());
            Assert.AreEqual(fromVertex, toVertexPredecessors.First());

            IEnumerable<Edge> actualEdges = graph.GraphStructure.GetEdgesBetween(fromVertex, toVertex);
            TestHelper.AssertEnumerablesAreEqual<Edge>(expectedEdges, actualEdges);

            actualEdges = graph.GraphStructure.GetEdgesLeadingFromTo(fromVertex, toVertex);
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

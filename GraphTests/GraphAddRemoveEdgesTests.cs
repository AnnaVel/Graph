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
    public class GraphAddRemoveEdgesTests
    {
        [Test]
        public void AddArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            graph.AddArrow(x, y);

            IEnumerable<Vertex> xSuccessors = x.GetSuccessors();
            Assert.AreEqual(1, xSuccessors.Count());
            Assert.AreEqual(y, xSuccessors.First());
        }

        [Test]
        public void AddUnweightedArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            graph.AddArrow(x, y);

            IEnumerable<double?> weights = x.GetArrowWeights(y);
            Assert.AreEqual(1, weights.Count());
            Assert.AreEqual(null, weights.First());
        }

        [Test]
        public void AddWeightedArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            double weight = 0.5;
            graph.AddArrow(x, y, weight);

            IEnumerable<double?> weights = x.GetArrowWeights(y);
            Assert.AreEqual(1, weights.Count());
            Assert.AreEqual(weight, weights.First());
        }

        [Test]
        public void AddMultipleWeightedAndUnweightedArrowsTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            double weight = 0.5;
            graph.AddArrow(x, y, weight);
            graph.AddArrow(x, y);
            graph.AddArrow(x, y, weight);
            graph.AddArrow(x, y);

            IEnumerable<Vertex> xSuccessors = x.GetSuccessors();
            Assert.AreEqual(1, xSuccessors.Count());
            Assert.AreEqual(y, xSuccessors.First());

            IEnumerable<double?> weights = x.GetArrowWeights(y);
            Assert.AreEqual(4, weights.Count());
            Assert.AreEqual(weight, weights.ElementAt(0));
            Assert.AreEqual(null, weights.ElementAt(1));
            Assert.AreEqual(weight, weights.ElementAt(2));
            Assert.AreEqual(null, weights.ElementAt(3));
        }

        [Test]
        public void AddArrowRemovedVerticesTest()
        {
            Assert.Fail();
        }

        [Test]
        public void AddLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            graph.AddLine(x, y);

            IEnumerable<Vertex> xSuccessors = x.GetSuccessors();
            Assert.AreEqual(1, xSuccessors.Count());
            Assert.AreEqual(y, xSuccessors.First());

            IEnumerable<Vertex> ySuccessors = y.GetSuccessors();
            Assert.AreEqual(1, ySuccessors.Count());
            Assert.AreEqual(x, ySuccessors.First());
        }

        [Test]
        public void AddUnweightedLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            graph.AddLine(x, y);

            IEnumerable<double?> xSuccessorWeights = x.GetArrowWeights(y);
            Assert.AreEqual(1, xSuccessorWeights.Count());
            Assert.AreEqual(null, xSuccessorWeights.First());

            IEnumerable<double?> ySuccessorWeights = y.GetArrowWeights(x);
            Assert.AreEqual(1, ySuccessorWeights.Count());
            Assert.AreEqual(null, ySuccessorWeights.First());
        }

        [Test]
        public void AddWeightedLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            double weight = 0.5;
            graph.AddLine(x, y, weight);

            IEnumerable<double?> xSuccessorWeights = x.GetArrowWeights(y);
            Assert.AreEqual(1, xSuccessorWeights.Count());
            Assert.AreEqual(weight, xSuccessorWeights.First());

            IEnumerable<double?> ySuccessorWeights = y.GetArrowWeights(x);
            Assert.AreEqual(1, ySuccessorWeights.Count());
            Assert.AreEqual(weight, ySuccessorWeights.First());
        }

        [Test]
        public void AddLineRemovedVerticesTest()
        {
            Assert.Fail();
        }

        [Test]
        public void RemoveAllEdgesSingleArrowTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);

            bool result = graph.RemoveAllEdges(x, y);

            Assert.IsTrue(result);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
        }

        [Test]
        public void RemoveAllEdgesLineTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddLine(x, y);

            bool result = graph.RemoveAllEdges(x, y);

            Assert.IsTrue(result);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
        }

        [Test]
        public void RemoveAllEdgesNoEdgesTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            bool result = graph.RemoveAllEdges(x, y);

            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveAllEdgesMultipleArrowsTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            graph.AddArrow(x, y);
            graph.AddArrow(x, y);
            graph.AddArrow(x, y);

            bool result = graph.RemoveAllEdges(x, y);

            Assert.IsTrue(result);
            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
        }

        [Test]
        public void RemoveEdgesRemovedVerticesTest()
        {
            Assert.Fail();
        }

        [Test]
        public void AddAndRemoveIterationTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            int iterations = 5;
            bool result = false;

            for (int i = 0; i < iterations; i++)
            {
                graph.AddArrow(x, y);
                result |= graph.RemoveAllEdges(x, y);
                Assert.IsTrue(result);
            }

            this.AssertOnlyRelationshipsHaveBeenRemoved(graph, x, y);
        }

        private void AssertOnlyRelationshipsHaveBeenRemoved(Graph graph, Vertex first, Vertex second)
        {
            Assert.AreEqual(2, graph.Vertices.Count());
            Assert.AreEqual(first, graph.Vertices.ElementAt(0));
            Assert.AreEqual(second, graph.Vertices.ElementAt(1));

            Assert.AreEqual(0, first.GetSuccessors().Count());
            Assert.AreEqual(0, second.GetSuccessors().Count());
            Assert.AreEqual(0, first.GetArrowWeights(second).Count());
            Assert.AreEqual(0, second.GetArrowWeights(first).Count());
        }
    }
}

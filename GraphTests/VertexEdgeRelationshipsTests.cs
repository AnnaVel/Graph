using GraphCore;
using GraphCore.Edges;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestFixture]
    public class VertexEdgeRelationshipsTests
    {
        [Test]
        public void GetSuccessorsCorrectnessTest()
        {
            string[] expectedSuccessorValues = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            List<ExpectedVertexDescriptor> expectedPredecessors = new List<ExpectedVertexDescriptor>();

            foreach (string expectedSuccessorValue in expectedSuccessorValues)
            {
                Vertex successor = graph.GraphStructure.AddVertex(expectedSuccessorValue);
                graph.GraphStructure.AddArrow(mainVertex, successor);

                expectedPredecessors.Add(new ExpectedVertexDescriptor() { Value = successor.ValueAsObject });
            }

            IEnumerable<Vertex> resultSuccessors = mainVertex.GetSuccessors();
            this.AssertVerticesAreAsExpected(expectedPredecessors, resultSuccessors);

            resultSuccessors = graph.GraphStructure.GetVertexSuccessors(mainVertex).ToArray();
            this.AssertVerticesAreAsExpected(expectedPredecessors, resultSuccessors);
        }

        [Test]
        public void GetSuccessorsNoSuccessors()
        {
            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            Vertex[] resultSuccessors = mainVertex.GetSuccessors().ToArray();
            Assert.AreEqual(0, resultSuccessors.Count());

            resultSuccessors = graph.GraphStructure.GetVertexSuccessors(mainVertex).ToArray();
            Assert.AreEqual(0, resultSuccessors.Count());
        }

        [Test]
        public void GetSuccessorsUnnecessaryIterationTest()
        {
            TimeSpan expectedSpeed = TimeSpan.FromSeconds(0.001);

            string[] expectedSuccessorValues = new string[1000000];

            for (int i = 0; i < expectedSuccessorValues.Length; i++)
            {
                expectedSuccessorValues[i] = i.ToString();
            }

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            foreach (string expectedSuccessorValue in expectedSuccessorValues)
            {
                Vertex successor = graph.GraphStructure.AddVertex(expectedSuccessorValue);
                graph.GraphStructure.AddArrow(mainVertex, successor);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Vertex firstVertex = mainVertex.GetSuccessors().First();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [Test]
        public void GetSuccessorsVertexFromOtherStructure()
        {
            Graph firstGraph = new Graph();
            Vertex firstX = firstGraph.GraphStructure.AddVertex("x");

            Graph secondGraph = new Graph();
            Vertex secondX = secondGraph.GraphStructure.AddVertex("x");
            Vertex secondY = secondGraph.GraphStructure.AddVertex("y");
            secondGraph.GraphStructure.AddLine(secondX, secondY);

            Assert.Throws<ArgumentException>(() => secondGraph.GraphStructure.GetVertexSuccessors(firstX));
        }

        [Test]
        public void GetSuccessorsRemovedVertex()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y);
            graph.GraphStructure.RemoveVertex(x);

            Assert.Throws<InvalidOperationException>(() =>
            {
                x.GetSuccessors();
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.GraphStructure.GetVertexSuccessors(x);
            });
        }

        [Test]
        public void GetPredecessorsCorrectnessTest()
        {
            string[] predecessorValues = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            List<ExpectedVertexDescriptor> expectedPredecessors = new List<ExpectedVertexDescriptor>();

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            foreach (string expectedPredecessorValue in predecessorValues)
            {
                Vertex predecessor = graph.GraphStructure.AddVertex(expectedPredecessorValue);
                graph.GraphStructure.AddArrow(predecessor, mainVertex);

                expectedPredecessors.Add(new ExpectedVertexDescriptor() { Value = predecessor.ValueAsObject });
            }

            IEnumerable<Vertex> resultPredecessors = mainVertex.GetPredecessors();
            this.AssertVerticesAreAsExpected(expectedPredecessors, resultPredecessors);

            resultPredecessors = graph.GraphStructure.GetVertexPredecessors(mainVertex);
            this.AssertVerticesAreAsExpected(expectedPredecessors, resultPredecessors);
        }

        [Test]
        public void GetPredecessorsNoPredecessors()
        {
            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            IEnumerable<Vertex> resultPredecessors = mainVertex.GetPredecessors();
            Assert.AreEqual(0, resultPredecessors.Count());

            resultPredecessors = graph.GraphStructure.GetVertexPredecessors(mainVertex);
            Assert.AreEqual(0, resultPredecessors.Count());
        }

        [Test]
        public void GetPredecessorsVertexFromOtherStructure()
        {
            Graph firstGraph = new Graph();
            Vertex firstY = firstGraph.GraphStructure.AddVertex("y");

            Graph secondGraph = new Graph();
            Vertex secondX = secondGraph.GraphStructure.AddVertex("x");
            Vertex secondY = secondGraph.GraphStructure.AddVertex("y");
            secondGraph.GraphStructure.AddLine(secondX, secondY);

            Assert.Throws<ArgumentException>(() => secondGraph.GraphStructure.GetVertexPredecessors(firstY));
        }

        [Test]
        public void GetPredecessorsRemovedVertex()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y);
            graph.GraphStructure.RemoveVertex(y);

            Assert.Throws<InvalidOperationException>(() =>
            {
                y.GetPredecessors();
            });

            Assert.Throws<InvalidOperationException>(() =>
            {
                graph.GraphStructure.GetVertexPredecessors(y);
            });
        }

        [Test]
        public void GetOutgoingEdgesTest()
        {
            string[] successorValues = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            List<ExpectedEdgeDescriptor> expectedEdges = new List<ExpectedEdgeDescriptor>();

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            foreach (string successorValue in successorValues)
            {
                Vertex successor = graph.GraphStructure.AddVertex(successorValue);
                graph.GraphStructure.AddArrow(mainVertex, successor);

                expectedEdges.Add(new ExpectedEdgeDescriptor()
                {
                    FirstVertexValue = mainVertex.ValueAsObject,
                    SecondVertexValue = successor.ValueAsObject,
                    Value = null,
                    IsDirected = true
                });
            }

            IEnumerable<Edge> resultEdges = mainVertex.GetOutgoingEdges();
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);

            resultEdges = graph.GraphStructure.GetEdgesGoingOutOfVertex(mainVertex);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);
        }

        [Test]
        public void GetOutgoingEdgesSpeedTest()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.01);

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            for(int i = 0; i < 1000000; i++)
            {
                Vertex successor = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddArrow(mainVertex, successor);
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();
            IEnumerable<Edge> resultEdges = mainVertex.GetOutgoingEdges();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime);

            sw.Reset();

            sw.Start();
            resultEdges = graph.GraphStructure.GetEdgesGoingOutOfVertex(mainVertex);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime);
        }

        [Test]
        public void GetIncomingEdgesTest()
        {
            string[] predecessorValues = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            List<ExpectedEdgeDescriptor> expectedEdges = new List<ExpectedEdgeDescriptor>();

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            foreach (string predecessorValue in predecessorValues)
            {
                Vertex predecessor = graph.GraphStructure.AddVertex(predecessorValue);
                graph.GraphStructure.AddArrow(mainVertex, predecessor);

                expectedEdges.Add(new ExpectedEdgeDescriptor()
                {
                    FirstVertexValue = mainVertex.ValueAsObject,
                    SecondVertexValue = predecessor.ValueAsObject,
                    Value = null,
                    IsDirected = true
                });
            }

            IEnumerable<Edge> resultEdges = mainVertex.GetIncomingEdges();
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);

            resultEdges = graph.GraphStructure.GetEdgesComingIntoVertex(mainVertex);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);
        }

        [Test]
        public void GetIncomingEdgesSpeedTest()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.01);

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            for (int i = 0; i < 1000000; i++)
            {
                Vertex predecessor = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddArrow(predecessor, mainVertex);
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();
            IEnumerable<Edge> resultEdges = mainVertex.GetIncomingEdges();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime);

            sw.Reset();

            sw.Start();
            resultEdges = graph.GraphStructure.GetEdgesComingIntoVertex(mainVertex);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime);
        }

        [Test]
        public void GetEdgesTo()
        {
            List<ExpectedEdgeDescriptor> expectedEdges = new List<ExpectedEdgeDescriptor>();

            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y);
            expectedEdges.Add(new ExpectedEdgeDescriptor() 
            { 
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = null 
            });

            graph.GraphStructure.AddArrow(x, y, 1.5);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = true,
                Value = 1.5
            });

            graph.GraphStructure.AddLine(x, y, graph);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = graph
            });

            graph.GraphStructure.AddArrow(y, x);

            IEnumerable<Edge> resultEdges = x.GetEdgesTo(y);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);

            resultEdges = graph.GraphStructure.GetEdgesLeadingFromTo(x, y);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);
        }

        [Test]
        public void GetEdgesToVertexFromAnotherGraph()
        {
            Graph firstGraph = new Graph();
            Vertex firstX = firstGraph.GraphStructure.AddVertex("x");

            Graph secondGraph = new Graph();
            Vertex secondX = secondGraph.GraphStructure.AddVertex("x");
            Vertex secondY = secondGraph.GraphStructure.AddVertex("y");

            Assert.Throws<ArgumentException>(() => { firstX.GetEdgesTo(secondY); });
            Assert.Throws<ArgumentException>(() => { firstGraph.GraphStructure.GetEdgesLeadingFromTo(firstX, secondY); });
        }

        [Test]
        public void GetEdgesToNonSuccessor()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            IEnumerable<Edge> resultEdges = x.GetEdgesTo(y);
            Assert.AreEqual(0, resultEdges.Count());

            resultEdges = graph.GraphStructure.GetEdgesLeadingFromTo(x, y);
            Assert.AreEqual(0, resultEdges.Count());
        }

        [Test]
        public void GetEdgesFrom()
        {
            List<ExpectedEdgeDescriptor> expectedEdges = new List<ExpectedEdgeDescriptor>();

            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = null
            });

            graph.GraphStructure.AddArrow(x, y, 1.5);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = true,
                Value = 1.5
            });

            graph.GraphStructure.AddLine(x, y, graph);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = graph
            });

            graph.GraphStructure.AddArrow(y, x);

            IEnumerable<Edge> resultEdges = y.GetEdgesFrom(x);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);

            resultEdges = graph.GraphStructure.GetEdgesLeadingFromTo(x, y);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);
        }

        [Test]
        public void GetEdgesFromVertexFromAnotherGraph()
        {
            Graph firstGraph = new Graph();
            Vertex firstX = firstGraph.GraphStructure.AddVertex("x");

            Graph secondGraph = new Graph();
            Vertex secondX = secondGraph.GraphStructure.AddVertex("x");
            Vertex secondY = secondGraph.GraphStructure.AddVertex("y");

            Assert.Throws<ArgumentException>(() => { secondY.GetEdgesFrom(firstX); });
            Assert.Throws<ArgumentException>(() => { secondGraph.GraphStructure.GetEdgesLeadingFromTo(firstX, secondY); });
        }

        [Test]
        public void GetEdgesFromNonPredecessor()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            IEnumerable<Edge> resultEdges = y.GetEdgesFrom(x);
            Assert.AreEqual(0, resultEdges.Count());

            resultEdges = graph.GraphStructure.GetEdgesLeadingFromTo(x, y);
            Assert.AreEqual(0, resultEdges.Count());
        }

        [Test]
        public void GetEdgesBetweenTest()
        {
            List<ExpectedEdgeDescriptor> expectedEdges = new List<ExpectedEdgeDescriptor>();

            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            graph.GraphStructure.AddLine(x, y);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = null
            });

            graph.GraphStructure.AddArrow(x, y, 1.5);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = true,
                Value = 1.5
            });

            graph.GraphStructure.AddLine(x, y, graph);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = x.ValueAsObject,
                SecondVertexValue = y.ValueAsObject,
                IsDirected = false,
                Value = graph
            });

            graph.GraphStructure.AddArrow(y, x);
            expectedEdges.Add(new ExpectedEdgeDescriptor()
            {
                FirstVertexValue = y.ValueAsObject,
                SecondVertexValue = x.ValueAsObject,
                IsDirected = true,
                Value = null
            });

            IEnumerable<Edge> resultEdges = graph.GraphStructure.GetEdgesBetween(x, y);
            this.AssertEdgesAreAsExpected(expectedEdges, resultEdges);
        }

        [Test]
        public void GetEdgesBetweenNonNeighbours()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            IEnumerable<Edge> resultEdges = graph.GraphStructure.GetEdgesBetween(x, y);
            Assert.AreEqual(0, resultEdges.Count());
        }

        [Test]
        public void GetEdgseBetweenDifferentGraphVertices()
        {
            Graph firstGraph = new Graph();
            Vertex firstX = firstGraph.GraphStructure.AddVertex("x");

            Graph secondGraph = new Graph();
            Vertex secondX = secondGraph.GraphStructure.AddVertex("x");
            Vertex secondY = secondGraph.GraphStructure.AddVertex("y");

            Assert.Throws<ArgumentException>(() => { secondGraph.GraphStructure.GetEdgesBetween(firstX, secondY); });
        }

        private void AssertVerticesAreAsExpected(IEnumerable<ExpectedVertexDescriptor> expectedVertices, IEnumerable<Vertex> actualVertices)
        {
            Assert.AreEqual(expectedVertices.Count(), actualVertices.Count());

            for (int i = 0; i < expectedVertices.Count(); i++)
            {
                ExpectedVertexDescriptor expectedVertex = expectedVertices.ElementAt(i);
                Vertex resultVertex = actualVertices.ElementAt(i);

                Assert.AreEqual(expectedVertex.Value, resultVertex.ValueAsObject);
            }
        }

        private void AssertEdgesAreAsExpected(IEnumerable<ExpectedEdgeDescriptor> expectedEdges, IEnumerable<Edge> actualEdges)
        {
            Assert.AreEqual(expectedEdges.Count(), actualEdges.Count());

            for (int i = 0; i < expectedEdges.Count(); i++)
            {
                ExpectedEdgeDescriptor expectedEdge = expectedEdges.ElementAt(i);
                Edge resultEdge = actualEdges.ElementAt(i);

                Assert.AreEqual(expectedEdge.Value, resultEdge.ValueAsObject);
            }
        }
    }
}

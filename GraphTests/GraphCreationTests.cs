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
    public class GraphCreationTests
    {
        [Test]
        public void CreateTwoVertextWeightedDirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddArrow(x, y, 1);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = true, Value = 1}
            },
            graph);
        }

        [Test]
        public void CreateTwoVertextWeightedUndirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y, 1);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = false, Value = 1}
            },
            graph);
        }

        [Test]
        public void CreateTwoVertextUnweightedDirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddArrow(x, y);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = true, Value = null}
            },
            graph);
        }

        [Test]
        public void CreateTwoVertextUnweightedUndirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddLine(x, y);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = false, Value = null}
            },
            graph);
        }

        [Test]
        public void CreateCircularGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Vertex z = graph.GraphStructure.AddVertex("z");
            graph.GraphStructure.AddArrow(x, y);
            graph.GraphStructure.AddArrow(y, z);
            graph.GraphStructure.AddArrow(z, x);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
                new ExpectedVertexDescriptor() { Value = "z"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = true, Value = null},
                new ExpectedEdgeDescriptor() { FirstVertexValue = "y", SecondVertexValue = "z", IsDirected = true, Value = null},
                new ExpectedEdgeDescriptor() { FirstVertexValue = "z", SecondVertexValue = "x", IsDirected = true, Value = null},
            },
            graph);
        }

        [Test]
        public void CreateDisconnectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddArrow(x, y);
            Vertex z = graph.GraphStructure.AddVertex("z");
            Vertex t = graph.GraphStructure.AddVertex("t");
            graph.GraphStructure.AddArrow(z, t);

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
                new ExpectedVertexDescriptor() { Value = "z"},
                new ExpectedVertexDescriptor() { Value = "t"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = true, Value = null},
                new ExpectedEdgeDescriptor() { FirstVertexValue = "z", SecondVertexValue = "t", IsDirected = true, Value = null}
            },
            graph);
        }

        [Test]
        public void CreateSingleVertexGraph()
        {
            Graph graph = new Graph();
            graph.GraphStructure.AddVertex("x");

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"}
            },
            new ExpectedEdgeDescriptor[0],
            graph);
        }

        [Test]
        public void CreateEmptyGraph()
        {
            Graph graph = new Graph();

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[0], new ExpectedEdgeDescriptor[0], graph);
        }

        [Test]
        public void CreateMixedUnweightedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            graph.GraphStructure.AddArrow(x, y);
            Vertex z = graph.GraphStructure.AddVertex("z");
            graph.GraphStructure.AddLine(x, z);
            Vertex v = graph.GraphStructure.AddVertex("v");

            this.AssertExpectedGraph(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor() { Value = "x"},
                new ExpectedVertexDescriptor() { Value = "y"},
                new ExpectedVertexDescriptor() { Value = "z"},
                new ExpectedVertexDescriptor() { Value = "v"},
            },
            new ExpectedEdgeDescriptor[]
            {
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "y", IsDirected = true, Value = null},
                new ExpectedEdgeDescriptor() { FirstVertexValue = "x", SecondVertexValue = "z", IsDirected = false, Value = null}
            },
            graph);
        }

        [Test]
        public void CreateGraphWithVerticesWithDuplicatingValues()
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");

            Assert.Throws<InvalidOperationException>(() => graph.GraphStructure.AddVertex("x"));
        }

        private void AssertExpectedGraph(ExpectedVertexDescriptor[] expectedVertices, ExpectedEdgeDescriptor[] expectedEdges, Graph actualGraph)
        {
            Vertex[] resultVertices = actualGraph.GraphStructure.Vertices.ToArray();
            this.AssertVertices(expectedVertices, resultVertices);

            Edge[] resultEdges = actualGraph.GraphStructure.Edges.ToArray();
            this.AssertEdges(expectedEdges, resultEdges);
        }

        private void AssertVertices(ExpectedVertexDescriptor[] expectedVertices, Vertex[] resultVertices)
        {
            Assert.AreEqual(resultVertices.Length, expectedVertices.Length);

            for (int i = 0; i < expectedVertices.Length; i++)
            {
                ExpectedVertexDescriptor expectedVertex = expectedVertices[i];
                Vertex resultVertex = resultVertices[i];

                Assert.AreEqual(expectedVertex.Value, resultVertex.ValueAsObject);
            }
        }

        private void AssertEdges(ExpectedEdgeDescriptor[] expectedEdges, Edge[] resultEdges)
        {
            Assert.AreEqual(resultEdges.Length, expectedEdges.Length);

            for (int i = 0; i < expectedEdges.Length; i++)
            {
                ExpectedEdgeDescriptor expectedEdge = expectedEdges[i];
                Edge resultEdge = resultEdges[i];

                Assert.AreEqual(expectedEdge.Value, resultEdge.ValueAsObject);
                Assert.AreEqual(expectedEdge.FirstVertexValue, resultEdge.FirstVertex.ValueAsObject);
                Assert.AreEqual(expectedEdge.SecondVertexValue, resultEdge.SecondVertex.ValueAsObject);
                Assert.AreEqual(expectedEdge.IsDirected, resultEdge.IsDirected);
            }
        }
    }
}

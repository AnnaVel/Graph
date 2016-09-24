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
    public class GraphCreationTests
    {
        [Test]
        public void CreateTwoVertextWeightedDirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y, 1);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", 1},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>())
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateTwoVertextWeightedUndirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddLine(x, y, 1);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", 1},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>()
                {
                    {"x", 1},
                })
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateTwoVertextUnweightedDirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>())
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateTwoVertextUnweightedUndirectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddLine(x, y);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>() 
                {
                    {"x", null},
                })
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateCircularGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            Vertex z = graph.AddVertex("z");
            graph.AddArrow(x, y);
            graph.AddArrow(y, z);
            graph.AddArrow(z, x);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>() 
                {
                    {"z", null},
                }),
                new ExpectedVertexDescriptor("z", new Dictionary<object,double?>() 
                {
                    {"x", null},
                })
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateDisconnectedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            Vertex z = graph.AddVertex("z");
            Vertex t = graph.AddVertex("t");
            graph.AddArrow(z, t);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>()),
                new ExpectedVertexDescriptor("z", new Dictionary<object,double?>() 
                {
                    {"t", null},
                }),
                new ExpectedVertexDescriptor("t", new Dictionary<object,double?>()),
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateSingleVertexGraph()
        {
            Graph graph = new Graph();
            graph.AddVertex("x");

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>())
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateEmptyGraph()
        {
            Graph graph = new Graph();

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[0], graph.Vertices.ToArray());
        }

        [Test]
        public void CreateMixedUnweightedGraph()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            Vertex z = graph.AddVertex("z");
            graph.AddLine(x, z);
            Vertex v = graph.AddVertex("v");

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<object,double?>()
                {
                    {"y", null},
                    {"z", null}
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<object,double?>()),
                new ExpectedVertexDescriptor("z", new Dictionary<object,double?>()
                {
                    {"x", null}
                }),
                new ExpectedVertexDescriptor("v", new Dictionary<object,double?>())
            },
            graph.Vertices.ToArray());
        }

        [Test]
        public void CreateGraphWithVerticesWithDuplicatingValues()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");

            Assert.Throws<InvalidOperationException>(() => graph.AddVertex("x"));
        }

        private void AssertExpectedVertices(ExpectedVertexDescriptor[] expectedVertices, Vertex[] resultVertices)
        {
            Assert.AreEqual(resultVertices.Length, expectedVertices.Length);

            for (int i = 0; i < expectedVertices.Length; i++)
            {
                ExpectedVertexDescriptor expectedVertex = expectedVertices[i];
                Vertex resultVertex = resultVertices[i];

                this.AssertExpectedVertex(expectedVertex, resultVertex);
            }
        }

        private void AssertExpectedVertex(ExpectedVertexDescriptor expectedVertex, Vertex resultVertex)
        {
            Assert.AreSame(expectedVertex.Value, resultVertex.ValueAsObject);

            KeyValuePair<object, double?>[] expectedSuccessorValuesAndWeights = expectedVertex.SuccessorNameToWeight.ToArray();
            Vertex[] resultSuccessors = resultVertex.GetSuccessors().ToArray();

            Assert.AreEqual(expectedSuccessorValuesAndWeights.Length, resultSuccessors.Length);

            for (int i = 0; i < expectedSuccessorValuesAndWeights.Length; i++)
            {
                object expectedSuccessorValue = expectedSuccessorValuesAndWeights[i].Key;
                double? expectedSuccessorWeight = expectedSuccessorValuesAndWeights[i].Value;
                object resultSuccessorValue = resultSuccessors[i].ValueAsObject;
                double? resultSuccessorWeight = resultVertex.GetMinArrowWeight(resultSuccessors[i]);

                Assert.AreEqual(expectedSuccessorValue, resultSuccessorValue);
                Assert.AreEqual(expectedSuccessorWeight, resultSuccessorWeight);
            }
        }
    }
}

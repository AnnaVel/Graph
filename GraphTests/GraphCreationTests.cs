using GraphCore;
using GraphCore.StructureDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestClass]
    public class GraphCreationTests
    {
        [TestMethod]
        public void CreateTwoVertextWeightedDirectedGraph()
        {
            ArrowDescriptor arrow = new ArrowDescriptor("x", "y", 1);

            Graph graph = new Graph(arrow);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", 1},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>())
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateTwoVertextWeightedUndirectedGraph()
        {
            EdgeDescriptor singleEdge = new EdgeDescriptor("x", "y", 1);

            Graph graph = new Graph(singleEdge);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", 1},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>()
                {
                    {"x", 1},
                })
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateTwoVertextUnweightedDirectedGraph()
        {
            ArrowDescriptor arrow = new ArrowDescriptor("x", "y");

            Graph graph = new Graph(arrow);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>())
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateTwoVertextUnweightedUndirectedGraph()
        {
            EdgeDescriptor singleEdge = new EdgeDescriptor("x", "y");

            Graph graph = new Graph(singleEdge);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>() 
                {
                    {"x", null},
                })
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateCircularGraph()
        {
            ArrowDescriptor firstArrow = new ArrowDescriptor("x", "y");
            ArrowDescriptor secondArrow = new ArrowDescriptor("y", "z");
            ArrowDescriptor thirdArrow = new ArrowDescriptor("z", "x");

            Graph graph = new Graph(firstArrow, secondArrow, thirdArrow);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>() 
                {
                    {"z", null},
                }),
                new ExpectedVertexDescriptor("z", new Dictionary<string,double?>() 
                {
                    {"x", null},
                })
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateDisconnectedGraph()
        {
            ArrowDescriptor firstArrow = new ArrowDescriptor("x", "y");
            ArrowDescriptor secondArrow = new ArrowDescriptor("z", "t");

            Graph graph = new Graph(firstArrow, secondArrow);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>() 
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>()),
                new ExpectedVertexDescriptor("z", new Dictionary<string,double?>() 
                {
                    {"t", null},
                }),
                new ExpectedVertexDescriptor("t", new Dictionary<string,double?>()),
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateSingleVertexGraph()
        {
            VertexDescriptor singleVertex = new VertexDescriptor("x");

            Graph graph = new Graph(singleVertex);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>())
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateEmptyGraph()
        {
            Graph graph = new Graph();

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[0], graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateMixedUnweightedGraph()
        {
            ArrowDescriptor arrow = new ArrowDescriptor("x", "y");
            EdgeDescriptor edge = new EdgeDescriptor("x", "z");
            VertexDescriptor separateVertex = new VertexDescriptor("v");

            Graph graph = new Graph(arrow, edge, separateVertex);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>()
                {
                    {"y", null},
                    {"z", null}
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>()),
                new ExpectedVertexDescriptor("z", new Dictionary<string,double?>()
                {
                    {"x", null}
                }),
                new ExpectedVertexDescriptor("v", new Dictionary<string,double?>())
            },
            graph.Vertices.ToArray());
        }

        [TestMethod]
        public void CreateGraphWithContradictingDescriptors()
        {
            ArrowDescriptor arrow = new ArrowDescriptor("x", "y");
            VertexDescriptor separateVertex = new VertexDescriptor("x");

            Graph graph = new Graph(arrow, separateVertex);

            this.AssertExpectedVertices(new ExpectedVertexDescriptor[]
            {
                new ExpectedVertexDescriptor("x", new Dictionary<string,double?>()
                {
                    {"y", null},
                }),
                new ExpectedVertexDescriptor("y", new Dictionary<string,double?>()),
            },
            graph.Vertices.ToArray());
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
            Assert.AreSame(expectedVertex.Name, resultVertex.Name);

            KeyValuePair<string, double?>[] expectedSuccessorNamesAndWeights = expectedVertex.SuccessorNameToWeight.ToArray();
            Vertex[] resultSuccessors = resultVertex.GetSuccessors().ToArray();

            Assert.AreEqual(expectedSuccessorNamesAndWeights.Length, resultSuccessors.Length);

            for (int i = 0; i < expectedSuccessorNamesAndWeights.Length; i++)
            {
                string expectedSuccessorName = expectedSuccessorNamesAndWeights[i].Key;
                double? expectedSuccessorWeight = expectedSuccessorNamesAndWeights[i].Value;
                string resultSuccessorName = resultSuccessors[i].Name;
                double? resultSuccessorWeight = resultVertex.GetArrowWeight(resultSuccessors[i]);

                Assert.AreEqual(expectedSuccessorName, resultSuccessorName);
                Assert.AreEqual(expectedSuccessorWeight, resultSuccessorWeight);
            }
        }
    }
}

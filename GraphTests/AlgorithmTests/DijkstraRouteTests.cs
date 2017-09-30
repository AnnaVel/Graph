using GraphCore;
using GraphCore.Algorithms;
using GraphCore.DynamicAttributes;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphTests.AlgorithmTests
{
    [TestFixture]
    public class DijkstraRouteTests
    {
        [Test]
        public void AssertParameterAndResultTypes()
        {
            Graph graph = new Graph();

            IAlgorithm algorithm = graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraRouteAlgorithmName);

            Assert.AreEqual(typeof(DijkstraRouteResult), algorithm.ResultType);
            Assert.AreEqual(typeof(DijkstraRouteParameter), algorithm.ParameterType);
        }

        [Test]
        public void BasicScenario()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);
            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));

            double expectedTotalDistance = 20;
            bool expectedResultIsValid = true;
            List<Vertex> expectedPath = new List<Vertex>()
            {
                graph.GraphStructure.GetVertexByValue(1),
                graph.GraphStructure.GetVertexByValue(3),
                graph.GraphStructure.GetVertexByValue(6),
                graph.GraphStructure.GetVertexByValue(5),
            };

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

        [Test]
        public void NegativeEdge()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex three = graph.GraphStructure.GetVertexByValue(3);
            Vertex four = graph.GraphStructure.GetVertexByValue(4);
            graph.GraphStructure.AddLine(three, four, -11);

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);

            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));

            double expectedTotalDistance = 4;
            bool expectedResultIsValid = false;
            List<Vertex> expectedPath = new List<Vertex>()
            {
                graph.GraphStructure.GetVertexByValue(1),
                graph.GraphStructure.GetVertexByValue(3),
                graph.GraphStructure.GetVertexByValue(4),
                graph.GraphStructure.GetVertexByValue(5),
            };

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

#if !TRAVISENVIRONMENT
#if !APPVEYORENVIRONMENT
        [Test]
        public void OneMillionVerticesInPathStressTest()
        {
            Graph graph = TestHelper.DefinePathShapedBigGraph();

            Vertex startVertex = graph.GraphStructure.Vertices.First();
            Vertex endVertex = graph.GraphStructure.Vertices.Last();

            double expectedSeconds = 5.5;
            long expectedMemory = 200000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(
                    AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(startVertex, endVertex));
            },
            TimeSpan.FromSeconds(expectedSeconds), expectedMemory);
        }

        [Test]
        public void OneMillionVerticesStarShapedStressTest()
        {
            // For this test the graph has one central vertex with a hundred neighbours
            // and each of them has a hundred neighbours and each of those has a hundred neighbours.
            // Therefore the vertices are 1 + 100 + 10000 + 1000000. The end vertex is the one added last.

            Graph graph = TestHelper.DefineStarShapedBigGraph();

            Vertex startVertex = graph.GraphStructure.Vertices.First();
            Vertex endVertex = graph.GraphStructure.Vertices.Last();

            int expectedSeconds = 15;
            long expectedMemory = 210000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(
                    AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(startVertex, endVertex));
            },
            TimeSpan.FromSeconds(expectedSeconds), expectedMemory);
        }
#endif
#endif

        [Test]
        public void DisconnectedGraph()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex seven = graph.GraphStructure.AddVertex(7);
            Vertex one = graph.GraphStructure.GetVertexByValue(1);

            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, seven));

            double expectedTotalDistance = double.PositiveInfinity;
            bool expectedResultIsValid = true;
            List<Vertex> expectedPath = new List<Vertex>();

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

        [Test]
        public void SetDynamicAttributesTest()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraRouteAlgorithmName).SetDynamicAttributesInStructure = true;
            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);
            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));

            Vertex two = graph.GraphStructure.GetVertexByValue(2);
            Vertex three = graph.GraphStructure.GetVertexByValue(3);
            Vertex four = graph.GraphStructure.GetVertexByValue(4);
            Vertex six = graph.GraphStructure.GetVertexByValue(6);

            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, one, true, null, 0);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, two, true, null, 7);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, three, true, null, 9);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, four, true, null, 20);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, five, null, null, 20);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, six, true, null, 11);
        }

        [Test]
        public void DoNotSetDynamicAttributesTest()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);
            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));

            Vertex two = graph.GraphStructure.GetVertexByValue(2);
            Vertex three = graph.GraphStructure.GetVertexByValue(3);
            Vertex four = graph.GraphStructure.GetVertexByValue(4);
            Vertex six = graph.GraphStructure.GetVertexByValue(6);

            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, one);
            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, two);
            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, three);
            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, four);
            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, five);
            TestHelper.AssertDijkstraAttributesForVertexAreNotSet(graph, six);
        }

        [Test]
        public void UnweightedEdgesTest()
        {
            Graph graph = TestHelper.DefineSimpleUnweightedGraph();

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex four = graph.GraphStructure.GetVertexByValue(4);
            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, four));

            double expectedTotalDistance = 2;
            bool expectedResultIsValid = true;
            List<Vertex> expectedPath = new List<Vertex>()
            {
                graph.GraphStructure.GetVertexByValue(1),
                graph.GraphStructure.GetVertexByValue(2),
                graph.GraphStructure.GetVertexByValue(4),
            };

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

        [Test]
        public void StartVertexAndEndVertexCoincide()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex startEndVertex = graph.GraphStructure.Vertices.First();

            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(startEndVertex, startEndVertex));

            double expectedTotalDistance = 0;
            bool expectedResultIsValid = true;
            List<Vertex> expectedPath = new List<Vertex>();

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

        [Test]
        public void GraphHasVertexWithSelfPointingCycle()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex three = graph.GraphStructure.GetVertexByValue(3);
            graph.GraphStructure.AddLine(three, three, 10);

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);
            DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraRouteResult>(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));

            double expectedTotalDistance = 20;
            bool expectedResultIsValid = true;
            List<Vertex> expectedPath = new List<Vertex>()
            {
                graph.GraphStructure.GetVertexByValue(1),
                graph.GraphStructure.GetVertexByValue(3),
                graph.GraphStructure.GetVertexByValue(6),
                graph.GraphStructure.GetVertexByValue(5),
            };

            Assert.AreEqual(expectedTotalDistance, result.TotalDistance);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedPath, result.ShortestPath);
        }

        [Test]
        public void ExecuteAlgorithmWithWrongParameter()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.GetVertexByValue(1);
            Vertex five = graph.GraphStructure.GetVertexByValue(5);

            Assert.Throws<ArgumentException>(() =>
           {
               DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraRouteResult>(
                   AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraParameter(one));
           });

            Assert.Throws<ArgumentException>(() =>
            {
                DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraCompleteTraversalResult>(
                    AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteParameter(one, five));
            });
        }
    }
}

using GraphCore;
using GraphCore.Algorithms;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests.AlgorithmTests
{
    [TestFixture]
    public class DijkstraCompleteTraversalTests
    {
        [Test]
        public void AssertParameterAndResultTypes()
        {
            Graph graph = new Graph();

            IAlgorithm algorithm = graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraCompleteTraversalAlgorithmName);

            Assert.AreEqual(typeof(DijkstraCompleteTraversalResult), algorithm.ResultType);
            Assert.AreEqual(typeof(DijkstraParameter), algorithm.ParameterType);
        }

        [Test]
        public void BasicScenario()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            Dictionary<Vertex, double> expectedDistances = new Dictionary<Vertex, double>()
            {
                { one, 0 },
                { two, 7 },
                { three, 9 },
                { four, 20 },
                { five, 20 },
                { six, 11 },
            };
            bool expectedResultIsValid = true;

            Assert.AreEqual(expectedDistances, result.DistancesFromStart);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
        }

        [Test]
        public void NegativeEdge()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex three = graph.GraphStructure.Vertices.First(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.First(v => v.ValueAsObject.Equals(4));
            graph.GraphStructure.AddLine(three, four, -11);

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault((v) => { return v.ValueAsObject.Equals(1); });

            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            bool expectedResultIsValid = false;
            Dictionary<Vertex, double> expectedDistances = new Dictionary<Vertex, double>()
            {
                { one, 0 },
                { two, 7 },
                { three, 9 },
                { four, -2 },
                { five, 4 },
                { six, 11 },
            };

            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedDistances, result.DistancesFromStart);
        }

#if !TRAVISENVIRONMENT
        [Test, Category("StressTest")]
        public void OneMillionVerticesInPathStressTest()
        {
            Graph graph = TestHelper.DefinePathShapedBigGraph();

            Vertex startVertex = graph.GraphStructure.Vertices.First();

            int expectedSeconds = 5;
            long expectedMemory = 200000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                    AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(startVertex));
            },
            TimeSpan.FromSeconds(expectedSeconds), expectedMemory);
        }

        [Test, Category("StressTest")]
        public void OneMillionVerticesStarShapedStressTest()
        {
            // For this test the graph has one central vertex with a hundred neighbours
            // and each of them has a hundred neighbours and each of those has a hundred neighbours.
            // Therefore the vertices are 1 + 100 + 10000 + 1000000. The end vertex is the one added last.

            Graph graph = TestHelper.DefineStarShapedBigGraph();

            Vertex startVertex = graph.GraphStructure.Vertices.First();

            int expectedSeconds = 15;
            long expectedMemory = 240000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                    AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(startVertex));
            },
            TimeSpan.FromSeconds(expectedSeconds), expectedMemory);
        }
#endif

        [Test]
        public void DisconnectedGraph()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex seven = graph.GraphStructure.AddVertex(7);
            Vertex one = graph.GraphStructure.Vertices.First(v => v.ValueAsObject.Equals(1));

            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            // Vertex seven is not included in the result, as vertices that are not in the collection are assumed to be at infinity distance from the start.
            Dictionary<Vertex, double> expectedDistances = new Dictionary<Vertex, double>()
            {
                { one, 0 },
                { two, 7 },
                { three, 9 },
                { four, 20 },
                { five, 20 },
                { six, 11 }
            };
            bool expectedResultIsValid = true;

            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedDistances, result.DistancesFromStart);
        }

        [Test]
        public void SetDynamicAttributesTest()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraCompleteTraversalAlgorithmName).SetDynamicAttributesInStructure = true;
            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, one, true, null, 0);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, two, true, null, 7);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, three, true, null, 9);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, four, true, null, 20);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, five, true, null, 20);
            TestHelper.AssertDijkstraAttributeValuesForVertexAreSet(graph, six, true, null, 11);
        }

        [Test]
        public void DoNotSetDynamicAttributesTest()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));

            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

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

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            Dictionary<Vertex, double> expectedDistances = new Dictionary<Vertex, double>()
            {
                { one, 0 },
                { two, 1 },
                { six, 1 },
                { three, 1 },
                { four, 2 },
                { five, 2 }
            };
            bool expectedResultIsValid = true;

            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
            Assert.AreEqual(expectedDistances, result.DistancesFromStart);
        }

        [Test]
        public void GraphHasVertexWithSelfPointingCycle()
        {
            Graph graph = TestHelper.DefineSimpleGraph();
            Vertex three = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(3));
            graph.GraphStructure.AddLine(three, three, 10);

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraCompleteTraversalResult>(
                AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));

            Vertex two = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(2));
            Vertex four = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(4));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));
            Vertex six = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(6));

            Dictionary<Vertex, double> expectedDistances = new Dictionary<Vertex, double>()
            {
                { one, 0 },
                { two, 7 },
                { three, 9 },
                { four, 20 },
                { five, 20 },
                { six, 11 },
            };
            bool expectedResultIsValid = true;

            Assert.AreEqual(expectedDistances, result.DistancesFromStart);
            Assert.AreEqual(expectedResultIsValid, result.ResultIsValid);
        }

        [Test]
        public void ExecuteAlgorithmWithWrongParameter()
        {
            Graph graph = TestHelper.DefineSimpleGraph();

            Vertex one = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(1));
            Vertex five = graph.GraphStructure.Vertices.FirstOrDefault(v => v.ValueAsObject.Equals(5));

            // The DijkstraRouteParameter is a subclass of the DijkstraParameter, so it will do, although providing more information than is necessary.
            Assert.DoesNotThrow(() =>
            {
                DijkstraCompleteTraversalResult result = graph.AlgorithmLibrary.Execute<DijkstraRouteParameter, DijkstraCompleteTraversalResult>(
                    AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraRouteParameter(one, five));
            });

            Assert.Throws<ArgumentException>(() =>
            {
                DijkstraRouteResult result = graph.AlgorithmLibrary.Execute<DijkstraParameter, DijkstraRouteResult>(
                    AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraParameter(one));
            });
        }
    }
}

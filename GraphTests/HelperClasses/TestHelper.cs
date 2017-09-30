using GraphCore;
using GraphCore.Algorithms;
using GraphCore.DynamicAttributes;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GraphTests
{
    internal static class TestHelper
    {
        public static void AssertEnumerablesAreEqual<T>(IEnumerable<T> expectedEnum, IEnumerable<T> actualEnum)
        {
            Assert.AreEqual(expectedEnum.Count(), actualEnum.Count());

            for (int i = 0; i < expectedEnum.Count(); i++)
            {
                Assert.AreEqual(expectedEnum.ElementAt(i), actualEnum.ElementAt(i));
            }
        }

        public static void AssertPerformanceAndMemory(Action action, TimeSpan expectedDuration, long expectedMemoryInKbs)
        {
            int bytesToKb = 1024;

            long memoryBefore = GC.GetTotalMemory(true);

            Stopwatch sw = new Stopwatch();

            sw.Start();

            action();

            sw.Stop();

            long memoryAfter = GC.GetTotalMemory(false);

            long memoryUsageInKb = (memoryAfter - memoryBefore) / bytesToKb;

            Assert.IsTrue(sw.Elapsed < expectedDuration, string.Format("Elapsed time was: {0} expected time was: {1}", sw.Elapsed, expectedDuration));
            Assert.IsTrue(memoryUsageInKb < expectedMemoryInKbs, string.Format("Used memory was: {0}kb, expected memory was: {1}kb", memoryUsageInKb, expectedMemoryInKbs));
            Assert.Pass(string.Format(@"Elapsed time was: {0},
expected time was: {1};
Used memory was: {2}kb,
expected memory was: {3}kb",
                         sw.Elapsed, expectedDuration, memoryUsageInKb, expectedMemoryInKbs));
        }

        public static void AssertDijkstraAttributeValuesForVertexAreSet(Graph graph, Vertex vertex, bool? expectedIsVisistedValue,
    bool? expectedIsCurrentValue, double? expectedDistanceValue)
        {
            IAlgorithm dijkstraAlgorithm = graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraRouteAlgorithmName);
            string isVisitedAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.First();
            string isCurrentAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.ElementAt(1);
            string distanceAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.ElementAt(2);

            AssertAttributeValueForVertexIsSet(graph, vertex, isVisitedAttributeName, expectedIsVisistedValue);
            AssertAttributeValueForVertexIsSet(graph, vertex, isCurrentAttributeName, expectedIsCurrentValue);
            AssertAttributeValueForVertexIsSet(graph, vertex, distanceAttributeName, expectedDistanceValue);
        }

        public static void AssertDijkstraAttributesForVertexAreNotSet(Graph graph, Vertex vertex)
        {
            IAlgorithm dijkstraAlgorithm = graph.AlgorithmLibrary.GetAlgorithm(AlgorithmNames.DijkstraRouteAlgorithmName);
            string isVisitedAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.First();
            string isCurrentAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.ElementAt(1);
            string distanceAttributeName = dijkstraAlgorithm.ReservedDynamicAttributeNames.ElementAt(2);

            AssertAttributeValueForVertexIsNotSet(graph, vertex, isVisitedAttributeName);
            AssertAttributeValueForVertexIsNotSet(graph, vertex, isCurrentAttributeName);
            AssertAttributeValueForVertexIsNotSet(graph, vertex, distanceAttributeName);
        }

        public static void AssertAttributeValueForVertexIsSet(Graph graph, Vertex vertex, string attributeName, object expectedAttributeValue)
        {
            IDynamicAttribute attribute = vertex.GetDynamicAttribute(attributeName);

            if (attribute == null)
            {
                Assert.AreEqual(expectedAttributeValue, attribute,
                    string.Format("Attribute was null while the expected was {0}. Vertex: {1} Attribute name: {2}", expectedAttributeValue, vertex.ValueAsObject, attributeName));
            }
            else
            {
                Assert.AreEqual(expectedAttributeValue, attribute.ValueAsObject,
                    string.Format("Attribute was {0} while the expected was {1}. Vertex: {2} Attribute name: {3}", attribute.ValueAsObject, expectedAttributeValue, vertex.ValueAsObject, attributeName));
            }
        }

        public static void AssertAttributeValueForVertexIsNotSet(Graph graph, Vertex vertex, string attributeName)
        {
            IDynamicAttribute attribute = vertex.GetDynamicAttribute(attributeName);
            Assert.IsNull(attribute);
        }

        public static Graph DefineSimpleUnweightedGraph()
        {
            Graph graph = new Graph();

            Vertex one = graph.GraphStructure.AddVertex(1);
            Vertex two = graph.GraphStructure.AddVertex(2);
            Vertex three = graph.GraphStructure.AddVertex(3);
            Vertex four = graph.GraphStructure.AddVertex(4);
            Vertex five = graph.GraphStructure.AddVertex(5);
            Vertex six = graph.GraphStructure.AddVertex(6);

            graph.GraphStructure.AddLine(one, two);
            graph.GraphStructure.AddLine(two, four);
            graph.GraphStructure.AddLine(four, five);
            graph.GraphStructure.AddLine(five, six);
            graph.GraphStructure.AddLine(six, one);
            graph.GraphStructure.AddLine(one, three);
            graph.GraphStructure.AddLine(six, three);
            graph.GraphStructure.AddLine(two, three);
            graph.GraphStructure.AddLine(four, three);

            return graph;
        }

        public static Graph DefineSimpleGraph()
        {
            Graph graph = new Graph();

            Vertex one = graph.GraphStructure.AddVertex(1);
            Vertex two = graph.GraphStructure.AddVertex(2);
            Vertex three = graph.GraphStructure.AddVertex(3);
            Vertex four = graph.GraphStructure.AddVertex(4);
            Vertex five = graph.GraphStructure.AddVertex(5);
            Vertex six = graph.GraphStructure.AddVertex(6);

            graph.GraphStructure.AddLine(one, two, 7);
            graph.GraphStructure.AddLine(two, four, 15);
            graph.GraphStructure.AddLine(four, five, 6);
            graph.GraphStructure.AddLine(five, six, 9);
            graph.GraphStructure.AddLine(six, one, 14);
            graph.GraphStructure.AddLine(one, three, 9);
            graph.GraphStructure.AddLine(six, three, 2);
            graph.GraphStructure.AddLine(two, three, 10);
            graph.GraphStructure.AddLine(four, three, 11);

            return graph;
        }

        public static Graph DefinePathShapedBigGraph()
        {
            Graph graph = new Graph();
            int numberOfVertices = 1000000;

            Vertex previousVertex = graph.GraphStructure.AddVertex(0);

            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddLine(previousVertex, currentVertex);
                previousVertex = currentVertex;
            }

            return graph;
        }

        public static Graph DefineStarShapedBigGraph()
        {
            Graph graph = new Graph();
            int vertexNumber = 1;
            Vertex startVertex = graph.GraphStructure.AddVertex(vertexNumber);
            vertexNumber++;

            List<Vertex> currentCenters = new List<Vertex>();
            currentCenters.Add(startVertex);

            int numberOfConcentricCircles = 3;
            int numberOfSuccessorsForEachVertex = 100;

            for (int i = 0; i < numberOfConcentricCircles; i++)
            {
                List<Vertex> newCenters = new List<Vertex>();
                foreach (Vertex center in currentCenters)
                {
                    for (int u = 0; u < numberOfSuccessorsForEachVertex; u++)
                    {
                        Vertex successor = graph.GraphStructure.AddVertex(vertexNumber);
                        graph.GraphStructure.AddLine(center, successor);
                        newCenters.Add(successor);
                        vertexNumber++;
                    }
                }
                currentCenters = newCenters;
            }

            return graph;
        }
    }
}

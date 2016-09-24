using GraphCore;
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
    public class VertexTests
    {
        [Test]
        public void GetSuccessorsCorrectnessTest()
        {
            string[] expectedSuccessorValues = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            Graph graph = new Graph();
            Vertex mainVertex = graph.AddVertex("x");

            foreach (string expectedSuccessorValue in expectedSuccessorValues)
            {
                Vertex successor = graph.AddVertex(expectedSuccessorValue);
                graph.AddArrow(mainVertex, successor);
            }

            IEnumerable<Vertex> resultSuccessorsEnumerable = mainVertex.GetSuccessors();
            Vertex[] resultSuccessors = resultSuccessorsEnumerable.ToArray();

            Assert.AreEqual(expectedSuccessorValues.Length, resultSuccessors.Length);

            for (int i = 0; i < expectedSuccessorValues.Length; i++)
            {
                string expectedSuccessorValue = expectedSuccessorValues[i];
                string resultSuccessorValue = (resultSuccessors[i] as TextValueVertex).Value;

                Assert.AreEqual(expectedSuccessorValue, resultSuccessorValue);
            }
        }

        [Test]
        public void GetSuccessorsUnnecessaryIterationTest()
        {
            TimeSpan expectedSpeed = TimeSpan.FromMilliseconds(3);

            string[] expectedSuccessorValues = new string[1000000];

            for (int i = 0; i < expectedSuccessorValues.Length; i++)
            {
                expectedSuccessorValues[i] = i.ToString();
            }

            Graph graph = new Graph();
            Vertex mainVertex = graph.AddVertex("x");

            foreach (string expectedSuccessorValue in expectedSuccessorValues)
            {
                Vertex successor = graph.AddVertex(expectedSuccessorValue);
                graph.AddArrow(mainVertex, successor);
            }

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Vertex firstVertex = mainVertex.GetSuccessors().First();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [Test]
        public void RemovedVertexGetSuccessors()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            graph.RemoveVertex(x);

            Assert.Throws<InvalidOperationException>(() => x.GetSuccessors());
        }

        [Test]
        public void GetArrowWeightCorrectnessTest()
        {
            double? expectedArrowWeight = 0.5;

            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y, expectedArrowWeight);

            Vertex resultY = x.GetSuccessors().First((v) => { return v.ValueAsObject == "y"; });

            double? resultArrowWeight = x.GetMinArrowWeight(resultY);

            Assert.AreEqual(expectedArrowWeight, resultArrowWeight);
        }

        [Test]
        public void GetArrowWeightSpeedTest()
        {
            TimeSpan expectedSpeed = TimeSpan.FromMilliseconds(3);

            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            IEnumerable<double?> resultArrowWeight = x.GetArrowWeights(y);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [Test]
        public void RemovedVertexGetArrowWeights()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            graph.RemoveVertex(x);
            
            Assert.Throws<InvalidOperationException>(() => x.GetArrowWeights(y));
        }

        [Test]
        public void GetMinArrowWeightWeightedGraphTest()
        {
            double minWeight = 0.5;
            double maxWeight = 0.6;

            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y, minWeight);
            graph.AddArrow(x, y, maxWeight);

            Assert.AreEqual(minWeight, x.GetMinArrowWeight(y));
        }

        [Test]
        public void GetMinArrowWeightUnweightedGraphTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            graph.AddArrow(x, y);

            Assert.AreEqual(null, x.GetMinArrowWeight(y));
        }

        [Test]
        public void GetMinArrowWeightMixedGraphTest()
        {
            double? minWeight = 0.5;

            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y, minWeight);
            graph.AddArrow(x, y);

            Assert.AreEqual(minWeight, x.GetMinArrowWeight(y));
        }

        [Test]
        public void GetMinArrowWeightNonSuccessorTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");

            Assert.Throws<ArgumentException>(() => x.GetMinArrowWeight(y));
        }

        [Test]
        public void GetMinArrowWeightOtherGraphVertexTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);

            Graph otherGraph = new Graph();
            Vertex otherGraphY = otherGraph.AddVertex("y");

            Assert.Throws<ArgumentException>(() => x.GetMinArrowWeight(otherGraphY));
        }

        [Test]
        public void RemovedVertexGetMinArrowWeight()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);
            graph.RemoveVertex(x);

            Assert.Throws<InvalidOperationException>(() => x.GetMinArrowWeight(y));
        }
    }
}

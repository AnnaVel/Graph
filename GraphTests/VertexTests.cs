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

            TextValueVertex[] resultSuccessors = (mainVertex.GetSuccessors() as IEnumerable<TextValueVertex>).ToArray();

            Assert.AreEqual(expectedSuccessorValues.Length, resultSuccessors.Length);

            for (int i = 0; i < expectedSuccessorValues.Length; i++)
            {
                string expectedSuccessorValue = expectedSuccessorValues[i];
                string resultSuccessorValue = resultSuccessors[i].Value;

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
            double? resultArrowWeight = x.GetMinArrowWeight(y);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [Test]
        public void GetArrowWeightNonexistantSuccessorTest()
        {
            Graph graph = new Graph();
            Vertex x = graph.AddVertex("x");
            Vertex y = graph.AddVertex("y");
            graph.AddArrow(x, y);

            Graph otherGraph = new Graph();
            Vertex otherGraphY = otherGraph.AddVertex("y");

            Assert.Throws<ArgumentException>(() => x.GetMinArrowWeight(otherGraphY));
        }
    }
}

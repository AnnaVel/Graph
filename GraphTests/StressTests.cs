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
    class StressTests
    {
        [Test]
        public void AddAMillionVertices()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(1.5);
#if TRAVISENVIRONMENT
            int expectedMemory = 141000;
#else
            int expectedMemory = 207000;
#endif

            this.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                for (int i = 0; i < numberOfVertices; i++)
                {
                    graph.AddVertex(i);
                }
            },
                expectedTime, expectedMemory);
        }

        [Test]
        public void AddAMillionVerticesWithEdgesBetweenThem()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(12);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(7);
#endif
            int expectedMemory = 864000;

            this.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                Vertex previousVertex = graph.AddVertex(0);
                for (int i = 1; i < numberOfVertices; i++)
                {
                    Vertex currentVertex = graph.AddVertex(i);
                    graph.AddLine(previousVertex, currentVertex);
                    previousVertex = currentVertex;
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void AddVertexWithAMillionEdgesLeadingFromItToOthers()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(38);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(8);
#endif
            int expectedMemory = 945000;

            this.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                Vertex mainVertex = graph.AddVertex(0);
                for (int i = 1; i < numberOfVertices; i++)
                {
                    Vertex currentVertex = graph.AddVertex(i);
                    graph.AddLine(mainVertex, currentVertex);
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void AddTwoVerticesWithAMillionEdgesBetweenThem()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(8);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(1.5);
#endif
            int expectedMemory = 132000;
            //This fluctuates between 74000 and 132000

            this.AssertPerformanceAndMemory(() =>
            {
                int numberOfEdges = 1000000;

                Graph graph = new Graph();

                Vertex firstVertex = graph.AddVertex(0);
                Vertex secondVertex = graph.AddVertex(1);
                for (int i = 1; i < numberOfEdges; i++)
                {
                    graph.AddLine(firstVertex, secondVertex);
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void RemoveAMillionVertices()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.5);
            int expectedMemory = 10;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < numberOfVertices; i++)
            {
                vertices.Add(graph.AddVertex(i));
            }

            this.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void RemoveAMillionVerticesWithEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
            int expectedMemory = 17000;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();

            Vertex previousVertex = graph.AddVertex(0);
            vertices.Add(previousVertex);

            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.AddVertex(i);
                graph.AddLine(previousVertex, currentVertex);
                previousVertex = currentVertex;
                vertices.Add(previousVertex);
            }

            this.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void RemoveAMillionVerticesWithEdgesBetweenThemReverseOrder()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(6);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
#endif
            int expectedMemory = 15000;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();

            Vertex previousVertex = graph.AddVertex(0);
            vertices.Add(previousVertex);

            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.AddVertex(i);
                graph.AddLine(previousVertex, currentVertex);
                previousVertex = currentVertex;
                vertices.Add(previousVertex);
            }

            vertices.Reverse();

            this.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void RemoveVertexWithAMillionEdgesLeadingFromItToOthers()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(15);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.6);
#endif
            int expectedMemory = 10;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            Vertex mainVertex = graph.AddVertex(0);
            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.AddVertex(i);
                graph.AddLine(mainVertex, currentVertex);
            }

            this.AssertPerformanceAndMemory(() =>
            {
                graph.RemoveVertex(mainVertex);
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void RemoveTwoVerticesWithAMillionEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.5);
            int expectedMemory = 10;

            int numberOfEdges = 1000000;

            Graph graph = new Graph();

            Vertex firstVertex = graph.AddVertex(0);
            Vertex secondVertex = graph.AddVertex(1);
            for (int i = 1; i < numberOfEdges; i++)
            {
                graph.AddLine(firstVertex, secondVertex);
            }

            this.AssertPerformanceAndMemory(() =>
            {
                graph.RemoveVertex(firstVertex);
                graph.RemoveVertex(secondVertex);
            },
            expectedTime, expectedMemory);
        }

        [Test]
        public void TraverseAMillionVerticesWithEdgesBetweenThem()
        {
#if TRAVISENVIRONMENT
            TimeSpan expectedTime = TimeSpan.FromSeconds(4);
#else
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
#endif
            int expectedMemory = 17000;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            Vertex firstVertex = graph.AddVertex(0);
            Vertex previousVertex = firstVertex;
            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.AddVertex(i);
                graph.AddArrow(previousVertex, currentVertex);
                previousVertex = currentVertex;
            }

            this.AssertPerformanceAndMemory(() =>
            {
                Vertex currentVertex = firstVertex;
                while (currentVertex != null)
                {
                    currentVertex = currentVertex.GetSuccessors().FirstOrDefault();
                }
            },
                expectedTime, expectedMemory);
        }

        private void AssertPerformanceAndMemory(Action action, TimeSpan expectedDuration, long expectedMemoryInKbs)
        {
            int bytesToKb = 1024;

            long memoryBefore = GC.GetTotalMemory(true);

            Graph graph = new Graph();

            Stopwatch sw = new Stopwatch();

            sw.Start();

            action();

            sw.Stop();

            long memoryAfter = GC.GetTotalMemory(false);

            long memoryUsageInKb = (memoryAfter - memoryBefore) / bytesToKb;

            Assert.IsTrue(sw.Elapsed < expectedDuration, string.Format("Elapsed time was: {0}", sw.Elapsed));
            Assert.IsTrue(memoryUsageInKb < expectedMemoryInKbs, string.Format("Used memory was: {0}kb", memoryUsageInKb));
            Assert.Pass(string.Format("Elapsed time was: {0}; Used memory was: {1}kb", sw.Elapsed, memoryUsageInKb));
        }
    }
}

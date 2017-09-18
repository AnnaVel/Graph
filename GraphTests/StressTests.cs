#if !TRAVISENVIRONMENT

using GraphCore;
using GraphCore.Edges;
using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GraphTests
{
   //[TestFixture]
    class StressTests
    {
        // [Test]
        public void AddAMillionVertices()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(1.5);
            int expectedMemory = 141000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                for (int i = 0; i < numberOfVertices; i++)
                {
                    graph.GraphStructure.AddVertex(i);
                }
            },
                expectedTime, expectedMemory);
        }

       // [Test]
        public void AddAMillionVerticesWithEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(8);
            int expectedMemory = 864000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                Vertex previousVertex = graph.GraphStructure.AddVertex(0);
                for (int i = 1; i < numberOfVertices; i++)
                {
                    Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                    graph.GraphStructure.AddLine(previousVertex, currentVertex);
                    previousVertex = currentVertex;
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void AddVertexWithAMillionEdgesLeadingFromItToOthers()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(8);
            int expectedMemory = 945000;

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                int numberOfVertices = 1000000;

                Graph graph = new Graph();

                Vertex mainVertex = graph.GraphStructure.AddVertex(0);
                for (int i = 1; i < numberOfVertices; i++)
                {
                    Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                    graph.GraphStructure.AddLine(mainVertex, currentVertex);
                }
            },
            expectedTime, expectedMemory);
        }

        //  [Test]
        public void AddTwoVerticesWithAMillionEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(1.5);
            int expectedMemory = 132000;
            //This fluctuates between 74000 and 132000

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                int numberOfEdges = 1000000;

                Graph graph = new Graph();

                Vertex firstVertex = graph.GraphStructure.AddVertex(0);
                Vertex secondVertex = graph.GraphStructure.AddVertex(1);
                for (int i = 1; i < numberOfEdges; i++)
                {
                    graph.GraphStructure.AddLine(firstVertex, secondVertex);
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveAMillionVertices()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.5);
            int expectedMemory = 10;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();
            for (int i = 0; i < numberOfVertices; i++)
            {
                vertices.Add(graph.GraphStructure.AddVertex(i));
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.GraphStructure.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveAMillionVerticesWithEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
            int expectedMemory = 17000;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();

            Vertex previousVertex = graph.GraphStructure.AddVertex(0);
            vertices.Add(previousVertex);

            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddLine(previousVertex, currentVertex);
                previousVertex = currentVertex;
                vertices.Add(previousVertex);
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.GraphStructure.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveAMillionVerticesWithEdgesBetweenThemReverseOrder()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
            int expectedMemory = 15000;
            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            List<Vertex> vertices = new List<Vertex>();

            Vertex previousVertex = graph.GraphStructure.AddVertex(0);
            vertices.Add(previousVertex);

            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddLine(previousVertex, currentVertex);
                previousVertex = currentVertex;
                vertices.Add(previousVertex);
            }

            vertices.Reverse();

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                foreach (Vertex vertex in vertices)
                {
                    graph.GraphStructure.RemoveVertex(vertex);
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveVertexWithAMillionEdgesLeadingFromItToOthers()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.6);
            int expectedMemory = 10;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            Vertex mainVertex = graph.GraphStructure.AddVertex(0);
            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddLine(mainVertex, currentVertex);
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.GraphStructure.RemoveVertex(mainVertex);
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveTwoVerticesWithAMillionEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.5);
            int expectedMemory = 10;

            int numberOfEdges = 1000000;

            Graph graph = new Graph();

            Vertex firstVertex = graph.GraphStructure.AddVertex(0);
            Vertex secondVertex = graph.GraphStructure.AddVertex(1);
            for (int i = 1; i < numberOfEdges; i++)
            {
                graph.GraphStructure.AddLine(firstVertex, secondVertex);
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.GraphStructure.RemoveVertex(firstVertex);
                graph.GraphStructure.RemoveVertex(secondVertex);
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void RemoveAMillionEdgesBetweenTwoVertices()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.001);
            int expectedMemory = 10;

            int numberOfEdges = 1000000;

            Graph graph = new Graph();

            Vertex firstVertex = graph.GraphStructure.AddVertex(0);
            Vertex secondVertex = graph.GraphStructure.AddVertex(1);
            for (int i = 1; i < numberOfEdges; i++)
            {
                graph.GraphStructure.AddLine(firstVertex, secondVertex);
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                graph.GraphStructure.RemoveEdgesBetween(firstVertex, secondVertex);
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void GetSuccessorsOfVertexWithAMillionEdgesLeadingFromItToOthers()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.001);
            int expectedMemory = 20;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            Vertex mainVertex = graph.GraphStructure.AddVertex(0);
            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddLine(mainVertex, currentVertex);
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                mainVertex.GetSuccessors();
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void TraverseAMillionVerticesWithEdgesBetweenThem()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(3);
            int expectedMemory = 17000;

            int numberOfVertices = 1000000;

            Graph graph = new Graph();

            Vertex firstVertex = graph.GraphStructure.AddVertex(0);
            Vertex previousVertex = firstVertex;
            for (int i = 1; i < numberOfVertices; i++)
            {
                Vertex currentVertex = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddArrow(previousVertex, currentVertex);
                previousVertex = currentVertex;
            }

            TestHelper.AssertPerformanceAndMemory(() =>
            {
                Vertex currentVertex = firstVertex;
                while (currentVertex != null)
                {
                    currentVertex = currentVertex.GetSuccessors().FirstOrDefault();
                }
            },
            expectedTime, expectedMemory);
        }

        // [Test]
        public void GetIncomingEdgesSpeedTest()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(1);

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

            Assert.IsTrue(sw.Elapsed < expectedTime, String.Format("Elapsed time for GetIncomingEdges() was: {0}", sw.Elapsed.ToString()));

            sw.Reset();

            sw.Start();
            resultEdges = graph.GraphStructure.GetEdgesComingIntoVertex(mainVertex);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime, String.Format("Elapsed time for GetEdgesComingIntoVertex(Vertex vertex) was: {0}", sw.Elapsed.ToString()));
        }

        //[Test]
        public void GetOutgoingEdgesSpeedTest()
        {
            TimeSpan expectedTime = TimeSpan.FromSeconds(0.8);

            Graph graph = new Graph();
            Vertex mainVertex = graph.GraphStructure.AddVertex("x");

            for (int i = 0; i < 1000000; i++)
            {
                Vertex successor = graph.GraphStructure.AddVertex(i);
                graph.GraphStructure.AddArrow(mainVertex, successor);
            }

            Stopwatch sw = new Stopwatch();

            sw.Start();
            IEnumerable<Edge> resultEdges = mainVertex.GetOutgoingEdges();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime, String.Format("Elapsed time for GetOutgoingEdges() was: {0}", sw.Elapsed.ToString()));

            sw.Reset();

            sw.Start();
            resultEdges = graph.GraphStructure.GetEdgesGoingOutOfVertex(mainVertex);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedTime, String.Format("Elapsed time for GetEdgesGoingOutOfVertex(Vertex vertex) was: {0}", sw.Elapsed.ToString()));
        }
    }
}
#endif
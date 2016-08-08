using GraphCore;
using GraphCore.StructureDescription;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestClass]
    public class VertexTests
    {
        [TestMethod]
        public void GetSuccessorsCorrectnessTest()
        {
            string[] expectedSuccessorNames = new string[]
            {
                "a", "b", "c", "d", "e"
            };

            List<ArrowDescriptor> arrowDescriptors = new List<ArrowDescriptor>();

            foreach (string expectedSuccessorName in expectedSuccessorNames)
            {
                arrowDescriptors.Add(new ArrowDescriptor("x", expectedSuccessorName));
            }

            Vertex x = this.CreateGraphAndGetSpecificVertex("x", arrowDescriptors.ToArray());

            Vertex[] resultSuccessors = x.GetSuccessors().ToArray();

            Assert.AreEqual(expectedSuccessorNames.Length, resultSuccessors.Length);

            for (int i = 0; i < expectedSuccessorNames.Length; i++)
            {
                string expectedSuccessorName = expectedSuccessorNames[i];
                string resultSuccessorName = resultSuccessors[i].Name;

                Assert.AreEqual(expectedSuccessorName, resultSuccessorName);
            }
        }

        [TestMethod]
        public void GetSuccessorsUnnecessaryIterationTest()
        {
            TimeSpan expectedSpeed = TimeSpan.FromMilliseconds(3);

            string[] expectedSuccessorNames = new string[1000000];

            for (int i = 0; i < expectedSuccessorNames.Length; i++)
            {
                expectedSuccessorNames[i] = i.ToString();
            }

            List<ArrowDescriptor> arrowDescriptors = new List<ArrowDescriptor>();

            foreach (string expectedSuccessorName in expectedSuccessorNames)
            {
                arrowDescriptors.Add(new ArrowDescriptor("x", expectedSuccessorName));
            }

            Vertex x = this.CreateGraphAndGetSpecificVertex("x", arrowDescriptors.ToArray());

            Stopwatch sw = new Stopwatch();
            sw.Start();
            Vertex firstVertex = x.GetSuccessors().First();
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [TestMethod]
        public void GetArrowWeightCorrectnessTest()
        {
            double? expectedArrowWeight = 0.5;
            ArrowDescriptor arrowDescriptor = new ArrowDescriptor("x", "y", expectedArrowWeight);

            Vertex x = this.CreateGraphAndGetSpecificVertex("x", arrowDescriptor);
            Vertex y = x.GetSuccessors().First((v) => { return v.Name == "y"; });

            double? resultArrowWeight = x.GetArrowWeight(y);

            Assert.AreEqual(expectedArrowWeight, resultArrowWeight);
        }

        [TestMethod]
        public void GetArrowWeightSpeedTest()
        {
            TimeSpan expectedSpeed = TimeSpan.FromMilliseconds(3);
            ArrowDescriptor arrowDescriptor = new ArrowDescriptor("x", "y");

            Vertex x = this.CreateGraphAndGetSpecificVertex("x", arrowDescriptor);
            Vertex y = x.GetSuccessors().First((v) => { return v.Name == "y"; });

            Stopwatch sw = new Stopwatch();
            sw.Start();
            double? resultArrowWeight = x.GetArrowWeight(y);
            sw.Stop();

            Assert.IsTrue(sw.Elapsed < expectedSpeed);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetArrowWeightNonexistantSuccessorTest()
        {
            ArrowDescriptor arrowDescriptor = new ArrowDescriptor("x", "y");
            Vertex x = this.CreateGraphAndGetSpecificVertex("x", arrowDescriptor);
            
            VertexDescriptor vertexDescriptor = new VertexDescriptor("y");
            Vertex otherGraphY = this.CreateGraphAndGetSpecificVertex("y", vertexDescriptor);

            x.GetArrowWeight(otherGraphY);
        }

        private Vertex CreateGraphAndGetSpecificVertex(string vertexName, params StructureDescriptor[] structureDescriptors)
        {
            Graph graph = new Graph(structureDescriptors);

            Vertex result = graph.Vertices.First((v) => { return v.Name == vertexName; });

            return result;
        }
    }
}

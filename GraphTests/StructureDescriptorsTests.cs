using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphCore.StructureDescription;

namespace GraphTests
{
    [TestClass]
    public class StructureDescriptorsTests
    {
        [TestMethod]
        public void CreateArrowDescriptorWithWeight()
        {
            string predecessorVertexName = "x";
            string successorVertexName = "y";
            double weight = 1;

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName, weight);

            Assert.AreEqual(predecessorVertexName, arrowDescriptor.PredecessorName);
            Assert.AreEqual(successorVertexName, arrowDescriptor.SuccessorName);
            Assert.AreEqual(weight, arrowDescriptor.Weight);
        }

        [TestMethod]
        public void CreateArrowDescriptorWithoutWeight()
        {
            string predecessorVertexName = "x";
            string successorVertexName = "y";

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);

            Assert.AreEqual(predecessorVertexName, arrowDescriptor.PredecessorName);
            Assert.AreEqual(successorVertexName, arrowDescriptor.SuccessorName);
            Assert.IsNull(arrowDescriptor.Weight);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArrowDescriptorWithEmptyPredecessorName()
        {
            string predecessorVertexName = string.Empty;
            string successorVertexName = "y";

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArrowDescriptorWithEmptySuccessorName()
        {
            string predecessorVertexName = "x";
            string successorVertexName = string.Empty;

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArrowDescriptorWithNullPredecessorName()
        {
            string predecessorVertexName = null;
            string successorVertexName = "y";

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateArrowDescriptorWithNullSuccessorName()
        {
            string predecessorVertexName = "x";
            string successorVertexName = null;

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);
        }

        [TestMethod]
        public void CreateEdgeDescriptorWithWeight()
        {
            string firstVertexName = "x";
            string secondVertexName = "y";
            double weight = 1;

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName, weight);

            Assert.AreEqual(firstVertexName, edgeDescriptor.FirstVertexName);
            Assert.AreEqual(secondVertexName, edgeDescriptor.SecondVertexName);
            Assert.AreEqual(weight, edgeDescriptor.Weight);
        }

        [TestMethod]
        public void CreateEdgeDescriptorWithoutWeight()
        {
            string firstVertexName = "x";
            string secondVertexName = "y";

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);

            Assert.AreEqual(firstVertexName, edgeDescriptor.FirstVertexName);
            Assert.AreEqual(secondVertexName, edgeDescriptor.SecondVertexName);
            Assert.IsNull(edgeDescriptor.Weight);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeDescriptorWithEmptyFirstVertexName()
        {
            string firstVertexName = string.Empty;
            string secondVertexName = "y";

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeDescriptorWithEmptySecondVertexName()
        {
            string firstVertexName = "x";
            string secondVertexName = string.Empty;

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeDescriptorWithNullFirstVertexName()
        {
            string firstVertexName = null;
            string secondVertexName = "y";

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateEdgeDescriptorWithNullSecondVertexName()
        {
            string firstVertexName = "x";
            string secondVertexName = null;

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);
        }

        [TestMethod]
        public void CreateVertexDescriptor()
        {
            string name = "x";

            VertexDescriptor vertexDescriptor = new VertexDescriptor(name);

            Assert.AreEqual(name, vertexDescriptor.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVertexDescriptorWithEmptyName()
        {
            string name = string.Empty;

            VertexDescriptor vertexDescriptor = new VertexDescriptor(name);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CreateVertexDescriptorWithNullName()
        {
            string name = null;

            VertexDescriptor vertexDescriptor = new VertexDescriptor(name);
        }
    }
}

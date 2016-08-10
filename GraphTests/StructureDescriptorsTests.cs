using System;
using NUnit.Framework;
using GraphCore.StructureDescription;

namespace GraphTests
{
    [TestFixture]
    public class StructureDescriptorsTests
    {
        [Test]
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

        [Test]
        public void CreateArrowDescriptorWithoutWeight()
        {
            string predecessorVertexName = "x";
            string successorVertexName = "y";

            ArrowDescriptor arrowDescriptor = new ArrowDescriptor(predecessorVertexName, successorVertexName);

            Assert.AreEqual(predecessorVertexName, arrowDescriptor.PredecessorName);
            Assert.AreEqual(successorVertexName, arrowDescriptor.SuccessorName);
            Assert.IsNull(arrowDescriptor.Weight);
        }

        [Test]
        public void CreateArrowDescriptorWithEmptyPredecessorName()
        {
            string predecessorVertexName = string.Empty;
            string successorVertexName = "y";

            Assert.Throws<ArgumentException>(() => new ArrowDescriptor(predecessorVertexName, successorVertexName));
        }

        [Test]
        public void CreateArrowDescriptorWithEmptySuccessorName()
        {
            string predecessorVertexName = "x";
            string successorVertexName = string.Empty;

            Assert.Throws<ArgumentException>(() => new ArrowDescriptor(predecessorVertexName, successorVertexName));
        }

        [Test]
        public void CreateArrowDescriptorWithNullPredecessorName()
        {
            string predecessorVertexName = null;
            string successorVertexName = "y";

            Assert.Throws<ArgumentException>(() => new ArrowDescriptor(predecessorVertexName, successorVertexName));
        }

        [Test]
        public void CreateArrowDescriptorWithNullSuccessorName()
        {
            string predecessorVertexName = "x";
            string successorVertexName = null;

            Assert.Throws<ArgumentException>(() => new ArrowDescriptor(predecessorVertexName, successorVertexName));
        }

        [Test]
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

        [Test]
        public void CreateEdgeDescriptorWithoutWeight()
        {
            string firstVertexName = "x";
            string secondVertexName = "y";

            EdgeDescriptor edgeDescriptor = new EdgeDescriptor(firstVertexName, secondVertexName);

            Assert.AreEqual(firstVertexName, edgeDescriptor.FirstVertexName);
            Assert.AreEqual(secondVertexName, edgeDescriptor.SecondVertexName);
            Assert.IsNull(edgeDescriptor.Weight);
        }

        [Test]
        public void CreateEdgeDescriptorWithEmptyFirstVertexName()
        {
            string firstVertexName = string.Empty;
            string secondVertexName = "y";

            Assert.Throws<ArgumentException>(() => new EdgeDescriptor(firstVertexName, secondVertexName));
        }

        [Test]
        public void CreateEdgeDescriptorWithEmptySecondVertexName()
        {
            string firstVertexName = "x";
            string secondVertexName = string.Empty;

            Assert.Throws<ArgumentException>(() => new EdgeDescriptor(firstVertexName, secondVertexName));
        }

        [Test]
        public void CreateEdgeDescriptorWithNullFirstVertexName()
        {
            string firstVertexName = null;
            string secondVertexName = "y";

            Assert.Throws<ArgumentException>(() => new EdgeDescriptor(firstVertexName, secondVertexName));
        }

        [Test]
        public void CreateEdgeDescriptorWithNullSecondVertexName()
        {
            string firstVertexName = "x";
            string secondVertexName = null;

            Assert.Throws<ArgumentException>(() => new EdgeDescriptor(firstVertexName, secondVertexName));
        }

        [Test]
        public void CreateVertexDescriptor()
        {
            string name = "x";

            VertexDescriptor vertexDescriptor = new VertexDescriptor(name);

            Assert.AreEqual(name, vertexDescriptor.Name);
        }

        [Test]
        public void CreateVertexDescriptorWithEmptyName()
        {
            string name = string.Empty;

            Assert.Throws<ArgumentException>(() => new VertexDescriptor(name));
        }

        [Test]
        public void CreateVertexDescriptorWithNullName()
        {
            string name = null;

            Assert.Throws<ArgumentException>(() => new VertexDescriptor(name));
        }
    }
}

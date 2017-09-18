using GraphCore;
using GraphCore.Edges;
using GraphCore.Vertices;
using NUnit.Framework;

namespace GraphTests
{
    [TestFixture]
    class EdgeWeightTests
    {
        [Test]
        public void CreateWeightedEdgeDoubleTest()
        {
            double weight = 2.2;

            Edge edge = this.CreateEdgeWithWeight<double>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeIntTest()
        {
            int weight = 2;

            Edge edge = this.CreateEdgeWithWeight<int>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeUIntTest()
        {
            uint weight = 2;

            Edge edge = this.CreateEdgeWithWeight<uint>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeFloatTest()
        {
            float weight = 2.1F;

            Edge edge = this.CreateEdgeWithWeight<float>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeDecimalTest()
        {
            decimal weight = 2.3M;

            Edge edge = this.CreateEdgeWithWeight<decimal>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeSbyteTest()
        {
            sbyte weight = 3;

            Edge edge = this.CreateEdgeWithWeight<sbyte>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeByteTest()
        {
            byte weight = 4;

            Edge edge = this.CreateEdgeWithWeight<byte>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeShortTest()
        {
            short weight = -10;

            Edge edge = this.CreateEdgeWithWeight<short>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeUshortTest()
        {
            ushort weight = 10;

            Edge edge = this.CreateEdgeWithWeight<ushort>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeLongTest()
        {
            long weight = 3000000000;

            Edge edge = this.CreateEdgeWithWeight<long>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        [Test]
        public void CreateWeightedEdgeUlongTest()
        {
            ulong weight = 3000000000;

            Edge edge = this.CreateEdgeWithWeight<ulong>(weight);

            Assert.AreEqual(true, edge.IsWeighted);
            Assert.AreEqual(weight, edge.Weight);
        }

        private Edge CreateEdgeWithWeight<T>(T number)
        {
            Graph graph = new Graph();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");

            return graph.GraphStructure.AddLine(x, y, number);
        }

    }
}

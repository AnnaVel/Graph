using GraphCore.Vertices;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    [TestFixture]
    public class AdjacencyListTests
    {
        [Test]
        public void AddAdjacentVertexToNewVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");

            double weight = 1;
            list.AddAdjacentVertexToVertex(x, y, weight);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(1, successors.Count());
            Assert.AreEqual(y, successors.First());

            IEnumerable<double?> weights = list.GetWeights(x, y);
            Assert.AreEqual(1, weights.Count());
            Assert.AreEqual(weight, weights.First());
        }

        [Test]
        public void AddAdjacentVertexToExistingVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            list.AddAdjacentVertexToVertex(x, y, null);
            Vertex z = new TextValueVertex("z");

            double weight = 1;
            list.AddAdjacentVertexToVertex(x, z, weight);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(2, successors.Count());
            Assert.AreEqual(z, successors.ElementAt(1));

            IEnumerable<double?> weights = list.GetWeights(x, z);
            Assert.AreEqual(1, weights.Count());
            Assert.AreEqual(weight, weights.First());
        }

        [Test]
        public void AddExistingAdjacentVertexToVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            list.AddAdjacentVertexToVertex(x, y, null);

            double weight = 1;
            list.AddAdjacentVertexToVertex(x, y, weight);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(1, successors.Count());
            Assert.AreEqual(y, successors.First());

            IEnumerable<double?> weights = list.GetWeights(x, y);
            Assert.AreEqual(2, weights.Count());
            Assert.AreEqual(weight, weights.ElementAt(1));
        }

        [Test]
        public void RemoveAdjacentVertexFromVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            list.AddAdjacentVertexToVertex(x, y, null);

            bool result = list.RemoveAdjacentVertexFromVertex(x, y);

            Assert.IsTrue(result);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(0, successors.Count());

            IEnumerable<double?> weights = list.GetWeights(x, y);
            Assert.AreEqual(0, weights.Count());
        }

        [Test]
        public void RemoveAdjacentVertexFromNonexistingVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");

            bool result = list.RemoveAdjacentVertexFromVertex(x, y);

            Assert.IsFalse(result);
        }

        [Test]
        public void RemoveNonexistingAdjacentVertexFromVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            Vertex z = new TextValueVertex("z");
            list.AddAdjacentVertexToVertex(x, y, null);

            bool result = list.RemoveAdjacentVertexFromVertex(x, z);

            Assert.IsFalse(result);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(1, successors.Count());
            Assert.AreEqual(y, successors.First());

            IEnumerable<double?> weights = list.GetWeights(x, y);
            Assert.AreEqual(1, weights.Count());
            Assert.AreEqual(null, weights.First());
        }

        [Test]
        public void RemoveAdjacentVertexWithTwoAdjacenciesToVertexTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            Vertex z = new TextValueVertex("z");
            list.AddAdjacentVertexToVertex(x, y, null);
            list.AddAdjacentVertexToVertex(x, y, 1);
            list.AddAdjacentVertexToVertex(x, z, null);

            bool result = list.RemoveAdjacentVertexFromVertex(x, y);

            Assert.IsTrue(result);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(1, successors.Count());
            Assert.AreEqual(z, successors.First());

            IEnumerable<double?> weightsY = list.GetWeights(x, y);
            Assert.AreEqual(0, weightsY.Count());

            IEnumerable<double?> weightsZ = list.GetWeights(x, z);
            Assert.AreEqual(1, weightsZ.Count());
            Assert.AreEqual(null, weightsZ.First());
        }

        [Test]
        public void RemoveAdjacencyListItemTest()
        {
            AdjacencyList list = new AdjacencyList();
            Vertex x = new TextValueVertex("x");
            Vertex y = new TextValueVertex("y");
            list.AddAdjacentVertexToVertex(x, y, null);
            list.AddAdjacentVertexToVertex(x, y, 1);

            bool result = list.RemoveAdjacencyListItem(x);

            Assert.IsTrue(result);

            IEnumerable<Vertex> successors = list.GetAdjacentVertices(x);
            Assert.AreEqual(0, successors.Count());

            IEnumerable<double?> weightsY = list.GetWeights(x, y);
            Assert.AreEqual(0, weightsY.Count());
        }
    }
}

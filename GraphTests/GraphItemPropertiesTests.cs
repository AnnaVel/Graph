using GraphCore;
using GraphCore.Edges;
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
    public class GraphItemPropertiesTests
    {
        private Vertex GetTestVertex()
        {
            Graph graph = new Graph();
            return graph.GraphStructure.AddVertex("x");
        }

        [Test]
        public void SetPropertyToVertexTest()
        {
            string propertyName = "name";
            object propertyValue = "test";
            Vertex vertex = this.GetTestVertex();

            vertex.SetProperty(propertyName, propertyValue);

            Assert.AreEqual(propertyValue, vertex.GetProperty(propertyName).ValueAsObject);
        }

        [Test]
        public void SetPropertyToEdgeTest()
        {
            string propertyName = "name";
            object propertyValue = "test";
            Graph graph = new Graph();
            Edge edge = graph.GraphStructure.AddLine(graph.GraphStructure.AddVertex("x"), graph.GraphStructure.AddVertex("y"));

            edge.SetProperty(propertyName, propertyValue);

            Assert.AreEqual(propertyValue, edge.GetProperty(propertyName).ValueAsObject);
        }

        [Test]
        public void SetPropertyWithEmptyNameTest()
        {
            string propertyName = string.Empty;
            object propertyValue = "test";
            Vertex vertex = this.GetTestVertex();

            Assert.Throws<ArgumentException>(() =>
                {
                    vertex.SetProperty(propertyName, propertyValue);
                });
        }

        [Test]
        public void SetPropertyWithNullNameTest()
        {
            string propertyName = null;
            object propertyValue = "test";
            Vertex vertex = this.GetTestVertex();

            Assert.Throws<ArgumentNullException>(() =>
            {
                vertex.SetProperty(propertyName, propertyValue);
            });
        }

        [Test]
        public void SetAlreadyExistingPropertyTest()
        {
            string propertyName = "name";
            object firstPropertyValue = "first";
            object secondPropertyValue = "second";
            Vertex vertex = this.GetTestVertex();
            vertex.SetProperty(propertyName, firstPropertyValue);

            vertex.SetProperty(propertyName, secondPropertyValue);

            Assert.AreEqual(secondPropertyValue, vertex.GetProperty(propertyName).ValueAsObject);
        }

        [Test]
        public void RemovePropertyTest()
        {
            string propertyName = "name";
            object propertyValue = "test";
            Vertex vertex = this.GetTestVertex();
            vertex.SetProperty(propertyName, propertyValue);

            bool result = vertex.RemoveProperty(propertyName);

            Assert.IsTrue(result);
            Assert.AreEqual(null, vertex.GetProperty(propertyName));
        }

        [Test]
        public void RemoveNonExistingPropertyTest()
        {
            string propertyName = "name";
            Vertex vertex = this.GetTestVertex();

            bool result = vertex.RemoveProperty(propertyName);

            Assert.IsFalse(result);
        }
    }
}

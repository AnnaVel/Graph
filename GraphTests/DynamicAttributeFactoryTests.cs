using GraphCore;
using GraphCore.Edges;
using GraphCore.GraphItemProperties;
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
    public class DynamicAttributeFactoryTests
    {
        [Test]
        public void CreateBoolDynamicAttribute()
        {
            this.TestCreationOfDynamicAttributeOfType(true, typeof(BooleanDynamicAttribute));
        }

        [Test]
        public void CreateStringDynamicAttribute()
        {
            this.TestCreationOfDynamicAttributeOfType("test", typeof(StringDynamicAttribute));
        }

        [Test]
        public void CreateIntegerDynamicAttribute()
        {
            this.TestCreationOfDynamicAttributeOfType(1, typeof(IntegerDynamicAttribute));
        }

        [Test]
        public void CreateDoubleDynamicAttribute()
        {
            this.TestCreationOfDynamicAttributeOfType(1d, typeof(DoubleDynamicAttribute));
        }

        [Test]
        public void CreateObjectDynamicAttribute()
        {
            this.TestCreationOfDynamicAttributeOfType(new Graph(), typeof(ObjectDynamicAttribute));
        }

        [Test]
        public void CreateCustomVertexDynamicAttribute()
        {
            string attributeName = "name";
            Graph graph = new Graph();
            graph.GraphStructure.VertexDynamicAttributeFactory = new CustomDynamicAttributeFactory();
            Vertex vertex = graph.GraphStructure.AddVertex("x");

            vertex.SetDynamicAttribute(attributeName, "test");

            IDynamicAttribute dynamicAttribute = vertex.GetDynamicAttribute(attributeName);
            Assert.AreEqual(typeof(CustomDynamicAttribute), dynamicAttribute.GetType());
        }

        [Test]
        public void CreateCustomEdgeDynamicAttribute()
        {
            string attributeName = "name";
            Graph graph = new Graph();
            graph.GraphStructure.EdgeDynamicAttributeFactory = new CustomDynamicAttributeFactory();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);

            line.SetDynamicAttribute(attributeName, "test");

            IDynamicAttribute dynamicAttribute = line.GetDynamicAttribute(attributeName);
            Assert.AreEqual(typeof(CustomDynamicAttribute), dynamicAttribute.GetType());
        }

        private void TestCreationOfDynamicAttributeOfType(object attributeValue, Type expectedDynamicAttributeType)
        {
            string attributeName = "name";
            Graph graph = new Graph();
            Vertex vertex = graph.GraphStructure.AddVertex("x");

            vertex.SetDynamicAttribute(attributeName, attributeValue);

            IDynamicAttribute dynamicAttribute = vertex.GetDynamicAttribute(attributeName);
            Assert.AreEqual(expectedDynamicAttributeType, dynamicAttribute.GetType());
        }
    }
}

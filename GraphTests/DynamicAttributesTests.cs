using GraphCore;
using GraphCore.DynamicAttributes;
using GraphCore.Edges;
using GraphCore.Utilities;
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
    public class DynamicAttributesTests
    {
        private Vertex GetTestVertex()
        {
            Graph graph = new Graph();
            return graph.GraphStructure.AddVertex("x");
        }

        [Test]
        public void SetDynamicAttributeToVertexTest()
        {
            string attributeName = "name";
            object attributeValue = "test";
            Vertex vertex = this.GetTestVertex();

            vertex.SetDynamicAttribute(attributeName, attributeValue);

            Assert.AreEqual(attributeValue, vertex.GetDynamicAttribute(attributeName).ValueAsObject);
        }

        [Test]
        public void SetDynamicAttributeToEdgeTest()
        {
            string attributeName = "name";
            object attributeValue = "test";
            Graph graph = new Graph();
            Edge edge = graph.GraphStructure.AddLine(graph.GraphStructure.AddVertex("x"), graph.GraphStructure.AddVertex("y"));

            edge.SetDynamicAttribute(attributeName, attributeValue);

            Assert.AreEqual(attributeValue, edge.GetDynamicAttribute(attributeName).ValueAsObject);
        }

        [Test]
        public void SetDynamicAttributeWithEmptyNameTest()
        {
            string attributeName = string.Empty;
            object attributeValue = "test";
            Vertex vertex = this.GetTestVertex();

            Assert.Throws<ArgumentException>(() =>
                {
                    vertex.SetDynamicAttribute(attributeName, attributeValue);
                });
        }

        [Test]
        public void SetDynamicAttributeWithNullNameTest()
        {
            string attributeName = null;
            object attributeValue = "test";
            Vertex vertex = this.GetTestVertex();

            Assert.Throws<ArgumentNullException>(() =>
            {
                vertex.SetDynamicAttribute(attributeName, attributeValue);
            });
        }

        [Test]
        public void SetAlreadyExistingDynamicAttributeTest()
        {
            string attributeName = "name";
            object firstDynamicAttributeValue = "first";
            object secondDynamicAttributeValue = "second";
            Vertex vertex = this.GetTestVertex();
            vertex.SetDynamicAttribute(attributeName, firstDynamicAttributeValue);

            vertex.SetDynamicAttribute(attributeName, secondDynamicAttributeValue);

            Assert.AreEqual(secondDynamicAttributeValue, vertex.GetDynamicAttribute(attributeName).ValueAsObject);
        }

        [Test]
        public void RemoveDynamicAttributeTest()
        {
            string attributeName = "name";
            object attributeValue = "test";
            Vertex vertex = this.GetTestVertex();
            vertex.SetDynamicAttribute(attributeName, attributeValue);

            bool result = vertex.RemoveDynamicAttribute(attributeName);

            Assert.IsTrue(result);
            Assert.AreEqual(null, vertex.GetDynamicAttribute(attributeName));
        }

        [Test]
        public void RemoveNonExistingDynamicAttributeTest()
        {
            string attributeName = "name";
            Vertex vertex = this.GetTestVertex();

            bool result = vertex.RemoveDynamicAttribute(attributeName);

            Assert.IsFalse(result);
        }

        [Test]
        public void GetDynamicAttributeThatWasLastSetInGroupBasicTest()
        {
            string attributeName = "name";
            object attributeValue = "test";
            Vertex vertex = this.GetTestVertex();

            vertex.SetDynamicAttribute(attributeName, attributeValue);

            Assert.AreEqual(attributeValue, vertex.GetDynamicAttributeThatWasLastSetInGroup(attributeName).ValueAsObject);
        }

        [Test]
        public void Test()
        {
            for(int i = 0; i < 100; i++)
            {
                this.GetDynamicAttributeThatWasLastSetInGroupThreeAttributesTest();
            }
        }

        [Test]
        public void GetDynamicAttributeThatWasLastSetInGroupThreeAttributesTest()
        {
            string attributeName1 = "name";
            object attributeValue1 = "test1";
            string attributeName2 = "name:two";
            object attributeValue2 = "test2";
            string attributeName3 = "name:three";
            object attributeValue3 = "test3";
            Vertex vertex = this.GetTestVertex();

            vertex.SetDynamicAttribute(attributeName1, attributeValue1);
            vertex.SetDynamicAttribute(attributeName2, attributeValue2);
            vertex.SetDynamicAttribute(attributeName3, attributeValue3);

            TimeStamp timeStamp1 = vertex.GetTimeStamp(attributeName1);
            TimeStamp timeStamp2 = vertex.GetTimeStamp(attributeName2);
            TimeStamp timeStamp3 = vertex.GetTimeStamp(attributeName3);

            Assert.AreEqual(attributeValue3, vertex.GetDynamicAttributeThatWasLastSetInGroup(attributeName1).ValueAsObject,
                "attribute1 timeStamp: {0}; attribute2 timeStamp: {1}; attribute3 timeStamp: {2}, currentTimestamp index: {3}",
                timeStamp1, timeStamp2, timeStamp3, TimeStampCreator.GetIndexTimeStampPart());
        }

        [Test]
        public void GetDynamicAttributeThatWasLastSetInGroupThreeAttributesRemoveLastFirstTest()
        {
            string attributeName1 = "name";
            object attributeValue1 = "test1";
            string attributeName2 = "name:two";
            object attributeValue2 = "test2";
            string attributeName3 = "name:three";
            object attributeValue3 = "test3";
            Vertex vertex = this.GetTestVertex();

            vertex.SetDynamicAttribute(attributeName1, attributeValue1);
            vertex.SetDynamicAttribute(attributeName2, attributeValue2);
            vertex.SetDynamicAttribute(attributeName3, attributeValue3);

            vertex.RemoveDynamicAttribute(attributeName3);

            Assert.AreEqual(attributeValue2, vertex.GetDynamicAttributeThatWasLastSetInGroup(attributeName1).ValueAsObject);
        }
    }
}

using GraphCore;
using GraphCore.VertexProperties;
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
    public class VertexPropertyFactoryTests
    {
        [Test]
        public void CreateBoolProperty()
        {
            this.TestCreationOfPropertyOfType(true, typeof(BooleanVertexProperty));
        }

        [Test]
        public void CreateStringProperty()
        {
            this.TestCreationOfPropertyOfType("test", typeof(StringVertexProperty));
        }

        [Test]
        public void CreateIntegerProperty()
        {
            this.TestCreationOfPropertyOfType(1, typeof(IntegerVertexProperty));
        }

        [Test]
        public void CreateDoubleProperty()
        {
            this.TestCreationOfPropertyOfType(1d, typeof(DoubleVertexProperty));
        }

        [Test]
        public void CreateObjectProperty()
        {
            this.TestCreationOfPropertyOfType(new Graph(), typeof(ObjectVertexProperty));
        }

        [Test]
        public void CreateCustomProperty()
        {
            string propertyName = "name";
            Graph graph = new Graph();
            graph.VertexPropertyFactory = new CustomVertexPropertyFactory();
            Vertex vertex = graph.AddVertex("x");

            vertex.SetProperty(propertyName, "test");

            IVertexProperty property = vertex.GetProperty(propertyName);
            Assert.AreEqual(typeof(CustomVertexProperty), property.GetType());
        }

        private void TestCreationOfPropertyOfType(object propertyValue, Type expectedVertexPropertyType)
        {
            string propertyName = "name";
            Graph graph = new Graph();
            Vertex vertex = graph.AddVertex("x");

            vertex.SetProperty(propertyName, propertyValue);

            IVertexProperty property = vertex.GetProperty(propertyName);
            Assert.AreEqual(expectedVertexPropertyType, property.GetType());
        }
    }
}

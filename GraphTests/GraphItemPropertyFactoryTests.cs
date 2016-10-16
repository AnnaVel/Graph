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
    public class GraphItemPropertyFactoryTests
    {
        [Test]
        public void CreateBoolProperty()
        {
            this.TestCreationOfPropertyOfType(true, typeof(BooleanGraphItemProperty));
        }

        [Test]
        public void CreateStringProperty()
        {
            this.TestCreationOfPropertyOfType("test", typeof(StringGraphItemProperty));
        }

        [Test]
        public void CreateIntegerProperty()
        {
            this.TestCreationOfPropertyOfType(1, typeof(IntegerGraphItemProperty));
        }

        [Test]
        public void CreateDoubleProperty()
        {
            this.TestCreationOfPropertyOfType(1d, typeof(DoubleGraphItemProperty));
        }

        [Test]
        public void CreateObjectProperty()
        {
            this.TestCreationOfPropertyOfType(new Graph(), typeof(ObjectGraphItemProperty));
        }

        [Test]
        public void CreateCustomVertexProperty()
        {
            string propertyName = "name";
            Graph graph = new Graph();
            graph.GraphStructure.VertexPropertyFactory = new CustomPropertyFactory();
            Vertex vertex = graph.GraphStructure.AddVertex("x");

            vertex.SetProperty(propertyName, "test");

            IGraphItemProperty property = vertex.GetProperty(propertyName);
            Assert.AreEqual(typeof(CustomProperty), property.GetType());
        }

        [Test]
        public void CreateCustomEdgeProperty()
        {
            string propertyName = "name";
            Graph graph = new Graph();
            graph.GraphStructure.EdgePropertyFactory = new CustomPropertyFactory();
            Vertex x = graph.GraphStructure.AddVertex("x");
            Vertex y = graph.GraphStructure.AddVertex("y");
            Edge line = graph.GraphStructure.AddLine(x, y);

            line.SetProperty(propertyName, "test");

            IGraphItemProperty property = line.GetProperty(propertyName);
            Assert.AreEqual(typeof(CustomProperty), property.GetType());
        }

        private void TestCreationOfPropertyOfType(object propertyValue, Type expectedVertexPropertyType)
        {
            string propertyName = "name";
            Graph graph = new Graph();
            Vertex vertex = graph.GraphStructure.AddVertex("x");

            vertex.SetProperty(propertyName, propertyValue);

            IGraphItemProperty property = vertex.GetProperty(propertyName);
            Assert.AreEqual(expectedVertexPropertyType, property.GetType());
        }
    }
}

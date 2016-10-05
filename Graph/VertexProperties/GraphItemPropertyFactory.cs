using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class GraphItemPropertyFactory
    {
        private readonly Dictionary<Predicate<object>, Func<string, object, IGraphItemProperty>> vertexPropertyConstructorFunctions;

        protected Dictionary<Predicate<object>, Func<string, object, IGraphItemProperty>> VertexPropertyConstructorFunctions
        {
            get
            {
                return this.vertexPropertyConstructorFunctions;
            }
        }

        public GraphItemPropertyFactory()
        {
            this.vertexPropertyConstructorFunctions = new Dictionary<Predicate<object>, Func<string, object, IGraphItemProperty>>();

            this.RegisterConstructorFunctions();
        }

        public IGraphItemProperty CreateVertexProperty(string name, object value)
        {
            foreach (var pair in this.vertexPropertyConstructorFunctions)
            {
                Predicate<object> predicate = pair.Key;

                if (predicate(value))
                {
                    var constructor = pair.Value;
                    return constructor(name, value);
                }
            }

            return new ObjectGraphItemProperty(name, value);
        }

        protected virtual void RegisterConstructorFunctions()
        {
            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is string;
                },
                (name, value) =>
                {
                    return new StringGraphItemProperty(name, (string)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is bool;
                },
                (name, value) =>
                {
                    return new BooleanGraphItemProperty(name, (bool)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is int;
                },
                (name, value) =>
                {
                    return new IntegerGraphItemProperty(name, (int)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is double;
                },
                (name, value) =>
                {
                    return new DoubleGraphItemProperty(name, (double)value);
                });
        }
    }
}

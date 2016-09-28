using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.VertexProperties
{
    public class VertexPropertyFactory
    {
        private readonly Dictionary<Predicate<object>, Func<string, object, IVertexProperty>> vertexPropertyConstructorFunctions;

        protected Dictionary<Predicate<object>, Func<string, object, IVertexProperty>> VertexPropertyConstructorFunctions
        {
            get
            {
                return this.vertexPropertyConstructorFunctions;
            }
        }

        public VertexPropertyFactory()
        {
            this.vertexPropertyConstructorFunctions = new Dictionary<Predicate<object>, Func<string, object, IVertexProperty>>();

            this.RegisterConstructorFunctions();
        }

        public IVertexProperty CreateVertexProperty(string name, object value)
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

            return new ObjectVertexProperty(name, value);
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
                    return new StringVertexProperty(name, (string)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is bool;
                },
                (name, value) =>
                {
                    return new BooleanVertexProperty(name, (bool)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is int;
                },
                (name, value) =>
                {
                    return new IntegerVertexProperty(name, (int)value);
                });

            this.vertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return value is double;
                },
                (name, value) =>
                {
                    return new DoubleVertexProperty(name, (double)value);
                });
        }
    }
}

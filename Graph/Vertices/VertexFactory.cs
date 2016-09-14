using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public class VertexFactory
    {
        private readonly Dictionary<Predicate<object>, Func<object, Vertex>> vertexConstructorFunctions;

        protected Dictionary<Predicate<object>, Func<object, Vertex>> VertexConstructorFunctions
        {
            get
            {
                return this.vertexConstructorFunctions;
            }
        }

        public VertexFactory()
        {
            this.vertexConstructorFunctions = new Dictionary<Predicate<object>, Func<object, Vertex>>();
            this.RegisterConstructorFunctions();
        }

        protected virtual void RegisterConstructorFunctions()
        {
            this.vertexConstructorFunctions.Add(
                (o) => { return o is string; },
                (o) => { return new TextValueVertex(o as string); });

            this.vertexConstructorFunctions.Add(
                (o) => { return o is double; },
                (o) => { return new DoubleValueVertex((double)o); }
            );
        }

        public Vertex CreateVertex(object value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            foreach (var pair in this.vertexConstructorFunctions)
            {
                var predicate = pair.Key;
                if (predicate(value))
                {
                    return pair.Value(value);
                }
            }

            return new ObjectValueVertex(value);
        }
    }
}

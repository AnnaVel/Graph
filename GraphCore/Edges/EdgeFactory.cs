using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public class EdgeFactory
    {
        private readonly Dictionary<Predicate<object>,  Func<Vertex, Vertex, bool, object, Edge>> edgeConstructorFunctions;

        protected Dictionary<Predicate<object>, Func<Vertex, Vertex, bool, object, Edge>> EdgeConstructorFunctions
        {
            get
            {
                return this.edgeConstructorFunctions;
            }
        }

        public EdgeFactory()
        {
            this.edgeConstructorFunctions = new Dictionary<Predicate<object>, Func<Vertex, Vertex, bool, object, Edge>>();
            this.RegisterConstructorFunctions();
        }

        protected virtual void RegisterConstructorFunctions()
        {
            this.edgeConstructorFunctions.Add(
                (o) => { return o == null; },
                (fv, sv, id, o) => { return new UnweightedEdge(fv, sv, id); }
            );

            this.edgeConstructorFunctions.Add(
                (o) => { return o is double? || o is double; },
                (fv, sv, id, o) => { return new DoubleValueEdge(fv, sv, id, (double?)o); }
            );
        }

        public Edge CreateEdge(Vertex firstVertex, Vertex secondVertex, bool isDirected, object value)
        {
            foreach (var pair in this.edgeConstructorFunctions)
            {
                var predicate = pair.Key;
                if (predicate(value))
                {
                    var constructor = pair.Value;
                    return constructor(firstVertex, secondVertex, isDirected, value);
                }
            }

            return new ObjectValueEdge(firstVertex, secondVertex, isDirected, value);
        }
    }
}

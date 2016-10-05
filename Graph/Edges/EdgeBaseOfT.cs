using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public class EdgeBase<T> : Edge
    {
        private T value;

        public T Value
        {
            get
            {
                return this.value;
            }
        }

        public override object ValueAsObject
        {
            get
            {
                return this.value as object;
            }
        }

        protected EdgeBase(Vertex firstVertex, Vertex secondVertex, bool isDirected, T value)
            :base(firstVertex, secondVertex, isDirected)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            this.value = value;
        }
    }
}

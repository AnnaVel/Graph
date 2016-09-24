using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public class VertexBase<T> : Vertex
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

        protected VertexBase(T value)
        {
            Guard.ThrowExceptionIfNull(value, "value");

            this.value = value;
        }
    }
}

using GraphCore.Utilities;

namespace GraphCore.Vertices
{
    public class VertexBase<T> : Vertex
    {
        private readonly T value;

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

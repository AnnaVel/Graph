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
        private readonly T value;
        private readonly bool isWeighted;
        private readonly double weight;

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

        public override bool IsWeighted
        {
            get
            {
                return this.isWeighted;
            }
        }

        public override double Weight
        {
            get
            {
                return this.weight;
            }
        }

        protected EdgeBase(Vertex firstVertex, Vertex secondVertex, bool isDirected, T value)
            :base(firstVertex, secondVertex, isDirected)
        {
            this.value = value;
            this.isWeighted = value.IsNumber();
            this.weight = value.GetEdgeWeightFromObject();
        }
    }
}

﻿using GraphCore.DynamicAttributes;
using GraphCore.Utilities;
using GraphCore.Vertices;

namespace GraphCore.Edges
{
    public abstract class Edge : GraphStructureItem
    {
        private readonly EdgeDynamicAttributeList dynamicAttributeList;

        private Vertex firstVertex;
        private Vertex secondVertex;
        private bool isDirected;

        public Vertex FirstVertex
        {
            get
            {
                return this.firstVertex;
            }
        }

        public Vertex SecondVertex
        {
            get
            {
                return this.secondVertex;
            }
        }

        public bool IsDirected
        {
            get
            {
                return this.isDirected;
            }
        }

        public abstract bool IsWeighted { get; }

        public abstract double Weight { get; }

        internal override DynamicAttributeList DynamicAttributeList
        {
            get
            {
                return this.dynamicAttributeList;
            }
        }

        public Edge(Vertex firstVertex, Vertex secondVertex, bool isDirected)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            this.firstVertex = firstVertex;
            this.secondVertex = secondVertex;
            this.isDirected = isDirected;

            this.dynamicAttributeList = new EdgeDynamicAttributeList(this);
        }
    }
}

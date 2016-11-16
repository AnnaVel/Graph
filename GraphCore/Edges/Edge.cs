﻿using GraphCore.GraphItemProperties;
using GraphCore.Utilities;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Edges
{
    public abstract class Edge : GraphStructureItem
    {
        private readonly EdgePropertyList propertyList;

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

        internal override GraphItemPropertyList PropertyList
        {
            get
            {
                return this.propertyList;
            }
        }

        public Edge(Vertex firstVertex, Vertex secondVertex, bool isDirected)
        {
            Guard.ThrowExceptionIfNull(firstVertex, "firstVertex");
            Guard.ThrowExceptionIfNull(secondVertex, "secondVertex");

            this.firstVertex = firstVertex;
            this.secondVertex = secondVertex;
            this.isDirected = isDirected;

            this.propertyList = new EdgePropertyList(this);
        }
    }
}
using GraphCore.Edges;
using GraphCore.GraphItemProperties;
using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public abstract class Vertex : GraphStructureItem
    {
        private readonly VertexPropertyList propertyList;

        internal override GraphItemPropertyList PropertyList
        {
            get
            {
                return this.propertyList;
            }
        }

        public Vertex()
        {
            this.propertyList = new VertexPropertyList(this);
        }

        public IEnumerable<Vertex> GetSuccessors()
        {
            this.VerifyVertexHasOwner();

            return this.Owner.GetVertexSuccessorsWithoutValidityCheck(this);
        }

        public IEnumerable<Vertex> GetPredecessors()
        {
            this.VerifyVertexHasOwner();

            return this.Owner.GetVertexPredecessorsWithoutValidityCheck(this);
        }

        public IEnumerable<Edge> GetOutgoingEdges()
        {
            this.VerifyVertexHasOwner();

            return this.Owner.GetEdgesGoingOutOfVertexWithoutValidityCheck(this);
        }

        public IEnumerable<Edge> GetIncomingEdges()
        {
            this.VerifyVertexHasOwner();

            return this.Owner.GetEdgesComingIntoVertexWithoutValidityCheck(this);
        }

        public IEnumerable<Edge> GetEdgesTo(Vertex successor)
        {
            Guard.ThrowExceptionIfNull(successor, "successor");
            this.VerifyVertexHasOwner();

            IEnumerable<Edge> successorEdges = this.Owner.GetEdgesLeadingFromTo(this, successor);
            return successorEdges;
        }

        public IEnumerable<Edge> GetEdgesFrom(Vertex predecessor)
        {
            Guard.ThrowExceptionIfNull(predecessor, "predecessor");
            this.VerifyVertexHasOwner();

            IEnumerable<Edge> predecessorEdges = this.Owner.GetEdgesLeadingFromTo(predecessor, this);
            return predecessorEdges;
        }

        private void VerifyVertexHasOwner()
        {
            if (this.Owner == null)
            {
                throw new InvalidOperationException("The vertex is not registered in a graph structure.");
            }
        }

    }
}

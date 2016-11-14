using GraphCore.Utilities;
using GraphViewModel.LayoutChange;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel
{
    public class LayoutUpdateContext
    {
        public IEnumerable<VertexLayoutChange> VertexChanges { get; }
        public IEnumerable<EdgeLayoutChange> EdgeChanges { get; }

        public LayoutUpdateContext(IEnumerable<VertexLayoutChange> vertexChanges, IEnumerable<EdgeLayoutChange> edgeChanges)
        {
            Guard.ThrowExceptionIfNull(vertexChanges, "vertexChanges");
            Guard.ThrowExceptionIfNull(edgeChanges, "edgeChanges");

            this.VertexChanges = vertexChanges;
            this.EdgeChanges = edgeChanges;
        }
    }
}

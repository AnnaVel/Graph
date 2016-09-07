using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Vertices
{
    public abstract class Vertex
    {
        public abstract object ValueAsObject { get; }
        public abstract IEnumerable<Vertex> GetSuccessors();
    }
}

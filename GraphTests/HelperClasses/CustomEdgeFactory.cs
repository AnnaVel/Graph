using GraphCore.Edges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomEdgeFactory : EdgeFactory
    {
        protected override void RegisterConstructorFunctions()
        {
            this.EdgeConstructorFunctions.Add(
                 (v) => { return v is string && (string)v == "customEdge"; },
                 (fv, sv, id, v) => { return new CustomValueEdge(fv, sv, id, (string)v); });

            base.RegisterConstructorFunctions();
        }
    }
}

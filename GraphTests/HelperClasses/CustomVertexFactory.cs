using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomVertexFactory : VertexFactory
    {
        protected override void RegisterConstructorFunctions()
        {
            this.VertexConstructorFunctions.Add(
                 (o) => { return o is string && (string)o == "customVertex"; },
                 (o) => { return new CustomValueVertex(); });

            base.RegisterConstructorFunctions();
        }
    }
}

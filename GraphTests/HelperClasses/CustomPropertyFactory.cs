using GraphCore.VertexProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomPropertyFactory : GraphItemPropertyFactory
    {
        protected override void RegisterConstructorFunctions()
        {
            this.VertexPropertyConstructorFunctions.Add(
                (value) =>
                {
                    return true;
                },
                (name, value) =>
                {
                    return new CustomProperty();
                });
        }
    }
}

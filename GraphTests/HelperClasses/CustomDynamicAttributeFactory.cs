using GraphCore.DynamicAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphTests
{
    public class CustomDynamicAttributeFactory : DynamicAttributeFactory
    {
        protected override void RegisterConstructorFunctions()
        {
            this.DynamicAttributeConstructorFunctions.Add(
                (value) =>
                {
                    return true;
                },
                (name, value) =>
                {
                    return new CustomDynamicAttribute();
                });
        }
    }
}

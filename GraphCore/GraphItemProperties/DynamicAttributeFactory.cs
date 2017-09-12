using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.GraphItemProperties
{
    public class DynamicAttributeFactory
    {
        private readonly Dictionary<Predicate<object>, Func<string, object, IDynamicAttribute>> dynamicAttributeConstructorFunctions;

        protected Dictionary<Predicate<object>, Func<string, object, IDynamicAttribute>> DynamicAttributeConstructorFunctions
        {
            get
            {
                return this.dynamicAttributeConstructorFunctions;
            }
        }

        public DynamicAttributeFactory()
        {
            this.dynamicAttributeConstructorFunctions = new Dictionary<Predicate<object>, Func<string, object, IDynamicAttribute>>();

            this.RegisterConstructorFunctions();
        }

        public IDynamicAttribute CreateDynamicAttribute(string name, object value)
        {
            foreach (var pair in this.dynamicAttributeConstructorFunctions)
            {
                Predicate<object> predicate = pair.Key;

                if (predicate(value))
                {
                    var constructor = pair.Value;
                    return constructor(name, value);
                }
            }

            return new ObjectDynamicAttribute(name, value);
        }

        protected virtual void RegisterConstructorFunctions()
        {
            this.dynamicAttributeConstructorFunctions.Add(
                (value) =>
                {
                    return value is string;
                },
                (name, value) =>
                {
                    return new StringDynamicAttribute(name, (string)value);
                });

            this.dynamicAttributeConstructorFunctions.Add(
                (value) =>
                {
                    return value is bool;
                },
                (name, value) =>
                {
                    return new BooleanDynamicAttribute(name, (bool)value);
                });

            this.dynamicAttributeConstructorFunctions.Add(
                (value) =>
                {
                    return value is int;
                },
                (name, value) =>
                {
                    return new IntegerDynamicAttribute(name, (int)value);
                });

            this.dynamicAttributeConstructorFunctions.Add(
                (value) =>
                {
                    return value is double;
                },
                (name, value) =>
                {
                    return new DoubleDynamicAttribute(name, (double)value);
                });
        }
    }
}

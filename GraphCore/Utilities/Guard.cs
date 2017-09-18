using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Utilities
{
    internal class Guard
    {
        public static void ThrowExceptionIfNull(object parameter, string parameterName)
        {
            if (parameter == null)
            {
                throw new ArgumentNullException(parameterName, string.Format("{0} cannot be null.", parameterName));
            }
        }

        public static void ThrowExceptionIfNullOrEmpty(string parameter, string parameterName)
        {
            if (parameter == string.Empty)
            {
                throw new ArgumentException(parameterName, string.Format("{0} cannot be empty.", parameterName));
            }

            Guard.ThrowExceptionIfNull(parameter, parameterName);
        }

        public static void ThrowExceptionIfNotOfType(object parameter, string parameterName, Type type)
        {
            Type parameterType = parameter.GetType();
            bool isOfType = parameterType == type || parameterType.IsSubclassOf(type);

            if (!isOfType)
            {
                throw new ArgumentException(parameterName, string.Format("{0} is not of type {1}.", parameterName, type));
            }
        }

        public static void ThrowExceptionIfNotEqual(object parameter1, object parameter2, string parameterName1, string parameterName2)
        {
            if(!parameter1.Equals(parameter2))
            {
                throw new ArgumentException(string.Format("(0) and {1} are not equal.", parameterName1, parameterName2));
            }
        }

        public static void ThrowExceptionIfTypeIsNotEqualOrSubtypeOf(Type subtype, Type supertype)
        {
            if(!(subtype.Equals(supertype) || subtype.IsSubclassOf(supertype)))
            {
                throw new ArgumentException(string.Format("(0) is not subtype or equal to {1}.", subtype.Name, supertype.Name));
            }
        }
    }
}

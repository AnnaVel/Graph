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
            if(parameter.GetType() != type)
            {
                throw new ArgumentException(parameterName, string.Format("{0} is not of type {1}.", parameterName, type));
            }
        }
    }
}

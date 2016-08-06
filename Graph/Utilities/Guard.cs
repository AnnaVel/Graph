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
            if (string.IsNullOrEmpty(parameter))
            {
                throw new ArgumentException(parameterName, string.Format("{0} cannot be null or empty.", parameterName));
            }
        }
    }
}

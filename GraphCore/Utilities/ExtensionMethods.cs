using GraphCore.Edges;
using GraphCore.Vertices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Utilities
{
    public static class ExtensionMethods
    {
        public static bool IsNumber(this object value)
        {
            return value is double
            || value is int
            || value is float
            || value is decimal
            || value is uint
            || value is long
            || value is ulong
            || value is short
            || value is ushort
            || value is sbyte
            || value is byte;
        }

        public static double GetEdgeWeightFromObject(this object value)
        {
            if (value is double)
            {
                return (double)value;
            }
            else if(value is int)
            {
                return (int)value;
            }
            else if(value is decimal)
            {
                return (double)(decimal)value;
            }
            else if (value is float)
            {
                return (float)value;
            }
            else if (value is uint)
            {
                return (uint)value;
            }
            else if (value is long)
            {
                return (long)value;
            }
            else if (value is ulong)
            {
                return (ulong)value;
            }
            else if (value is short)
            {
                return (short)value;
            }
            else if (value is ushort)
            {
                return (ushort)value;
            }
            else if (value is byte)
            {
                return (byte)value;
            }
            else if (value is sbyte)
            {
                return (sbyte)value;
            }
            else
            {
                return Edge.UnweightedEdgeDefaultWeight;
            }
        }
    }
}

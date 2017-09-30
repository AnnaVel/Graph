using GraphCore.Utilities;
using GraphCore.Vertices;
using System.Collections.Generic;

namespace GraphCore.Algorithms
{
    public class DijkstraResult
    {
        bool resultIsValid;

        public bool ResultIsValid
        {
            get
            {
                return this.resultIsValid;
            }
        }

        public DijkstraResult(bool resultIsValid)
        {
            this.resultIsValid = resultIsValid;
        }
    }
}

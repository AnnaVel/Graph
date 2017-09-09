using GraphCore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCore.Algorithms
{
    public class AlgorithmLibrary
    {
        private readonly GraphStructure graphStructure;
        private readonly Dictionary<string, IAlgorithm> algorithmList;

        public AlgorithmLibrary(GraphStructure graphStructure)
        {
            Guard.ThrowExceptionIfNull(graphStructure, "graphStructure");

            this.graphStructure = graphStructure;
            this.algorithmList = new Dictionary<string, IAlgorithm>();
            this.RegisterInBuiltAlgorithms();
        }

        public R ExecuteAlgorithm<R, P>(string name, P parameter)
        {
            return (R)this.ExecuteAlgorithm(name, parameter);
        }

        public object ExecuteAlgorithm(string name, object parameter)
        {
            IAlgorithm algorithm = this.algorithmList[name];

            return algorithm.ExecuteBase(this.graphStructure, parameter);
        }

        public void RegisterAlgorithm(string name, IAlgorithm algorithm)
        {
            this.algorithmList.Add(name, algorithm);
        }

        public bool UnregisterAlgorithm(string name)
        {
            return this.algorithmList.Remove(name);
        }

        public bool ContainsAlgorithm(string name)
        {
            return this.algorithmList.ContainsKey(name);
        }

        public IEnumerable<string> EnumerateAlgorithmNames()
        {
            return this.algorithmList.Keys;
        }

        public IAlgorithm GetAlgorithm(string name)
        {
            return this.algorithmList[name];
        }

        private void RegisterInBuiltAlgorithms()
        {
            this.algorithmList.Add(AlgorithmNames.FindNextVertexName, new FindNextVertex());
        }
    }
}

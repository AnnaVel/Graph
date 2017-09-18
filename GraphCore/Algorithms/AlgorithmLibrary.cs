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
        private Dictionary<string, IAlgorithm> algorithmList;

        public AlgorithmLibrary(GraphStructure graphStructure)
        {
            Guard.ThrowExceptionIfNull(graphStructure, "graphStructure");

            this.graphStructure = graphStructure;
            this.algorithmList = new Dictionary<string, IAlgorithm>();

            this.RegisterInBuiltAlgorithms();
        }

        public object ExecuteBase(string algorithmName, object parameter)
        {
            if(!this.algorithmList.ContainsKey(algorithmName))
            {
                throw new ArgumentException("This algorithm is not present in the library");
            }

            return this.algorithmList[algorithmName].ExecuteBase(this.graphStructure, parameter);
        }

        public R Execute<P, R>(string algorithmName, P parameter)
        {
            if (!this.ContainsAlgorithm(algorithmName))
            {
                throw new ArgumentException("This algorithm is not present in the library");
            }

            IAlgorithm algorithm = this.algorithmList[algorithmName];

            Guard.ThrowExceptionIfTypeIsNotEqualOrSubtypeOf(typeof(P), algorithm.ParameterType);
            Guard.ThrowExceptionIfTypeIsNotEqualOrSubtypeOf(algorithm.ResultType, typeof(R));

            R result = (R)this.ExecuteBase(algorithmName, parameter);

            return result;
        }

        public void RegisterAlgorithm(IAlgorithm algorithm)
        {
            if(this.ContainsAlgorithm(algorithm.Name))
            {
                throw new ArgumentException("This algorithm is already registered.");
            }

            this.algorithmList.Add(algorithm.Name, algorithm);
        }

        public bool UnregisterAlgorithm(IAlgorithm algorithm)
        {
            return this.algorithmList.Remove(algorithm.Name);
        }

        public void ClearDynamicAttributesSetByAlgorithm(string name)
        {
            if (!this.ContainsAlgorithm(name))
            {
                throw new ArgumentException("This algorithm is not present in the library.");
            }

            IAlgorithm algorithm = this.algorithmList[name];

            algorithm.ClearAllSetDynamicAttributes(this.graphStructure);
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
            if (!this.ContainsAlgorithm(name))
            {
                throw new ArgumentException("This algorithm is not present in the library.");
            }

            return this.algorithmList[name];
        }

        public void ToggleAlgorithmsShouldSetDynamicAttributesToItems(bool shouldSet)
        {
            foreach(IAlgorithm algorithm in this.algorithmList.Values)
            {
                algorithm.SetDynamicAttributesInStructure = shouldSet;
            }
        }

        private void RegisterInBuiltAlgorithms()
        {
            this.algorithmList.Add(AlgorithmNames.DijkstraRouteAlgorithmName, new DijkstraRouteAlgorithm());
            this.algorithmList.Add(AlgorithmNames.DijkstraCompleteTraversalAlgorithmName, new DijkstraCompleteTraversalAlgorithm());
        }
    }
}

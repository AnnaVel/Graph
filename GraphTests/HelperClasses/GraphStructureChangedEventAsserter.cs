using GraphCore;
using GraphCore.Events;
using GraphCore.Utilities;
using System;
using System.Collections.Generic;

namespace GraphTests
{
    internal class GraphStructureChangedEventAsserter
    {
        private List<GraphStructureChangeDescriptor> firedChanges;
        private List<GraphStructureChangeDescriptor> expectedChanges;

        public GraphStructureChangedEventAsserter(Graph eventObject)
        {
            Guard.ThrowExceptionIfNull(eventObject, "eventObject");

            this.firedChanges = new List<GraphStructureChangeDescriptor>();
            this.expectedChanges = new List<GraphStructureChangeDescriptor>();
            eventObject.GraphStructure.GraphStructureChanged += GraphStructure_GraphStructureChanged;
        }

        public void AddExpectedChange(ChangeAction changeAction, GraphStructureItem structureItem)
        {
            this.expectedChanges.Add(new GraphStructureChangeDescriptor(changeAction, structureItem));
        }

        public void AssertFiredChanges()
        {
            TestHelper.AssertEnumerablesAreEqual(this.expectedChanges, this.firedChanges);
        }

        private void GraphStructure_GraphStructureChanged(GraphStructureChangedEventArgs args)
        {
            foreach (GraphStructureItem itemChanged in args.ItemsChanged)
            {
                firedChanges.Add(new GraphStructureChangeDescriptor(args.ChangeAction, itemChanged));
            }
        }
    }
}

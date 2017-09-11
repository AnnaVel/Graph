using GraphCore.Events;
using GraphCore.GraphItemProperties;
using GraphCore.Utilities;
using GraphViewModel.Events;
using System;
using System.Collections.Generic;
using System.Windows.Media;
using static GraphCore.GraphItemProperties.GraphItemPropertyList;

namespace GraphViewModel.ViewModels
{
    public delegate void GraphViewModelPropertyChangedEventHandler(GraphViewModelPropertyChangedEventArgs args);
    public abstract class GraphItemViewModel : NotifyPropertyChangedViewModel
    {
        private readonly object relatedItem;
        private readonly Dictionary<string, object> graphPropertyValues;
        private readonly PropertyPriorityCalculator propertyPriority;
        private readonly GraphItemPropertyChangedEventHandler graphPropertyChangedEventHandler;

        protected object RelatedItemAsObject
        {
            get
            {
                return this.relatedItem;
            }
        }

        protected Dictionary<string, object> GraphPropertyValues
        {
            get
            {
                return this.graphPropertyValues;
            }
        }

        protected GraphItemViewModel(object relatedItem)
        {
            Guard.ThrowExceptionIfNull(relatedItem, "relatedItem");

            this.relatedItem = relatedItem;
            this.graphPropertyChangedEventHandler = new GraphItemPropertyChangedEventHandler(this.OnRelatedItemPropertyChanged);

            this.SubscribeToRelatedItemEvents(this.graphPropertyChangedEventHandler);
            this.CalculatePropertyValues();

            this.graphPropertyValues = new Dictionary<string, object>();
            this.propertyPriority = new PropertyPriorityCalculator();
        }

        public void ReleaseRelatedItem()
        {
            this.UnsubscribeFromRelatedItemEvents(this.graphPropertyChangedEventHandler);
        }

        public object GetValueForProperty(string propertyName)
        {
            if(!this.graphPropertyValues.ContainsKey(propertyName))
            {
                //object propertyValue = this.RecalculatePropertyValue(propertyName);
                //this.graphPropertyValues[propertyName] = propertyValue;
                return null;
            }

            return this.graphPropertyValues[propertyName];
        }

        public event GraphViewModelPropertyChangedEventHandler GraphItemViewModelPropertyChanged;

        protected abstract void SubscribeToRelatedItemEvents(GraphItemPropertyChangedEventHandler handler);

        protected abstract void UnsubscribeFromRelatedItemEvents(GraphItemPropertyChangedEventHandler handler);

        protected abstract IEnumerable<string> EnumeratePropertyValuesForRelatedItem();

        protected abstract object RecalculatePropertyValue(string propertyName);

        private void CalculatePropertyValues()
        {
            foreach (string propertyName in EnumeratePropertyValuesForRelatedItem())
            {
                this.GraphPropertyValues[propertyName] = this.RecalculatePropertyValue(propertyName);
            }
        }

        private void OnRelatedItemPropertyChanged(GraphItemPropertyChangedEventArgs args)
        {
            string propertyName;
            string prefix;
            this.propertyPriority.RecordPropertyChangeAndGetPropertyNameToVisualize(args, out prefix, out propertyName);

            bool propertyValueChanged = false;
            object recalculatedValue = this.RecalculatePropertyValue(propertyName);

            if(!this.graphPropertyValues.ContainsKey(prefix))
            {
                this.graphPropertyValues[prefix] = recalculatedValue;
                propertyValueChanged = true;
            }
            else if(this.graphPropertyValues[prefix] != recalculatedValue)
            {
                this.graphPropertyValues[propertyName] = recalculatedValue;
                propertyValueChanged = true;
            }

            if(propertyValueChanged)
            {
                this.OnGraphPropertyValueChanged(prefix);
            }
        }



        private void OnGraphPropertyValueChanged(string propertyName)
        {
            if(this.GraphItemViewModelPropertyChanged != null)
            {
                this.GraphItemViewModelPropertyChanged(new GraphViewModelPropertyChangedEventArgs(propertyName));
            }
        }
    }
}

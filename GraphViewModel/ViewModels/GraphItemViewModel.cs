using GraphCore.Events;
using GraphCore.DynamicAttributes;
using GraphCore.Utilities;
using GraphViewModel.Events;
using System;
using System.Collections.Generic;
using static GraphCore.DynamicAttributes.DynamicAttributeList;

namespace GraphViewModel.ViewModels
{
    public delegate void ViewModelDynamicAttributeChangedEventHandler(ViewModelDynamicAttributeChangedEventArgs args);
    public abstract class GraphItemViewModel : NotifyPropertyChangedViewModel
    {
        private readonly object relatedItem;
        private readonly Dictionary<string, object> dynamicAttributeValues;
        private readonly DynamicAttributeChangedEventHandler dynamicAttributeChangedEventHandler;

        protected object RelatedItemAsObject
        {
            get
            {
                return this.relatedItem;
            }
        }

        protected Dictionary<string, object> DynamicAttributeValues
        {
            get
            {
                return this.dynamicAttributeValues;
            }
        }

        protected GraphItemViewModel(object relatedItem)
        {
            Guard.ThrowExceptionIfNull(relatedItem, "relatedItem");

            this.relatedItem = relatedItem;
            this.dynamicAttributeChangedEventHandler = new DynamicAttributeChangedEventHandler(this.OnRelatedItemDynamicAttributeChanged);

            this.SubscribeToRelatedItemEvents(this.dynamicAttributeChangedEventHandler);
            this.CalculateDynamicAttributeValues();

            this.dynamicAttributeValues = new Dictionary<string, object>();
        }

        public void ReleaseRelatedItem()
        {
            this.UnsubscribeFromRelatedItemEvents(this.dynamicAttributeChangedEventHandler);
        }

        public object GetValueForDynamicAttribute(string attributeName)
        {
            if(!this.dynamicAttributeValues.ContainsKey(attributeName))
            {
                object attributeValue = this.RecalculateDynamicAttributeValue(attributeName);
                this.dynamicAttributeValues[attributeName] = attributeValue;
            }

            return this.dynamicAttributeValues[attributeName];
        }

        public event ViewModelDynamicAttributeChangedEventHandler ViewModelDynamicAttributeChanged;

        protected abstract void SubscribeToRelatedItemEvents(DynamicAttributeChangedEventHandler handler);

        protected abstract void UnsubscribeFromRelatedItemEvents(DynamicAttributeChangedEventHandler handler);

        protected abstract IEnumerable<string> EnumerateDynamicAttributeNamesForRelatedItem();

        protected abstract object RecalculateDynamicAttributeValue(string attributeName);

        private void CalculateDynamicAttributeValues()
        {
            foreach (string attributeName in EnumerateDynamicAttributeNamesForRelatedItem())
            {
                this.DynamicAttributeValues[attributeName] = this.RecalculateDynamicAttributeValue(attributeName);
            }
        }

        private void OnRelatedItemDynamicAttributeChanged(DynamicAttributeChangedEventArgs args)
        {
            string attributeName = args.DynamicAttributeName;

            bool viewModelAttributeValueChanged = false;
            object recalculatedValue = this.RecalculateDynamicAttributeValue(attributeName);

            if(!this.dynamicAttributeValues.ContainsKey(attributeName))
            {
                this.dynamicAttributeValues[attributeName] = recalculatedValue;
                viewModelAttributeValueChanged = true;
            }
            else if(this.dynamicAttributeValues[attributeName] != recalculatedValue)
            {
                this.dynamicAttributeValues[attributeName] = recalculatedValue;
                viewModelAttributeValueChanged = true;
            }

            if(viewModelAttributeValueChanged)
            {
                this.OnViewModelDynamicAttributeValueChanged(attributeName);
            }
        }

        private void OnViewModelDynamicAttributeValueChanged(string attributeName)
        {
            if(this.ViewModelDynamicAttributeChanged != null)
            {
                this.ViewModelDynamicAttributeChanged(new ViewModelDynamicAttributeChangedEventArgs(attributeName));
            }
        }
    }
}

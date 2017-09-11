using GraphCore.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphViewModel.ViewModels
{
    internal class PropertyPriorityCalculator
    {
        private Dictionary<string, List<string>> propertyHistory;

        public PropertyPriorityCalculator()
        {
            this.propertyHistory = new Dictionary<string, List<string>>();
        }

        public void RecordPropertyChangeAndGetPropertyNameToVisualize(GraphItemPropertyChangedEventArgs args, out string prefix, out string prioritizedProperty)
        {
            string propertyName = args.PropertyName;
            PropertyChangeAction changeAction = args.ChangeAction;

            prefix = GetPrefixFromName(propertyName);
            this.RecordPropertyChange(prefix, propertyName, changeAction);
            prioritizedProperty = this.GetPrioritizedPropertyName(prefix);
        }

        private string GetPrefixFromName(string propertyName)
        {
            bool isComplex = this.IsNameComplex(propertyName);
            string prefix;

            if (!isComplex)
            {
                prefix = propertyName;
            }
            else
            {
                prefix = this.GetPrefixFromComplexName(propertyName);
            }

            return prefix;
        }


        private string GetPrioritizedPropertyName(string prefix)
        {
            string result;

            if(this.propertyHistory.ContainsKey(prefix))
            {
                if(this.propertyHistory[prefix].Count == 0)
                {
                    throw new InvalidOperationException("If the list is empty, it should have been removed.");
                }

                result = this.propertyHistory[prefix].Last();
            }
            else
            {
                result = prefix;
            }

            return result;
        }

        private void RecordPropertyChange(string prefix, string propertyName, PropertyChangeAction changeAction)
        {
            if (changeAction == PropertyChangeAction.Set)
            {
                if (!this.propertyHistory.ContainsKey(prefix))
                {
                    this.propertyHistory.Add(prefix, new List<string>());
                }

                this.propertyHistory[prefix].Add(propertyName);
                this.RemoveRepetitions(this.propertyHistory[prefix]);
            }
            else if(changeAction == PropertyChangeAction.Remove)
            {
                if(this.propertyHistory.ContainsKey(prefix))
                {
                    this.propertyHistory[prefix].Remove(propertyName);

                    if(this.propertyHistory[prefix].Count == 0)
                    {
                        this.propertyHistory.Remove(prefix);
                    }
                    else
                    {
                        this.RemoveRepetitions(this.propertyHistory[prefix]);
                    }
                }
            }
            else
            {
                throw new NotSupportedException();
            }
        }

        private string GetPrefixFromComplexName(string propertyName)
        {
            int indexOfSeparator = propertyName.IndexOf(':');

            return propertyName.Substring(0, indexOfSeparator);
        }

        private bool IsNameComplex(string propertyName)
        {
            return propertyName.Contains(":");
        }

        private void RemoveRepetitions(List<string> list)
        {
           // for()
        }
    }
}

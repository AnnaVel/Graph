using GraphView.Views;
using GraphViewModel.Events;
using GraphViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace GraphView.Utilities
{
    /// <summary>
    /// The extension tries to retrieve a value from the property dictionary of the view model.
    /// If the value is not found, the default value is used. After that,
    /// if a converter has been specified, it is applied.
    /// 
    /// The extension relies on caching the target object in order to update its value,
    /// so setting the Style in which it is used to x:Shared=false is essential.
    /// Not setting x:Shared to false might lead to unexpected behavior.
    /// </summary>
    [MarkupExtensionReturnType()]
    public class GraphPropertyExtension : MarkupExtension
    {
        private IValueConverter converter;

        private object targetObject;
        private object targetProperty;

        public object PropertyName { get; set; }

        public IValueConverter Converter
        {
            get
            {
                return this.converter;
            }
            set
            {
                this.converter = value;
            }
        }

        public object DefaultValue { get; set; }

        public GraphPropertyExtension()
        {
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            IProvideValueTarget ipvt =
                (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

            if (ipvt.TargetObject.GetType().FullName == "System.Windows.SharedDp")
            {
                return this;
            }

            this.targetObject = ipvt.TargetObject;
            this.targetProperty = ipvt.TargetProperty;

            GraphItemViewModel graphItemViewModel = this.GetAssociatedGraphItemViewModel();
            graphItemViewModel.GraphItemViewModelPropertyChanged += GraphItemViewModel_GraphItemViewModelPropertyChanged;

            return this.GetGraphPropertyValue();
        }

        private object GetGraphPropertyValue()
        {
            GraphItemViewModel graphItemViewModel = this.GetAssociatedGraphItemViewModel();

            if (graphItemViewModel == null)
            {
                return null;
            }

            object result = graphItemViewModel.GetValueForProperty(this.PropertyName as string);

            if(result == null)
            {
                result = this.DefaultValue;
            }

            if (this.Converter != null)
            {
                return this.Converter.Convert(result, typeof(object), null, null);
            }
            else
            {
                return result;
            }
        }

        private GraphItemViewModel GetAssociatedGraphItemViewModel()
        {
            GraphItemView targetView = (targetObject as FrameworkElement).TemplatedParent as GraphItemView;

            if (targetView == null)
            {
                throw new InvalidOperationException("This markup extension cannot be used with targets other than GraphItemView.");
            }

            if(targetView.DataContext == null)
            {
                return null;
            }

            GraphItemViewModel graphItemViewModel = targetView.DataContext as GraphItemViewModel;
            
            if(graphItemViewModel == null)
            {
                throw new InvalidOperationException("The data context of the graph item view is supposed to be graph item view model.");
            }

            return graphItemViewModel;
        }

        private void UpdateValue()
        {
            if (this.targetObject != null)
            {
                object value = this.GetGraphPropertyValue();

                if (this.targetProperty is DependencyProperty)
                {
                    DependencyObject obj = this.targetObject as DependencyObject;
                    DependencyProperty prop = this.targetProperty as DependencyProperty;

                    Action updateAction = () => obj.SetValue(prop, value);

                    // Check whether the target object can be accessed from the
                    // current thread, and use Dispatcher.Invoke if it can't

                    if (obj.CheckAccess())
                    {
                        updateAction();
                    }
                    else
                    {
                        obj.Dispatcher.Invoke(updateAction);
                    }

                }
                else // _targetProperty is PropertyInfo
                {
                    PropertyInfo prop = this.targetProperty as PropertyInfo;
                    prop.SetValue(this.targetObject, value, null);
                }
            }
        }

        private void GraphItemViewModel_GraphItemViewModelPropertyChanged(GraphViewModelPropertyChangedEventArgs args)
        {
            if (this.PropertyName as string == args.PropertyName)
            {
                this.UpdateValue();
            }
        }
    }

}

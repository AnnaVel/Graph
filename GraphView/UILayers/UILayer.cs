using GraphCore.Utilities;
using GraphViewModel;
using GraphViewModel.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GraphView.UILayers
{
    internal abstract class UILayer
    {
        private readonly Canvas mainContainer;
        public UILayer(Canvas container)
        {
            Guard.ThrowExceptionIfNull(container, "container");

            this.mainContainer = container;
        }
        public abstract void UpdateUI(LayoutUpdateContext updateContext);

        protected void AddElementToUI(UIElement element)
        {
            this.mainContainer.Children.Add(element);
        }

        protected void RemoveElementFromUI(UIElement element)
        {
            this.mainContainer.Children.Remove(element);
        }
    }
}

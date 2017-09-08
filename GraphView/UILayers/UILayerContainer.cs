using GraphCore.Utilities;
using GraphViewModel;
using GraphViewModel.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GraphView.UILayers
{
    internal class UILayerContainer
    {
        private List<UILayer> layers;

        public UILayerContainer(Canvas mainContainer)
        {
            Guard.ThrowExceptionIfNull(mainContainer, "mainContainer");

            this.layers = new List<UILayer>();
            this.layers.Add(new EdgeUILayer(mainContainer));
            this.layers.Add(new VertexUILayer(mainContainer));
        }
        public void UpdateUI(LayoutUpdateContext updateContext)
        {
            foreach(UILayer uiLayer in this.layers)
            {
                uiLayer.UpdateUI(updateContext);
            }
        }
    }
}

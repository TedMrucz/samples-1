using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using VGTrader.Common;

namespace MenuHub.Models
{
    [Export(typeof(MenuHubModel))]
    public class MenuHubModel
    {
        private IViewContext viewContext = null;

        [ImportingConstructor]
        public MenuHubModel(IViewContext viewContext)
        {
            this.viewContext = viewContext;
        }
    }
}

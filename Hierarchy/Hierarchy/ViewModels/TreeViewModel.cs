using Hierarchy.Interfaces;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Prism.Mvvm;
using Hierarchy.Models;


namespace Hierarchy.ViewModels
{
    [Export("TreeViewModel", typeof(IViewModelBase))]
    public class TreeViewModel : BindableBase, IViewModelBase
    {
        private IModel Model { get; set; }

        private IList<TreeItem> items;
        public IList<TreeItem> Items
        {
            get { return items; }
            set { SetProperty(ref items, value); }
        }

        [ImportingConstructor]
        public TreeViewModel([Import("TreeModel")]IModel model)
        {
            Model = model;
            Items = model.Items;
        }
    }
}

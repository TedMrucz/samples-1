using Hierarchy.Interfaces;
using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace Hierarchy.Views
{
    [Export("Tree", typeof(IViewBase))]
    public partial class Tree : UserControl, IViewBase
    {
        [ImportingConstructor]
        public Tree([Import("TreeViewModel")]IViewModelBase viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}

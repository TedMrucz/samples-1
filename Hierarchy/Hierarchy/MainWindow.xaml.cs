using Hierarchy.Interfaces;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Windows;
using Prism.Regions;


namespace Hierarchy
{
    [Export(typeof(IShellWnd))]
    public partial class MainWindow : Window, IShellWnd
    {
        [Import]
        private IRegionManager regionManager = null;

        private CompositionContainer container = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        public CompositionContainer Container
        {
            set { this.container = value; }
        }

        public virtual Window Shell
        {
            get { return this; }
        }
    }
}

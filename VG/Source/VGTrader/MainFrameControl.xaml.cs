using System.Windows;
using System.Windows.Controls;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Primitives;
using System.ComponentModel.Composition.Hosting;
using Prism.Regions;

namespace VGTrader
{
	[Export(typeof(IShellCtrl))]
	public partial class MainFrameControl : UserControl, IShellCtrl
    {
        [Import]
		private IRegionManager regionManager = null;

        private CompositionContainer container = null;

		public MainFrameControl() 
        {
            InitializeComponent();
        }

        private void OnControlLoaded(object sender, RoutedEventArgs e)
        {
			if (this.regionManager != null)
            {
				this.regionManager.RequestNavigate("MainRegion", "MenuHubViewModel");
            }
        }

        public void OnRequestNavigateCompletted(NavigationResult nr)
        {

        }

        protected override void OnContentChanged(object oldContent, object newContent)
        {
            base.OnContentChanged(oldContent, newContent);
        }

        public CompositionContainer Container
        {
            set { this.container = value; }
        }
       
        public virtual UserControl Shell
        {
            get { return this; }
        }
    }

	///////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////
    public interface IShellCtrl
    {
        UserControl Shell { get; }
        CompositionContainer Container { set; }
		object DataContext { set; }
    }
}

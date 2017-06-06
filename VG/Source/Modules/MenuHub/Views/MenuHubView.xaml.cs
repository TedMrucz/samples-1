using System.ComponentModel.Composition;
using VGTrader.Common;
using VGTrader.Library;

namespace MenuHub.Views
{
	/// <summary>
	/// Interaction logic for MenuHubView.xaml
	/// </summary>
	[Export("MenuHubView", typeof(IViewBase))]
	public partial class MenuHubView : ViewBase
	{
		public MenuHubView()
		{
			InitializeComponent();
		}
	}
}

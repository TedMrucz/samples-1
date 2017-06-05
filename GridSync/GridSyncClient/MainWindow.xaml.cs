using System.Windows;

namespace GridSyncClient
{
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			DataContext = new ClientViewModel(Dispatcher);
		}
	}
}

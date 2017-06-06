using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using VGTrader.Entities;

namespace VGTrader
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		private void OnStartApp(object sender, StartupEventArgs e)
		{
			string val = ConfigurationManager.AppSettings["ForceAuthentication"];
			if (val.Equals("true"))
			{
				Application.Current.MainWindow = new LoginBox();
				Application.Current.MainWindow.DataContext = new FrameContext();
				Application.Current.MainWindow.Show();
			}
			else
			{
				Application.Current.MainWindow = new Window();
				ModularBootstrapper bootstrapper = new ModularBootstrapper(new CurrentUserParam("Guest", 1, "admin"));
				bootstrapper.Run();
			}
		}

		private void OnExit(object sender, ExitEventArgs e)
		{
			//if (serviceHost!= null)
			//	serviceHost.Close();
		}

	}
}

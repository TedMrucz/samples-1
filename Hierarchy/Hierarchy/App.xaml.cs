using System.Windows;

namespace Hierarchy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartApp(object sender, StartupEventArgs e)
        {
            ModularBootstrapper bootstrapper = new ModularBootstrapper();
            bootstrapper.Run();
        }
    }
}

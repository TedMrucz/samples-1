
using System.Windows;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using Prism.Mef;
using Prism.Modularity;
using Hierarchy.Interfaces;


namespace Hierarchy
{
    public class ModularBootstrapper : MefBootstrapper, IPartImportsSatisfiedNotification
    {
        [Import]
        protected IShellViewModel mainFrameWndModel = null;

        public ModularBootstrapper()
        {
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

            this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModularBootstrapper).Assembly));
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
            return this.Container.GetExportedValue<IShellWnd>() as DependencyObject;
        }

        public virtual Window ShellCtrl
        {
            get { return ((IShellWnd)this.Shell).Shell; }
        }

        public void OnImportsSatisfied()
        {
        }

        protected override void InitializeShell()
        {
            base.Container.SatisfyImportsOnce(this);

            base.InitializeShell();

            ((IShellWnd)this.ShellCtrl).Container = base.Container;
            this.ShellCtrl.DataContext = this.mainFrameWndModel;

            Application.Current.MainWindow.Show();
        }
    }
}

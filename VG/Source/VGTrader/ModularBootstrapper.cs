using Glass.Regions;
using Glass.UX.Windows.Controls;
using Microsoft.Practices.ServiceLocation;
using Prism.Mef;
using Prism.Mef.Events;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using VGTrader.Common;
using VGTrader.Resources;
using VGTrader.Entities;
using VGTrader.Library;


namespace VGTrader
{
	public class ModularBootstrapper : MefBootstrapper, IPartImportsSatisfiedNotification
    {
        [Import]
        protected IResourceMgr resMgr = null;
		[Import]
		protected IViewContext viewContext = null;
		[Import]
		protected IEventAggregator eventAggregator = null;
		[Import]
		protected IRegionManager regionManager = null;
		[Import]
		IServiceLocator serviceLocator = null;


        public ModularBootstrapper(CurrentUserParam currentUserParamNew)
        {
            this.CurrentUserParam = currentUserParamNew;
        }

        protected override void ConfigureAggregateCatalog()
        {
            base.ConfigureAggregateCatalog();

			this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ModularBootstrapper).Assembly));
			this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(MefEventAggregator).Assembly));
			this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ResourceMgr).Assembly));
			this.AggregateCatalog.Catalogs.Add(new AssemblyCatalog(typeof(ServiceLocator).Assembly));

			DirectoryCatalog catalog = new DirectoryCatalog("DataProvider");
			DirectoryCatalog modules = new DirectoryCatalog("Modules");

			this.AggregateCatalog.Catalogs.Add(catalog);
			this.AggregateCatalog.Catalogs.Add(modules);
		}

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
			var factory = Container.GetExportedValue<IRegionBehaviorFactory>();
			factory.AddIfMissing(RegionManagerRegistrationBehavior.BehaviorKey, typeof(RegionManagerRegistrationBehavior));

			return base.ConfigureDefaultRegionBehaviors();
		}

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new ConfigurationModuleCatalog();
        }

        protected override DependencyObject CreateShell()
        {
			var shell = this.Container.GetExportedValue<VGTrader.IShellCtrl>();
            return shell as DependencyObject;
        }

        public virtual UserControl ShellCtrl
        {
            get { return ((IShellCtrl)this.Shell).Shell; }
        }

        private CurrentUserParam currentUserParam = null;
        public CurrentUserParam CurrentUserParam
        {
            set { this.currentUserParam = value; }
            get { return this.currentUserParam; }
        }

        public void OnImportsSatisfied()
        {
            //throw new NotImplementedException();
        }

        protected override void InitializeModules()
        {
            base.InitializeModules();

            this.eventAggregator.GetEvent<InitializeModules>().Publish(0);
        }

        protected override void InitializeShell()
        {
            base.Container.SatisfyImportsOnce(this);

            base.InitializeShell();

            if (this.viewContext == null)
                Trace.WriteLine("ERROR - ViewContext failed!");
            else
                this.viewContext.Initialize();

            if (this.resMgr == null)
                Trace.WriteLine("ERROR - ResourceManager failed!");
            else
            {
                Application.Current.Properties.Add("ResourceManager", resMgr.Initialize());
            }

            if (this.viewContext != null)
            {
                this.viewContext.CurrentUserParam = currentUserParam;
				//this.viewContext.Container = this.Container;
            }

            ModernWindow window = new ModernWindow();
            ((IShellCtrl)this.Shell).Container = this.Container;

            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = window;
			window.Title = resMgr.GetStringID("APP_Name");
            window.Loaded += (o, args) => 
            {
				this.ShellCtrl.DataContext = this.viewContext;
				window.ContentFrame.Content = this.ShellCtrl;
                FrameContext frameContext = new FrameContext();
				frameContext.Initialize(this.regionManager);
                window.Closing += frameContext.OnCloseWindow;
                window.NavigateBack.DataContext = frameContext;

				//IViewModelBase viewModel = this.serviceLocator.GetInstance<IViewModelBase>("StatusBarViewModel");
				//window.StatusBar.Content = viewModel.View;
            };

            window.Top = 0;
            window.Left = 0;
            window.Width = 1200;
            window.Height = 760;
            window.Topmost = false;
            Application.Current.MainWindow.Show();
        }
    }
}


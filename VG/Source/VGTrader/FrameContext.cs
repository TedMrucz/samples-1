using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using VGTrader.Entities;

namespace VGTrader
{
    public class FrameContext : BindableBase
	{
		private IRegionManager regionManager;
        public DelegateCommand WindowLoaded { get; set; }
        public DelegateCommand<object> Login { get; set; }
        public DelegateCommand Cancel { get; set; }
		public DelegateCommand BrowseBack { get; set; }

		private string userName = String.Empty;
		public string UserName
		{
			get { return this.userName; }
			set { this.userName = value; OnPropertyChanged(() => UserName); }
		}

		private string password = String.Empty;
		public string Password
		{
			get { return this.password; }
			set { this.password = value; OnPropertyChanged(() => Password); }
		}

		private string domain = "OMICORN";
		public string Domain
		{
			get { return this.domain; }
			set { this.domain = value; OnPropertyChanged(() => Domain); }
		}

		private string status = String.Empty;
		public string Status
		{
			get { return this.status; }
			set { this.status = value; OnPropertyChanged(() => Status); }
		}

		private BusinessRoleLookup role;
		public BusinessRoleLookup Role
		{
			get { return this.role; }
			set { this.role = value; OnPropertyChanged(() => Role); }
		}

		private IList<BusinessRoleLookup> roles;
		public IList<BusinessRoleLookup> Roles
		{
			get { return this.roles; }
			set { this.roles = value; OnPropertyChanged(() => Roles); }
		}

        private int logCount = -1;

		public FrameContext()
		{
            this.WindowLoaded = new DelegateCommand(OnWindowLoaded);
            this.Login = new DelegateCommand<object>(OnLogin, CanLogin);
            this.Cancel = new DelegateCommand(OnCancel);
			this.BrowseBack = new DelegateCommand(OnBrowseBack, CanBrowseBack);
		}

		public void Initialize(IRegionManager regionManager)
        {
			this.regionManager = regionManager;
			this.regionManager.Regions["MainRegion"].NavigationService.Navigated += (s, e) => BrowseBack.RaiseCanExecuteChanged();
        }

        private void OnWindowLoaded()
        {
            OnUserLogin();
        }

		private void OnBrowseBack()
		{
			this.regionManager.Regions["MainRegion"].NavigationService.Journal.GoBack();
		}

		public bool CanBrowseBack()
		{
			return this.regionManager.Regions["MainRegion"].NavigationService.Journal.CanGoBack;
		}

        public void OnCloseWindow(object target, CancelEventArgs e)
        {
        }

        private bool canCloseWindow = true;
        public bool CanCloseWindow()
        {
            return this.canCloseWindow;
        }

        #region Authentication

  

        private async void OnUserLogin()
        {
            var identity = WindowsIdentity.GetCurrent();
			this.UserName = identity.Name;

			var roles = await Task.Factory.StartNew(() => new List<BusinessRoleLookup>());

			this.Roles = roles.ToList();
			this.canLogin = true;
			this.Login.RaiseCanExecuteChanged();
        }

        private void OnCancel()
        {
            Application.Current.Shutdown();
        }

        private bool canLogin = false;
        public bool CanLogin(object param)
        {
            return this.canLogin;
        }

        private void OnLogin(object param)
        {
            PasswordBox passwordBox = param as PasswordBox;

            this.Password = passwordBox.Password;
			if (this.Role == null)
			{
				this.Role = new BusinessRoleLookup(1, "admin");
			}

            if (String.IsNullOrEmpty(this.Role.ShortDesc))
            {
                this.Status = "Select Entitlement first";
                return;
            }

            this.EnterApp();

            logCount += 1;

            //if (logCount < 4)
            //{
            //    System.Windows.Input.Cursor cursor = Application.Current.MainWindow.Cursor;
            //    Application.Current.MainWindow.Cursor = System.Windows.Input.Cursors.Wait;
            //    bool isAuthenticated = false;

            //    Task authenticateTask = Task.Factory.StartNew(() =>
            //    {
            //        try
            //        {
            //            LdapAuthentication ldapAuthentication = new LdapAuthentication("");
            //            isAuthenticated = ldapAuthentication.IsAuthenticated(domain, userName, password);
            //        }
            //        catch (Exception ex)
            //        {
            //            this.Status = ex.Message;
            //        }
            //    });

            //    authenticateTask.ContinueWith((t) =>
            //    {
            //        if (isAuthenticated)
            //            this.EnterApp();

            //        Application.Current.MainWindow.Cursor = cursor;

            //    }, TaskScheduler.FromCurrentSynchronizationContext());
            //}
            //else
            //{
            //    this.Status = "Access denied! Contact system administrator! ";
            //    Thread.Sleep(3000);

            //    Application.Current.Shutdown();
            //}
        }

        private void EnterApp()
        {
            if (this.Role != null && !String.IsNullOrEmpty(this.Role.ShortDesc))
            {
				CurrentUserParam currentUserParam = new CurrentUserParam(this.userName, this.Role.Id, this.Role.ShortDesc);

                ModularBootstrapper bootstrapper = new ModularBootstrapper(currentUserParam);
                bootstrapper.Run();
            }
        }

        #endregion
    }

	///////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////
	///////////////////////////////////////////////////////////////
	[ValueConversion(typeof(Boolean), typeof(Visibility))]
	public class BoolToCollapse : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			bool data = (bool)value;
			if (data)
				return Visibility.Visible;

			return Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return value;
		}
	}
}

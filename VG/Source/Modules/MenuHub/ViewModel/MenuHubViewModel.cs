using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using Prism.Events;
using Prism.Commands;
using Prism.Regions;
using VGTrader.Common;
using VGTrader.Library;
using DataProvider.Common;
using DataProvider.Entities;

namespace MenuHub.ViewModels
{
	[Export("MenuHubViewModel", typeof(IViewModelBase))]
    public class MenuHubViewModel : ViewModelBase
	{
		private readonly IRegionManager regionManager;
		private readonly IDataProvider dataProvider;

		public DelegateCommand ControlLoaded { get; private set; }
        public DelegateCommand<object> TileCommandEvent { get; private set; }
		public DelegateCommand<object> ColorSchema { get; private set; }
		public DelegateCommand<object> HighlightColor { get; private set; }
		public DelegateCommand TestCommand { get; private set; }

		[ImportingConstructor]
		public MenuHubViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IViewContext viewContext, IDataProvider dataProvider, [Import("MenuHubView")]IViewBase view) 
            : base(view)
		{
			this.eventAggregator = eventAggregator;
			this.regionManager = regionManager;
            this.viewContext = viewContext;
			this.dataProvider = dataProvider;  

			this.ControlLoaded = new DelegateCommand(OnControlLoaded);
            this.TileCommandEvent = new DelegateCommand<object>(OnTileCommandEvent);
			this.ColorSchema = new DelegateCommand<object>(OnColorSchema);
			this.HighlightColor = new DelegateCommand<object>(OnHighlightColor);
			this.TestCommand = new DelegateCommand(OnTestCommand);
		}

		private void OnTestCommand()
		{
			this.OnLoadData();
		}

		private async Task<IList<RoleType>> OnLoadData()
		{
			var list = await this.dataProvider.GetRoleTypes();
			return list;
		}

		private void OnControlLoaded()
        {
            //if (this.viewContext != null)
            //    this.viewContext.LoadConfigItem("MenuBar", "MenuBar", "TopMenu", this);

			//this.regionManager.RequestNavigate("MenuBar", "ModalBusyViewModel");

			//this.Buttons = this.viewContext.TileBar.Items;
        }

 		public void OnRequestNavigateCompletted(NavigationResult nr)
		{

		}

        private void OnTileCommandEvent(object param)
        {
            int index = Convert.ToInt32(param);
    //        if (this.viewContext.GetCurrent().TileBar.Items != null && -1 < index && index < this.viewContext.GetCurrent().TileBar.Items.Count)
    //        {
    //            var data = this.viewContext.TileBar.Items[index];
				//var parameters = new NavigationParameters();
				//parameters.Add("MainNavigationParam", data.Name);

				//this.regionManager.RequestNavigate("MainRegion", "MainViewModel", parameters);
    //        }
        }

		private void OnColorSchema(object param)
		{
			int mode = 0;
			if (int.TryParse(param.ToString(), out mode))
			{
				switch (mode)
				{
					case 1: Glass.UX.Presentation.AppearanceManager.Current.ThemeSource = Glass.UX.Presentation.AppearanceManager.LightThemeSource; break;
					case 2: Glass.UX.Presentation.AppearanceManager.Current.ThemeSource = Glass.UX.Presentation.AppearanceManager.DarkThemeSource; break;
					case 3: Glass.UX.Presentation.AppearanceManager.Current.ThemeSource = Glass.UX.Presentation.AppearanceManager.GrayThemeSource; break;
				}
			}
		}

		private void OnHighlightColor(object param)
		{
			int mode = 0;
			if (int.TryParse(param.ToString(), out mode))
			{
				switch (mode)
				{
					case 1: Glass.UX.Presentation.AppearanceManager.Current.AccentColor = Colors.LimeGreen; break;
					case 2: Glass.UX.Presentation.AppearanceManager.Current.AccentColor = Colors.DodgerBlue; break;
					case 3: Glass.UX.Presentation.AppearanceManager.Current.AccentColor = Colors.BurlyWood; break;
				}
			}
		}
	}
}

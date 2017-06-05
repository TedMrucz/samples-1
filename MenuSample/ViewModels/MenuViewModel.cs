using MenuSample.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace MenuSample.ViewModels
{
    public class MenuViewModel : BindableBase
	{
        public DelegateCommand ControlLoaded { get; set; }
        public DelegateCommand<object> MenuItemCommand { get; set; }

        public MenuViewModel()
        {
            this.ControlLoaded = new DelegateCommand(OnControlLoaded);
            this.MenuItemCommand = new DelegateCommand<object>(OnMenuItemCommand);
        }

        private void OnControlLoaded()
        {
            if (0 == this.Menus.Count)
            {
                var menu = new ObservableCollection<MenuOption>();
                menu.Add(new MenuOption("Option One"));
                menu.Add(new MenuOption("Option Two"));
                this.Menus = menu;
            }
        }

        private void OnMenuItemCommand(object param)
        {
            
        }

        private ObservableCollection<MenuOption> menus = new ObservableCollection<MenuOption>();
        public ObservableCollection<MenuOption> Menus
        {
            get { return menus; }
            set { SetProperty(ref menus, value); }
        }
    }
}

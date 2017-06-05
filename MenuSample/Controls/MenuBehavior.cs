using MenuSample.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interactivity;

namespace MenuSample.Controls
{
    public class MenuBehavior : Behavior<ContextMenu>
    {
        public MenuBehavior()
            : base()
        { }

        protected override void OnAttached()
        {
            base.OnAttached();
            ContextMenu asspciated = this.AssociatedObject as ContextMenu;
            asspciated.Opened += OnOpened;
            /// code
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            /// code
        }

        private void OnOpened(object sender, RoutedEventArgs e)
        {
            ContextMenu associated = this.AssociatedObject as ContextMenu;

            ObservableCollection<MenuOption> menus = associated.ItemsSource as ObservableCollection<MenuOption>;
            menus.Add(new MenuOption("Another item"));
        }
    }
}
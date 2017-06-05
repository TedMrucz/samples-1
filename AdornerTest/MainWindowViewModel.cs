using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace AdornerTest
{
    public class MainWindowViewModel : BindableBase
    {
        private string legendItem;
        public string LegendItem
        {
            get { return legendItem; }
            set { SetProperty(ref legendItem, value); }
        }

        private ObservableCollection<string> legendItems = new ObservableCollection<string>();
        public ObservableCollection<string> LegendItems
        {
            get { return legendItems; }
            set { SetProperty(ref legendItems, value); }
        }

        public DelegateCommand AddCommand { get; set; }
        private void OnAddCommand()
        {
            this.LegendItems.Add(this.LegendItem);
        }

        public MainWindowViewModel()
        {
            this.AddCommand = new DelegateCommand(OnAddCommand);
        }
    }
}

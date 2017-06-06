using ItemsFilter.Model;
using System.Collections.ObjectModel;

namespace ItemsFilter.Design 
{
    class MultiValueFilterModel : IMultiValueFilter 
    {
        private string[] values;
        private ObservableCollection<object> selectedValues;
        public MultiValueFilterModel()
        {
            values = new string[]
            {
                "Item 1",
                "Item2"
            };
            selectedValues = new ObservableCollection<object>();
            selectedValues.Add(values[0]);
        }
        public System.Collections.IEnumerable AvailableValues
        {
            get { return values; }
            set { ; }
        }

        public System.Collections.ObjectModel.ReadOnlyObservableCollection<object> SelectedValues
        {
            get { return new System.Collections.ObjectModel.ReadOnlyObservableCollection<object>(selectedValues); }
        }

        public void SelectedValuesChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e) { ; }

        public void Attach(ItemsFilter.FilterPresenter presenter) { ; }

        public void Detach(ItemsFilter.FilterPresenter presenter) { ; }

        public void Attach(ItemsFilter.ViewModel.FilterControlVm vm) { ; }

        public void Detach(ItemsFilter.ViewModel.FilterControlVm vm) { ; }

        public bool IsActive 
        {
            get { return true; }
            set { ; }
        }

        public string Name 
        {
            get { return "Equality:"; }
            set { ; }
        }

        public void IsMatch(ItemsFilter.FilterPresenter sender, ItemsFilter.FilterEventArgs e) { ; }
    }
}

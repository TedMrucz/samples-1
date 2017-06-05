using DecisionPoint.Models;
using Prism.Commands;
using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Threading.Tasks;


namespace DecisionPoint.ViewModels
{
    public class UserViewModel : BindableBase
    {
        public DelegateCommand ControlLoaded { get; set; }

        private UserModel model = new UserModel();

        public UserViewModel()
        {
            this.ControlLoaded = new DelegateCommand(OnControlLoaded);
        }

        private void OnControlLoaded()
        {
            if (this.model != null)
            {
                Task task = Task.Factory.StartNew(() =>
                {
                    this.model.GetData();
                });

                task.ContinueWith((t) =>
                {
                    this.Customers = this.model.Customers;
                    this.SelectedCustomerIndex = 0;

                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
       
        private ObservableCollection<Customer> cutomers;
        public ObservableCollection<Customer> Customers
        {
            get { return this.cutomers; }
            set { SetProperty(ref this.cutomers, value); }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get { return this.selectedCustomer; }
            set { SetProperty(ref this.selectedCustomer, value); }
        }

        private int selectedCustomerIndex = -1;
        public int SelectedCustomerIndex
        {
            get { return selectedCustomerIndex; }
            set { SetProperty(ref selectedCustomerIndex, value); }
        }
    }
}

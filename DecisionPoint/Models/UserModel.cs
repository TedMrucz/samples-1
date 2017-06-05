using System.Collections.ObjectModel;

namespace DecisionPoint.Models
{
    public class UserModel
    {
        public UserModel()
        { }

        private ObservableCollection<Customer> cutomers;
        public ObservableCollection<Customer> Customers
        {
            get { return this.cutomers; }
            set { this.cutomers = value; }
        }

        public void GetData()
        {
            var customers = new ObservableCollection<Customer>();

            customers.Add(new Customer { FirstName = "Anna", LastName = "Smith", PostalCode = "L5V1P5" });
            customers.Add(new Customer { FirstName = "Keith", LastName = "Harris", PostalCode = "L5V1P5" });
            customers.Add(new Customer { FirstName = "John", LastName = "Adams", PostalCode = "L5V1P5" });

            this.Customers = customers;
        }
    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostalCode { get; set; }
    }
}

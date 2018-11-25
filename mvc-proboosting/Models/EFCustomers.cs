namespace mvc_proboosting.Models
{
    using System.Linq;

    public class EFCustomers : ICustomersMock
    {
        //connect to db
        private BoostTaskModel db = new BoostTaskModel();

        public IQueryable<Customer> Customers => this.db.Customers;

        public IQueryable<Booster> Boosters => this.db.Boosters;

        public Customer Save(Customer customer)
        {
            if (customer.CustomerId == 0)
            {
                // insert
                db.Customers.Add(customer);
            }
            else
            {
                // update
                db.Entry(customer).State = System.Data.Entity.EntityState.Modified;
            }

            db.SaveChanges();
            return customer;
        }

        public void Delete(Customer customer)
        {
            this.db.Customers.Remove(customer);
            this.db.SaveChanges();
        }
    }
}
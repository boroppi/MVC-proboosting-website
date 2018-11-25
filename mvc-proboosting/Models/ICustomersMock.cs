namespace mvc_proboosting.Models
{
    using System.Linq;

    public interface ICustomersMock
    {
        IQueryable<Customer> Customers { get; }

        IQueryable<Booster> Boosters { get; }

        Customer Save(Customer customer);

        void Delete(Customer customer);
    }
}
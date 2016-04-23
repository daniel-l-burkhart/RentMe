using System.Collections.Generic;
using RentMe.DAL.Interfaces;
using RentMe.DAL.Repository;
using RentMe.Model;

namespace RentMe.Controller
{
    /// <summary>
    ///     The controller for the customer model objects.
    /// </summary>
    public class CustomerController
    {
        /// <summary>
        ///     The customer
        /// </summary>
        private readonly ICustomerRepository<Customer> customer;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerController" /> class.
        /// </summary>
        public CustomerController()
        {
            this.customer = new CustomerRepository();
        }

        /// <summary>
        ///     Gets all customers.
        /// </summary>
        /// <returns></returns>
        public IList<Customer> GetAllCustomers()
        {
            return this.customer.GetAll();
        }

        /// <summary>
        ///     Searches the customers.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public IList<Customer> SearchCustomers(string firstName, string lastName, string phoneNumber)
        {
            if (firstName != string.Empty && lastName != string.Empty)
            {
                return this.customer.SearchByCustomerName(firstName, lastName);
            }
            return this.customer.SearchByCustomerPhoneNumber(phoneNumber);
        }

        /// <summary>
        ///     Inserts the customer.
        /// </summary>
        /// <param name="newCustomer">The new customer.</param>
        public void InsertCustomer(Customer newCustomer)
        {
            this.customer.Add(newCustomer);
        }

        /// <summary>
        ///     Updates the customer.
        /// </summary>
        /// <param name="updateCustomer">The update customer.</param>
        public void UpdateCustomer(Customer updateCustomer)
        {
            this.customer.Update(updateCustomer);
        }

        /// <summary>
        ///     Deletes the customer.
        /// </summary>
        /// <param name="deletedCustomer">The deleted customer.</param>
        public void DeleteCustomer(Customer deletedCustomer)
        {
            this.customer.Delete(deletedCustomer);
        }

        /// <summary>
        ///     Gets the customer by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Customer GetCustomerById(int id)
        {
            return this.customer.GetById(id);
        }
    }
}
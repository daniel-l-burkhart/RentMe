using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    ///     Customer Implementation of Repository Pattern.
    /// </summary>
    public class CustomerRepository : ICustomerRepository<Customer>
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomerRepository" /> class.
        /// </summary>
        /// <param name="connectionLabel">The connection label.</param>
        public CustomerRepository(string connectionLabel = "MySqlDbConnection")
        {
            this.connectionString = connectionLabel;
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(Customer entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "INSERT INTO CUSTOMER(fname, lname, phoneNumber, email, ssn, address, city, state, zipcode) VALUES(@fName, @lName, @phonenumber, @email, @ssn, @address, @city, @state, @zipcode)",
                        conn);

                cmd.Parameters.AddWithValue("@fName", entity.Fname);
                cmd.Parameters.AddWithValue("@lName", entity.Lname);
                cmd.Parameters.AddWithValue("@phonenumber", entity.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", entity.Email);
                cmd.Parameters.AddWithValue("@ssn", entity.Ssn);
                cmd.Parameters.AddWithValue("@address", entity.Address);
                cmd.Parameters.AddWithValue("@city", entity.City);
                cmd.Parameters.AddWithValue("@state", entity.State);
                cmd.Parameters.AddWithValue("@zipcode", entity.ZipCode);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(Customer entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd = new MySqlCommand("DELETE FROM CUSTOMER WHERE customerID=@customerID", conn);
                cmd.Parameters.AddWithValue("@customerID", entity.CustomerId);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(Customer entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "UPDATE CUSTOMER SET fname = @fname, lname = @lname, phoneNumber = @phonenumber, email = @email, ssn = @ssn, address = @address, city = @city, state = @state, zipcode = @zipcode WHERE customerID = @customerID",
                        conn);

                cmd.Parameters.AddWithValue("@fName", entity.Fname);
                cmd.Parameters.AddWithValue("@lName", entity.Lname);
                cmd.Parameters.AddWithValue("@phonenumber", entity.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", entity.Email);
                cmd.Parameters.AddWithValue("@ssn", entity.Ssn);
                cmd.Parameters.AddWithValue("@address", entity.Address);
                cmd.Parameters.AddWithValue("@city", entity.City);
                cmd.Parameters.AddWithValue("@state", entity.State);
                cmd.Parameters.AddWithValue("@zipcode", entity.ZipCode);
                cmd.Parameters.AddWithValue("@customerID", entity.CustomerId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Customer GetById(int id)
        {
            Customer selectedCustomer = null;

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from CUSTOMER WHERE customerID=@customerID", conn))
                {
                    cmd.Parameters.AddWithValue("@customerID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            selectedCustomer = new Customer
                            {
                                CustomerId = (int) dataReader["customerID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                ZipCode = dataReader["zipCode"] as string
                            };
                        }
                    }
                }
            }

            return selectedCustomer;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Customer> GetAll()
        {
            var allCustomers = new SortableBindingList<Customer>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from CUSTOMER", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currCustomer = new Customer
                            {
                                CustomerId = (int) dataReader["customerID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                ZipCode = dataReader["zipCode"] as string
                            };

                            allCustomers.Add(currCustomer);
                        }
                    }
                }
            }

            return allCustomers;
        }

        /// <summary>
        ///     Searches the name of the by customer.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        public SortableBindingList<Customer> SearchByCustomerName(string firstName, string lastName)
        {
            var customers = new SortableBindingList<Customer>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from CUSTOMER WHERE fname=@fname AND lname=@lname", conn))
                {
                    cmd.Parameters.AddWithValue("fname", firstName);
                    cmd.Parameters.AddWithValue("lname", lastName);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currCustomer = new Customer
                            {
                                CustomerId = (int) dataReader["customerID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                ZipCode = dataReader["zipCode"] as string
                            };

                            customers.Add(currCustomer);
                        }
                    }
                }
            }

            return customers;
        }

        /// <summary>
        ///     Searches the by customer phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        public SortableBindingList<Customer> SearchByCustomerPhoneNumber(string phoneNumber)
        {
            var customers = new SortableBindingList<Customer>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from CUSTOMER WHERE phoneNumber=@phoneNumber", conn))
                {
                    cmd.Parameters.AddWithValue("phoneNumber", phoneNumber);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currCustomer = new Customer
                            {
                                CustomerId = (int) dataReader["customerID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                ZipCode = dataReader["zipCode"] as string
                            };

                            customers.Add(currCustomer);
                        }
                    }
                }
            }

            return customers;
        }
    }
}
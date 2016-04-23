using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    ///     Rental transaction repository concrete implementation
    /// </summary>
    public class RentalTransactionRepository : IRentalTransactionRepository<RentalTransaction>
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentalRepository" /> class.
        /// </summary>
        /// <param name="connectionlabel">The connectionlabel.</param>
        public RentalTransactionRepository(string connectionlabel = "MySqlDbConnection")
        {
            this.connectionString = connectionlabel;
        }

        /// <summary>
        ///     Adds the and return.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int AddAndReturn(RentalTransaction entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd =
                    new MySqlCommand(
                        "INSERT INTO RENTALTRANSACTION(dateSubmitted, totalCost, customerID, employeeID) VALUES(@dateSubmitted, @totalCost, @customerID, @employeeID)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@dateSubmitted", entity.DateSubmitted);
                    cmd.Parameters.AddWithValue("@totalCost", entity.TotalCost);
                    cmd.Parameters.AddWithValue("@customerID", entity.CustomerId);
                    cmd.Parameters.AddWithValue("@employeeID", entity.EmployeeId);

                    cmd.ExecuteScalar();

                    cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));

                    return Convert.ToInt32(cmd.Parameters["@newId"].Value);
                }
            }
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(RentalTransaction entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand("DELETE FROM RENTALTRANSACTION WHERE rentalTransactionID=@rentalTransactionID",
                        conn);
                cmd.Parameters.AddWithValue("@rentalTransactionID", entity.RentalTransactionId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SortableBindingList<RentalTransaction> GetByCustomerID(int id)
        {
            var customerTransactions = new SortableBindingList<RentalTransaction>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RENTALTRANSACTION where customerID = @customerID", conn))
                {
                    cmd.Parameters.AddWithValue("@customerID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var rentalTransactionID = (Convert.IsDBNull(dataReader["rentalTransactionID"]) ? 0 : (int)dataReader["rentalTransactionID"]);
                            var customerID = (Convert.IsDBNull(dataReader["customerID"]) ? 0 : (int)dataReader["customerID"]);
                            var employeeID = (Convert.IsDBNull(dataReader["employeeID"]) ? 0 : (int)dataReader["employeeID"]);

                            var currTransaction = new RentalTransaction
                            {

                                RentalTransactionId = rentalTransactionID.ToString(),
                                DateSubmitted =
                                    (Convert.IsDBNull(dataReader["dateSubmitted"])
                                        ? DateTime.MinValue
                                        : (DateTime)dataReader["dateSubmitted"]),
                                TotalCost = (Convert.IsDBNull(dataReader["totalCost"]) ? 0 : Math.Round((double)dataReader["totalCost"], 2)),
                                CustomerId = customerID.ToString(),
                                EmployeeId = employeeID.ToString()
                            };

                            customerTransactions.Add(currTransaction);
                        }
                    }
                }
            }

            return customerTransactions;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<RentalTransaction> GetAll()
        {
            var allRentalTransactions = new SortableBindingList<RentalTransaction>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RENTALTRANSACTION", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currRentalTransaction = new RentalTransaction
                            {
                                RentalTransactionId = (dataReader["rentalTransactionId"] as string),
                                DateSubmitted =
                                    (Convert.IsDBNull(dataReader["dateSubmitted"])
                                        ? DateTime.MinValue
                                        : (DateTime) dataReader["dueDate"]),
                                TotalCost =
                                    (Convert.IsDBNull(dataReader["totalCost"]) ? 0 : Math.Round((double) (dataReader["totalCost"]), 2)),
                                CustomerId = (dataReader["customerId"] as string),
                                EmployeeId = (dataReader["employeeId"] as string)
                            };

                            allRentalTransactions.Add(currRentalTransaction);
                        }
                    }
                }
            }

            return allRentalTransactions;
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public RentalTransaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public void Update(RentalTransaction entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(RentalTransaction entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd =
                    new MySqlCommand(
                        "INSERT INTO RENTALTRANSACTION(dateSubmitted, totalCost, customerID, employeeID) VALUES(@dateSubmitted, @totalCost, @customerID, @employeeID)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@dateSubmitted", entity.DateSubmitted);
                    cmd.Parameters.AddWithValue("@totalCost", entity.TotalCost);
                    cmd.Parameters.AddWithValue("@customerID", entity.CustomerId);
                    cmd.Parameters.AddWithValue("@employeeID", entity.EmployeeId);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
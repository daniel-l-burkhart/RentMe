using System;
using RentMe.DAL.Interfaces;
using RentMe.Model;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace RentMe.DAL.Repository
{
    internal class ReturnTransactionRepository : IReturnTransactionRepository<ReturnTransaction>
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentalRepository" /> class.
        /// </summary>
        /// <param name="connectionlabel">The connectionlabel.</param>
        public ReturnTransactionRepository(string connectionlabel = "MySqlDbConnection")
        {
            this.connectionString = connectionlabel;
        }

        public void Add(ReturnTransaction entity)
        {
            throw new NotImplementedException();
        }

        public int AddAndReturn(ReturnTransaction entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd =
                    new MySqlCommand(
                        "INSERT INTO RETURNTRANSACTION(returnDate, customerID, employeeID) VALUES(@returnDate, @customerID, @employeeID)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@returnDate", entity.ReturnDate);
                    cmd.Parameters.AddWithValue("@customerID", entity.CustomerId);
                    cmd.Parameters.AddWithValue("@employeeID", entity.EmployeeId);

                    cmd.ExecuteScalar();

                    cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));

                    return Convert.ToInt32(cmd.Parameters["@newId"].Value);
                }
            }
        }

        public void Delete(ReturnTransaction entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand("DELETE FROM RETURNTRANSACTION WHERE returnTransactionID=@returnTransactionID",
                        conn);
                cmd.Parameters.AddWithValue("@returnTransactionID", entity.ReturnTransactionId);
                cmd.ExecuteNonQuery();
            }
        }

        public SortableBindingList<ReturnTransaction> GetAll()
        {
            var allReturnTransactions = new SortableBindingList<ReturnTransaction>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RETURNTRANSACTION", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currReturnTransaction = new ReturnTransaction
                            {
                                ReturnTransactionId = (dataReader["rentalTransactionId"] as string),
                                ReturnDate =
                                    (Convert.IsDBNull(dataReader["dateSubmitted"])
                                        ? DateTime.MinValue
                                        : (DateTime)dataReader["dueDate"]),
                                CustomerId = (dataReader["customerId"] as string),
                                EmployeeId = (dataReader["employeeId"] as string)
                            };

                            allReturnTransactions.Add(currReturnTransaction);
                        }
                    }
                }
            }

            return allReturnTransactions;
        }

        public ReturnTransaction GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(ReturnTransaction entity)
        {
            throw new NotImplementedException();
        }
    }
}
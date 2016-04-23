using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    ///     Concrete implementation for rental table.
    /// </summary>
    public class RentalRepository : IRentalRepository<Rental>
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentalRepository" /> class.
        /// </summary>
        /// <param name="connectionlabel">The connectionlabel.</param>
        public RentalRepository(string connectionlabel = "MySqlDbConnection")
        {
            this.connectionString = connectionlabel;
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(Rental entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd =
                    new MySqlCommand(
                        "INSERT INTO RENTAL(transactionID, itemID, quantity, dueDate, isReturned) VALUES(@transactionID, @itemID, @quantity, @dueDate, @isReturned)",
                        conn))
                {
                    cmd.Parameters.AddWithValue("@transactionID", entity.TransactionId);
                    cmd.Parameters.AddWithValue("@itemID", entity.ItemId);
                    cmd.Parameters.AddWithValue("@quantity", entity.Quantity);
                    cmd.Parameters.AddWithValue("@dueDate", entity.DueDate);
                    cmd.Parameters.AddWithValue("@isReturned", entity.IsReturned);

                    cmd.ExecuteScalar();
                }
            }
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Rental entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd = new MySqlCommand("DELETE FROM RENTAL WHERE rentalID=@rentalID", conn);
                cmd.Parameters.AddWithValue("@rentalID", entity.RentalId);
                cmd.ExecuteNonQuery();

                conn.Close();
            }
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Rental entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (
                    var cmd =
                        new MySqlCommand(
                            "UPDATE RENTAL SET rentalID = @rentalID, transactionID = @transactionID, itemID = @itemID, quantity = @quantity, dueDate = @dueDate, isReturned = @isReturned WHERE rentalID = @rentalID",
                            conn))
                {
                    cmd.Parameters.AddWithValue("@rentalID", entity.RentalId);
                    cmd.Parameters.AddWithValue("@transactionID", entity.TransactionId);
                    cmd.Parameters.AddWithValue("@itemID", entity.ItemId);
                    cmd.Parameters.AddWithValue("@quantity", entity.Quantity);
                    cmd.Parameters.AddWithValue("@dueDate", entity.DueDate);
                    cmd.Parameters.AddWithValue("@isReturned", entity.IsReturned);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SortableBindingList<Rental> GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public SortableBindingList<Rental> GetByTransactionID(int id)
        {
            var transactionRentals = new SortableBindingList<Rental>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RENTAL where transactionID = @transactionID", conn))
                {
                    cmd.Parameters.AddWithValue("@transactionID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var rentalId = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var transactionId = (Convert.IsDBNull(dataReader["transactionID"]) ? 0 : (int)dataReader["transactionID"]);
                            var itemId = (Convert.IsDBNull(dataReader["itemID"])
                                ? 0
                                : (int)dataReader["itemID"]);

                            var currRental = new Rental
                            {

                                Quantity = (Convert.IsDBNull(dataReader["quantity"]) ? 0 : (int)dataReader["quantity"]),
                                DueDate =
                                    (Convert.IsDBNull(dataReader["dueDate"])
                                        ? DateTime.MinValue
                                        : (DateTime)dataReader["dueDate"]),
                                RentalId = rentalId.ToString(),
                                TransactionId = transactionId.ToString(),
                                ItemId = itemId.ToString(),
                                IsReturned = dataReader["isReturned"] as bool? ?? false
                            };

                            transactionRentals.Add(currRental);
                        }
                    }
                }
            }

            return transactionRentals;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Rental> GetAll()
        {
            var allRentals = new SortableBindingList<Rental>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RENTAL", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var rentalId = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var transactionId = (Convert.IsDBNull(dataReader["transactionID"]) ? 0 : (int)dataReader["transactionID"]);
                            var itemId = (Convert.IsDBNull(dataReader["itemID"])
                                ? 0
                                : (int)dataReader["itemID"]);

                            var currRental = new Rental
                            {
                                Quantity = (Convert.IsDBNull(dataReader["quantity"]) ? 0 : (int)dataReader["quantity"]),
                                DueDate =
                                    (Convert.IsDBNull(dataReader["dueDate"])
                                        ? DateTime.MinValue
                                        : (DateTime)dataReader["dueDate"]),
                                RentalId = rentalId.ToString(),
                                TransactionId = transactionId.ToString(),
                                ItemId = itemId.ToString(),
                                IsReturned = dataReader["isReturned"] as bool? ?? false
                            };

                            allRentals.Add(currRental);
                        }
                    }
                }
            }

            return allRentals;
        }

        Rental IRepository<Rental>.GetById(int id)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from RENTAL where rentalID = @rentalID", conn))
                {
                    cmd.Parameters.AddWithValue("@rentalID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var rentalId = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var transactionId = (Convert.IsDBNull(dataReader["transactionID"]) ? 0 : (int)dataReader["transactionID"]);
                            var itemId = (Convert.IsDBNull(dataReader["itemID"])
                                ? 0
                                : (int)dataReader["itemID"]);

                            var currRental = new Rental
                            {
                                Quantity = (Convert.IsDBNull(dataReader["quantity"]) ? 0 : (int)dataReader["quantity"]),
                                DueDate =
                                    (Convert.IsDBNull(dataReader["dueDate"])
                                        ? DateTime.MinValue
                                        : (DateTime)dataReader["dueDate"]),
                                RentalId = rentalId.ToString(),
                                TransactionId = transactionId.ToString(),
                                ItemId = itemId.ToString(),
                                IsReturned = dataReader["isReturned"] as bool? ?? false
                            };

                            return currRental;
                        }
                    }
                }
            }

            return null;
        }
    }
}
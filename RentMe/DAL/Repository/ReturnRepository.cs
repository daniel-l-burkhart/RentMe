using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    ///     Concrete implementation for return repository
    /// </summary>
    public class ReturnRepository : IReturnRepository<Return>
    {
        private readonly string connectionString;

        public ReturnRepository(string connectionlabel = "MySqlDbConnection")
        {
            this.connectionString = connectionlabel;
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(Return entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "INSERT INTO ITEMRETURN(fine, rentalID, rentalTransactionID, returnTransactionID) VALUES(@fine, @rentalID, @rentalTransactionID, @returnTransactionID)",
                        conn);

                cmd.Parameters.AddWithValue("@fine", entity.Fine);
                cmd.Parameters.AddWithValue("@rentalID", entity.RentalId);
                cmd.Parameters.AddWithValue("@rentalTransactionID", entity.RentalTransactionId);
                cmd.Parameters.AddWithValue("@returnTransactionID", entity.ReturnTransactionId);

                cmd.ExecuteNonQuery();
            }
        }

        public int AddAndReturn(Return entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "INSERT INTO ITEMRETURN(fine, rentalID, rentalTransactionID, returnTransactionID) VALUES(@fine, @rentalID, @rentalTransactionID, @returnTransactionID)",
                        conn);

                cmd.Parameters.AddWithValue("@fine", entity.Fine);
                cmd.Parameters.AddWithValue("@rentalID", entity.RentalId);
                cmd.Parameters.AddWithValue("@rentalTransactionID", entity.RentalTransactionId);
                cmd.Parameters.AddWithValue("@returnTransactionID", entity.ReturnTransactionId);

                cmd.ExecuteNonQuery();

                cmd.Parameters.Add(new MySqlParameter("newId", cmd.LastInsertedId));

                return Convert.ToInt32(cmd.Parameters["@newId"].Value);
            }
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Return entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd = new MySqlCommand("DELETE FROM ITEMRETURN WHERE returnID=@returnID", conn);
                cmd.Parameters.AddWithValue("@returnID", entity.ReturnId);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Return entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (
                    var cmd =
                        new MySqlCommand(
                            "UPDATE ITEMRETURN SET fine = @fine, returnID = @returnID, rentalID = @rentalID, rentalTransactionID = @rentalTransactionID, returnTransactionID = @returnTransactionID WHERE returnID = @returnID",
                            conn))
                {
                    cmd.Parameters.AddWithValue("@fine", entity.Fine);
                    cmd.Parameters.AddWithValue("@returnID", entity.ReturnId);
                    cmd.Parameters.AddWithValue("@rentalID", entity.RentalId);
                    cmd.Parameters.AddWithValue("@rentalTransactionID", entity.RentalTransactionId);
                    cmd.Parameters.AddWithValue("@returnTransactionID", entity.ReturnTransactionId);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Return GetById(int id)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from ITEMRETURN WHERE returnID=@returnID", conn))
                {
                    cmd.Parameters.AddWithValue("@returnID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var returnId = (Convert.IsDBNull(dataReader["returnID"]) ? 0 : (int)dataReader["returnID"]);
                            var rentalID = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var rentalTransactionId = (Convert.IsDBNull(dataReader["rentalTransactionId"]) ? 0 : (int)dataReader["rentalTransactionId"]);
                            var returnTransactionId = (Convert.IsDBNull(dataReader["returnTransactionId"]) ? 0 : (int)dataReader["returnTransactionId"]);

                            var currReturn = new Return
                            {
                                Fine = (Convert.IsDBNull(dataReader["fine"])) ? 0 : Math.Round((double)dataReader["fine"], 2),
                                ReturnId = returnId.ToString(),
                                RentalId = rentalID.ToString(),
                                RentalTransactionId = rentalTransactionId.ToString(),
                                ReturnTransactionId = returnTransactionId.ToString()
                            };
                            return currReturn;
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Return> GetAll()
        {
            var allReturns = new SortableBindingList<Return>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from ITEMRETURN", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var returnId = (Convert.IsDBNull(dataReader["returnID"]) ? 0 : (int)dataReader["returnID"]);
                            var rentalID = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var rentalTransactionId = (Convert.IsDBNull(dataReader["rentalTransactionId"]) ? 0 : (int)dataReader["rentalTransactionId"]);
                            var returnTransactionId = (Convert.IsDBNull(dataReader["returnTransactionId"]) ? 0 : (int)dataReader["returnTransactionId"]);

                            var currReturn = new Return
                            {
                                Fine = (Convert.IsDBNull(dataReader["fine"])) ? 0 : Math.Round((double)dataReader["fine"], 2),
                                ReturnId = returnId.ToString(),
                                RentalId = rentalID.ToString(),
                                RentalTransactionId = rentalTransactionId.ToString(),
                                ReturnTransactionId = returnTransactionId.ToString()
                            };

                            allReturns.Add(currReturn);
                        }
                    }
                }
            }

            return allReturns;
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Return> GetAllWithFinesByCustomerID(int custID)
        {
            var allReturns = new SortableBindingList<Return>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from ITEMRETURN as i, RENTALTRANSACTION as r WHERE i.rentalTransactionId = r.rentalTransactionId AND r.customerID=" + custID, conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var returnId = (Convert.IsDBNull(dataReader["returnID"]) ? 0 : (int)dataReader["returnID"]);
                            var rentalID = (Convert.IsDBNull(dataReader["rentalID"]) ? 0 : (int)dataReader["rentalID"]);
                            var rentalTransactionId = (Convert.IsDBNull(dataReader["rentalTransactionId"]) ? 0 : (int)dataReader["rentalTransactionId"]);
                            var returnTransactionId = (Convert.IsDBNull(dataReader["returnTransactionId"]) ? 0 : (int)dataReader["returnTransactionId"]);

                            var currReturn = new Return
                            {
                                Fine = (Convert.IsDBNull(dataReader["fine"])) ? 0 : Math.Round((double)dataReader["fine"], 2),
                                ReturnId = returnId.ToString(),
                                RentalId = rentalID.ToString(),
                                RentalTransactionId = rentalTransactionId.ToString(),
                                ReturnTransactionId = returnTransactionId.ToString()
                            };

                            if (currReturn.Fine > 0) allReturns.Add(currReturn);
                        }
                    }
                }
            }

            return allReturns;
        }
    }
}
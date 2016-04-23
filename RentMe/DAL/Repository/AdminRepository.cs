using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    /// Repository of Admin operations
    /// </summary>
    public class AdminRepository : IAdminInterface
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public AdminRepository(string connectionString = "MySqlDbConnection")
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        /// Runs the SQL query.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public List<CustomDataGridView> RunSqlQuery(string text)
        {
            var adminQueryResult = new List<CustomDataGridView>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand(text, conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            adminQueryResult.Add(new CustomDataGridView());

                            for (var i = 0; i < dataReader.FieldCount; i++)
                            {
                                adminQueryResult[adminQueryResult.Count - 1].Columns.Add(dataReader.GetName(i));
                                adminQueryResult[adminQueryResult.Count - 1].Items.Add(checkForNull(dataReader, i));
                            }
                        }
                    }
                }
            }

            return adminQueryResult;
        }


        /// <summary>
        /// Checks for null.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="i">The i.</param>
        /// <returns></returns>
        private static string checkForNull(MySqlDataReader dataReader, int i)
        {

            try
            {
                return dataReader.GetString(i);
            }
            catch
            {
                return string.Empty;
            }
            
        }

        /// <summary>
        /// Inserts the new employee.
        /// </summary>
        /// <param name="newEmployee">The new employee.</param>
        public void InsertNewEmployee(Employee newEmployee)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "INSERT INTO EMPLOYEE(fname, lname, phoneNumber, email, ssn, address, city, state, zipcode) VALUES(@fName, @lName, @phonenumber, @email, @ssn, @address, @city, @state, @zipcode, @username, @password, @adminFlag)",
                        conn);

                cmd.Parameters.AddWithValue("@fName", newEmployee.Fname);
                cmd.Parameters.AddWithValue("@lName", newEmployee.Lname);
                cmd.Parameters.AddWithValue("@phonenumber", newEmployee.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", newEmployee.Email);
                cmd.Parameters.AddWithValue("@ssn", newEmployee.Ssn);
                cmd.Parameters.AddWithValue("@address", newEmployee.Address);
                cmd.Parameters.AddWithValue("@address2", newEmployee.Address2);
                cmd.Parameters.AddWithValue("@city", newEmployee.City);
                cmd.Parameters.AddWithValue("@state", newEmployee.State);
                cmd.Parameters.AddWithValue("@zipcode", newEmployee.ZipCode);
                cmd.Parameters.AddWithValue("@username", newEmployee.UserName);
                cmd.Parameters.AddWithValue("@password", newEmployee.PassWord);
                cmd.Parameters.AddWithValue("@adminFlag", newEmployee.AdminFlag);

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Generates the admin report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<CustomDataGridView> GenerateAdminReport(DateTime fromDate, DateTime toDate)
        {
           var adminReportResult = new List<CustomDataGridView>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT CUSTOMER.customerID, CONCAT(CUSTOMER.fname, ' ', CUSTOMER.lname) AS CustomerName, RENTALTRANSACTION.dateSubmitted, ITEM.name, ITEM.type, ITEM.style, ITEM.pattern FROM CUSTOMER, RENTAL, RENTALTRANSACTION, ITEM WHERE RENTALTRANSACTION.customerID = CUSTOMER.customerID AND RENTAL.itemID = ITEM.itemID AND RENTAL.transactionID = RENTALTRANSACTION.rentalTransactionID AND (RENTALTRANSACTION.dateSubmitted BETWEEN @fromDate AND @toDate)", conn))
                {
                    cmd.Parameters.AddWithValue("@fromDate", fromDate);
                    cmd.Parameters.AddWithValue("@toDate", toDate);

                    Console.WriteLine(cmd);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            adminReportResult.Add(new CustomDataGridView());

                            for (var i = 0; i < dataReader.FieldCount; i++)
                            {
                                Console.WriteLine(dataReader.GetName(i));
                                adminReportResult[adminReportResult.Count - 1].Columns.Add(dataReader.GetName(i));
                                adminReportResult[adminReportResult.Count - 1].Items.Add(checkForNull(dataReader, i));
                            }
                        }
                    }
                }
            }

            return adminReportResult;
        }

    }


}
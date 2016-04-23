using System.Collections.Generic;
using RentMe.DAL.Repository;
using RentMe.Model;
using System;

namespace RentMe.Controller
{
    /// <summary>
    ///     Controller for Admin operations
    /// </summary>
    public class AdminController
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     The admin repository
        /// </summary>
        private readonly AdminRepository adminRepository;

        /// <summary>
        ///     Initializes a new instance of the <see cref="AdminController" /> class.
        /// </summary>
        /// <param name="connectionlabel">The connectionlabel.</param>
        public AdminController(string connectionlabel = "MySqlDbConnection")
        {
            this.connectionString = connectionlabel;
            this.adminRepository = new AdminRepository();
        }

        /// <summary>
        ///     Runs the admin SQL query.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <returns></returns>
        public List<CustomDataGridView> RunAdminSqlQuery(string text)
        {
            return this.adminRepository.RunSqlQuery(text);
        }

        /// <summary>
        ///     Inserts the employee.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="address">The address.</param>
        /// <param name="city">The city.</param>
        /// <param name="state">The state.</param>
        /// <param name="zipCode">The zip code.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="emailAddress">The email address.</param>
        /// <param name="ssn">The SSN.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="passWord">The pass word.</param>
        /// <param name="adminFlag">The admin flag.</param>
        public void InsertEmployee(string firstName, string lastName, string address, string city, string state,
            string zipCode, string phoneNumber, string emailAddress, string ssn, string userName, string passWord,
            string adminFlag)
        {
            var newEmployee = new Employee
            {
                Fname = firstName,
                Lname = lastName,
                Address = address,
                City = city,
                State = state,
                ZipCode = zipCode,
                PhoneNumber = phoneNumber,
                Email = emailAddress,
                Ssn = ssn,
                UserName = userName,
                PassWord = passWord
            };

            switch (adminFlag)
            {
                case "True":
                    newEmployee.AdminFlag = true;
                    break;
                case "False":
                    newEmployee.AdminFlag = false;
                    break;
            }

            this.adminRepository.InsertNewEmployee(newEmployee);
        }

        /// <summary>
        ///     Generates the report.
        /// </summary>
        /// <param name="fromDate">From date.</param>
        /// <param name="toDate">To date.</param>
        /// <returns></returns>
        public List<CustomDataGridView> GenerateReport(DateTime fromDate, DateTime toDate)
        {
            return this.adminRepository.GenerateAdminReport(fromDate, toDate);
        }
    }
}
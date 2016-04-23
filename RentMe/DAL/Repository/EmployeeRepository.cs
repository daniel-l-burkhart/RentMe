using System;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    public class EmployeeRepository : IEmployeeRepository<Employee>
    {
        #region Instance Variable

        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        #endregion

        #region Constructor

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmployeeRepository" /> class.
        /// </summary>
        /// <param name="connection">The connection.</param>
        public EmployeeRepository(string connection = "MySqlDbConnection")
        {
            this.connectionString = connection;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(Employee entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Update(Employee entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "UPDATE EMPLOYEE SET phoneNumber = @phonenumber, email = @email, address = @address, city = @city, state = @state, zipcode = @zipcode, password = @password WHERE employeeID = " +
                        entity.EmployeeId, conn);

                cmd.Parameters.AddWithValue("@phonenumber", entity.PhoneNumber);
                cmd.Parameters.AddWithValue("@email", entity.Email);
                cmd.Parameters.AddWithValue("@address", entity.Address);
                cmd.Parameters.AddWithValue("@city", entity.City);
                cmd.Parameters.AddWithValue("@state", entity.State);
                cmd.Parameters.AddWithValue("@zipcode", entity.ZipCode);
                cmd.Parameters.AddWithValue("@password", entity.PassWord);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Employee GetById(int id)
        {
            Employee selectedEmployee = null;

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from EMPLOYEE WHERE employeeID=@employeeID", conn))
                {
                    cmd.Parameters.AddWithValue("@employeeID", id);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            selectedEmployee = new Employee
                            {
                                EmployeeId = (int) dataReader["customerID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                ZipCode = dataReader["zipCode"] as string,
                                UserName = dataReader["username"] as string,
                                PassWord = dataReader["password"] as string,
                                AdminFlag = dataReader["adminFlag"] as bool? ?? false
                            };
                        }
                    }
                }
            }

            return selectedEmployee;
        }

        public bool isManager(int id)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from MANAGER", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var employeeId = (int)dataReader["employeeID"];

                            if (employeeId == id)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        ///     Gets all employees by supervisor.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Employee> GetAll()
        {
            var allEmployees = new SortableBindingList<Employee>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from EMPLOYEE", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currEmployee = new Employee
                            {
                                EmployeeId = (int) dataReader["employeeID"],
                                Ssn = dataReader["ssn"] as string,
                                Fname = dataReader["fname"] as string,
                                Lname = dataReader["lname"] as string,
                                Address = dataReader["address"] as string,
                                Address2 = dataReader["address"] as string,
                                City = dataReader["city"] as string,
                                State = dataReader["state"] as string,
                                Email = dataReader["email"] as string,
                                PhoneNumber = dataReader["phoneNumber"] as string,
                                ZipCode = dataReader["zipCode"] as string,
                                UserName = dataReader["username"] as string,
                                PassWord = dataReader["password"] as string,
                                AdminFlag = dataReader["adminFlag"] as bool? ?? false
                            };

                            allEmployees.Add(currEmployee);
                        }
                    }
                }
            }

            return allEmployees;
        }

        #endregion
    }
}
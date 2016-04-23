using RentMe.DAL.Interfaces;
using RentMe.DAL.Repository;
using RentMe.Model;

namespace RentMe.Controller
{
    /// <summary>
    ///     The Employee Controller
    /// </summary>
    public class EmployeeController
    {
        /// <summary>
        ///     The employee
        /// </summary>
        private readonly IEmployeeRepository<Employee> employee;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EmployeeController" /> class.
        /// </summary>
        public EmployeeController()
        {
            this.employee = new EmployeeRepository();
        }

        /// <summary>
        ///     Updates the employee.
        /// </summary>
        /// <param name="updateEmployee">The update employee.</param>
        public void UpdateEmployee(Employee updateEmployee)
        {
            this.employee.Update(updateEmployee);
        }

        /// <summary>
        ///     Gets the employee by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Employee GetEmployeeById(int id)
        {
            return this.employee.GetById(id);
        }

        public bool isManager(int id)
        {
            return this.employee.isManager(id);
        }

        /// <summary>
        ///     Gets all employees.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Employee> GetAllEmployees()
        {
            return this.employee.GetAll();
        }
    }
}
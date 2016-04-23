namespace RentMe.Model
{
    /// <summary>
    ///     Employee class.
    /// </summary>
    public class Employee
    {
        /// <summary>
        ///     Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        ///     The employee identifier.
        /// </value>
        public int EmployeeId { get; set; }

        /// <summary>
        ///     Gets or sets the fname.
        /// </summary>
        /// <value>
        ///     The fname.
        /// </value>
        public string Fname { get; set; }

        /// <summary>
        ///     Gets or sets the lname.
        /// </summary>
        /// <value>
        ///     The lname.
        /// </value>
        public string Lname { get; set; }

        /// <summary>
        ///     Gets or sets the phone number.
        /// </summary>
        /// <value>
        ///     The phone number.
        /// </value>
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the email.
        /// </summary>
        /// <value>
        ///     The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        ///     Gets or sets the SSN.
        /// </summary>
        /// <value>
        ///     The SSN.
        /// </value>
        public string Ssn { get; set; }

        /// <summary>
        ///     Gets or sets the address.
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        public string Address { get; set; }

        /// <summary>
        ///     Gets or sets the address2.
        /// </summary>
        /// <value>
        ///     The address2.
        /// </value>
        public string Address2 { get; set; }

        /// <summary>
        ///     Gets or sets the city.
        /// </summary>
        /// <value>
        ///     The city.
        /// </value>
        public string City { get; set; }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        public string State { get; set; }

        /// <summary>
        ///     Gets or sets the zip code.
        /// </summary>
        /// <value>
        ///     The zip code.
        /// </value>
        public string ZipCode { get; set; }

        /// <summary>
        ///     Gets or sets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserName { get; set; }

        /// <summary>
        ///     Gets or sets the pass word.
        /// </summary>
        /// <value>
        ///     The pass word.
        /// </value>
        public string PassWord { get; set; }

        /// <summary>
        ///     Gets or sets a value indicating whether [admin flag].
        /// </summary>
        /// <value>
        ///     <c>true</c> if [admin flag]; otherwise, <c>false</c>.
        /// </value>
        public bool AdminFlag { get; set; }
    }
}
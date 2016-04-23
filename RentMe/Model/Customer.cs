namespace RentMe.Model
{
    /// <summary>
    ///     Customer Model Objects
    /// </summary>
    public class Customer
    {
        /// <summary>
        ///     Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        ///     The customer identifier.
        /// </value>
        public int CustomerId { get; set; }

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
        ///     Gets or sets the address.
        /// </summary>
        /// <value>
        ///     The address.
        /// </value>
        public string Address { get; set; }

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
    }
}
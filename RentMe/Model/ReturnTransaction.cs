using System;

namespace RentMe.Model
{
    /// <summary>
    ///     Return Transaction model object.
    /// </summary>
    public class ReturnTransaction
    {
        /// <summary>
        ///     Gets or sets the return transaction identifier.
        /// </summary>
        /// <value>
        ///     The return transaction identifier.
        /// </value>
        public string ReturnTransactionId { get; set; }

        /// <summary>
        ///     Gets or sets the return date.
        /// </summary>
        /// <value>
        ///     The return date.
        /// </value>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        ///     Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        ///     The employee identifier.
        /// </value>
        public string EmployeeId { get; set; }

        /// <summary>
        ///     Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        ///     The customer identifier.
        /// </value>
        public string CustomerId { get; set; }
    }
}
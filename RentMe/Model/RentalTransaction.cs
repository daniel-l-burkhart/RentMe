using System;

namespace RentMe.Model
{
    /// <summary>
    ///     Rental Transaction Model object.
    /// </summary>
    public class RentalTransaction
    {
        /// <summary>
        ///     Gets or sets the rental transaction identifier.
        /// </summary>
        /// <value>
        ///     The rental transaction identifier.
        /// </value>
        public string RentalTransactionId { get; set; }

        /// <summary>
        ///     Gets or sets the date submitted.
        /// </summary>
        /// <value>
        ///     The date submitted.
        /// </value>
        public DateTime DateSubmitted { get; set; }

        /// <summary>
        ///     Gets or sets the total cost.
        /// </summary>
        /// <value>
        ///     The total cost.
        /// </value>
        public double TotalCost { get; set; }

        /// <summary>
        ///     Gets or sets the customer identifier.
        /// </summary>
        /// <value>
        ///     The customer identifier.
        /// </value>
        public string CustomerId { get; set; }

        /// <summary>
        ///     Gets or sets the employee identifier.
        /// </summary>
        /// <value>
        ///     The employee identifier.
        /// </value>
        public string EmployeeId { get; set; }
    }
}
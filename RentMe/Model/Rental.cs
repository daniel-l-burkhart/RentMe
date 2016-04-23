using System;

namespace RentMe.Model
{
    /// <summary>
    ///     Rental model object for storing database items into objects.
    /// </summary>
    public class Rental
    {
        /// <summary>
        ///     Gets or sets the rental identifier.
        /// </summary>
        /// <value>
        ///     The rental identifier.
        /// </value>
        public string RentalId { get; set; }

        /// <summary>
        ///     Gets or sets the transaction identifier.
        /// </summary>
        /// <value>
        ///     The transaction identifier.
        /// </value>
        public string TransactionId { get; set; }

        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        public string ItemId { get; set; }

        /// <summary>
        ///     Gets or sets the quantity.
        /// </summary>
        /// <value>
        ///     The quantity.
        /// </value>
        public int Quantity { get; set; }

        /// <summary>
        ///     Gets or sets the due date.
        /// </summary>
        /// <value>
        ///     The due date.
        /// </value>
        public DateTime DueDate { get; set; }

        public bool IsReturned
        {
            get;
            set;
        }
    }
}
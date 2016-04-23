namespace RentMe.Model
{
    /// <summary>
    ///     The return model object for storing items from the return database.
    /// </summary>
    public class Return
    {
        /// <summary>
        ///     Gets or sets the fine.
        /// </summary>
        /// <value>
        ///     The fine.
        /// </value>
        public double Fine { get; set; }

        /// <summary>
        ///     Gets or sets the return identifier.
        /// </summary>
        /// <value>
        ///     The return identifier.
        /// </value>
        public string ReturnId { get; set; }

        /// <summary>
        ///     Gets or sets the rental identifier.
        /// </summary>
        /// <value>
        ///     The rental identifier.
        /// </value>
        public string RentalId { get; set; }

        /// <summary>
        ///     Gets or sets the rental transaction identifier.
        /// </summary>
        /// <value>
        ///     The rental transaction identifier.
        /// </value>
        public string RentalTransactionId { get; set; }

        /// <summary>
        ///     Gets or sets the return transaction identifier.
        /// </summary>
        /// <value>
        ///     The return transaction identifier.
        /// </value>
        public string ReturnTransactionId { get; set; }
    }
}
namespace RentMe.Model
{
    /// <summary>
    ///     The items of furniture.
    /// </summary>
    public class Item
    {
        /// <summary>
        ///     Gets or sets the item identifier.
        /// </summary>
        /// <value>
        ///     The item identifier.
        /// </value>
        public int ItemId { get; set; }

        /// <summary>
        ///     Gets or sets the name.
        /// </summary>
        /// <value>
        ///     The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public string Type { get; set; }

        /// <summary>
        ///     Gets or sets the style.
        /// </summary>
        /// <value>
        ///     The style.
        /// </value>
        public string Style { get; set; }

        /// <summary>
        ///     Gets or sets the pattern.
        /// </summary>
        /// <value>
        ///     The pattern.
        /// </value>
        public string Pattern { get; set; }

        /// <summary>
        ///     Gets or sets the cost per day.
        /// </summary>
        /// <value>
        ///     The cost per day.
        /// </value>
        public double CostPerDay { get; set; }

        /// <summary>
        ///     Gets or sets the late fee per day.
        /// </summary>
        /// <value>
        ///     The late fee per day.
        /// </value>
        public double LateFeePerDay { get; set; }
    }
}
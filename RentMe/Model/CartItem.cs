namespace RentMe.Model
{
    /// <summary>
    ///     Cart Item Class.
    /// </summary>
    internal class CartItem
    {
        /// <summary>
        ///     Gets or sets the qty.
        /// </summary>
        /// <value>
        ///     The qty.
        /// </value>
        public int Qty { get; set; }

        /// <summary>
        ///     Gets or sets the number of days.
        /// </summary>
        /// <value>
        ///     The number of days.
        /// </value>
        public int NumOfDays { get; set; }

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
        ///     Initializes a new instance of the <see cref="CartItem" /> class.
        /// </summary>
        /// <param name="item">The item.</param>
        public CartItem(Item item)
        {
            this.Qty = 1;
            this.NumOfDays = 1;
            this.Name = item.Name;
            this.ItemId = item.ItemId;
            this.Style = item.Style;
            this.Pattern = item.Pattern;
            this.Type = item.Type;
            this.CostPerDay = item.CostPerDay;
            this.LateFeePerDay = item.LateFeePerDay;
        }
    }
}
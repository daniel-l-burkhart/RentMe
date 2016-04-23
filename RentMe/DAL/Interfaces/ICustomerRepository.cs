namespace RentMe.DAL.Interfaces
{
    /// <summary>
    ///     Customer Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICustomerRepository<T> : IRepository<T>
    {
        /// <summary>
        ///     Searches the name of the by customer.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <returns></returns>
        SortableBindingList<T> SearchByCustomerName(string firstName, string lastName);

        /// <summary>
        ///     Searches the by customer phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns></returns>
        SortableBindingList<T> SearchByCustomerPhoneNumber(string phoneNumber);
    }
}
using RentMe.Model;

namespace RentMe.DAL.Interfaces
{
    /// <summary>
    ///     Rental transaction repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRentalTransactionRepository<T> : IRepository<T>
    {
        /// <summary>
        ///     Adds the and return.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        int AddAndReturn(RentalTransaction entity);

        SortableBindingList<RentalTransaction> GetByCustomerID(int id);
    }
}
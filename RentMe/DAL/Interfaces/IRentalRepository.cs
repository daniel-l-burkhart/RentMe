using RentMe.Model;

namespace RentMe.DAL.Interfaces
{
    public interface IRentalRepository<T> : IRepository<T>
    {
        SortableBindingList<Rental> GetByTransactionID(int id);
    }
}
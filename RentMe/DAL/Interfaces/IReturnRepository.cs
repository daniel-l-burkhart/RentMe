using RentMe.Model;

namespace RentMe.DAL.Interfaces
{
    public interface IReturnRepository<T> : IRepository<T>
    {
        int AddAndReturn(Return entity);

        SortableBindingList<Return> GetAllWithFinesByCustomerID(int custID);
    }
}
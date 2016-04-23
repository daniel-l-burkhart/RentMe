using RentMe.Model;

namespace RentMe.DAL.Interfaces
{
    public interface IReturnTransactionRepository<T> : IRepository<T>
    {
        int AddAndReturn(ReturnTransaction entity);
    }
}
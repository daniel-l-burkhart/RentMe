namespace RentMe.DAL.Interfaces
{
    public interface IEmployeeRepository<T> : IRepository<T>
    {
        bool isManager(int id);
    }
}
using RentMe.DAL.Interfaces;
using RentMe.DAL.Repository;
using RentMe.Model;

namespace RentMe.Controller
{
    /// <summary>
    ///     Rental Controller
    /// </summary>
    internal class RentalController
    {
        /// <summary>
        ///     The employee
        /// </summary>
        private readonly IRentalRepository<Rental> rental;

        /// <summary>
        ///     The employee
        /// </summary>
        private readonly IRentalTransactionRepository<RentalTransaction> rentalTransaction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentalController" /> class.
        /// </summary>
        public RentalController()
        {
            this.rentalTransaction = new RentalTransactionRepository();
            this.rental = new RentalRepository();
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddRental(Rental entity)
        {
            this.rental.Add(entity);
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteRental(Rental entity)
        {
            this.rental.Delete(entity);
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateRental(Rental entity)
        {
            this.rental.Update(entity);
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Rental GetRentalById(int id)
        {
            return this.rental.GetById(id);
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public SortableBindingList<Rental> GetRentalByTransactionId(int id)
        {
            return this.rental.GetByTransactionID(id);
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Rental> GetAllRentals()
        {
            return this.rental.GetAll();
        }

        /// <summary>
        ///     Adds the and return transaction.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int AddAndReturnTransaction(RentalTransaction entity)
        {
            return this.rentalTransaction.AddAndReturn(entity);
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void Add(RentalTransaction entity)
        {
            this.rentalTransaction.Add(entity);
        }

        /// <summary>
        ///     Deletes the transaction.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteTransaction(RentalTransaction entity)
        {
            this.rentalTransaction.Delete(entity);
        }

        /// <summary>
        ///     Gets all transactions.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<RentalTransaction> GetAllTransactions()
        {
            return this.rentalTransaction.GetAll();
        }

        /// <summary>
        ///     Gets the transaction by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public RentalTransaction GetTransactionById(int id)
        {
            return this.rentalTransaction.GetById(id);
        }

        public SortableBindingList<RentalTransaction> GetTransactionByCustomer(int id)
        {
            return this.rentalTransaction.GetByCustomerID(id);
        }

        /// <summary>
        ///     Updates the transaction.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateTransaction(RentalTransaction entity)
        {
            this.rentalTransaction.Update(entity);
        }
    }
}
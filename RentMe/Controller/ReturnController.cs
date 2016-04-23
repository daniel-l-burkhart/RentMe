using RentMe.DAL.Interfaces;
using RentMe.DAL.Repository;
using RentMe.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMe.Controller
{
    internal class ReturnController
    {
        private readonly IReturnRepository<Return> itemReturn;
        
        private readonly IReturnTransactionRepository<ReturnTransaction> returnTransaction;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentalController" /> class.
        /// </summary>
        public ReturnController()
        {
            this.returnTransaction = new ReturnTransactionRepository();
            this.itemReturn = new ReturnRepository();
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void AddReturn(Return entity)
        {
            this.itemReturn.Add(entity);
        }

        public int AddAndReturn(Return entity)
        {
            return this.itemReturn.AddAndReturn(entity);
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteReturn(Return entity)
        {
            this.itemReturn.Delete(entity);
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void UpdateReturn(Return entity)
        {
            this.itemReturn.Update(entity);
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Return GetReturnById(int id)
        {
            return this.itemReturn.GetById(id);
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Return> GetAllReturns()
        {
            return this.itemReturn.GetAll();
        }

        /// <summary>
        ///     Adds the and return transaction.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int AddAndReturnTransaction(ReturnTransaction entity)
        {
            return this.returnTransaction.AddAndReturn(entity);
        }

        /// <summary>
        ///     Deletes the transaction.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void DeleteTransaction(ReturnTransaction entity)
        {
            this.returnTransaction.Delete(entity);
        }

        /// <summary>
        ///     Gets all transactions.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<ReturnTransaction> GetAllTransactions()
        {
            return this.returnTransaction.GetAll();
        }

        public SortableBindingList<Return> GetAllWithFinesByCustomerID(int custID)
        {
            return this.itemReturn.GetAllWithFinesByCustomerID(custID);
        }
    }
}

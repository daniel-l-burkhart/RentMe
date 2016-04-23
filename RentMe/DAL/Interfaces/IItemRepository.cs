using System.Collections.Generic;

namespace RentMe.DAL.Interfaces
{
    /// <summary>
    ///     The Item Repository.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IItemRepository<T> : IRepository<T>
    {
        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        SortableBindingList<T> SearchFurniture(int itemId);

        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="type">The type.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        SortableBindingList<T> SearchFurniture(List<string> style, List<string> type, List<string> pattern);

        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="type">The type.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        SortableBindingList<T> SearchFurniture(string style, string type, string pattern);
    }
}
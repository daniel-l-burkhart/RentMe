using System.Collections.Generic;
using RentMe.DAL.Interfaces;
using RentMe.DAL.Repository;
using RentMe.Model;

namespace RentMe.Controller
{
    /// <summary>
    ///     The Item Controller
    /// </summary>
    public class ItemController
    {
        /// <summary>
        ///     The items
        /// </summary>
        private readonly IItemRepository<Item> items;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemController" /> class.
        /// </summary>
        public ItemController()
        {
            this.items = new ItemRepository();
        }

        /// <summary>
        ///     Inserts the item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        public void InsertItem(Item newItem)
        {
            this.items.Add(newItem);
        }

        /// <summary>
        ///     Gets all items.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Item> GetAllItems()
        {
            return this.items.GetAll();
        }

        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <param name="type">The type.</param>
        /// <param name="pattern">The pattern.</param>
        /// <returns></returns>
        public SortableBindingList<Item> SearchFurniture(List<string> style, List<string> type, List<string> pattern)
        {
            return this.items.SearchFurniture(style, type, pattern);
        }

        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="productId">The product identifier.</param>
        /// <returns></returns>
        public SortableBindingList<Item> SearchFurniture(int productId)
        {
            return this.items.SearchFurniture(productId);
        }
    }
}
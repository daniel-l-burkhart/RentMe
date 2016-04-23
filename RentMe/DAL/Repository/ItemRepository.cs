using System;
using System.Collections.Generic;
using System.Configuration;
using MySql.Data.MySqlClient;
using RentMe.DAL.Interfaces;
using RentMe.Model;

namespace RentMe.DAL.Repository
{
    /// <summary>
    ///     Items impelemntation of repository.
    /// </summary>
    public class ItemRepository : IItemRepository<Item>
    {
        /// <summary>
        ///     The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ItemRepository" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public ItemRepository(string connectionString = "MySqlDbConnection")
        {
            this.connectionString = connectionString;
        }

        /// <summary>
        ///     Adds the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(Item entity)
        {
            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var cmd =
                    new MySqlCommand(
                        "INSERT INTO ITEM(itemID, name, type, style, pattern, costPerDay, lateFeePerDay) VALUES(@itemID, @name, @type, @style, @pattern, @costPerDay, @lateFeePerDay)",
                        conn);

                cmd.Parameters.AddWithValue("@itemID", entity.ItemId);
                cmd.Parameters.AddWithValue("@name", entity.Name);
                cmd.Parameters.AddWithValue("@type", entity.Type);
                cmd.Parameters.AddWithValue("@style", entity.Style);
                cmd.Parameters.AddWithValue("@pattern", entity.Pattern);
                cmd.Parameters.AddWithValue("@costPerDay", entity.CostPerDay);
                cmd.Parameters.AddWithValue("@lateFeePerDay", entity.LateFeePerDay);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        ///     Deletes the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(Item entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(Item entity)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Item GetById(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Gets all.
        /// </summary>
        /// <returns></returns>
        public SortableBindingList<Item> GetAll()
        {
            var allItems = new SortableBindingList<Item>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from ITEM", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currItem = new Item
                            {
                                ItemId =
                                    (Convert.IsDBNull(dataReader["itemID"])
                                        ? int.MinValue
                                        : (int) dataReader["itemID"]),
                                Name = dataReader["name"] as string,
                                Type = dataReader["type"] as string,
                                Style = dataReader["style"] as string,
                                Pattern = dataReader["pattern"] as string,
                                CostPerDay =
                                    (Convert.IsDBNull(dataReader["costPerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["costPerDay"], 2)),
                                LateFeePerDay =
                                    (Convert.IsDBNull(dataReader["lateFeePerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["lateFeePerDay"], 2))
                            };

                            allItems.Add(currItem);
                        }
                    }
                }
            }

            return allItems;
        }

        /// <summary>
        ///     Searches the furniture.
        /// </summary>
        /// <param name="itemId">The item identifier.</param>
        /// <returns></returns>
        public SortableBindingList<Item> SearchFurniture(int itemId)
        {
            var searchedItems = new SortableBindingList<Item>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                using (var cmd = new MySqlCommand("SELECT * from ITEM WHERE itemId LIKE '" + itemId + "%'", conn))
                {
                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currItem = new Item
                            {
                                ItemId =
                                    (Convert.IsDBNull(dataReader["itemID"])
                                        ? int.MinValue
                                        : (int) dataReader["itemID"]),
                                Name = dataReader["name"] as string,
                                Type = dataReader["type"] as string,
                                Style = dataReader["style"] as string,
                                Pattern = dataReader["pattern"] as string,
                                CostPerDay =
                                    (Convert.IsDBNull(dataReader["costPerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["costPerDay"], 2)),
                                LateFeePerDay =
                                    (Convert.IsDBNull(dataReader["lateFeePerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["lateFeePerDay"], 2))
                            };

                            searchedItems.Add(currItem);
                        }
                    }
                }
            }

            return searchedItems;
        }

        public SortableBindingList<Item> SearchFurniture(List<string> style, List<string> type, List<string> pattern)
        {
            var searchedItems = new SortableBindingList<Item>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var styleConnString = " 1 ";
                var typeConnString = " 1 ";
                var patternConnString = " 1 ";

                if (pattern.Count > 0)
                {
                    patternConnString = " pattern ";
                }
                if (style.Count > 0)
                {
                    styleConnString = " style ";
                }
                if (type.Count > 0)
                {
                    typeConnString = " type ";
                }

                var styleString = "(";
                var patternString = "(";
                var typeString = "(";

                foreach (var s in style)
                {
                    styleString = styleString + "'" + s + "'" + ",";
                }
                foreach (var p in pattern)
                {
                    patternString = patternString + "'" + p + "'" + ",";
                }
                foreach (var t in type)
                {
                    typeString = typeString + "'" + t + "'" + ",";
                }

                styleString = styleString.TrimEnd(',') + ")";
                patternString = patternString.TrimEnd(',') + ")";
                typeString = typeString.TrimEnd(',') + ")";

                using (
                    var cmd =
                        new MySqlCommand(
                            "SELECT * from ITEM WHERE" + patternConnString + "AND" + styleConnString + "AND" +
                            typeConnString, conn))
                {
                    cmd.CommandText = cmd.CommandText.Replace("style", "style IN " + styleString);
                    cmd.CommandText = cmd.CommandText.Replace("pattern", "pattern IN " + patternString);
                    cmd.CommandText = cmd.CommandText.Replace("type", "type IN " + typeString);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currItem = new Item
                            {
                                ItemId =
                                    (Convert.IsDBNull(dataReader["itemID"])
                                        ? int.MinValue
                                        : (int) dataReader["itemID"]),
                                Name = dataReader["name"] as string,
                                Type = dataReader["type"] as string,
                                Style = dataReader["style"] as string,
                                Pattern = dataReader["pattern"] as string,
                                CostPerDay =
                                    (Convert.IsDBNull(dataReader["costPerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["costPerDay"], 2)),
                                LateFeePerDay =
                                    (Convert.IsDBNull(dataReader["lateFeePerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["lateFeePerDay"], 2))
                            };

                            searchedItems.Add(currItem);
                        }
                    }
                }
            }

            return searchedItems;
        }

        public SortableBindingList<Item> SearchFurniture(string style, string type, string pattern)
        {
            var searchedItems = new SortableBindingList<Item>();

            var connStr = ConfigurationManager.ConnectionStrings[this.connectionString].ConnectionString;

            using (var conn = new MySqlConnection(connStr))
            {
                conn.Open();

                var styleConnString = " 1 ";
                var typeConnString = " 1 ";
                var patternConnString = " 1 ";

                if (pattern != string.Empty)
                {
                    patternConnString = " pattern ";
                }
                if (style != string.Empty)
                {
                    styleConnString = " style ";
                }
                if (type != string.Empty)
                {
                    typeConnString = " type ";
                }

                using (
                    var cmd =
                        new MySqlCommand(
                            "SELECT * from ITEM WHERE" + patternConnString + "AND" + styleConnString + "AND" +
                            typeConnString, conn))
                {
                    cmd.CommandText = cmd.CommandText.Replace("style", "style = " + style);
                    cmd.CommandText = cmd.CommandText.Replace("pattern", "pattern = " + pattern);
                    cmd.CommandText = cmd.CommandText.Replace("type", "type = " + type);

                    using (var dataReader = cmd.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            var currItem = new Item
                            {
                                ItemId =
                                    (Convert.IsDBNull(dataReader["itemID"])
                                        ? int.MinValue
                                        : (int) dataReader["itemID"]),
                                Name = dataReader["name"] as string,
                                Type = dataReader["type"] as string,
                                Style = dataReader["style"] as string,
                                Pattern = dataReader["pattern"] as string,
                                CostPerDay =
                                    (Convert.IsDBNull(dataReader["costPerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["costPerDay"], 2)),
                                LateFeePerDay =
                                    (Convert.IsDBNull(dataReader["lateFeePerDay"])
                                        ? double.MinValue
                                        : Math.Round((double) dataReader["lateFeePerDay"], 2))
                            };

                            searchedItems.Add(currItem);
                        }
                    }
                }
            }

            return searchedItems;
        }
    }
}
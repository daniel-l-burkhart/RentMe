using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using RentMe.Controller;
using RentMe.Model;
using RentMe.Properties;

namespace RentMe.View
{
    /// <summary>
    ///     Code behind for UI.
    /// </summary>
    public partial class Home : Form
    {
        /// <summary>
        ///     The admin controller
        /// </summary>
        private readonly AdminController adminController;

        /// <summary>
        ///     The cart
        /// </summary>
        private readonly SortableBindingList<CartItem> cart = new SortableBindingList<CartItem>();

        /// <summary>
        ///     The customer
        /// </summary>
        private readonly CustomerController customer;

        /// <summary>
        ///     The controller
        /// </summary>
        private readonly EmployeeController employee;

        /// <summary>
        ///     The furn category
        /// </summary>
        private readonly List<string> furnCategory = new List<string>
        {
            "Lamp",
            "Fan",
            "Clock",
            "Chair",
            "Table",
            "Crib",
            "Dresser",
            "Sofa",
            "Bed"
        };

        /// <summary>
        ///     The furn color
        /// </summary>
        private readonly List<string> furnColor = new List<string>
        {
            " Red",
            "Orange",
            "Yellow",
            "Green",
            "Blue",
            "Purple",
            "White",
            "Black",
            "Brown"
        };

        /// <summary>
        ///     The furn pattern
        /// </summary>
        private readonly List<string> furnPattern = new List<string>
        {
            "Striped",
            "Solid",
            "PolkaDot",
            "Leopard",
            "Mohagany",
            "Stained",
            "Checkered",
            "Abstract",
            "Bronze"
        };

        /// <summary>
        ///     The furn style
        /// </summary>
        private readonly List<string> furnStyle = new List<string>
        {
            "ModernAngular",
            "ModernRounded",
            "ModernFlat",
            "Vintage",
            "Futuristic",
            "Simplistic",
            "Retro",
            "Antique"
        };

        /// <summary>
        ///     The items
        /// </summary>
        private readonly ItemController items;

        /// <summary>
        ///     The logged in user
        /// </summary>
        private readonly Employee loggedInUser;

        /// <summary>
        ///     The rental
        /// </summary>
        private readonly RentalController rental;

        /// <summary>
        ///     The return controller
        /// </summary>
        private readonly ReturnController returnController;

        /// <summary>
        ///     The add to cart pending
        /// </summary>
        private bool addToCartPending;

        /// <summary>
        ///     The rental customer
        /// </summary>
        private Customer rentalCustomer;

        /// <summary>
        ///     The view transactions pending
        /// </summary>
        private bool viewTransactionsPending;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Home" /> class.
        /// </summary>
        public Home()
        {
            this.InitializeComponent();

            this.employee = new EmployeeController();
            this.customer = new CustomerController();
            this.items = new ItemController();
            this.rental = new RentalController();
            this.adminController = new AdminController();
            this.returnController = new ReturnController();
            this.customerChooseButton.Enabled = false;

            this.showAllInventory();
            this.populateComboBoxes();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Home" /> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public Home(Employee user) : this()
        {
            this.loggedInUser = user;
            this.fillInAccountTab();
            this.processAdminManagerCredentials();
        }

        /// <summary>
        ///     Processes the admin manager credentials.
        /// </summary>
        private void processAdminManagerCredentials()
        {
            if (this.loggedInUser.AdminFlag != true)
            {
                this.rentMeTabControl.TabPages.Remove(this.adminTabPage);
            }
        }

        /// <summary>
        ///     Fills the in account tab.
        /// </summary>
        private void fillInAccountTab()
        {
            this.userTab.Text = this.loggedInUser.Fname + Resources.space + this.loggedInUser.Lname;
            this.accountFNameTextBox.Text = this.loggedInUser.Fname;
            this.accountLNameTextBox.Text = this.loggedInUser.Lname;
            this.accountAddressTextBox.Text = this.loggedInUser.Address;
            this.accountCityTextBox.Text = this.loggedInUser.City;
            this.accountStateTextBox.Text = this.loggedInUser.State;
            this.accountZipTextBox.Text = this.loggedInUser.ZipCode;
            this.accountEmailTextBox.Text = this.loggedInUser.Email;
            this.accountPhoneTextBox.Text = this.loggedInUser.PhoneNumber;
            this.accountSSNTextBox.Text = Resources.hiddenSSN +
                                          this.loggedInUser.Ssn.Substring(this.loggedInUser.Ssn.Length - 4);
        }

        /// <summary>
        ///     Handles the FormClosed event of the Home control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosedEventArgs" /> instance containing the event data.</param>
        private void Home_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        ///     Handles the Click event of the accountUpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void accountUpdateButton_Click(object sender, EventArgs e)
        {
            if (accountInputsCorrect(this.accountLayoutPanel))
            {
                var employee = new Employee
                {
                    Address = this.accountAddressTextBox.Text,
                    City = this.accountCityTextBox.Text,
                    State = this.accountStateTextBox.Text,
                    ZipCode = this.accountZipTextBox.Text,
                    PhoneNumber = this.accountPhoneTextBox.Text,
                    Email = this.accountEmailTextBox.Text,
                    EmployeeId = this.loggedInUser.EmployeeId,
                    PassWord = this.loggedInUser.PassWord
                };
                if (this.accountPasswordTextBox.Text.Length > 0)
                {
                    employee.PassWord = this.accountPasswordTextBox.Text;
                }

                this.employee.UpdateEmployee(employee);

                MessageBox.Show(Resources.informationHasBeenUpdated);
            }
            else
            {
                MessageBox.Show(Resources.CheckInputBoxesForErrors);
            }
        }

        /// <summary>
        ///     Handles the Click event of the accountLogOutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void accountLogOutButton_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        /// <summary>
        ///     Handles the Click event of the clearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryClearButton_Click(object sender, EventArgs e)
        {
            this.viewAllItems();
        }

        /// <summary>
        ///     Handles the Click event of the inventoryStyleLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryStyleLabel_Click(object sender, EventArgs e)
        {
            if (this.inventoryStyleCheckList.Visible)
            {
                this.inventoryStyleCheckList.Visible = false;
            }
            else
            {
                this.inventoryStyleCheckList.Visible = true;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inventoryCategoryLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryCategoryLabel_Click(object sender, EventArgs e)
        {
            if (this.inventoryCategoryCheckList.Visible)
            {
                this.inventoryCategoryCheckList.Visible = false;
            }
            else
            {
                this.inventoryCategoryCheckList.Visible = true;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inventoryPatternLabel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryPatternLabel_Click(object sender, EventArgs e)
        {
            if (this.inventoryPatternCheckList.Visible)
            {
                this.inventoryPatternCheckList.Visible = false;
            }
            else
            {
                this.inventoryPatternCheckList.Visible = true;
            }
        }

        /// <summary>
        ///     Determines whether [is check list closed] [the specified check list].
        /// </summary>
        /// <param name="checkList">The check list.</param>
        /// <returns></returns>
        private bool isCheckListClosed(Control checkList)
        {
            return checkList.Tag.ToString() == "Closed";
        }

        /// <summary>
        ///     Itemses the not contained.
        /// </summary>
        /// <param name="listOfItems">The list of items.</param>
        /// <param name="item">The item.</param>
        /// <returns></returns>
        private bool itemsNotContained(IEnumerable<Item> listOfItems, Item item)
        {
            var itemCount = 0;

            foreach (var piece in listOfItems)
            {
                if (piece.ItemId == item.ItemId)
                {
                    itemCount++;
                }
            }

            return itemCount == 0;
        }

        /// <summary>
        ///     Handles the SelectedValueChanged event of the inventoryCheckList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryCheckList_SelectedValueChanged(object sender, EventArgs e)
        {
            if (this.noItemsChecked())
            {
                this.inventoryDataGrid.DataSource = this.items.GetAllItems();
            }
            else
            {
                var styles = new List<string>();
                var types = new List<string>();
                var patterns = new List<string>();
                foreach (var item in this.inventoryStyleCheckList.CheckedItems)
                {
                    styles.Add(item.ToString());
                }
                foreach (var item in this.inventoryCategoryCheckList.CheckedItems)
                {
                    types.Add(item.ToString());
                }
                foreach (var item in this.inventoryPatternCheckList.CheckedItems)
                {
                    patterns.Add(item.ToString());
                }

                var items = this.items.SearchFurniture(styles, types, patterns);

                this.inventoryDataGrid.DataSource = items;
            }
        }

        /// <summary>
        ///     Handles the TextChanged event of the inventoryIDTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryIDTextBox_TextChanged(object sender, EventArgs e)
        {
            this.showAllInventory();
            if (this.inventoryIDTextBox.Text.Length > 0)
            {
                var inventoryId = 0;
                try
                {
                    inventoryId = int.Parse(this.inventoryIDTextBox.Text);
                }
                catch (FormatException formatException)
                {
                    MessageBox.Show("Item ID must be a number");
                }

                this.inventoryDataGrid.DataSource =
                    this.items.SearchFurniture(inventoryId);
            }
        }

        /// <summary>
        ///     Noes the items checked.
        /// </summary>
        /// <returns></returns>
        private bool noItemsChecked()
        {
            return (this.inventoryCategoryCheckList.CheckedItems.Count == 0) &&
                   (this.inventoryPatternCheckList.CheckedItems.Count == 0) &&
                   (this.inventoryStyleCheckList.CheckedItems.Count == 0);
        }

        /// <summary>
        ///     Shows all inventory.
        /// </summary>
        private void showAllInventory()
        {
            var items = this.items.GetAllItems();
            foreach (var item in items)
            {
                if (item.Style != null)
                {
                    if (!this.inventoryStyleCheckList.Items.Contains(item.Style))
                    {
                        this.inventoryStyleCheckList.Items.Add(item.Style);
                    }
                }
                if (item.Pattern != null)
                {
                    if (!this.inventoryPatternCheckList.Items.Contains(item.Pattern))
                    {
                        this.inventoryPatternCheckList.Items.Add(item.Pattern);
                    }
                }
                if (item.Type != null)
                {
                    if (!this.inventoryCategoryCheckList.Items.Contains(item.Type))
                    {
                        this.inventoryCategoryCheckList.Items.Add(item.Type);
                    }
                }
            }
            this.adjustListSize(this.inventoryCategoryCheckList);
            this.adjustListSize(this.inventoryPatternCheckList);
            this.adjustListSize(this.inventoryStyleCheckList);

            this.inventoryCategoryCheckList.Visible = false;
            this.inventoryStyleCheckList.Visible = false;
            this.inventoryPatternCheckList.Visible = false;

            this.viewAllItems();
        }

        /// <summary>
        ///     Views all items.
        /// </summary>
        private void viewAllItems()
        {
            this.inventoryDataGrid.DataSource = this.items.GetAllItems();

            for (var i = 0; i < this.inventoryCategoryCheckList.Items.Count; i++)
            {
                this.inventoryCategoryCheckList.SetItemChecked(i, false);
            }
            for (var i = 0; i < this.inventoryStyleCheckList.Items.Count; i++)
            {
                this.inventoryStyleCheckList.SetItemChecked(i, false);
            }
            for (var i = 0; i < this.inventoryPatternCheckList.Items.Count; i++)
            {
                this.inventoryPatternCheckList.SetItemChecked(i, false);
            }
        }

        /// <summary>
        ///     Adjusts the size of the list.
        /// </summary>
        /// <param name="checkListBox">The check ListBox.</param>
        private void adjustListSize(CheckedListBox checkListBox)
        {
            var height = checkListBox.ItemHeight*checkListBox.Items.Count;
            checkListBox.Height = height + checkListBox.Height - checkListBox.ClientSize.Height;
        }

        /// <summary>
        ///     Handles the Click event of the customerSearchButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerSearchButton_Click(object sender, EventArgs e)
        {
            if ((this.customerViewCustLastNameTextBox.Text == string.Empty &&
                 this.customerViewCustFirstNameTextBox.Text != string.Empty) ||
                (this.customerViewCustFirstNameTextBox.Text == string.Empty &&
                 this.customerViewCustLastNameTextBox.Text != string.Empty))
            {
                MessageBox.Show(Resources.FirstAndLastNameMustBeFilled);
            }
            else
            {
                this.customerViewDataGridView.DataSource =
                    this.customer.SearchCustomers(this.customerViewCustFirstNameTextBox.Text,
                        this.customerViewCustLastNameTextBox.Text, this.customerViewPhoneTextBox.Text);
            }
        }

        /// <summary>
        ///     Handles the Click event of the customerViewClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerViewClearButton_Click(object sender, EventArgs e)
        {
            this.clearInputBoxes(this.customerViewLayoutPanel);
            this.populateCustomers();
        }

        /// <summary>
        ///     Creates the items.
        /// </summary>
        private void createItems()
        {
            var rand = new Random();
            for (var i = 0; i < 300; i++)
            {
                var allItems = this.items.GetAllItems();

                var category = rand.Next(9);
                var style = rand.Next(8);
                var pattern = rand.Next(9);
                var color = rand.Next(9);

                var newItem = new Item
                {
                    Pattern = this.furnPattern[pattern],
                    Style = this.furnStyle[style],
                    Type = this.furnCategory[category],
                    Name = this.furnColor[color] + " " + this.furnCategory[category],
                    ItemId = int.Parse((category + 1 + style + 1 + pattern + 1 + color + 1).ToString())
                };
                switch (category)
                {
                    case 0:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 1, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 1:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 1, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 2:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 2, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 3:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 3, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 4:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 4, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 5:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 4, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 6:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 5, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 7:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 5, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                    case 8:
                        newItem.CostPerDay = Math.Round(rand.NextDouble() + 6, 2);
                        newItem.LateFeePerDay = Math.Round(newItem.CostPerDay + 1, 2);
                        break;
                }
                var contains = 0;
                foreach (var currItem in allItems)
                {
                    if (currItem.ItemId == newItem.ItemId)
                    {
                        contains++;
                    }
                }
                if (contains == 0)
                {
                    this.items.InsertItem(newItem);
                }
            }
        }

        /// <summary>
        ///     Handles the DataBindingComplete event of the dataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewBindingCompleteEventArgs" /> instance containing the event data.</param>
        private void dataGridView_DataBindingComplete(object sender,
            DataGridViewBindingCompleteEventArgs e)
        {
            var dataGridView = sender as DataGridView;
            // Put each of the columns into programmatic sort mode.
            if (dataGridView == null)
            {
                return;
            }
            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }

        /// <summary>
        ///     Handles the Click event of the inventoryAddToRentalButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void inventoryAddToRentalButton_Click(object sender, EventArgs e)
        {
            this.addToCart();
        }

        /// <summary>
        ///     Adds to cart.
        /// </summary>
        private void addToCart()
        {
            if (this.rentalCustomer != null)
            {
                this.rentalTransactionDataGridView.DataSource = this.cart;
                var itemsToAdd = new SortableBindingList<Item>();
                foreach (DataGridViewRow row in this.inventoryDataGrid.SelectedRows)
                {
                    var itemId = int.Parse(row.Cells[0].Value.ToString());
                    itemsToAdd.Add(this.items.SearchFurniture(itemId)[0]);
                }
                foreach (var item in itemsToAdd)
                {
                    var itemContained = 0;
                    foreach (var cartItem in this.cart)
                    {
                        if (item.ItemId == cartItem.ItemId)
                        {
                            cartItem.Qty++;
                            itemContained++;
                        }
                    }
                    if (itemContained == 0)
                    {
                        this.cart.Add(new CartItem(item));
                    }
                }
                this.rentMeTabControl.SelectedTab = this.employeeTab;
                this.employeeTabControl.SelectedTab = this.emlpoyeeRentTab;
                this.addToCartPending = false;
            }
            else
            {
                this.addToCartPending = true;
                this.chooseCustomer(Resources.selectACustomerBeforeCartCreation);
            }
        }

        /// <summary>
        ///     Handles the Click event of the rentalDeleteSelectedButon control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentalDeleteSelectedButon_Click(object sender, EventArgs e)
        {
            if (this.rentalTransactionDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show(Resources.deleteErrorMessage);
            }
            else
            {
                foreach (DataGridViewRow currentSelectedRow in this.rentalTransactionDataGridView.SelectedRows)
                {
                    foreach (var cartItem in this.cart)
                    {
                        if (cartItem.ItemId != int.Parse(currentSelectedRow.Cells[2].Value.ToString()))
                        {
                            continue;
                        }
                        this.cart.Remove(cartItem);
                        return;
                    }
                }
            }
        }

        /// <summary>
        ///     Handles the CellValueChanged event of the rentalTransactionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs" /> instance containing the event data.</param>
        private void rentalTransactionDataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            this.calculateRentalTotal();
        }

        /// <summary>
        ///     Calculates the rental total.
        /// </summary>
        /// <returns></returns>
        private double calculateRentalTotal()
        {
            var total = 0.0;
            if (this.rentalTransactionDataGridView.Rows.Count <= 0)
            {
                return total;
            }
            foreach (DataGridViewRow row in this.rentalTransactionDataGridView.Rows)
            {
                if (row.Cells.Count <= 5)
                {
                    continue;
                }
                total += double.Parse(row.Cells[0].Value.ToString())*double.Parse(row.Cells[4].Value.ToString());
                this.rentalTotalLabel.Text = Resources.dollarSign + total + Resources.space + Resources.perDay;
            }
            return total;
        }

        /// <summary>
        ///     Handles the RowsAdded event of the rentalTransactionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowsAddedEventArgs" /> instance containing the event data.</param>
        private void rentalTransactionDataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            this.calculateRentalTotal();
        }

        /// <summary>
        ///     Handles the RowsRemoved event of the rentalTransactionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewRowsRemovedEventArgs" /> instance containing the event data.</param>
        private void rentalTransactionDataGridView_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            this.calculateRentalTotal();
        }

        /// <summary>
        ///     Handles the Click event of the rentalCheckoutButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentalCheckoutButton_Click(object sender, EventArgs e)
        {
            if (this.rentalTransactionDataGridView.Rows.Count > 0 && this.rentalCustomer != null)
            {
                var rentalTransaction = new RentalTransaction
                {
                    DateSubmitted = DateTime.Now,
                    TotalCost = this.calculateRentalTotal(),
                    EmployeeId = this.loggedInUser.EmployeeId.ToString(),
                    CustomerId = this.rentalCustomer.CustomerId.ToString()
                };

                var transaction = this.rental.AddAndReturnTransaction(rentalTransaction);

                foreach (DataGridViewRow row in this.rentalTransactionDataGridView.Rows)
                {
                    var newRental = new Rental
                    {
                        TransactionId = transaction.ToString(),
                        ItemId = row.Cells[2].Value.ToString(),
                        Quantity = int.Parse(row.Cells[0].Value.ToString()),
                        DueDate = DateTime.Now.AddDays(int.Parse(row.Cells[1].Value.ToString()))
                    };
                    this.rental.AddRental(newRental);
                }

                MessageBox.Show(Resources.rentalSuccessful);

                this.cart.Clear();
                this.rentalTotalLabel.Text = "";

                this.showCustomerRentals(transaction);
            }
            else
            {
                MessageBox.Show(Resources.addItemsToCart);
            }
        }

        /// <summary>
        ///     Shows the customer rentals.
        /// </summary>
        /// <param name="id">The identifier.</param>
        private void showCustomerRentals(int id = -1)
        {
            if (id != -1)
            {
                var customerRentals = this.rental.GetRentalByTransactionId(id);
                this.employeeReturnDataGrid.DataSource = customerRentals;
                this.rentMeTabControl.SelectedTab = this.employeeTab;
                this.employeeTabControl.SelectedTab = this.employeeReturnTab;
            }
        }

        /// <summary>
        ///     Handles the Click event of the clearSQLQueryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void clearSQLQueryButton_Click(object sender, EventArgs e)
        {
            this.sqlQueryTextBox.Text = string.Empty;
        }

        /// <summary>
        ///     Handles the Click event of the runSQLQueryButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void runSQLQueryButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.adminQueryDataGridView.DataSource = this.adminController.RunAdminSqlQuery(this.sqlQueryTextBox.Text);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(Resources.queryFailed + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the Click event of the insertNewEmployeeSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void insertNewEmployeeSubmitButton_Click(object sender, EventArgs e)
        {
            if (allInputsCorrect(this.newEmployeeTabLayoutPanel))
            {
                this.createNewEmployee();
                this.clearInputBoxes(this.newEmployeeTabLayoutPanel);
            }
            else
            {
                MessageBox.Show(Resources.CheckInputBoxesForErrors);
            }
        }

        /// <summary>
        ///     Creates the new employee.
        /// </summary>
        private void createNewEmployee()
        {
            try
            {
                this.adminController.InsertEmployee(this.insertNewEmployeeFirstNameTextBox.Text,
                    this.insertNewEmployeeLastNameTextBox.Text, this.insertNewEmployeeAddressTextBox.Text
                    , this.insertNewEmployeeCityTextBox.Text, this.insertNewEmployeeStateComboBox.Text,
                    this.insertNewEmployeeZipTextBox.Text, this.insertNewEmployeePhoneNumberTextBox.Text
                    , this.insertNewEmployeeEmailTextBox.Text, this.insertNewEmployeeSSNTextBox.Text,
                    this.userNameTextBox.Text, this.insertNewEmployeePasswordTextBox.Text, this.adminFlagComboBox.Text);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(Resources.insertFailed + exception.Message);
            }
        }

        /// <summary>
        ///     Handles the Click event of the employeeReturnAllButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeeReturnAllButton_Click(object sender, EventArgs e)
        {
            this.employeeReturnDataGrid.DataSource = this.rental.GetAllRentals();
        }

        /// <summary>
        ///     Handles the Click event of the customerTransactionsButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerTransactionsButton_Click(object sender, EventArgs e)
        {
            if (this.rentalCustomer != null)
            {
                this.viewCustomerTransactions();
            }
            else
            {
                this.chooseCustomer(Resources.selectACustomerBeforeTransactionViewing);
                this.viewTransactionsPending = true;
            }
        }

        /// <summary>
        ///     Views the customer transactions.
        /// </summary>
        private void viewCustomerTransactions()
        {
            this.employeeReturnDataGrid.DataSource = this.rental.GetTransactionByCustomer(this.rentalCustomer.CustomerId);
            this.rentMeTabControl.SelectedTab = this.employeeTab;
            this.employeeTabControl.SelectedTab = this.employeeReturnTab;
            this.viewTransactionsPending = false;
        }

        /// <summary>
        ///     Chooses the customer.
        /// </summary>
        /// <param name="message">The message.</param>
        private void chooseCustomer(string message)
        {
            MessageBox.Show(message);
            this.rentMeTabControl.SelectedTab = this.customersTab;
            this.customerTabControl.SelectedTab = this.customerViewTab;
        }

        /// <summary>
        ///     Handles the DoubleClick event of the employeeReturnDataGrid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeeReturnDataGrid_DoubleClick(object sender, EventArgs e)
        {
            if (this.employeeReturnDataGrid.DataSource != null
                && this.employeeReturnDataGrid.DataSource.GetType() == typeof (SortableBindingList<RentalTransaction>)
                && this.employeeReturnDataGrid.SelectedRows[0].Index < this.employeeReturnDataGrid.Rows.Count - 1)

            {
                var transactionID = this.employeeReturnDataGrid.SelectedRows[0].Cells[0].Value.ToString();
                this.employeeReturnDataGrid.DataSource = this.rental.GetRentalByTransactionId(int.Parse(transactionID));
            }
        }

        /// <summary>
        ///     Handles the Click event of the employeeReturnRentalButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeeReturnRentalButton_Click(object sender, EventArgs e)
        {
            if (this.employeeReturnDataGrid.Rows.Count > 0
                && this.employeeReturnDataGrid.DataSource.GetType() == typeof (SortableBindingList<Rental>))
            {
                var returnTransaction = new ReturnTransaction
                {
                    ReturnDate = DateTime.Now,
                    EmployeeId = this.loggedInUser.EmployeeId.ToString(),
                    CustomerId = this.rentalCustomer.CustomerId.ToString()
                };

                var transaction = this.returnController.AddAndReturnTransaction(returnTransaction);

                foreach (DataGridViewRow row in this.employeeReturnDataGrid.SelectedRows)
                {
                    var rental = this.rental.GetRentalById(int.Parse(row.Cells[0].Value.ToString()));

                    if (!rental.IsReturned)
                    {
                        var dueDate = rental.DueDate;
                        var finePerDay = this.items.SearchFurniture(int.Parse(rental.ItemId))[0].LateFeePerDay;

                        var newReturn = new Return
                        {
                            Fine = (DateTime.Now - dueDate).TotalDays*finePerDay,
                            RentalId = rental.RentalId,
                            RentalTransactionId = rental.TransactionId,
                            ReturnTransactionId = transaction.ToString()
                        };
                        var returnID = 0;
                        try
                        {
                            returnID = this.returnController.AddAndReturn(newReturn);
                        }
                        catch (MySqlException exception)
                        {
                            MessageBox.Show(Resources.insertFailed + exception.Message);
                        }

                        rental.IsReturned = true;

                        this.rental.UpdateRental(rental);
                    }
                }

                MessageBox.Show(Resources.returnSuccessful);

                this.employeeReturnDataGrid.DataSource = this.rental.GetAllRentals();
                this.employeeTabControl.SelectedTab = this.employeeSettlePenaltiesTab;
            }
            else
            {
                MessageBox.Show(Resources.emptyRentals);
            }
        }

        /// <summary>
        ///     Shows the penalties.
        /// </summary>
        private void showPenalties()
        {
            this.employeeSettleDataGridView.DataSource =
                this.returnController.GetAllWithFinesByCustomerID(this.rentalCustomer.CustomerId);
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the employeeTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.employeeTabControl.SelectedIndex == 2)
            {
                if (this.rentalCustomer != null)
                {
                    this.showPenalties();
                }
                else
                {
                    this.chooseCustomer(Resources.selectACustomerBeforeTransactionViewing);
                }
            }
        }

        /// <summary>
        ///     Handles the Click event of the employeeSettleButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeeSettleButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in this.employeeSettleDataGridView.SelectedRows)
            {
                var itemReturn = this.returnController.GetReturnById(int.Parse(row.Cells[1].Value.ToString()));
                itemReturn.Fine = 0;
                this.returnController.UpdateReturn(itemReturn);
            }
            this.showPenalties();
        }

        /// <summary>
        ///     Handles the SelectionChanged event of the rentalTransactionDataGridView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentalTransactionDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (this.rentalTransactionDataGridView.SelectedRows.Count == 1)
            {
                this.rentalQtyUpDown.Enabled = true;
                this.rentalDaysUpDown.Enabled = true;
                this.rentalQtyUpDown.Value =
                    int.Parse(this.rentalTransactionDataGridView.SelectedRows[0].Cells[0].Value.ToString());
                this.rentalDaysUpDown.Value =
                    int.Parse(this.rentalTransactionDataGridView.SelectedRows[0].Cells[1].Value.ToString());
            }
            else
            {
                this.rentalQtyUpDown.Enabled = false;
                this.rentalDaysUpDown.Enabled = false;
            }
        }

        /// <summary>
        ///     Handles the ValueChanged event of the rentalQtyUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentalQtyUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.rentalTransactionDataGridView.SelectedRows[0].Cells[0].Value =
                int.Parse(this.rentalQtyUpDown.Value.ToString());
        }

        /// <summary>
        ///     Handles the ValueChanged event of the rentalDaysUpDown control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentalDaysUpDown_ValueChanged(object sender, EventArgs e)
        {
            this.rentalTransactionDataGridView.SelectedRows[0].Cells[1].Value =
                int.Parse(this.rentalDaysUpDown.Value.ToString());
        }

        /// <summary>
        ///     Handles the 1 event of the runSQLQueryButton_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void runSQLQueryButton_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (this.sqlQueryTextBox.Text == string.Empty)
                {
                    MessageBox.Show(Resources.emptyQueryMessage);
                }
                else
                {

                    var adminResult = this.adminController.RunAdminSqlQuery(this.sqlQueryTextBox.Text);

                    this.adminQueryDataGridView.Columns.Clear();

                    this.createDataGridView(adminResult, this.adminQueryDataGridView);

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(Resources.sqlQueryFailed + Environment.NewLine + exception.Message);
            }
        }

        /// <summary>
        ///     Creates the data grid view.
        /// </summary>
        /// <param name="queryResult">The admin result.</param>
        /// <param name="dataGridView">The data grid view.</param>
        private void createDataGridView(List<CustomDataGridView> queryResult, DataGridView dataGridView)
        {
            foreach (var t in queryResult[0].Columns)
            {
                dataGridView.Columns.Add(t, t);
            }

            for (var i = 0; i < queryResult.Count; i++)
            {
                this.addValueToRow(queryResult, i, dataGridView);
            }
        }

        /// <summary>
        ///     Adds the value to row.
        /// </summary>
        /// <param name="queryResult">The admin result.</param>
        /// <param name="i">The i.</param>
        /// <param name="currDataGridView"></param>
        private void addValueToRow(IReadOnlyList<CustomDataGridView> queryResult, int i, DataGridView currDataGridView)
        {
            currDataGridView.Rows.Add();

            for (var j = 0; j < queryResult[i].Columns.Count; j++)
            {
                currDataGridView[j, i].Value = queryResult[i].Items[j];
            }
        }

        /// <summary>
        ///     Handles the Click event of the generateReportButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void generateReportButton_Click(object sender, EventArgs e)
        {
            var fromDate = this.fromDateTimePicker.Value;
            var toDate = this.toDateTimePicker.Value.Date;

            try
            {
                var reportResult = this.adminController.GenerateReport(fromDate, toDate);

                this.adminReportDataGridView.Columns.Clear();

                this.createDataGridView(reportResult, this.adminReportDataGridView);
            }
            catch (Exception generateReportException)
            {
                MessageBox.Show(Resources.reportGenerationFailed + Environment.NewLine + generateReportException.Message);
            }
        }

        #region tab control selected index changes events

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the rentMeTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void rentMeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var homeTab = sender as TabControl;
            if (homeTab == null)
            {
                return;
            }
            switch (homeTab.SelectedIndex)
            {
                case InventoryTabIndex:
                    this.showAllInventory();
                    break;
                case CustomersTabIndex:
                    this.populateCustomers();
                    break;
                case EmployeesTabIndex:
                    this.calculateRentalTotal();
                    break;
                case ManagersTabIndex:
                    break;
            }
        }

        /// <summary>
        ///     Handles the SelectedIndexChanged event of the customerTabControl control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            var customerTab = sender as TabControl;
            if (customerTab == null)
            {
                return;
            }
            switch (customerTab.SelectedIndex)
            {
                case NewCustomerTabIndex:
                    this.customerChooseButton.Enabled = false;
                    break;
                case ViewCustomerTabIndex:
                    this.populateCustomers();
                    this.customerChooseButton.Enabled = true;
                    break;
                case UpdateCustomerTabIndex:
                    this.customerChooseButton.Enabled = false;
                    break;
            }
        }

        #endregion

        #region customer tab nav button click actions

        /// <summary>
        ///     Handles the Click event of the customerNewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerNewButton_Click(object sender, EventArgs e)
        {
            this.customerTabControl.SelectedTab = this.customerNewTab;
        }

        /// <summary>
        ///     Handles the Click event of the customerViewButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerViewButton_Click(object sender, EventArgs e)
        {
            if (this.customerViewDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }
            this.rentalCustomer =
                this.customer.GetCustomerById(
                    int.Parse(this.customerViewDataGridView.SelectedRows[0].Cells[0].Value.ToString()));
            MessageBox.Show(Resources.customerSelected + this.rentalCustomer.Fname + Resources.space +
                            this.rentalCustomer.Lname);
            if (this.addToCartPending)
            {
                this.addToCart();
            }
            else if (this.viewTransactionsPending)
            {
                this.viewCustomerTransactions();
            }
        }

        /// <summary>
        ///     Handles the Click event of the customerUpdateButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerUpdateButton_Click(object sender, EventArgs e)
        {
            if (this.customerViewDataGridView.SelectedRows.Count <= 0)
            {
                return;
            }

            this.customerTabControl.SelectedIndex = UpdateCustomerTabIndex;
            this.populateUpdateFields();
            this.customerUpdateSubmitButton.Enabled = true;
        }

        /// <summary>
        ///     Handles the Click event of the customerDeleteButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerDeleteButton_Click(object sender, EventArgs e)
        {
            if (this.customerTabControl.SelectedIndex == ViewCustomerTabIndex
                && this.customerViewDataGridView.SelectedRows.Count > 0)
            {
                this.deleteCustomer();
            }
        }

        #endregion

        #region customer tab submit and clear button actions

        /// <summary>
        ///     Handles the Click event of the customerNewSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerNewSubmitButton_Click(object sender, EventArgs e)
        {
            if (allInputsCorrect(this.customerNewLayoutPanel))
            {
                this.createNewCustomer();
                this.clearInputBoxes(this.customerNewLayoutPanel);
            }
            else
            {
                MessageBox.Show(Resources.CheckInputBoxesForErrors);
            }
        }

        /// <summary>
        ///     Handles the Click event of the customerNewClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerNewClearButton_Click(object sender, EventArgs e)
        {
            this.clearInputBoxes(this.customerNewLayoutPanel);
        }

        /// <summary>
        ///     Handles the Click event of the customerUpdateSubmitButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerUpdateSubmitButton_Click(object sender, EventArgs e)
        {
            if (allInputsCorrect(this.customerUpdateLayoutPanel))
            {
                var customerSelection = this.customerViewDataGridView.SelectedRows[0];
                this.updateCustomer(int.Parse(customerSelection.Cells[IdCell].Value.ToString()));
                this.clearInputBoxes(this.customerUpdateLayoutPanel);
            }
            else
            {
                MessageBox.Show(Resources.CheckInputBoxesForErrors);
            }
        }

        /// <summary>
        ///     Handles the Click event of the customerUpdateClearButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void customerUpdateClearButton_Click(object sender, EventArgs e)
        {
            this.clearInputBoxes(this.customerUpdateLayoutPanel);
        }

        #endregion

        #region customer tab SQL queries

        /// <summary>
        ///     Creates the new customer.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void createNewCustomer()
        {
            /**
             * Hey Steven. Should we not just pass in the string values to the 
             * controller and then to the model instead of having model objects
             * in our view class?
             */
            var customer = new Customer
            {
                Fname = this.customerNewFNameTextBox.Text,
                Lname = this.customerNewLNameTextBox.Text,
                Address = this.customerNewAddressTextBox.Text,
                City = this.customerNewCityTextBox.Text,
                State = this.customerNewStateComboBox.Text,
                ZipCode = this.customerNewZipcodeTextBox.Text,
                PhoneNumber = this.customerNewPhoneTextBox.Text,
                Email = this.customerNewEmailTextBox.Text,
                Ssn = this.customerNewSSNTextBox.Text
            };

            try
            {
                this.customer.InsertCustomer(customer);
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(Resources.insertFailed + exception.Message);
            }

            this.customerTabControl.SelectedTab = this.customerViewTab;
        }

        /// <summary>
        ///     Populates the customers.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void populateCustomers()
        {
            this.customerViewDataGridView.DataSource = this.customer.GetAllCustomers();
        }

        /// <summary>
        ///     Updates the customer.
        /// </summary>
        /// <param name="customerId">The customer identifier.</param>
        /// <exception cref="Exception"></exception>
        private void updateCustomer(int customerId)
        {
            var customer = new Customer
            {
                CustomerId = customerId,
                Fname = this.customerUpdateFNameTextBox.Text,
                Lname = this.customerUpdateLNameTextBox.Text,
                Address = this.customerUpdateAddressTextBox.Text,
                City = this.customerUpdateCityTextBox.Text,
                State = this.customerUpdateStateComboBox.Text,
                ZipCode = this.customerUpdateZipcodeTextBox.Text,
                PhoneNumber = this.customerUpdatePhoneTextBox.Text,
                Email = this.customerUpdateEmailTextBox.Text,
                Ssn = this.customerUpdateSSNTextBox.Text
            };

            this.customer.UpdateCustomer(customer);

            this.customerTabControl.SelectedTab = this.customerViewTab;
        }

        /// <summary>
        ///     Deletes the customer.
        /// </summary>
        /// <exception cref="Exception"></exception>
        private void deleteCustomer()
        {
            try
            {
                var currentCustomerId = (int) this.customerViewDataGridView.SelectedRows[0].Cells[0].Value;
                var currentCustomer = this.customer.GetCustomerById(currentCustomerId);
                this.customer.DeleteCustomer(currentCustomer);
                this.populateCustomers();
            }
            catch (MySqlException exception)
            {
                MessageBox.Show(Resources.deleteFailed + exception.Message);
            }
        }

        #endregion

        #region customer tab helper methods        

        /// <summary>
        ///     Populates the update fields.
        /// </summary>
        private void populateUpdateFields()
        {
            var customerSelection = this.customerViewDataGridView.SelectedRows[0];
            var currCustomer =
                this.customer.GetCustomerById(int.Parse(customerSelection.Cells[IdCell].Value.ToString()));

            this.customerUpdateFNameTextBox.Text = currCustomer.Fname;
            this.customerUpdateLNameTextBox.Text = currCustomer.Lname;
            this.customerUpdateAddressTextBox.Text = currCustomer.Address;
            this.customerUpdateCityTextBox.Text = currCustomer.City;
            this.customerUpdateStateComboBox.Text = currCustomer.State;
            this.customerUpdateZipcodeTextBox.Text = currCustomer.ZipCode;
            this.customerUpdatePhoneTextBox.Text = currCustomer.PhoneNumber;
            this.customerUpdateEmailTextBox.Text = currCustomer.Email;
            this.customerUpdateSSNTextBox.Text = currCustomer.Ssn;
        }

        /// <summary>
        ///     Populates the combo boxes.
        /// </summary>
        private void populateComboBoxes()
        {
            this.populateStateComboBoxes();
        }

        /// <summary>
        ///     Populates the state combo boxes.
        /// </summary>
        private void populateStateComboBoxes()
        {
            var states = new List<string>
            {
                "Alabama",
                "Alaska",
                "Arizona",
                "Arkansas",
                "California",
                "Colorado",
                "Connecticut",
                "Delaware",
                "Florida",
                "Georgia",
                "Hawaii",
                "Idaho",
                "Illinois",
                "Indiana",
                "Iowa",
                "Kansas",
                "Kentucky",
                "Louisiana",
                "Maine",
                "Maryland",
                "Massachusetts",
                "Michigan",
                "Minnesota",
                "Mississippi",
                "Missouri",
                "Montana",
                "Nebraska",
                "Nevada",
                "New Hampshire",
                "New Jersey",
                "New Mexico",
                "New York",
                "North Carolina",
                "North Dakota",
                "Ohio",
                "Oklahoma",
                "Oregon",
                "Pennsylvania",
                "Rhode Island",
                "South Carolina",
                "South Dakota",
                "Tennessee",
                "Texas",
                "Utah",
                "Vermont",
                "Virginia",
                "Washington",
                "West Virginia",
                "Wisconsin",
                "Wyoming"
            };
            this.customerNewStateComboBox.Items.Add(states);
            this.customerUpdateStateComboBox.Items.Add(states);
        }

        #endregion

        #region employee tab nav button click actions

        /// <summary>
        ///     Handles the Click event of the employeesRentFurnitureButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeesRentFurnitureButton_Click(object sender, EventArgs e)
        {
            this.employeeTabControl.SelectedTab = this.emlpoyeeRentTab;
        }

        /// <summary>
        ///     Handles the Click event of the employeesReturnFurnitureButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void employeesReturnFurnitureButton_Click(object sender, EventArgs e)
        {
            this.employeeTabControl.SelectedTab = this.employeeReturnTab;
        }

        /// <summary>
        ///     Handles the Click event of the EmployeesSettlePenaltiesButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void EmployeesSettlePenaltiesButton_Click(object sender, EventArgs e)
        {
            this.employeeTabControl.SelectedTab = this.employeeSettlePenaltiesTab;
        }

        #endregion

        #region constants

        /// <summary>
        ///     The inventory tab index
        /// </summary>
        private const int InventoryTabIndex = 0;

        /// <summary>
        ///     The customers tab index
        /// </summary>
        private const int CustomersTabIndex = 1;

        /// <summary>
        ///     The employees tab index
        /// </summary>
        private const int EmployeesTabIndex = 2;

        /// <summary>
        ///     The managers tab index
        /// </summary>
        private const int ManagersTabIndex = 3;

        /// <summary>
        ///     The new customer tab index
        /// </summary>
        private const int NewCustomerTabIndex = 0;

        /// <summary>
        ///     The view customer tab index
        /// </summary>
        private const int ViewCustomerTabIndex = 1;

        /// <summary>
        ///     The update customer tab index
        /// </summary>
        private const int UpdateCustomerTabIndex = 2;

        /// <summary>
        ///     The rent furniture tab index
        /// </summary>
        private const int RentFurnitureTabIndex = 0;

        /// <summary>
        ///     The return furniture tab index
        /// </summary>
        private const int ReturnFurnitureTabIndex = 1;

        /// <summary>
        ///     The settle penalties tab index
        /// </summary>
        private const int SettlePenaltiesTabIndex = 2;

        /// <summary>
        ///     The first name cell
        /// </summary>
        private const int IdCell = 0;

        #endregion

        #region Form validation methods

        /// <summary>
        ///     Handles the TextChanged event of the stringTextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void stringTextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox != null && (textBox.Text.Length == 0 || textBox.Text.Length >= 45))
            {
                textBox.BackColor = Color.LightSalmon;
                textBox.Tag = "Incorrect";
            }
            else
            {
                if (textBox == null)
                {
                    return;
                }
                textBox.BackColor = Color.White;
                textBox.Tag = "Correct";
            }
        }

        /// <summary>
        ///     Handles the TextChanged event of the stringComboBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void stringComboBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as ComboBox;
            if (textBox != null && (textBox.Text.Length == 0 || textBox.Text.Length >= 45))
            {
                textBox.BackColor = Color.LightSalmon;
                textBox.Tag = "Incorrect";
            }
            else
            {
                if (textBox == null)
                {
                    return;
                }
                textBox.BackColor = Color.White;
                textBox.Tag = "Correct";
            }
        }

        /// <summary>
        ///     Handles the TextChanged event of the int11TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void int11TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            long textInt;
            if (textBox != null &&
                (textBox.Text.Length == 0 || textBox.Text.Length > 11 || !long.TryParse(textBox.Text, out textInt)))
            {
                textBox.BackColor = Color.LightSalmon;
                textBox.Tag = "Incorrect";
            }
            else
            {
                if (textBox == null)
                {
                    return;
                }
                textBox.BackColor = Color.White;
                textBox.Tag = "Correct";
            }
        }

        /// <summary>
        ///     Handles the TextChanged event of the int9TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void int9TextBox_TextChanged(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            long textInt;
            if (textBox != null &&
                (textBox.Text.Length == 0 || textBox.Text.Length > 9 || !long.TryParse(textBox.Text, out textInt)))
            {
                textBox.BackColor = Color.LightSalmon;
                textBox.Tag = "Incorrect";
            }
            else
            {
                if (textBox == null)
                {
                    return;
                }
                textBox.BackColor = Color.White;
                textBox.Tag = "Correct";
            }
        }

        /// <summary>
        ///     Alls the inputs correct.
        /// </summary>
        /// <param name="layoutPanel">The layout panel.</param>
        /// <returns></returns>
        private static bool allInputsCorrect(TableLayoutPanel layoutPanel)
        {
            var numOfWrongInputs = 0;
            foreach (Control control in layoutPanel.Controls)
            {
                if (control.GetType() == typeof (TextBox))
                {
                    var textBox = control as TextBox;
                    if (textBox != null && (string) textBox.Tag != "Correct")
                    {
                        numOfWrongInputs++;
                    }
                }
                else if (control.GetType() == typeof (ComboBox))
                {
                    var comboBox = control as ComboBox;
                    if (comboBox != null && (string) comboBox.Tag != "Correct")
                    {
                        numOfWrongInputs++;
                    }
                }
            }

            return numOfWrongInputs <= 0;
        }

        /// <summary>
        ///     Alls the inputs correct.
        /// </summary>
        /// <param name="layoutPanel">The layout panel.</param>
        /// <returns></returns>
        private static bool accountInputsCorrect(TableLayoutPanel layoutPanel)
        {
            var numOfWrongInputs = 0;
            foreach (Control control in layoutPanel.Controls)
            {
                if (control.GetType() == typeof (TextBox) && (string) control.Tag != "Void")
                {
                    var textBox = control as TextBox;
                    if (textBox != null && (string) textBox.Tag != "Correct")
                    {
                        numOfWrongInputs++;
                    }
                }
                else if (control.GetType() == typeof (ComboBox))
                {
                    var comboBox = control as ComboBox;
                    if (comboBox != null && (string) comboBox.Tag != "Correct")
                    {
                        numOfWrongInputs++;
                    }
                }
            }

            return numOfWrongInputs <= 0;
        }

        /// <summary>
        ///     Clears the input boxes.
        /// </summary>
        /// <param name="layoutPanel">The layout panel.</param>
        private void clearInputBoxes(Control layoutPanel)
        {
            foreach (Control control in layoutPanel.Controls)
            {
                if (control.GetType() == typeof (TextBox))
                {
                    var textBox = control as TextBox;
                    if (textBox != null)
                    {
                        textBox.Text = string.Empty;
                        textBox.BackColor = Color.White;
                        textBox.Text = string.Empty;
                        textBox.Tag = "Incorrect";
                    }
                }
                else if (control.GetType() == typeof (ComboBox))
                {
                    var comboBox = control as ComboBox;
                    if (comboBox != null)
                    {
                        comboBox.Text = string.Empty;
                        comboBox.BackColor = Color.White;
                        comboBox.Text = string.Empty;
                        comboBox.Tag = "Incorrect";
                    }
                }
                if (control.HasChildren &&
                    (control.GetType() == typeof (TableLayoutPanel) || control.GetType() == typeof (FlowLayoutPanel)))
                {
                    this.clearInputBoxes(control as Panel);
                }
            }
        }

        #endregion

        private void clearSQLQueryButton_Click_1(object sender, EventArgs e)
        {
            this.adminQueryDataGridView.Columns.Clear();
            this.adminQueryDataGridView.Rows.Clear();

            this.sqlQueryTextBox.Clear();
        }
    }
}
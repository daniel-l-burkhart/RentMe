using System;
using System.Drawing;
using System.Windows.Forms;
using RentMe.Controller;

namespace RentMe.View
{
    /// <summary>
    ///     The login screen for the interface.
    /// </summary>
    public partial class RentMeForm : Form
    {
        /// <summary>
        ///     The controller
        /// </summary>
        private readonly EmployeeController employee;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RentMeForm" /> class.
        /// </summary>
        public RentMeForm()
        {
            this.InitializeComponent();
            this.employee = new EmployeeController();
        }

        /// <summary>
        ///     Handles the Click event of the loginButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void loginButton_Click(object sender, EventArgs e)
        {
            this.attemptLogin();
        }

        /// <summary>
        ///     Disables username and password fields then attemps login
        /// </summary>
        private void attemptLogin()
        {
            this.toggleUserPassEnabled();

            this.checkCredentials();
        }

        /// <summary>
        ///     Attempts the login and wipes password field if login fails.
        /// </summary>
        private void checkCredentials()
        {
            var employees = this.employee.GetAllEmployees();
            foreach (var employee in employees)
            {
                if (employee.UserName != this.usernameBox.Text || employee.PassWord != this.passwordBox.Text)
                {
                    continue;
                }
                Form home = new Home(employee);
                Hide();
                home.ShowDialog();
                break;
            }
        }

        private void toggleUserPassEnabled()
        {
            this.usernameBox.Enabled = !this.usernameBox.Enabled;
            this.passwordBox.Enabled = !this.passwordBox.Enabled;
        }

        /// <summary>
        ///     Handles the Focus event of the box control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void box_Focus(object sender, EventArgs e)
        {
            if ((sender as TextBox) == this.usernameBox)
            {
                resetUsernameBox(this.usernameBox, "username");
            }
            else
            {
                resetUsernameBox(this.passwordBox, "password");
            }
        }

        /// <summary>
        ///     Resets the username box.
        /// </summary>
        /// <param name="box">The box.</param>
        /// <param name="boxText">The box text.</param>
        private static void resetUsernameBox(Control box, string boxText)
        {
            if (box.Text == boxText)
            {
                box.Text = string.Empty;
                box.ForeColor = Color.Black;
            }
            else if (box.Text == string.Empty)
            {
                box.Text = boxText;
                box.ForeColor = Color.Gray;
            }
        }

        private void login_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char) 13)
            {
                this.attemptLogin();
            }
        }

        #region constants

        #endregion
    }
}
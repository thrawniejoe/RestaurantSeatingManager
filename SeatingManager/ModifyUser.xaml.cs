using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SeatingManager
{
    /// <summary>
    /// Interaction logic for ModifyUser.xaml
    /// </summary>
    public partial class ModifyUser : Window
    {
        private int userID;

        public delegate void Refresh();
        public event Refresh refreshPage;

        public ModifyUser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
            SeatingManagerDBDataSetTableAdapters.usersTableAdapter seatingManagerDBDataSetusersTableAdapter = new SeatingManagerDBDataSetTableAdapters.usersTableAdapter();
            seatingManagerDBDataSetusersTableAdapter.Fill(seatingManagerDBDataSet.users);
            CollectionViewSource usersViewSource = ((CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();

            var context = new SeatingManagerDBEntities();
            user p = context.users.First(u => u.userID == userID);
            firstNameTextBox.Text = p.firstName;
            lastNameTextBox.Text = p.lastName;
            phoneTextBox.Text = p.phone;
            isActiveTextBox.Text = Convert.ToString(p.isActive);
            sectionIDTextBox.Text = Convert.ToString(p.sectionID);
            passwordTextBox.Text = p.password;
            dateHiredDatePicker.SelectedDate = p.dateHired;

            switch (p.title)
            {
                case "Administrator": titleTextBox.SelectedIndex = 0; break;
                case "Manager": titleTextBox.SelectedIndex = 1; break;
                case "Host": titleTextBox.SelectedIndex = 2; break;
                case "Server": titleTextBox.SelectedIndex = 6; break;
          
            }

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (Validate())
            {
                //Updates user in database
                using (var db = new SeatingManagerDBEntities())
                {
                    var result = db.users.SingleOrDefault(b => b.userID == userID);
                    if (result != null)
                    {
                        result.firstName = firstNameTextBox.Text;
                        result.lastName = lastNameTextBox.Text;
                        result.phone = phoneTextBox.Text;
                        result.isActive = Convert.ToByte(isActiveTextBox.Text);
                        result.sectionID = Convert.ToInt16(sectionIDTextBox.Text);

                        // set salt and hashed password, store it to the database
                        string salt = ModelClass.Password.CreateSalt(12);
                        string password = ModelClass.Password.Hash(passwordTextBox.Text, salt);
                        result.password = password;
                        result.passwordSalt = salt;

                        //result.password = passwordTextBox.Text;
                        switch (titleTextBox.Text)
                        {
                            case "Administrator": result.role = 0; break;
                            case "Manager": result.role = 2; break;
                            case "Host": result.role = 1; break;
                            case "Server": result.role = 3; break;
                            default: result.role = 3; break;
                        }
                        result.dateHired = dateHiredDatePicker.SelectedDate.Value;
                        db.SaveChanges();
                    }
                }
                
                this.Close();

            }




            
            refreshPage();
        }

        private Boolean Validate()
        {
            var check = true;
            const int min = 2;
            const int passMin = 6;
            const int max = 30;
            const int exact = 10;
            lblErrorMessage.Content = "";
            

            //First Name Validation
            if (!Validations.CheckEmptyString(firstNameTextBox.Text))
            {
                lblErrorMessage.Content = "First name cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(firstNameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "First name must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(firstNameTextBox.Text))
            {
                lblErrorMessage.Content = "First name must be letters only.";
                check = false;
            }

            //Last Name Validation
            if (!Validations.CheckEmptyString(lastNameTextBox.Text))
            {
                lblErrorMessage.Content = "Last name cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(lastNameTextBox.Text, min, max))
            {
                lblErrorMessage.Content = "Last name must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(lastNameTextBox.Text))
            {
                lblErrorMessage.Content = "Last name must be letters only.";
                check = false;
            }

            //Phone Number Validation
            if (!Validations.CheckIfNumeric(phoneTextBox.Text))
            {
                lblErrorMessage.Content = "Phone number must be numbers only.";
                check = false;
            }

            if (!Validations.CheckIfExact(phoneTextBox.Text, exact))
            {
                lblErrorMessage.Content = "Phone number must be " + exact + " in length";
                check = false;
            }

            //Password Validation
            if (!Validations.CheckEmptyString(passwordTextBox.Text))
            {
                lblErrorMessage.Content = "Password cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(passwordTextBox.Text, passMin, max))
            {
                lblErrorMessage.Content = "Password must be between " + passMin + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(passwordTextBox.Text))
            {
                lblErrorMessage.Content = "Password must be letters only.";
                check = false;
            }

            //Title Selection Validation
            if (titleTextBox.SelectedIndex == -1)
            {
                lblErrorMessage.Content = "Must select a title";
                check = false;
            }

            //Hire Date Validation
            if (!Validations.CheckEmptyString(dateHiredDatePicker.Text))
            {
                lblErrorMessage.Content = "Hire date cannot be blank.";
                check = false;
            }









            return check;
        }

        public void getID(int myid)
        {
            userID = myid;
        }
    }
}

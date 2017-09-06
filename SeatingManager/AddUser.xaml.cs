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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    /// 
    

    public partial class AddUser : Window
    {
        public delegate void Refresh();
        public event Refresh refreshPage;
        

        public AddUser()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //gets connection info for database
            var context = new SeatingManager.SeatingManagerDBEntities();
            if(Validate())
            {
                //creates new user object
                var User = new user();

                User.firstName = firstNameTextBox.Text;
                User.lastName = lastNameTextBox.Text;
                User.password = passwordTextBox.Text;
                User.title = titleTextBox.Text;

                switch (titleTextBox.Text)
                {
                    case "Admin": User.role = 0; break;
                    case "Manager": User.role = 2; break;
                    case "Host": User.role = 1; break;
                    case "Server": User.role = 3; break;
                    default: User.role = 3; break;
                }
                //LIST OF ROLES
                // 0 => ADMIN
                // 1 => HOST
                // 2 => MANAGER
                // 3 => SERVER   
                User.phone = phoneTextBox.Text;
                User.isActive = 0;
                User.isOnDuty = 0;
                User.sectionID = 0;
                User.dateHired = Convert.ToDateTime(dateHiredDatePicker.SelectedDate);
                //adds User object to db
                context.users.Add(User);
                context.SaveChanges();
                refreshPage();
                //MessageBox.Show("User Added To System");
                this.Close();
            }           
        }


        private Boolean Validate()
        {
            var check = true;
            const int min = 2;
            const int passMin = 6;
            const int max = 30;
            const int exact = 10;
            lblFirstNameError.Content = "";
            lblLastNameError.Content = "";
            lblPhoneError.Content = "";
            lblPasswordError.Content = "";
            lblTitleError.Content = "";

            //First Name Validation
            if (!Validations.CheckEmptyString(firstNameTextBox.Text))
            {
                lblFirstNameError.Content = "field cannot be blank.";
                check = false;
            }
        
            if(!Validations.CheckStringMinMax(firstNameTextBox.Text,min,max))
            {
                lblFirstNameError.Content = "Must be between " + min + " and " + max + " in length";
                check = false;
            }
         
            if (!Validations.CheckIfAlpha(firstNameTextBox.Text))
            {
               lblFirstNameError.Content = "Field must be letters only.";
                check = false;
            }

            //Last Name Validation
            if (!Validations.CheckEmptyString(lastNameTextBox.Text))
            {
                lblLastNameError.Content = "field cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(lastNameTextBox.Text, min, max))
            {
                lblLastNameError.Content = "Must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(lastNameTextBox.Text))
            {
                lblLastNameError.Content = "Field must be letters only.";
                check = false;
            }

            //Phone Number Validation
            if (!Validations.CheckIfNumeric(phoneTextBox.Text))
            {
                lblPhoneError.Content = "Field must be numbers only.";
                check = false;
            }

            if (!Validations.CheckIfExact(phoneTextBox.Text, exact))
            {
                lblPhoneError.Content = "field must be" + exact + " in length";
                check = false;
            }

            //Password Validation
            if (!Validations.CheckEmptyString(passwordTextBox.Text))
            {
                lblPasswordError.Content = "field cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(passwordTextBox.Text, passMin, max))
            {
                lblPasswordError.Content = "Must be between " + passMin + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(passwordTextBox.Text))
            {
                lblPasswordError.Content = "Field must be letters only.";
                check = false;
            }

            if (titleTextBox.SelectedIndex == -1)
            {
                lblTitleError.Content = "Must select a title";
                check = false;
            }
           













            return check;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            dateHiredDatePicker.SelectedDate = DateTime.Today;
        }

        private void lastNameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
    }
}

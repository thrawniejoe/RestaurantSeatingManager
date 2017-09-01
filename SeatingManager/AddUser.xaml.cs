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

            //VALIDATION GOES HERE











            //creates new user object
            user User = new user();

            User.firstName = firstNameTextBox.Text;
            User.lastName = lastNameTextBox.Text;
            User.password = passwordTextBox.Text;
            User.title = titleTextBox.Text;

            switch (titleTextBox.Text)
            {
                case "Admin":       User.role = 0;      break;
                case "Manager":     User.role = 2;      break;
                case "Host":        User.role = 1;      break;
                case "Server":      User.role = 3;      break;
                default:            User.role = 3;      break;
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
        }
    }
}

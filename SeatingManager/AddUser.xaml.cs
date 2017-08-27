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
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            //gets connection info for database
            var context = new SeatingManager.SeatingManagerDBEntities();

            //creates new user object
            user User = new user();

            User.firstName = firstNameTextBox.Text;
            User.lastName = lastNameTextBox.Text;
            User.password = passwordTextBox.Text;
            User.title = titleTextBox.Text;
            User.role = Convert.ToInt16(roleTextBox.Text);
            User.phone = phoneTextBox.Text;
            User.isActive = 0;
            User.isOnDuty = 0;
            User.sectionID = 0;
            User.dateHired = Convert.ToDateTime(dateHiredDatePicker);
            //adds User object to db
            context.users.Add(User);
            context.SaveChanges();
            MessageBox.Show("User Added To System");
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
            //SeatingManager.SeatingManagerDBDataSetTableAdapters.usersTableAdapter seatingManagerDBDataSetusersTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.usersTableAdapter();
            //seatingManagerDBDataSetusersTableAdapter.Fill(seatingManagerDBDataSet.users);
            //System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            //usersViewSource.View.MoveCurrentToFirst();
        }
    }
}

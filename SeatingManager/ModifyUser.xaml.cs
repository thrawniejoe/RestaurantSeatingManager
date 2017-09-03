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
            //passwordTextBox.Text = p.password;
            titleTextBox.Text = p.title;
            dateHiredDatePicker.SelectedDate = p.dateHired;
            

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            //INSERT VALIDATION CHECK HERE




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
                    //string salt = ModelClass.Password.CreateSalt(12);
                    //string password = ModelClass.Password.Hash(passwordTextBox.Text, salt);
                    //result.password = password;
                    //result.passwordSalt = salt;

                    result.password = passwordTextBox.Text;
                    result.title = titleTextBox.Text;
                    result.dateHired = dateHiredDatePicker.SelectedDate.Value;
                    db.SaveChanges();
                }
            }
            refreshPage();
            this.Close();
        }

        public void getID(int myid)
        {
            userID = myid;
        }
    }
}

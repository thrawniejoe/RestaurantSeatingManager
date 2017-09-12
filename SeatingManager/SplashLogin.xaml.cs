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
    /// Interaction logic for SplashLogin.xaml
    /// </summary>
    public partial class SplashLogin : Window
    {
        
        public SplashLogin()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //----------------MUST HAVE THIS TO CREATE YOUR CONTEXT AS IS DONE ON THE btnLogin-------------------------//
            SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
        }

        private void Move(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            string userName = txtUsername.Text;

            var uCheck = (from u in context.users
                          where u.firstName.Equals(userName)
                          select u.password).SingleOrDefault();


            var getRole = (from u in context.users
                           where u.firstName.Equals(userName)
                           select u.role).SingleOrDefault();

            //if (Convert.ToString(uCheck) == txtPassword.Text)
            if (ModelClass.Password.ConfirmPassword(userName, txtPassword.Text))
            {
                //MessageBox.Show("confirmed");
                Window mainWindow = null;
                switch (getRole)
                {
                    case 0:
                        mainWindow = new ManagerMain();
                        break;
                    case 1:
                        mainWindow = new ManagerMain();
                        break;
                    case 2:
                        mainWindow = new MainWindow();
                        break;
                }
                Properties.Settings.Default.CurrentUserRole = getRole; //saves the user role to the applcation settings file
                Properties.Settings.Default.Save();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid Username and/or Password. Please try again.");
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            byte[] salt = ModelClass.Password.CreateSalt(12);
            byte[] hashedPassword1 = ModelClass.Password.Hash("password", salt);
            byte[] hashedPassword2 = ModelClass.Password.Hash("password", salt);

            if (hashedPassword1.SequenceEqual(hashedPassword2))
            {
                MessageBox.Show("consistent");
            }
            else
            {
                MessageBox.Show("not the same");
            }
        }
    }
}

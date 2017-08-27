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
            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
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

            //MessageBox.Show(Convert.ToString(uCheck));
           if (Convert.ToString(uCheck) == txtPassword.Text)
            {
            switch (getRole)
                {
                    case 0:
                        ManagerMain manager = new ManagerMain();
                        manager.Show();
                        break;
                    case 1:
                        MainWindow main = new MainWindow();
                        main.Show();
                        break;
                    case 2:
                        MainWindow main2 = new MainWindow();
                        main2.Show();
                        break;
                }
                
                this.Close();
            }


            //if (txtUsername.Text.Equals("john") && txtPassword.Password.Equals("password"))
            //{
            //    main.Show();
            //    this.Close();
            //}
            else
            {
                MessageBox.Show("Invalid Username and/or Password. Please try again.");
            }
        }


    }
}

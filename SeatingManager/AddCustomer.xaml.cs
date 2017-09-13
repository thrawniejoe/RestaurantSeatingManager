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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            //gets connection info for database
            var context = new SeatingManager.SeatingManagerDBEntities();
            
            //creates new customer object
            customer cust = new customer();

            cust.customerName = customerNameTextBox.Text;
            cust.timeIn = timeInDatePicker.DisplayDate;
            cust.wait = Convert.ToByte(waitTextBox.Text);

            if(radioReservationNo.IsChecked != null && (bool)radioReservationNo.IsChecked)
                cust.reservation = Convert.ToByte(0);
            else
            {
                cust.reservation = Convert.ToByte(1);
            }

            //adds customer object to db
            context.customers.Add(cust);
            context.SaveChanges();
            MessageBox.Show("Customer Added To System");
        }
    }
}

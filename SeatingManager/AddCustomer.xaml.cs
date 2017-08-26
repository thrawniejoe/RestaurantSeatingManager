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
            // Load data into the table customer. You can modify this code as needed.
            //SeatingManager.SeatingManagerDBDataSetTableAdapters.customerTableAdapter seatingManagerDBDataSetcustomerTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.customerTableAdapter();
            //seatingManagerDBDataSetcustomerTableAdapter.Fill(seatingManagerDBDataSet.customer);
            //System.Windows.Data.CollectionViewSource customerViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customerViewSource")));
            //customerViewSource.View.MoveCurrentToFirst();
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
            cust.reservation = Convert.ToByte(reservationComboBox.Text);

            //adds customer object to db
            context.customers.Add(cust);
            MessageBox.Show("Customer Added To System");
        }
    }
}

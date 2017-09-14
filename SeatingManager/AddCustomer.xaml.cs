using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            txtTimeIn.Text = DateTime.Now.ToShortTimeString();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            

            if (Validate())
            {
                //gets connection info for database
                var context = new SeatingManager.SeatingManagerDBEntities();
                var dateIn = DateTime.Now;
                var timeIn = Convert.ToDateTime(txtTimeIn.Text);
                var dateTimeIn = dateIn.Date + timeIn.TimeOfDay;

                //creates new customer object
                customer cust = new customer();
                cust.customerName = customerNameTextBox.Text;
                cust.timeIn = dateTimeIn;
                cust.timeMade = DateTime.Now;
                switch (cboWaitTime.Text)
                {
                    case "5": cust.wait = 5; break;
                    case "10": cust.wait = 10; break;
                    case "15": cust.wait = 15; break;
                    case "20": cust.wait = 20; break;
                    case "25": cust.wait = 25; break;
                    case "30": cust.wait = 30; break;
                    case "35": cust.wait = 35; break;
                    case "40": cust.wait = 40; break;
                    case "45": cust.wait = 45; break; 
                }

                if (radioReservationNo.IsChecked != null && (bool)radioReservationNo.IsChecked)
                    cust.reservation = Convert.ToByte(0);
                else
                {
                    cust.reservation = Convert.ToByte(1);
                }
                context.customers.Add(cust);
                context.SaveChanges();
                MessageBox.Show("Customer Added To System");
                this.Close();
            }
            
        }

        private Boolean Validate() {

            var check = true;
            var min = 2;
            var max = 30;
            lblCustNameError.Content = "";
            lblTimeInError.Content = "";
            lblWaitTimeError.Content = "";


            //Check customer name validation
            if (!Validations.CheckEmptyString(customerNameTextBox.Text))
            {
                lblCustNameError.Content = "field cannot be blank.";
                check = false;
            }

            if (!Validations.CheckStringMinMax(customerNameTextBox.Text, min, max))
            {
                lblCustNameError.Content = "Must be between " + min + " and " + max + " in length";
                check = false;
            }

            if (!Validations.CheckIfAlpha(customerNameTextBox.Text))
            {
                lblCustNameError.Content = "Field must be letters only.";
                check = false;
            }

            //Check wait validation
            if (cboWaitTime.SelectedIndex == -1)
            {
                lblWaitTimeError.Content = "field cannot be blank.";
                check = false;
            }


            //Check time in validation
            if (!Validations.CheckIfValidTime(txtTimeIn.Text))
            {
                lblTimeInError.Content = "Field needs to be proper time entry 12:34 AM|PM.";
                check = false;
            }



            return check;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

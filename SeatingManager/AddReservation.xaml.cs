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
    /// Interaction logic for AddReservation.xaml
    /// </summary>
    public partial class AddReservation : Window
    {
        public delegate void RefreshList();
        public event RefreshList EngageRefreshList;
        SeatingManagerDBEntities db = null;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        public AddReservation()
        {
            InitializeComponent();
            // set hours
            for (int x = 1; x <= 12; x++)
            {
                cboHour.Items.Add(x);
            }
            // set minutes
            cboMinute.Items.Add("00");
            int y = 15;
            while (y < 60)
            {
                cboMinute.Items.Add(y.ToString());
                y += 15;
            }
            // set time of the day
            cboAmPm.Items.Add("AM");
            cboAmPm.Items.Add("PM");
        }



        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            db = new SeatingManagerDBEntities();
            DateTime rightNow = DateTime.Now;

            //set selected date
            dtpResDate.SelectedDate = rightNow;

            // set selected time
            if (rightNow.Hour == 0)
            {
                cboHour.SelectedIndex = cboHour.Items.IndexOf(12);
                cboAmPm.SelectedIndex = 0;
            }
            else if (rightNow.Hour <= 12)
            {
                if (rightNow.Minute >= 45)
                {
                    cboHour.SelectedIndex = cboHour.Items.IndexOf(rightNow.Hour + 1);
                }
                else
                {
                    cboHour.SelectedIndex = cboHour.Items.IndexOf(rightNow.Hour);
                }
                cboAmPm.SelectedIndex = 0;
            }
            else
            {
                if (rightNow.Minute >= 45)
                {
                    cboHour.SelectedIndex = cboHour.Items.IndexOf(rightNow.Hour + 1 - 12);
                }
                else
                {
                    cboHour.SelectedIndex = cboHour.Items.IndexOf(rightNow.Hour - 12);
                }
                cboAmPm.SelectedIndex = 1;
            }
            if (rightNow.Minute <= 15)
            {
                cboMinute.SelectedIndex = 1;
            }
            else if (rightNow.Minute > 15 && rightNow.Minute <= 30)
            {
                cboMinute.SelectedIndex = 2;
            }
            else if (rightNow.Minute > 30 && rightNow.Minute <= 45)
            {
                cboMinute.SelectedIndex = 3;
            }
            else
            {
                cboMinute.SelectedIndex = 0;
            }


            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            // Load data into the table customers. You can modify this code as needed.
            SeatingManager.SeatingManagerDBDataSetTableAdapters.customersTableAdapter seatingManagerDBDataSetcustomersTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.customersTableAdapter();
            seatingManagerDBDataSetcustomersTableAdapter.Fill(seatingManagerDBDataSet.customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int custId = Convert.ToInt32(cboCustomer.SelectedValue);
            Reservation r = new Reservation();
            int hour = 0;
            if (cboAmPm.SelectedValue.ToString() == "AM")
            {
                hour = Convert.ToInt32(cboHour.SelectedValue);
            }
            else
            {
                hour = Convert.ToInt32(cboHour.SelectedValue) + 12;
            }
            //r.Customer = db.customers.Where(c => c.customerID == custId).Single();
            int id;
            if (db.Reservations.Count() == 0)
            {
                id = 1;
            }
            else
            {
                id = db.Reservations.Last().Id + 1;
            }
            //r.CustomerID = custId;
            //r.ReservationDateTime = new DateTime(dtpResDate.SelectedDate.Value.Year,
            //                                     dtpResDate.SelectedDate.Value.Month,
            //                                     dtpResDate.SelectedDate.Value.Day,
            //                                     hour,
            //                                     Convert.ToInt32(cboMinute.SelectedValue),
            //                                     0);
            //db.Reservations.Add(r);
            SeatingManagerDBDataSet ds = new SeatingManagerDBDataSet();
            SeatingManagerDBDataSet.ReservationRow newReservationRow;
            newReservationRow = ds.Reservation.NewReservationRow();
            newReservationRow.Id = id;
            newReservationRow.CustomerID = custId;
            newReservationRow.ReservationDateTime = new DateTime(dtpResDate.SelectedDate.Value.Year,
                                                     dtpResDate.SelectedDate.Value.Month,
                                                     dtpResDate.SelectedDate.Value.Day,
                                                     hour,
                                                     Convert.ToInt32(cboMinute.SelectedValue),
                                                     0);
            ds.Reservation.Rows.Add(newReservationRow);
            SeatingManagerDBDataSetTableAdapters.ReservationTableAdapter ta = new SeatingManagerDBDataSetTableAdapters.ReservationTableAdapter();
            ta.Update(ds.Reservation);
            EngageRefreshList();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

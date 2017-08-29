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
    /// Interaction logic for ManagerMain.xaml
    /// </summary>
    public partial class ManagerMain : Window
    {
        public ManagerMain()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser aU = new AddUser();
            aU.refreshPage += RefreshList;
            aU.Owner = this;
            //aU.Owner = Application.Current.MainWindow;
            aU.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            aU.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            SeatingManager.SeatingManagerDBDataSet seatingManagerDBDataSet = ((SeatingManager.SeatingManagerDBDataSet)(this.FindResource("seatingManagerDBDataSet")));
            // Load data into the table users. You can modify this code as needed.
            SeatingManager.SeatingManagerDBDataSetTableAdapters.usersTableAdapter seatingManagerDBDataSetusersTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.usersTableAdapter();
            seatingManagerDBDataSetusersTableAdapter.Fill(seatingManagerDBDataSet.users);
            System.Windows.Data.CollectionViewSource usersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("usersViewSource")));
            usersViewSource.View.MoveCurrentToFirst();
            // Load data into the table tablemaps. You can modify this code as needed.
            SeatingManager.SeatingManagerDBDataSetTableAdapters.tablemapsTableAdapter seatingManagerDBDataSettablemapsTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.tablemapsTableAdapter();
            seatingManagerDBDataSettablemapsTableAdapter.Fill(seatingManagerDBDataSet.tablemaps);
            System.Windows.Data.CollectionViewSource tablemapsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tablemapsViewSource")));
            tablemapsViewSource.View.MoveCurrentToFirst();
            // Load data into the table customers. You can modify this code as needed.
            SeatingManager.SeatingManagerDBDataSetTableAdapters.customersTableAdapter seatingManagerDBDataSetcustomersTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.customersTableAdapter();
            seatingManagerDBDataSetcustomersTableAdapter.Fill(seatingManagerDBDataSet.customers);
            System.Windows.Data.CollectionViewSource customersViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("customersViewSource")));
            customersViewSource.View.MoveCurrentToFirst();
        }

        public void RefreshList()
        {
            //usersListView.Items.Clear();
            usersListView.InvalidateProperty(ListView.ItemsSourceProperty);
            var context = new SeatingManager.SeatingManagerDBEntities();
            var getList = (from u in context.users                          
                           select u);
            usersListView.ItemsSource = null;
            usersListView.ItemsSource = getList.ToList();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshList();
        }

        //Removes a user from the list
        private void btnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            user nu = new user { userID = myid };
            context.users.Attach(nu); //attacts the user object by the id given to the object above
            context.users.Remove(nu);
            context.SaveChanges();
            RefreshList();
        }
    }
}

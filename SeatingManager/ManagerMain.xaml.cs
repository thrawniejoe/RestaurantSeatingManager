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
            var context = new SeatingManager.SeatingManagerDBEntities();
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

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(usersListView.ItemsSource);
            //view.Filter = UserFilter;
            var getList = (from u in context.users
                           where u.title == "Server"
                           select u);
            serverListView.ItemsSource = getList.ToList();
            cboFilterList.ItemsSource = FilterList();
        }

        public void RefreshList()
        {
            usersListView.InvalidateProperty(ListView.ItemsSourceProperty);
            var context = new SeatingManager.SeatingManagerDBEntities();

            //Refresh UserListView
            usersListView.ItemsSource = null;

            if (cboFilterList.SelectedItem == null)
            {
                var getList = (from u in context.users
                           select u);
                usersListView.ItemsSource = getList.ToList();
            }
            else
            {
                var getList = (from nu in context.users
                                where nu.title == cboFilterList.SelectedValue.ToString()
                select nu);
                usersListView.ItemsSource = getList.ToList();
            }

            //Refresh ServerListView
            serverListView.ItemsSource = null;
            var getServ = (from su in context.users
                           where su.title == "Server"
                           select su);
            serverListView.ItemsSource = getServ.ToList();


            //Refresh CustomerListView
            customersListView.ItemsSource = null;
            var getCust = (from su in context.users
                           where su.title == "Server"
                           select su);
            serverListView.ItemsSource = getCust.ToList();

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

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                user nu = new user { userID = myid };
                context.users.Attach(nu); //attaches the user object by the id given to the object above
                context.users.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshList();
        }

        //Filters the UserList to whatever is entered in the combobox
        private void User_Filter()
        {
            if(cboFilterList.SelectedItem != null)
            {
                var context = new SeatingManager.SeatingManagerDBEntities();
                string filterValuue = cboFilterList.SelectedValue.ToString();
                usersListView.InvalidateProperty(ListView.ItemsSourceProperty);

                var getListA = (from u in context.users
                                where u.title == filterValuue
                                select u);

                usersListView.ItemsSource = null;
                usersListView.ItemsSource = getListA.ToList();
            }
        }



        //Creates a list of User Roles
        private List<string> FilterList()
        {
            List<string> titleList = new List<string>();
            titleList.Add("Administrator");
            titleList.Add("Manager");
            titleList.Add("Host");
            titleList.Add("Server");

            return titleList;
        }

        private void cboFilterList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User_Filter();
        }

        private void btnClearFilter_Click(object sender, RoutedEventArgs e)
        {
            usersListView.InvalidateProperty(ListView.ItemsSourceProperty);
            var context = new SeatingManager.SeatingManagerDBEntities();
            var getList = (from u in context.users
                           select u);
            usersListView.ItemsSource = null;
            usersListView.ItemsSource = getList.ToList();
            cboFilterList.SelectedItem = null;
        }
    }
}

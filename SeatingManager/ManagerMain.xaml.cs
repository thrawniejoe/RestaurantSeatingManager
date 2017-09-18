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
        public delegate void getUserID(int uid);

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

            //CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(usersDataGrid.ItemsSource);
            //view.Filter = UserFilter;
            var getServerList = (from u in context.users
                                 where u.title == "Server"
                                 select u);
            serversDataGrid.ItemsSource = getServerList.ToList();
            cboFilterList.ItemsSource = FilterList();
            int userRole = Properties.Settings.Default.CurrentUserRole;
            switch (userRole)
            {
                case 0: //admin
                    //currently don't need to do anything
                    break;
                case 1:  //manager
                    var getManagerUserList = (from mul in context.users
                                              where mul.role != 0
                                              select mul);
                    usersDataGrid.ItemsSource = getManagerUserList.ToList();
                    break;
            }
            // Load data into the table tablesections. You can modify this code as needed.
            SeatingManager.SeatingManagerDBDataSetTableAdapters.tablesectionsTableAdapter seatingManagerDBDataSettablesectionsTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.tablesectionsTableAdapter();
            seatingManagerDBDataSettablesectionsTableAdapter.Fill(seatingManagerDBDataSet.tablesections);
            System.Windows.Data.CollectionViewSource tablesectionsViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("tablesectionsViewSource")));
            tablesectionsViewSource.View.MoveCurrentToFirst();
            RefreshList();
            cboTableType.ItemsSource = fillTableType();
        }

        public void RefreshList()
        {
            usersDataGrid.InvalidateProperty(ListView.ItemsSourceProperty);
            var context = new SeatingManager.SeatingManagerDBEntities();

            //Refresh UserListView
            usersDataGrid.ItemsSource = null;

            if (cboFilterList.SelectedItem == null)
            {
                var getList = (from u in context.users
                           select u);
                usersDataGrid.ItemsSource = getList.ToList();
            }
            else
            {
                var getList = (from nu in context.users
                                where nu.title == cboFilterList.SelectedValue.ToString()
                select nu);
                usersDataGrid.ItemsSource = getList.ToList();
            }

            //Refresh Server GridView
            serversDataGrid.ItemsSource = null;
            var getServ = (from su in context.users
                           where su.title == "Server"
                           select su);
            serversDataGrid.ItemsSource = getServ.ToList();


            //Refresh Customer GridView
            customersDataGrid.ItemsSource = null;
            var getCust = (from su in context.users
                           where su.title == "Server"
                           select su);
            serversDataGrid.ItemsSource = getCust.ToList();

            //Refresh Table Section GridView
            tablesectionsDataGrid.ItemsSource = null;
            sectionColorComboBox.ItemsSource = null;
            var getSec = (from su in context.tablesections
                           select su);
            tablesectionsDataGrid.ItemsSource = getSec.ToList();
            sectionColorComboBox.ItemsSource = getSec.ToList();

            //Refresh Table Gridview
            tablemapsDataGrid.ItemsSource = null;
            var getTm = (from tm in context.tablemaps
                          select tm);
            tablemapsDataGrid.ItemsSource = getTm.ToList();

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
                usersDataGrid.InvalidateProperty(ListView.ItemsSourceProperty);

                var getListA = (from u in context.users
                                where u.title == filterValuue
                                select u);

                usersDataGrid.ItemsSource = null;
                usersDataGrid.ItemsSource = getListA.ToList();
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
            usersDataGrid.InvalidateProperty(ListView.ItemsSourceProperty);
            var context = new SeatingManager.SeatingManagerDBEntities();
            var getList = (from u in context.users
                           select u);
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = getList.ToList();
            cboFilterList.SelectedItem = null;
        }

        private void btnModifyUser_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender; //creates new button and gets the button that was pressed data
            int i = Convert.ToInt16(btn.Tag);

            ModifyUser Mu = new ModifyUser();
            getUserID del = new getUserID(Mu.getID);
            del(i); //sets delagate value
            Mu.refreshPage += RefreshList;  //forwards refresh button to the modifty user form
            Mu.Owner = this; //sets the owner of this new form the managerMain form
            Mu.WindowStartupLocation = WindowStartupLocation.CenterOwner; //center the new dialog on the mainwindow
            Mu.ShowDialog();
        }

        private void btnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this table?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                tablemap nu = new tablemap { tableID = myid };
                context.tablemaps.Attach(nu); //attaches the user object by the id given to the object above
                context.tablemaps.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshList();
        }

        private void btnDeleteServer_Click(object sender, RoutedEventArgs e)
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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurrentUserRole = -1; //saves the user role to the applcation settings file
            Properties.Settings.Default.Save();
            SplashLogin sL = new SplashLogin();
            sL.Show();
        }

        private void btnAddSection_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();

            tablesection Section = new tablesection();
            if (txtSection.Text != null || txtSection.Text == "")
            {
                Section.sectionColor = txtSection.Text;
                context.tablesections.Add(Section);

                context.SaveChanges();
                MessageBox.Show("Section " + txtSection.Text + "Added");
                txtSection.Text = "";
                RefreshList();
            }
            else
            {
                MessageBox.Show("Please Enter a name for your section");
            }
        }

        private void btnDeleteSection_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Section?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                tablesection nu = new tablesection { tableSectionID = myid };
                context.tablesections.Attach(nu); //attaches the user object by the id given to the object above
                context.tablesections.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshList();
        }

        private List<int> fillTableType()
        {
            List<int> tabletypes = new List<int>();

            tabletypes.Add(2);
            tabletypes.Add(4);
            tabletypes.Add(6);


            return tabletypes;
        }

        private void btnAddTable_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(sectionColorComboBox.Text);
            var context = new SeatingManager.SeatingManagerDBEntities();
            var getSectionID = (from u in context.tablesections
                                where u.sectionColor.Equals(sectionColorComboBox.Text)
                                select u).SingleOrDefault();

            tablemap tm = new tablemap();
            tm.tableType = Convert.ToInt16(cboTableType.SelectedItem);
            tm.sectionID = getSectionID.tableSectionID;
            tm.visible = 1;
            tm.numberOfSeats = Convert.ToInt16(cboTableType.SelectedItem);
            tm.tableX = Convert.ToInt32(txtRow.Text);
            tm.tableY = Convert.ToInt32(txtColumn.Text);
            context.tablemaps.Add(tm);
            context.SaveChanges();
            RefreshList();
        }

        private void btnDeleteCust_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                customer nu = new customer { customerID = myid };
                context.customers.Attach(nu); //attaches the user object by the id given to the object above
                context.customers.Remove(nu); //Adds the change to Deletes the user from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshList();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

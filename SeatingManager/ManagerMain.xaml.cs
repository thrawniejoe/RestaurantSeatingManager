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
        }

        public void refreshList()
        {
            usersListView.InvalidateArrange();
            usersListView.UpdateLayout();

        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            refreshList();
        }
    }
}

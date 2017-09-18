using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<TableBC> tableList2;
        private static List<Button> mergedTables;
        private static List<Button> buttonMerge;
        private static List<Button> buttons;
        private static List<customer> reservationList;
        int isButtonClicked = 0;
        int firstLoad = 0;
        int nameToUse = 0;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        public MainWindow()
        {
            tableList2 = new List<TableBC>();
            mergedTables = new List<Button>();
            buttonMerge = new List<Button>();
            buttons = new List<Button>();
            reservationList = new List<customer>();
            InitializeComponent();
            tableList();
            loadSections();
            loadTables();
            putButtonsOnCanvas();
            firstLoad = 1;
        }

        //click a button
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Tag.ToString() != "0")
            {
                int bThickness = Convert.ToInt32(btn.BorderThickness.Top);
                if (bThickness == 0)
                {
                    btn.BorderThickness = new Thickness(2);
                    txtTest.Text = btn.Name.ToString();
                    buttonMerge.Add(btn);
                    isButtonClicked = 1;
                }
            }
        }

        //list of tables from the database
        public void tableList()
             {
                    int x = 0;
                    int y = 0;
                    int numOfSeats = 0;
                    int section = 0;
                    int counter = 1;

                    var context = new SeatingManager.SeatingManagerDBEntities();
                    var getTableData = context.tablemaps.SqlQuery("Select * from tablemaps Order by tableY, tableX").ToList();
                    tablemap tm = new tablemap();
            foreach (tablemap tmp in getTableData)
            {
                x = Convert.ToInt32(tmp.tableX);
                y = Convert.ToInt32(tmp.tableY);
                numOfSeats = Convert.ToInt32(tmp.numberOfSeats);
                section = Convert.ToInt32(tmp.sectionID);
                int xCord = (x - 1) * 110;
                int yCord = (y - 1) * 90;
                TableBC table = new TableBC(counter, xCord, yCord, numOfSeats, 1, section, counter, numOfSeats);
                tableList2.Add(table);
                counter++;
            }
        }

        //merge buttons
        private void btnAddBtn_Click(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            int counter1 = 0;
            int counter2 = 0;
            int tmerge = 0;
            string newList2 = "";
            int tableIndex = 0;
            int mergeOk = 0;

            ImageBrush tableBrush = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\8t.png", UriKind.Relative)
                );

            ImageBrush tableBrush2 = new ImageBrush();
            tableBrush2.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\Merged.png", UriKind.Relative)
                );


            foreach (Button b in buttonMerge)
            {
                while (counter1 < buttonMerge.Count())
                {
                    Button btn = buttonMerge[counter1] as Button;
                    tableIndex = Convert.ToInt32(btn.Tag) - 1;
                    if (tableList2[tableIndex].IsMerged == 2)
                    {
                        MessageBox.Show("This merge includes a table that has already been merged", "Merge Error");
                        mergeOk = 1;
                    }
                    counter1++;
                }
            }

            foreach (Button b in buttonMerge)
            {
                while (counter < buttonMerge.Count())
                {
                    if (counter == 0 && mergeOk == 0)
                    {
                        Button btn = buttonMerge[counter] as Button;
                        buttons[Convert.ToInt32(btn.Tag)-1].Background = tableBrush;
                        buttons[Convert.ToInt32(btn.Tag)-1].BorderThickness = new Thickness(0);
                        buttons[Convert.ToInt32(btn.Tag)-1].FontSize = 14;
                        buttons[Convert.ToInt32(btn.Tag)-1].Foreground = Brushes.Black;
                        tableIndex = Convert.ToInt32(btn.Tag) - 1;
                        tmerge = tableList2[tableIndex].TableNameOrig;
                        tableList2[tableIndex].IsMerged = 2;
                    }
                    else if (counter > 0 && mergeOk == 0)
                    {
                        Button btn = buttonMerge[counter] as Button;
                        buttons[Convert.ToInt32(btn.Tag)-1].Content = "Merged with \n" + buttonMerge[0].Content.ToString();
                        buttons[Convert.ToInt32(btn.Tag)-1].Foreground = Brushes.White;
                        buttons[Convert.ToInt32(btn.Tag)-1].Background = tableBrush2;
                        buttons[Convert.ToInt32(btn.Tag)-1].BorderThickness = new Thickness(0);
                        buttons[Convert.ToInt32(btn.Tag)-1].FontSize = 14;
                        buttons[Convert.ToInt32(btn.Tag) - 1].HorizontalContentAlignment = HorizontalAlignment.Center;
                        tableIndex = Convert.ToInt32(btn.Tag) - 1;
                        tableList2[tableIndex].TableNameOrig = tmerge;
                        tableList2[tableIndex].IsMerged = 0;
                        btn.Tag = 0;
                    }
                    counter++;
                }
            }

            if (mergeOk == 1) { 
                foreach (Button bb in buttonMerge)
                    {
                     while (counter2 < buttonMerge.Count())
                        {
                            Button btn2 = buttonMerge[counter2] as Button;
                            tableIndex = Convert.ToInt32(btn2.Tag) - 1;
                            btn2.BorderThickness = new Thickness(0);
                            counter2++;
                        }
                        counter2++;
                    }
            }

            buttonMerge.Clear();
            isButtonClicked = 0;

            foreach (TableBC t in tableList2)
            {
                string newList = t.TableName.ToString() + ", " + t.TableX.ToString() + ", " + t.TableY.ToString() + ", " + t.TableSeats.ToString() + ", " + "\t" + t.IsMerged.ToString() + ", " + t.Section.ToString() + ", " + t.TableNameOrig.ToString() + ", " + t.TableSeatsOrig.ToString() + "\n";
                newList2 += newList;
                txtTest.Text = newList2;

            }
        }

        //load buttons
        private void loadTables()
        {
            string name = "";
            string bn = "";
            string bc = "t";
            int btnName = 0;
            foreach (TableBC t in tableList2)
            {
                bn = t.TableSeats.ToString();
                name = t.TableName.ToString();
                btnName = t.TableName;

                ImageBrush tableBrush = new ImageBrush();
                tableBrush.ImageSource =
                    new BitmapImage(
                        new Uri(@"..\\..\\images\" + bn + bc + ".png", UriKind.Relative)
                    );

                Button newBtn = new Button();
                newBtn.Content = name;
                newBtn.Name = "Button" + btnName;
                newBtn.Tag = t.TableName;
                newBtn.Width = 110;
                newBtn.Height = 90;
                newBtn.Margin = new Thickness(0);
                newBtn.BorderThickness = new Thickness(0);
                newBtn.Background = tableBrush;
                newBtn.Foreground = Brushes.Black;
                newBtn.FontSize = 15;
                newBtn.Click += btn1_Click;
                buttons.Add(newBtn);
            }
        }

        private void putButtonsOnCanvas()
        {
            if (firstLoad == 1)
            {
                tables.Children.RemoveRange(0, tableList2.Count());
            }
            int count = 0;
            foreach (Button newBtn in buttons)
            {
                TableBC tbc = tableList2[count] as TableBC;
                Button addBtn = newBtn as Button;
                int y = Convert.ToInt32(tbc.TableX);
                int x = Convert.ToInt32(tbc.TableY);
                addBtn.Content = "Table " + newBtn.Tag;
                addBtn.Name = newBtn.Name;
                addBtn.Tag = newBtn.Tag;
                addBtn.Width = 110;
                addBtn.Height = 90;
                addBtn.FontSize = 15;
                addBtn.Margin = new Thickness(0);
                addBtn.BorderThickness = new Thickness(0);
                addBtn.Background = newBtn.Background;
                addBtn.Foreground = Brushes.Black;
                addBtn.Click += btn1_Click;
                tables.Children.Add(addBtn); //adds button to the Canvas
                Canvas.SetTop(addBtn, x);
                Canvas.SetLeft(addBtn, y);
                count++;
            }
        }

        //load section colors
        private void loadSections()
        {
            int section = 0;
            foreach (TableBC t in tableList2)
            {
                section = t.Section;

                Label newLbl = new Label();

                if (section == 1)
                {
                    newLbl.Background = Brushes.LightGoldenrodYellow;
                }
                else if (section == 2)
                {
                    newLbl.Background = Brushes.LightCoral;
                }
                else if (section == 3)
                {
                    newLbl.Background = Brushes.LightSteelBlue;
                }
                else if (section == 4)
                {
                    newLbl.Background = Brushes.LightSeaGreen;
                }
                else if (section == 5)
                {
                    newLbl.Background = Brushes.LightCyan;
                }

                newLbl.Width = 110;
                newLbl.Height = 90;
                tables2.Children.Add(newLbl); //adds label to the Canvas
                Canvas.SetTop(newLbl, t.TableY);
                Canvas.SetLeft(newLbl, t.TableX);
            }
        }

        //adds a customer 
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            //opens add customer screen and centers it in the middle of the main screen
            AddCustomer addCust = new AddCustomer();
            addCust.Owner = this;
            //addCust.Owner = Application.Current.MainWindow;
            addCust.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCust.ShowDialog();
            RefreshList();
        }

        //clears the table
        private void btnClearTable_Click(object sender, RoutedEventArgs e)
        {
            string newList2 = "";
            int mergedTableNumber = 0;
            int unMergeNumber = 0;

            if (isButtonClicked == 1)
            {
                foreach (Button btn in buttonMerge)
                {
                    mergedTableNumber = Convert.ToInt32(btn.Tag) - 1;
                    unMergeNumber = Convert.ToInt32(tableList2[mergedTableNumber].TableName);

                    foreach (TableBC tbc in tableList2)
                    {
                        if (tbc.TableNameOrig == unMergeNumber)
                        {
                            Button newBtn = new Button();
                            newBtn.Tag = tbc.TableName.ToString();
                            mergedTables.Add(newBtn);
                            tbc.IsMerged = 1;
                            tbc.TableNameOrig = tbc.TableName;
                        }
                    }
                    foreach (Button bb in mergedTables)
                    {
                        foreach (TableBC tb in tableList2)
                        {
                            if ((string)bb.Tag == tb.TableName.ToString())
                            {
                                ImageBrush tableBrush = new ImageBrush();
                                tableBrush.ImageSource =
                                new BitmapImage(
                                    new Uri(@"..\\..\\images\" + tb.TableSeats + "t.png", UriKind.Relative));
                                Button btn2 = bb as Button;
                                buttons[Convert.ToInt32(btn2.Tag) - 1].Content = "Table " + tb.TableName.ToString();
                                buttons[Convert.ToInt32(btn2.Tag) - 1].Foreground = Brushes.Black;
                                buttons[Convert.ToInt32(btn2.Tag) - 1].Background = tableBrush;
                                buttons[Convert.ToInt32(btn2.Tag) - 1].BorderThickness = new Thickness(0);
                                buttons[Convert.ToInt32(btn2.Tag) - 1].FontSize = 15;
                                buttons[Convert.ToInt32(btn2.Tag) - 1].Tag = tb.TableName;
                            }
                        }
                    }
                }  
            }
            
            foreach (TableBC t in tableList2)
            {
                string newList = t.TableName.ToString() + ", " + t.TableX.ToString() + ", " + t.TableY.ToString() + ", " + t.TableSeats.ToString() + ", " + "\t" + t.IsMerged.ToString() + ", " + t.Section.ToString() + ", " + t.TableNameOrig.ToString() + ", " + t.TableSeatsOrig.ToString() + "\n";
                newList2 += newList;
                txtTest.Text = newList2;

            }

            buttonMerge.Clear();
            mergedTables.Clear();
        }

        //assigns the customer to a table
        private void btnAssignCustomer_Click(object sender, RoutedEventArgs e)
        {
            string customer = "";
            if (nameToUse == 0)
            {
                customer = "Matt";
                nameToUse++;
            }
            else if (nameToUse == 1)
            {
                customer = "Joe";
                nameToUse++;
            }
            else if (nameToUse == 2)
            {
                customer = "John";
                nameToUse++;
            }
            else if (nameToUse == 3)
            {
                customer = "Taylor";
                nameToUse++;
            }
            else if (nameToUse == 4)
            {
                customer = "Mark";
                nameToUse = 0;
            }

            DateTime dt = DateTime.Now;
            string time = dt.ToString("h:mm");
            ImageBrush tableBrush = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\taken.png", UriKind.Relative)
                );

            foreach (Button b in buttonMerge)
            {
                b.Content = b.Content + "\n" + customer + "\nSeated: " + time;
                b.FontSize = 14;
                b.BorderThickness = new Thickness(0);
                b.Background = tableBrush;
            }
            buttonMerge.Clear();
            isButtonClicked = 0;
        }


        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void tab2_Loaded(object sender, RoutedEventArgs e)
        {
            
        }


        public void RefreshList()
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            //Refresh Server GridView
            serversDataGrid.ItemsSource = null;
            var getServ = (from su in context.users
                           where su.title == "Server" && su.isOnDuty == 1
                           select su);
            serversDataGrid.ItemsSource = getServ.ToList();


            //Refresh Customer GridView
            customersDataGrid.ItemsSource = null;
            var getCust = (from su in context.customers
                where su.reservation == 0
                orderby su.timeIn  
                select su);
            customersDataGrid.ItemsSource = getCust.ToList();

            //Refresh Reservation GridView
            reservationDataGrid.ItemsSource = null;
            var getResrv = (from su in context.customers
                            where su.reservation == 1
                            orderby su.timeIn
                            select su);
            reservationDataGrid.ItemsSource = getResrv.ToList();

            //Refresh Server List
            usersDataGrid.ItemsSource = null;
            var getServers = (from su in context.users
                              where su.title == "Server"
                              select su);
            usersDataGrid.ItemsSource = getServers.ToList();
        }


        private void btnbtnSetOnDuty_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button cBut = sender as Button;
            int id = Convert.ToInt16(cBut.Tag);
            var result = context.users.SingleOrDefault(b => b.userID == id);
            if (result != null)
            {
                if (result.isOnDuty == 1)
                {
                    result.isOnDuty = 0;
                }
                else
                {
                    result.isOnDuty = 1;
                }
                context.SaveChanges();
                RefreshList();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshList();
            // Load data into the table Reservation. You can modify this code as needed.
            //SeatingManager.SeatingManagerDBDataSetTableAdapters.ReservationTableAdapter seatingManagerDBDataSetReservationTableAdapter = new SeatingManager.SeatingManagerDBDataSetTableAdapters.ReservationTableAdapter();
            //seatingManagerDBDataSetReservationTableAdapter.Fill(seatingManagerDBDataSet.Reservation);
            //System.Windows.Data.CollectionViewSource reservationViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("reservationViewSource")));
            //reservationViewSource.View.MoveCurrentToFirst();
        }

        private void btnDeleteServer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnDeleteCust_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this customer?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                var nu = new customer { customerID = myid };
                context.customers.Attach(nu); //attaches the user object by the id given to the object above
                context.customers.Remove(nu); //Adds the change to Deletes the customer from the database
                context.SaveChanges();  //Saves changes to the database
            }
            RefreshList();
        }

        //Moves customer from reservation list to customer list.
        private void btnCheckInReservation_Click(object sender, RoutedEventArgs e)
        {
            var context = new SeatingManager.SeatingManagerDBEntities();
            Button b = sender as Button;
            int myid = Convert.ToInt16(b.Tag);

            MessageBox.Show("Customer checked in! Vibrating flashy coaster thing activated!");

            var cust = context.customers.SingleOrDefault(c => c.customerID == myid);
            if (cust != null)
            {
                cust.reservation = 0;
                cust.timeIn = DateTime.Today;
                context.SaveChanges();
                RefreshList();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnCreateReservation_Click(object sender, RoutedEventArgs e)
        {
            AddReservation addRes = new AddReservation();
            addRes.EngageRefreshList += RefreshList;
            addRes.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.CurrentUserRole = -1; //saves the user role to the applcation settings file
            Properties.Settings.Default.Save();
            SplashLogin sL = new SplashLogin();
            sL.Show();
        }
    }
}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SeatingManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<TableBC> tableList2 = new List<TableBC>();
        private static List<string> tableMerge = new List<string>();
        private static List<Button> buttonMerge = new List<Button>();
        int isButtonClicked = 0;

        public MainWindow()
        {
            InitializeComponent();
            tableList();
            loadSections();
            loadTables();
        }

        //click a button
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            if (btn.Tag.ToString() != "0")
            {
            btn.BorderThickness = new Thickness(2);
            txtTest.Text = btn.Name.ToString();
            string buttonToMerge = btn.Name.ToString();
            tableMerge.Add(buttonToMerge);
            buttonMerge.Add(btn);
            isButtonClicked = 1;
            }
        }

        //list of tables from the database
        public void tableList()
        {
            tableList2 = TableDA.GetTables();
        }

        //merge buttons
        private void btnAddBtn_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            int tmerge = 0;
            string newList2 = "";
            int tableIndex = 0;

            ImageBrush tableBrush = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\8t.png", UriKind.Relative)
                );

            ImageBrush tableBrush2 = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\Merged.png", UriKind.Relative)
                );


            foreach (Button b in buttonMerge)
            {
                while (count < tableMerge.Count())
                {
                    Button btn = buttonMerge[count] as Button;
                    if (count == 0)
                    {
                        btn.Background = tableBrush2;
                        btn.BorderThickness = new Thickness(0);
                        btn.FontSize = 18;
                        btn.Content = "Table " + buttonMerge[0].Content;
                        btn.Foreground = Brushes.Black;
                        tableIndex = Convert.ToInt32(btn.Tag) - 1;
                        tmerge = tableList2[tableIndex].TableNameOrig;
                        tableList2[tableIndex].IsMerged = 2;
                    }
                    else
                    {
                        btn.Content = "Merged with \n" + buttonMerge[0].Content;
                        btn.Foreground = Brushes.White;
                        btn.Background = tableBrush;
                        btn.BorderThickness = new Thickness(0);
                        btn.FontSize = 15;
                        tableIndex = Convert.ToInt32(btn.Tag) - 1;
                        tableList2[tableIndex].TableNameOrig = tmerge;
                        tableList2[tableIndex].IsMerged = 0;
                        btn.Tag = 0;
                    }
                    count++;
                }
            }
            //txtTest.Text = tmerge;
            tableMerge.Clear();
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
            //string isVisible = "";
            string bn = "";
            string bc = "t";
            int btnName = 0;
            foreach (TableBC t in tableList2)
            {
                bn = t.TableSeats.ToString();
                name = t.TableName.ToString();
                //isVisible = t.IsMerged.ToString();
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
                newBtn.Click += btn1_Click;

                /*if (isVisible == "1")
                {
                    newBtn.Visibility = Visibility.Visible;
                }
                else
                {
                    newBtn.Visibility = Visibility.Hidden;
                }*/

                tables.Children.Add(newBtn); //adds button to the Canvas
                Canvas.SetTop(newBtn, t.TableY);
                Canvas.SetLeft(newBtn, t.TableX);
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
        }

        //clears the table
        private void btnClearTable_Click(object sender, RoutedEventArgs e)
        {
            if (isButtonClicked == 1)
            {
                tables.Children.Clear();
                tableMerge.Clear();
                buttonMerge.Clear();
                loadTables();
                loadSections();
            }
        }

        //assigns the customer to a table
        private void btnAssignCustomer_Click(object sender, RoutedEventArgs e)
        {
            ImageBrush tableBrush = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\taken.png", UriKind.Relative)
                );

            foreach (Button b in buttonMerge)
            {
                b.Content = "Customer";
                b.BorderThickness = new Thickness(0);
                b.Background = tableBrush;
            }
            tableMerge.Clear();
            buttonMerge.Clear();
            isButtonClicked = 0;
        }





        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

            //// Define the Columns.test
            //ColumnDefinition myColDef1 = new ColumnDefinition();
            //myColDef1.Width = new GridLength(1, GridUnitType.Auto);
            //ColumnDefinition myColDef2 = new ColumnDefinition();
            //myColDef2.Width = new GridLength(1, GridUnitType.Auto);
            //ColumnDefinition myColDef3 = new ColumnDefinition();
            //myColDef3.Width = new GridLength(1, GridUnitType.Auto);

            //// Add the ColumnDefinitions to the Grid.
            //myGrid.ColumnDefinitions.Add(myColDef1);
            //myGrid.ColumnDefinitions.Add(myColDef2);
            //myGrid.ColumnDefinitions.Add(myColDef3);

            //int totalButtons = 11;
            //int btnCounter = 0;
            //int curT = totalButtons;

            //StackPanel myStackPanel = new StackPanel();
            //myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            //myStackPanel.VerticalAlignment = VerticalAlignment.Top;
            //Grid.SetColumn(myStackPanel, 0);
            //Grid.SetRow(myStackPanel, 0);

            //StackPanel myStackPanel1 = new StackPanel();
            //myStackPanel1.HorizontalAlignment = HorizontalAlignment.Stretch;
            //myStackPanel1.VerticalAlignment = VerticalAlignment.Top;
            //myStackPanel1.Orientation = Orientation.Vertical;
            //Grid.SetColumn(myStackPanel1, 1);
            //Grid.SetRow(myStackPanel1, 0);

            //StackPanel myStackPanel2 = new StackPanel();
            //myStackPanel2.HorizontalAlignment = HorizontalAlignment.Left;
            //myStackPanel2.VerticalAlignment = VerticalAlignment.Top;
            //Grid.SetColumn(myStackPanel2, 2);
            //Grid.SetRow(myStackPanel2, 0);

            //int LoopCount = 0;
            //int mainLoopCount = 0;
            //do
            //{


            //    //checks if total number of buttons needed is greater than 4
            //    if (curT > 4)
            //    {
            //        LoopCount = 4;
            //    }
            //    else
            //    {
            //        LoopCount = curT;
            //    }

            //    //MessageBox.Show(Convert.ToString(mainLoopCount) + " |  " + Convert.ToString(curT));
            //    //Generates buttons
            //    for (int i = 0; i < LoopCount; i++)
            //    {


            //        btnCounter++;
            //        System.Windows.Controls.Button newBtn = new Button();
            //        newBtn.Content = btnCounter;
            //        newBtn.Name = "Button" + btnCounter;
            //        newBtn.Width = 75;
            //        newBtn.Height = 50;
            //        newBtn.Margin = new Thickness(5);
            //        newBtn.Click += btn1_Click;


            //        switch (mainLoopCount)
            //        {
            //            case 0:
            //                myStackPanel.Children.Add(newBtn); //adds button to the StackPanel
            //                break;
            //            case 1:
            //                 myStackPanel1.Children.Add(newBtn); //adds button to the StackPanel
            //                break;
            //            case 2:
            //                myStackPanel2.Children.Add(newBtn); //adds button to the StackPanel
            //                break;
            //        }
            //    }

            //    curT = curT - LoopCount; //subtracks from total buttons
            //    mainLoopCount++;
            //}
            //while (curT > 0);


            //myGrid.Children.Add(myStackPanel);
            //myGrid.Children.Add(myStackPanel1);
            //myGrid.Children.Add(myStackPanel2);

        }

        private void tab2_Loaded(object sender, RoutedEventArgs e)
        {
            //
            //THIS ISNT WORKING CORRECTLY, TOO LAZY TO FIX
            //
            //ColumnDefinition myColDef1 = new ColumnDefinition();
            //myColDef1.Width = new GridLength(1, GridUnitType.Auto);

            //WrapPanel myStackPanel = new WrapPanel();
            //myStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            //myStackPanel.VerticalAlignment = VerticalAlignment.Top;
            //Grid.SetColumn(myStackPanel, 0);
            //Grid.SetRow(myStackPanel, 0);


            //int LoopCount = 0;
            //int mainLoopCount = 0;
            //int totalButtons = 11;
            //int btnCounter = 0;
            //int curT = totalButtons;

            //do
            //{
            //    //MessageBox.Show(Convert.ToString(mainLoopCount) + " |  " + Convert.ToString(curT));
            //    //Generates buttons
            //    for (int i = 0; i < LoopCount; i++)
            //    {


            //        btnCounter++;
            //        System.Windows.Controls.Button newBtn = new Button();
            //        newBtn.Content = btnCounter;
            //        newBtn.Name = "Button" + btnCounter;
            //        newBtn.Width = 75;
            //        newBtn.Height = 50;
            //        newBtn.Margin = new Thickness(5);
            //        //newBtn.Click += btn1_Click;

            //        myStackPanel.Children.Add(newBtn); //adds button to the StackPanel
            //    }

            //    curT = curT - LoopCount; //subtracks from total buttons
            //    //mainLoopCount++;
            //}
            //while (curT > 0);
            //myGrid.Children.Add(myStackPanel);

        }
    }
}

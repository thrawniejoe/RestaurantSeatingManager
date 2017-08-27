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
        private int btnCounter = 0;

        public MainWindow()
        {
            InitializeComponent();



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

        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            
            MessageBox.Show("Button Click: " + btn.Content);
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

        private void btnAddBtn_Click(object sender, RoutedEventArgs e)
        {
            //Click and adds a button
            System.Windows.Controls.Button newBtn = new Button();
            newBtn.Content = btnCounter;
            newBtn.Name = "Button" + btnCounter;
            newBtn.Width = 75;
            newBtn.Height = 50;
            newBtn.Margin = new Thickness(5);
            newBtn.Click += btn1_Click;
            btnCounter++;
            myWrapPan.Children.Add(newBtn); //adds button to the WrapPannel
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            //opens add customer screen and centers it in the middle of the main screen
            AddCustomer addCust = new AddCustomer();
            addCust.Owner = this;
            //addCust.Owner = Application.Current.MainWindow;
            addCust.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            addCust.ShowDialog();
        }
    }
}

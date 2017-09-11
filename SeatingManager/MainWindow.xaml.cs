﻿using System;
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
        private static List<Button> mergedTables = new List<Button>();
        private static List<Button> buttonMerge = new List<Button>();
        private static List<Button> buttons = new List<Button>();
        int isButtonClicked = 0;
        int firstLoad = 0;

        public MainWindow()
        {
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
            tableList2 = TableDA.GetTables();
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
                        buttons[Convert.ToInt32(btn.Tag)-1].FontSize = 15;
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
                        buttons[Convert.ToInt32(btn.Tag)-1].FontSize = 15;
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
                newBtn.Width = t.TableX;
                newBtn.Height = t.TableY;
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
            ImageBrush tableBrush = new ImageBrush();
            tableBrush.ImageSource =
                new BitmapImage(
                    new Uri(@"..\\..\\images\taken.png", UriKind.Relative)
                );

            foreach (Button b in buttonMerge)
            {
                b.Content = b.Content + "\nCustomer";
                b.FontSize = 15;
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
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SeatingManager
{
    class TableDA
    {

        public static string connstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mark\Source\Repos\RestaurantSeatingManager\SeatingManager\SeatingManagerDB.mdf;Integrated Security=True;Connect Timeout=30";
        static int x = 0;
        static int y = 0;
        static int numOfSeats = 0;
        static int section = 0;
        static int counter = 1;

        //makes the list of tables for the restaurant
        public static List<TableBC> GetTables()
        {
            // create the array list for tables
            List<TableBC> tableList = new List<TableBC>();

            SqlDataReader reader = null;
            SqlConnection conn = new SqlConnection(connstring);
            string selectSatement = "Select * from tablemaps Order by tableY, tableX";
            SqlCommand selectCommand = new SqlCommand(selectSatement, conn);

            conn.Open();

            reader = selectCommand.ExecuteReader();

            while (reader.Read())
            {
                x = Convert.ToInt32(reader["tableX"]);
                y = Convert.ToInt32(reader["tableY"]);
                numOfSeats = Convert.ToInt32(reader["numberOfSeats"]);
                section = Convert.ToInt32(reader["sectionID"]);
                int xCord = x * 110;
                int yCord = y * 90;
                TableBC table = new TableBC(counter, xCord, yCord, numOfSeats, 1, section, counter, numOfSeats);
                tableList.Add(table);
                counter++;
            }

            return tableList;

            //adds the tiles to the list
            /*TableBC table0 = new TableBC(1, 0, 0, 6, 1, 1, 1, 6);
            tableList.Add(table0);
            TableBC table2 = new TableBC(2, 0, 90, 6, 1, 1, 2, 6);
            tableList.Add(table2);
            TableBC table3 = new TableBC(3, 330, 90, 2, 1, 2, 3, 2);
            tableList.Add(table3);
            TableBC table5 = new TableBC(4, 440, 90, 4, 1, 2, 4, 4);
            tableList.Add(table5);
            TableBC table6 = new TableBC(5, 550, 90, 4, 1, 2, 5, 4);
            tableList.Add(table6);
            TableBC table8 = new TableBC(6, 0, 180, 6, 1, 1, 6, 6);
            tableList.Add(table8);
            TableBC table9 = new TableBC(7, 110, 180, 4, 1, 1, 7, 4);
            tableList.Add(table9);
            TableBC table10 = new TableBC(8, 220, 180, 4, 1, 1, 8, 4);
            tableList.Add(table10);
            TableBC table11 = new TableBC(9, 330, 180, 2, 1, 2, 9, 2);
            tableList.Add(table11);
            TableBC table12 = new TableBC(10, 440, 180, 4, 1, 2, 10, 4);
            tableList.Add(table12);
            TableBC table13 = new TableBC(11, 550, 180, 4, 1, 2, 11, 4);
            tableList.Add(table13);
            TableBC table14 = new TableBC(12, 660, 180, 2, 1, 2, 12, 2);
            tableList.Add(table14);
            TableBC table16 = new TableBC(13, 0, 270, 6, 1, 1, 13, 6);
            tableList.Add(table16);
            TableBC table17 = new TableBC(14, 110, 270, 4, 1, 1, 14, 4);
            tableList.Add(table17);
            TableBC table18 = new TableBC(15, 220, 270, 4, 1, 1, 15, 4);
            tableList.Add(table18);
            TableBC table19 = new TableBC(16, 330, 270, 2, 1, 3, 16, 2);
            tableList.Add(table19);
            TableBC table20 = new TableBC(17, 440, 270, 4, 1, 3, 17, 4);
            tableList.Add(table20);
            TableBC table21 = new TableBC(18, 550, 270, 4, 1, 3, 18, 4);
            tableList.Add(table21);
            TableBC table22 = new TableBC(19, 660, 270, 2, 1, 3, 19, 2);
            tableList.Add(table22);
            TableBC table24 = new TableBC(20, 0, 360, 6, 1, 4, 20, 6);
            tableList.Add(table24);
            TableBC table25 = new TableBC(21, 110, 360, 4, 1, 4, 21, 4);
            tableList.Add(table25);
            TableBC table26 = new TableBC(22, 220, 360, 4, 1, 4, 22, 4);
            tableList.Add(table26);
            TableBC table27 = new TableBC(23, 330, 360, 2, 1, 3, 23, 2);
            tableList.Add(table27);
            TableBC table28 = new TableBC(24, 440, 360, 4, 1, 3, 24, 4);
            tableList.Add(table28);
            TableBC table29 = new TableBC(25, 550, 360, 4, 1, 3, 25, 4);
            tableList.Add(table29);
            TableBC table30 = new TableBC(26, 660, 360, 2, 1, 3, 26, 2);
            tableList.Add(table30);
            TableBC table32 = new TableBC(27, 0, 450, 6, 1, 4, 27, 6);
            tableList.Add(table32);
            TableBC table33 = new TableBC(28, 110, 450, 4, 1, 4, 28, 4);
            tableList.Add(table33);
            TableBC table34 = new TableBC(29, 220, 450, 4, 1, 5, 29, 4);
            tableList.Add(table34);
            TableBC table35 = new TableBC(30, 330, 450, 2, 1, 5, 30, 2);
            tableList.Add(table35);
            TableBC table36 = new TableBC(31, 440, 450, 4, 1, 5, 31, 4);
            tableList.Add(table36);
            TableBC table37 = new TableBC(32, 550, 450, 4, 1, 5, 32, 4);
            tableList.Add(table37);
            TableBC table39 = new TableBC(33, 0, 540, 6, 1, 4, 33, 6);
            tableList.Add(table39);
            TableBC table40 = new TableBC(34, 110, 540, 4, 1, 4, 34, 4);
            tableList.Add(table40);
            TableBC table41 = new TableBC(35, 220, 540, 4, 1, 5, 35, 4);
            tableList.Add(table41);
            TableBC table42 = new TableBC(36, 330, 540, 2, 1, 5, 36, 2);
            tableList.Add(table42);
            TableBC table43 = new TableBC(37, 440, 540, 4, 1, 5, 37, 4);
            tableList.Add(table43);*/


        }
    }
}

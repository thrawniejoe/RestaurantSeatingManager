using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatingManager
{
    class TableBC
    {
        //variables
        private int tableName;
        private int tableX;
        private int tableY;
        private int tableSeats;
        private int isMerged;
        private int section;
        private int tableNameOrig;
        private int tableSeatsOrig;

        //constructor that sets all properties to system defaults
        public TableBC() { }

        //constructor that has all the properties
        public TableBC(int tableName, int tableX, int tableY, int tableSeats, int isMerged, int section, int tableNameOrig, int tableSeatsOrig)
        {
            this.TableName = tableName;
            this.TableX = tableX;
            this.TableY = tableY;
            this.TableSeats = tableSeats;
            this.IsMerged = isMerged;
            this.Section = section;
            this.TableNameOrig = tableNameOrig;
            this.TableSeatsOrig = tableSeatsOrig;
        }

        //get and set methods
        public int TableName
        {
            get
            {
                return tableName;
            }
            set
            {
                tableName = value;
            }
        }

        public int TableX
        {
            get
            {
                return tableX;
            }
            set
            {
                tableX = value;
            }
        }

        public int TableY
        {
            get
            {
                return tableY;
            }
            set
            {
                tableY = value;
            }
        }

        public int TableSeats
        {
            get
            {
                return tableSeats;
            }
            set
            {
                tableSeats = value;
            }
        }

        public int IsMerged
        {
            get
            {
                return isMerged;
            }
            set
            {
                isMerged = value;
            }
        }

        public int Section
        {
            get
            {
                return section;
            }
            set
            {
                section = value;
            }
        }

        public int TableSeatsOrig
        {
            get
            {
                return tableSeatsOrig;
            }
            set
            {
                tableSeatsOrig = value;
            }
        }

        public int TableNameOrig
        {
            get
            {
                return tableNameOrig;
            }
            set
            {
                tableNameOrig = value;
            }
        }
    }

}

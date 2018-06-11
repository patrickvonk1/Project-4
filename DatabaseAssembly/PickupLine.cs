using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAssembly
{
    public class PickupLine
    {
        //ID For Database
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Text { get; set; }

        public bool IsFavourited { get; set; }

        public PickupLineType PickupLineType { get; set; }
    }

    public enum PickupLineType
    {
        Straat, 
        Club,
        OV,
        Sportschool,
    }
}

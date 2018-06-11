using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAssembly
{
    public class MotivationLine
    {
        //ID For Database
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Text { get; set; }

        public bool IsFavourited { get; set; }

        public MotivationLineType MotivationLineType { get; set; }
    }

    public enum MotivationLineType
    {
        School,
        Sportschool
    }
}

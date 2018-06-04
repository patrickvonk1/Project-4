using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAssembly
{
    public class JokeLine
    {
        //ID For Database
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string Text { get; set; }

        public bool IsFavourited { get; set; }

        public JokeLineType JokeLineType { get; set; }
    }

    public enum JokeLineType
    {
        Dad,
        Pun,
        Corny
    }
}

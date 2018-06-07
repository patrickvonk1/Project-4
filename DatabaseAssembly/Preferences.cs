using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseAssembly
{
    public class Preferences
    {
        //ID For Database
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public bool IsProfanityFilterEnabled { get;set; }

        public AttractedGender AttractedGender { get; set; }
        public AppTheme AppTheme { get; set; }
    }

    public enum AttractedGender
    {
        Female,
        Male,
        Both
    }

    public enum AppTheme
    {
        Dark,
        Light
    }
}

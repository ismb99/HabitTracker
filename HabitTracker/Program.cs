using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;

namespace HabitTracker
{
    public class Program

    {
        static void Main(string[] args)
        {
            Database db = new Database();
            db.DatabaseProvider();
            ProgramHelpers.MainMenu();
        }
    }

}

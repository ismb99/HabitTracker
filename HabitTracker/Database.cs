using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class Database
    {
        private const string connectionString = @"URI=file:C:\Users\Ismail\Desktop\coding_Tracker.db";

        public void DatabaseProvider()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = @"CREATE TABLE IF NOT EXISTS  drinking_cola(id INTEGER PRIMARY KEY AUTOINCREMENT,Date TEXT, Quantity INTEGER)";
                    tableCmd.ExecuteNonQuery();
                }

            };
        }


        // Show all records in the database
        public static void DisplayAll()
        {
            Console.Clear();
            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = @"Select * From drinking_cola";

                    var cmd = new SQLiteCommand(tableCmd.CommandText, connection);
                    var reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetInt32(2)}\n");
                            Console.WriteLine("-------------------------------------------------------------------");
                        }
                    }

                    else
                    {
                        Console.WriteLine("Table is empty");
                    }
                }
            };
        }

        public static void Instert()
        {
            Console.Clear();
            string date = UserInputs.GetDate();
            var quantity = UserInputs.GetUserInputQuantity();

            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    connection.Open();
                    command.CommandText = @"Select Date From drinking_cola";
                    var cmd = new SQLiteCommand(command.CommandText, connection);
                    var reader = cmd.ExecuteReader();

                    while (!reader.Read())
                    {
                        if (reader.ToString() != date)
                        {
                            using (var tableCmd = connection.CreateCommand())
                            {
                                tableCmd.CommandText = 
                                    $"INSERT INTO drinking_cola(Date, Quantity) " +
                                    $"VALUES('{date}', {quantity})";
                                tableCmd.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            Console.WriteLine("Date exsist");
                            ProgramHelpers.MainMenu();
                        }
                    }

                }
            };

        }

        public static void Delete()
        {
            Console.Clear();
            DisplayAll();

            Console.WriteLine("Choose the id you want to remove");
            string idInput = Console.ReadLine();

            if (idInput == "0") ProgramHelpers.MainMenu();

            int numInput = int.Parse(idInput);

            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"DELETE FROM drinking_cola WHERE id= {numInput}";
                    tableCmd.ExecuteNonQuery();
                }

            };
        }

        public static void DeleteAll()
        {
            Console.Clear();
            DisplayAll();

            Console.WriteLine("Do you want to delete all record?, Y/N");

            string input = Console.ReadLine();

            switch (input)
            {
                case "n": ProgramHelpers.MainMenu(); break;
                case "y":
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        using (var tableCmd = connection.CreateCommand())
                        {
                            connection.Open();
                            tableCmd.CommandText = $"DELETE FROM drinking_cola";
                            tableCmd.ExecuteNonQuery();
                            Console.WriteLine("All records are deleted");
                            Console.WriteLine("-----------------------------------------");
                        }

                    };
                    break;

                default:
                    Console.WriteLine("Wrong choice, returning to Main Menu");
                    ProgramHelpers.MainMenu(); break;
            }
        }

        public static void Update()
        {
            DisplayAll();

            Console.WriteLine("Choose the id you want to update, press 0 if you want to go back to main menu");

            string id = Console.ReadLine();
            if (id == "0") ProgramHelpers.MainMenu();
            int numId = int.Parse(id);

            using (var connection = new SQLiteConnection(connectionString))
            {
                using (var tableCmd = connection.CreateCommand())
                {
                    connection.Open();
                    tableCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM drinking_cola WHERE Id = {numId})";

                    int checkQuery = Convert.ToInt32(tableCmd.ExecuteScalar());

                    if (checkQuery == 0)
                    {
                        Console.WriteLine($"Record with id {numId} dosent exist");
                        Update(); // ??
                    }

                    string date = UserInputs.GetDate();
                    var quantity = UserInputs.GetUserInputQuantity();

                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = $"UPDATE drinking_cola SET date = '{date}', quantity = {quantity} WHERE Id = {numId}";
                        command.ExecuteNonQuery();
                    }

                }

            };
        }

    }

}

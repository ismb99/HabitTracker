using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;

namespace HabitTracker
{
    public class Program

    {
        static string connectionString = @"URI=file:C:\Users\Ismail\Desktop\habit1.db";

        static void Main(string[] args)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"Create table if not EXISTS  drinking_cola(id integer primary key autoincrement,Date Text, Quantity integer)";
                command.ExecuteNonQuery();
                connection.Close();
            };

            MainMenu();
        }

        static void MainMenu()
        {
            bool closeApp = false;
            while (closeApp == false)
            {
                Console.WriteLine("Welcome to the habit tracker, make a choice\n\n");
                Console.WriteLine("0 to quit");
                Console.WriteLine("1 show all records");
                Console.WriteLine("2 insert");
                Console.WriteLine("3 Delete");
                Console.WriteLine("4 Update");
                Console.WriteLine("---------------------------\n\n");

                string Userinput = Console.ReadLine();

                switch (Userinput)
                {
                    case "0":
                        Console.WriteLine("Goodbye");
                        closeApp = true;
                        break;

                    case "1":
                        DisplayAll();
                        break;

                    case "2":
                        Instert();
                        break;

                    case "3":
                        Delete();
                        break;

                    case "4":
                        Update();
                        break;

                    default:
                        Console.WriteLine("wrong choice, you can only choose between 0-4\n");
                        break;
                }
            }
        }

        // Show all records in the database
        private static void DisplayAll()
        {
            Console.Clear();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"Select * From drinking_cola";

                SQLiteCommand cmd = new SQLiteCommand(command.CommandText, connection);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetInt32(2)}\n");
                }
                connection.Close();
            };
        }

        // Add records into the database
        private static void Instert()
        {
            Console.Clear();
            string date = GetDate();
            var quantity = GetUserInputQuantity();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO drinking_cola(Date, Quantity) VALUES('{date}', {quantity})";
                command.ExecuteNonQuery();
                connection.Close();
            };
        }

        // Delete record from db
        // Delete based on id
        private static void Delete()
        {
            Console.Clear();
            DisplayAll();
            Console.WriteLine("Choose the id you want to remove");
            string idInput = Console.ReadLine();
            int numInput = int.Parse(idInput);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM drinking_cola WHERE id= {numInput}";

                command.ExecuteNonQuery();
                connection.Close();

                if (numInput == 0)
                {
                    MainMenu();
                }
            };
        }

        // Uppdate the records
        private static void Update()
        {
            DisplayAll();

            Console.WriteLine("Choose the id you want to update");

            string id = Console.ReadLine();
            int numId = int.Parse(id);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();


                var checkCmd = connection.CreateCommand();
                checkCmd.CommandText = $"SELECT EXISTS(SELECT 1 FROM drinking_cola WHERE Id = {numId})";
                int checkQuery = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (checkQuery == 0)
                {
                    Console.WriteLine($"Record with id {numId} dosent exist");
                    connection.Close();
                    Update(); // ??
                }

                string date = GetDate();
                var quantity = GetUserInputQuantity();

                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE drinking_cola SET date = '{date}', quantity = {quantity} WHERE Id = {numId}";
                command.ExecuteNonQuery();
                connection.Close();

            };
        }

        private static int GetUserInputQuantity()
        {
            Console.Write("Type the Quantity: ");
            string qytStr = Console.ReadLine();

            if (qytStr == "0") MainMenu();

            int quantity = int.Parse(qytStr);


            bool isValid = false;

            while (isValid == false)
            {
                if( quantity <= 0)
                {
                    Console.WriteLine("Quantity must be more then 0, try again");
                    int x = int.Parse(Console.ReadLine());

                    if(x > 0)
                    {
                        Console.WriteLine($"Qutantity =  {quantity} added to db");
                        isValid = true;
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            return quantity;
        }

        private static string GetDate()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: yyyy-mm-dd). Type 0 to return to main manu.\n\n");
            string date = Console.ReadLine();
            if (date == "0") MainMenu();

            while (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (Format: yyyy-mm-dd hh:mm). Type 0 to return to main manu or try again:\n\n");
                date = Console.ReadLine();
            }
            return date;
        }

    }

}

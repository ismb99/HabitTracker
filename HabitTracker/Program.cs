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

            bool kör = false;
            while (kör == false)
            {
                Console.WriteLine("Welcome to the habit tracker, make a choice\n\n");
                Console.WriteLine("0 to quit");
                Console.WriteLine("1 show all records");
                Console.WriteLine("2 insert");
                Console.WriteLine("3 Delete");
                Console.WriteLine("4 Update");
                Console.WriteLine("---------------------------\n\n");

                string input = Console.ReadLine();

                switch (input)
                {
                    case "0":
                        Console.WriteLine("Goodbye");
                        kör = true;
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

                }
            }
        }

        private static void Instert()
        {
            Console.Clear();
            Console.WriteLine("skriv in datum");
            string dateInput = Console.ReadLine();
            Console.WriteLine("Skriv antal ggr");
            string qtyStr = Console.ReadLine();
            int quantity = int.Parse(qtyStr);

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO drinking_cola(Date, Quantity) VALUES('{dateInput}', {quantity})";
                command.ExecuteNonQuery();
                connection.Close();
            };

        }

        internal static void Update()
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

                Console.WriteLine("Type the date");
                string date = Console.ReadLine();
                Console.WriteLine("Type the Quantity");
                string qytStr = Console.ReadLine();
                int quantity = int.Parse(qytStr);

                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE drinking_cola SET date = '{date}', quantity = {quantity} WHERE Id = {numId}";
                command.ExecuteNonQuery();
                connection.Close();



            };
        }


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
                    Console.WriteLine($"{reader.GetInt32(0)} {reader.GetString(1)} {reader.GetInt32(2)}");
                    Console.WriteLine();
                }

                connection.Close();

            };

        }


    }
   
}

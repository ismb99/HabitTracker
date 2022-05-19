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
       private const string connectionString = @"URI=file:C:\Users\Ismail\Desktop\habit1.db";

        public void DatabaseProvider()
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = @"Create table if not EXISTS  drinking_cola(id integer primary key autoincrement,Date Text, Quantity integer)";
                command.ExecuteNonQuery();
                connection.Close();
            };
        }


        // Show all records in the database
        public static void DisplayAll()
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

        public static void Instert()
        {
            Console.Clear();
            string date = ProgramHelpers.GetDate();
            var quantity = ProgramHelpers.GetUserInputQuantity();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"INSERT INTO drinking_cola(Date, Quantity) VALUES('{date}', {quantity})";
                command.ExecuteNonQuery();
                connection.Close();
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
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM drinking_cola WHERE id= {numInput}";

                command.ExecuteNonQuery();
                connection.Close();
            };
        }

        public static void Update()
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

                string date = ProgramHelpers.GetDate();
                var quantity = ProgramHelpers.GetUserInputQuantity();

                var command = connection.CreateCommand();
                command.CommandText = $"UPDATE drinking_cola SET date = '{date}', quantity = {quantity} WHERE Id = {numId}";
                command.ExecuteNonQuery();
                connection.Close();

            };
        }

    }



}

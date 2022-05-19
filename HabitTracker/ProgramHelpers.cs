using System;
using System.Globalization;

namespace HabitTracker
{
    public static class ProgramHelpers
    {

        public static void MainMenu()
        {
            bool closeApp = false;
            while (!closeApp)
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
                        Database.DisplayAll();
                        break;

                    case "2":
                        Database.Instert();
                        break;

                    case "3":
                        Database.Delete();
                        break;

                    case "4":
                        Database.Update();
                        break;

                    default:
                        Console.WriteLine("wrong choice, you can only choose between 0-4\n");
                        break;
                }
            }
        }


        public static int GetUserInputQuantity()
        {
            Console.Write("Type the Quantity: ");
            string qytStr = Console.ReadLine();

            if (qytStr == "0") ProgramHelpers.MainMenu();

            int quantity = int.Parse(qytStr);

            bool isValid = false;

            while (!isValid)
            {
                if(quantity < 0)
                {
                    while (quantity < 0)
                    {
                        Console.WriteLine("Quantity must be more then 0, try again");
                        quantity = int.Parse(Console.ReadLine());
                    }
                }
               
                Console.WriteLine($"Qutantity {quantity} added to db\n");
                isValid = true;
           
            }
            return quantity;
        }

        public static string GetDate()
        {
            Console.WriteLine("\n\nPlease insert the date: (Format: yyyy-mm-dd). Type 0 to return to main manu.\n\n");
            string date = Console.ReadLine();
            if (date == "0") ProgramHelpers.MainMenu();

            while (!DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                Console.WriteLine("\n\nInvalid date. (Format: yyyy-mm-dd hh:mm). Type 0 to return to main manu or try again:\n\n");
                date = Console.ReadLine();
            }
            return date;
        }
    }
}

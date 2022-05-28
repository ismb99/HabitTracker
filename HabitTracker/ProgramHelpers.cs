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
                Console.WriteLine("4 Delete All");
                Console.WriteLine("5 Update");
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
                        Database.DeleteAll();
                        break;

                    case "5":
                        Database.Update();
                        break;


                    default:
                        Console.WriteLine("wrong choice, you can only choose between 0-4\n");
                        break;
                }
            }
        }



    }
}

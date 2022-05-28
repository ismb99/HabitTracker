using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HabitTracker
{
    public class UserInputs
    {
        public static int GetUserInputQuantity()
        {
            Console.Write("Type the Quantity: ");
            string qytStr = Console.ReadLine();

            if (qytStr == "0") ProgramHelpers.MainMenu();

            int quantity = int.Parse(qytStr);

            bool isValid = false;

            while (!isValid)
            {
                if (quantity < 0)
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

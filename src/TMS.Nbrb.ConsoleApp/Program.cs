using System;
using System.Linq;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Manager;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.ConsoleApp
{
    class Program
    {
       static IRequestService requestService = new RequestService();

        static void Main(string[] args)
        {
            var data = requestService.GetAllAsync().GetAwaiter().GetResult();
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine("| ID | Abbreviation |            Currency Name            |     Multilanguage Name     |");
            Console.WriteLine("_______________________________________________________________________________________");

            foreach (var item in data)
            {
                Console.WriteLine("|{0,4}|{1, 14}|{2, 37}|{3, 28}|", item.Cur_ID, item.Cur_Abbreviation, item.Cur_Name, item.Cur_Name_Eng);
            }

            Console.WriteLine("_______________________________________________________________________________________");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Conversation");
            Console.ResetColor();
            Console.ReadKey();

            Console.WriteLine("Enter currency abbreviation:");
            var abbreviation=Console.ReadLine();
            var code = data.FirstOrDefault(x => x.Cur_Abbreviation.ToLower() == abbreviation.ToLower()).Cur_ID;
            Conversion(code);
        }
        public static void Conversion(int code)
        {
            var coefficient = requestService.GetRateAsync(code.ToString()).GetAwaiter().GetResult().Cur_OfficialRate;
            Console.WriteLine("Enter amount, BYN:");
            var amount =Console.ReadLine();
            CurrencyConversion.Conversion(Convert.ToInt32(amount), coefficient);
        }
    }
}
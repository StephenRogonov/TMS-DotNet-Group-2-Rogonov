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
        static IFileService fileService = new FileService();

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
            Console.WriteLine("Conversion");
            Console.ResetColor();

            Console.WriteLine("Enter currency abbreviation:");
            var abbreviation = Console.ReadLine();
            var code = data.FirstOrDefault(x => x.Cur_Abbreviation.ToLower() == abbreviation.ToLower()).Cur_ID;
            Conversion(code);

            Console.WriteLine("\nEnter currency ID (numeric value only) to export the exchange rate dynamics during the past week.\nPress any other key to skip.\n");
            var currCode = Console.ReadLine();

            int i = 0;
            if (int.TryParse(currCode, out i))
            {
                var currency = requestService.GetAsync(currCode).GetAwaiter().GetResult();
                var header = currency.Cur_Name_Eng + " dynamics for " + currency.Cur_Scale + $" over the past week {DateTime.Now.AddDays(-7):dd-MM-yyyy} - {DateTime.Now:dd-MM-yyyy} is:";
                var dynamics = requestService.GetRatesAsync(currCode).GetAwaiter().GetResult();
                fileService.WriteToFile(header);
                foreach (var item in dynamics)
                {
                    fileService.WriteToFile(item.Date.ToString("dd-MM-yyyy") + " - " + item.Cur_OfficialRate);
                }
                Console.WriteLine("Data is successfully added to file.");
            }
        }
        public static void Conversion(int code)
        {
            var coefficient = requestService.GetRateAsync(code.ToString()).GetAwaiter().GetResult().Cur_OfficialRate;
            Console.WriteLine("Enter amount, BYN:");
            var amount = Console.ReadLine();
            CurrencyConversion.Conversion(Convert.ToInt32(amount), coefficient);
        }
    }
}
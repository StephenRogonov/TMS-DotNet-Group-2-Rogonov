using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Manager;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.ConsoleApp
{
    class Program
    {
        static readonly IRequestService requestService = new RequestService();
        static readonly IFileService fileService = new FileService();

        static void Main(string[] args)
        {
            Console.Title = "NBRB Converter v.1.0";
            const string welcome = "Welcome to NBRB Converter v.1.0\n\n";
            foreach (char c in welcome)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write(c);
                Thread.Sleep(30);
                Console.ResetColor();
            }

            var dateTime = DateTime.Today;
            Console.WriteLine("List of current currencies {0:dd/MM/yyyy}", dateTime);
            requestService.RequestTable(requestService);

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nConversion");
            Console.ResetColor();

            Console.WriteLine("Enter currency abbreviation:");
            while (true)
            {
                var abbreviation = Console.ReadLine();

                Regex regex = new Regex(@"[\d!#/., ]");
                MatchCollection matches = regex.Matches(abbreviation);
                if (matches.Count == 0 & abbreviation.Length == 3)
                {
                    var allCurrencies = requestService.GetAllAsync().GetAwaiter().GetResult();
                    var code = allCurrencies.FirstOrDefault(x => x.Cur_Abbreviation.ToLower() == abbreviation.ToLower()).Cur_ID;
                    Conversion(code);
                    break;
                }
                else
                {
                    Console.WriteLine("The entered abbreviation is not correct, please check and try again");
                }
            }

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
            Console.WriteLine("Enter amount to convert to BYN:");
            var amount = Console.ReadLine();
            CurrencyConversion.Conversion(Convert.ToInt32(amount), coefficient);
        }
    }
}
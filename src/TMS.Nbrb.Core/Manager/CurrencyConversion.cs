using System;
using System.Linq;
using System.Text.RegularExpressions;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.Core.Manager
{
    /// <summary>
    /// Currency conversion manager.
    /// </summary>
    public class CurrencyConversion
    {
        static readonly IRequestService requestService = new RequestService();
        static string code;

        public void Conversion()
        {
            Console.WriteLine("Enter currency abbreviation:");
            while (true)
            {
                var abbreviation = Console.ReadLine();
                Regex regex = new Regex(@"[\d!#/., ]");
                MatchCollection matches = regex.Matches(abbreviation);
                if (matches.Count == 0 & abbreviation.Length == 3)
                {
                    var allCurrencies = requestService.GetAllAsync().GetAwaiter().GetResult();
                    code = allCurrencies.FirstOrDefault(x => x.Cur_Abbreviation.ToLower() == abbreviation.ToLower()).Cur_ID.ToString();
                    break;
                }
                else
                {
                    Console.WriteLine("The entered abbreviation is not correct, please check and try again");
                }
            }

            var coefficient = requestService.GetRateAsync(code.ToString()).GetAwaiter().GetResult().Cur_OfficialRate;
            var scale = requestService.GetRateAsync(code.ToString()).GetAwaiter().GetResult().Cur_Scale;
            Console.WriteLine("Enter amount to convert to BYN:");
            int.TryParse(Console.ReadLine(), out int amount);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Conversion result = " + amount * coefficient / scale);
            Console.ResetColor();
            Console.WriteLine("Press any key to continue");
            Console.ReadKey();
        }
    }
}

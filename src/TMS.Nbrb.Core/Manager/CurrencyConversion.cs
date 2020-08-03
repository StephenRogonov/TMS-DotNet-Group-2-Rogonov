using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.Core.Manager
{
    /// <summary>
    /// Currency conversion manager.
    /// </summary>
    public class CurrencyConversion
    {
        private readonly IRequestService _requestService;
        private string code;

        public CurrencyConversion()
        {
            _requestService = new RequestService();
        }

        public async Task ConversionAsync()
        {
            Console.WriteLine("Enter currency abbreviation:");
            while (true)
            {
                var abbreviation = Console.ReadLine();
                Regex regex = new Regex(@"[\d!#/., ]");
                MatchCollection matches = regex.Matches(abbreviation);
                if (matches.Count == 0 & abbreviation.Length == 3)
                {
                    var allCurrencies = await _requestService.GetAllAsync();
                    code = allCurrencies.FirstOrDefault(x => x.Cur_Abbreviation.ToLower() == abbreviation.ToLower()).Cur_ID.ToString();
                    break;
                }
                else
                {
                    Console.WriteLine("The entered abbreviation is not correct, please check and try again");
                }
            }

            var coefficient = (await _requestService.GetRateAsync(code.ToString())).Cur_OfficialRate;
            var scale = (await _requestService.GetRateAsync(code.ToString())).Cur_Scale;
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

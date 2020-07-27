using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Models;

namespace TMS.Nbrb.Core.Interfaces
{
    public interface IRequestService
    {
        Task<Currency> GetAsync(string code);
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Rate> GetRateAsync(string code);
        Task<IEnumerable<Dynamics>> GetRatesAsync(string code);

        async Task RequestTable(IRequestService requestService)
        {
            var allCurrencies = await requestService.GetAllAsync();
            Console.WriteLine(new string('-', 88));
            Console.WriteLine("| ID | Abbreviation |            Currency Name            |     Multilanguage Name     |");
            Console.WriteLine(new string('-', 88));

            foreach (var item in allCurrencies)
            {
                Console.WriteLine("|{0,4}|{1, 14}|{2, 37}|{3, 28}|", item.Cur_ID, item.Cur_Abbreviation, item.Cur_Name, item.Cur_Name_Eng);
            }
            Console.WriteLine(new string('-', 88));
        }
    }
}

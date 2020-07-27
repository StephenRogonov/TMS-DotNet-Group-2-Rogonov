using System;
using System.Collections.Generic;
using System.Text;
using TMS.Nbrb.Core.Models;
using System.Threading.Tasks;


namespace TMS.Nbrb.Core.Interfaces
{
   public interface IRequestService
    {
        Task<Currency> GetAsync(string code);
        Task<IEnumerable<Currency>> GetAllAsync();
        Task<Rate> GetRateAsync(string code);
        Task<IEnumerable<Dynamics>> GetRatesAsync(string code);
        
        void RequestTable(IRequestService requestService)
        {
            var allCurrencies = requestService.GetAllAsync().GetAwaiter().GetResult();
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine("| ID | Abbreviation |            Currency Name            |     Multilanguage Name     |");
            Console.WriteLine("_______________________________________________________________________________________");

            foreach (var item in allCurrencies)
            {
                Console.WriteLine("|{0,4}|{1, 14}|{2, 37}|{3, 28}|", item.Cur_ID, item.Cur_Abbreviation, item.Cur_Name, item.Cur_Name_Eng);
            }
            Console.WriteLine("_______________________________________________________________________________________");
        }
    }
}

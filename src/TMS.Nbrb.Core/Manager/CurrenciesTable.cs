using System;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.Core.Manager
{
    /// <summary>
    /// Manager for displaying currencies.
    /// </summary>
    public class CurrenciesTable
    {
        static readonly IRequestService requestService = new RequestService();
        public void ShowCurrencies()
        {
            var dateTime = DateTime.Today;
            Console.WriteLine("List of current currencies {0:dd/MM/yyyy}", dateTime);
            RequestTable(requestService).GetAwaiter().GetResult();
        }

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

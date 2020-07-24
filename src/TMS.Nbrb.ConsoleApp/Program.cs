using System;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            IRequestService requestService = new RequestService();
            
            var data = requestService.GetAllAsync().GetAwaiter().GetResult();
            Console.WriteLine("_______________________________________________________________________________________");
            Console.WriteLine("| ID | Abbrevistion |            Currency Name            |     Multilanguage Name     |");
            Console.WriteLine("_______________________________________________________________________________________");

            foreach (var item in data)
            {
                Console.WriteLine("|{0,4}|{1, 14}|{2, 37}|{3, 28}|", item.Cur_ID, item.Cur_Abbreviation, item.Cur_Name, item.Cur_Name_Eng);
                //Console.WriteLine("|  " + item.Cur_ID+"  | "+item.Cur_Abbreviation +" |  "+ item.Cur_Name + " | " + item.Cur_Name_EngMulti + " |");
            }

            Console.WriteLine("_______________________________________________________________________________________");

            Console.ReadKey();
        }
    }
}
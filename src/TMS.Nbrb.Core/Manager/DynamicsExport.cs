using System;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.Core.Manager
{
    /// <summary>
    /// Dynamics export to txt file.
    /// </summary>
    public class DynamicsExport
    {
        static readonly IRequestService requestService = new RequestService();
        static readonly IFileService fileService = new FileService();

        public void FileExport()
        {
            Console.WriteLine("\nEnter currency ID (numeric value only) to export the exchange rate dynamics during the past week.\nPress any other key to skip.\n");
            var currCode = Console.ReadLine();

            if (int.TryParse(currCode, out int i))
            {
                var currency = requestService.GetAsync(currCode).GetAwaiter().GetResult();
                var header = currency.Cur_Name_Eng + " dynamics for " + currency.Cur_Scale + $" over the past week {DateTime.Now.AddDays(-7):dd-MM-yyyy} - {DateTime.Now:dd-MM-yyyy} is:";
                var dynamics = requestService.GetDynamicsAsync(currCode).GetAwaiter().GetResult();
                fileService.WriteToFileAsync(header).GetAwaiter().GetResult();
                foreach (var item in dynamics)
                {
                    fileService.WriteToFileAsync(item.Date.ToString("dd-MM-yyyy") + " - " + item.Cur_OfficialRate).GetAwaiter().GetResult();
                }
                Console.WriteLine("Data is successfully added to file.");
            }
        }
    }
}

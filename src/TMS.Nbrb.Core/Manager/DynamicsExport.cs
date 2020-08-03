using System;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Services;

namespace TMS.Nbrb.Core.Manager
{
    /// <summary>
    /// Dynamics export to txt file.
    /// </summary>
    public class DynamicsExport
    {
        private readonly IRequestService _requestService;
        private readonly IFileService _fileService;

        public DynamicsExport()
        {
            _requestService = new RequestService();
            _fileService = new FileService();
        }

        public async Task FileExportAsync()
        {
            Console.WriteLine("\nEnter currency ID (numeric value only) to export the exchange rate dynamics during the past week.\nPress any other key to skip.\n");
            var currCode = Console.ReadLine();
            if (int.TryParse(currCode, out _))
            {
                var currency = await _requestService.GetAsync(currCode);
                var header = currency.Cur_Name_Eng + " dynamics for " + currency.Cur_Scale + $" over the past week {DateTime.Now.AddDays(-7):dd-MM-yyyy} - {DateTime.Now:dd-MM-yyyy} is:";
                var dynamics = await _requestService.GetDynamicsAsync(currCode);
                await _fileService.WriteToFileAsync(header);
                foreach (var item in dynamics)
                {
                    await _fileService.WriteToFileAsync(item.Date.ToString("dd-MM-yyyy") + " - " + item.Cur_OfficialRate);
                }
                Console.WriteLine("Data is successfully added to file.");
            }
        }
    }
}

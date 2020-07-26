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
    }
}

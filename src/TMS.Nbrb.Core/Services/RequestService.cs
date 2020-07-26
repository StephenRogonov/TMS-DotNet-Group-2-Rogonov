using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Models;

namespace TMS.Nbrb.Core.Services
{
    public class RequestService: IRequestService
    {
        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            var response = await "https://www.nbrb.by/api/exrates/currencies"
               .GetJsonAsync<List<Currency>>();
            response = response.Where(x => x.Cur_DateEnd > DateTime.Now).ToList();

            return response;
        }
        public async Task<Currency> GetAsync(string code)
        {
            var response = await "https://www.nbrb.by/api/exrates/currencies"
                .AppendPathSegment(code)
                .GetJsonAsync<Currency>();

            return response;
        }
        public async Task<Rate> GetRateAsync(string code)
        {
            var response = await "https://www.nbrb.by/api/exrates/rates"
                .AppendPathSegment(code)
                .GetJsonAsync<Rate>();

            return response;
        }
        public async Task<IEnumerable<Dynamics>> GetRatesAsync(string code)
        {
            var response = await "https://www.nbrb.by/api/exrates/rates/dynamics"
                .AppendPathSegment(code)
                .SetQueryParams(new { startDate = DateTime.Now.AddDays(-7).ToString("yyyy-M-d"), endDate = DateTime.Now.ToString("yyyy-M-d") })
                .GetJsonAsync<List<Dynamics>>();

            return response;
        }
    }
}

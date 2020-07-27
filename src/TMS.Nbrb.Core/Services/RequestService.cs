using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Helpers;
using TMS.Nbrb.Core.Interfaces;
using TMS.Nbrb.Core.Models;

namespace TMS.Nbrb.Core.Services
{
    public class RequestService: IRequestService
    {
        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            var response = await Constants.Url
                .AppendPathSegments("exrates", "currencies")
                .GetJsonAsync<List<Currency>>();

            return response.Where(x => x.Cur_DateEnd > DateTime.Now).ToList();
        }

        public async Task<Currency> GetAsync(string code)
        {
            var response = await Constants.Url
                .AppendPathSegments("exrates", "currencies", code)
                .GetJsonAsync<Currency>();

            return response;
        }

        public async Task<Rate> GetRateAsync(string code)
        {
            var response = await Constants.Url
                .AppendPathSegments("exrates", "rates", code)
                .GetJsonAsync<Rate>();

            return response;
        }

        public async Task<IEnumerable<Dynamics>> GetRatesAsync(string code)
        {
            var response = await Constants.Url
                .AppendPathSegments("exrates", "rates", "dynamics", code)
                .SetQueryParams(new { startDate = DateTime.Now.AddDays(-7).ToString("yyyy-M-d"), endDate = DateTime.Now.ToString("yyyy-M-d") })
                .GetJsonAsync<List<Dynamics>>();

            return response;
        }
    }
}

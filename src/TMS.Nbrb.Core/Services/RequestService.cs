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
    }
}

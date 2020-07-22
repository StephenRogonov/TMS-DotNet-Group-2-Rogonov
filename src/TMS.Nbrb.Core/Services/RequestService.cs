using Flurl;
using Flurl.Http;
using System;
using System.Collections.Generic;
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

            return response;
        }
        private int List<T>()
        {
            throw new NotImplementedException();
        }
        public async Task<Currency> GetAsync(string code)
        {
            var response = await "https://www.nbrb.by/api/exrates/currencies"
                .AppendPathSegment(code)
                .GetJsonAsync<Currency>();

            return response;
        }
    }
}

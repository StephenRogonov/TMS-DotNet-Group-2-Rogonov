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
    /// <inheritdoc cref="IRequestService"/>
    public class RequestService : IRequestService
    {
        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            try
            {
                var response = await Constants.Url
                    .AppendPathSegments("exrates", "currencies")
                    .GetJsonAsync<List<Currency>>();
                return response.Where(x => x.Cur_DateEnd > DateTime.Now).ToList();
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<Currency> GetAsync(string code)
        {
            try
            {
                var response = await Constants.Url
                .AppendPathSegments("exrates", "currencies", code)
                .GetJsonAsync<Currency>();

                return response;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<Rate> GetRateAsync(string code)
        {
            try
            {
                var response = await Constants.Url
                .AppendPathSegments("exrates", "rates", code)
                .GetJsonAsync<Rate>();

                return response;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public async Task<IEnumerable<Dynamics>> GetDynamicsAsync(string code)
        {
            try
            {
                var response = await Constants.Url
                .AppendPathSegments("exrates", "rates", "dynamics", code)
                .SetQueryParams(new { startDate = DateTime.Now.AddDays(-7).ToString("yyyy-M-d"), endDate = DateTime.Now.ToString("yyyy-M-d") })
                .GetJsonAsync<List<Dynamics>>();

                return response;
            }
            catch (FlurlHttpTimeoutException)
            {
                Console.WriteLine("Request timed out.");
            }
            catch (FlurlHttpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}

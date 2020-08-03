using System.Collections.Generic;
using System.Threading.Tasks;
using TMS.Nbrb.Core.Models;

namespace TMS.Nbrb.Core.Interfaces
{
    /// <summary>
    /// Service for working with requests.
    /// </summary>
    public interface IRequestService
    {
        /// <summary>
        /// Get specific currency.
        /// </summary>
        /// <param name="code">Currency code.</param>
        /// <returns>Currency.</returns>
        Task<Currency> GetAsync(string code);

        /// <summary>
        /// Get all currencies.
        /// </summary>
        /// <returns>All currencies.</returns>
        Task<IEnumerable<Currency>> GetAllAsync();

        /// <summary>
        /// Get rate of the specific currency.
        /// </summary>
        /// <param name="code">Currency code.</param>
        /// <returns>Currency rate.</returns>
        Task<Rate> GetRateAsync(string code);

        /// <summary>
        /// Get daily rate of the specific currency over a specified time period. 
        /// </summary>
        /// <param name="code">Currency code.</param>
        /// <returns>Currency rates.</returns>
        Task<IEnumerable<Dynamics>> GetDynamicsAsync(string code);
    }
}

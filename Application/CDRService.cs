using Domain.Entities;
using Infrastructure;

namespace Application
{
    public class CDRService : ICDRService
    {
        private readonly ICDRRepository _cdrRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CDRService"/> class.
        /// </summary>
        /// <param name="cdrRepository">The CDR repository to be used for data access.</param>
        public CDRService(ICDRRepository cdrRepository)
        {
            _cdrRepository = cdrRepository ?? throw new ArgumentNullException(nameof(cdrRepository));
        }

        /// <summary>
        /// Retrieves a CDR by its reference asynchronously.
        /// </summary>
        /// <param name="reference">The unique reference of the CDR to retrieve.</param>
        /// <returns>The CDR with the specified reference.</returns>
        public async Task<CDR> GetByReferenceAsync(string reference)
        {
            return await _cdrRepository.GetByReferenceAsync(reference);
        }

        /// <summary>
        /// Retrieves the count and total duration of all calls within a specified time period asynchronously.
        /// </summary>
        /// <param name="startDate">The start date of the time period.</param>
        /// <param name="endDate">The end date of the time period.</param>
        /// <returns>A dictionary containing the count and total duration of calls.</returns>
        public async Task<Dictionary<string, object>> GetCallCountAndTotalDurationAsync(DateTime startDate, DateTime endDate)
        {
            var filteredCdrs = await _cdrRepository.GetCdrsByDateRangeAsync(startDate, endDate);

            var count = filteredCdrs.Count();
            var totalDuration = filteredCdrs.Sum(a => a.Duration);

            return new Dictionary<string, object>
            {
                { "Count", count },
                { "TotalDuration", totalDuration }
            };
        }

        /// <summary>
        /// Retrieves all CDRs for a specific caller ID within a specified time period asynchronously.
        /// </summary>
        /// <param name="callerId">The caller ID for which to retrieve CDRs.</param>
        /// <param name="startDate">The start date of the time period.</param>
        /// <param name="endDate">The end date of the time period.</param>
        /// <returns>A collection of CDRs for the specified caller ID.</returns>
        public async Task<IEnumerable<CDR>> GetCdrsByCallerIdAsync(string callerId, DateTime startDate, DateTime endDate)
        {
            return await _cdrRepository.GetCdrsByCallerIdAsync(callerId, startDate, endDate);
        }

        /// <summary>
        /// Retrieves the N most expensive calls for a specific caller ID within a specified time period asynchronously.
        /// </summary>
        /// <param name="callerId">The caller ID for which to retrieve the most expensive calls.</param>
        /// <param name="startDate">The start date of the time period.</param>
        /// <param name="endDate">The end date of the time period.</param>
        /// <param name="count">The number of most expensive calls to retrieve.</param>
        /// <returns>A collection of the N most expensive calls for the specified caller ID.</returns>
        public async Task<IEnumerable<CDR>> GetMostExpensiveCallsAsync(string callerId, DateTime startDate, DateTime endDate, int count)
        {
            var filteredCdrs = await _cdrRepository.GetCdrsByCallerIdAsync(callerId, startDate, endDate);
            return filteredCdrs.OrderByDescending(a => a.Cost).Take(count);
        }
    }
}
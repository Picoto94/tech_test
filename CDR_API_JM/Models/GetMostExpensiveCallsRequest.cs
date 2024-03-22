using System.ComponentModel.DataAnnotations;

namespace CDR_API_JM.Models
{
    /// <summary>
    /// Request object for retrieving N most expensive calls
    /// </summary>
    public class GetMostExpensiveCallsRequest
    {
        /// <summary>
        /// Caller ID for filtering CDRs
        /// </summary>
        [Required]
        public string CallerId { get; set; }

        /// <summary>
        /// Start date for the time period
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date for the time period. Should be in ISO 8601 format.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Number of most expensive calls to retrieve. Should be in ISO 8601 format.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int Count { get; set; }
    }
}
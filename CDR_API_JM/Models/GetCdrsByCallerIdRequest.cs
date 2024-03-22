using System.ComponentModel.DataAnnotations;
using Swashbuckle.AspNetCore.Annotations;

namespace CDR_API_JM.Models
{
    /// <summary>
    /// Request object for retrieving all CDRs for a specific Caller ID
    /// </summary>
    public class GetCdrsByCallerIdRequest
    {
        /// <summary>
        /// Caller ID for filtering CDRs
        /// </summary>
        [Required]
        public string CallerId { get; set; }

        /// <summary>
        /// Start date for the time period. Should be in ISO 8601 format.
        /// </summary>
        [Required]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// End date for the time period. Should be in ISO 8601 format.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }
    }
}
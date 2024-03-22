using System.ComponentModel.DataAnnotations;

namespace CDR_API_JM.Models
{
    /// <summary>
    /// Request object for retrieving call count and total duration
    /// </summary>
    public class GetCallCountAndTotalDurationRequest
    {
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
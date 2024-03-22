using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Globalization;

namespace Infrastructure
{
    /// <summary>
    /// Repository for accessing Call Detail Records (CDRs) from CSV files.
    /// </summary>
    public class CDRRepository : ICDRRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IEnumerable<CDR> _cdrs;

        /// <summary>
        /// Initializes a new instance of the CDRRepository class.
        /// </summary>
        /// <param name="configuration">The configuration containing the path to the CSV files.</param>
        public CDRRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _cdrs = ReadCDRsFromCsvDirectory();
        }

        /// <summary>
        /// Retrieves all Call Detail Records (CDRs).
        /// </summary>
        /// <returns>An enumerable collection of CDRs.</returns>
        public IEnumerable<CDR> GetAllCDRs()
        {
            return _cdrs;
        }
        /// <summary>
        /// Reads CSV files in a specified directory.
        /// </summary>
        /// <returns>A list of CDRs read from all the CSV files.</returns>
        private List<CDR> ReadCDRsFromCsvDirectory()
        {
            var csvDirectoryPath = Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "\\Infrastructure";
            var cdrs = new List<CDR>();

            foreach (var csvFilePath in Directory.GetFiles(csvDirectoryPath, "*.csv"))
            {
                cdrs.AddRange(ReadCDRsFromCsv(csvFilePath));
            }

            return cdrs;
        }

        /// <summary>
        /// Retrieves a Call Detail Record (CDR) by its reference asynchronously.
        /// </summary>
        /// <param name="reference">The reference of the CDR to retrieve.</param>
        /// <returns>The CDR with the specified reference, or null if not found.</returns>
        public async Task<CDR> GetByReferenceAsync(string reference)
        {
            return await Task.FromResult(_cdrs.FirstOrDefault(a => a.Reference == reference));
        }

        /// <summary>
        /// Retrieves Call Detail Records (CDRs) for a specific caller ID within a specified date range asynchronously.
        /// </summary>
        /// <param name="callerId">The caller ID to filter the CDRs.</param>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>An enumerable collection of CDRs filtered by the caller ID and date range.</returns>
        public async Task<IEnumerable<CDR>> GetCdrsByCallerIdAsync(string callerId, DateTime startDate, DateTime endDate)
        {
            var filteredCdrs = FilterCdrsByDateRange(startDate, endDate)
                                .Where(a => a.CallerId == callerId);
            return await Task.FromResult(filteredCdrs);
        }
        /// <summary>
        /// Reads CDRs from a CSV file
        /// </summary>
        /// <returns>A list of CDRs read from the CSV file.</returns>
        private IEnumerable<CDR> ReadCDRsFromCsv(string csvFilePath)
        {
            var fullPath = Path.Combine(Directory.GetCurrentDirectory(), csvFilePath);

            var cdrs = new List<CDR>();

            using (var reader = new StreamReader(fullPath))
            {
                // Ignore the header line
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var callerId = values[0];
                    var recipient = values[1];
                    var callDateStr = values[2].Trim();
                    var endTimeStr = values[3].Trim();
                    var durationStr = values[4].Trim();
                    var costStr = values[5].Trim();
                    var reference = values[6];
                    var currency = values[7];
                    var typeStr = values[8].Trim();

                    DateTime callDate;
                    DateTime endTime;
                    double duration;
                    decimal cost;
                    CallType type;

                    // Tenta converter as strings de data para DateTime
                    if (!DateTime.TryParseExact(callDateStr, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out callDate))
                    {
                        callDate = DateTime.MinValue;
                    }

                    // Ajuste o formato da string de hora para incluir horas, minutos e segundos
                    if (!DateTime.TryParseExact(endTimeStr, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out endTime))
                    {
                        endTime = DateTime.MinValue;
                    }

                    // Tenta converter a string de duração para um número
                    if (!double.TryParse(durationStr, out duration))
                    {
                        duration = 0.0;
                    }

                    // Tenta converter a string de custo para um número
                    if (!decimal.TryParse(costStr, out cost))
                    {
                        cost = 0;
                    }

                    // Tenta converter a string de tipo para o enum CallType
                    if (!Enum.TryParse(typeStr, out type))
                    {
                        type = CallType.None;
                    }

                    var cdr = new CDR
                    {
                        CallerId = callerId,
                        Recipient = recipient,
                        CallDate = callDate,
                        EndTime = endTime,
                        Duration = duration,
                        Cost = cost,
                        Reference = reference,
                        Currency = currency,
                        Type = type
                    };

                    cdrs.Add(cdr);
                }
            }

            return cdrs;
        }
        /// <summary>
        /// Retrieves Call Detail Records (CDRs) for a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>An enumerable collection of CDRs within the specified date range.</returns>
        public async Task<IEnumerable<CDR>> GetCdrsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var filteredCdrs = await Task.FromResult(FilterCdrsByDateRange(startDate, endDate));

            return filteredCdrs;
        }
        /// <summary>
        /// Filters CDRs by date range.
        /// </summary>
        /// <param name="startDate">The start date of the date range.</param>
        /// <param name="endDate">The end date of the date range.</param>
        /// <returns>An enumerable collection of CDRs within the specified date range.</returns>
        private IEnumerable<CDR> FilterCdrsByDateRange(DateTime startDate, DateTime endDate)
        {
            return _cdrs.Where(a => a.CallDate >= startDate && a.CallDate <= endDate);
        }
    }
}

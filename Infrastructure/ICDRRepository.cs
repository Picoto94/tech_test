using Domain.Entities;

namespace Infrastructure
{
    public interface ICDRRepository
    {
        IEnumerable<CDR> GetAllCDRs();
        Task<CDR> GetByReferenceAsync(string reference);
        Task<IEnumerable<CDR>> GetCdrsByCallerIdAsync(string callerId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<CDR>> GetCdrsByDateRangeAsync(DateTime startDate, DateTime endDate);
    }
}
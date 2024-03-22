using Domain.Entities;


namespace Application
{
    public interface ICDRService
    {
        Task<CDR> GetByReferenceAsync(string reference);
        Task<Dictionary<string, object>> GetCallCountAndTotalDurationAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<CDR>> GetCdrsByCallerIdAsync(string callerId, DateTime startDate, DateTime endDate);
        Task<IEnumerable<CDR>> GetMostExpensiveCallsAsync(string callerId, DateTime startDate, DateTime endDate, int count);
    }
}

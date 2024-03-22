using System;

namespace Domain.Entities
{
    public class CDR
    {
        public string CallerId { get; set; }
        public string Recipient { get; set; }
        public DateTime CallDate { get; set; }
        public DateTime EndTime { get; set; }
        public double Duration { get; set; }
        public decimal Cost { get; set; }
        public string Reference { get; set; }
        public string Currency { get; set; }
        public CallType Type { get; set; }
    }

    public enum CallType
    {
        None = 0,
        Domestic = 1,
        International = 2
    }
}

using System;
using DataExchangeWorkerService.Interface;

namespace DataExchangeWorkerService.Models
{
    public class ClientBOutputModel : IMapFrom<ClientBModel>
    {
        public int ClaimId { get; set; }
        public string BaName { get; set; }
        public string MobileNumber { get; set; }
        public DateTime ClaimDate { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string TransactionId { get; set; }
    }
}

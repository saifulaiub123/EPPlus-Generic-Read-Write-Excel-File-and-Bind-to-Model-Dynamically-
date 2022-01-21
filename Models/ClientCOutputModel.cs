using DataExchangeWorkerService.Interface;

namespace DataExchangeWorkerService.Models
{
    public class ClientCOutputModel : IMapFrom<ClientCModel>
    {
        public string DepoCode { get; set; }
        public string DepoName { get; set; }
    }
}

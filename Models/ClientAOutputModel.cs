using AutoMapper;
using DataExchangeWorkerService.Interface;

namespace DataExchangeWorkerService.Models
{
    public class ClientAOutputModel : IMapFrom<ClientAModel>
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string OrderNo { get; set; }
        public int Quantity { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ClientAModel, ClientAOutputModel>()
                .ForMember(m => m.ClientId, opt => opt.MapFrom(c => c.ClientId));

            profile.CreateMap<ClientAModel, ClientAOutputModel>()
                .ForMember(m => m.ClientName, opt => opt.MapFrom(c => c.ClientName));
        }
    }
}

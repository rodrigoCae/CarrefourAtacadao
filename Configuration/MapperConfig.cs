using AutoMapper;
using Carrefour_Atacadao_BackEnd.DTO;
using Carrefour_Atacadao_BackEnd.Entites;

namespace Carrefour_Atacadao_BackEnd.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<TbCidade, TbCidadeDTO>().ReverseMap();
            CreateMap<TbCliente, TbClienteDTO>().ReverseMap();
            CreateMap<TbClienteEndereco, TbClienteEnderecoDTO>().ReverseMap();
            CreateMap<TbEndereco, TbEnderecoDTO>().ReverseMap();
        }
    }
}

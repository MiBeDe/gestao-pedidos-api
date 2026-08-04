using AutoMapper;
using GestaoPedidos.Application.ViewModels;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ClienteViewModel, ClienteEntity>().MaxDepth(32).ReverseMap();
            CreateMap<ResultadoOperacaoViewModel, ResultadoOperacaoModel>().MaxDepth(32).ReverseMap();
            CreateMap<ParametrosPaginacaoViewModel, ParametrosPaginacaoModel>().MaxDepth(32).ReverseMap();
            CreateMap<ProdutoViewModel, ProdutoEntity>().MaxDepth(32).ReverseMap();
            CreateMap<PedidoViewModel, PedidoEntity>().MaxDepth(32).ReverseMap();
            CreateMap<PedidoProdutosViewModel, PedidoProdutosEntity>().MaxDepth(32).ReverseMap();
            CreateMap<StatusPedidoViewModel, StatusPedidoEntity>().MaxDepth(32).ReverseMap();
            CreateMap<PedidoListViewModel, PedidoEntity>().MaxDepth(32).ReverseMap();
            CreateMap<ClienteDropdownViewModel, ClienteDropdownModel>().MaxDepth(32).ReverseMap();
            CreateMap<ProdutoDropdownViewModel, ProdutoDropdownModel>().MaxDepth(32).ReverseMap();
        }
    }
}

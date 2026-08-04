using AutoMapper;
using GestaoPedidos.Application.Interfaces;
using GestaoPedidos.Application.ViewModels;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Application.Services
{
    public class PedidoAppService : IPedidoAppService
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IMapper _mapper;

        public PedidoAppService(IPedidoRepository pedidoRepository, IMapper mapper)
        {
            _pedidoRepository = pedidoRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacaoViewModel> CadastrarPedidoAsync(PedidoViewModel pedido, CancellationToken cancellationToken)
        {
            ResultadoOperacaoViewModel result = new();

            if (!pedido.Produtos.Any())
            {
                result.Sucesso = false;
                result.Mensagem = "Não é possível criar um pedido sem informar um produto! Por favor, selecione ao menos um produto para continuar.";
                return result;
            }
                
            
            var resultPedido = await _pedidoRepository.CadastrarPedidoAsync(_mapper.Map<PedidoEntity>(pedido), cancellationToken);

           if (resultPedido > 0)
           {
                List<PedidoProdutosEntity> pedidoProdutos = _mapper.Map<List<PedidoProdutosEntity>>(pedido.Produtos);
                pedidoProdutos.ForEach(x => x.IdPedido = resultPedido);

                await _pedidoRepository.CadastrarPedidoProdutosAsync(pedidoProdutos, cancellationToken);

                result.Sucesso = true;
                result.Mensagem = "Pedido cadastrado com sucesso!";
           }

            return result;
        }

        public async Task<PaginacaoResultViewModel<PedidoListViewModel>> ObterPedidoComPaginacaoAsync(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken)
        {
            var result = await _pedidoRepository.ObterPedidoComPaginacaoAsync(_mapper.Map<ParametrosPaginacaoModel>(parametros), cancellationToken);

            return new PaginacaoResultViewModel<PedidoListViewModel>
            {
                Pagina = result.Pagina,
                TamanhoPagina = result.TamanhoPagina,
                TotalRegistros = result.TotalRegistros,
                TotalPaginas = result.TotalPaginas,
                PossuiProximaPagina = result.PossuiProximaPagina,
                PossuiPaginaAnterior = result.PossuiPaginaAnterior,
                Itens = _mapper.Map<List<PedidoListViewModel>>(result.Itens)
            };
        }

        public async Task<ResultadoOperacaoViewModel> AlterarStatusAsync(int idPedido, int idStatus, CancellationToken cancellationToken)
        {
            var result = await _pedidoRepository.AlterarStatusAsync(idPedido, idStatus, cancellationToken);

            return _mapper.Map<ResultadoOperacaoViewModel>(result);
        }

    }
}

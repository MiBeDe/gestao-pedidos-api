using GestaoPedidos.Application.ViewModels;

namespace GestaoPedidos.Application.Interfaces
{
    public interface IPedidoAppService
    {
        Task<ResultadoOperacaoViewModel> CadastrarPedidoAsync(PedidoViewModel pedido, CancellationToken cancellationToken);
        Task<PaginacaoResultViewModel<PedidoListViewModel>> ObterPedidoComPaginacaoAsync(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken);
        Task<ResultadoOperacaoViewModel> AlterarStatusAsync(int idPedido, int idStatus, CancellationToken cancellationToken);
    }
}

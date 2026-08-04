using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IPedidoRepository
    {
        Task<int> CadastrarPedidoAsync(PedidoEntity pedido, CancellationToken cancellationToken);
        Task CadastrarPedidoProdutosAsync(IEnumerable<PedidoProdutosEntity> pedidoProduto, CancellationToken cancellationToken);
        Task<PaginacaoResultModel<PedidoEntity>> ObterPedidoComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken);
        Task<ResultadoOperacaoModel> AlterarStatusAsync(int idPedido, int idStatus, CancellationToken cancellationToken);
        Task<IEnumerable<PedidoProdutosEntity>> ObterPedidoProdutoByIdPedido(int idPedido);  
    }
}

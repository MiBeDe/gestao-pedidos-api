using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IProdutoRepository
    {
        Task<PaginacaoResultModel<ProdutoEntity>> ObterProdutosComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken);
        Task<ResultadoOperacaoModel> CadastrarProdutoAsync(ProdutoEntity produto, CancellationToken cancellationToken);
        Task<bool> ProdutoCadastrado(string nomeProduto, CancellationToken cancellationToken);
        Task SubtrairQuantidadeProduto(IEnumerable<PedidoProdutosEntity> produtosPedido);
        Task DevolucaoQuantidadeProduto(IEnumerable<PedidoProdutosEntity> produtosPedido);
        Task<ResultadoOperacaoModel> ObterProdutoQuantidadeValidaAsync(int idProduto, int quantidadeSolicitado, CancellationToken cancellationToken);
        Task<IEnumerable<ProdutoDropdownModel>> ObterProdutosDropdown(CancellationToken cancellationToken); 
    }
}

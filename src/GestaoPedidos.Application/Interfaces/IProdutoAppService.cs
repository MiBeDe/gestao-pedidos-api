using GestaoPedidos.Application.ViewModels;

namespace GestaoPedidos.Application.Interfaces
{
    public interface IProdutoAppService
    {
        Task<PaginacaoResultViewModel<ProdutoViewModel>> ObterProdutosComPaginacaoAsync(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken);
        Task<ResultadoOperacaoViewModel> CadastrarProdutoAsync(ProdutoViewModel produto, CancellationToken cancellationToken);
        Task<ResultadoOperacaoViewModel> ObterProdutoQuantidadeValidaAsync(int idProduto, int quantidadeSolicitado, CancellationToken cancellationToken);
        Task<IEnumerable<ProdutoDropdownViewModel>> ObterProdutosDropdown(CancellationToken cancellationToken);
    }
}

using GestaoPedidos.Application.ViewModels;

namespace GestaoPedidos.Application.Interfaces
{
    public interface IClienteAppService
    {
        Task<ResultadoOperacaoViewModel> CadastrarClienteAsync(ClienteViewModel cliente, CancellationToken cancellationToken = default);
        Task<PaginacaoResultViewModel<ClienteViewModel>> ObterClientesComPaginacao(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken = default);
        Task<IEnumerable<ClienteDropdownViewModel>> ObterClientesDropdown(CancellationToken cancellationToken);
    }
}

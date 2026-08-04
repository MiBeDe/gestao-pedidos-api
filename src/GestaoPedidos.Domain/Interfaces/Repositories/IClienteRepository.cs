using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Domain.Interfaces.Repositories
{
    public interface IClienteRepository
    {
        Task<ResultadoOperacaoModel> CadastrarClienteAsync(ClienteEntity cliente, CancellationToken cancellationToken);
        Task<bool> CpfCadastrado(string cpf, CancellationToken cancellationToken);
        Task<PaginacaoResultModel<ClienteEntity>> ObterClientesComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken);
        Task<IEnumerable<ClienteDropdownModel>> ObterClientesDropdown(CancellationToken cancellationToken);
    }
}

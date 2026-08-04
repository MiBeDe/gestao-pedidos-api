using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;
using GestaoPedidos.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly GestaoPedidosContext _context;

        public ClienteRepository(GestaoPedidosContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultModel<ClienteEntity>> ObterClientesComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken)
        {
            var query = _context.Clientes.AsNoTracking();

            var totalRegistros = await query.CountAsync(cancellationToken);

            var itens = await query.OrderBy(x => x.NomeCompleto)
                                   .Skip((parametros.Pagina - 1) * parametros.TamanhoPagina)
                                   .Take(parametros.TamanhoPagina)
                                   .ToListAsync(cancellationToken);

            return new PaginacaoResultModel<ClienteEntity>
            {
                Itens = itens,
                Pagina = parametros.Pagina,
                TamanhoPagina = parametros.TamanhoPagina,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<ResultadoOperacaoModel> CadastrarClienteAsync(ClienteEntity cliente, CancellationToken cancellationToken)
        {
            ResultadoOperacaoModel result = new();

            await _context.Clientes.AddAsync(cliente, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            result.Sucesso = true;
            result.Mensagem = "Cadastro realizado com sucesso";


            return result;
        }

        public async Task<bool> CpfCadastrado(string cpf, CancellationToken cancellationToken) =>
            await _context.Clientes.AnyAsync(x => x.Cpf == cpf, cancellationToken);

        public async Task<IEnumerable<ClienteDropdownModel>> ObterClientesDropdown(CancellationToken cancellationToken) =>
                     await _context.Clientes.Select(x => new ClienteDropdownModel
                     {
                         IdCliente = x.IdCliente,
                         NomeCompleto = x.NomeCompleto,
                     }).ToListAsync(cancellationToken);

    }
}

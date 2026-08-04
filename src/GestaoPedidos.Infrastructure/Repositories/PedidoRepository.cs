using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;
using GestaoPedidos.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly GestaoPedidosContext _context;

        public PedidoRepository(GestaoPedidosContext context)
        {
            _context = context;
        }

        public async Task<int> CadastrarPedidoAsync(PedidoEntity pedido, CancellationToken cancellationToken)
        {
            ResultadoOperacaoModel result = new();

            await _context.Pedidos.AddAsync(pedido, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            return pedido.IdPedido;
        }

        public async Task CadastrarPedidoProdutosAsync(IEnumerable<PedidoProdutosEntity> pedidoProduto, CancellationToken cancellationToken)
        {
            await _context.PedidoProdutos.AddRangeAsync(pedidoProduto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<PaginacaoResultModel<PedidoEntity>> ObterPedidoComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken)
        {
            var query = _context.Pedidos.AsNoTracking();

            var totalRegistros = await query.CountAsync(cancellationToken);

            var itens = await query.OrderByDescending(x => x.IdPedido)
                                   .Include(p => p.Cliente)
                                   .Include(p => p.StatusPedido)
                                   .Include(p => p.PedidoProdutos)
                                   .ThenInclude(pp => pp.Produto)
                                   .Skip((parametros.Pagina - 1) * parametros.TamanhoPagina)
                                   .Take(parametros.TamanhoPagina)
                                   .ToListAsync();

            return new PaginacaoResultModel<PedidoEntity>
            {
                Itens = itens,
                Pagina = parametros.Pagina,
                TamanhoPagina = parametros.TamanhoPagina,
                TotalRegistros = totalRegistros
            };
                                   

        }

        public async Task<ResultadoOperacaoModel> AlterarStatusAsync(int idPedido, int idStatus, CancellationToken cancellationToken)
        {
            ResultadoOperacaoModel result = new();

            var pedido = await _context.Pedidos.Where(x => x.IdPedido == idPedido).FirstOrDefaultAsync();
                
            if(pedido != null)
            {
                pedido.AlterarStatus(idPedido,idStatus, pedido.IdStatus);
                _context.Pedidos.Update(pedido);
                await _context.SaveChangesAsync();
            }

            result.Sucesso = true;
            result.Mensagem = "Status atualizado com sucesso!";

            return result;
        }

        public async Task<IEnumerable<PedidoProdutosEntity>> ObterPedidoProdutoByIdPedido(int idPedido) =>
            await _context.PedidoProdutos.Where(x => x.IdPedido == idPedido).ToListAsync();
    }
}

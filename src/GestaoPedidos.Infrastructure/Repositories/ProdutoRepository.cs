using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Enums;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;
using GestaoPedidos.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace GestaoPedidos.Infrastructure.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GestaoPedidosContext _context;

        public ProdutoRepository(GestaoPedidosContext context)
        {
            _context = context;
        }

        public async Task<PaginacaoResultModel<ProdutoEntity>> ObterProdutosComPaginacaoAsync(ParametrosPaginacaoModel parametros, CancellationToken cancellationToken)
        {
            var query = _context.Produtos.AsNoTracking();

            var totalRegistros = await query.CountAsync(cancellationToken);

            var itens = await query.OrderByDescending(x => x.IdProduto)
                                   .Skip((parametros.Pagina - 1) * parametros.TamanhoPagina)
                                   .Take(parametros.TamanhoPagina)
                                   .ToListAsync(cancellationToken);

            return new PaginacaoResultModel<ProdutoEntity>
            {
                Itens = itens,
                Pagina = parametros.Pagina,
                TamanhoPagina = parametros.TamanhoPagina,
                TotalRegistros = totalRegistros
            };
        }

        public async Task<ResultadoOperacaoModel> CadastrarProdutoAsync(ProdutoEntity produto, CancellationToken cancellationToken)
        {
            ResultadoOperacaoModel result = new();

            await _context.Produtos.AddAsync(produto, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            result.Sucesso = true;
            result.Mensagem = "Cadastro realizado com sucesso";

            return result;
        }

        public async Task<bool> ProdutoCadastrado(string nomeProduto, CancellationToken cancellationToken) =>
            await _context.Produtos.AnyAsync(x => x.NomeProduto == nomeProduto, cancellationToken);

        public async Task SubtrairQuantidadeProduto(IEnumerable<PedidoProdutosEntity> produtosPedido)
        {
            foreach (var produto in produtosPedido)
            {
                var produtoEntity = await _context.Produtos.Where(x => x.IdProduto == produto.IdProduto).FirstOrDefaultAsync();
                var quantidadeAtualizada = produtoEntity.Quantidade - produto.Quantidade;

                produtoEntity.AlterarQuantidade(quantidadeAtualizada);

                _context.Produtos.Update(produtoEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DevolucaoQuantidadeProduto(IEnumerable<PedidoProdutosEntity> produtosPedido)
        {
            foreach (var produto in produtosPedido)
            {
                var produtoEntity = await _context.Produtos.Where(x => x.IdProduto == produto.IdProduto).FirstOrDefaultAsync();
                var quantidadeAtualizada = produtoEntity.Quantidade + produto.Quantidade;

                produtoEntity.AlterarQuantidade(quantidadeAtualizada);

                _context.Produtos.Update(produtoEntity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ResultadoOperacaoModel> ObterProdutoQuantidadeValidaAsync(int idProduto, int quantidadeSolicitado, CancellationToken cancellationToken)
        {
            ResultadoOperacaoModel result = new();

            var produto = await _context.Produtos.Where(x => x.IdProduto == idProduto).FirstOrDefaultAsync(cancellationToken);

            var idPedidos = await _context.Pedidos.Where(x => x.IdStatus == (int)StatusPedido.Criado).Select(x => x.IdPedido).ToListAsync();
            var pedidoProdutos = await _context.PedidoProdutos.Where(x => idPedidos.Contains(x.IdPedido) && x.IdProduto == idProduto)
                                                      .GroupBy(x => x.IdProduto)
                                                      .Select(p => new ProdutoQuantidadeModel
                                                      {
                                                          IdProduto = p.Key,
                                                          Quantidade = p.Sum(p => p.Quantidade)
                                                      }).FirstOrDefaultAsync(cancellationToken);

            var quantidadeEstoqueLiberado = (produto.Quantidade - (pedidoProdutos != null ? pedidoProdutos.Quantidade : 0));

            if(!(quantidadeSolicitado <= quantidadeEstoqueLiberado))
            {
                result.Sucesso = false;
                result.Mensagem = $"Para incluir esse produto é necessário adicionar mais {(quantidadeSolicitado - quantidadeEstoqueLiberado)} unidades ao estoque. Base de cálculo ( Pedidos Aguardando Confimação + Quantidade Solicitada ).";
                return result;
            }

            result.Sucesso = true;
            result.Mensagem = "Produto com quantidade suficiente";
            result.Dados.Add(produto);

            return result;
        }

        public async Task<IEnumerable<ProdutoDropdownModel>> ObterProdutosDropdown(CancellationToken cancellationToken) =>
                     await _context.Produtos.Select(x => new ProdutoDropdownModel {
                                             IdProduto = x.IdProduto,
                                              NomeProduto = x.NomeProduto,     
                     }).ToListAsync();
    }
}

using AutoMapper;
using GestaoPedidos.Application.Interfaces;
using GestaoPedidos.Application.ViewModels;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Application.Services
{
    public class ProdutoAppService : IProdutoAppService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMapper _mapper;

        public ProdutoAppService(IProdutoRepository produtoRepository, IMapper mapper)
        {
            _produtoRepository = produtoRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacaoViewModel> CadastrarProdutoAsync(ProdutoViewModel produto, CancellationToken cancellationToken)
        {
            ResultadoOperacaoViewModel result = new();

            if(!await _produtoRepository.ProdutoCadastrado(produto.NomeProduto, cancellationToken))
            {
                result = _mapper.Map<ResultadoOperacaoViewModel>(await _produtoRepository.CadastrarProdutoAsync(_mapper.Map<ProdutoEntity>(produto), cancellationToken));
            }
            else
            {
                result.Sucesso = false;
                result.Mensagem = "Produto já encontra-se cadastrado!";
            }

            return result;
        }

        public async Task<PaginacaoResultViewModel<ProdutoViewModel>> ObterProdutosComPaginacaoAsync(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken)
        {
            var result = await _produtoRepository.ObterProdutosComPaginacaoAsync(_mapper.Map<ParametrosPaginacaoModel>(parametros), cancellationToken);

            return new PaginacaoResultViewModel<ProdutoViewModel>
            {
                Pagina = result.Pagina,
                TamanhoPagina = result.TamanhoPagina,
                TotalRegistros = result.TotalRegistros,
                TotalPaginas = result.TotalPaginas,
                PossuiProximaPagina = result.PossuiProximaPagina,
                PossuiPaginaAnterior = result.PossuiPaginaAnterior,
                Itens = _mapper.Map<List<ProdutoViewModel>>(result.Itens)
            };
        }

        public async Task<ResultadoOperacaoViewModel> ObterProdutoQuantidadeValidaAsync(int idProduto, int quantidadeSolicitado, CancellationToken cancellationToken)
        {
            var resultadoOperacaoViewModel = _mapper.Map<ResultadoOperacaoViewModel>(await _produtoRepository.ObterProdutoQuantidadeValidaAsync(idProduto, quantidadeSolicitado, cancellationToken));        

            if (resultadoOperacaoViewModel.Dados!.Any())
            {
                ProdutoViewModel produtoViewModel = new();

                produtoViewModel.IdProduto = resultadoOperacaoViewModel.Dados![0].IdProduto;
                produtoViewModel.NomeProduto = resultadoOperacaoViewModel.Dados![0].NomeProduto;
                produtoViewModel.Quantidade = resultadoOperacaoViewModel.Dados![0].Quantidade;
                produtoViewModel.Descricao = resultadoOperacaoViewModel.Dados![0].Descricao;
                produtoViewModel.Preco = resultadoOperacaoViewModel.Dados![0].Preco;
                produtoViewModel.QuantidadeSolicitado = quantidadeSolicitado;

                resultadoOperacaoViewModel.Dados.Clear();
                resultadoOperacaoViewModel.Dados.Add(produtoViewModel);
            }
           
            return resultadoOperacaoViewModel;
        }
            

        public async Task<IEnumerable<ProdutoDropdownViewModel>> ObterProdutosDropdown(CancellationToken cancellationToken) =>
            _mapper.Map<IEnumerable<ProdutoDropdownViewModel>>(await _produtoRepository.ObterProdutosDropdown(cancellationToken));
    }
}

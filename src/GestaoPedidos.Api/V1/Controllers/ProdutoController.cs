using Asp.Versioning;
using GestaoPedidos.Application.Interfaces;
using GestaoPedidos.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GestaoPedidos.Api.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutoController : Controller
    {
        private readonly IProdutoAppService _produtoAppService;

        public ProdutoController(IProdutoAppService produtoAppService)
        {
            _produtoAppService = produtoAppService;
        }

        /// <summary>
        /// Obtém lista de todos os produtos cadastrados.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginacaoResultViewModel<ProdutoViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterProdutos([FromQuery] ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken)
        {
            var result = await _produtoAppService.ObterProdutosComPaginacaoAsync(parametros, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Obtém listagem produtos para dropdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("ProdutoDropdown")]
        [ProducesResponseType(typeof(IEnumerable<ProdutoDropdownViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterProdutosDropdown(CancellationToken cancellationToken) =>
            Ok(await _produtoAppService.ObterProdutosDropdown(cancellationToken));

        /// <summary>
        /// Realiza o cadastro do Produto.
        /// </summary>
        /// <param name="produto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultadoOperacaoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CadastrarProduto(ProdutoViewModel produto, CancellationToken cancellationToken) =>
            Ok(await _produtoAppService.CadastrarProdutoAsync(produto, cancellationToken));

        /// <summary>
        /// Verifica se produto possuí estoque disponível.
        /// </summary>
        /// <param name="idProduto"></param>
        /// <param name="quantidadeSolicitado"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("ObterProdutoQuantidadeValida")]
        [ProducesResponseType(typeof(ResultadoOperacaoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterProdutoQuantidadeValidaAsync(int idProduto, int quantidadeSolicitado, CancellationToken cancellationToken) =>
            Ok(await _produtoAppService.ObterProdutoQuantidadeValidaAsync(idProduto, quantidadeSolicitado, cancellationToken));
    }
}

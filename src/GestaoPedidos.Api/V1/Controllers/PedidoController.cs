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
    public class PedidoController : Controller
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        /// <summary>
        /// Obtém lista de todos os pedidos cadastrados.
        /// </summary>
        /// <param name="parametros"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginacaoResultViewModel<PedidoListViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterPedidos([FromQuery] ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken)
        {
            var result = await _pedidoAppService.ObterPedidoComPaginacaoAsync(parametros, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Realiza o cadastro de um novo pedido.
        /// </summary>
        /// <param name="pedido"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultadoOperacaoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CadastrarPedido(PedidoViewModel pedido, CancellationToken cancellationToken) =>
            Ok(await _pedidoAppService.CadastrarPedidoAsync(pedido, cancellationToken));

        /// <summary>
        /// Altera Status do pedido.
        /// </summary>
        /// <param name="idPedido"></param>
        /// <param name="idStatus"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPut("")]
        [ProducesResponseType(typeof(PedidoListViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AlterarStatus(int idPedido, int idStatus, CancellationToken cancellationToken)
        {
            var result = await _pedidoAppService.AlterarStatusAsync(idPedido, idStatus, cancellationToken);

            return Ok(result);
        }

        
    }
}

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
    public class ClienteController : Controller
    {
        private readonly IClienteAppService _clienteAppService;

        public ClienteController(IClienteAppService clienteAppService)
        {
            _clienteAppService = clienteAppService;
        }

        /// <summary>
        /// Obtém lista de todos os clientes cadastrados.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        [ProducesResponseType(typeof(PaginacaoResultViewModel<ClienteViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterClientes([FromQuery] ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken)
        {
            var result = await _clienteAppService.ObterClientesComPaginacao(parametros, cancellationToken);

            return Ok(result);
        }

        /// <summary>
        /// Obtém lista para preencher dropdown.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpGet("ClienteDropdown")]
        [ProducesResponseType(typeof(ClienteDropdownViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ObterClientesDropdown(CancellationToken cancellationToken) =>
            Ok(await _clienteAppService.ObterClientesDropdown(cancellationToken));

        /// <summary>
        /// Realiza o cadastro do Cliente que será proprietário de pedido.
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("")]
        [ProducesResponseType(typeof(ResultadoOperacaoViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CadastrarCliente(ClienteViewModel cliente, CancellationToken cancellationToken) =>
            Ok(await _clienteAppService.CadastrarClienteAsync(cliente, cancellationToken));
    }
}

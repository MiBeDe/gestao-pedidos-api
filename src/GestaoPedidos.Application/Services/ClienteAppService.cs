using AutoMapper;
using GestaoPedidos.Application.Interfaces;
using GestaoPedidos.Application.ViewModels;
using GestaoPedidos.Domain.Entities;
using GestaoPedidos.Domain.Interfaces.Repositories;
using GestaoPedidos.Domain.Models;

namespace GestaoPedidos.Application.Services
{
    public class ClienteAppService : IClienteAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IMapper _mapper;

        public ClienteAppService(IClienteRepository clienteRepository, IMapper mapper)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
        }

        public async Task<ResultadoOperacaoViewModel> CadastrarClienteAsync(ClienteViewModel cliente, CancellationToken cancellationToken = default) 
        {
            ResultadoOperacaoViewModel result = new();

            if(cliente.Cpf == null || cliente.Cpf.Length < 11)
            {
                result.Sucesso = false;
                result.Mensagem = "Preencha corretamente o CPF!";
                return result;
            }

            if(!await _clienteRepository.CpfCadastrado(cliente.Cpf, cancellationToken))
            {
                result = _mapper.Map<ResultadoOperacaoViewModel>(await _clienteRepository.CadastrarClienteAsync(_mapper.Map<ClienteEntity>(cliente), cancellationToken));
            }
            else
            {
                result.Sucesso = false;
                result.Mensagem = "CPF já encontra-se cadastrado!";
            }

            return result;
        }

        public async Task<PaginacaoResultViewModel<ClienteViewModel>> ObterClientesComPaginacao(ParametrosPaginacaoViewModel parametros, CancellationToken cancellationToken = default)
        {
            var result = await _clienteRepository.ObterClientesComPaginacaoAsync(_mapper.Map<ParametrosPaginacaoModel>(parametros), cancellationToken);

            return new PaginacaoResultViewModel<ClienteViewModel>
            {
                Pagina = result.Pagina,
                TamanhoPagina = result.TamanhoPagina,
                TotalRegistros = result.TotalRegistros,
                TotalPaginas = result.TotalPaginas,
                PossuiProximaPagina = result.PossuiProximaPagina,
                PossuiPaginaAnterior = result.PossuiPaginaAnterior,
                Itens = _mapper.Map<List<ClienteViewModel>>(result.Itens)
            };
        }

        public async Task<IEnumerable<ClienteDropdownViewModel>> ObterClientesDropdown(CancellationToken cancellationToken) =>
            _mapper.Map<IEnumerable<ClienteDropdownViewModel>>(await _clienteRepository.ObterClientesDropdown(cancellationToken));
    }
}

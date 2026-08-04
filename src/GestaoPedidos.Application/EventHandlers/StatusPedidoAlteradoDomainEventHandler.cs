using GestaoPedidos.Domain.Enums;
using GestaoPedidos.Domain.Events;
using GestaoPedidos.Domain.Interfaces.Repositories;
using MediatR;

namespace GestaoPedidos.Application.EventHandlers
{
    public class StatusPedidoAlteradoDomainEventHandler : INotificationHandler<StatusPedidoAlteradoDomainEvent>
    {
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IProdutoRepository _produtoRepository;

        public StatusPedidoAlteradoDomainEventHandler(IPedidoRepository pedidoRepository, IProdutoRepository produtoRepository)
        {
            _pedidoRepository = pedidoRepository;
            _produtoRepository = produtoRepository;
        }

        public async Task Handle(StatusPedidoAlteradoDomainEvent notification, CancellationToken cancellationToken)
        {
            switch (notification.idStatus)
            {
                case (int)StatusPedido.Confirmado:

                    var pedidoProdutosConfirmado = await _pedidoRepository.ObterPedidoProdutoByIdPedido(notification.idPedido);
                    await _produtoRepository.SubtrairQuantidadeProduto(pedidoProdutosConfirmado);

                break;
                case (int)StatusPedido.Cancelado:

                    var pedidoProdutosCancelado = await _pedidoRepository.ObterPedidoProdutoByIdPedido(notification.idPedido);
                    await _produtoRepository.DevolucaoQuantidadeProduto(pedidoProdutosCancelado);

                break;
            }
        }
    }
}

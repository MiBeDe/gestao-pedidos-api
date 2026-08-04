using GestaoPedidos.Domain.Common;

namespace GestaoPedidos.Domain.Events
{
    public record StatusPedidoAlteradoDomainEvent(int idPedido, int idStatus) : DomainEvent();
}

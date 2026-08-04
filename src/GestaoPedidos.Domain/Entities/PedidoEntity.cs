using GestaoPedidos.Domain.Common;
using GestaoPedidos.Domain.Events;
using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Domain.Entities
{
    public class PedidoEntity : Entity
    {
        [Key]
        public int IdPedido { get; private set; }
        public int IdCliente { get; private set; }
        public int IdStatus { get; private set; }
        public decimal ValorTotalPedido { get; private set; }

        public virtual ClienteEntity Cliente { get; set; } = null!;
        public virtual StatusPedidoEntity StatusPedido { get; set; } = null!;
        public virtual ICollection<PedidoProdutosEntity> PedidoProdutos { get; set; } = new List<PedidoProdutosEntity>();

        private PedidoEntity(){ }

        public PedidoEntity(int idCliente, int idPedidoProduto, int idStatus, decimal valorTotalPedido)
        {
            IdCliente = idCliente;
            IdStatus = idStatus;
            ValorTotalPedido = valorTotalPedido;
        }

        public void AlterarStatus(int idPedido, int idStatus, int idStatusAtual)
        {
            if(!(idStatusAtual == 1 && idStatus == 3))
            {
                Raise(new StatusPedidoAlteradoDomainEvent(idPedido, idStatus));
            }

            IdStatus = idStatus;
        }
    }
}

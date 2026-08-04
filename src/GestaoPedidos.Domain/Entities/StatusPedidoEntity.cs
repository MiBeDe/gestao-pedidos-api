using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Domain.Entities
{
    public class StatusPedidoEntity
    {
        [Key]
        public int IdStatus { get; private set; }
        public string Descricao { get; private set; }

        public StatusPedidoEntity() { }

        public StatusPedidoEntity(string descricao)
        {
            Descricao = descricao;
        }
    }
}

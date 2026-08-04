using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Domain.Entities
{
    public class PedidoProdutosEntity
    {
        [Key]
        public int IdPedidoProduto { get; private set; }
        public int IdPedido { get; set; }
        public int IdProduto { get; private set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal SubTotal { get; set; }


        public virtual ProdutoEntity Produto { get; private set; } = null!;
        public virtual PedidoEntity Pedido { get; set; } = null!;

        private PedidoProdutosEntity() { }

        public PedidoProdutosEntity(int idProduto)
        {
            IdProduto = idProduto;            
        }

    }
}

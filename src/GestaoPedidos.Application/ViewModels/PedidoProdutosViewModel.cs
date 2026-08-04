namespace GestaoPedidos.Application.ViewModels
{
    public class PedidoProdutosViewModel
    {
        public int? IdPedidoProduto { get; set; }
        public int? IdPedido { get; set; }
        public int IdProduto { get; set; }
        public decimal ValorUnitario { get; set; }
        public int Quantidade { get; set; }
        public decimal SubTotal { get; set; }

        public ProdutoViewModel? Produto { get; set; }
    }
}

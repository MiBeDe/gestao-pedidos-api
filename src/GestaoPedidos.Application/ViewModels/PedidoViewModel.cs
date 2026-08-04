namespace GestaoPedidos.Application.ViewModels
{
    public class PedidoViewModel
    {
        public int IdCliente { get; set; }
        public int IdStatus { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public IEnumerable<PedidoProdutosViewModel> Produtos { get; set; }
    }
}

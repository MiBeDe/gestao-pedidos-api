namespace GestaoPedidos.Application.ViewModels
{
    public class PedidoListViewModel
    {
        public int IdPedido { get; set; }
        public decimal ValorTotalPedido { get; set; }
        public ClienteViewModel Cliente { get; set; }
        public StatusPedidoViewModel StatusPedido { get; set; }
        public IEnumerable<PedidoProdutosViewModel> PedidoProdutos { get; set; }

    }
}

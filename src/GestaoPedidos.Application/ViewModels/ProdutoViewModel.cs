namespace GestaoPedidos.Application.ViewModels
{
    public class ProdutoViewModel
    {
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public int? QuantidadeSolicitado { get; set; }

    }
}

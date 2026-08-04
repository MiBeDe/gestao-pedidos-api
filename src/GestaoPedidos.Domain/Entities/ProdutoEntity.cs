using System.ComponentModel.DataAnnotations;

namespace GestaoPedidos.Domain.Entities
{
    public class ProdutoEntity
    {
        [Key]
        public int IdProduto { get; private set; }
        public string NomeProduto { get; private set; }
        public string Descricao { get; private set; }
        public decimal Preco { get; private set; }
        public int Quantidade { get; private set; }

        private ProdutoEntity() { }

        public ProdutoEntity(string nomeProduto, string descricao, decimal preco, int quantidade)
        {
            NomeProduto = nomeProduto;
            Descricao = descricao;
            Preco = preco;
            Quantidade = quantidade;
        }

        public void AlterarQuantidade(int quantidade)
        {
            Quantidade = quantidade;
        }
    }
}

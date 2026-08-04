namespace GestaoPedidos.Domain.Models
{
    public class PaginacaoResultModel<T>
    {
        public IReadOnlyCollection<T> Itens { get; set; } = [];
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas => (int)Math.Ceiling((double)TotalRegistros / TamanhoPagina);
        public bool PossuiProximaPagina => Pagina < TotalPaginas;
        public bool PossuiPaginaAnterior => Pagina > 1;
    }
}

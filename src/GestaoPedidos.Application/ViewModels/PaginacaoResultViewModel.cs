namespace GestaoPedidos.Application.ViewModels
{
    public class PaginacaoResultViewModel<T>
    {
        public IReadOnlyCollection<T> Itens { get; set; }
        public int Pagina { get; set; }
        public int TamanhoPagina { get; set; }
        public int TotalRegistros { get; set; }
        public int TotalPaginas { get; set; }
        public bool PossuiProximaPagina { get; set; }
        public bool PossuiPaginaAnterior { get; set; }
    }
}

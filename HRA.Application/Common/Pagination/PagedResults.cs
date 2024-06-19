namespace HRA.Application.Common.Pagination
{
    public class PagedResults<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalNumberOfPages { get; set; }//Numero total de grupos de pagina
        public int TotalNumberOfRecords { get; set; }
        public List<T> Data { get; set; } = new List<T>(); //COlección de elemento de cualquier elemento, este es generico, est inicializado con List<T>();
    }
}

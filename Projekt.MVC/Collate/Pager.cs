using Projekt.Service.Collate;

namespace Projekt.MVC.Collate
{
    public class Pager : IPager
    {
        public Pager(int currentPage, int pageSize)
        {
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}

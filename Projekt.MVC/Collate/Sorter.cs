using Projekt.Service.Collate;

namespace Projekt.MVC.Collate
{
    public class Sorter : ISorter
    {
        public Sorter(string sortDirection)
        {
            SortDirection = sortDirection;
        }

        public string SortDirection { get; set; }
    }
}

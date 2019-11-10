using Projekt.Service.Collate;

namespace Projekt.MVC.Collate
{
    public class Filter : IFilter
    {
        public Filter(string search, int? makeId)
        {
            Search = search;
            MakeId = makeId;
        }

        public string Search { get; set; }
        public int? MakeId { get; set; }
    }
}

namespace Projekt.Service.Collate
{
    public interface IPager
    {
        int CurrentPage { get; set; }
        int PageSize { get; set; }
    }
}

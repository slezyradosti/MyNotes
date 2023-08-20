namespace Domain.Repositories.Repos.DTOs
{
    public interface IFilter
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? SortColumn { get; set; }
        public string? SortOrder { get; set; }
        public string? FilterQuery { get; set; }
    }
}

namespace Application.Core
{
    public class PageList<T> : List<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int RecordCount { get; set; }

        public PageList(IEnumerable<T> items, int pageIndex, int pageSize, int recordCount)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            PageCount = (int)Math.Ceiling(recordCount / (double)pageSize);
            RecordCount = recordCount;
            AddRange(items);
        }
    }
}

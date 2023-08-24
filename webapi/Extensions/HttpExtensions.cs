using Application.Core;
using System.Text.Json;

namespace webapi.Extensions
{
    public static class HttpExtensions
    {
        public static void AddPaginationHeader<T>(this HttpResponse response,
            PageList<T> values)
        {
            var paginationHeader = new
            {
                values.PageIndex,
                values.PageSize,
                values.RecordCount,
                values.PageCount
            };

            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}

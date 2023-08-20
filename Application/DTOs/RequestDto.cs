using Domain.Repositories.Repos.DTOs;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class RequestDto : IFilter
    {
        [DefaultValue(0)]
        public int PageIndex { get; set; } = 0;

        [DefaultValue(10)]
        [Range(1, 100)]
        public int PageSize { get; set; } = 10;

        [DefaultValue("Id")]
        public string? SortColumn { get; set; } = "Id";

        [DefaultValue("ASC")]
        [RegularExpression("ASC|DESC")]
        public string? SortOrder { get; set; } = "ASC";

        [DefaultValue(null)]
        public string? FilterQuery { get; set; } = null;
    }
}

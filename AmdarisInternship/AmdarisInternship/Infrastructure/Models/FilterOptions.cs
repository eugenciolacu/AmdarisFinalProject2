using Microsoft.Data.SqlClient;

namespace AmdarisInternship.API.Infrastructure.Models
{
    public class FilterOptions
    {
        public string SearchTerm { get; set; }

        public SortOrder Order { get; set; }
    }
}

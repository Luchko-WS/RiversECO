using System.Collections.Generic;

namespace RiversECO.Models
{
    public class PagedList<T> : List<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Total { get; set; }
    }
}

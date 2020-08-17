using System.Collections.Generic;

namespace RiversECO.Dtos.Responses
{
    public class PagedListDto<T>
    {
        public PagedListDto()
        {
            Items = new List<T>();
        }

        public PageDto Page { get; set; }
        public List<T> Items { get; set; }
    }
}

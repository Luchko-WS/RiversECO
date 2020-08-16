using System.Net;

namespace RiversECO.Dtos.Responses
{
    public class ApiErrorDetails
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string Details { get; set; }
    }
}

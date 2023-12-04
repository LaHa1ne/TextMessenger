using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TextMessenger.DataLayer.Responses
{
    public class BaseResponse<T>
    {
        public string Description { get; set; } = null!;
        public HttpStatusCode StatusCode { get; set; }
        public T Data { get; set; }
    }
}

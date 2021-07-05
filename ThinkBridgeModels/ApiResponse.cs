using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ThinkBridgeModels
{
   public class ApiResponse
    {
        public string message { set; get; }
        public Boolean status { set; get; }
        public string error { set; get; }
        public dynamic data { set; get; }
        public HttpStatusCode statusCode { set; get; }
    }
}

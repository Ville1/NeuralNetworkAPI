using Microsoft.AspNetCore.Http;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class Response
    {
        public string Message { get; set; }

        public Response(string message)
        {
            Message = message;
        }

        public Response(HttpResponse response, HttpStatusCode code)
        {
            response.StatusCode = (int)code;
            Message = null;
        }

        public Response(HttpResponse response, HttpStatusCode code, string message)
        {
            response.StatusCode = (int)code;
            Message = message;
        }
    }
}

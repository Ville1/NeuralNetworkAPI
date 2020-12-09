using Microsoft.AspNetCore.Http;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class TeachResponse : Response
    {
        public float SucceesRate { get; set; }

        public TeachResponse(string message) : base(message)
        { }

        public TeachResponse(HttpResponse response, HttpStatusCode code) : base(response, code)
        { }

        public TeachResponse(HttpResponse response, HttpStatusCode code, string message) : base(response, code, message)
        { }
    }
}

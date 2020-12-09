using Microsoft.AspNetCore.Http;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class NetworkOutputResponse : Response
    {
        public string Output { get; set; }

        public NetworkOutputResponse(string message) : base(message)
        { }

        public NetworkOutputResponse(HttpResponse response, HttpStatusCode code) : base(response, code)
        { }

        public NetworkOutputResponse(HttpResponse response, HttpStatusCode code, string message) : base(response, code, message)
        { }
    }
}

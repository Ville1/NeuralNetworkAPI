using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class NetworkMetadataListResponse : Response
    {
        public List<NetworkMetadata> Networks { get; set; }

        public NetworkMetadataListResponse(string message) : base(message)
        { }

        public NetworkMetadataListResponse(HttpResponse response, HttpStatusCode code) : base(response, code)
        { }

        public NetworkMetadataListResponse(HttpResponse response, HttpStatusCode code, string message) : base(response, code, message)
        { }
    }
}

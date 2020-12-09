using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class NetworkMetadataResponse : Response
    {
        public NetworkMetadata Network { get; set; }

        public NetworkMetadataResponse(string message) : base(message)
        { }

        public NetworkMetadataResponse(HttpResponse response, HttpStatusCode code) : base(response, code)
        { }

        public NetworkMetadataResponse(HttpResponse response, HttpStatusCode code, string message) : base(response, code, message)
        { }
    }

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

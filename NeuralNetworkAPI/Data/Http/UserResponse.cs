using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Net;

namespace NeuralNetworkAPI.Data.Http
{
    public class UserResponse : Response
    {
        public List<UserResponseData> Users { get; set; }

        public UserResponse(string message) : base(message)
        { }

        public UserResponse(HttpResponse response, HttpStatusCode code) : base(response, code)
        { }

        public UserResponse(HttpResponse response, HttpStatusCode code, string message) : base(response, code, message)
        { }
    }

    public class UserResponseData
    {
        public long Id { get; set; }
        public string Username { get; set; }
    }
}

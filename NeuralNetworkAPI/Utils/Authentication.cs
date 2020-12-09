using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Repository;
using System;
using System.Linq;

namespace NeuralNetworkAPI.Utils
{
    public class Authentication
    {
        public static User GetUser(HttpRequest request)
        {
            StringValues values;
            if(!request.Headers.TryGetValue("Authorization", out values)) {
                return null;
            }
            string authorization = values[0];
            if(!authorization.StartsWith("Basic ")) {
                return null;
            }
            authorization = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authorization.Substring(6)));
            string[] authorizationArray = authorization.Split(":");
            return Repositories.Users.GetAll().FirstOrDefault(x => x.Username == authorizationArray[0] && x.Password == authorizationArray[1]);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using NeuralNetworkAPI.Data;
using NeuralNetworkAPI.Data.Http;
using NeuralNetworkAPI.Utils;
using System.Net;

namespace NeuralNetworkAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkController : ControllerBase
    {
        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public Response Create(NetworkMetadata network)
        {
            User user = Authentication.GetUser(Request);
            if(user == null) {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return new Response();
            }

            Response.StatusCode = 200;
            return new Response();
        }
    }
}
